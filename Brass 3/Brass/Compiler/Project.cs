using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;
using System.Xml;

namespace Brass3 {

	public partial class Compiler {

		private Project project;
		/// <summary>
		/// Gets the currently loaded project file.
		/// </summary>
		public Project Project {
			get { return this.project; }
		}

		/// <summary>
		/// Load a project file into the compiler.
		/// </summary>
		/// <param name="p">The project to load.</param>
		public void LoadProject(Project p) {

			this.project = p;

			//TODO: Clear state on project load.

			// Plugins:
			foreach (Project.PluginCollectionInfo Collection in p.Plugins) {
				this.LoadPluginsFromAssembly(Collection.Source, Collection.Exclusions.ToArray());
			}

			if (!string.IsNullOrEmpty(p.Assembler) && this.assemblers.PluginExists(p.Assembler)) this.CurrentAssembler = this.Assemblers[p.Assembler];
			this.SourceFile = GetFullFilename(p, p.SourceFile);
			this.DestinationFile = GetFullFilename(p, p.DestinationFile);

			if (this.OutputWriters.PluginExists(p.OutputWriter)) {
				this.OutputWriter = this.OutputWriters[p.OutputWriter];
			} else {
				this.OnWarningRaised(new NotificationEventArgs(this, "Output writer not set."));
			}

			foreach (KeyValuePair<string, string> ListingWriter in p.ListingFiles) {
				if (this.ListingWriters.PluginExists(ListingWriter.Value)) {
					this.ListingFiles.Add(GetFullFilename(p, ListingWriter.Key), this.ListingWriters[ListingWriter.Value]);
				}
			}
		}

		private static string GetFullFilename(Project p, string relativeName) {
			return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(p.ProjectFilename), relativeName));
		}

	}
}
