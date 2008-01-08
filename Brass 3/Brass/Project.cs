using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace BeeDevelopment.Brass3 {

	/// <summary>
	/// Describes a <see cref="Project"/>.
	/// </summary>
	public class Project {

		#region Types

		/// <summary>
		/// Represents information about loading plugin collections.
		/// </summary>
		public class PluginCollectionInfo {
			private string source;
			/// <summary>
			/// Gets or sets the source filename of the plugin collection.
			/// </summary>
			public string Source {
				get { return this.source; }
				set { this.source = value; }
			}

			private readonly List<string> exclusions;
			/// <summary>
			/// Gets the exclusions for the plugin collection.
			/// </summary>
			public List<string> Exclusions {
				get { return this.exclusions; }
			}

			/// <summary>
			/// Creates an instance of the PluginCollectionInfo class.
			/// </summary>
			public PluginCollectionInfo() {
				this.exclusions = new List<string>();
			}

			/// <summary>
			/// Creates an instance of the PluginCollectionInfo class.
			/// </summary>
			public PluginCollectionInfo(string source, string[] exclusions)
				: this() {
				this.source = source;
				this.exclusions.AddRange(exclusions);
			}
		}

		#endregion

		#region Properties

		private string configurationName;
		/// <summary>
		/// Gets or sets a particular build configuration name.
		/// </summary>
		public string ConfigurationName {
			get { return this.configurationName; }
			set { this.configurationName = value; }
		}

		private string configurationDisplayName;
		/// <summary>
		/// Gets or sets a particular build configuration's display name.
		/// </summary>
		public string ConfigurationDisplayName {
			get { return string.IsNullOrEmpty(this.configurationDisplayName) ? this.ConfigurationName : this.configurationDisplayName; }
			set { this.configurationDisplayName = value; }
		}

		private string projectFilename;
		/// <summary>
		/// Gets or sets the filename of the project.
		/// </summary>
		public string ProjectFilename {
			get { return this.projectFilename; }
			set { this.projectFilename = value;}
		}

		private string sourceFile;
		/// <summary>
		/// Gets or sets the source file.
		/// </summary>
		public string SourceFile {
			get { return this.sourceFile;}
			set { this.sourceFile = value; }
		}

		private string destinationFile;
		/// <summary>
		/// Gets or sets the destination file.
		/// </summary>
		public string DestinationFile {
			get { return this.destinationFile; }
			set { this.destinationFile = value; }
		}

		private string stringEncoder;
		/// <summary>
		/// Gets or sets the string encoder.
		/// </summary>
		public string StringEncoder {
			get { return this.stringEncoder; }
			set { this.stringEncoder = value; }
		}

		private string outputWriter;
		/// <summary>
		/// Gets or sets the current output writer.
		/// </summary>
		public string OutputWriter {
			get { return this.outputWriter; }
			set { this.outputWriter = value; }
		}

		private List<PluginCollectionInfo> plugins;
		/// <summary>
		/// Gets the list of plugins referenced by the project.
		/// </summary>
		public List<PluginCollectionInfo> Plugins {
			get { return this.plugins; }
		}

		private string assembler;
		/// <summary>
		/// Gets or sets the name of the assembler.
		/// </summary>
		public string Assembler {
			get { return this.assembler; }
			set { this.assembler = value; }
		}

		private Dictionary<string, string> listingFiles;
		/// <summary>
		/// Gets the dictionary of listing file names to listing file writer plugins.
		/// </summary>
		public Dictionary<string, string> ListingFiles {
			get { return this.listingFiles; }
		}

		private string header;
		/// <summary>
		/// Gets or sets an assembly snippet header.
		/// </summary>
		public string Header {
			get { return this.header; }
			set { this.header = value; }
		}

		private bool appendHeader;
		/// <summary>
		/// Gets or sets whether to append a header.
		/// </summary>
		public bool AppendHeader {
			get { return this.appendHeader; }
			set { this.appendHeader = value; }
		}

		private string footer;
		/// <summary>
		/// Gets or sets an assembly snippet footer.
		/// </summary>
		public string Footer {
			get { return this.footer; }
			set { this.footer = value; }
		}

		private bool appendFooter;
		/// <summary>
		/// Gets or sets whether to append a footer.
		/// </summary>
		public bool AppendFooter {
			get { return this.appendFooter; }
			set { this.appendFooter = value; }
		}

		private Dictionary<string, Project> buildConfigurations = new Dictionary<string, Project>();
		/// <summary>
		/// Gets a dictionary describing the build configurations within the current project.
		/// </summary>
		public Dictionary<string, Project> BuildConfigurations {
			get { return this.buildConfigurations; }
		}

		/// <summary>
		/// Describes a predefined label.
		/// </summary>
		public struct PredefinedLabel {

			/// <summary>
			/// Gets or sets the name of the predefined label to create.
			/// </summary>
			public string Name;
			
			/// <summary>
			/// Defines the type of the label to create.
			/// </summary>
			public enum CreationType {
				/// <summary>
				/// The predefined label is a string.
				/// </summary>
				String,
				/// <summary>
				/// The predefined label is a number.
				/// </summary>
				Number,
				/// <summary>
				/// The predefined label is the result of an evaluation.
				/// </summary>
				Evaluation
			}

			/// <summary>
			/// Gets or sets the type of the label to create. 
			/// </summary>
			public CreationType Type;

			/// <summary>
			/// Gets or sets the string representation of the label.
			/// </summary>
			public string Value;

			/// <summary>
			/// Creates an instance of a <see cref="PredefinedLabel"/>.
			/// </summary>
			/// <param name="name">The name of the label to create.</param>
			/// <param name="type">The type of the label to create.</param>
			/// <param name="value">The string representation of the label's value.</param>
			public PredefinedLabel(string name, CreationType type, string value) {
				this.Name = name;
				this.Type = type;
				this.Value = value;
			}
		}

		private Dictionary<string, PredefinedLabel> PredefinedLabels = new Dictionary<string,PredefinedLabel>();

		/// <summary>
		/// Gets labels predefined in the project file.
		/// </summary>
		public PredefinedLabel[] Labels {
			get {
				return new List<PredefinedLabel>(this.PredefinedLabels.Values).ToArray();
			}
		}

		#endregion

		#region Constructor
		
		/// <summary>
		/// Creates a new instance of the <see cref="Project"/> class.
		/// </summary>
		public Project() {
			this.plugins = new List<PluginCollectionInfo>();
			this.listingFiles = new Dictionary<string, string>();
		}

		#endregion

		static string CorrectFilename(string name) {

			name = Path.GetFullPath(name);
			Stack<string> PathComponents = new Stack<string>();

			string Root = Path.GetPathRoot(name);

			while (name != Root) {
				PathComponents.Push(Path.GetFileName(name));
				name = Path.GetDirectoryName(name);
			}

			string WorkingPath = Root;

			while (PathComponents.Count > 0) {
				string TestDirectory = PathComponents.Pop();
				string[] s = Directory.Exists(WorkingPath) ? Directory.GetFileSystemEntries(WorkingPath, TestDirectory) : new string[] { };
				if (s.Length == 1) {
					WorkingPath = s[0];
				} else {
					WorkingPath += Path.DirectorySeparatorChar + TestDirectory;
				}
			}

			return char.ToUpperInvariant(WorkingPath[0]) + WorkingPath.Substring(1);
		}

		/// <summary>
		/// Gets the relative path between two files.
		/// </summary>
		/// <param name="relativeTo">The path to compare the second one to.</param>
		/// <param name="filename">The filename to find the relative path to.</param>
		/// <returns>The relative path between the two files.</returns>
		public static string GetRelativePath(string relativeTo, string filename) {

			string a = CorrectFilename(relativeTo);
			string b = CorrectFilename(filename);
			filename = b;

			// If the path roots are different, we can't have a relative file at all!
			if (Path.GetPathRoot(a) != Path.GetPathRoot(b)) {
				return b;
			}

			string Root = Path.GetPathRoot(a);

			List<string> PathA = new List<string>();
			while (a != Root) {
				PathA.Add(Path.GetFileName(a));
				a = Path.GetDirectoryName(a);
			}
			PathA.RemoveAt(0);
			PathA.Reverse();

			List<string> PathB = new List<string>();
			while (b != Root) {
				PathB.Add(Path.GetFileName(b));
				b = Path.GetDirectoryName(b);
			}
			PathB.RemoveAt(0);
			PathB.Reverse();


			while (PathA.Count > 0 && PathB.Count > 0 && PathA[0] == PathB[0]) {
				PathA.RemoveAt(0);
				PathB.RemoveAt(0);
			}

			StringBuilder FinalPath = new StringBuilder(64);

			for (int i = 0; i < PathA.Count; ++i) {
				FinalPath.Append("..");
				FinalPath.Append(Path.DirectorySeparatorChar);
			}
			//FinalPath.Append(string.Join(Path.DirectorySeparatorChar.ToString(), PathA.ToArray()));
			//if (FinalPath.Length > 0) FinalPath.Append(Path.DirectorySeparatorChar);
			FinalPath.Append(string.Join(Path.DirectorySeparatorChar.ToString(), PathB.ToArray()));

			return Path.Combine(FinalPath.ToString(), Path.GetFileName(filename));
		}


		private static bool TryGetNamedItem(XmlNode node, string name, out XmlAttribute attribute) {
			foreach (XmlAttribute A in node.Attributes) {
				if (A.Name == name) {
					attribute = A;
					return true;
				}
			}
			attribute = default(XmlAttribute);
			return false;
		}

		/// <summary>
		/// Load a project from an <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="project">The <see cref="XmlDocument"/> to load the project from.</param>
		public void Load(XmlDocument project) {
			this.Load(project, true);
		}

		private void Load(XmlDocument project, bool newProject) {

			if (newProject) {

				this.sourceFile = null;
				this.destinationFile = null;
				this.outputWriter = null;
				this.assembler = null;

				this.plugins.Clear();
				this.listingFiles.Clear();

				this.header = null;
				this.footer = null;

				this.PredefinedLabels.Clear();


			}
			this.LoadFromNode(project.DocumentElement);
		}

		private void LoadFromNode(XmlNode rootNode) {

			// Break child elements down:
			Dictionary<string, List<XmlNode>> ChildNodes = new Dictionary<string, List<XmlNode>>();

			foreach (XmlNode N in rootNode.ChildNodes) {
				List<XmlNode> Destination = null;
				if (!ChildNodes.TryGetValue(N.Name, out Destination)) {
					Destination = new List<XmlNode>();
					ChildNodes.Add(N.Name, Destination);
				}
				Destination.Add(N);
			}

			List<XmlNode> CurrentNodeList;
			XmlAttribute CurrentAttribute;

			// Do we have a template?
			if (ChildNodes.TryGetValue("template", out CurrentNodeList)) {
				foreach (XmlNode N in CurrentNodeList) {
					if (TryGetNamedItem(N, "source", out CurrentAttribute)) {
							string TemplateDirectory = Environment.GetEnvironmentVariable("Brass.Templates");

							bool LoadedTemplate = false;

							string TemplateName;
							if (!string.IsNullOrEmpty(this.projectFilename)) {
								TemplateName = Path.Combine(this.projectFilename, CurrentAttribute.Value);
								if (File.Exists(TemplateName)) {
									XmlDocument Template = new XmlDocument();
									Template.Load(TemplateName);
									this.Load(Template, false);
									LoadedTemplate = true;
								}
							}

							if (!LoadedTemplate && !string.IsNullOrEmpty(TemplateDirectory)) {
								TemplateName = Path.Combine(TemplateDirectory, CurrentAttribute.Value);
								if (File.Exists(TemplateName)) {
									XmlDocument Template = new XmlDocument();
									Template.Load(TemplateName);
									this.Load(Template, false);
									LoadedTemplate = true;
								}
							}

							if (!LoadedTemplate) {
								throw new FileNotFoundException(string.Format(Strings.ErrorProjectTemplateNotFound, CurrentAttribute.Value) + (string.IsNullOrEmpty(TemplateDirectory) ? (" " + Strings.ErrorEnvironmentNotSetTemplates) : ""));
							}
					}
				}
			}

			// Load plugins:
			if (ChildNodes.TryGetValue("plugins", out CurrentNodeList)) {
				foreach (XmlNode N in CurrentNodeList) {
					foreach (XmlNode PluginCollection in N.ChildNodes) {
						if (PluginCollection.Name != "collection") continue;
						if (TryGetNamedItem(PluginCollection, "source", out CurrentAttribute)) {
							List<string> Exclusions = new List<string>();
							foreach (XmlNode Exclusion in PluginCollection.ChildNodes) {
								if (Exclusion.Name == "exclude") {
									XmlAttribute ExcludeName;
									if (TryGetNamedItem(Exclusion, "name", out ExcludeName)) {
										Exclusions.Add(ExcludeName.Value);
									}
								}
							}
							this.plugins.Add(new PluginCollectionInfo(CurrentAttribute.Value, Exclusions.ToArray()));
						}
					}
				}
			}

			// Handle input:			
			if (ChildNodes.TryGetValue("input", out CurrentNodeList)) {
				// Get source:
				if (TryGetNamedItem(CurrentNodeList[CurrentNodeList.Count - 1], "source", out CurrentAttribute)) this.sourceFile = CurrentAttribute.Value;
				if (TryGetNamedItem(CurrentNodeList[CurrentNodeList.Count - 1], "assembler", out CurrentAttribute)) this.assembler = CurrentAttribute.Value;
				if (TryGetNamedItem(CurrentNodeList[CurrentNodeList.Count - 1], "stringencoder", out CurrentAttribute)) this.stringEncoder = CurrentAttribute.Value;

				foreach (XmlNode GetHeaderFooter in CurrentNodeList[CurrentNodeList.Count - 1].ChildNodes) {
					if (GetHeaderFooter.Name == "header") {
						this.appendHeader = TryGetNamedItem(GetHeaderFooter, "append", out CurrentAttribute) && CurrentAttribute.Value.ToLowerInvariant() == "true";
						this.header = GetHeaderFooter.InnerText;
					} else if (GetHeaderFooter.Name == "footer") {
						this.appendFooter = TryGetNamedItem(GetHeaderFooter, "append", out CurrentAttribute) && CurrentAttribute.Value.ToLowerInvariant() == "true";
						this.footer = GetHeaderFooter.InnerText;
					} else if (GetHeaderFooter.Name == "label") {
						if (TryGetNamedItem(GetHeaderFooter, "name", out CurrentAttribute)) {
							string Name = CurrentAttribute.Value;
							if (TryGetNamedItem(GetHeaderFooter, "value", out CurrentAttribute)) {
								string Value = CurrentAttribute.Value;
								PredefinedLabel.CreationType CreationType = PredefinedLabel.CreationType.Number;
								if (TryGetNamedItem(GetHeaderFooter, "type", out CurrentAttribute)) {
									switch (CurrentAttribute.Value.ToLowerInvariant()) {
										case "string":
											CreationType = PredefinedLabel.CreationType.String;
											break;
										case "eval":
											CreationType = PredefinedLabel.CreationType.Evaluation;
											break;
									}
								}
								string HashName = Name.ToLowerInvariant();
								if (this.PredefinedLabels.ContainsKey(HashName)) this.PredefinedLabels.Remove(HashName);
								this.PredefinedLabels.Add(HashName, new PredefinedLabel(Name, CreationType, Value));
							}
						}
					}
				}
			}

			// Handle output:			
			if (ChildNodes.TryGetValue("output", out CurrentNodeList)) {

				// Get destination:
				if (TryGetNamedItem(CurrentNodeList[CurrentNodeList.Count - 1], "destination", out CurrentAttribute)) {
					this.destinationFile = CurrentAttribute.Value;
				}

				// Get output writer:
				if (TryGetNamedItem(CurrentNodeList[CurrentNodeList.Count - 1], "writer", out CurrentAttribute)) {
					this.outputWriter = CurrentAttribute.Value;
				}

				foreach (XmlNode OutputDirectives in CurrentNodeList[CurrentNodeList.Count - 1].ChildNodes) {
					switch (OutputDirectives.Name) {
						case "listing":
							XmlAttribute ListingDestination, ListingWriter;
							if (TryGetNamedItem(OutputDirectives, "destination", out ListingDestination) && TryGetNamedItem(OutputDirectives, "writer", out ListingWriter)) {
								this.listingFiles.Add(ListingDestination.Value, ListingWriter.Value);
							}
							break;
					}

				}
			}

			// Build configuration fun:
			foreach (XmlNode N in rootNode.ChildNodes) {
				if (N.Name == "buildconfiguration") {
					XmlAttribute ConfigurationAttribute;
					if (TryGetNamedItem(N, "name", out ConfigurationAttribute)) {

						Project P = new Project();
						P.ConfigurationName = ConfigurationAttribute.Value;

						XmlAttribute ConfigurationDisplayName;
						if (TryGetNamedItem(N, "displayname", out ConfigurationDisplayName)) P.ConfigurationDisplayName = ConfigurationDisplayName.Value;

						this.BuildConfigurations.Add(ConfigurationAttribute.Value.ToLowerInvariant(), P);
						P.LoadFromNode(N);

						/*if (ConfigurationAttribute.Value.ToLowerInvariant() == CurrentConfiguration) {
							this.LoadFromNode(N, configuration, configurationIndex + 1);
						}*/
					}
				}
			}

		}


		/// <summary>
		/// Merge two project files.
		/// </summary>
		/// <param name="master">The master project.</param>
		/// <param name="merger">The project to merge on top of the master.</param>
		/// <returns>A merged project.</returns>
		public static Project Merge(Project master, Project merger) {

			Project Result = new Project();
			Result.ConfigurationName = Project.Merge(master.ConfigurationName, merger.ConfigurationName, true, ".");
			Result.ProjectFilename = master.ProjectFilename;

			Result.SourceFile = Project.Merge(master.SourceFile, merger.SourceFile);
			Result.DestinationFile = Project.Merge(master.DestinationFile, merger.DestinationFile);
			Result.StringEncoder = Project.Merge(master.StringEncoder, merger.StringEncoder);
			Result.OutputWriter = Project.Merge(master.OutputWriter, merger.OutputWriter);
			Result.Assembler = Project.Merge(master.Assembler, merger.Assembler);
			Result.Header = Project.Merge(master.Header, merger.Header, merger.AppendHeader, Environment.NewLine);
			Result.Footer = Project.Merge(master.Footer, merger.Footer, merger.AppendHeader, Environment.NewLine);

			Result.plugins = Project.Merge(master.Plugins, merger.Plugins);

			Result.listingFiles = Project.Merge(master.ListingFiles, merger.ListingFiles);
			
			Result.PredefinedLabels = Project.Merge(master.PredefinedLabels, merger.PredefinedLabels);

			return Result;

		}

		private static string Merge(string master, string merger) {
			return (string.IsNullOrEmpty(merger)) ? master : merger;
		}
		private static string Merge(string master, string merger, bool append, string joinSequence) {
			if (append) {
				if (string.IsNullOrEmpty(master)) {
					return merger;
				} else if (string.IsNullOrEmpty(merger)) {
					return master;
				} else {
					return master + joinSequence + merger;
				}
			} else {
				return Merge(master, merger);
			}
		}

		private static T[] Merge<T>(T[] master, T[] merger) {
			List<T> A = new List<T>(master);
			foreach (T B in merger) if (!A.Contains(B)) A.Add(B);
			return A.ToArray();
		}

		private static List<T> Merge<T>(List<T> master, List<T> merger) {
			List<T> A = new List<T>(master.ToArray());
			foreach (T B in merger) if (!A.Contains(B)) A.Add(B);
			return A;
		}

		private static Dictionary<TKey, TValue> Merge<TKey, TValue>(Dictionary<TKey, TValue> master, Dictionary<TKey, TValue> merger) {
			Dictionary<TKey, TValue> Result = new Dictionary<TKey, TValue>();
			foreach (KeyValuePair<TKey, TValue> B in merger) Result.Add(B.Key, B.Value);
			foreach (KeyValuePair<TKey, TValue> A in master) if (!Result.ContainsKey(A.Key)) Result.Add(A.Key, A.Value);			
			return Result;
		}

		/// <summary>
		/// Gets a project instance derived from a particular build configuration.
		/// </summary>
		/// <param name="configuration">The name of the build configuration to use.</param>
		public Project GetBuildConfiguration(string configuration) {
			Project Result = this;
			Project CurrentConfiguration = this;
			foreach (string Component in configuration.Split('.')) {
				string Hash = Component.ToLowerInvariant();
				if (!CurrentConfiguration.BuildConfigurations.ContainsKey(Hash)) throw new InvalidOperationException(string.Format(Strings.ErrorProjectInvalidBuildConfigurationComponent, Component));
				Result = Merge(Result, CurrentConfiguration.BuildConfigurations[Hash]);
				CurrentConfiguration = CurrentConfiguration.BuildConfigurations[Hash];
			}
			return Result;
		}

		/// <summary>
		/// Gets an array of configuration names.
		/// </summary>
		/// <returns>An array of elements whose key is the configuration name and whose value is the display name.</returns>
		public KeyValuePair<string, string>[] GetBuildConfigurationNames() {
			List<KeyValuePair<string, string>> Result = new List<KeyValuePair<string, string>>();
			AddBuildConfigurations(null, null, Result, this);
			return Result.ToArray();
		}

		private void AddBuildConfigurations(string path, string displayPath, List<KeyValuePair<string, string>> result, Project p) {
			foreach (KeyValuePair<string, Project> BuildConfiguration in p.BuildConfigurations) {
				string SubPath = (path == null ? "" : path + ".") + BuildConfiguration.Value.ConfigurationName;
				string SubDisplayPath = (displayPath == null ? "" : displayPath + ", ") + BuildConfiguration.Value.ConfigurationDisplayName;
				result.Add(new KeyValuePair<string, string>(SubPath, SubDisplayPath));
				AddBuildConfigurations(SubPath, SubDisplayPath, result, BuildConfiguration.Value);
			}
		}

		/// <summary>
		/// Load a project from a file.
		/// </summary>
		/// <param name="filename">The name of the file to load the project from.</param>
		public void Load(string filename) {
			this.ProjectFilename = filename;
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.Load(filename);
			this.Load(XmlDoc);
		}

	}
}
