using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("toradians(expression)")]
	[Description("Converts an angle in degrees to one in degrees.")]
	[Category("Maths")]
	[SeeAlso(typeof(ToDegrees))]
	public class ToRadians : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "toradians" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, source.EvaluateExpression(compiler, Args[0]).NumericValue * Math.PI / 180d);
		}

		#endregion

	}
}
