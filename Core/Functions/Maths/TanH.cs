using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("tanh(expression)")]
	[Description("Returns the hyperbolic tangent of the specified angle.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class TanH : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Tanh(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
