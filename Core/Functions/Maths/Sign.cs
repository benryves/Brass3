using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("sign(expression)")]
	[Description("Returns a value indicating the sign of a number.")]
	[CodeExample(".echoln sign(+100) ; Outputs  1.\r\n.echoln sign(-100) ; Outputs -1.\r\n.echoln sign(0)    ; Outputs  0.")]
	[Category("Maths")]
	public class Sign : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "sign" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, Math.Sign(source.EvaluateExpression(compiler, Args[0]).Value));
		}

		#endregion

	}
}
