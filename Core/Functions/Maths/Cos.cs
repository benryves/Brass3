using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("cos(expression)")]
	[Description("Returns the cosine of the specified angle.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class Cos : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Cos(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
