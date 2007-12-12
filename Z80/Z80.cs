using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.IO;
using System.Globalization;

namespace Z80 {
	public class Z80 : IAssembler {

		#region Types

		public class Instruction : IComparable {

			#region Types

			[Flags()]
			public enum OperandType {

				None = 0x00,

				Register = 0x01, // A, IX, Z, NC, ...
				Index = 0x02,    // IX*, IY*, ...
				Value = 0x04,    // *

				Types = Value | Register | Index,

				Indirect = 0x0100, // (*)

				Modifiers = Indirect,

			};

			public enum InstructionClass {
				None,
				Relative,
				ZBit,
				ZIndex,
				Restart,
			}

			#endregion


			#region Properties

			private int identifier;
			public int Identifier {
				get { return this.identifier; }
				set { this.identifier = value; }
			}

			private readonly string name;
			public string Name {
				get { return this.name; }
			}

			private readonly KeyValuePair<OperandType, string>[] operands;
			public KeyValuePair<OperandType, string>[] Operands {
				get { return this.operands; }
			}

			private readonly InstructionClass instructionClass;
			public InstructionClass Class {
				get { return this.instructionClass; }
			}

			private readonly byte[] opcodes;
			public byte[] Opcodes {
				get { return this.opcodes; }
			}

			private readonly int size;
			public int Size {
				get { return this.size; }
			}

			private readonly int shift;
			public int Shift {
				get { return this.shift; }
			}

			private readonly int or;
			public int Or {
				get { return this.or; }
			}

			#endregion

			#region Methods

			public bool TryMatchSource(TokenisedSource source, TokenisedSource[] arguments) {
				// Sanity check:
				if (operands.Length != arguments.Length) return false;

				for (int i = 0; i < operands.Length; ++i) {
					if ((operands[i].Key & OperandType.Indirect) != OperandType.None) { // Is it indirect?
						// Check (*)
						if (arguments[i].Tokens.Length < 3 || arguments[i].Tokens[0].Data != "(" || arguments[i].Tokens[arguments[i].Tokens.Length - 1].Data != ")") return false;
						if (arguments[i].GetCloseBracketIndex(0) != arguments[i].Tokens.Length - 1) return false; // (*)+(*) = invalid.
						if ((operands[i].Key & OperandType.Register) != OperandType.None && (arguments[i].Tokens.Length != 3 || arguments[i].Tokens[1].DataLowerCase != operands[i].Value)) return false;
						if ((operands[i].Key & OperandType.Index) != OperandType.None && (arguments[i].Tokens[1].DataLowerCase != operands[i].Value)) return false;
					} else { // 
						if ((operands[i].Key & OperandType.Register) != OperandType.None) {
							if (arguments[i].Tokens.Length != 1 || arguments[i].Tokens[0].DataLowerCase != operands[i].Value) return false;
						} else if ((operands[i].Key & OperandType.Index) != OperandType.None) {
							return false;
						}
					}
				}

				return true;

			}

			public override string ToString() {
				List<string> OperandStrings = new List<string>();
				foreach (KeyValuePair<OperandType, string> Op in this.Operands) {
					string OperandText = Op.Value;
					if ((Op.Key & OperandType.Value) != OperandType.None) OperandText = "*";
					if ((Op.Key & OperandType.Indirect) != OperandType.None) OperandText = "(" + OperandText + ")";
					OperandStrings.Add(OperandText);
				}
				return this.Name + " " + string.Join(",", OperandStrings.ToArray());
			}


			public int CompareTo(object obj) {
				Instruction other = (Instruction)obj;
				if (this.name != other.name) {
					return this.name.CompareTo(other.name);
				} else if (this.operands.Length != other.operands.Length) {
					return other.operands.Length.CompareTo(this.operands.Length);
				} else {
					for (int i = 0; i < this.operands.Length; ++i) {
						if ((this.operands[i].Key & OperandType.Indirect) != (other.operands[i].Key & OperandType.Indirect)) {
							return (this.operands[i].Key & OperandType.Indirect) == OperandType.Indirect ? -1 : +1;
						}
						if ((this.operands[i].Key & OperandType.Types) != (other.operands[i].Key & OperandType.Types)) {
							return ((int)(this.operands[i].Key & OperandType.Types)).CompareTo((int)(other.operands[i].Key & OperandType.Types));
						}
					}
				}
				return 0;

			}

