using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using System.ComponentModel;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Output {

	[Description("Writes output as a raw binary file.")]
	[Warning("Gaps between non-consecutive addresses are ignored, so if you need to have filler data between addresses use directive that inserts that data rather than just offsetting the current program counter.")]
	[Remarks("The assembled data are output in order of page followed by address. This output writer plugin is compatible with output modifier plugins that change the size of output data.")]
	[CodeExample("Gaps between non-consecutivate addresses are ignored.", "$: = $0000\r\n.db 1\r\n\r\n$: = $1000\r\n.db 2\r\n\r\n; The above outputs a 2-byte file.")]
	public class Raw : IOutputWriter {

		public string DefaultExtension {
			get { return "bin"; }
		}

		public void WriteOutput(Compiler compiler, Stream stream) {

			if (compiler.Output.Length == 0) return;
			
			BinaryWriter BW = new BinaryWriter(stream);
			foreach (Compiler.OutputData OD in compiler.Output) {
				BW.Write(OD.Data);
			}
		}

	
	}
}