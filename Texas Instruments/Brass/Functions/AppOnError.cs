using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

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
					compiler.WriteStaticOutput((byte)BCall.Z80Instruction.LdHl);
					compiler.WriteStaticOutput((ushort)(double)source.GetCommaDelimitedArguments(compiler, 0, TokenisedSource.ValueArgument)[0]);
					compiler.WriteStaticOutput((byte)BCall.Z80Instruction.Call);
					compiler.WriteStaticOutput((ushort)0x59);
					break;
				case "appofferr":
					source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { });
					compiler.WriteStaticOutput((byte)BCall.Z80Instruction.Call);
					compiler.WriteStaticOutput((ushort)0x5C);
					break;
				default:
					throw new InvalidOperationException();
			}

			return new Label(compiler.Labels, double.NaN);

		}

	}
}