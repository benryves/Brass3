using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Legacy.Directives {

	[Description("Switches the compiler between big and little endian modes.")]
	[PluginName("lsfirst"), PluginName("msfirst")]
	[Remarks("This is based on a TASM directive.")]
	[CodeExample("<c>.lsfirst</c> is the same as <c>.little</c>.", ".lsfirst\r\n.int $123456 ; Outputs $56, $34, $12.")]
	[CodeExample("<c>.big</c> is the same as <c>.big</c>.", ".msfirst\r\n.int $123456 ; Outputs $12, $34, $56.")]
	[Category("Output Manipulation")]
	[SeeAlso(typeof(Core.Directives.Endian))]
	public class LsMsFirst : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			switch (directive) {
				case "lsfirst":
					compiler.Endianness = Endianness.Little;
					break;
				case "msfirst":
					compiler.Endianness = Endianness.Little;
					break;
			}
		}
	}
}
