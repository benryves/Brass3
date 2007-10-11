using System;
using System.Collections.Generic;
using System.Text;

using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;
using Brass3;

namespace Legacy {
	[Syntax(".asm")]
	[Syntax(".endasm")]
	[Description("Switches compilation or or off.")]
	[Remarks("The code between <c>.endasm</c> and <c>.asm</c> is still parsed. If you don't want it to be parsed at all, consider using multi-line comments instead.")]
	public class EndAsm : IDirective {

		public string Name { get { return this.Names[0]; } }
		public string[] Names { get { return new string[] { "endasm", "asm" }; } }

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
