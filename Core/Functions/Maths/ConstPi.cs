using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("pi()")]
	[Description("Returns the ratio of the circumference of a circle to its diameter, specified by the constant, π.")]
	[CodeExample(".echoln pi() ; Outputs 3.142...")]
	[Remarks("The value of this constant is 3.14159265358979323846.")]
	[Category("Maths")]
	public class Pi : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 0);
			return new Label(compiler.Labels, Math.PI);
		}
	}
}
