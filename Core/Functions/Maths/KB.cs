using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("kb(value)")]
	[Description("Returns the number of bytes in <c>value</c> kilobytes.")]
	[CodeExample(".echoln kb(16) ; Outputs 16384.")]
	[Warning("A kilobyte here means 1024 bytes.")]
	[Category("Maths")]
	public class KiloByte : IFunction {
		#region IFunction Members

		public string[] Names {
			get { return new string[] { "kb" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			return new Label(compiler.Labels, 1024 * (source.EvaluateExpression(compiler, Args[0]).Value));
		}

		#endregion

	}
}
