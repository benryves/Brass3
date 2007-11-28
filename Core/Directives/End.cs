using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".end")]
	[Description("Stops all further assembling.")]
	[Category("Flow Control")]
	public class End : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			source.GetCommaDelimitedArguments(index + 1, 0);
			compiler.SwitchOff(null);
		}

	}
}
