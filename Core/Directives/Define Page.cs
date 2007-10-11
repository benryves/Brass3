using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".defpage page, address, size")]
	[Description("Defines the base address and size of a page.")]
	[Remarks("If you use this directive before <c>.page</c> the program counter and output counter values are set for you automatically.")]
	
	[SeeAlso(typeof(Output.RawPages))]
	[SeeAlso(typeof(Directives.Page))]
	public class DefPage : IDirective {

		public string[] Names {
			get { return new string[] { "defpage" }; }
		}

		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass != AssemblyPass.Pass1) return;
			Output.RawPages PagedRawWriter;
			if ((PagedRawWriter = compiler.GetPluginInstanceFromType(typeof(Output.RawPages)) as Output.RawPages) == null) throw new CompilerExpection(source, "Paged raw writer plugin not loaded.");
			
			int[] Args = Array.ConvertAll<int, int>(source.GetCommaDelimitedArguments(index + 1, 3), delegate(int arg) {
				return (int)source.EvaluateExpression(compiler, arg).Value;
			});

			if (PagedRawWriter.PageDeclarations.ContainsKey(Args[0])) PagedRawWriter.PageDeclarations.Remove(Args[0]);
			PagedRawWriter.PageDeclarations.Add(Args[0], new Core.Output.RawPages.PageDeclaration(Args[1], Args[2]));

		}

	}
}
