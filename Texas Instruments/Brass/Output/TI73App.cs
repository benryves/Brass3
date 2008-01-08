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
	[Description("Writes and signs a TI-73 application (*.7xk).")]
	[Remarks(TI8XApp.Remarks)]
	[SeeAlso(typeof(TI8XApp)), SeeAlso(typeof(Directives.AppHeader)), SeeAlso(typeof(Directives.TIVariableName))]
	public class TI73App : TI8XApp, IOutputWriter {

		public override string DefaultExtension {
			get { return "7xk"; }
		}

		public override void WriteOutput(Compiler compiler, Stream stream) {
			base.Sign(compiler, stream);
		}

	}
}
