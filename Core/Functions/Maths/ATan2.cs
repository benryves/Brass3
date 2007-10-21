using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("atan2(y, x)")]
	[Description("Returns the angle whose tangent is the quotient of two specified numbers.")]
	[Remarks("The return value is the angle in radians in the Cartesian plane formed by the x-axis, and a vector starting from the origin, (0,0), and terminating at the point, (x,y).")]
	[Category("Maths")]
	public class ATan2 : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 2);
			return new Label(compiler.Labels, Math.Atan2(
				source.EvaluateExpression(compiler, Args[0]).NumericValue,
				source.EvaluateExpression(compiler, Args[1]).NumericValue
			));
		}
	}
}
