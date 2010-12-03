using System;
using System.Collections.Generic;
using System.IO;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;

namespace BeeDevelopment.FileSizeProfileReport {
	public class FileSizeProfileReport : IListingWriter {

		private Dictionary<string, long> FileSizeCounter = new Dictionary<string,long>();

		public FileSizeProfileReport(Compiler compiler) {
			compiler.CompilationBegun += (sender, e) => {
				this.FileSizeCounter.Clear();
			};
			compiler.OutputDataWritten += (sender, e) => {
				if (!e.Data.Background && compiler.CurrentFile != null) {
					long CurrentLength = 0;
					if (FileSizeCounter.TryGetValue(compiler.CurrentFile, out CurrentLength)) {
						FileSizeCounter.Remove(compiler.CurrentFile);
					}
					FileSizeCounter.Add(compiler.CurrentFile, CurrentLength + e.Data.Data.Length);
				}
			};
		}

		public void WriteListing(Compiler compiler, Stream stream) {
			var Writer = new StreamWriter(stream);
			var FilesBySize = new List<string>(this.FileSizeCounter.Keys);
			FilesBySize.Sort((a, b) => this.FileSizeCounter[b].CompareTo(this.FileSizeCounter[a]));
			foreach (var item in FilesBySize) {
				var Size = this.FileSizeCounter[item];
				Writer.WriteLine("{0}\t{1}", Size.ToString().PadRight(8), item);
			}
			Writer.Flush();
		}

	}
}
