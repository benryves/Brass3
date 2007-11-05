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
		/// <param name="project">The project to load.</param>
		public void LoadProject(Project project) {

			this.project = project;

			//TODO: Clear state on project load.

			// Plugins:
			foreach (Project.PluginCollectionInfo Collection in project.Plugins) {
				this.LoadPluginsFromAssembly(Collection.Source, Collection.Exclusions.ToArray());
			}

			if (!string.IsNullOrEmpty(project.Assembler) && this.assemblers.PluginExists(project.Assembler)) this.CurrentAssembler = this.Assemblers[project.Assembler];
			this.SourceFile = GetFullFilename(project, project.SourceFile);
			this.DestinationFile = GetFullFilename(project, project.DestinationFile);
			this.LoadStateFromProject(project);
		}

		private void LoadStateFromProject(Project project) {

			

			if (this.OutputWriters.PluginExists(project.OutputWriter)) {
				this.OutputWriter = this.OutputWriters[project.OutputWriter];
			} else {
				this.OnWarningRaised(new NotificationEventArgs(this, "Output writer not set."));
			}

			if (this.StringEncoders.PluginExists(project.StringEncoder)) {
				this.StringEncoder = this.StringEncoders[project.StringEncoder];
			} else {
				this.OnWarningRaised(new NotificationEventArgs(this, "String encoder not set."));
			}


			this.ListingWriters.Clear();

			foreach (KeyValuePair<string, string> ListingWriter in project.ListingFiles) {
				if (this.ListingWriters.PluginExists(ListingWriter.Value)) {
					this.ListingFiles.Add(GetFullFilename(project, ListingWriter.Key), this.ListingWriters[ListingWriter.Value]);
				}
			}

		}

		private static string GetFullFilename(Project p, string relativeName) {
			return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(p.ProjectFilename), relativeName));
		}

	}
}
