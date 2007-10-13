using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("acos(expression)")]
	[Description("Returns the angle whose cosine is the specified number.")]
	[Remarks("Angles are in radians.")]
	[Category("Maths")]
	public class ACos : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "acos" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Acos(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}

		#endregion

	}
}
