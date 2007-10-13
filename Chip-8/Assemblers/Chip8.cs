using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Chip8.Assemblers {
	public class Chip8 : IAssembler {
	
		public enum InstructionType {
			ScDown,
            Cls,
            Rts,
            ScRight,
            ScLeft,
            Low,
            High,
            Jmp,
            Jsr,
            SkEqImmediate,
            SkNeImmediate,
            SkEqRegister,
            MovRegImmediate,
            MovRegReg,
            AddRegImmediate,
            OrRegReg,
            AndRegReg,
            XorRegReg,
            AddRegReg,
            SubRegReg,
            Shr,
            Rsb,
            Shl,
            SkNeRegister,
            Mvi,
            Jmi,
            Rand,
            Sprite,
            XSprite,
            SkPr,
            SkUp,
            GDelay,
            Key,
            SDelay,
            SSound,
            Adi,
            Font,
            XFont,
            Bcd,
            Str,
            Ldr,
		}

		public enum ArgumentType {
			Register,
			Immediate,
		}

		static ArgumentType[] GetArguments(InstructionType instruction) {
			switch (instruction) {
				case InstructionType.ScDown:
					return new ArgumentType[] { ArgumentType.Immediate };
				case InstructionType.Cls:
					return new ArgumentType[] { };
				case InstructionType.Rts:
					return new ArgumentType[] { };
				case InstructionType.ScRight:
					return new ArgumentType[] { };
				case InstructionType.ScLeft:
					return new ArgumentType[] { };
				case InstructionType.Low:
					return new ArgumentType[] { };
				case InstructionType.High:
					return new ArgumentType[] { };
				case InstructionType.Jmp:
					return new ArgumentType[] { ArgumentType.Immediate };
				case InstructionType.Jsr:
					return new ArgumentType[] { ArgumentType.Immediate };
				case InstructionType.SkEqImmediate:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Immediate };
				case InstructionType.SkNeImmediate:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Immediate };
				case InstructionType.SkEqRegister:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.MovRegImmediate:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Immediate };
				case InstructionType.MovRegReg:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.AddRegImmediate:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Immediate };
				case InstructionType.OrRegReg:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.AndRegReg:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register};
				case InstructionType.XorRegReg:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register};
				case InstructionType.AddRegReg:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.SubRegReg:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.Shr:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.Rsb:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.Shl:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.SkNeRegister:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.Mvi:
					return new ArgumentType[] { ArgumentType.Immediate };
				case InstructionType.Jmi:
					return new ArgumentType[] { ArgumentType.Immediate };
				case InstructionType.Rand:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Immediate };
				case InstructionType.Sprite:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register, ArgumentType.Immediate };
				case InstructionType.XSprite:
					return new ArgumentType[] { ArgumentType.Register, ArgumentType.Register };
				case InstructionType.SkPr:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.SkUp:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.GDelay:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.Key:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.SDelay:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.SSound:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.Adi:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.Font:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.XFont:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.Bcd:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.Str:
					return new ArgumentType[] { ArgumentType.Register };
				case InstructionType.Ldr:
					return new ArgumentType[] { ArgumentType.Register };
				default:
					throw new InvalidOperationException();
			}
		}

		public string Name { get { return "chip8"; } }

		/// <summary>
		/// Returns true if a token is a valid register name.
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		private static bool IsRegisterName(TokenisedSource.Token token) {
			const string ValidRegisterNames = "0123456789AaBbCcDdEeFf";
			return (token.Data.Length == 2 && char.ToLowerInvariant(token.Data[0]) == 'v' && ValidRegisterNames.IndexOf(token.Data[1]) != -1);
		}

		/// <summary>
		/// Returns true if a chunk of source is a valid register name.
		/// </summary>
		/// <param name="source">The source to check.</param>
		private static bool IsRegisterName(TokenisedSource source) {
			return source.Tokens.Length == 1 && IsRegisterName(source.Tokens[0]);
		}

		public bool TryMatchSource(Compiler compiler, TokenisedSource source, int index) {

			// Search for matching instructions:
			List<InstructionType> MatchedInstructions = new List<InstructionType>();

			#region Grab instructions by name

			switch (source.Tokens[index].DataLowerCase) {
				case "scdown":
					MatchedInstructions.Add(InstructionType.ScDown);
					break;
				case "cls":
					MatchedInstructions.Add(InstructionType.Cls);
					break;
				case "rts":
					MatchedInstructions.Add(InstructionType.Rts);
					break;
				case "scleft":
					MatchedInstructions.Add(InstructionType.ScLeft);
					break;
				case "scright":
					MatchedInstructions.Add(InstructionType.ScRight);
					break;
				case "low":
					MatchedInstructions.Add(InstructionType.Low);
					break;
				case "high":
					MatchedInstructions.Add(InstructionType.High);
					break;
				case "jmp":
					MatchedInstructions.Add(InstructionType.Jmp);
					break;
				case "jsr":
					MatchedInstructions.Add(InstructionType.Jsr);
					break;
				case "skeq":
					MatchedInstructions.Add(InstructionType.SkEqImmediate);
					MatchedInstructions.Add(InstructionType.SkEqRegister);
					break;
				case "skne":
					MatchedInstructions.Add(InstructionType.SkNeImmediate);
					MatchedInstructions.Add(InstructionType.SkNeRegister);
					break;
				case "mov":
					MatchedInstructions.Add(InstructionType.MovRegImmediate);
					MatchedInstructions.Add(InstructionType.MovRegReg);
					break;
				case "or":
					MatchedInstructions.Add(InstructionType.OrRegReg);
					break;
				case "and":
					MatchedInstructions.Add(InstructionType.AndRegReg);
					break;
				case "xor":
					MatchedInstructions.Add(InstructionType.XorRegReg);
					break;
				case "add":
					MatchedInstructions.Add(InstructionType.AddRegImmediate);
					MatchedInstructions.Add(InstructionType.AddRegReg);
					break;
				case "sub":
					MatchedInstructions.Add(InstructionType.SubRegReg);
					break;
				case "shr":
					MatchedInstructions.Add(InstructionType.Shr);
					break;
				case "shl":
					MatchedInstructions.Add(InstructionType.Shl);
					break;
				case "mvi":
					MatchedInstructions.Add(InstructionType.Mvi);
					break;
				case "jmi":
					MatchedInstructions.Add(InstructionType.Jmi);
					break;
				case "rand":
					MatchedInstructions.Add(InstructionType.Rand);
					break;
				case "sprite":
					MatchedInstructions.Add(InstructionType.Sprite);
					break;
				case "xsprite":
					MatchedInstructions.Add(InstructionType.XSprite);
					break;
				case "skpr":
					MatchedInstructions.Add(InstructionType.SkPr);
					break;
				case "skup":
					MatchedInstructions.Add(InstructionType.SkUp);
					break;
				case "gdelay":
					MatchedInstructions.Add(InstructionType.GDelay);
					break;
				case "key":
					MatchedInstructions.Add(InstructionType.Key);
					break;
				case "sdelay":
					MatchedInstructions.Add(InstructionType.SDelay);
					break;
				case "ssound":
					MatchedInstructions.Add(InstructionType.SSound);
					break;
				case "adi":
					MatchedInstructions.Add(InstructionType.Adi);
					break;
				case "font":
					MatchedInstructions.Add(InstructionType.Font);
					break;
				case "xfont":
					MatchedInstructions.Add(InstructionType.XFont);
					break;
				case "bcd":
					MatchedInstructions.Add(InstructionType.Bcd);
					break;
				case "str":
					MatchedInstructions.Add(InstructionType.Str);
					break;
				case "ldr":
					MatchedInstructions.Add(InstructionType.Ldr);
					break;
			}

			#endregion

			// Found any?
			if (MatchedInstructions.Count == 0) return false;

			// Grab args:
			int[] InputArgs = source.GetCommaDelimitedArguments(index + 1, 0, int.MaxValue);
			List<InstructionType> MatchedArguments = new List<InstructionType>();

			foreach (InstructionType TryMatchArguments in MatchedInstructions) {
				ArgumentType[] MatchArgs = GetArguments(TryMatchArguments);
				if (MatchArgs.Length != InputArgs.Length) continue;

				bool SuccessfulArgumentMatch = true;
				for (int i = 0; SuccessfulArgumentMatch && i < MatchArgs.Length; ++i) {
					switch (MatchArgs[i]) {
						case ArgumentType.Immediate:
							break;
						case ArgumentType.Register:
							SuccessfulArgumentMatch = IsRegisterName(source.GetExpressionTokens(InputArgs[i]));
							break;
					}
				}

				if (SuccessfulArgumentMatch) MatchedArguments.Add(TryMatchArguments);

			}

			// Matched any?
			if (MatchedArguments.Count != 1) return false;

			// Huzzah!
			source.MatchedItem = (int)MatchedArguments[0];
			source.Tokens[index].Type = TokenisedSource.Token.TokenTypes.Instruction;
			return true;
		}

		public void Assemble(Compiler compiler, TokenisedSource source, int index) {
			switch (compiler.CurrentPass) {
				case AssemblyPass.Pass1:
					compiler.IncrementProgramAndOutputCounters(2); // If only it was always this easy, huh?
					break;
				case AssemblyPass.Pass2:

					InstructionType Instruction = (InstructionType)source.MatchedItem;

					ArgumentType[] ArgumentTypes = GetArguments(Instruction);
					int[] Arguments = new int[ArgumentTypes.Length];

					int[] PassedArguments = source.GetCommaDelimitedArguments(index + 1);
					if (PassedArguments.Length != Arguments.Length) throw new InvalidOperationException();

					// Evaluate arguments:
					for (int i = 0; i < ArgumentTypes.Length; ++i) {
						switch (ArgumentTypes[i]) {
							case ArgumentType.Immediate:
								Arguments[i] = (int)source.EvaluateExpression(compiler, PassedArguments[i]).NumericValue;
								break;
							case ArgumentType.Register:
								Arguments[i] = "0123456789abcdef".IndexOf(char.ToLowerInvariant(source.GetExpressionTokens(PassedArguments[i]).Tokens[0].Data[1]));
								CheckRange16(source, Arguments[i]);
								break;
						}
					}

					switch ((InstructionType)source.MatchedItem) {
						case InstructionType.ScDown:
							CheckRange16(source, Arguments[0]);
							compiler.WriteOutput((byte)0x00);
							compiler.WriteOutput((byte)(0xC0 | Arguments[0]));
							break;
						case InstructionType.Cls:
							compiler.WriteOutput((byte)0x00);
							compiler.WriteOutput((byte)0xE0);
							break;
						case InstructionType.Rts:
							compiler.WriteOutput((byte)0x00);
							compiler.WriteOutput((byte)0xEE);
							break;
						case InstructionType.ScRight:
							compiler.WriteOutput((byte)0x00);
							compiler.WriteOutput((byte)0xFB);
							break;
						case InstructionType.ScLeft:
							compiler.WriteOutput((byte)0x00);
							compiler.WriteOutput((byte)0xFC);
							break;
						case InstructionType.Low:
							compiler.WriteOutput((byte)0x00);
							compiler.WriteOutput((byte)0xFE);
							break;
						case InstructionType.High:
							compiler.WriteOutput((byte)0x00);
							compiler.WriteOutput((byte)0xFF);
							break;
						case InstructionType.Jmp:
							CheckRange4K(source, Arguments[0]);
							compiler.WriteOutput((byte)(0x10 | (Arguments[0] >> 8)));
							compiler.WriteOutput((byte)(Arguments[0]));
							break;
						case InstructionType.Jsr:
							CheckRange4K(source, Arguments[0]);
							compiler.WriteOutput((byte)(0x20 | (Arguments[0] >> 8)));
							compiler.WriteOutput((byte)(Arguments[0]));
							break;
						case InstructionType.SkEqImmediate:
							//CheckRange256(Arguments[1]);
							compiler.WriteOutput((byte)(0x30 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1]));
							break;
						case InstructionType.SkNeImmediate:
							//CheckRange256(Arguments[1]);
							compiler.WriteOutput((byte)(0x40 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1]));
							break;
						case InstructionType.SkEqRegister:
							compiler.WriteOutput((byte)(0x50 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1] << 4));
							break;
						case InstructionType.MovRegImmediate:
							CheckRange256(source, Arguments[1]);
							compiler.WriteOutput((byte)(0x60 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1]));
							break;
						case InstructionType.AddRegImmediate:
							CheckRange256(source, Arguments[1]);
							compiler.WriteOutput((byte)(0x70 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1]));
							break;
						case InstructionType.MovRegReg:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)(0x00 | (Arguments[1] << 4)));
							break;
						case InstructionType.OrRegReg:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)(0x01 | (Arguments[1] << 4)));
							break;
						case InstructionType.AndRegReg:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)(0x02 | (Arguments[1] << 4)));
							break;
						case InstructionType.XorRegReg:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)(0x03 | (Arguments[1] << 4)));
							break;
						case InstructionType.AddRegReg:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)(0x04 | (Arguments[1] << 4)));
							break;
						case InstructionType.SubRegReg:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)(0x05 | (Arguments[1] << 4)));
							break;
						case InstructionType.Shr:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)0x06);
							break;
						case InstructionType.Rsb:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)(0x07 | (Arguments[1] << 4)));
							break;
						case InstructionType.Shl:
							compiler.WriteOutput((byte)(0x80 | Arguments[0]));
							compiler.WriteOutput((byte)0x0E);
							break;
						case InstructionType.SkNeRegister:
							compiler.WriteOutput((byte)(0x90 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1] << 4));
							break;
						case InstructionType.Mvi:
							//CheckRange4K(Arguments[0]);
							compiler.WriteOutput((byte)(0xA0 | (Arguments[0] >> 8)));
							compiler.WriteOutput((byte)(Arguments[0]));
							break;
						case InstructionType.Jmi:
							CheckRange4K(source, Arguments[0]);
							compiler.WriteOutput((byte)(0xB0 | (Arguments[0] >> 8)));
							compiler.WriteOutput((byte)(Arguments[0]));
							break;
						case InstructionType.Rand:
							compiler.WriteOutput((byte)(0xC0 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1]));
							break;
						case InstructionType.Sprite:
							CheckRange(source, Arguments[2], 0, 16);
							Arguments[2] &= 0x0F;
							compiler.WriteOutput((byte)(0xD0 | Arguments[0]));
							compiler.WriteOutput((byte)((Arguments[1] << 4) | Arguments[2]));
							break;
						case InstructionType.XSprite:
							compiler.WriteOutput((byte)(0xD0 | Arguments[0]));
							compiler.WriteOutput((byte)(Arguments[1] << 4));
							break;
						case InstructionType.SkPr:
							compiler.WriteOutput((byte)(0xE0 | Arguments[0]));
							compiler.WriteOutput((byte)0x9E);
							break;
						case InstructionType.SkUp:
							compiler.WriteOutput((byte)(0xE0 | Arguments[0]));
							compiler.WriteOutput((byte)0xA1);
							break;
						case InstructionType.GDelay:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x07);
							break;
						case InstructionType.Key:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x0A);
							break;
						case InstructionType.SDelay:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x15);
							break;
						case InstructionType.SSound:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x18);
							break;
						case InstructionType.Adi:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x1E);
							break;
						case InstructionType.Font:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x29);
							break;
						case InstructionType.XFont:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x30);
							break;
						case InstructionType.Bcd:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x33);
							break;
						case InstructionType.Str:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x55);
							break;
						case InstructionType.Ldr:
							compiler.WriteOutput((byte)(0xF0 | Arguments[0]));
							compiler.WriteOutput((byte)0x65);
							break;
						default:
							throw new NotImplementedException();
					}
					break;
			}
		}

		private static void CheckRange(TokenisedSource source, int value, int min, int max) {
			if (value < min || value > max) {
				throw new CompilerExpection(source, string.Format("Argument out of bounds (must be between {0} and {1}).", min, max));
			}
		}
		private static void CheckRange16(TokenisedSource source, int value) {
			CheckRange(source, value, 0, 15);
		}
		private static void CheckRange256(TokenisedSource source, int value) {
			CheckRange(source, value, 0, 255);
		}
		private static void CheckRange4K(TokenisedSource source, int value) {
			CheckRange(source, value, 0, 4095);
		}

	}
}
