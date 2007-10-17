using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using Brass3.Utility;
using System.Drawing;

namespace Core.Listing {
	public class VeraDoc : IListingWriter {

		public string Name { get { return "veradoc"; } }

		#region Types

		/// <summary>
		/// Describes the current documentation section.
		/// </summary>
		private enum DocumentationSection {
			/// <summary>
			/// Description of a routine.
			/// </summary>
			Description,
			/// <summary>
			/// Preconditions of a routine.
			/// </summary>
			Precondition,
			/// <summary>
			/// Postconditions of a routine.
			/// </summary>
			Postcondition,
			/// <summary>
			/// Related routines.
			/// </summary>
			SeeAlso,
			/// <summary>
			/// Warning for a routine.
			/// </summary>
			Warning,
			/// <summary>
			/// Example code for a routine.
			/// </summary>
			Example,
		}

		/// <summary>
		/// Defines the documentation for a particular file.
		/// </summary>
		private class DocumentedFile {

			#region Properties

			private readonly string name;
			/// <summary>
			/// Gets the name of the file.
			/// </summary>
			public string Name { get { return this.name; } }

			private readonly Dictionary<string, DocumentedFile> routines;
			/// <summary>
			/// Gets the routines contained within the file.
			/// </summary>
			public Dictionary<string, DocumentedFile> Routines { get { return this.routines; } }

			private readonly StringBuilder description;
			/// <summary>
			/// Gets the description of the file.
			/// </summary>
			public StringBuilder Description { get { return this.description; } }

			private readonly StringBuilder preconditions;
			/// <summary>
			/// Gets the preconditions of the file.
			/// </summary>
			public StringBuilder Preconditions { get { return this.preconditions; } }

			private readonly StringBuilder postconditions;
			/// <summary>
			/// Gets the postconditions of the file.
			/// </summary>
			public StringBuilder Postconditions { get { return this.postconditions; } }

			private readonly StringBuilder seeAlso;
			/// <summary>
			/// Gets the names of related files.
			/// </summary>
			public StringBuilder SeeAlso { get { return this.seeAlso; } }

			private readonly StringBuilder warning;
			/// <summary>
			/// Gets a warning.
			/// </summary>
			public StringBuilder Warning { get { return this.warning; } }

			private readonly StringBuilder example;
			/// <summary>
			/// Gets an example.
			/// </summary>
			public StringBuilder Example { get { return this.example; } }

			#endregion

			#region Constructor
			
			/// <summary>
			/// Creates a new instance of the documented file.
			/// </summary>
			/// <param name="name"></param>
			public DocumentedFile(string name) {

				this.name = name;

				this.routines = new Dictionary<string, DocumentedFile>();

				this.description = new StringBuilder();
				this.preconditions = new StringBuilder();
				this.postconditions = new StringBuilder();
				this.seeAlso = new StringBuilder();
				this.warning = new StringBuilder();
				this.example = new StringBuilder();

			}

			#endregion

			#region Methods

			/// <summary>
			/// Append some documentation to a particular section.
			/// </summary>
			/// <param name="section">The section to append to.</param>
			/// <param name="toAppend">The string to append.</param>
			public void Append(DocumentationSection section, string toAppend) {
				switch (section) {
					case DocumentationSection.Description:
						this.description.AppendLine(toAppend);
						break;
					case DocumentationSection.Precondition:
						this.preconditions.AppendLine(toAppend);
						break;
					case DocumentationSection.Postcondition:
						this.postconditions.AppendLine(toAppend);
						break;
					case DocumentationSection.SeeAlso:
						this.seeAlso.AppendLine(toAppend);
						break;
					case DocumentationSection.Warning:
						this.warning.AppendLine(toAppend);
						break;
					case DocumentationSection.Example:
						this.example.AppendLine(toAppend);
						break;
					default:
						throw new NotImplementedException();
				}
			}

