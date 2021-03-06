using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;
using System.Globalization;

namespace SegaMasterSystem.Directives {
	[Description("Sets the region the Sega header.")]
	[Syntax(".segaregion japan | export | international")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(SegaProduct))]
	[SeeAlso(typeof(SegaVersion))]
	[SeeAlso(typeof(Output.SmsRom))]
	[SeeAlso(typeof(Output.GGRom))]
	public class SegaRegion : IDirective {

		public enum Regions { Japan, Export, International };
		private Regions region = Regions.Export;

		/// <summary>
		/// Gets or sets the ROM region.
		/// </summary>
		public Regions Region {
			get { return this.region; }
			set {
				if (!Enum.IsDefined(typeof(Regions), value)) throw new ArgumentException("Invalid region.");
				this.region = value;
			}
		}

		public SegaRegion(Compiler c) {
			c.CompilationBegun += delegate(object sender, EventArgs e) {
				this.Region = Regions.Export;
			};
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			TokenisedSource.Token Region = source.GetExpressionToken(source.GetCommaDelimitedArguments(index + 1, 1)[0]);
			switch (Region.DataLowerCase) {
				case "japan":
					this.Region = Regions.Japan;
					break;
				case "export":
					this.Region = Regions.Export;
					break;
				case "international":
					this.Region = Regions.International;
					break;
				default:
					throw new CompilerException(Region, "Unrecognised region '" + Region.Data + "' (expected Japan, Export or International).");
			}

		}
	}
}
