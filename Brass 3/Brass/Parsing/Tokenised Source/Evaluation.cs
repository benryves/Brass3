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
			return EvaluateExpression(compiler, index, false);
		}

		/// <summary>
		/// Evaluate an expression within the source line.
		/// </summary>
		/// <param name="index">The index of the expression to evaluate.</param>
		/// <param name="canCreateImplicitLabels">True if labels can be implicitly created by the evaluation.</param>
		/// <remarks>The source must have been broken into expressions first, either by the assembler, a directive or an assignment.</remarks>
		public Label EvaluateExpression(Compiler compiler, int index, bool canCreateImplicitLabels) {


			List<Label> TempLabels = new List<Label>();

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
								//if (this.tokens[i].ExpressionGroup == index) {
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
								//}
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
								TempLabels.Add(ToEvaluate);
							}
							LabelsToEvaluate.AddLast(new LabelAccessor(ToEvaluate));
						}
					}

					PreviousToken = T;

				}
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
										Result.Label.NumericValue = (double)~((int)Op.Label.NumericValue);
										break;
									case Operator.OperatorType.UnaryDecrement:
										Result.Label.NumericValue--;
										break;
									case Operator.OperatorType.UnaryIncrement:
										Result.Label.NumericValue++;
										break;
									case Operator.OperatorType.UnaryLogicalNot:
										Result.Label.NumericValue = (Op.Label.NumericValue == 0f) ? 1f : 0f;
										break;
									case Operator.OperatorType.UnarySubtraction:
										Result.Label.NumericValue = -Op.Label.NumericValue;
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
									Result.Label.NumericValue = Math.Pow(OpA.Label.NumericValue, OpB.Label.NumericValue);
									break;

								// Arithmetic: multiplicative
								case Operator.OperatorType.Modulo:
									Result.Label.NumericValue = OpA.Label.NumericValue % OpB.Label.NumericValue;
									break;
								case Operator.OperatorType.Multiplication:
								case Operator.OperatorType.AssignmentMultiplication:
									Result.Label.NumericValue = OpA.Label.NumericValue * OpB.Label.NumericValue;
									break;
								case Operator.OperatorType.Division:
								case Operator.OperatorType.AssignmentDivision:
									Result.Label.NumericValue = OpA.Label.NumericValue / OpB.Label.NumericValue;
									break;

								// Arithmetic: additive:
								case Operator.OperatorType.Addition:
								case Operator.OperatorType.AssignmentAddition:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.StringValue = OpA.Label.StringValue + OpB.Label.StringValue;
									} else {
										Result.Label.NumericValue = OpA.Label.NumericValue + OpB.Label.NumericValue;
									}
									break;
								case Operator.OperatorType.Subtraction:
								case Operator.OperatorType.AssignmentSubtraction:
									Result.Label.NumericValue = OpA.Label.NumericValue - OpB.Label.NumericValue;
									break;

								// Shift:
								case Operator.OperatorType.ShiftLeft:
								case Operator.OperatorType.AssignmentShiftLeft:
									Result.Label.NumericValue = ((int)OpA.Label.NumericValue << (int)OpB.Label.NumericValue);
									break;
								case Operator.OperatorType.ShiftRight:
								case Operator.OperatorType.AssignmentShiftRight:
									Result.Label.NumericValue = ((int)OpA.Label.NumericValue >> (int)OpB.Label.NumericValue);
									break;

								// Relational and type testing:
								case Operator.OperatorType.GreaterOrEqualTo:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.NumericValue = OpA.Label.StringValue.CompareTo(OpB.Label.StringValue) >= 0 ? 1 : 0;
									} else {
										Result.Label.NumericValue = (OpA.Label.NumericValue >= OpB.Label.NumericValue) ? 1 : 0;
									}
									break;
								case Operator.OperatorType.LessOrEqualTo:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.NumericValue = OpA.Label.StringValue.CompareTo(OpB.Label.StringValue) <= 0 ? 1 : 0;
									} else {
										Result.Label.NumericValue = (OpA.Label.NumericValue <= OpB.Label.NumericValue) ? 1 : 0;
									}
									break;
								case Operator.OperatorType.GreaterThan:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.NumericValue = OpA.Label.StringValue.CompareTo(OpB.Label.StringValue) > 0 ? 1 : 0;
									} else {
										Result.Label.NumericValue = (OpA.Label.NumericValue > OpB.Label.NumericValue) ? 1 : 0;
									}
									break;
								case Operator.OperatorType.LessThan:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.NumericValue = OpA.Label.StringValue.CompareTo(OpB.Label.StringValue) < 0 ? 1 : 0;
									} else {
										Result.Label.NumericValue = (OpA.Label.NumericValue < OpB.Label.NumericValue) ? 1 : 0;
									}
									break;

								// Equality:
								case Operator.OperatorType.NotEqual:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.NumericValue = (OpA.Label.StringValue != OpB.Label.StringValue) ? 1 : 0;
									} else {
										Result.Label.NumericValue = (OpA.Label.NumericValue != OpB.Label.NumericValue) ? 1 : 0;
									}
									break;
								case Operator.OperatorType.Equal:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.NumericValue = (OpA.Label.StringValue == OpB.Label.StringValue) ? 1 : 0;
									} else {
										Result.Label.NumericValue = (OpA.Label.NumericValue == OpB.Label.NumericValue) ? 1 : 0;
									}
									break;

								// Bitwise:
								case Operator.OperatorType.AssignmentBitwiseXor:
								case Operator.OperatorType.BitwiseXor:
									Result.Label.NumericValue = ((int)OpA.Label.NumericValue ^ (int)OpB.Label.NumericValue);
									break;
								case Operator.OperatorType.AssignmentBitwiseAnd:
								case Operator.OperatorType.BitwiseAnd:
									if (OpA.Label.IsString || OpB.Label.IsString) {
										Result.Label.StringValue = OpA.Label.StringValue + OpB.Label.StringValue;
									} else {
										Result.Label.NumericValue = ((int)OpA.Label.NumericValue & (int)OpB.Label.NumericValue);
									}
									break;
								case Operator.OperatorType.AssignmentBitwiseOr:
								case Operator.OperatorType.BitwiseOr:
									Result.Label.NumericValue = ((int)OpA.Label.NumericValue | (int)OpB.Label.NumericValue);
									break;

								// Conditional:
								case Operator.OperatorType.LogicalAnd:
									Result.Label.NumericValue = ((OpA.Label.NumericValue != 0) && (OpB.Label.NumericValue != 0)) ? 1 : 0;
									break;
								case Operator.OperatorType.LogicalOr:
									Result.Label.NumericValue = ((OpA.Label.NumericValue != 0) || (OpB.Label.NumericValue != 0)) ? 1 : 0;
									break;

								// Assignment:
								case Operator.OperatorType.AssignmentEqual:
									if (OpB.Label.IsString) {
										Result.Label.StringValue = OpB.Label.StringValue;
									} else {
										Result.Label.NumericValue = OpB.Label.NumericValue;
									}
									break;

								// Indexing:
								case Operator.OperatorType.IndexingOpen:
									Result.Label.NumericValue += OpB.Label.NumericValue * OpA.Label.Size;

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
											Result.Label.NumericValue += SubField.Offset;
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
									EvaluatedTernaries.Add(O.ExpressionPosition.Previous.Value, O.ExpressionPosition.Previous.Value.Label.NumericValue != 0);
									break;
								case Operator.OperatorType.ConditionalResultSplitter:

									if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Previous == null || !EvaluatedTernaries.ContainsKey(O.ExpressionPosition.Previous.Previous.Value)) throw new InvalidExpressionSyntaxExpection(O.Token, "Missing matching conditional operator '?'.");
									bool Result = EvaluatedTernaries[O.ExpressionPosition.Previous.Previous.Value];
									EvaluatedTernaries.Remove(O.ExpressionPosition.Previous.Previous.Value);

									LabelAccessor Condition = (LabelAccessor)O.ExpressionPosition.Previous.Previous.Value.Clone();
									O.ExpressionPosition.List.Remove(O.ExpressionPosition.Previous.Previous);
									O.ExpressionPosition.List.AddBefore(O.ExpressionPosition.Previous, Condition);
									if (Condition.Label.NumericValue != 0) {
										Condition.Label.NumericValue = O.ExpressionPosition.Previous.Value.Label.NumericValue;
									} else {
										Condition.Label.NumericValue = O.ExpressionPosition.Next.Value.Label.NumericValue;
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

			try {
				if (LabelsToEvaluate.Count != 1) throw new InvalidExpressionSyntaxExpection(OutermostTokenisedSource, "Too many tokens left.");
				if (!canCreateImplicitLabels) {
					foreach (Label L in TempLabels) if (!L.Created) throw new InvalidOperationException("Labels cannot be implicitly created.");
				}

				return LabelsToEvaluate.First.Value.Label;
			} catch {

				foreach (Label L in TempLabels) {
					compiler.Labels.Remove(L);
				}

				throw;
			}

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
