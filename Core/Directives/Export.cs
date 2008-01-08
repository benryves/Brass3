using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".export\r\n[labels]\r\n.endexport")]
	[Syntax(".export label [, label [, ... ]]")]
	[Description("Disables or enables automatic exporting of label names.")]
	[PluginName("export"), PluginName("endexport")]
	public class Export : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			switch (directive) {
				case "export":
					int[] Args = source.GetCommaDelimitedArguments(index + 1, 0, int.MaxValue);
					if (Args.Length == 0) {
						compiler.Labels.ExportLabels = true;
					} else {
						foreach (int Arg in Args) {
							Label L = source.EvaluateExpression(compiler, Arg);
							L.Exported = true;
							L.SetImplicitlyCreated();
						}
					}
					break;
				case "endexport":
					source.GetCommaDelimitedArguments(index + 1, 0);
					compiler.Labels.ExportLabels = false;
					break;
				default:
					throw new InvalidOperationException();

			}

			
			
		}

	}
}
