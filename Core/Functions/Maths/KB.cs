using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("kb(value)")]
	[Description("Returns the number of bytes in <c>value</c> kilobytes.")]
	[CodeExample(".echoln kb(16) ; Outputs 16384.")]
	[Warning("A kilobyte here means 1024 bytes.")]
	[Category("Maths")]
	public class KB : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, 1024 * (source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
