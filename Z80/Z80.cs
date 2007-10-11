using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using System.IO;
using Brass3.Plugins;
using System.ComponentModel;

namespace Z80 {

	[Description("An assembler for the ZiLOG Z80 microprocessor.")]
	public partial class Z80 : IAssembler {

		public string Name { get { return "z80"; } }

		private class Instruction {
			public int Index;
			public string InstructionName;
			public byte[] Opcodes;
			public int Size;
			public int Shift;
			public int Or;
			public TokenisedSource Arguments;
			public bool IndexHack = false;

			public enum InstructionRule {
				NoTouch,
				JmpPage,
				ZPage,
				R1, R2,
				CRel,
				Swap,
				Combine,
				CSwap,
				ZBit,
				ZIdX,
				MBit,
				MZero,
				ThreeArg,
				ThreeRel,
				T1,
				TDma,
				TAr,
				I1, I2, I3, I4, I5, I6, I7, I8,
				RST
			};
			public InstructionRule Rule;

			public int[] Variables = new int[0];

			public override string ToString() {
				return InstructionName + (Arguments == null ? "" : (" " + Arguments.ToString()));
			}


		}

		private Instruction[] Instructions;

		private Dictionary<string, List<Instruction>> InstructionMap;


		private void HighlightSource(TokenisedSource source, int index) {
			for (; index < source.Tokens.Length; ++index) {
				if (source.Tokens[index].IsExpressionOperand && source.Tokens[index].ExpressionGroup == 0) source.Tokens[index].Type = TokenisedSource.Token.TokenTypes.Instruction;
			}
		}

		private static bool DequeueToPointParens(Queue<TokenisedSource.Token> tokens, string match, int expressionGroup) {
			int ParenDepth = 0;
			while (tokens.Count > 0) {
				switch (tokens.Peek().Data) {
					case "(":
						++ParenDepth;
						break;
					case ")":
						--ParenDepth;
						break;
				}
				if (ParenDepth >= 0 && tokens.Peek().DataLowerCase != match) tokens.Peek().ExpressionGroup = expressionGroup;
				if (tokens.Dequeue().DataLowerCase == match && ParenDepth <= 0) return true;
			}
			return false;
		}

