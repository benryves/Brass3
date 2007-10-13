using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3.Attributes;

namespace Brass3 {
	public partial class TokenisedSource {

		/// <summary>
		/// Evaluate an entire expression.
		/// </summary>
		public Label EvaluateExpression(Compiler compiler) {
			for (int i = 0; i < this.tokens.Length; ++i) this.tokens[i].ExpressionGroup = 0;
			return this.EvaluateExpression(compiler, 0);
		}

		/// <summary>
		/// Evaluate an expression within the source line.
		/// </summary>
		/// <param name="index">The index of the expression to evaluate.</param>
		/// <remarks>The source must have been broken into expressions first, either by the assembler, a directive or an assignment.</remarks>
		public Label EvaluateExpression(Compiler compiler, int index) {

			if (this.tokens.Length == 0) throw new InvalidExpressionSyntaxExpection(OutermostTokenisedSource, "Nothing to evaluate.");

			LinkedList<LabelAccessor> LabelsToEvaluate = new LinkedList<LabelAccessor>();
			List<Operator> Operators = new List<Operator>(10);
			int BraceDepth = 0;
			Token PreviousToken = null;
			for (int i = 0; i < this.tokens.Length; ++i) {
				if (this.tokens[i].ExpressionGroup == index) {
					Token T = this.tokens[i];
					if (T.Type == Token.TokenTypes.Punctuation) {
						if (T.Data == "(") {
							++BraceDepth;
						} else if (T.IsCloseBracket) {
							--BraceDepth;
						} else {
							if (T.IsOpenBracket) ++BraceDepth; // "["


							LabelsToEvaluate.AddLast((LabelAccessor)null);
							Operator Op = new Operator(T, LabelsToEvaluate.Last, BraceDepth);
							if (Op.Type == Operator.OperatorType.Addition || Op.Type == Operator.OperatorType.Subtraction) {
								// Check for unaries:
								if ((PreviousToken == null) || (!(PreviousToken.Type == Token.TokenTypes.None || PreviousToken.Type == Token.TokenTypes.Function || PreviousToken.Type == Token.TokenTypes.String || (PreviousToken.Type == Token.TokenTypes.Punctuation && PreviousToken.IsCloseBracket)))) {
									switch (Op.Type) {
										case Operator.OperatorType.Addition:
											Op.Type = Operator.OperatorType.UnaryAddition;
											break;
										case Operator.OperatorType.Subtraction:
											Op.Type = Operator.OperatorType.UnarySubtraction;
											break;
									}
								}
							}
							Operators.Add(Op);
						}
					} else {

						// Check for function calls:
						
						

						// Handle function calls:
						if (T.Type == Token.TokenTypes.Function) {

							int FunctionOpenBracePosition = 0;

							if (T.Type == Token.TokenTypes.Function) {
								for (FunctionOpenBracePosition = i + 1; FunctionOpenBracePosition < this.tokens.Length; ++FunctionOpenBracePosition) {
									if (this.tokens[FunctionOpenBracePosition].ExpressionGroup != T.ExpressionGroup) continue;
									break;
								}
							}

							if (!compiler.Functions.PluginExists(T.Data)) {
								throw new CompilerExpection(T, "Function '" + T.Data + "' not declared.");
							}

							List<Token> InsideFunctionTokens = new List<Token>();
							int OriginalBraceDepth = BraceDepth;
							bool SkipFirstToken = true;
							for (i = FunctionOpenBracePosition; i < this.tokens.Length; ++i) {
								if (this.tokens[i].ExpressionGroup == index) {
									switch (this.tokens[i].Data) {
										case "(":
											++BraceDepth;
											break;
										case ")":
											--BraceDepth;
											if (BraceDepth == OriginalBraceDepth) {
												goto FoundInsideFunctionTokens;
											}
											break;
									}
									if (!SkipFirstToken) InsideFunctionTokens.Add(this.tokens[i]);
									SkipFirstToken = false;
								}
							}
						FoundInsideFunctionTokens:

							
							TokenisedSource SourceInsideFunction = new TokenisedSource(InsideFunctionTokens.ToArray(), this.OutermostTokenisedSource);
							int[] OriginalExpressionIndices = Array.ConvertAll<Token, int>(SourceInsideFunction.Tokens, delegate(Token Tok) { return Tok.ExpressionGroup; });

							// Call function...

							Plugins.IFunction Function = compiler.Functions[T.Data];

							Label ResultFromFunction = Function.Invoke(compiler, SourceInsideFunction, T.Data.ToLowerInvariant());

							for (int j = 0; j < OriginalExpressionIndices.Length; ++j) SourceInsideFunction.Tokens[j].ExpressionGroup = OriginalExpressionIndices[j];

							object[] o = Function.GetType().GetCustomAttributes(typeof(Attributes.SatisfiesAssignmentRequirementAttribute), true);
							if (o.Length > 0) {
								ResultFromFunction.IsConstant &= !(o[0] as Attributes.SatisfiesAssignmentRequirementAttribute).SatisfiesAssignmentRequirement;
							}


							// Chuck into stuff-to-evaluate list:
							LabelsToEvaluate.AddLast(new LabelAccessor(ResultFromFunction));


						} else {

							Label ToEvaluate;
							if (!compiler.Labels.TryParse(T, out ToEvaluate)) {
								// Create a temporary label:
								ToEvaluate = compiler.Labels.Create(T);
								ToEvaluate.ChangeCount = -1;
								ToEvaluate.Created = false;
							}
							LabelsToEvaluate.AddLast(new LabelAccessor(ToEvaluate));
						}
					}

					PreviousToken = T;

				}
			}

			if (Operators.Count == 0) {
				if (LabelsToEvaluate.Count != 1) throw new InvalidExpressionSyntaxExpection(OutermostTokenisedSource, "Too many constants left after evaluation.");
				return LabelsToEvaluate.First.Value.Label;
			}

			Operators.Sort();

			Dictionary<LabelAccessor, bool> EvaluatedTernaries = new Dictionary<LabelAccessor, bool>();

			foreach (Operator O in Operators) {

				switch (O.OperandCount) {
					case 1: {

							if (O.Type == Operator.OperatorType.LabelAccess) {

								LabelAccessor LabelToAccess = null;
								if (O.ExpressionPosition.Next != null && O.ExpressionPosition.Next.Value != null) {
									LabelToAccess = O.ExpressionPosition.Next.Value;
									LabelToAccess.AccessesPage = true;
								} else if (O.ExpressionPosition.Previous != null && O.ExpressionPosition.Previous.Value != null) {
									LabelToAccess = O.ExpressionPosition.Previous.Value;
									LabelToAccess.AccessesPage = false;
								} else {
									throw new InvalidExpressionSyntaxExpection(O.Token, "No label found for label access operator.");
								}

							} else {

								if (O.ExpressionPosition.Next == null) throw new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
								LabelAccessor Op = O.ExpressionPosition.Next.Value;
								LabelAccessor Result = Op;

								if (!O.IsAssignment) {
									Result = (LabelAccessor)Result.Clone();
									O.ExpressionPosition.List.AddAfter(O.ExpressionPosition.Next, Result);
									O.ExpressionPosition.List.Remove(O.ExpressionPosition.Next);
								} else {
									Result.Label.Created = true;
								}

								switch (O.Type) {
									case Operator.OperatorType.UnaryAddition:
										break;
									case Operator.OperatorType.UnaryBitwiseNot:
										Result.Label.Value = (double)~((int)Op.Label.Value);
										break;
									case Operator.OperatorType.UnaryDecrement:
										Result.Label.Value--;
										break;
									case Operator.OperatorType.UnaryIncrement:
										Result.Label.Value++;
										break;
									case Operator.OperatorType.UnaryLogicalNot:
										Result.Label.Value = (Op.Label.Value == 0f) ? 1f : 0f;
										break;
									case Operator.OperatorType.UnarySubtraction:
										Result.Label.Value = -Op.Label.Value;
										break;
									default:
										throw new CompilerExpection(O.Token, O.Type.ToString());
								}
							}

						} break;
					case 2: {


							if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Value == null) throw new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
							if (O.ExpressionPosition.Next == null || O.ExpressionPosition.Next.Value == null) throw new InvalidExpressionSyntaxExpection(O.Token, "Expected operand after operator.");

							LabelAccessor OpA = O.ExpressionPosition.Previous.Value;
							LabelAccessor OpB = O.ExpressionPosition.Next.Value;
							LabelAccessor Result = OpA;

							if (!O.IsAssignment) {
								Result = (LabelAccessor)Result.Clone();
								O.ExpressionPosition.List.AddBefore(O.ExpressionPosition.Previous, Result);
								O.ExpressionPosition.List.Remove(O.ExpressionPosition.Previous);
							} else {
								Result.Label.Created = true;
							}

							switch (O.Type) {

								// Power:
								case Operator.OperatorType.Power:
									Result.Label.Value = Math.Pow(OpA.Label.Value, OpB.Label.Value);
									break;

								// Arithmetic: multiplicative
								case Operator.OperatorType.Modulo:
									Result.Label.Value = OpA.Label.Value % OpB.Label.Value;
									break;
								case Operator.OperatorType.Multiplication:
								case Operator.OperatorType.AssignmentMultiplication:
									Result.Label.Value = OpA.Label.Value * OpB.Label.Value;
									break;
								case Operator.OperatorType.Division:
								case Operator.OperatorType.AssignmentDivision:
									Result.Label.Value = OpA.Label.Value / OpB.Label.Value;
									break;

								// Arithmetic: additive:
								case Operator.OperatorType.Addition:
								case Operator.OperatorType.AssignmentAddition:
									Result.Label.Value = OpA.Label.Value + OpB.Label.Value;
									break;
								case Operator.OperatorType.Subtraction:
								case Operator.OperatorType.AssignmentSubtraction:
									Result.Label.Value = OpA.Label.Value - OpB.Label.Value;
									break;

								// Shift:
								case Operator.OperatorType.ShiftLeft:
								case Operator.OperatorType.AssignmentShiftLeft:
									Result.Label.Value = ((int)OpA.Label.Value << (int)OpB.Label.Value);
									break;
								case Operator.OperatorType.ShiftRight:
								case Operator.OperatorType.AssignmentShiftRight:
									Result.Label.Value = ((int)OpA.Label.Value >> (int)OpB.Label.Value);
									break;

								// Relational and type testing:
								case Operator.OperatorType.GreaterOrEqualTo:
									Result.Label.Value = (OpA.Label.Value >= OpB.Label.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.LessOrEqualTo:
									Result.Label.Value = (OpA.Label.Value <= OpB.Label.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.GreaterThan:
									Result.Label.Value = (OpA.Label.Value > OpB.Label.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.LessThan:
									Result.Label.Value = (OpA.Label.Value < OpB.Label.Value) ? 1 : 0;
									break;

								// Equality:
								case Operator.OperatorType.NotEqual:
									Result.Label.Value = (OpA.Label.Value != OpB.Label.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.Equal:
									Result.Label.Value = (OpA.Label.Value == OpB.Label.Value) ? 1 : 0;
									break;

								// Bitwise:
								case Operator.OperatorType.AssignmentBitwiseXor:
								case Operator.OperatorType.BitwiseXor:
									Result.Label.Value = ((int)OpA.Label.Value ^ (int)OpB.Label.Value);
									break;
								case Operator.OperatorType.AssignmentBitwiseAnd:
								case Operator.OperatorType.BitwiseAnd:
									Result.Label.Value = ((int)OpA.Label.Value & (int)OpB.Label.Value);
									break;
								case Operator.OperatorType.AssignmentBitwiseOr:
								case Operator.OperatorType.BitwiseOr:
									Result.Label.Value = ((int)OpA.Label.Value | (int)OpB.Label.Value);
									break;

								// Conditional:
								case Operator.OperatorType.LogicalAnd:
									Result.Label.Value = ((OpA.Label.Value != 0) && (OpB.Label.Value != 0)) ? 1 : 0;
									break;
								case Operator.OperatorType.LogicalOr:
									Result.Label.Value = ((OpA.Label.Value != 0) || (OpB.Label.Value != 0)) ? 1 : 0;
									break;

								// Assignment:
								case Operator.OperatorType.AssignmentEqual:
									Result.Label.Value = OpB.Label.Value;
									break;

								// Indexing:
								case Operator.OperatorType.IndexingOpen:
									Result.Label.Value += OpB.Label.Value * OpA.Label.Size;

									// Now, here we have a special case:
									// token[index].field
									//             ^^^^^^
									// There will be NO OPERATOR BETWEEN THE TWO,
									// so fix that little issue...

									if (O.ExpressionPosition.Next.Next != null && O.ExpressionPosition.Next.Next.Value != null) {
										LabelAccessor FieldName = O.ExpressionPosition.Next.Next.Value;
										if (OpA.Label.Type == null) throw new CompilerExpection(OpA.Label.Token, "Couldn't get type information.");
										if (FieldName.Label.Name.Length > 0 && FieldName.Label.Name[0] == '.') {
											string Field = FieldName.Label.Name.Substring(1);
											DataStructure.Field SubField = (OpA.Label.Type as DataStructure)[Field];
											Result.Label.Value += SubField.Offset;
											Result.Label.Type = SubField.DataType;
											if (!FieldName.Label.Created) compiler.Labels.Remove(FieldName.Label);
											LabelsToEvaluate.Remove(O.ExpressionPosition.Next.Next);
										} else {
											throw new CompilerExpection(FieldName.Label.Token, "Expected field access.");
										}
									}

									break;
								
								default:
									throw new CompilerExpection(O.Token, O.Type.ToString());
							}

							LabelsToEvaluate.Remove(O.ExpressionPosition.Next);
						} break;
					case 3: {
							switch (O.Type) {
								case Operator.OperatorType.ConditionalQuery:
									if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Value == null) throw new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
									EvaluatedTernaries.Add(O.ExpressionPosition.Previous.Value, O.ExpressionPosition.Previous.Value.Label.Value != 0);
									break;
								case Operator.OperatorType.ConditionalResultSplitter:
									
									if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Previous == null || !EvaluatedTernaries.ContainsKey(O.ExpressionPosition.Previous.Previous.Value)) throw new InvalidExpressionSyntaxExpection(O.Token, "Missing matching conditional operator '?'.");
									bool Result = EvaluatedTernaries[O.ExpressionPosition.Previous.Previous.Value];
									EvaluatedTernaries.Remove(O.ExpressionPosition.Previous.Previous.Value);

									LabelAccessor Condition = (LabelAccessor)O.ExpressionPosition.Previous.Previous.Value.Clone();
									O.ExpressionPosition.List.Remove(O.ExpressionPosition.Previous.Previous);
									O.ExpressionPosition.List.AddBefore(O.ExpressionPosition.Previous, Condition);
									if (Condition.Label.Value != 0) {
										Condition.Label.Value = O.ExpressionPosition.Previous.Value.Label.Value;
									} else {
										Condition.Label.Value = O.ExpressionPosition.Next.Value.Label.Value;
									}
									O.ExpressionPosition.List.Remove(O.ExpressionPosition.Previous);
									O.ExpressionPosition.List.Remove(O.ExpressionPosition.Next);
									break;
								default:
									throw new CompilerExpection(O.Token, O.Type.ToString());
							}

						} break;
					default:
						throw new CompilerExpection(O.Token, O.OperandCount + " operands unsupported.");
				}
				LabelsToEvaluate.Remove(O.ExpressionPosition);
			}

			if (LabelsToEvaluate.Count != 1) throw new InvalidExpressionSyntaxExpection(OutermostTokenisedSource, "Too many tokens left.");
			return LabelsToEvaluate.First.Value.Label;

		}


		private class LabelAccessor : ICloneable {
			private Label label;
			public Label Label {
				get {
					label.AccessingPage = this.AccessesPage;
					return this.label;
				}
				set {
					this.label = value;
				}
			}
			public bool AccessesPage = false;
			public LabelAccessor(Label label) {
				this.label = label;
			}

			public object Clone() {
				Label L = label.Clone() as Label;
				LabelAccessor LA = new LabelAccessor(L);
				LA.AccessesPage = this.AccessesPage;
				return LA;
			}


		}

	}
}
