using System;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Brass.TexasInstruments {

	[Description("Unsquishes output binary data into ASCII text.")]
	[Syntax(".squish")]
	[Syntax(".unsquish")]
	[Remarks("Unsquished data is output as hexadecmial ASCII.")]
	[PluginName("squish"), PluginName("unsquish")]
	public class Squish : IOutputModifier, IDirective {


		private bool Squishing = true;

		public Squish(Compiler c) {
			c.CompilationBegun += (sender, e) => this.Squishing = true;
			c.OutputDataWritten += (sender, e) => e.Data.SetStoredData(typeof(Squish), this.Squishing);
			c.BeforeOutputDataModified += (sender, e) => this.Squishing = (bool)(e.Data.GetStoredData(typeof(Squish)) ?? true);
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
