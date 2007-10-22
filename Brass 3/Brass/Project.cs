using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Brass3 {
	public class Project {

		#region Types

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

		private string outputWriter;
		/// <summary>
		/// Gets or sets the current output writer.
		/// </summary>
		public string OutputWriter {
			get { return this.outputWriter; }
			set { this.outputWriter = value; }
		}

		private readonly List<PluginCollectionInfo> plugins;
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

		private readonly Dictionary<string, string> listingFiles;
		/// <summary>
		/// Gets the dictionary of listing file names to listing file writer plugins.
		/// </summary>
		public Dictionary<string, string> ListingFiles {
			get { return this.listingFiles; }
		}

		#endregion

		#region Constructor
		
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

		public static string GetRelativeFilename(string relativeTo, string filename) {

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

		public void Load(XmlDocument project) {

			this.sourceFile = null;
			this.destinationFile = null;
			this.outputWriter = null;
			this.assembler = null;

			this.plugins.Clear();
			this.listingFiles.Clear();
			


			// Break child elements down:
			Dictionary<string, List<XmlNode>> ChildNodes = new Dictionary<string, List<XmlNode>>();
			foreach (XmlNode N in project.DocumentElement.ChildNodes) {
				List<XmlNode> Destination = null;
				if (!ChildNodes.TryGetValue(N.Name, out Destination)) {
					Destination = new List<XmlNode>();
					ChildNodes.Add(N.Name, Destination);
				}
				Destination.Add(N);
			}

			List<XmlNode> CurrentNodeList;
			XmlAttribute CurrentAttribute;

			// Load plugins:
			this.plugins.Clear();
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

				foreach (XmlNode OutputDirectives in CurrentNodeList[CurrentNodeList.Count-1].ChildNodes) {
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
		}

		public void Load(string filename) {
			this.ProjectFilename = filename;
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.Load(filename);
			this.Load(XmlDoc);
		}

		/// <summary>
		/// Save a project file.
		/// </summary>
		/// <param name="filename">The name of the file to save to.</param>
		public void Save(string filename) {

			this.ProjectFilename = filename;

			using (XmlWriter XW = XmlWriter.Create(filename)) {
				XW.WriteStartElement("brassproj");
				XW.WriteAttributeString("version", "3");
				
				XW.WriteStartElement("plugins");
				foreach (PluginCollectionInfo Collection in this.Plugins) {
					XW.WriteStartElement("collection");
					XW.WriteAttributeString("source", Path.GetFileName(Collection.Source));
					foreach (string Exclusion in Collection.Exclusions) {
						XW.WriteStartElement("exclude");
						XW.WriteAttributeString("name", Exclusion);
						XW.WriteEndElement();
					}
					XW.WriteEndElement();
				}
				XW.WriteEndElement();

				XW.WriteStartElement("input");
				if (!string.IsNullOrEmpty(this.SourceFile)) XW.WriteAttributeString("source", GetRelativeFilename(filename, this.SourceFile));
				if (!string.IsNullOrEmpty(this.Assembler)) XW.WriteAttributeString("assembler", this.Assembler);
				XW.WriteEndElement();

				XW.WriteStartElement("output");
				if (!string.IsNullOrEmpty(this.DestinationFile)) XW.WriteAttributeString("destination", GetRelativeFilename(filename, this.DestinationFile));
				if (!string.IsNullOrEmpty(this.OutputWriter)) XW.WriteAttributeString("writer", this.OutputWriter);

				foreach (KeyValuePair<string, string> List in this.ListingFiles) {
					XW.WriteStartElement("listing");
					XW.WriteAttributeString("destination", List.Key);
					XW.WriteAttributeString("writer", List.Value);
					XW.WriteEndElement();
				}

				XW.WriteEndElement();

				XW.WriteEndElement();
			}			
		}

	}
}
