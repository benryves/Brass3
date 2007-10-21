using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("ln(expression)")]
	[Description("Returns the natural logarithm of a specified number.")]
	[Category("Maths")]
	[SeeAlso(typeof(Log))]
	[SeeAlso(typeof(Log10))]
	public class Ln : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Log(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
