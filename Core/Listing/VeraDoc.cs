using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

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
							return s.Trim() + " "; 
						});
						return string.Join(Environment.NewLine, Components).Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine).Trim();
				}
			}

			/// <summary>
			/// Writes this file's attributes to an XML writer.
			/// </summary>
			public void WriteAttributeString(XmlWriter writer) {
				foreach (DocumentationSection Section in Enum.GetValues(typeof(DocumentationSection))) {
					string AttributeText = this.GetValue(Section);
					if (!string.IsNullOrEmpty(AttributeText)) {
						if (Section ==  DocumentationSection.SeeAlso) {
							foreach (string s in AttributeText.Split(',')) {
								writer.WriteElementString(Section.ToString().ToLowerInvariant(), s.Trim());									
							}
						} else {
							writer.WriteElementString(Section.ToString().ToLowerInvariant(), AttributeText);
						}
					}
				}
			}

			#endregion

		}

		#endregion

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

					Console.WriteLine("Sep: " + Comments);

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
							continue;
						}
					}

					switch (Comments) {
						case "Pre:":
							CurrentSection = DocumentationSection.Precondition;
							break;
						case "Post:":
							CurrentSection = DocumentationSection.Postcondition;
							break;
						case "SeeAlso:":
							CurrentSection = DocumentationSection.SeeAlso;
							break;
						case "Warning:":
							CurrentSection = DocumentationSection.Warning;
							break;
						case "Example:":
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

			// Let's be enterprisey and use XML:
			XmlWriter VeraDocWriter = XmlWriter.Create(stream);

			VeraDocWriter.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"veradoc.xsl\"");
			VeraDocWriter.WriteStartElement("veradoc"); // Document root.
			foreach (KeyValuePair<string, DocumentedFile> DocumentedFile in DocumentedFiles) {
				VeraDocWriter.WriteStartElement("file");
				VeraDocWriter.WriteElementString("source", DocumentedFile.Value.Name);

				// Write file attributes.
				DocumentedFile.Value.WriteAttributeString(VeraDocWriter);

				// Write routines.
				foreach (KeyValuePair<string, DocumentedFile> Routine in DocumentedFile.Value.Routines) {
					VeraDocWriter.WriteStartElement("routine");
					VeraDocWriter.WriteElementString("name", Routine.Value.Name);
					Routine.Value.WriteAttributeString(VeraDocWriter);
					VeraDocWriter.WriteEndElement();
				}


				VeraDocWriter.WriteEndElement();
			}
			VeraDocWriter.WriteEndElement();
			VeraDocWriter.Flush();
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


	}
}
