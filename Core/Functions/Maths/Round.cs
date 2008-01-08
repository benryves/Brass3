using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("round(expression)")]
	[Syntax("round(expression, places)")]
	[Description("Rounds a value to the nearest integer or specified number of decimal places.")]
	[CodeExample(".echoln round(pi()) ; Outputs 3.")]
	[CodeExample(".echoln round(pi(), 3) ; Outputs 3.142.")]
	[Category("Maths")]
	[SeeAlso(typeof(Truncate))]
	[SeeAlso(typeof(Floor))]
	[SeeAlso(typeof(Ceiling))]
	public class Round : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1, 2);
			return new Label(compiler.Labels, Math.Round(
				source.EvaluateExpression(compiler, Args[0]).NumericValue,
				Args.Length == 1 ? 0 : (int)source.EvaluateExpression(compiler, Args[1]).NumericValue
			));
		}
	}
}
