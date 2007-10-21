using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;
using System.Globalization;

namespace Legacy.Latenite {
	[Category("Latenite 1")]
	[Description("Writes an XML error log for Latenite 1.")]
	[Remarks("Each error is output as an XML element (<c>error</c>, <c>warning</c> or <c>message</c>) with a text description in a <c>&lt;![CDATA[...]]&gt;</c> block.\r\nEach XML element will have, if available, a <c>file</c> and <c>line</c> attribute corresponding to the file name and line number that the error was generated on.")]
	public class Latenite1Errors : IListingWriter {

		public void WriteListing(Compiler compiler, Stream stream) {
			XmlWriter ErrorFile = XmlWriter.Create(stream);


			ErrorFile.WriteStartElement("latenite");

			ErrorFile.WriteAttributeString("version", "2");

			KeyValuePair<string, Compiler.NotificationEventArgs[]>[] ErrorFileContents = new KeyValuePair<string, Compiler.NotificationEventArgs[]>[] {
				new KeyValuePair<string, Compiler.NotificationEventArgs[]>("error", compiler.AllErrors),
				new KeyValuePair<string, Compiler.NotificationEventArgs[]>("warning", compiler.AllWarnings),
				new KeyValuePair<string, Compiler.NotificationEventArgs[]>("message", compiler.AllInformation)
			};

			foreach (KeyValuePair<string, Compiler.NotificationEventArgs[]> ErrorFileSection in ErrorFileContents) {
				foreach (Compiler.NotificationEventArgs Error in ErrorFileSection.Value) {
					ErrorFile.WriteStartElement(ErrorFileSection.Key);

					if (Error.LineNumber != 0) ErrorFile.WriteAttributeString("line", Error.LineNumber.ToString(CultureInfo.InvariantCulture));
					if (!string.IsNullOrEmpty(Error.Filename)) ErrorFile.WriteAttributeString("file", Error.Filename);

					ErrorFile.WriteCData(Error.Message);

					ErrorFile.WriteEndElement();
				}
			}
			

			ErrorFile.WriteEndElement();

			ErrorFile.Flush();
		}


	}
}