			#endregion

			#region Constructors

			/// <summary>
			/// Creates an instance of an instruction.
			/// </summary>
			/// <param name="name">The name of the instruction.</param>
			/// <param name="code">The code emitted for the instruction.</param>
			/// <param name="operands">Matching operands for the instruction.</param>
			public Instruction(string name, KeyValuePair<OperandType, string>[] operands,  byte[] code, int size, InstructionClass instructionClass, int shift, int or) {
				this.name = name;
				this.operands = operands;
				this.opcodes = code;
				this.size = size;
				this.instructionClass = instructionClass;
				this.shift = shift;
				this.or = or;
			}

			/// <summary>
			/// Creates an instance of an instruction from a line of code from a TASM table.
			/// </summary>
			/// <param name="tasmLine">A line from a TASM table.</param>
			public Instruction(string tasmLine) {
				
				string[] Components = tasmLine.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (Components.Length < 2) throw new InvalidDataException("Expected at least six arguments on the line.");

				// Set the name:
				this.name = Components[0].ToLowerInvariant();

				// Decode the operands:
				List<KeyValuePair<OperandType, string>> Operands = new List<KeyValuePair<OperandType, string>>();
				foreach (string o in Components[1].Split(',')) {
					string Operand = o.Trim().ToLowerInvariant();
					if (string.IsNullOrEmpty(Operand) || Operand == "\"\"") continue;
					OperandType Type = OperandType.None;
					if (Operand[0] == '(' && Operand[Operand.Length - 1] == ')') {
						Type |= OperandType.Indirect;
						Operand = Operand.Substring(1, Operand.Length - 2);
					}
					switch (Operand) {
						case "*":
							Type |= OperandType.Value;
							Operand = null;
							break;
						case "ix*":
						case "iy*":
							Type |= OperandType.Index;
							Operand = Operand.Remove(2);
							break;
						default:
							Type |= OperandType.Register;
							break;
					}
					Operands.Add(new KeyValuePair<OperandType, string>(Type, Operand));
				}
				this.operands = Operands.ToArray();

				// Get the code:
				this.opcodes = new byte[Components[2].Length / 2];
				for (int i = 0; i < this.opcodes.Length; ++i) {
					this.opcodes[this.opcodes.Length - i - 1] = (byte)Convert.ToInt32(Components[2].Substring(i * 2, 2), 16);
				}

				// Set the size:
				this.size = int.Parse(Components[3], CultureInfo.InvariantCulture);

				// Set the class:
				switch (Components[4].ToLowerInvariant()) {
					case "nop":
						this.instructionClass = InstructionClass.None;
						break;
					case "r1":
						this.instructionClass = InstructionClass.Relative;
						break;
					case "zbit":
						this.instructionClass = InstructionClass.ZBit;
						break;
					case "zix":
						this.instructionClass = InstructionClass.ZIndex;
						break;
					case "rst":
						this.instructionClass = InstructionClass.Restart;
						break;
					default:
						throw new NotSupportedException("Instruction class '" + Components[4] + "' not supported.");
				}

				// Set shift and or rules:
				this.shift = Components.Length >= 7 ? Convert.ToInt32(Components[6], 16) : 0;
				this.or = Components.Length >= 8 ? Convert.ToInt32(Components[7], 16) : 0;

			}

			#endregion
		}


		#endregion

		#region Private Fields


		private Dictionary<int, Dictionary<string, List<Instruction>>> Instructions;
		private List<Instruction> AllInstructions;


		#endregion

		#region Public Methods

