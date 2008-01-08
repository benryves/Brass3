using System;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Functions {

	[Description("Returns true if the current plugin matches passed name.")]
	[Remarks("Plugins can have multiple aliases, and comparing the names via strings is rather awkward - hence this function.")]
	[PluginName("outputwriteris"), PluginName("assembleris"), PluginName("stringencoderis")]
	[CodeExample(@".echo 'Output writer is '
#if outputwriteris('ti8x')
	.echo 'TI-83 Plus'
#elseif outputwriteris('ti83')
	.echo 'TI-83'
#else
	.echo 'not recognised'
#endif
.echoln '.'")]
	public class PluginIs : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			string PluginName = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] {
				TokenisedSource.ArgumentType.SingleToken | TokenisedSource.ArgumentType.String
			})[0] as string;

			bool Result = false;
			
			IPlugin Reference = null;
			IPlugin Current = null;

			switch (function) {
				case "outputwriteris":
					Current = compiler.OutputWriter;
					Reference = GetPlugin(compiler.OutputWriters, PluginName);
					break;
				case "assembleris":
					Current = compiler.CurrentAssembler;
					Reference = GetPlugin(compiler.Assemblers, PluginName);
					break;
				case "stringencoderis":
					Current = compiler.StringEncoder;
					Reference = GetPlugin(compiler.StringEncoders, PluginName);
					break;
			}

			if (Reference != null && Current != null) {
				Result = Reference == Current;
			}

			return new Label(compiler.Labels, Result ? 1d : 0d);

		}

		private static T GetPlugin<T>(PluginCollection<T> plugins, string name) where T : class, IPlugin {
			return plugins.Contains(name) ? plugins[name] : null;
		}

	}
}
