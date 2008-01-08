using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	[Description("Evaluates a snippet of code contained in a string.")]
	[Syntax("eval(\"expression\")")]
	[Remarks("The expression can contain expressions and assembly source code (including directives). The macro preprocessor is run as normal on source code in the string.")]
	[CodeExample("Two's complement 16-bit negation for the Z80.",
@"; A function to negate 16-bit register pairs on
; the Z80 (which can only negate 8-bit registers
; natively).
.macro neg_16(register)
	
	; Convert the passed register to a string.
	registerstr = strtoken(register)

	; Preserve A and F:
	push af

	; Get the most and least significant;
	; registers from the register pair:
	hi = strsub(registerstr, 0, 1)
	lo = strsub(registerstr, 1, 1)

	; We can only perform one's complement
	; on A, so we need to copy the register
	; to complement to and from A.
	cpl = ""ld a,{0} \\ cpl \\ ld {0},a""

	; Complement both registers:
	eval(strformat(cpl, hi))
	eval(strformat(cpl, lo))

	; Restore A and F:
	pop af

	; Increment the 16-bit pair by one to
	; complete the two's complement operation.
	eval(""inc "" + hi + lo)

.endmacro

; Negate HL:
neg_16(hl)")]
	[SatisfiesAssignmentRequirement(true)]
	public class Eval : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			object[] Source = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.String });

			Label Result = null;
			try {
				//compiler.AllowPositionToChange = false;
				foreach (TokenisedSource TS in TokenisedSource.FromString(compiler, Source[0] as string)) {
					Compiler.SourceStatement S = new Compiler.SourceStatement(compiler, TS.GetCode(), compiler.CurrentFile, compiler.CurrentLineNumber);
					Result = S.Compile(false);
				}
				if (Result != null) {
					return Result.Clone() as Label;
				} else {
					return new Label(compiler.Labels, double.NaN);
				}
			} finally {
			//	compiler.AllowPositionToChange = true;
			}
		}
	}
}
