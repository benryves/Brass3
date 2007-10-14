using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	[Description("Evaluates a snippet of code contained in a string.")]
	[Syntax("eval(\"expression\")")]
	[Remarks("The expression can contain expressions and assembly source code (including directives). The macro preprocessor is run as normal on source code in the string.")]
	[Warning("As the code is evaluated outside rather than inside the original source directives that modify the flow control will not work (for example, loops won't work with this directive).")]
	[CodeExample("Two's complement 16-bit negation for the Z80.", "; A function to negate 16-bit register pairs on\r\n; the Z80 (which can only negate 8-bit registers\r\n; natively).\r\n.function neg_16(register)\r\n\t\r\n\t; Preserve A and F:\r\n\tpush af\r\n\t\r\n\t; Get the high and least significant\r\n\t; registers from the register pair:\r\n\thi = strsub(register, 0, 1)\r\n\tlo = strsub(register, 1, 1)\r\n\t\r\n\t; We can only perform one's complement\r\n\t; on A, so we need to copy the register\r\n\t; to complement to and from A.\r\n\tcpl = \"ld a,{0} \\ cpl \\ ld {0},a\"\r\n\t\r\n\t; Complement both registers:\r\n\teval(strformat(cpl, hi))\r\n\teval(strformat(cpl, lo))\r\n\t\r\n\t; Restore A and F:\r\n\tpop af\r\n\t\r\n\t; Increment the 16-bit pair by one to\r\n\t; complete the two's complement operation.\r\n\teval(\"inc \" + hi + lo)\r\n\t\r\n.endfunction\r\n\r\n\r\n; Negate HL:\r\nneg_16(\"hl\")")]
	[SatisfiesAssignmentRequirement(true)]
	public class Eval : IFunction {

		public string[] Names {
			get { return new string[] { "eval" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			object[] Source = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.String });

			Label Result = null;
			try {
				compiler.AllowPositionToChange = false;
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
				compiler.AllowPositionToChange = true;
			}
		}
	}
}