		public bool TryMatchSource(Compiler compiler, TokenisedSource source, int index) {

			List<Instruction> PossibleMatches;
			if (!InstructionMap.TryGetValue(source.Tokens[index].Data.ToLowerInvariant(), out PossibleMatches)) {
				return false;
			}

			foreach (Instruction Match in PossibleMatches) {

				foreach (TokenisedSource.Token Tok in source.Tokens) Tok.ExpressionGroup=0;

				Queue<TokenisedSource.Token> SourceQueue = new Queue<TokenisedSource.Token>(source.Tokens);
				for (int i = 0; i < index + 1; ++i) SourceQueue.Dequeue();
				Queue<TokenisedSource.Token> MatchQueue = new Queue<TokenisedSource.Token>(Match.Arguments.Tokens);

				bool Matched = true;
				int CurrentExpressionGroup = 1;
				while (Matched && MatchQueue.Count > 0 && SourceQueue.Count > 0) {

					TokenisedSource.Token TokenToMatch = MatchQueue.Dequeue();

					if (TokenToMatch.Data == "*") {

						if (MatchQueue.Count == 0) {
							while (SourceQueue.Count > 0) SourceQueue.Dequeue().ExpressionGroup = CurrentExpressionGroup;
							++CurrentExpressionGroup;
							break;
						}

						TokenToMatch = MatchQueue.Dequeue();

						if (!DequeueToPointParens(SourceQueue, TokenToMatch.DataLowerCase, CurrentExpressionGroup++)) {
							Matched = false;
							break;
						}

					} else if (TokenToMatch.Data == "(*)") {
						if (SourceQueue.Peek().DataLowerCase != "(") {
							Matched = false;
							break;
						}
						Matched = (DequeueToPointParens(SourceQueue, ")", CurrentExpressionGroup++));
					} else {
						if (TokenToMatch.DataLowerCase != SourceQueue.Dequeue().DataLowerCase) {
							Matched = false;
							break;
						}
					}
				}

				if (Matched && SourceQueue.Count == 0 && MatchQueue.Count == 0) {
					HighlightSource(source, index);
					source.MatchedItem = Match.Index;
					return true;
				}


			}
			
			// Now:
			/*foreach (Instruction I in PossibleMatches) {

				source.MatchedItem = I.Index;

				List<int> MatchedTokens = new List<int>();

				if (I.Arguments == null) {
					if (source.Tokens.Length == index + 1) {
						HighlightSource(source, index);
						return true; // Match!
					} else {
						continue;
					}
				}

				int ExpressionGrouper = 0;

				bool Matches = index + 1 != source.Tokens.Length;
				int i = 0;
				int j;
				int BraceDepth = 0;
				for (j = index + 1; i < I.Arguments.Tokens.Length && j < source.Tokens.Length; ++i, ++j) {

					string CurrentSourceToken = source.Tokens[j].DataLowerCase;
					if (CurrentSourceToken == "(") {
						++BraceDepth;
					} else if (CurrentSourceToken == ")") {
						--BraceDepth;
					}

					TokenisedSource.Token Reference = I.Arguments.Tokens[i];
					TokenisedSource.Token Test = source.Tokens[j];

					if (Reference.Data == "*") {

						++ExpressionGrouper;

						if (i == I.Arguments.Tokens.Length - 1) {  // We're at the last point anyway...
							for (; j < source.Tokens.Length; ++j) source.Tokens[j].ExpressionGroup = ExpressionGrouper;
							source.MatchedItem = I.Index;
							HighlightSource(source, index);
							return true;
						}

						TokenisedSource.Token EndOfWildcard = I.Arguments.Tokens[++i];
						for (; j < source.Tokens.Length; ++j) {
							string AttemptToEndWildcard = source.Tokens[j].DataLowerCase;
							if (AttemptToEndWildcard == "(") {
								++BraceDepth;
							} else if (AttemptToEndWildcard == ")") {
								--BraceDepth;
							}
							if (BraceDepth == 0 && AttemptToEndWildcard == EndOfWildcard.DataLowerCase) break;
							source.Tokens[j].ExpressionGroup = ExpressionGrouper;
						}

					} else {
						if (Reference.Type != Test.Type) {
							Matches = false;
							break; // Doesn't match.
						}

						if (Reference.DataLowerCase != Test.DataLowerCase) {
							Matches = false;
							break; // Doesn't match.
						}
					}


				}
				if (Matches && (i + 1) >= I.Arguments.Tokens.Length && j >= source.Tokens.Length) {
					HighlightSource(source, index);
					return true;
				}
			}*/

			return false;

		}

