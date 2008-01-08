using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("sin(expression)")]
	[Description("Returns the sine of the specified angle.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class Sin : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Sin(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
