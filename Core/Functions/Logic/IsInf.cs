using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("isinf(expression)")]
	[Description("Returns true if the expression evaluates to positive or negative infinity.")]
	[Category("Logic")]
	[SeeAlso(typeof(IsNaN))]
	[SeeAlso(typeof(IsPosInf))]
	[SeeAlso(typeof(IsNegInf))]
	public class IsInf : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, double.IsInfinity(source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(0, 1)[0]).NumericValue) ? 1 : 0);
		}

	}
}