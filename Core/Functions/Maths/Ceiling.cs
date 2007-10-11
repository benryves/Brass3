using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("ceiling(expression)")]
	[Description("Returns the smallest integer greater than or equal to the specified number.")]
	[Category("Maths")]
	[SeeAlso(typeof(Round))]
	[SeeAlso(typeof(Floor))]
	[SeeAlso(typeof(Truncate))]
	public class Ceiling : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "ceiling" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Ceiling(source.EvaluateExpression(compiler, Args[0]).Value));
		}

		#endregion

	}
}
