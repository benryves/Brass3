using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("if(condition, true_value, false_value)")]
	[Description("Returns <c>true_value</c> if <c>condition</c> evaluates to true, <c>false_value</c> otherwise.")]
	[Category("Logic")]
	public class If : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 3);
			return new Label(compiler.Labels, source.EvaluateExpression(compiler, Args[source.EvaluateExpression(compiler, Args[0]).NumericValue != 0 ? 1 : 2]).NumericValue);
		}
	}
}