		/// <summary>
		/// Create an instance of a TASM assembler from a .tab file.
		/// </summary>
		/// <param name="tableFileName">The name of the table file to load.</param>
		public Z80(Compiler compiler) {

			this.InstructionMap = new Dictionary<string, List<Instruction>>(512);

			List<Instruction> Instructions = new List<Instruction>();
			foreach (string line in Properties.Resources.TASM80.Split('\n')) {
				string Line = line;
				if (Line.Trim() == "") continue;
				bool IndexHack = false;
			ReparseHack: ;
				string[] Components = Line.Trim().Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				Array.Resize<string>(ref Components, 8);

				string InstructionName = Components[0].ToLowerInvariant();

				List<Instruction> PossibleMatches;
				if (!InstructionMap.TryGetValue(InstructionName, out PossibleMatches)) {
					PossibleMatches = new List<Instruction>();
					InstructionMap.Add(InstructionName, PossibleMatches);
				}

				Instruction PossibleMatch = new Instruction();
				PossibleMatch.IndexHack = IndexHack;
				PossibleMatches.Add(PossibleMatch);

				PossibleMatch.Index = Instructions.Count;
				Instructions.Add(PossibleMatch);

				PossibleMatch.InstructionName = InstructionName;

				if (Components[1] != "\"\"") {
					TokenisedSource[] MatchingAssistant = TokenisedSource.FromString(compiler, Components[1]);
					if (MatchingAssistant.Length != 1) throw new InvalidDataException();

					List<TokenisedSource.Token> MergeTokens = new List<TokenisedSource.Token>();
					TokenisedSource.Token[] TokensToMerge = MatchingAssistant[0].GetCode().Tokens;
					for (int i = 0; i < TokensToMerge.Length; ++i) {
						if (TokensToMerge[i].Data == "(" && i < TokensToMerge.Length - 2 && TokensToMerge[i + 1].Data == "*" && TokensToMerge[i + 2].Data == ")") {
							MergeTokens.Add(new TokenisedSource.Token(null, TokenisedSource.Token.TokenTypes.None, "(*)", 0));
							i += 2;
						} else {
							MergeTokens.Add(TokensToMerge[i]);
						}
					}
					PossibleMatch.Arguments = new TokenisedSource(MergeTokens.ToArray());

					List<int> Variables = new List<int>();

					for (int i = 0; i < PossibleMatch.Arguments.Tokens.Length; ++i) {
						if (PossibleMatch.Arguments.Tokens[i].Data == "*" || PossibleMatch.Arguments.Tokens[i].Data == "(*)") Variables.Add(Variables.Count);
					}

					PossibleMatch.Variables = Variables.ToArray();
				} else {
					PossibleMatch.Arguments = new TokenisedSource(new TokenisedSource.Token[] { });
					PossibleMatch.Variables = new int[] { };
				}

				PossibleMatch.Opcodes = new byte[Components[2].Length / 2];
				for (int i = 0; i < PossibleMatch.Opcodes.Length; ++i) {
					PossibleMatch.Opcodes[PossibleMatch.Opcodes.Length - i - 1] = (byte)Convert.ToInt32(Components[2].Substring(i * 2, 2), 16);
				}
				
				PossibleMatch.Size = Convert.ToInt32(Components[3], 16);

				switch (Components[4]) {
					case "NOP":
						PossibleMatch.Rule = Instruction.InstructionRule.NoTouch;
						break;
					case "ZIX":
						PossibleMatch.Rule = Instruction.InstructionRule.ZIdX;
						break;
					case "ZBIT":
						PossibleMatch.Rule = Instruction.InstructionRule.ZBit;
						break;
					case "R1":
						PossibleMatch.Rule = Instruction.InstructionRule.R1;
						break;
					case "R2":
						PossibleMatch.Rule = Instruction.InstructionRule.R2;
						break;
					default:
						throw new Exception();
				}


				PossibleMatch.Shift = Components.Length >= 7 ? Convert.ToInt32(Components[6], 16) : 0;
				PossibleMatch.Or = Components.Length >= 8 ? Convert.ToInt32(Components[7], 16) : 0;

				if (IndexHack) {
					Instruction A = PossibleMatches[PossibleMatches.Count - 1];
					PossibleMatches[PossibleMatches.Count - 1] = PossibleMatches[PossibleMatches.Count - 2];
					PossibleMatches[PossibleMatches.Count - 2] = A;
				}

				if (Line.Contains("(IX*)") || Line.Contains("(IY*")) {
					Line = Line.Replace("(IX*)", "(IX)").Replace("(IY*)", "(IY)");
					IndexHack = true;
					goto ReparseHack;
				}

			}

			this.Instructions = Instructions.ToArray();
		}



