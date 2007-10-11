using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("max(expression, expression)")]
	[Description("Returns the larger of two numbers.")]
	[CodeExample(".echoln max(12, 34) ; Outputs 34.")]
	[Category("Maths")]
	public class Max : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "max" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 2);
			return new Label(compiler.Labels, Math.Max(
				source.EvaluateExpression(compiler, Args[0]).Value,
				source.EvaluateExpression(compiler, Args[1]).Value
			));
		}

		#endregion

	}
}
