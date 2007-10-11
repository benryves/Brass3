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

		public Compiler() {
			this.assembler = null;
			this.assemblers = new NamedPluginCollection<IAssembler>();
			this.labels = new LabelCollection(this);
			this.directives = new AliasedPluginCollection<IDirective>();
			this.functions = new AliasedPluginCollection<IFunction>();
			this.outputWriters = new NamedPluginCollection<IOutputWriter>();
			this.outputModifiers = new NamedPluginCollection<IOutputModifier>();
			this.stringEncoders = new NamedPluginCollection<IStringEncoder>();
			this.output = new List<OutputData>();
			this.listingWriters = new NamedPluginCollection<IListingWriter>();
			this.listingFiles = new Dictionary<string, IListingWriter>();
			this.numberEncoders = new NamedPluginCollection<INumberEncoder>();
			this.dataStructures = new NamedPluginCollection<IDataStructure>();
		}


		#endregion


		#region Public Methods



		#region Misc.

		/// <summary>
		/// Resolve a filename.
		/// </summary>
		/// <param name="filename">The filename to try and resolve.</param>
		/// <returns>The resolved filename, after checking various directories.</returns>
		public string ResolveFilename(string filename) {
			string CurrentFilename = Path.Combine(Path.GetDirectoryName(this.CurrentFile), filename);
			if (File.Exists(CurrentFilename)) return CurrentFilename;
			string LocalFilename = Path.Combine(Path.GetDirectoryName(this.SourceFile), filename);
			if (File.Exists(LocalFilename)) return LocalFilename;
			return filename;
		}

		#endregion

		#endregion

	}
}
