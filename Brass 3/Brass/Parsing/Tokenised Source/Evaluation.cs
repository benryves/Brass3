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

			LinkedList<Label> LabelsToEvaluate = new LinkedList<Label>();
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


							LabelsToEvaluate.AddLast((Label)null);
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
							LabelsToEvaluate.AddLast(ResultFromFunction);


						} else {

							Label ToEvaluate;
							if (!compiler.Labels.TryParse(T, out ToEvaluate)) {
								// Create a temporary label:
								ToEvaluate = compiler.Labels.Create(T);
								ToEvaluate.ChangeCount = -1;
								ToEvaluate.Created = false;
							}
							LabelsToEvaluate.AddLast(ToEvaluate);
						}
					}

					PreviousToken = T;

				}
			}

			if (Operators.Count == 0) {
				if (LabelsToEvaluate.Count != 1) throw new InvalidExpressionSyntaxExpection(OutermostTokenisedSource, "Too many constants left after evaluation.");
				return LabelsToEvaluate.First.Value;
			}

			Operators.Sort();

			Dictionary<Label, bool> EvaluatedTernaries = new Dictionary<Label, bool>();

			foreach (Operator O in Operators) {

				switch (O.OperandCount) {
					case 1: {
							if (O.ExpressionPosition.Next == null) throw new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
							Label Op = O.ExpressionPosition.Next.Value;
							Label Result = Op;

							if (!O.IsAssignment) {
								Result = (Label)Result.Clone();
								O.ExpressionPosition.List.AddAfter(O.ExpressionPosition.Next, Result);
								O.ExpressionPosition.List.Remove(O.ExpressionPosition.Next);
							} else {
								Result.Created = true;
							}

							switch (O.Type) {
								case Operator.OperatorType.UnaryAddition:
									break;
								case Operator.OperatorType.UnaryBitwiseNot:
									Result.Value = (double)~((int)Op.Value);
									break;
								case Operator.OperatorType.UnaryDecrement:
									Result.Value--;
									break;
								case Operator.OperatorType.UnaryIncrement:
									Result.Value++;
									break;
								case Operator.OperatorType.UnaryLogicalNot:
									Result.Value = (Op.Value == 0f) ? 1f : 0f;
									break;
								case Operator.OperatorType.UnarySubtraction:
									Result.Value = -Op.Value;
									break;
								default:
									throw new CompilerExpection(O.Token, O.Type.ToString());
							}

						} break;
					case 2: {


							if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Value == null) throw new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
							if (O.ExpressionPosition.Next == null || O.ExpressionPosition.Next.Value == null) throw new InvalidExpressionSyntaxExpection(O.Token, "Expected operand after operator.");

							Label OpA = O.ExpressionPosition.Previous.Value;
							Label OpB = O.ExpressionPosition.Next.Value;
							Label Result = OpA;

							if (!O.IsAssignment) {
								Result = (Label)Result.Clone();
								O.ExpressionPosition.List.AddBefore(O.ExpressionPosition.Previous, Result);
								O.ExpressionPosition.List.Remove(O.ExpressionPosition.Previous);
							} else {
								Result.Created = true;
							}

							switch (O.Type) {

								// Power:
								case Operator.OperatorType.Power:
									Result.Value = Math.Pow(OpA.Value, OpB.Value);
									break;

								// Arithmetic: multiplicative
								case Operator.OperatorType.Modulo:
									Result.Value = OpA.Value % OpB.Value;
									break;
								case Operator.OperatorType.Multiplication:
								case Operator.OperatorType.AssignmentMultiplication:
									Result.Value = OpA.Value * OpB.Value;
									break;
								case Operator.OperatorType.Division:
								case Operator.OperatorType.AssignmentDivision:
									Result.Value = OpA.Value / OpB.Value;
									break;

								// Arithmetic: additive:
								case Operator.OperatorType.Addition:
								case Operator.OperatorType.AssignmentAddition:
									Result.Value = OpA.Value + OpB.Value;
									break;
								case Operator.OperatorType.Subtraction:
								case Operator.OperatorType.AssignmentSubtraction:
									Result.Value = OpA.Value - OpB.Value;
									break;

								// Shift:
								case Operator.OperatorType.ShiftLeft:
								case Operator.OperatorType.AssignmentShiftLeft:
									Result.Value = ((int)OpA.Value << (int)OpB.Value);
									break;
								case Operator.OperatorType.ShiftRight:
								case Operator.OperatorType.AssignmentShiftRight:
									Result.Value = ((int)OpA.Value >> (int)OpB.Value);
									break;

								// Relational and type testing:
								case Operator.OperatorType.GreaterOrEqualTo:
									Result.Value = (OpA.Value >= OpB.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.LessOrEqualTo:
									Result.Value = (OpA.Value <= OpB.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.GreaterThan:
									Result.Value = (OpA.Value > OpB.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.LessThan:
									Result.Value = (OpA.Value < OpB.Value) ? 1 : 0;
									break;

								// Equality:
								case Operator.OperatorType.NotEqual:
									Result.Value = (OpA.Value != OpB.Value) ? 1 : 0;
									break;
								case Operator.OperatorType.Equal:
									Result.Value = (OpA.Value == OpB.Value) ? 1 : 0;
									break;

								// Bitwise:
								case Operator.OperatorType.AssignmentBitwiseXor:
								case Operator.OperatorType.BitwiseXor:
									Result.Value = ((int)OpA.Value ^ (int)OpB.Value);
									break;
								case Operator.OperatorType.AssignmentBitwiseAnd:
								case Operator.OperatorType.BitwiseAnd:
									Result.Value = ((int)OpA.Value & (int)OpB.Value);
									break;
								case Operator.OperatorType.AssignmentBitwiseOr:
								case Operator.OperatorType.BitwiseOr:
									Result.Value = ((int)OpA.Value | (int)OpB.Value);
									break;

								// Conditional:
								case Operator.OperatorType.LogicalAnd:
									Result.Value = ((OpA.Value != 0) && (OpB.Value != 0)) ? 1 : 0;
									break;
								case Operator.OperatorType.LogicalOr:
									Result.Value = ((OpA.Value != 0) || (OpB.Value != 0)) ? 1 : 0;
									break;

								// Assignment:
								case Operator.OperatorType.AssignmentEqual:
									Result.Value = OpB.Value;
									break;

								// Indexing:
								case Operator.OperatorType.IndexingOpen:
									Result.Value += OpB.Value * OpA.Size;

									// Now, here we have a special case:
									// token[index].field
									//             ^^^^^^
									// There will be NO OPERATOR BETWEEN THE TWO,
									// so fix that little issue...

									if (O.ExpressionPosition.Next.Next != null && O.ExpressionPosition.Next.Next.Value != null) {
										Label FieldName = O.ExpressionPosition.Next.Next.Value;
										if (OpA.Type == null) throw new CompilerExpection(OpA.Token, "Couldn't get type information.");
										if (FieldName.Name.Length > 0 && FieldName.Name[0] == '.') {
											string Field = FieldName.Name.Substring(1);
											DataStructure.Field SubField = (OpA.Type as DataStructure)[Field];
											Result.Value += SubField.Offset;
											Result.Type = SubField.DataType;
											if (!FieldName.Created) compiler.Labels.Remove(FieldName);
											LabelsToEvaluate.Remove(O.ExpressionPosition.Next.Next);
										} else {
											throw new CompilerExpection(FieldName.Token, "Expected field access.");
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
									EvaluatedTernaries.Add(O.ExpressionPosition.Previous.Value, O.ExpressionPosition.Previous.Value.Value != 0);
									break;
								case Operator.OperatorType.ConditionalResultSplitter:
									
									if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Previous == null || !EvaluatedTernaries.ContainsKey(O.ExpressionPosition.Previous.Previous.Value)) throw new InvalidExpressionSyntaxExpection(O.Token, "Missing matching conditional operator '?'.");
									bool Result = EvaluatedTernaries[O.ExpressionPosition.Previous.Previous.Value];
									EvaluatedTernaries.Remove(O.ExpressionPosition.Previous.Previous.Value);

									Label Condition = (Label)O.ExpressionPosition.Previous.Previous.Value.Clone();
									O.ExpressionPosition.List.Remove(O.ExpressionPosition.Previous.Previous);
									O.ExpressionPosition.List.AddBefore(O.ExpressionPosition.Previous, Condition);
									if (Condition.Value != 0) {
										Condition.Value = O.ExpressionPosition.Previous.Value.Value;
									} else {
										Condition.Value = O.ExpressionPosition.Next.Value.Value;
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
			return LabelsToEvaluate.First.Value;

		}	
	
	}
}
