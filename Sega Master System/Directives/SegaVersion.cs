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
	[SeeAlso(typeof(Output.GameGearRom))]
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
			c.PassBegun += delegate(object sender, EventArgs e) {
				if (c.CurrentPass == AssemblyPass.Pass1) {
					this.Version = 0;
				}
			};			
		}

		public string Name { get { return this.Names[0]; } }
		public string[] Names { get { return new string[] { "segaversion" }; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass == AssemblyPass.Pass2) {
				this.Version = (int)source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(index + 1, 1)[0]).NumericValue;
			}
		}


	}
}
