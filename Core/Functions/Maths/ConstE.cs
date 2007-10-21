using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("e()")]
	[Syntax("e(power)")]
	[Description("Returns the natural logarithmic base specified by the constant e, or e raised to the specified power.")]
	[CodeExample(".echoln e() ; Outputs 2.718...")]
	[Remarks("The value of the constant e is 2.7182818284590452354.")]
	[Category("Maths")]
	[PluginName("e"), PluginName("exp")]
	public class ConstE : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 0, 1);
			return new Label(compiler.Labels, Args.Length == 0 ? Math.E : Math.Exp(source.EvaluateExpression(compiler, Args[0]).NumericValue));
		}
	}
}