			/// <summary>
			/// Get a formatted value from the source file.
			/// </summary>
			/// <param name="section">Type of the section.</param>
			public string GetValue(DocumentationSection section) {
				switch (section) {
					case DocumentationSection.Example:
						return this.Example.ToString();
					default:
						StringBuilder ToFormat;
						switch (section) {
							case DocumentationSection.Description:
								ToFormat = this.Description;
								break;
							case DocumentationSection.Example:
								ToFormat = this.Example;
								break;
							case DocumentationSection.Postcondition:
								ToFormat = this.Postconditions;
								break;
							case DocumentationSection.Precondition:
								ToFormat = this.Preconditions;
								break;
							case DocumentationSection.SeeAlso:
								ToFormat = this.SeeAlso;
								break;
							case DocumentationSection.Warning:
								ToFormat = this.Warning;
								break;
							default:
								throw new NotImplementedException();
						}
						string[] Components = Array.ConvertAll<string, string>(ToFormat.ToString().Split('\n'), delegate(string s) {
							return s.Trim(); 
						});

						StringBuilder Result = new StringBuilder(ToFormat.Length);

						switch (section) {
							case DocumentationSection.Example:
							case DocumentationSection.Precondition:
							case DocumentationSection.Postcondition:
								Result.Append(ToFormat);
								break;
							default:
								bool DumpedNewline = false;
								for (int i = 0; i < Components.Length; ++i) {
									if (Components[i] == "") {
										if (!DumpedNewline) Result.Append(Environment.NewLine);
										DumpedNewline = true;
									} else {
										Result.Append(Components[i] + " ");
										DumpedNewline = false;
									}
								}
								break;
						}

		

						return section == DocumentationSection.Example ? Result.ToString() : Result.ToString().Trim();
				}
			}

