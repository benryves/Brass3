using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using BeeDevelopment.Brass3.Plugins;
using System.Xml;

namespace BeeDevelopment.Brass3 {
	/// <summary>
	/// Defines a compiler that manages the various stages of building a project.
	/// </summary>
	public partial class Compiler {

		#region Constructor

		/// <summary>
		/// Creates an instance of a compiler.
		/// </summary>
		public Compiler() {
			this.assembler = null;
			this.assemblers = new PluginCollection<IAssembler>(this);
			this.labels = new LabelCollection(this);
			this.directives = new PluginCollection<IDirective>(this);
			this.functions = new PluginCollection<IFunction>(this);
			this.outputWriters = new PluginCollection<IOutputWriter>(this);
			this.outputModifiers = new PluginCollection<IOutputModifier>(this);
			this.stringEncoders = new PluginCollection<IStringEncoder>(this);
			this.output = new List<OutputData>();
			this.listingWriters = new PluginCollection<IListingWriter>(this);
			this.listingFiles = new Dictionary<string, IListingWriter>();
			this.numberEncoders = new PluginCollection<INumberEncoder>(this);
			this.invisiblePlugins = new PluginCollection<IPlugin>(this);
			this.dataStructures = new PluginCollection<IDataStructure>(this);
			this.MacroLookup = new Dictionary<string, PreprocessMacro>(128);
			this.includeSearchDirectories = new List<string>();

			this.statements = new LinkedList<SourceStatement>();

			this.allErrors = new List<NotificationEventArgs>();
			this.allWarnings = new List<NotificationEventArgs>();
			this.allInformation = new List<NotificationEventArgs>();
			this.Breakpoints = new List<Breakpoint>();
		}


		#endregion

	}
}
