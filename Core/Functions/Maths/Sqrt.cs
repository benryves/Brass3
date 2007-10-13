using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("sqrt(expression)")]
	[Description("Returns the square root of a specified number.")]
	[Category("Maths")]
	public class Sqrt : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "sqrt" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Sqrt(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}

		#endregion

	}
}
