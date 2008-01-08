using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Zip;

namespace Core.Listing {

	[Category("Documentation")]
	[Description("Generates XML from compiled Vera documentation comments.")]
	[Remarks(
@"This plugin outputs a Zip archive containing the XML documents and associated stylesheets and images to display them.
The current specification for the documentation comments can be viewed <a href=""http://timendus.student.utwente.nl/~vera/wiki/index.php/Coding_standards\"">here</a>.
The following tags are supported by this plugin:
<table>
	<tr>
		<th><c>Pre</c></th>
		<td>Specify the preconditions (input state) for the routine, one per line.</td>
	</tr>
	<tr>
		<th><c>Post</c></th>
		<td>Specify the postconditions (output state) for the routine, one per line.</td>
	</tr>
	<tr>
		<th><c>Warning</c></th>
		<td>Include any special warnings governing the use of the routine to the user here.</td>
	</tr>
	<tr>
		<th><c>SeeAlso</c></th>
		<td>List similar routines that you would like to draw the reader's attention to (comma-delimited list).</td>
	</tr>
	<tr>
		<th><c>Example</c></th>
		<td>A sample block of code making use of the routine. Indent with tabs; formatting is preserved.</td>
	</tr>
	<tr>
		<th><c>Authors</c></th>
		<td>The author(s) of the routine, one per line. Inclusion of a name and an email address is recommended.</td>
	</tr>
</table>")]

	[CodeExample("Sample taken from the Vera project.",
@";; Vera - the calc lover's OS,
;; copyright (C) 2007 The Vera Development Team.
;; 
;; This is a test assembly file to test the reference implementation
;; asmdoc tool.

; ===================
; We'll put in a fake console routine here

;; === console.printline ===
;;
;; Print a null terminated string to the display
;; using some crappy display port writes
;;
;; Don't use it too often, it'll fry your screen :-)
;;
;; Pre:
;;   hl = pointer to string
;;
;; Post:
;;   String displayed on screen
;;   hl,bc,a destroyed
;;
;; SeeAlso:
;;   console.putchar, console.set_cursor_x, console.set_cursor_y
;;
;; Warning:
;;   Do not call this function if you have manually
;;   set console_cursor_x or console_cursor_y outside
;;   the console boundaries.
;;
;; Example:
;;    	ld hl,str_welcome
;;    	call console.printline
;;    	ret
;;   str_welcome:
;;    	.db ""Welcome to Vera"",0

console.printline:
	ret

;; === console.newline ===
;;
;; Print a newline to the console
;;
;; Post:
;;   Updated cursor position, scrolled screen if necessary
;;   hl,a destroyed
;;
;; SeeAlso:
;;   console.printline, console.set_cursor_x, console.set_cursor_y
;;
;; Example:
;;    	call console.newline

console.newline:
	ret

; === console.putchar ===
;
; Internally used routine, just one semicolon
;
; Pre:
;   a = character
;
; Post:
;   Character put on display
;   hl,a destroyed
;
; SeeAlso:
;   console.printline
;
; Example:
;   	ld a,'F'
;    	call console.putchar

console.putchar:
	ret

; ===================
; End of file
; ===================")]

	public class VeraDoc : IListingWriter {

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
			/// <summary>
			/// Routine authors.
			/// </summary>
			Authors,
		}

		/// <summary>
		/// Describes the way that documentation is formatted.
		/// </summary>
		private enum SectionFormatting {
			/// <summary>
			/// No formatting.
			/// </summary>
			None,
			/// <summary>
			/// Has an item per line.
			/// </summary>
			OneItemPerLine,
			/// <summary>
			/// Sections are made up of paragraphs.
			/// </summary>
			Paragraphs,
		}

	

		/// <summary>
		/// Defines the documentation for a particular file.
		/// </summary>
		private class DocumentedFile {

			#region Properties

			private Dictionary<DocumentationSection, StringBuilder> Sections;

			private readonly string name;
			/// <summary>
			/// Gets the name of this file or routine.
			/// </summary>
			public string Name { get { return this.name; } }

			private readonly Dictionary<string, DocumentedFile> routines;
			/// <summary>
			/// Gets the routines in this file.
			/// </summary>
			public Dictionary<string, DocumentedFile> Routines { get { return this.routines; } }

			private readonly Compiler.SourceStatement statement;
			/// <summary>
			/// Gets the source statement that declared this item.
			/// </summary>
			public Compiler.SourceStatement Statement {
				get { return this.statement; }
			}

			private Label label;
			/// <summary>
			/// Gets or sets the label that this routine refers to.
			/// </summary>
			public Label Label {
				get { return this.label; }
				set { this.label = value; }
			}

			#endregion

			#region Constructor
			
			/// <summary>
			/// Creates a new instance of the documented file.
			/// </summary>
			/// <param name="name"></param>
			public DocumentedFile(string name, Compiler.SourceStatement statement) {
				this.name = name;
				this.routines = new Dictionary<string, DocumentedFile>();
				this.Sections = new Dictionary<DocumentationSection, StringBuilder>();
				this.statement = statement;
				foreach (DocumentationSection Section in Enum.GetValues(typeof(DocumentationSection))) {
					Sections.Add(Section, new StringBuilder(32));
				}
			}

			#endregion

			#region Methods

			/// <summary>
			/// Append some documentation to a particular section.
			/// </summary>
			/// <param name="section">The section to append to.</param>
			/// <param name="toAppend">The string to append.</param>
			public void Append(DocumentationSection section, string toAppend) {
				this.Sections[section].AppendLine(toAppend);
			}

			/// <summary>
			/// Get a formatted value from the source file.
			/// </summary>
			/// <param name="section">Type of the section.</param>
			public string[] GetValue(DocumentationSection section) {

				// Easy...
				if (GetSectionFormatting(section) == SectionFormatting.None) return new string[] { this.Sections[section].ToString() };

				// Grab components (one per line);
				string[] Components = Array.ConvertAll<string, string>(this.Sections[section].ToString().Split('\n'), delegate(string s) {
					return s.Trim();
				});

				if (GetSectionFormatting(section) == SectionFormatting.Paragraphs) {

					List<StringBuilder> Result = new List<StringBuilder>(Components.Length);

					Result.Add(new StringBuilder());

					bool DumpedNewline = false;
					for (int i = 0; i < Components.Length; ++i) {
						if (Components[i] == "") {
							if (!DumpedNewline) Result.Add(new StringBuilder());
							DumpedNewline = true;
						} else {
							Result[Result.Count - 1].Append(Components[i] + " ");
							DumpedNewline = false;
						}
					}

					Components = Array.ConvertAll<StringBuilder, string>(Result.ToArray(), delegate(StringBuilder s) { return s.ToString(); });

				}


				return Array.FindAll<string>(Components, delegate(string s) { return !string.IsNullOrEmpty(s); });

			}

			/// <summary>
			/// Writes this file's attributes to an XML writer.
			/// </summary>
			public void WriteElements(Compiler compiler, XmlWriter writer) {

				if (this.Label != null) {
					writer.WriteStartElement("label");
					writer.WriteElementString("value", Label.NumericValue.ToString(CultureInfo.InvariantCulture));
					writer.WriteElementString("page", Label.Page.ToString(CultureInfo.InvariantCulture));
					writer.WriteElementString("size", Label.Size.ToString(CultureInfo.InvariantCulture));
					writer.WriteEndElement();
				}

				foreach (DocumentationSection Section in Enum.GetValues(typeof(DocumentationSection))) {
					string[] AttributeText = this.GetValue(Section);

					if (AttributeText.Length == 0 || !Array.TrueForAll(AttributeText, delegate(string s) { return !string.IsNullOrEmpty(s.Trim()); })) {
						if (Section == DocumentationSection.Description) {
							string Error = string.Format("'{0}' is missing a description.", this.Statement == null ? compiler.GetRelativeFilename(this.name) : this.name);
							if (this.Statement != null) {
								compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, Error, this.Statement));
							} else {
								compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, Error, this.name, 0));
							}
						}
						continue;
					}

					switch (GetSectionFormatting(Section)) {
						case SectionFormatting.None:
							writer.WriteStartElement(Section.ToString().ToLowerInvariant());
							foreach (string Attribute in AttributeText) {
								foreach (string Line in Attribute.Split('\n')) {
									string Trimmed = Line.TrimEnd();
									if (string.IsNullOrEmpty(Trimmed)) continue;
									writer.WriteElementString("line", Line.TrimEnd());					
								}
							}
							writer.WriteEndElement();
							break;

						case SectionFormatting.OneItemPerLine:
							foreach (string Attribute in AttributeText) {
								writer.WriteElementString(Section.ToString().ToLowerInvariant(), Attribute.Trim());
							}
							break;

						case SectionFormatting.Paragraphs:
							writer.WriteStartElement(Section.ToString().ToLowerInvariant());
							foreach (string Attribute in AttributeText) {
								writer.WriteElementString("p", Attribute.Trim());
							}
							writer.WriteEndElement();
							break;

						default:
							throw new NotImplementedException();
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

			// All documented files.
			Dictionary<string, DocumentedFile> DocumentedFiles = new Dictionary<string, DocumentedFile>();

			// Iterate over all compiled source documentation:
			foreach (KeyValuePair<string, Compiler.SourceStatement[]> Statements in compiler.GetSourceStatementsByFile(true)) {

				// The current file to document.
				DocumentedFile CurrentFile = new DocumentedFile(Statements.Key, null);
				DocumentedFiles.Add(Statements.Key, CurrentFile);

				// The current routine to document.
				DocumentedFile CurrentRoutine = null;

				// Ready to receive a comment after a seperator.
				bool WaitingForSeperatorComment = false;


				// Keep track of the current section to append comments to.
				DocumentationSection CurrentSection = DocumentationSection.Description;

				// Have we hit the end-of-file notification chappy?
				bool HitEndOfFile = false;

				foreach (Compiler.SourceStatement Statement in Statements.Value) {

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

					bool IsWhitespace = Array.TrueForAll<TokenisedSource.Token>(CommentedSource.Tokens, delegate(TokenisedSource.Token t) {
						return t.Type == TokenisedSource.Token.TokenTypes.WhiteSpace || t.Type == TokenisedSource.Token.TokenTypes.Seperator;
					});

					// What if there was no comment on the source line?
					if (DocumentationCommentsIndex == CommentedSource.Tokens.Length) {
						WaitingForSeperatorComment = false;
						continue;
					}

					if (CommentedSource.Tokens.Length > 0 && HitEndOfFile && !(IsWhitespace || HasSeperatorComments)) {
						compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, Strings.ErrorVeraDocFileContentsPastEndOfFile, Statement));
					}

					// Have we got comments?
					if (WaitingForSeperatorComment) {

						string Comments = CommentedSource.Tokens[DocumentationCommentsIndex].Data.Substring(1).Trim();

						if (Comments.ToLowerInvariant() == "end of file") {
							HitEndOfFile = true;
						}

						WaitingForSeperatorComment = false;

					} else if (HasDocumentationComments) {


						// Get the comments (and comments ONLY).
						string Comments = CommentedSource.Tokens[DocumentationCommentsIndex].Data.Substring(2).TrimEnd().TrimStart(' ');

						string CheckRoutineHeader = Comments.Trim(); // Do a full trim.
						if (CheckRoutineHeader.StartsWith("=") && CheckRoutineHeader.EndsWith("=")) {
							string RoutineName = CheckRoutineHeader.Trim('=').Trim();

							if (!string.IsNullOrEmpty(RoutineName)) {

								// Set current routine.
								if (!CurrentFile.Routines.TryGetValue(RoutineName.ToLowerInvariant(), out CurrentRoutine)) {
									CurrentRoutine = new DocumentedFile(RoutineName, Statement);
									CurrentFile.Routines.Add(RoutineName.ToLowerInvariant(), CurrentRoutine);
								}

								// Check routine name validity:
								string Error;
								if (!ValidRoutineName(RoutineName, out Error)) {
									compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, Error, Statement));
								}


								Label CheckMatchingLabel;
								if (!compiler.Labels.TryParse(new TokenisedSource.Token(RoutineName), out CheckMatchingLabel)) {
									Error = string.Format(Strings.ErrorVeraDocDocumentedInvisibleRoutine, RoutineName);
									compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, Error, Statement));
								} else {
									CurrentRoutine.Label = CheckMatchingLabel;
									if (CheckMatchingLabel.Name != RoutineName) {
										Error = string.Format(Strings.ErrorVeraDocDocumentedRoutineSpellingIncorrect, RoutineName, CheckMatchingLabel.Name);
										compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, Error, Statement));
									}
								}

