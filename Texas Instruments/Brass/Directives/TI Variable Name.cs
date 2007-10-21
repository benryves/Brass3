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
	[CodeExample("/* The following would create a group file\r\ncontaining two programs, PRGMA and PRGMB. */\r\n\r\n.page 1\r\n.tivariablename \"PRGMA\"\r\n\r\n.org $9D93\r\n\tret\r\n\r\n.page 2\r\n.tivariablename \"PRGMB\"\r\n\r\n.org $9D93\r\n\tret\r\n\r\n/* Naturally, you need to be using a TI program\r\noutput writing plugin for this to work. */")]
	[SeeAlso(typeof(Output.TI8X)), SeeAlso(typeof(Output.TI83)), SeeAlso(typeof(Output.TI82)), SeeAlso(typeof(Output.TI73)), SeeAlso(typeof(Output.TI86)), SeeAlso(typeof(Output.TI85))]
	public class TIVariableName : IDirective {

		internal Dictionary<int, string> VariableNames;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass == AssemblyPass.Pass2) {
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
