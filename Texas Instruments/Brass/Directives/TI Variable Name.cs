using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace TexasInstruments.Brass.Directives {

	[Category("Texas Instruments")]
	[Description("Sets the variable name for the current calculator program.")]
	[Remarks(@"If you are using one of the assembly program output writers (<see cref=""ti73""/>, <see cref=""ti82""/>, <see cref=""ti83""/>, <see cref=""ti85""/>, <see cref=""ti86""/> or <see cref=""ti8x""/>) you can specify one variable name per page. (Each page is output as a seperate variable with its own name).
If you are using one of the application output writers (<see cref=""ti73app"" /> or <see cref=""ti8xapp""/>) only the variable name defined on page 0 will be used.")]

	[CodeExample("Creating group file variables.", @"/* The following would create a group file
containing two programs, PRGMA and PRGMB. */

.page 1
.tivariablename ""PRGMA""

	.org $9D93
	ret

.page 2
.tivariablename ""PRGMB""

	.org $9D93
	ret

/* Naturally, you need to be using a TI program
output writing plugin for this to work. */")]

	[SeeAlso(typeof(Output.TI8X)), SeeAlso(typeof(Output.TI83)), SeeAlso(typeof(Output.TI82)), SeeAlso(typeof(Output.TI73)), SeeAlso(typeof(Output.TI86)), SeeAlso(typeof(Output.TI85))]
	public class TIVariableName : IDirective {

		internal Dictionary<int, string> VariableNames;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass == AssemblyPass.WritingOutput) {
				int Page = compiler.Labels.ProgramCounter.Page;
				int NameIndex = source.GetCommaDelimitedArguments(index + 1, 1)[0];
				if (VariableNames.ContainsKey(Page)) VariableNames.Remove(Page);
				VariableNames.Add(Page, source.GetExpressionStringConstant(compiler, NameIndex));
			}
		}
		
		public TIVariableName(Compiler c) {
			this.VariableNames = new Dictionary<int, string>();
			c.PassBegun += delegate(object sender, EventArgs e) {
				this.VariableNames.Clear();
			};

		}


	}
}