								CurrentSection = DocumentationSection.Description;
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
							case "Authors:":
								CurrentSection = DocumentationSection.Authors;
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

				// Check end of file...
				if (!HitEndOfFile) {
					compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, Strings.ErrorVeraDocNoEndOfFile, Statements.Value[Statements.Value.Length - 1]));
				}

			}


			// By this point everything should have been documented...

			XmlWriterSettings XmlSettings = new XmlWriterSettings();
			XmlSettings.Indent = true;
			XmlSettings.NewLineHandling = NewLineHandling.Entitize;

			var OutputZip = new ZipFile();

			VeraDoc.WriteResourceToZip(OutputZip, "index.html", Properties.Resources.VeraDoc_Index_HTML.Replace("$(main)", GetXmlFilename(compiler, compiler.SourceFile)));
			VeraDoc.WriteResourceToZip(OutputZip, "veradoc.xsl", Properties.Resources.VeraDoc_VeraDoc_XSL);
			VeraDoc.WriteResourceToZip(OutputZip, "veradoc.css", Properties.Resources.VeraDoc_VeraDoc_CSS);
			VeraDoc.WriteResourceToZip(OutputZip, "icon_file.png", Properties.Resources.VeraDoc_Icon_File_PNG);
			VeraDoc.WriteResourceToZip(OutputZip, "icon_folder.png", Properties.Resources.VeraDoc_Icon_Folder_PNG);
			VeraDoc.WriteResourceToZip(OutputZip, "icon_error.png", Properties.Resources.VeraDoc_Icon_Error_PNG);

			foreach (KeyValuePair<string, DocumentedFile> DocumentedFile in DocumentedFiles) {

				string XmlWriterPath = GetXmlFilename(compiler, DocumentedFile.Value.Name);
				using (var DocWriterStream = new MemoryStream(1024)) {
					using (XmlWriter VeraDocWriter = XmlWriter.Create(DocWriterStream, XmlSettings)) {

						int CssDepth = GetPathDepth(XmlWriterPath);
						StringBuilder CssPath = new StringBuilder(32);
						for (int i = 0; i < CssDepth; ++i) CssPath.Append("../");

						VeraDocWriter.WriteProcessingInstruction("xml-stylesheet", @"type=""text/xsl"" href=""" + CssPath.ToString() + @"veradoc.xsl""");

						VeraDocWriter.WriteStartElement("file"); // Document root.
						VeraDocWriter.WriteElementString("name", Path.GetFileName(DocumentedFile.Value.Name));

						// Write file attributes.
						DocumentedFile.Value.WriteElements(compiler, VeraDocWriter);

						// Write routines.
						foreach (KeyValuePair<string, DocumentedFile> Routine in DocumentedFile.Value.Routines) {
							VeraDocWriter.WriteStartElement("routine");
							VeraDocWriter.WriteElementString("name", Routine.Value.Name);
							Routine.Value.WriteElements(compiler, VeraDocWriter);
							VeraDocWriter.WriteEndElement();
						}


						VeraDocWriter.WriteEndElement();
						VeraDocWriter.Flush();

						OutputZip.Add(new ZipFileEntry() {
							Name = XmlWriterPath,
							Data = DocWriterStream.ToArray()
						});
					}
				}
			}

			// Tree:
			{
				using (var DocWriterStream = new MemoryStream(1024)) {
					using (XmlWriter VeraDocWriter = XmlWriter.Create(DocWriterStream, XmlSettings)) {

						VeraDocWriter.WriteProcessingInstruction("xml-stylesheet", @"type=""text/xsl"" href=""veradoc.xsl""");
						VeraDocWriter.WriteStartElement("tree");

						foreach (Compiler.SourceTreeEntry SourceTree in compiler.GetSourceTree(true).Children) {
							WriteTree(SourceTree, VeraDocWriter, null);
						}

						VeraDocWriter.WriteEndElement();
						VeraDocWriter.Flush();

						OutputZip.Add(new ZipFileEntry() {
							Name = "tree.xml",
							Data = DocWriterStream.ToArray()
						});
					}
				}

			}

			OutputZip.Save(stream);
		}

		/// <summary>
		/// Write general data to a zip file.
		/// </summary>
		private static void WriteResourceToZip(ZipFile zip, string filename, byte[] data) {
			zip.Add(new ZipFileEntry() {
				Data = data,
				Name = filename,
			});
		}

		/// <summary>
		/// Write string data to a zip file.
		/// </summary>
		private static void WriteResourceToZip(ZipFile zip, string filename, string data) {
			zip.Add(new ZipFileEntry() {
				Data = Encoding.UTF8.GetBytes(data),
				Name = filename,
			});
		}

		/// <summary>
		/// Write image data to a zip file (as a PNG).
		/// </summary>
		private static void WriteResourceToZip(ZipFile zip, string filename, Image data) {
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
			name = name.Replace("..", "parent");
			name = compiler.GetRelativeFilename(name);
			name = name.Replace(Path.DirectorySeparatorChar, '.');
			name = name.Replace(Path.AltDirectorySeparatorChar, '.');
			name += ".xml";
			return name;
		}

		/// <summary>
		/// Gets the depth of a path.
		/// </summary>
		/// <param name="path">Path to get the depth of.</param>
		/// <returns>How deeply nested a path is.</returns>
		private static int GetPathDepth(string path) {
			return path.Split(new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar }).Length - 1;
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

		/// <summary>
		/// Gets a formatting style for a section.
		/// </summary>
		/// <param name="section">The section to get a formatting style for.</param>
		private static SectionFormatting GetSectionFormatting(DocumentationSection section) {
			switch (section) {
				case DocumentationSection.Example:
					return SectionFormatting.None;

				case DocumentationSection.Description:
				case DocumentationSection.Warning:
					return SectionFormatting.Paragraphs;

				default:
					return SectionFormatting.OneItemPerLine;
			}
		}

		/// <summary>
		/// Check if a complete routine name is valid.
		/// </summary>
		/// <param name="name">The routine name to check.</param>
		/// <param name="invalidReason">The reason for failure if the name is invalid.</param>
		/// <returns>True if valid, false if invalid.</returns>
		private static bool ValidRoutineName(string name, out string invalidReason) {
			invalidReason = null;
			
			// check 7-bit ASCII validity;
			if (Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(name)) != name) {
				invalidReason = string.Format(Strings.ErrorVeraDocNameNot7Bit, name);
				return false;
			}

			if (name != name.ToLowerInvariant()) {
				invalidReason = string.Format(Strings.ErrorVeraDocNameContainsUppercase, name);
				return false;
			}


			foreach (string s in name.Split('.')) {
				if (!ValidRoutineComponentName(s, out invalidReason)) return false;
			}

			return true;
		}


		/// <summary>
		/// Check if a component part of a routine name is valid.
		/// </summary>
		/// <param name="name">The part of the routine name to check.</param>
		/// <param name="invalidReason">The reason for failure if the name is invalid.</param>
		/// <returns>True if valid, false if invalid.</returns>
		private static bool ValidRoutineComponentName(string name, out string invalidReason) {
			
			invalidReason = null;

			string ValidChars = "abcdefghijklmnopqrstuvwxyz0123456789_";
			
			if (name.Length == 0) {
				invalidReason = string.Format(Strings.ErrorVeraDocNameIsZeroLength, name);
				return false;
			}

			if (char.IsNumber(name[0])) {
				invalidReason = string.Format(Strings.ErrorVeraDocNameStartsWithNumber, name);
				return false;
			}

			if (name[0] == '_') {
				invalidReason = string.Format(Strings.ErrorVeraDocNameStartsWithUnderscore, name);
				return false;
			}

			if (name[name.Length - 1] == '_') {
				invalidReason = string.Format(Strings.ErrorVeraDocNameEndsWithUnderscore, name);
				return false;
			}

			foreach (char c in name) {
				if (ValidChars.IndexOf(c) == -1) {
					invalidReason = string.Format(Strings.ErrorVeraDocNameContainsInvalidChar, name, c.ToString());
					return false;
				}
			}

			return true;

		}

		#endregion

	}
}