			/// <summary>
			/// Writes this file's attributes to an XML writer.
			/// </summary>
			public void WriteElements(XmlWriter writer) {
				foreach (DocumentationSection Section in Enum.GetValues(typeof(DocumentationSection))) {
					string AttributeText = this.GetValue(Section);
					if (!string.IsNullOrEmpty(AttributeText)) {
						switch (Section) {
							case DocumentationSection.SeeAlso:
								foreach (string s in AttributeText.Split(',')) {
									writer.WriteElementString(Section.ToString().ToLowerInvariant(), s.Trim());
								}
								break;
							case DocumentationSection.Description:
							case DocumentationSection.Warning:
								writer.WriteStartElement(Section.ToString().ToLowerInvariant());
								foreach (string s in AttributeText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)) {
									writer.WriteElementString("p", s.Trim());
								}
								writer.WriteEndElement();
								break;
							case DocumentationSection.Example:
								writer.WriteStartElement("example");
								foreach (string s in AttributeText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)) {
									writer.WriteElementString("line", s.TrimEnd());
								}
								writer.WriteEndElement();
								break;
							case DocumentationSection.Precondition:
							case DocumentationSection.Postcondition:
								foreach (string s in AttributeText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)) {
									writer.WriteElementString(Section.ToString().ToLowerInvariant(), s.Trim());
								}
								break;
							default:
								writer.WriteElementString(Section.ToString().ToLowerInvariant(), AttributeText);
								break;
						}
					}
				}
			}

			#endregion

		}
		
		#endregion

		#region Methods
		
		/// <summary>
		/// Write a VeraDoc listing file to a stream.
		/// </summary>
		/// <param name="compiler">The compiler to get the documentation comments from.</param>
		/// <param name="stream">The stream to write the documentation to.</param>
		public void WriteListing(Compiler compiler, Stream stream) {

			// Keep track of the current section to append comments to.
			DocumentationSection CurrentSection = DocumentationSection.Description;

			// All documented files.
			Dictionary<string, DocumentedFile> DocumentedFiles = new Dictionary<string,DocumentedFile>();

			// The current file to document.
			DocumentedFile CurrentFile = null;

			// The current routine to document.
			DocumentedFile CurrentRoutine = null;

			// Ready to receive a comment after a seperator.
			bool WaitingForSeperatorComment = false;

			// Iterate over all compiled source documentation:
			foreach (Compiler.SourceStatement Statement in compiler.Statements) {

				// Set current file (creating new one if required):
				if (CurrentFile == null || CurrentFile.Name != Statement.SourceFile) {
					if (!DocumentedFiles.TryGetValue(Statement.SourceFile, out CurrentFile)) {
						CurrentFile = new DocumentedFile(Statement.SourceFile);
						DocumentedFiles.Add(Statement.SourceFile, CurrentFile);
					}
				}

				// Get original source.
				TokenisedSource CommentedSource = Statement.Source.OutermostTokenisedSource;
				
				bool HasDocumentationComments = false;
				bool HasSeperatorComments = false;

				// Hunt for the first chunk of comments on a file:
				int DocumentationCommentsIndex;
				for (DocumentationCommentsIndex = 0; DocumentationCommentsIndex < CommentedSource.Tokens.Length; ++DocumentationCommentsIndex) {
					if (CommentedSource.Tokens[DocumentationCommentsIndex].Type == TokenisedSource.Token.TokenTypes.WhiteSpace) continue;
					if (CommentedSource.Tokens[DocumentationCommentsIndex].Type == TokenisedSource.Token.TokenTypes.Comment) {
						TokenisedSource.Token CommentToken = CommentedSource.Tokens[DocumentationCommentsIndex];
						HasDocumentationComments = CommentToken.Data.StartsWith(";;");
						HasSeperatorComments = CommentToken.Data.StartsWith(";") && VeraDoc.IsLine(CommentToken.Data.Substring(1).Trim());
					}
					break;
				}
				
				// What if there was no comment on the source line?
				if (DocumentationCommentsIndex == CommentedSource.Tokens.Length) {
					WaitingForSeperatorComment = false;
					continue;
				}

				// Have we got comments?
				if (WaitingForSeperatorComment) {

					string Comments = CommentedSource.Tokens[DocumentationCommentsIndex].Data.Substring(1).Trim();
					WaitingForSeperatorComment = false;

				} else  if (HasDocumentationComments) {				
					
					
					// Get the comments (and comments ONLY).
					string Comments = CommentedSource.Tokens[DocumentationCommentsIndex].Data.Substring(2).TrimEnd().TrimStart(' ');

					string CheckRoutineHeader = Comments.Trim(); // Do a full trim.
					if (CheckRoutineHeader.StartsWith("=") && CheckRoutineHeader.EndsWith("=")) {
						string RoutineName = CheckRoutineHeader.Trim('=').Trim();
						if (!string.IsNullOrEmpty(RoutineName)) {
							// Set current routine.
							if (!CurrentFile.Routines.TryGetValue(RoutineName.ToLowerInvariant(), out CurrentRoutine)) {
								CurrentRoutine = new DocumentedFile(RoutineName);
								CurrentFile.Routines.Add(RoutineName.ToLowerInvariant(), CurrentRoutine);
							}
							CurrentSection = DocumentationSection.Description;
							continue;
						}
					}

					switch (Comments.ToLowerInvariant()) {
						case "pre:":
							CurrentSection = DocumentationSection.Precondition;
							break;
						case "post:":
							CurrentSection = DocumentationSection.Postcondition;
							break;
						case "seealso:":
							CurrentSection = DocumentationSection.SeeAlso;
							break;
						case "warning:":
							CurrentSection = DocumentationSection.Warning;
							break;
						case "example:":
							CurrentSection = DocumentationSection.Example;
							break;
						default:
							(CurrentRoutine ?? CurrentFile).Append(CurrentSection, Comments);
							break;
					}

				} else if (HasSeperatorComments) {
					WaitingForSeperatorComment = true;
				} else {
					CurrentRoutine = null;
				}
				
			}

			// By this point everything should have been documented...

			using (Zip.ZipFile ZipFile = new Zip.ZipFile(stream)) {

				VeraDoc.WriteResourceToZip(ZipFile, "index.html", Properties.Resources.VeraDoc_Index_HTML.Replace("$(main)", GetXmlFilename(compiler, compiler.SourceFile)));
				VeraDoc.WriteResourceToZip(ZipFile, "veradoc.xsl", Properties.Resources.VeraDoc_VeraDoc_XSL);
				VeraDoc.WriteResourceToZip(ZipFile, "veradoc.css", Properties.Resources.VeraDoc_VeraDoc_CSS);
				VeraDoc.WriteResourceToZip(ZipFile, "icon_file.png", Properties.Resources.VeraDoc_Icon_File_PNG);
				VeraDoc.WriteResourceToZip(ZipFile, "icon_folder.png", Properties.Resources.VeraDoc_Icon_Folder_PNG);
				VeraDoc.WriteResourceToZip(ZipFile, "icon_error.png", Properties.Resources.VeraDoc_Icon_Error_PNG);

				foreach (KeyValuePair<string, DocumentedFile> DocumentedFile in DocumentedFiles) {

					using (XmlWriter VeraDocWriter = XmlWriter.Create(ZipFile.AddFile(GetXmlFilename(compiler, DocumentedFile.Value.Name)).FileData)) {

						VeraDocWriter.WriteProcessingInstruction("xml-stylesheet", @"type=""text/xsl"" href=""veradoc.xsl""");

						VeraDocWriter.WriteStartElement("file"); // Document root.
						VeraDocWriter.WriteElementString("name", Path.GetFileName(DocumentedFile.Value.Name));

						// Write file attributes.
						DocumentedFile.Value.WriteElements(VeraDocWriter);

						// Write routines.
						foreach (KeyValuePair<string, DocumentedFile> Routine in DocumentedFile.Value.Routines) {
							VeraDocWriter.WriteStartElement("routine");
							VeraDocWriter.WriteElementString("name", Routine.Value.Name);
							Routine.Value.WriteElements(VeraDocWriter);
							VeraDocWriter.WriteEndElement();
						}


						VeraDocWriter.WriteEndElement();
						VeraDocWriter.Flush();
					}
				}

				// Tree:
				{
					using (XmlWriter VeraDocWriter = XmlWriter.Create(ZipFile.AddFile("tree.xml").FileData)) {

						VeraDocWriter.WriteProcessingInstruction("xml-stylesheet", @"type=""text/xsl"" href=""veradoc.xsl""");
						VeraDocWriter.WriteStartElement("tree");

						foreach (Compiler.SourceTreeEntry SourceTree in compiler.GetSourceTree(true).Children) {
							WriteTree(SourceTree, VeraDocWriter, null);
						}						

						VeraDocWriter.WriteEndElement();
						VeraDocWriter.Flush();
					}

				}

				ZipFile.Save();
			}
			
		}

		/// <summary>
		/// Write general data to a zip file.
		/// </summary>
		private static void WriteResourceToZip(Zip.ZipFile zip, string filename, byte[] data) {
			zip.AddFile(filename).FileData.Write(data, 0, data.Length);
		}

		/// <summary>
		/// Write string data to a zip file.
		/// </summary>
		private static void WriteResourceToZip(Zip.ZipFile zip, string filename, string data) {
			VeraDoc.WriteResourceToZip(zip, filename, Encoding.UTF8.GetBytes(data));
		}

		/// <summary>
		/// Write image data to a zip file (as a PNG).
		/// </summary>
		private static void WriteResourceToZip(Zip.ZipFile zip, string filename, Image data) {
			using (MemoryStream MS = new MemoryStream()) {
				data.Save(MS, System.Drawing.Imaging.ImageFormat.Png);
				VeraDoc.WriteResourceToZip(zip, filename, MS.ToArray());
			}
		}

		/// <summary>
		/// Gets the filename of an XML document for documentation.
		/// </summary>
		/// <param name="compiler">The compiler providing the file.</param>
		/// <param name="name">The name of the source file.</param>
		private static string GetXmlFilename(Compiler compiler, string name) {
			name = compiler.GetRelativeFilename(name);
			name = name.Replace(Path.DirectorySeparatorChar, '.');
			name = name.Replace(Path.AltDirectorySeparatorChar, '.');
			name += ".xml";
			return name;
		}

		/// <summary>
		/// Write tree.xml
		/// </summary>
		/// <param name="tree">The root of the tree to write.</param>
		/// <param name="writer">The XML writer being used to write the tree.</param>
		private static void WriteTree(Compiler.SourceTreeEntry tree, XmlWriter writer, string baseDir) {
			writer.WriteStartElement(tree.IsDirectory ? "dir" : "treefile");
			writer.WriteElementString("name", tree.Name);
			if (tree.IsDirectory) {
				foreach (Compiler.SourceTreeEntry Subtree in tree.Children) {
					WriteTree(Subtree, writer, tree.Name);
				}				
			} else {
				writer.WriteElementString("file", (baseDir == null ? "" : baseDir + ".") + tree.Name + ".xml");
			}
			writer.WriteEndElement();
		}

		/// <summary>
		/// Returns true if the passed string is a line ("======").
		/// </summary>
		/// <param name="s">The string to test.</param>
		/// <returns>True if the string is a line; false otherwise.</returns>
		private static bool IsLine(string s) {
			if (s.Length == 0) return false;
			foreach (char c in s) if (c != '=') return false;
			return true;
		}

		#endregion

	}
}
