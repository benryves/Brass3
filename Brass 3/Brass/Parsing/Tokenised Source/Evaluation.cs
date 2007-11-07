using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3.Attributes;

namespace Brass3 {
	public partial class TokenisedSource {

		#region Types

		/// <summary>
		/// Class used to reference labels.
		/// </summary>
		private class LabelAccessor : ICloneable {
			private Label label;
			/// <summary>
			/// Gets or sets the label being referenced.
			/// </summary>
			public Label Label {
				get { label.AccessingPage = this.AccessesPage; return this.label; }
				set { this.label = value; }
			}
			
			/// <summary>
			/// Gets or sets whether the label is being referenced by page.
			/// </summary>
			public bool AccessesPage = false;

			/// <summary>
			/// Createas an instance of the <see cref="LabelAccessor"/> from a label.
			/// </summary>
			/// <param name="label">The label we are accessing.</param>
			public LabelAccessor(Label label) {
				this.label = label;
			}

			/// <summary>
			/// Clone a label accessor and its contained label.
			/// </summary>
			public object Clone() {
				Label L = label.Clone() as Label;
				LabelAccessor LA = new LabelAccessor(L);
				LA.AccessesPage = this.AccessesPage;
				return LA;
			}
		}


		#endregion

		#region TryEvaluateExpression

		/// <summary>
		/// Try to evaluate an entire expression.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <param name="result">Outputs the result, if successful.</param>
		/// <param name="reasonForFailure">Outputs an exception containing the reason for failure, if any.</param>
		/// <returns>True if the expression was evaluated successfully, false otherwise.</returns>
		public bool TryEvaluateExpression(Compiler compiler, out Label result, out CompilerExpection reasonForFailure) {
			for (int i = 0; i < this.tokens.Length; ++i) this.tokens[i].ExpressionGroup = 0;
			return this.TryEvaluateExpression(compiler, 0, out result, out reasonForFailure);
		}

		/// <summary>
		/// Try to evaluate an expression within the source line.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <param name="index">The index of the expression to evaluate.</param>
		/// <param name="result">Outputs the result, if successful.</param>
		/// <param name="reasonForFailure">Outputs an exception containing the reason for failure, if any.</param>
		/// <remarks>The source must have been broken into expressions first, either by the assembler, a directive or an assignment.</remarks>
		/// <returns>True if the expression was evaluated successfully, false otherwise.</returns>
		public bool TryEvaluateExpression(Compiler compiler, int index, out Label result, out CompilerExpection reasonForFailure) {
			return this.TryEvaluateExpression(compiler, index, false, false, out result, out reasonForFailure);
		}

