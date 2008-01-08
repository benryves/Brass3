using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("sinh(expression)")]
	[Description("Returns the hyperbolic sine of the specified angle.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class SinH : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Sinh(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
