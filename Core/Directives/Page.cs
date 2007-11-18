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
	[Remarks(@"This directive sets the page number of the program counter and output counter.
The first time a particular page is switched to using this directive it checks to see if the page has been defined (with <see cref=""defpage""/>) and sets the program counter and output counter accordingly (otherwise it simply resets both to zero).
Subsequent use of this directive only changes the page numbers and not the program counter or output counter.")]
	[Category("Output Manipulation")]
	[SeeAlso(typeof(DefPage))]

	public class Page : IDirective {

		Queue<int> PageOffsetAddresses;
		internal List<int> SwitchedToPages;

		public Page(Compiler compiler) {
			
			this.PageOffsetAddresses = new Queue<int>();
			this.SwitchedToPages = new List<int>();

			compiler.PassBegun += delegate(object sender, EventArgs e) {
				if (compiler.CurrentPass == AssemblyPass.CreatingLabels) {
					this.PageOffsetAddresses.Clear();
					this.SwitchedToPages.Clear();
				}
			};
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			int Argument = (int)(double)source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.ValueArgument)[0];
			
			compiler.Labels.ProgramCounter.Page = Argument;
			compiler.Labels.OutputCounter.Page = Argument;

			switch (compiler.CurrentPass) {
				case AssemblyPass.CreatingLabels:

					if (!this.SwitchedToPages.Contains(compiler.Labels.ProgramCounter.Page)) {
						int DefaultAddress = 0;
						Output.RawPages PagedRawWriter;
						if ((PagedRawWriter = compiler.GetPluginInstanceFromType(typeof(Output.RawPages)) as Output.RawPages) != null) {
							Output.RawPages.PageDeclaration PD;
							if (PagedRawWriter.PageDeclarations.TryGetValue(compiler.Labels.ProgramCounter.Page, out PD)) {
								DefaultAddress = PD.Address;
							}
						}
						compiler.Labels.ProgramCounter.NumericValue = DefaultAddress;
						compiler.Labels.OutputCounter.NumericValue = DefaultAddress;
						this.SwitchedToPages.Add(compiler.Labels.ProgramCounter.Page);
					}

					PageOffsetAddresses.Enqueue((int)compiler.Labels.ProgramCounter.NumericValue);
					PageOffsetAddresses.Enqueue((int)compiler.Labels.OutputCounter.NumericValue);

					break;
				case AssemblyPass.WritingOutput:
					compiler.Labels.ProgramCounter.NumericValue = PageOffsetAddresses.Dequeue();
					compiler.Labels.OutputCounter.NumericValue = PageOffsetAddresses.Dequeue();
					break;
			}

			
			

		}

	}
}