		/// <summary>
		/// Try to evaluate an expression within the source line.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <param name="index">The index of the expression to evaluate.</param>
		/// <param name="canCreateImplicitLabels">True if labels can be implicitly created by the evaluation.</param>
		/// <param name="result">Outputs the result, if successful.</param>
		/// <param name="reasonForFailure">Outputs an exception containing the reason for failure, if any.</param>
		/// <remarks>The source must have been broken into expressions first, either by the assembler, a directive or an assignment.</remarks>
		/// <returns>True if the expression was evaluated successfully, false otherwise.</returns>
		public bool TryEvaluateExpression(Compiler compiler, int index, bool canCreateImplicitLabels, bool canPerformAssignments, out Label result, out CompilerExpection reasonForFailure) {

			// Set the default outputs...
			reasonForFailure = default(CompilerExpection);
			result = default(Label);

			// Check for reusables first;
			bool IsReusableLabel = true;
			char? ReusableChar = null;
			string ReusableName = "";

			for (int i = 0; i < this.tokens.Length; ++i) {
				if (tokens[i].ExpressionGroup == index) {
					if (tokens[i].type != Token.TokenTypes.Punctuation) {
						IsReusableLabel = false;
						break;
					} else {
						if (tokens[i].Data == "+" || tokens[i].Data == "++") {
							if (ReusableChar == null) {
								ReusableChar = '+';
								ReusableName = tokens[i].Data;
							} else if (tokens[i].Data[0] != ReusableChar) {
								IsReusableLabel = false;
								break;
							} else {
								ReusableName += tokens[i].Data;
							}
						} else if (tokens[i].Data == "-" || tokens[i].Data == "--") {
							if (ReusableChar == null) {
								ReusableChar = '-';
								ReusableName = tokens[i].Data;
							} else if (tokens[i].Data[0] != ReusableChar) {
								IsReusableLabel = false;
								break;
							} else {
								ReusableName += tokens[i].Data;
							}
						} else if (tokens[i].Data == ":") {
							ReusableName += ":";
						} else {
							IsReusableLabel = false;
							break;
						}
					}
				}
			}




			if (IsReusableLabel) {

				if (ReusableName.StartsWith(":") && !ReusableName.EndsWith(":")) {
					ReusableName = ReusableName.Substring(1);
				} else if (ReusableName.EndsWith(":") && !ReusableName.StartsWith(":")) {
					ReusableName = ReusableName.Remove(ReusableName.Length - 1);
				}


				Label Reusable;
				if (canCreateImplicitLabels && compiler.CurrentPass == AssemblyPass.Pass1) {
					Reusable = compiler.Labels.CreateReusable(ReusableName);
					Reusable.Created = false;
				} else {
					if (!compiler.Labels.TryParse(new Token(ReusableName), out Reusable)) {
						reasonForFailure = new CompilerExpection(this, string.Format("Couldn't get value for reusable label '{0}'.", ReusableName));
						return false;
					}
				}
				result = Reusable;
				return true;
			}

			List<Label> TempLabels = new List<Label>();

			if (this.tokens.Length == 0) {
				reasonForFailure = new InvalidExpressionSyntaxExpection(OutermostTokenisedSource, "Nothing to evaluate.");
				return false;
			}

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

							if (!canPerformAssignments && Op.IsAssignment) {
								reasonForFailure = new CompilerExpection(T, "You cannot perform assignments in this expression.");
								return false;
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

							if (!compiler.Functions.Contains(T.Data)) {
								reasonForFailure = new CompilerExpection(T, "Function '" + T.Data + "' not declared.");
								return false;
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
									reasonForFailure = new InvalidExpressionSyntaxExpection(O.Token, "No label found for label access operator.");
									return false;
								}

							} else {

								if (O.ExpressionPosition.Next == null) {
									reasonForFailure = new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
									return false;
								}

								LabelAccessor Op = O.ExpressionPosition.Next.Value;
								LabelAccessor Result = Op;

								if (!O.IsAssignment) {
									
									try {
										Result = (LabelAccessor)Result.Clone();
									} catch (CompilerExpection c) {
										reasonForFailure = c;
										return false;
									}

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
										reasonForFailure = new CompilerExpection(O.Token, O.Type.ToString());
										return false;
								}
							}

						} break;
					case 2: {


							if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Value == null) {
								reasonForFailure = new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
								return false;
							}

							if (O.ExpressionPosition.Next == null || O.ExpressionPosition.Next.Value == null) {
								reasonForFailure = new InvalidExpressionSyntaxExpection(O.Token, "Expected operand after operator.");
								return false;
							}

							LabelAccessor OpA = O.ExpressionPosition.Previous.Value;
							LabelAccessor OpB = O.ExpressionPosition.Next.Value;
							LabelAccessor Result = OpA;

							if (!O.IsAssignment) {
								
								try {
									Result = (LabelAccessor)Result.Clone();
								} catch (CompilerExpection c) {
									reasonForFailure = c;
									return false;
								}

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
										if (OpA.Label.Type == null) {
											reasonForFailure = new CompilerExpection(OpA.Label.Token, "Couldn't get type information.");
											return false;
										}
										if (FieldName.Label.Name.Length > 0 && FieldName.Label.Name[0] == '.') {
											string Field = FieldName.Label.Name.Substring(1);
											DataStructure.Field SubField = (OpA.Label.Type as DataStructure)[Field];
											Result.Label.NumericValue += SubField.Offset;
											Result.Label.Type = SubField.DataType;
											if (!FieldName.Label.Created) compiler.Labels.Remove(FieldName.Label);
											LabelsToEvaluate.Remove(O.ExpressionPosition.Next.Next);
										} else {
											reasonForFailure = new CompilerExpection(FieldName.Label.Token, "Expected field access.");
											return false;
										}
									}

									break;

								default:
									reasonForFailure = new CompilerExpection(O.Token, O.Type.ToString());
									return false;
							}

