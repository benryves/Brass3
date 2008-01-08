using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("toradians(expression)")]
	[Description("Converts an angle in radians to one in degrees.")]
	[Category("Maths")]
	[SeeAlso(typeof(ToRadians))]
	public class ToDegrees : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, source.EvaluateExpression(compiler, Args[0]).NumericValue * 180d / Math.PI);
		}
	}
}
