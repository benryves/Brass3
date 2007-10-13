using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

using TexasInstruments.Types;
using TexasInstruments.Types.Variables;


namespace TexasInstruments.Brass.Output {

	[Category("Texas Instruments")]
	[Description("Writes a TI-86 program file (*.86p).")]
	[Remarks(TI8X.Remarks)]
	[SeeAlso(typeof(Output.TI8X)), SeeAlso(typeof(Output.TI83)), SeeAlso(typeof(Output.TI82)), SeeAlso(typeof(Output.TI73)), SeeAlso(typeof(Output.TI85))]
	public class TI86 : TI8X {

		public override string Name {
			get { return "ti86"; }
		}

		public override string DefaultExtension {
			get { return "86p"; }
		}

		public override CalculatorModel Model {
			get { return CalculatorModel.TI86; }
		}	
	}
}
