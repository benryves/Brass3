using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace SegaMasterSystem.Output {
	[Description("Outputs a Game Gear ROM file with a Sega and SDSC header.")]
	[Remarks("This plugin is based on the raw pages output plugin, so the rules between the two are roughly the same.")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(SmsRom))]
	[SeeAlso(typeof(Core.Output.RawPages))]
	public class GameGearRom : SmsRom {

		protected override bool IsGameGear {
			get { return true; }
		}

		public override string DefaultExtension {
			get { return "gg"; }
		}

		public override string Name {
			get { return "ggrom"; }
		}

		public GameGearRom(Compiler c)
			: base(c) {
		}

	}
}
