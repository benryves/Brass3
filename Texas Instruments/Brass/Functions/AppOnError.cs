using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace TexasInstruments.Brass.Functions {
	[Category("Texas Instruments")]
	[Syntax("AppOnError(handler)"), Syntax("AppOffError()")]
	[PluginName("apponerr"), PluginName("appofferr")]
	[CodeExample("Compute 1/X; carry flag is set on error.",
@"/* Compute 1/X and return CA = 0 if no error, *
 * otherwise return CA = 1. This example has  *
 * been copied from the TI-83 Plus SDK.       *
 * Test by deleting the variable X or setting *
 * X to 0.                                    */

	AppOnErr(My_Err_Handle)
;
	.bcall _RclX    ; OP1 = (X)
	.bcall _FPRecip ; 1 / OP1
;
; If no error then returns from the call
;
	AppOffErr()	    ; remove the error handler
	or a            ; CA = 0 for no error
	ret
;
; control comes here if X = 0 and generates
; an error
;
My_Err_Handle:
	scf             ; CA = 1 for error
	ret")]
	[SatisfiesAssignmentRequirement(true)]
	public class AppOnError : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			switch (function) {
				case "apponerr":
					switch (compiler.CurrentPass) {
						case AssemblyPass.CreatingLabels:
							compiler.IncrementProgramAndOutputCounters(6);
							break;
						case AssemblyPass.WritingOutput:
							compiler.WriteOutput((byte)BCall.Z80Instruction.LdHl);
							compiler.WriteOutput((ushort)(double)source.GetCommaDelimitedArguments(compiler, 0, TokenisedSource.ValueArgument)[0]);
							compiler.WriteOutput((byte)BCall.Z80Instruction.Call);
							compiler.WriteOutput((ushort)0x59);
							break;
					}
					break;
				case "appofferr":
					switch (compiler.CurrentPass) {
						case AssemblyPass.CreatingLabels:
							compiler.IncrementProgramAndOutputCounters(3);
							break;
						case AssemblyPass.WritingOutput:
							source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { });
							compiler.WriteOutput((byte)BCall.Z80Instruction.Call);
							compiler.WriteOutput((ushort)0x5C);
							break;
					}					
					break;
				default:
					throw new InvalidOperationException();
			}

			return new Label(compiler.Labels, double.NaN);

		}

	}
}