							LabelsToEvaluate.Remove(O.ExpressionPosition.Next);
						} break;
					case 3: {
							switch (O.Type) {
								case Operator.OperatorType.ConditionalQuery:
									if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Value == null) {
										reasonForFailure = new InvalidExpressionSyntaxExpection(O.Token, "Expected operand before operator.");
										return false;
									}
									EvaluatedTernaries.Add(O.ExpressionPosition.Previous.Value, O.ExpressionPosition.Previous.Value.Label.NumericValue != 0);
									break;
								case Operator.OperatorType.ConditionalResultSplitter:

									if (O.ExpressionPosition.Previous == null || O.ExpressionPosition.Previous.Previous == null || !EvaluatedTernaries.ContainsKey(O.ExpressionPosition.Previous.Previous.Value)) {
										reasonForFailure = new InvalidExpressionSyntaxExpection(O.Token, "Missing matching conditional operator '?'.");
										return false;
									}

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
									reasonForFailure = new CompilerExpection(O.Token, O.Type.ToString());
									return false;
							}

						} break;
					default:
						reasonForFailure = new CompilerExpection(O.Token, O.OperandCount + " operands unsupported.");
						return false;
				}
				LabelsToEvaluate.Remove(O.ExpressionPosition);
			}


			if (LabelsToEvaluate.Count != 1) {
				reasonForFailure = new InvalidExpressionSyntaxExpection(OutermostTokenisedSource, "Syntax error.");
			} else {
				if (!canCreateImplicitLabels) {
					foreach (Label L in TempLabels) {
						if (!L.Created) {
							string Error = string.Format("Label '{0}' not found.", L.Name);
							if (L.Token == null) {
								reasonForFailure = new InvalidExpressionSyntaxExpection(this, Error);
							} else {
								reasonForFailure = new InvalidExpressionSyntaxExpection(L.Token, Error);
							}
						}
					}
				}
			}

			if (reasonForFailure != null) {
				foreach (Label L in TempLabels) {
					compiler.Labels.Remove(L);
				}
				return false;
			} else {
				result = LabelsToEvaluate.First.Value.Label;
				return true;
			}

		}


		#endregion

		#region EvaluateExpression

		/// <summary>
		/// Evaluate an entire expression.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <returns>The result of the evaluation.</returns>
		public Label EvaluateExpression(Compiler compiler) {
			Label L; CompilerExpection E;
			return ThrowOrReturn(this.TryEvaluateExpression(compiler, out L, out E), L, E);
		}

		/// <summary>
		/// Evaluate an expression within the source line.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <param name="index">The index of the expression to evaluate.</param>
		/// <remarks>The source must have been broken into expressions first, either by the assembler, a directive or an assignment.</remarks>
		/// <returns>The result of the evaluation.</returns>
		public Label EvaluateExpression(Compiler compiler, int index) {
			Label L; CompilerExpection E;
			return ThrowOrReturn(this.TryEvaluateExpression(compiler, index, out L, out E), L, E);
		}

		/// <summary>
		/// Evaluate an expression within the source line.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <param name="index">The index of the expression to evaluate.</param>
		/// <param name="canCreateImplicitLabels">True if labels can be implicitly created by the evaluation.</param>
		/// <remarks>The source must have been broken into expressions first, either by the assembler, a directive or an assignment.</remarks>
		/// <returns>The result of the evaluation.</returns>
		public Label EvaluateExpression(Compiler compiler, int index, bool canCreateImplicitLabels, bool canPerformAssignments) {
			Label L; CompilerExpection E;
			return ThrowOrReturn(this.TryEvaluateExpression(compiler, index, canCreateImplicitLabels, canPerformAssignments, out L, out E), L, E);
		}

		/// <summary>
		/// Throw an exception (on failure) or return a label (on success).
		/// </summary>
		/// <param name="success">True on success, false on failure.</param>
		/// <param name="result">The label to return if successful.</param>
		/// <param name="reasonForFailure">The exception to throw if not successful.</param>
		private Label ThrowOrReturn(bool success, Label result, CompilerExpection reasonForFailure) {
			if (success) {
				return result;
			} else {
				throw reasonForFailure;
			}
		}

		#endregion

	}
}