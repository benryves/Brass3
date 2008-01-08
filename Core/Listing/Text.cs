using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

namespace Core.Listing {

	public class Text : IListingWriter {

		public void WriteListing(Compiler compiler, Stream stream) {
			TextWriter ListWriter = new StreamWriter(stream, Encoding.ASCII);
			foreach (Compiler.SourceStatement Statement in compiler.Statements) {
				ListWriter.WriteLine(Statement.ToString());
				foreach (Compiler.OutputData Data in Statement.GeneratedOutputData) {
					foreach (byte b in Data.Data) {
						ListWriter.Write(b.ToString("X2") + " ");
					}
				}
				ListWriter.WriteLine();
			}
			ListWriter.Flush();
		}
		
	}
}
