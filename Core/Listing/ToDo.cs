using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Globalization;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

namespace Core.Listing {

	[Category("Documentation")]
	[Description("Outputs a to-do list in XML format.")]
	[Remarks(
@"The to-do list is populated by comments from your project.
Any comment that starts with <c>todo:</c> or <c>hack:</c> will be written to the XML log file.")]
	[CodeExample(
@"/* todo: Fix case when bc is negative. */
; hack: Hard-coded delay to give hardware time to respond.")]
	public class ToDo : IListingWriter {

		private struct ToDoItem {
			public string Type;
			public string Comment;
			public string Filename;
			public int Line; 
			public ToDoItem(string type, string text, string filename, int line) {
				this.Type = type; this.Comment = text; this.Filename = filename; this.Line = line;
			}
		}

		public void WriteListing(Compiler compiler, Stream stream) {

			List<ToDoItem> ToDoList = new List<ToDoItem>();
			
			foreach (Compiler.SourceStatement Statement in compiler.Statements) {

				foreach (TokenisedSource.Token Token in Statement.Source.OutermostTokenisedSource.Tokens) {

					if (Token.Type == TokenisedSource.Token.TokenTypes.Comment) {
						string CommentsContent = Token.GetCommentString().Trim();
						string CommentsContentLower = CommentsContent.ToLowerInvariant();
						if (CommentsContentLower.StartsWith("todo:")) {
							ToDoList.Add(new ToDoItem("todo", CommentsContent.Substring(5).TrimStart(), Statement.Filename, Statement.LineNumber));
						} else if (CommentsContentLower.StartsWith("hack:")) {
							ToDoList.Add(new ToDoItem("hack", CommentsContent.Substring(5).TrimStart(), Statement.Filename, Statement.LineNumber));
						}
					}
					
				}

			}

			XmlWriterSettings Settings = new XmlWriterSettings();
			Settings.Indent = true;
			XmlWriter Writer = XmlWriter.Create(stream, Settings);

			Writer.WriteStartElement("todo");
			foreach (ToDoItem Item in ToDoList) {
				Writer.WriteStartElement(Item.Type);
				Writer.WriteElementString("comment", Item.Comment);
				Writer.WriteElementString("filename", compiler.GetRelativeFilename(Item.Filename));
				Writer.WriteElementString("line", Item.Line.ToString(CultureInfo.InvariantCulture));
				Writer.WriteEndElement();
			}
			Writer.WriteEndElement();

			Writer.Flush();
			Writer.Close();

		}
	}
}
