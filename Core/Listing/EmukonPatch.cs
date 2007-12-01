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
	[Remarks("resources://Core.Documentation/EmukonPatchRemarks")]
	[Description("resources://Core.Documentation/EmukonPatchDescription")]

	public class EmukonPatch : IListingWriter {

		public void WriteListing(Compiler compiler, Stream stream) {
			TextWriter ListWriter = new StreamWriter(stream, Encoding.ASCII);
			foreach (Label L in compiler.Labels) {
				if (L.Exported && L != compiler.Labels.ProgramCounter && !string.IsNullOrEmpty(L.Name)) {
					ListWriter.WriteLine("Label {0}, {1}", (int)L.NumericValue, L.Page);
				}
			}
			ListWriter.Flush();
		}
		
	}
}
