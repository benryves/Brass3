using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("tan(expression)")]
	[Description("Returns the tangent of the specified angle.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class Tan : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Tan(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
