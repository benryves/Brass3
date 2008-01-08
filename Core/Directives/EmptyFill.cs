using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".emptyfill value")]
	[Description("Used to fill unused space in binaries for padding directives.")]
	[Category("Output Manipulation")]
	public class EmptyFill : IDirective {
		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			compiler.EmptyFill = (byte)(source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(index + 1, 1)[0])).NumericValue;
		}
	}
}
