using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;
using System.Xml;

namespace Brass3 {

	public partial class Compiler {

		public void LoadProject(Project p) {


			//TODO: Clear state on project load.

			// Plugins:
			foreach (Project.PluginCollectionInfo Collection in p.Plugins) {
				this.LoadPluginsFromAssembly(Collection.Source, Collection.Exclusions.ToArray());
			}

			if (!string.IsNullOrEmpty(p.Assembler) && this.assemblers.PluginExists(p.Assembler)) this.CurrentAssembler = this.Assemblers[p.Assembler];
			this.SourceFile = p.SourceFile;
			this.DestinationFile = p.DestinationFile;

			if (this.OutputWriters.PluginExists(p.OutputWriter)) {
				this.OutputWriter = this.OutputWriters[p.OutputWriter];
			} else {
				this.OnWarningRaised(new CompilerNotificationEventArgs("Output writer not set."));
			}

			foreach (KeyValuePair<string, string> ListingWriter in p.ListingFiles) {
				if (this.ListingWriters.PluginExists(ListingWriter.Value)) {
					this.ListingFiles.Add(ListingWriter.Key, this.ListingWriters[ListingWriter.Value]);
				}
			}
		}

	}
}
