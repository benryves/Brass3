using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Description("Repeats a series of statements in a loop.")]
	[Syntax(".while condition\r\n\t[statements]\r\n.loop")]
	[Syntax(".rept count\r\n\t[statements]\r\n.loop")]
	[Syntax(".for variable is start to end [step step]\r\n\t[statements]\r\n.loop")]
	[Syntax(".for setup, condition, increment\r\n\t[statements]\r\n.loop")]
	[CodeExample("Outputs a 5x5 grid of '#' symbols.", "#rept 5\r\n\t.rept 5\r\n\t\t#echo \"#\"\r\n\t.loop\r\n\t.echoln\r\n#loop")]
	[CodeExample("C-style for-loop.", ".for i = 0, i < 10, ++i\r\n\t.echoln i\r\n.loop")]
	[CodeExample("BASIC-style for-loop.", ".for i is 10 to 1\r\n\t.echoln i, \"...\"\r\n.loop\r\n\r\n.echoln \"Blast off!\"")]
	[Remarks("Both a C-style for-loop and a BASIC-style for loop are provided.")]
	[Category("Flow Control")]
	[PluginName("while"), PluginName("rept"), PluginName("for"), PluginName("loop")]
	public class Repetition : IDirective {

		private class RepetitionStackEntry {
			public bool WasSuccessful = false;
			public LinkedListNode<Compiler.SourceStatement> SourcePosition;

			public int RepeatCount;

			public string RepeatType;

			public RepetitionStackEntry(string repeatType, bool wasSuccessful, LinkedListNode<Compiler.SourceStatement> sourcePosition) {
				this.RepeatType = repeatType;
				this.WasSuccessful = wasSuccessful;
				this.SourcePosition = sourcePosition;
			}

			public bool FirstTimeAround = true;

			public override string ToString() {
				switch (RepeatType) {
					case "rept":
						return RepeatType + " " + RepeatCount;
					default:
						return RepeatType;
				}
			}
		}

		private Stack<RepetitionStackEntry> RepetitionStack;
		private RepetitionStackEntry LastLoopHit;

		public Repetition(Compiler c) {
			this.RepetitionStack = new Stack<RepetitionStackEntry>();
			c.CompilationBegun += new EventHandler(delegate(object sender, EventArgs e) { this.RepetitionStack.Clear(); this.LastLoopHit = null; });

		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			switch (directive) {
				case "while":
					if (!compiler.IsSwitchedOn) break; {
						int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);
						bool WasSuccessful = source.EvaluateExpression(compiler, Args[0], false, true).NumericValue != 0;
						LinkedListNode<Compiler.SourceStatement> WhileLoopIndex = compiler.CurrentStatement;
						if (!WasSuccessful) {
							compiler.SwitchOff(typeof(Repetition));
						}
						RepetitionStack.Push(new RepetitionStackEntry("while", WasSuccessful, WhileLoopIndex));
					}
					break;
				case "for":
					if (!compiler.IsSwitchedOn) break; {
						int[] Args = source.GetCommaDelimitedArguments(index + 1, 1, 3);

						switch (Args.Length) {
							case 1: {
									int CurrentExpressionIndex = 1;
									DirectiveArgumentException BasicStyleForExection = new DirectiveArgumentException(source, Strings.ErrorRepetitionForBasicSyntax);
									int TokensInVariableBlock = 0;
									for (int i = index + 1; i < source.Tokens.Length; ++i) {
										source.Tokens[i].ExpressionGroup = 0;
										switch (source.Tokens[i].Data.ToLowerInvariant()) {
											case "is":
											case "=":
												if (CurrentExpressionIndex != 1) throw BasicStyleForExection;
												++CurrentExpressionIndex;
												break;
											case "to":
												if (CurrentExpressionIndex != 2) throw BasicStyleForExection;
												++CurrentExpressionIndex;
												break;
											case "step":
												if (CurrentExpressionIndex != 3) throw BasicStyleForExection;
												++CurrentExpressionIndex;
												break;
											default:
												if (CurrentExpressionIndex == 1) ++TokensInVariableBlock;
												if (TokensInVariableBlock > 1) throw BasicStyleForExection;
												source.Tokens[i].ExpressionGroup = CurrentExpressionIndex;
												break;
										}
									}


									Label Variable = source.EvaluateExpression(compiler, 1, true, false);
									double Start = source.EvaluateExpression(compiler, 2).NumericValue;
									double End = source.EvaluateExpression(compiler, 3).NumericValue;
									double Step = CurrentExpressionIndex == 3 ? Math.Sign(End - Start) : source.EvaluateExpression(compiler, 4).NumericValue;

									if (Math.Sign(Step) == 0 || Math.Sign(Step) != Math.Sign(End - Start)) throw new CompilerException(source, Strings.ErrorRepetitionInfiniteLoop);


									if (LastLoopHit == null) {
										Variable.NumericValue = Start;
									} else {
										Variable.NumericValue += Step;
									}

									bool WasSuccessful = (Start < End) ? (Variable.NumericValue <= End) : (Variable.NumericValue >= End);
									LinkedListNode<Compiler.SourceStatement> WhileLoopIndex = compiler.CurrentStatement;
									if (!WasSuccessful) {
										compiler.SwitchOff(typeof(Repetition));
									}
									RepetitionStack.Push(new RepetitionStackEntry("while", WasSuccessful, WhileLoopIndex));
								}

								break;
							case 3: {
									if (LastLoopHit == null) {
										source.EvaluateExpression(compiler, Args[0], false, true); // Execute the first bit.
									} else {
										source.EvaluateExpression(compiler, Args[2], false, true); // Execute the last bit.
									}

									bool WasSuccessful = source.EvaluateExpression(compiler, Args[1]).NumericValue != 0;
									LinkedListNode<Compiler.SourceStatement> WhileLoopIndex = compiler.RememberPosition();
									if (!WasSuccessful) {
										compiler.SwitchOff(typeof(Repetition));
									}
									RepetitionStack.Push(new RepetitionStackEntry("while", WasSuccessful, WhileLoopIndex));
								}
								break;
							default:
								throw new DirectiveArgumentException(source, Strings.ErrorRepetitionInvalidArgumentCount);
						}
						
					}
					LastLoopHit = null;
					break;
				case "rept":
				case "repeat":
					if (!compiler.IsSwitchedOn) break; {
						int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);
						int RepeatCount = (int)source.EvaluateExpression(compiler, Args[0]).NumericValue;
						LinkedListNode<Compiler.SourceStatement> WhileLoopIndex = compiler.CurrentStatement;
						if (RepeatCount < 1) compiler.SwitchOff(typeof(Repetition));
						RepetitionStackEntry RSE = new RepetitionStackEntry("rept", RepeatCount > 0, WhileLoopIndex);
						RSE.RepeatCount = RepeatCount;
						RepetitionStack.Push(RSE);
					}
					LastLoopHit = null;
					break;
				case "loop": {

						if (RepetitionStack.Count == 0) {
							compiler.SwitchOn();
							LastLoopHit = null;
						} else {


							this.LastLoopHit = RepetitionStack.Pop();

							bool CanRepeat = LastLoopHit.WasSuccessful;

							if (LastLoopHit.RepeatType == "rept") {
								CanRepeat = (--LastLoopHit.RepeatCount) > 0;
								if (CanRepeat) RepetitionStack.Push(LastLoopHit);
							}

							if (CanRepeat) {
								bool IsWhile = LastLoopHit.RepeatType == "while";
								compiler.RecallPosition(IsWhile ? LastLoopHit.SourcePosition : LastLoopHit.SourcePosition.Next, IsWhile);
							} else {
								LastLoopHit = null;
							}
							if (RepetitionStack.Count == 0 || Array.TrueForAll<RepetitionStackEntry>(RepetitionStack.ToArray(), delegate(RepetitionStackEntry e) { return e.WasSuccessful; })) {
								compiler.SwitchOn();
							}
						}
					}
					break;
			}

		}

	}
}
