using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;
using System.Globalization;

namespace SegaMasterSystem.Directives {
	[Description("Sets the version number in the Sega header.")]
	[Remarks("The version number needs be between 0 and 15.")]
	[Syntax(".segaversion version")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(SegaRegion))]
	[SeeAlso(typeof(SegaProduct))]
	[SeeAlso(typeof(Output.SmsRom))]
	[SeeAlso(typeof(Output.GGRom))]
	public class SegaVersion : IDirective {

		private int version = 0;
		/// <summary>
		/// Gets or sets the version number.
		/// </summary>
		public int Version {
			get { return this.version; }
			set {
				if (value < 0 || value > 15) throw new ArgumentOutOfRangeException("Version number must be between 0 and 15.");
				this.version = value;
			}
		}

		public SegaVersion(Compiler c) {
			c.CompilationBegun += delegate(object sender, EventArgs e) {
				this.Version = 0;
			};
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			this.Version = (int)source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(index + 1, 1)[0]).NumericValue;
		}


	}
}
