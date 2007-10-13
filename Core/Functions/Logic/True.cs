using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("true()")]
	[Syntax("true(conditional)")]
	[Description("Returns a constant that evaluates to true, or true if the conditional also evaluates to true.")]
	[CodeExample("Use as a constant.", ".echoln true() ; Outputs 1.")]
	[CodeExample("Use as a function.", ".echoln true(0) ; Outputs 0.")]
	[CodeExample("Use as a function.", ".echoln true(-100) ; Outputs 1.")]
	[Remarks("Use of <c>true</c> as a function is useful if you want to guarantee that the result of a conditional expression evaluates to exactly 1 rather than non-zero.")]
	[Category("Logic")]
	[SeeAlso(typeof(False))]
	public class True : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "true" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 0, 1);
			return new Label(compiler.Labels, Args.Length == 0 ? 1 : ((source.EvaluateExpression(compiler, Args[0]).NumericValue != 0) ? 1 : 0));
		}

		#endregion

	}
}
