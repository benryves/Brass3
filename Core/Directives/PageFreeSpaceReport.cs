using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3;
using System.ComponentModel;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Directives {
	[Description("Enables or disables display of a free space report for the raw pages plugin.")]
	[Syntax(".pagefreespacereport [enabled]")]
	[SeeAlso(typeof(Output.RawPages))]
	public class PageFreeSpaceReport : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			var RawPagesPlugin = compiler.GetPluginInstanceFromType<Output.RawPages>();
			if (RawPagesPlugin == null) return;
			var ReportMode = Array.ConvertAll(
				source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional }),
				v => Convert.ToBoolean(v)
			);
			RawPagesPlugin.DisplayPageFreeSpace = (ReportMode.Length == 0 || ReportMode[0]);
		}

		
	}
}
