using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using System.ComponentModel;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Output {

	public class ContiguousRaw : IOutputWriter {

		public string DefaultExtension {
			get { return "bin"; }
		}

		public void WriteOutput(Compiler compiler, Stream stream) {

			if (compiler.Output.Length == 0) return;
			
			BinaryWriter BW = new BinaryWriter(stream);
			int? LastWrittenAddress = null;
			foreach (Compiler.OutputData OD in compiler.Output) {
				if (LastWrittenAddress.HasValue) {
					for (int i = LastWrittenAddress.Value; i < OD.OutputCounter; i++) {
						BW.Write((byte)0x00);
					}
				}
				BW.Write(OD.Data);
				LastWrittenAddress = OD.OutputCounter + 1;
			}
		}

	
	}
}