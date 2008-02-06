using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;
using System.Globalization;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;

namespace Legacy.Latenite {
	[Category("Latenite 1")]
	[Description("Writes an XML debugging log for Latenite 1.")]
	public class Latenite1Debug : IListingWriter {

		public void WriteListing(Compiler compiler, Stream stream) {

			var Settings = new XmlWriterSettings() {
				Indent = true,
			};

			XmlWriter ErrorFile = XmlWriter.Create(stream, Settings);


			ErrorFile.WriteStartElement("brass");
			ErrorFile.WriteAttributeString("version", Assembly.GetExecutingAssembly().GetName().Version.ToString());

			ErrorFile.WriteStartElement("debug");

			ErrorFile.WriteAttributeString("binary", compiler.DestinationFile);

			StringDictionary EnvironmentVariables = Process.GetCurrentProcess().StartInfo.EnvironmentVariables;
			if (EnvironmentVariables.ContainsKey("debug_debugger")) ErrorFile.WriteAttributeString("debugger", EnvironmentVariables["debug_debugger"].Trim('"'));
			if (EnvironmentVariables.ContainsKey("debug_debugger_args")) ErrorFile.WriteAttributeString("debugger_args", EnvironmentVariables["debug_debugger_args"]);

			ErrorFile.WriteEndElement();

			foreach (var Breakpoint in compiler.Breakpoints) {
				ErrorFile.WriteStartElement("breakpoint");
				ErrorFile.WriteAttributeString("address", Breakpoint.Address.ToString());
				ErrorFile.WriteAttributeString("page", Breakpoint.Page.ToString());
				ErrorFile.WriteAttributeString("description", Breakpoint.Description);
				ErrorFile.WriteEndElement();
			}

			ErrorFile.WriteStartElement("module");
			foreach (var Label in compiler.Labels) {
				if (!Label.IsString && Label.Created) {
					ErrorFile.WriteStartElement("label");
					ErrorFile.WriteAttributeString("name", Label.Name);
					ErrorFile.WriteAttributeString("value", ((int)Label.NumericValue).ToString());
					ErrorFile.WriteAttributeString("page", Label.Page.ToString());
					ErrorFile.WriteAttributeString("exported", Label.Exported ? "true" : "false");
					ErrorFile.WriteAttributeString("fullname", Label.Name);
					if (Label.DataType != null) ErrorFile.WriteAttributeString("type", Label.DataType.ToString());
					ErrorFile.WriteEndElement();
				}
			}
			ErrorFile.WriteEndElement();

			ErrorFile.WriteEndElement();

			ErrorFile.Flush();
		}


	}
}
