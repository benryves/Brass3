using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".page <param>number</param>")]
	[Description("Sets the current page number.")]
	[CodeExample(".page 2\r\n.echoln \"Current page = \", :$")]
	[Category("Output Manipulation")]
	public class Page : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);
			compiler.Labels.ProgramCounter.Page = (int)source.EvaluateExpression(compiler, Args[0]).NumericValue;

			Output.RawPages PagedRawWriter;
			if ((PagedRawWriter = compiler.GetPluginInstanceFromType(typeof(Output.RawPages)) as Output.RawPages) != null) {
				Output.RawPages.PageDeclaration PD;
				if (PagedRawWriter.PageDeclarations.TryGetValue(compiler.Labels.ProgramCounter.Page, out PD)) {
					compiler.Labels.ProgramCounter.NumericValue = PD.Address;
					compiler.Labels.OutputCounter.NumericValue = PD.Address;
				}
			}

		}

	}
}