		public bool TryMatchSource(Compiler compiler, TokenisedSource source, int index) {

			TokenisedSource[] Arguments = Array.ConvertAll<int, TokenisedSource>(source.GetCommaDelimitedArguments(index + 1), delegate(int i) { return source.GetExpressionTokens(i); });

			Dictionary<string, List<Instruction>> InstructionsByArgs;
			if (!this.Instructions.TryGetValue(Arguments.Length, out InstructionsByArgs)) return false;

			List<Instruction> InstructionsByName;
			if (!InstructionsByArgs.TryGetValue(source.Tokens[index].DataLowerCase, out InstructionsByName)) return false;

			foreach (Instruction I in InstructionsByName) {
				if (I.TryMatchSource(source, Arguments)) {
					source.MatchedItem = I.Identifier;
					return true;
				}
			}

			return false;


		}

		public void Assemble(Compiler compiler, TokenisedSource source, int index) {

			// Fetch the instruction that matches this line of code:
			Instruction I = this.AllInstructions[source.MatchedItem];

			compiler.WriteDynamicOutput(I.Size, Generator => {

				// Storage for the results of the evaluation:
				List<int> OperandResults = new List<int>(I.Operands.Length);

				int[] SourceArguments = source.GetCommaDelimitedArguments(index + 1, I.Operands.Length);
				for (int i = 0; i < SourceArguments.Length; ++i) {

					switch (I.Operands[i].Key & Instruction.OperandType.Types) {

						case Instruction.OperandType.Register:
							// Do nothing!
							break;

						case Instruction.OperandType.Value:
							// Evaluate:
							OperandResults.Add((int)source.EvaluateExpression(compiler, SourceArguments[i]).NumericValue);

							if ((I.Operands[i].Key & Instruction.OperandType.Indirect) == Instruction.OperandType.None) {
								TokenisedSource CheckAccidentalIndirection = (source.GetExpressionTokens(SourceArguments[i]));
								if (CheckAccidentalIndirection.Tokens[0].Data == "(" && CheckAccidentalIndirection.GetCloseBracketIndex(0) == CheckAccidentalIndirection.Tokens.Length - 1) {
									string ErrorMessage = string.Format("Attempted use of indirection with an unsupported instruction ({0}).", I);
									compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new CompilerException(CheckAccidentalIndirection, ErrorMessage)));
								}
							}
							break;

						case Instruction.OperandType.Index:
							// Kludge for evaluations of form ix+*
							TokenisedSource IndexAccess = source.GetExpressionTokens(SourceArguments[i]);

							// Sanity check:
							if (IndexAccess.Tokens.Length < 3
								|| IndexAccess.Tokens[1].DataLowerCase != I.Operands[i].Value
								|| IndexAccess.Tokens[0].Data != "("
								|| IndexAccess.Tokens[IndexAccess.Tokens.Length - 1].Data != ")"
								) throw new CompilerException(source, "Instruction not implemented properly.");

							if (IndexAccess.Tokens.Length == 3) {
								// If it's only three tokens, chances are it goes (ix) or (iy):
								OperandResults.Add(0);
							} else {
								// It'll hopefully be (ix*)
								IndexAccess.ReplaceToken(1, new TokenisedSource.Token[] { });
								OperandResults.Add((int)IndexAccess.EvaluateExpression(compiler).NumericValue);
							}
							break;
						default:
							throw new CompilerException(source, "Instruction not implemented properly.");
					}

				}

				// Now we have calculated the values of the various arguments,
				// We need to build and output the data.

				// The following code is pretty much a copy-and-paste job from Brass 1. Sorry.

				if (I.Class == Instruction.InstructionClass.ZIndex && OperandResults.Count == 1) {
					OperandResults[0] &= 0xFF;
				}

				if (OperandResults.Count > 0 && I.Class != Instruction.InstructionClass.ZBit && !(I.Class == Instruction.InstructionClass.ZIndex && OperandResults.Count != 1)) {
					OperandResults[0] <<= I.Shift;
					OperandResults[0] |= I.Or;
				}

				byte[] OutputData = new byte[I.Size];
				for (int i = 0; i < I.Opcodes.Length; ++i) {
					OutputData[i] = I.Opcodes[i];
				}


				switch (I.Class) {
					case Instruction.InstructionClass.None:
						for (int i = I.Opcodes.Length; i < I.Size; ++i) {
							OutputData[i] = (byte)OperandResults[0];
							OperandResults[0] >>= 8;
						}
						break;
					case Instruction.InstructionClass.Relative:
						OperandResults[0] -= (int)(compiler.Labels.ProgramCounter.NumericValue + I.Size);
						if (OperandResults[0] > 127 || OperandResults[0] < -128) {
							throw new CompilerException(source.Tokens[index], "Range of relative jump exceeded.");
						}
						OutputData[OutputData.Length - 1] = (byte)OperandResults[0];
						break;
					case Instruction.InstructionClass.ZIndex:
						if (OperandResults.Count == 2) {
							for (int i = I.Opcodes.Length; i < I.Size; i++) {
								OutputData[i] = (byte)OperandResults[i - I.Opcodes.Length];
							}
						} else {
							for (int j = I.Opcodes.Length; j < I.Size; ++j) {
								OutputData[j] = (byte)(OperandResults[0] & 0xFF);
								OperandResults[0] >>= 8;
							}
						}

						break;
					case Instruction.InstructionClass.ZBit:
						if (OperandResults[0] < 0 || OperandResults[0] > 7) {
							throw new CompilerException(source.Tokens[index], "Bit index must be in the range 0-7 (not " + OperandResults[0] + ").");
						}
						OperandResults[0] *= 8;
						if (I.Size == 4) {
							int SecondArgument = OperandResults[1];

							if (SecondArgument > 127 || SecondArgument < -128) {
								throw new CompilerException(source.Tokens[index], "Range of IX must be between -128 and 127 (not " + SecondArgument + ").");
							}

							OutputData[2] = (byte)((SecondArgument | (I.Or & 0xFF)) & 0xFF);
							OutputData[3] = (byte)(OperandResults[0] | (I.Or >> 8));
						} else if (I.Size == 2) {
							OutputData[1] += (byte)(OperandResults[0]);
						} else {
							throw new CompilerException(source.Tokens[index], "ZBIT instruction not supported.");
						}
						break;
					case Instruction.InstructionClass.Restart:
						if (OperandResults[0] < 0x00 || OperandResults[0] > 0x38) {
							throw new CompilerException(source.Tokens[index], "You can only restart to addresses between $00 and $38 inclusive.");
						} else if ((OperandResults[0] & 0x07) != 0) {
							throw new CompilerException(source.Tokens[index], "You can only restart to addresses divisible by eight.");
						} else {
							OutputData[0] = (byte)((int)OutputData[0] + OperandResults[0]);
						}
						break;
					default:
						throw new NotImplementedException();
				}

				// Commit!
				Generator.Data = OutputData;
			});

		}


		#endregion

		#region Constructor

		public Z80() {

			this.Instructions = new Dictionary<int, Dictionary<string, List<Instruction>>>();

			this.AllInstructions = new List<Instruction>();
			foreach (string s in Properties.Resources.TASM80.Split('\n')) {
				string Source = s.Trim();
				if (string.IsNullOrEmpty(Source)) continue;
				this.AddInstruction(new Instruction(Source), false);
			}
			AllInstructions.Sort();
			for (int i = 0; i < AllInstructions.Count; ++i) {
				AllInstructions[i].Identifier = i;				
			}
		}

		public void AddInstruction(Instruction instruction, bool resort) {
			instruction.Identifier = this.AllInstructions.Count;
			this.AllInstructions.Add(instruction);
			Dictionary<string, List<Instruction>> InstructionsByArgs;
			if (!this.Instructions.TryGetValue(instruction.Operands.Length, out InstructionsByArgs)) {
				InstructionsByArgs = new Dictionary<string, List<Instruction>>();
				this.Instructions.Add(instruction.Operands.Length, InstructionsByArgs);
			}

			List<Instruction> InstructionsByName;
			if (!InstructionsByArgs.TryGetValue(instruction.Name, out InstructionsByName)) {
				InstructionsByName = new List<Instruction>();
				InstructionsByArgs.Add(instruction.Name, InstructionsByName);
			}
			InstructionsByName.Add(instruction);
			if (resort) InstructionsByName.Sort();
		}

		#endregion

	}
}