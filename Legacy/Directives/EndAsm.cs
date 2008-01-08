using System;
using System.Collections.Generic;
using System.Text;

using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

using System.ComponentModel;
using BeeDevelopment.Brass3;

namespace Legacy.Directives {
	[Syntax(".asm")]
	[Syntax(".endasm")]
	[Description("Switches compilation or or off.")]
	[Remarks("The code between <c>.endasm</c> and <c>.asm</c> is still parsed. If you don't want it to be parsed at all, consider using multi-line comments instead.")]
	[PluginName("endasm"), PluginName("asm")]
	public class EndAsm : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			
			source.GetCommaDelimitedArguments(index + 1, 0);

			switch (directive) {
				case "endasm":
					compiler.SwitchOff(typeof(EndAsm));
					break;
				case "asm":
					compiler.SwitchOn();
					break;
			}


		}
	}
}
