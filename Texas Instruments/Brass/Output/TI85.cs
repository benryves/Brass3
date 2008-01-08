using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

using TexasInstruments.Types;
using TexasInstruments.Types.Variables;


namespace TexasInstruments.Brass.Output {

	[Category("Texas Instruments")]
	[Description("Writes a TI-85 program file (*.85p).")]
	[Remarks(TI8X.Remarks)]
	[SeeAlso(typeof(Output.TI8X)), SeeAlso(typeof(Output.TI83)), SeeAlso(typeof(Output.TI82)), SeeAlso(typeof(Output.TI73)), SeeAlso(typeof(Output.TI86))]
	public class TI85 : TI8X {

		public override string DefaultExtension {
			get { return "85p"; }
		}

		public override CalculatorModel Model {
			get { return CalculatorModel.TI85; }
		}	
	}
}
