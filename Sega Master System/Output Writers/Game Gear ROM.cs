using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace SegaMasterSystem.Output {
	[Description("Outputs a Game Gear ROM file with a Sega and SDSC header.")]
	[Remarks("This plugin is based on the raw pages output plugin, so the rules between the two are roughly the same.")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(SmsRom))]
	[SeeAlso(typeof(Core.Output.RawPages))]
	public class GGRom : SmsRom {

		protected override bool IsGameGear {
			get { return true; }
		}

		public override string DefaultExtension {
			get { return "gg"; }
		}

		public GGRom(Compiler c)
			: base(c) {
		}

	}
}