		public void Assemble(Compiler compiler, TokenisedSource source, int index) {

			Instruction I = this.Instructions[source.MatchedItem];

			if (compiler.CurrentPass == AssemblyPass.Pass1) {
				compiler.IncrementProgramAndOutputCounters(I.Size);
			} else {

				int IndexHackOffset = I.IndexHack ? 1 : 0;
				int[] ExpressionParts = new int[I.Variables.Length + IndexHackOffset];
				for (int i = 0; i < I.Variables.Length; ++i) {
					ExpressionParts[i + IndexHackOffset] = (int)source.EvaluateExpression(compiler, i + 1).Value;
				}

				if (I.Rule == Instruction.InstructionRule.ZIdX && ExpressionParts.Length == 1) {
					ExpressionParts[0] &= 0xFF;
				}

				if (ExpressionParts.Length > 0 && I.Rule != Instruction.InstructionRule.ZBit && !(I.Rule == Instruction.InstructionRule.ZIdX && ExpressionParts.Length != 1)) {
					ExpressionParts[0] <<= I.Shift;
					if (I.Rule == Instruction.InstructionRule.Swap) {
						I.Rule = Instruction.InstructionRule.NoTouch;
						ExpressionParts[0] = ((ExpressionParts[0] & 0xFF) << 8) | (ExpressionParts[0] >> 8);
					}
					ExpressionParts[0] |= I.Or;
				}

				byte[] OutputData = new byte[I.Size];
				for (int i = 0; i < I.Opcodes.Length; ++i) {
					OutputData[i] = I.Opcodes[i];	
				}
				

				switch (I.Rule) {
					case Instruction.InstructionRule.NoTouch:
						for (int i = I.Opcodes.Length; i < I.Size; ++i) {
							OutputData[i] = (byte)ExpressionParts[0];
							ExpressionParts[0] >>= 8;
						}
						break;
					case Instruction.InstructionRule.R1:
						ExpressionParts[0] -= (int)(compiler.Labels.ProgramCounter.Value + I.Size);
						if (ExpressionParts[0] > 127 || ExpressionParts[0] < -128) {
							throw new CompilerExpection(source.Tokens[index], "Range of relative jump exceeded.");
						}
						OutputData[OutputData.Length - 1] = (byte)ExpressionParts[0];
						break;
					case Instruction.InstructionRule.R2:
						ExpressionParts[0] -= (int)(compiler.Labels.ProgramCounter.Value + I.Size);
						if (ExpressionParts[0] > 32767 || ExpressionParts[0] < -32768) {
							throw new CompilerExpection(source.Tokens[index], "Range of relative jump exceeded.");
						}
						OutputData[OutputData.Length - 2] = (byte)(ExpressionParts[0] & 0xFF);
						OutputData[OutputData.Length - 1] = (byte)(ExpressionParts[0] >> 8);
						break;
					case Instruction.InstructionRule.ZIdX:
						if (ExpressionParts.Length== 2) {
							for (int i = I.Opcodes.Length; i < I.Size; i++) {
								OutputData[i] = (byte)ExpressionParts[i - I.Opcodes.Length];
							}
						} else {
							for (int j = I.Opcodes.Length; j < I.Size; ++j) {
								OutputData[j] = (byte)(ExpressionParts[0] & 0xFF);
								ExpressionParts[0] >>= 8;
							}
						}

						break;
					case Instruction.InstructionRule.ZBit:
						if (ExpressionParts[0] < 0 || ExpressionParts[0] > 7) {
							throw new CompilerExpection(source.Tokens[index], "Bit index must be in the range 0-7 (not " + ExpressionParts[0] + ").");
						}
						ExpressionParts[0] *= 8;
						if (I.Size == 4) {
							int SecondArgument = ExpressionParts[1];
							
							if (SecondArgument > 127 || SecondArgument < -128) {
								throw new CompilerExpection(source.Tokens[index], "Range of IX must be between -128 and 127 (not " + SecondArgument + ").");
							}

							OutputData[2] = (byte)((SecondArgument | (I.Or & 0xFF)) & 0xFF);
							OutputData[3] = (byte)(ExpressionParts[0] | (I.Or >> 8));
						} else if (I.Size == 2) {
							OutputData[1] += (byte)(ExpressionParts[0]);
						} else {
							throw new CompilerExpection(source.Tokens[index], "ZBIT instruction not supported.");
						}
						break;
					case Instruction.InstructionRule.RST:
						//TODO: Check valid RST address.
						OutputData[0] = (byte)((int)OutputData[0] + ExpressionParts[0]);
						break;
					default:
						throw new NotImplementedException();
				}

				compiler.WriteOutput(OutputData);

			}

		}
	}

}