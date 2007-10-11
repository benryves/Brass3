using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
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
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "round" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1, 2);
			return new Label(compiler.Labels, Math.Round(
				source.EvaluateExpression(compiler, Args[0]).Value,
				Args.Length == 1 ? 0 : (int)source.EvaluateExpression(compiler, Args[1]).Value
			));
		}

		#endregion

	}
}
