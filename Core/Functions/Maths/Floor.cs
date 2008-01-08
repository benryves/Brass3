using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("floor(expression)")]
	[Description("Returns the largest integer less than or equal to the specified number.")]
	[Category("Maths")]
	[SeeAlso(typeof(Round))]
	[SeeAlso(typeof(Truncate))]
	[SeeAlso(typeof(Ceiling))]
	public class Floor : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Floor(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
