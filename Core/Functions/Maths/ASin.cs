using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("asin(expression)")]
	[Description("Returns the angle whose sine is the specified number.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class ASin : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Asin(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
