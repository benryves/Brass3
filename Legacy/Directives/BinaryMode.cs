using System;
using System.Collections.Generic;
using System.Text;

using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;
using Brass3;

namespace Legacy.Directives {
	[Syntax(".binarymode mode")]
	[Description("Specifies the format of the output binary.")]
	[Remarks("This sets the output writer plugin at compile-time. It is strongly recommended that this is set in the project file rather than in the source file.")]
	[Warning("<c>intel</c> is translated into <c>intelhex</c>.")]
	public class BinaryMode : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			string Token = source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.StringOrTokenArgument)[0] as string;
			switch (Token.ToLowerInvariant()) {
				case "intel":
					Token = "intelhex";
					break;
			}
			compiler.OutputWriter = compiler.OutputWriters[Token];
		}
	}
}
