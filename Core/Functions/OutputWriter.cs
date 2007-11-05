using System;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Core.Functions {

	[Description("Gets or sets the current output writer name.")]
	[Syntax("outputwriter([plugin name])")]
	[CodeExample("outputwriter(\"raw\")\r\n.echoln outputwriter()\r\n; Outputs \"raw\".")]
	public class OutputWriter : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			object[] Arguments = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.SingleToken | TokenisedSource.ArgumentType.String | TokenisedSource.ArgumentType.Optional
			});

			if (Arguments.Length == 1) {
				string Plugin = Arguments[0] as string;
				if (compiler.OutputWriters.PluginExists(Plugin)) {
					compiler.OutputWriter = compiler.OutputWriters[Plugin];
				} else {
					compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, new CompilerExpection(source, string.Format("Output plugin '{0}' not found.", Plugin))));
				}
			}

			return new Label(compiler.Labels, Compiler.GetPluginName(compiler.OutputWriter));

		}

	}
}
