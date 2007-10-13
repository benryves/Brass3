using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("log(expression, base)")]
	[Description("Returns the logarithm of a specified number in a specified base.")]
	[Category("Maths")]
	[SeeAlso(typeof(Log10))]
	[SeeAlso(typeof(LogE))]
	public class Log : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "log" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 2);
			return new Label(compiler.Labels, Math.Log(source.EvaluateExpression(compiler, Args[0]).NumericValue, source.EvaluateExpression(compiler, Args[1]).NumericValue));
		}

		#endregion

	}
}
