using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using System.ComponentModel;
using Brass3.Attributes;

namespace Core.Directives {

	[Description("Switches the compiler between big and little endian modes.")]
	[Syntax(".little")]
	[Syntax(".big")]
	[Remarks("There is no way to ensure that third-party plugins respect the compiler's endian mode. If you encounter problems with a particular plugin, please contact its author.")]
	[Category("Data")]
	[CodeExample(".big\r\n.int $123456 ; Outputs $12, $34, $56.")]
	[CodeExample(".little\r\n.int $123456 ; Outputs $56, $34, $12.")]
	public class Endian : IDirective {
		#region IDirective Members

		public string[] Names {
			get {
				return new string[] { "little", "big" };
			}
		}

		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			switch (directive) {
				case "little":
					compiler.Endianness = Endianness.Little;
					break;
				case "big":
					compiler.Endianness = Endianness.Big;
					break;
			}
		}


		#endregion

	}
}
