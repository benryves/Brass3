using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".echo expression [, expression [, ... ]]")]
	[Description("Writes the text representation of the specified expressions to the console.\r\n<c>echoln</c> writes a new-line sequence at the end of the expression list.")]
	[Remarks("Expressions can be either numeric or string constants. This directive is only invoked during the second pass.")]
	[CodeExample(".echoln \"Program counter = \", $")]
	[PluginName("echo"), PluginName("echoln")]
	public class Echo : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			foreach (int Expression in source.GetCommaDelimitedArguments(index + 1)) {
				compiler.OnMessageRaised(new Compiler.NotificationEventArgs(compiler, source.EvaluateExpression(compiler, Expression).StringValue));
			}
			if (directive == "echoln") compiler.OnMessageRaised(new Compiler.NotificationEventArgs(compiler, Environment.NewLine));
		}

	}
}
