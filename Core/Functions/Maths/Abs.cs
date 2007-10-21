using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("abs(expression)")]
	[Description("Returns the absolute value of a specified number.")]
	[CodeExample(".echoln abs(-100) ; Outputs 100.")]
	[Category("Maths")]
	public class Abs : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Abs(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
