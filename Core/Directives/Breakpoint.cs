using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;
using System.ComponentModel;

namespace Core.Directives {
	[Category("Debugging")]
	[Syntax(".breakpoint [\"Description\"]")]
	[Description("resources://Core.Documentation/BreakpointDescription")]
	[Remarks("resources://Core.Documentation/BreakpointRemarks")]
	public class Breakpoint : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.CurrentPass == AssemblyPass.WritingOutput) {

				var Arguments = source.GetCommaDelimitedArguments(compiler, index + 1, new[] { TokenisedSource.ArgumentType.String | TokenisedSource.ArgumentType.Optional });

				string Description = null;
				if (Arguments.Length == 1) Description = Arguments[0] as string;

				compiler.Breakpoints.Add(new Brass3.Breakpoint(compiler) {
					Description = Description
				});
			}

		}

	}
}
