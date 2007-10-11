using System;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Brass.TexasInstruments {

	[Description("Unsquishes output binary data into ASCII text.")]
	[Syntax(".squish")]
	[Syntax(".unsquish")]
	[Remarks("Unsquished data is output as hexadecmial ASCII.")]
	public class Squish : IOutputModifier, IDirective {

		#region Name

		public string[] Names {
			get { return new string[] { "squish", "unsquish" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		#endregion

		private bool Squishing = true;

		public Squish(Compiler c) {
			c.PassBegun += new EventHandler(delegate(object sender, EventArgs e) { this.Squishing = true; });
		}

		public byte[] ModifyOutput(Compiler compiler, byte data) {
			if (this.Squishing) {
				return new byte[] { data };
			} else {
				return Encoding.ASCII.GetBytes(data.ToString("X2"));
			}
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			source.GetCommaDelimitedArguments(index + 1, 0);
			this.Squishing = directive == "squish";
		}
	}
}
