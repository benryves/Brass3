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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;

namespace Legacy.Latenite {
	[Category("Latenite 1")]
	[Description("Writes an XML debugging log for Latenite 1.")]
	public class Latenite1Debug : IListingWriter {

		public void WriteListing(Compiler compiler, Stream stream) {
			XmlWriter ErrorFile = XmlWriter.Create(stream);


			ErrorFile.WriteStartElement("brass");
			ErrorFile.WriteAttributeString("version", Assembly.GetExecutingAssembly().GetName().Version.ToString());

			ErrorFile.WriteStartElement("debug");

			ErrorFile.WriteAttributeString("binary", compiler.DestinationFile);

			StringDictionary EnvironmentVariables = Process.GetCurrentProcess().StartInfo.EnvironmentVariables;
			if (EnvironmentVariables.ContainsKey("debug_debugger")) ErrorFile.WriteAttributeString("debugger", EnvironmentVariables["debug_debugger"].Trim('"'));
			if (EnvironmentVariables.ContainsKey("debug_debugger_args")) ErrorFile.WriteAttributeString("debugger_args", EnvironmentVariables["debug_debugger_args"]);

			ErrorFile.WriteEndElement();
			ErrorFile.WriteEndElement();

			ErrorFile.Flush();
		}


	}
}
