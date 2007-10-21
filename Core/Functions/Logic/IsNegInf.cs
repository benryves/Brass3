using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("isneginf(expression)")]
	[Description("Returns true if the expression evaluates to negative infinity.")]
	[Category("Logic")]
	[SeeAlso(typeof(IsInf))]
	[SeeAlso(typeof(IsNaN))]
	[SeeAlso(typeof(IsPosInf))]
	public class IsNegInf : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, double.IsNegativeInfinity(source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(0, 1)[0]).NumericValue) ? 1 : 0);
		}

	}
}