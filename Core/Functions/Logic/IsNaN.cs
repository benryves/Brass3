using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("isnan(expression)")]
	[Description("Returns true if the expression evaluates to NaN.")]
	[Category("Logic")]
	[SeeAlso(typeof(IsInf))]
	[SeeAlso(typeof(IsPosInf))]
	[SeeAlso(typeof(IsNegInf))]
	public class IsNaN : IFunction {

		public string[] Names {
			get { return new string[] { "isnan" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, double.IsNaN(source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(0, 1)[0]).NumericValue) ? 1 : 0);
		}

	}
}