using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("truncate(expression)")]
	[Description("Calculates the integral part of a number.")]
	[Category("Maths")]
	[SeeAlso(typeof(Round))]
	[SeeAlso(typeof(Floor))]
	[SeeAlso(typeof(Ceiling))]
	public class Truncate : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "truncate" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Truncate(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}

		#endregion

	}
}
