using System;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace TexasInstruments.Brass.Functions {
	[Category("Texas Instruments")]

	[PluginName("bcall")]
	[PluginName("bcallz"), PluginName("bcallnz")]
	[PluginName("bcallc"), PluginName("bcallnc")]
	[PluginName("bcallpe"), PluginName("bcallpo")]
	[PluginName("bcallp"), PluginName("bcallm")]
	[PluginName("bjump")]
	[PluginName("bjumpz"), PluginName("bjumpnz")]
	[PluginName("bjumpc"), PluginName("bjumpnc")]
	[PluginName("bjumppe"), PluginName("bjumppo")]
	[PluginName("bjumpp"), PluginName("bjumpm")]
	[DisplayName("bcall/bjump")]
	[Syntax("bcall(_target)")]
	[Syntax("bcallz(_target)"), Syntax("bcallnz(_target)")]
	[Syntax("bcallc(_target)"), Syntax("bcallnc(_target)")]
	[Syntax("bcallpe(_target)"), Syntax("bcallpo(_target)")]
	[Syntax("bcallp(_target)"), Syntax("bcallm(_target)")]
	[Syntax("bjump(_target)")]
	[Syntax("bjumpz(_target)"), Syntax("bjumpnz(_target)")]
	[Syntax("bjumpc(_target)"), Syntax("bjumpnc(_target)")]
	[Syntax("bjumppe(_target)"), Syntax("bjumppo(_target)")]
	[Syntax("bjumpp(_target)"), Syntax("bjumpm(_target)")]
	[Description("Calls or jumps to a function, switching page if required on compatible hardware.")]
	[SeeAlso(typeof(TexasInstruments.Brass.Output.TI8X)), SeeAlso(typeof(TexasInstruments.Brass.Output.TI73))]
	[SeeAlso(typeof(Directives.BCall))]
	[Remarks(
@"The function generates Z80 code depending on the current output writer.
If the output writer is the TI8X or TI73 plugin then the instruction is expanded as follows:
<table>
	<tr><th>Call</th><th>Generated Code</th><th>Size</th></tr>
	<tr><th>bcall(target)</th><td><c>rst $28 \ .dw target</td><td>3</td></tr>
	<tr><th>bcallz(target)</th><td><c>jr nz,+ \ rst $28 \ .dw target \ +</td><td>5</td></tr>
	<tr><th>bcallnz(target)</th><td><c>jr z,+ \ rst $28 \ .dw target \ +</td><td>5</td></tr>
	<tr><th>bcallc(target)</th><td><c>jr nc,+ \ rst $28 \ .dw target \ +</td><td>5</td></tr>
	<tr><th>bcallnc(target)</th><td><c>jr c,+ \ rst $28 \ .dw target \ +</td><td>5</td></tr>
	<tr><th>bcallpe(target)</th><td><c>jp po,+ \ rst $28 \ .dw target \ +</td><td>6</td></tr>
	<tr><th>bcallpo(target)</th><td><c>jp pe,+ \ rst $28 \ .dw target \ +</td><td>6</td></tr>
	<tr><th>bcallp(target)</th><td><c>jp m,+ \ rst $28 \ .dw target \ +</td><td>6</td></tr>
	<tr><th>bcallm(target)</th><td><c>jp p,+ \ rst $28 \ .dw target \ +</td><td>6</td></tr>
	<tr><th>bjump(target)</th><td><c>call $50 \ .dw target</td><td>5</td></tr>
	<tr><th>bjumpz(target)</th><td><c>jr nz,+ \ call $50 \ .dw target \ +</td><td>7</td></tr>
	<tr><th>bjumpnz(target)</th><td><c>jr z,+ \ call $50 \ .dw target \ +</td><td>7</td></tr>
	<tr><th>bjumpc(target)</th><td><c>jr nc,+ \ call $50 \ .dw target \ +</td><td>7</td></tr>
	<tr><th>bjumpnc(target)</th><td><c>jr c,+ \ call $50 \ .dw target \ +</td><td>7</td></tr>
	<tr><th>bjumppe(target)</th><td><c>jp po,+ \ call $50 \ .dw target \ +</td><td>8</td></tr>
	<tr><th>bjumppo(target)</th><td><c>jp pe,+ \ call $50 \ .dw target \ +</td><td>8</td></tr>
	<tr><th>bjumpp(target)</th><td><c>jp m,+ \ call $50 \ .dw target \ +</td><td>8</td></tr>
	<tr><th>bjumpm(target)</th><td><c>jp p,+ \ call $50 \ .dw target \ +</td><td>8</td></tr>
</table>
If another output writer is in use, a regular Z80 call (three bytes) is generated.")]

	[CodeExample( 
@"/* Set cursor to top-left position. */
	xor a
	ld (curCol),a
	ld (curRow),a
	
/* Display a string. */
	ld hl,String
	bcall(_putS)

/* Wait for a key, then return. */
-	bcall(_getCSC)
	or a
	jr z,-
	ret

String .byte ""Brass 3"", 0")]

	[SatisfiesAssignmentRequirement(true)]
	public class BCall : IFunction {

		internal enum Z80Instruction : byte {
			Rst28h = 0xEF,
			Call = 0xCD,
			CallC = 0xDC,
			CallM = 0xFC,
			CallNC = 0xD4,
			CallNZ = 0xC4,
			CallP = 0xF4,
			CallPE = 0xEC,
			CallPO = 0xE4,
			CallZ = 0xCC,
			Jr = 0x18,
			JrC = 0x38,
			JrNC = 0x30,
			JrNZ = 0x20,
			JrZ = 0x28,
			JpC = 0xDA,
			JpM = 0xFA,
			JpNC = 0xD2,
			JpNZ = 0xC2,
			JpP = 0xF2,
			JpPE = 0xEA,
			JpPO = 0xE2,
			JpZ = 0xCA,
			Jp = 0xC3,
			Ret = 0xC9,
			LdHl = 0x21,
			XorD = 0xAA,
		}

		internal enum Condition {
			None,
			Z, NZ,
			C, NC,
			PE, PO,
			P, M,
		}

		internal void RomCall(Compiler compiler, string function, ushort address) {

			Type OutputWriterType = compiler.OutputWriter.GetType();
			bool RequiresVoodoo = OutputWriterType == typeof(TexasInstruments.Brass.Output.TI8X) || OutputWriterType == typeof(TexasInstruments.Brass.Output.TI73);

			int InstructionSize = 3;

			if (RequiresVoodoo) {
				switch (function.Substring(5)) {
					case "z":
					case "nz":
					case "c":
					case "nc":
						InstructionSize = 5; // jr ?,$+5 \ rst $28 \ .dw xxx
						break;
					case "pe":
					case "po":
					case "p":
					case "m":
						InstructionSize = 6; // jp ?,$+6 \ rst $28 \ .dw xxx
						break;
				}

				if (function.StartsWith("bjump")) InstructionSize += 2;

			}


			switch (compiler.CurrentPass) {

				case AssemblyPass.Pass1:
					compiler.IncrementProgramAndOutputCounters(InstructionSize);
					break;

				case AssemblyPass.Pass2:

					if (RequiresVoodoo) {

						// Output Z80 instructions to invoke ROM call handler at $28, or jump over it.

						ushort JumpTarget = (ushort)(compiler.Labels.ProgramCounter.NumericValue + InstructionSize);

						switch (function.Substring(5)) {
							case "":
								break;
							case "z":
								compiler.WriteOutput((byte)Z80Instruction.JrNZ);
								compiler.WriteOutput((byte)(InstructionSize - 2));
								break;
							case "nz":
								compiler.WriteOutput((byte)Z80Instruction.JrZ);
								compiler.WriteOutput((byte)(InstructionSize - 2));
								break;
							case "c":
								compiler.WriteOutput((byte)Z80Instruction.JrNC);
								compiler.WriteOutput((byte)(InstructionSize - 2));
								break;
							case "nc":
								compiler.WriteOutput((byte)Z80Instruction.JrC);
								compiler.WriteOutput((byte)(InstructionSize - 2));
								break;
							case "pe":
								compiler.WriteOutput((byte)Z80Instruction.JpPO);
								compiler.WriteOutput(JumpTarget);
								break;
							case "po":
								compiler.WriteOutput((byte)Z80Instruction.JpPE);
								compiler.WriteOutput(JumpTarget);
								break;
							case "p":
								compiler.WriteOutput((byte)Z80Instruction.JpM);
								compiler.WriteOutput(JumpTarget);
								break;
							case "m":
								compiler.WriteOutput((byte)Z80Instruction.JpP);
								compiler.WriteOutput(JumpTarget);
								break;
							default:
								throw new InvalidOperationException();
						}

						if (function.StartsWith("bcall")) {
							compiler.WriteOutput((byte)Z80Instruction.Rst28h);
						} else {
							compiler.WriteOutput((byte)Z80Instruction.Call);
							compiler.WriteOutput((ushort)0x50);
						}

					} else {

						// Output regular Z80 calls.
						switch (function) {
							case "bcall": compiler.WriteOutput((byte)Z80Instruction.Call); break;
							case "bcallz": compiler.WriteOutput((byte)Z80Instruction.CallZ); break;
							case "bcallnz": compiler.WriteOutput((byte)Z80Instruction.CallNZ); break;
							case "bcallc": compiler.WriteOutput((byte)Z80Instruction.CallC); break;
							case "bcallnc": compiler.WriteOutput((byte)Z80Instruction.CallNC); break;
							case "bcallpe": compiler.WriteOutput((byte)Z80Instruction.CallPE); break;
							case "bcallpo": compiler.WriteOutput((byte)Z80Instruction.CallPO); break;
							case "bcallp": compiler.WriteOutput((byte)Z80Instruction.CallP); break;
							case "bcallm": compiler.WriteOutput((byte)Z80Instruction.CallM); break;
							case "bjump": compiler.WriteOutput((byte)Z80Instruction.Jp); break;
							case "bjumpz": compiler.WriteOutput((byte)Z80Instruction.JpZ); break;
							case "bjumpnz": compiler.WriteOutput((byte)Z80Instruction.JpNZ); break;
							case "bjumpc": compiler.WriteOutput((byte)Z80Instruction.JpC); break;
							case "bjumpnc": compiler.WriteOutput((byte)Z80Instruction.JpNC); break;
							case "bjumppe": compiler.WriteOutput((byte)Z80Instruction.JpPE); break;
							case "bjumppo": compiler.WriteOutput((byte)Z80Instruction.JpPO); break;
							case "bjumpp": compiler.WriteOutput((byte)Z80Instruction.JpP); break;
							case "bjumpm": compiler.WriteOutput((byte)Z80Instruction.JpM); break;
							default: throw new InvalidOperationException();
						}
					}

					compiler.WriteOutput(address);
					break;
			}

		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			this.RomCall(compiler, function, (ushort)(compiler.CurrentPass == AssemblyPass.Pass2 ? source.EvaluateExpression(compiler).NumericValue : 0));			
			return new Label(compiler.Labels, double.NaN);
		}
	}
}
