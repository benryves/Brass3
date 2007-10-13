using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("floor(expression)")]
	[Description("Returns the largest integer less than or equal to the specified number.")]
	[Category("Maths")]
	[SeeAlso(typeof(Round))]
	[SeeAlso(typeof(Truncate))]
	[SeeAlso(typeof(Ceiling))]
	public class Floor : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "floor" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Floor(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}

		#endregion

	}
}
