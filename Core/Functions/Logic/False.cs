using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("false()")]
	[Syntax("false(conditional)")]
	[Description("Returns a constant that evaluates to false, or false if the conditional also evaluates to false.")]
	[CodeExample("Use as a constant", ".echoln false() ; Outputs 0.")]
	[CodeExample("Use as a function", ".echoln false(0) ; Outputs 1.")]
	[CodeExample("Use as a function", ".echoln false(-100) ; Outputs 0.")]
	[Remarks("Use of <c>false</c> as a function is useful if you want to guarantee that the result of a conditional expression evaluates to exactly 1 rather than non-zero.")]
	[Category("Logic")]
	[SeeAlso(typeof(True))]
	public class False : IFunction {
	
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 0, 1);
			return new Label(compiler.Labels, Args.Length == 0 ? 0 : ((source.EvaluateExpression(compiler, Args[0]).NumericValue != 0) ? 0 : 1));
		}

	}
}
