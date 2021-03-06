using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("min(expression, expression)")]
	[Description("Returns the smaller of two numbers.")]
	[CodeExample(".echoln min(12, 34) ; Outputs 12.")]
	[Category("Maths")]
	public class Min : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 2);
			return new Label(compiler.Labels, Math.Min(
				source.EvaluateExpression(compiler, Args[0]).NumericValue,
				source.EvaluateExpression(compiler, Args[1]).NumericValue
			));
		}
	}
}
