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
	public class Page : IDirective {

		public string[] Names {
			get { return new string[] { "page" }; }
		}

		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);
			compiler.Labels.ProgramCounter.Page = (int)source.EvaluateExpression(compiler, Args[0]).Value;

			Output.RawPages PagedRawWriter;
			if ((PagedRawWriter = compiler.GetPluginInstanceFromType(typeof(Output.RawPages)) as Output.RawPages) != null) {
				Output.RawPages.PageDeclaration PD;
				if (PagedRawWriter.PageDeclarations.TryGetValue(compiler.Labels.ProgramCounter.Page, out PD)) {
					compiler.Labels.ProgramCounter.Value = PD.Address;
					compiler.OutputCounter = PD.Address;
				}
			}

		}

	}
}
