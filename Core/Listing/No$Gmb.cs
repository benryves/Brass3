using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Core.Listing {

	[Category("Debugging")]
	[PluginName("no$gmb")]
	public class NoGmb : IListingWriter {

		public void WriteListing(Compiler compiler, Stream stream) {
			TextWriter ListWriter = new StreamWriter(stream, Encoding.ASCII);
			foreach (Label L in compiler.Labels) {
				if (L.Exported && L != compiler.Labels.ProgramCounter && !string.IsNullOrEmpty(L.Name)) {
					ListWriter.WriteLine("{0:X4}:{1:X4} {2}", L.Page & 0xFFFF, (int)L.NumericValue & 0xFFFF, L.Name);
				}
			}
			ListWriter.Flush();
		}
		
	}
}
