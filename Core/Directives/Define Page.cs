using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".defpage page, size, address")]
	[Description("Defines the base address and size of a page.")]
	[Remarks("If you use this directive before using the <see cref=\"page\" /> directive the program counter and output counter values are set for you automatically.")]
	[Category("Output Manipulation")]
	[SeeAlso(typeof(Output.RawPages))]
	[SeeAlso(typeof(Directives.Page))]
	public class DefPage : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			Output.RawPages PagedRawWriter;
			if ((PagedRawWriter = compiler.GetPluginInstanceFromType(typeof(Output.RawPages)) as Output.RawPages) == null) throw new CompilerExpection(source, string.Format(Strings.ErrorPluginNotLoaded, "rawpages"));

			int[] Args = Array.ConvertAll<object, int>(source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.Value, 
				TokenisedSource.ArgumentType.Value, 
				TokenisedSource.ArgumentType.Value
			}), delegate(object arg) {
				return (int)(double)arg;
			});

			switch (compiler.CurrentPass) {
				case AssemblyPass.CreatingLabels: {

						if (PagedRawWriter.PageDeclarations.ContainsKey(Args[0])) PagedRawWriter.PageDeclarations.Remove(Args[0]);
						PagedRawWriter.PageDeclarations.Add(Args[0], new Core.Output.RawPages.PageDeclaration(Args[1], Args[2]));

						Page P;
						if ((P = compiler.GetPluginInstanceFromType<Page>()) != null) {
							if (P.SwitchedToPages.Contains(Args[0])) {
								compiler.OnWarningRaised(string.Format(Strings.ErrorDefPagePageUsedBeforeDefined, Args[0]));
							}
						}

					} break;
				case AssemblyPass.WritingOutput: {
						if (compiler.GetOutputDataOnPage(Args[0]).Length > 0) {
							compiler.OnWarningRaised(string.Format(Strings.ErrorDefPageDataAlreadyOutput, Args[0]));
						}
					} break;

			}
		}
	}
}