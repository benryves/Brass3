using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using BeeDevelopment.Brass3.Plugins;
using System.Xml;

namespace BeeDevelopment.Brass3 {

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

			if (!string.IsNullOrEmpty(project.Assembler) && this.assemblers.Contains(project.Assembler)) this.CurrentAssembler = this.Assemblers[project.Assembler];
			this.SourceFile = GetFullFilename(project, project.SourceFile);
			if (string.IsNullOrEmpty(project.DestinationFile)) {
				this.DestinationFile = null;
			} else {
				this.DestinationFile = GetFullFilename(project, project.DestinationFile);
			}
			this.LoadStateFromProject(project);
		}

		private void LoadStateFromProject(Project project) {

			if (this.OutputWriters.Contains(project.OutputWriter)) {
				this.OutputWriter = this.OutputWriters[project.OutputWriter];
			} else {
				this.OnWarningRaised(new NotificationEventArgs(this, "Output writer not set."));
			}

			if (project.StringEncoder == null ) {
				this.StringEncoder = null;
			} else if (this.StringEncoders.Contains(project.StringEncoder)) {
				this.StringEncoder = this.StringEncoders[project.StringEncoder];
			} else {
				this.OnWarningRaised(new NotificationEventArgs(this, "String encoder not set."));
			}

			this.Header = project.Header;
			this.Footer = project.Footer;

			this.ListingFiles.Clear();

			foreach (KeyValuePair<string, string> ListingWriter in project.ListingFiles) {
				if (this.ListingWriters.Contains(ListingWriter.Value)) {
					this.ListingFiles.Add(GetFullFilename(project, ListingWriter.Key), this.ListingWriters[ListingWriter.Value]);
				}
			}

			// Set debugger.
			if (!string.IsNullOrEmpty(project.Debugger)) {
				if (this.Debuggers.Contains(project.Debugger)) {
					this.Debugger = this.Debuggers[project.Debugger];
				} else {
					this.OnWarningRaised(new NotificationEventArgs(this, "Debugger not set."));
				}
			}
			

			foreach (Project.PredefinedLabel PL in project.Labels) {
				Label L;
				TokenisedSource.Token T = new TokenisedSource.Token(PL.Name);
				if (this.Labels.TryParse(T, out L)) this.Labels.Remove(L);
				L = this.Labels.Create(T);
				switch (PL.Type) {
					case  Project.PredefinedLabel.CreationType.String:
						L.StringValue = PL.Value;
						break;
					case Project.PredefinedLabel.CreationType.Number:
						L.NumericValue = this.Labels.Parse(new TokenisedSource.Token(PL.Value)).NumericValue;
						break;
					case Project.PredefinedLabel.CreationType.Evaluation:
						Label Evaluated = null;
						foreach (TokenisedSource Source in TokenisedSource.FromString(this, PL.Value)) {
							Evaluated = Source.EvaluateExpression(this);
						}
						if (Evaluated.IsString) {
							L.StringValue = Evaluated.StringValue;
						} else {
							L.NumericValue = Evaluated.NumericValue;
						}
						break;
					
				}
				
				L.SetConstant();
			}

		}

		private static string GetFullFilename(Project p, string relativeName) {
			return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(p.ProjectFilename), relativeName));
		}

	}
}
