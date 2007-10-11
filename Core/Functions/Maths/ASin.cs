using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("asin(expression)")]
	[Description("Returns the angle whose sine is the specified number.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class ASin : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "asin" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Asin(source.EvaluateExpression(compiler, Args[0]).Value));
		}

		#endregion

	}
}
