using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("log10(expression)")]
	[Description("Returns the base 10 logarithm of a specified number.")]
	[Category("Maths")]
	[SeeAlso(typeof(Log))]
	[SeeAlso(typeof(LogE))]
	public class Log10 : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "log10" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Log10(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}

		#endregion

	}
}
