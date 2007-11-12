using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;
using System.Xml;

namespace Brass3 {
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
			this.assemblers = new NamedPluginCollection<IAssembler>(this);
			this.labels = new LabelCollection(this);
			this.directives = new NamedPluginCollection<IDirective>(this);
			this.functions = new NamedPluginCollection<IFunction>(this);
			this.outputWriters = new NamedPluginCollection<IOutputWriter>(this);
			this.outputModifiers = new NamedPluginCollection<IOutputModifier>(this);
			this.stringEncoders = new NamedPluginCollection<IStringEncoder>(this);
			this.output = new List<OutputData>();
			this.listingWriters = new NamedPluginCollection<IListingWriter>(this);
			this.listingFiles = new Dictionary<string, IListingWriter>();
			this.numberEncoders = new NamedPluginCollection<INumberEncoder>(this);
			this.invisiblePlugins = new NamedPluginCollection<IPlugin>(this);
			this.dataStructures = new NamedPluginCollection<IDataStructure>(this);
			this.MacroLookup = new Dictionary<string, PreprocessMacro>(128);
			this.includeSearchDirectories = new List<string>();

			this.statements = new LinkedList<SourceStatement>();

			this.allErrors = new List<NotificationEventArgs>();
			this.allWarnings = new List<NotificationEventArgs>();
			this.allInformation = new List<NotificationEventArgs>();
		}


		#endregion


		#region Public Methods





		#endregion

	}
}
