using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;
using System.Xml;

namespace Brass3 {
	public partial class Compiler {

		#region Plugins


		#region Assembler

		private readonly PluginCollection<IAssembler> assemblers;
		/// <summary>
		/// Gets the loaded assemblers.
		/// </summary>
		public PluginCollection<IAssembler> Assemblers {
			get { return this.assemblers; }
		}

		private IAssembler assembler;
		/// <summary>
		/// Gets or sets the assembler being used by the compiler.
		/// </summary>
		public IAssembler CurrentAssembler {
			get { return this.assembler; }
			set { this.assembler = value; }
		}

		#endregion

		#region Directives

		private readonly PluginCollection<IDirective> directives;
		/// <summary>
		/// Gets the directives being used by the compiler.
		/// </summary>
		public PluginCollection<IDirective> Directives {
			get { return this.directives; }
		}

		#endregion

		#region Output Writer

		private readonly PluginCollection<IOutputWriter> outputWriters;
		/// <summary>
		/// Gets the output writers available to the compiler.
		/// </summary>
		public PluginCollection<IOutputWriter> OutputWriters {
			get { return this.outputWriters; }
		}

		private IOutputWriter outputWriter;
		/// <summary>
		/// Gets or sets the output writer being used by the compiler.
		/// </summary>
		public IOutputWriter OutputWriter {
			get { return this.outputWriter; }
			set { this.outputWriter = value; }
		}

		#endregion

		#region Listing Writer

		private readonly PluginCollection<IListingWriter> listingWriters;
		/// <summary>
		/// Gets the listing writers available to the compiler.
		/// </summary>
		public PluginCollection<IListingWriter> ListingWriters {
			get { return this.listingWriters; }
		}

		private Dictionary<string, IListingWriter> listingFiles;
		/// <summary>
		/// Gets or the filenames and associated plugins being used to generate listing files.
		/// </summary>
		public Dictionary<string, IListingWriter> ListingFiles {
			get { return this.listingFiles; }
		}

		#endregion

		#region Functions

		private readonly PluginCollection<IFunction> functions;
		/// <summary>
		/// Gets the functions being used by the compiler.
		/// </summary>
		public PluginCollection<IFunction> Functions {
			get { return this.functions; }
		}

		#endregion

		#region Output Modifiers

		/// <summary>
		/// Gets the output modifiers being used by the compiler.
		/// </summary>
		private readonly PluginCollection<IOutputModifier> outputModifiers;
		/// <summary>
		/// Gets the output modifiers being used by the compiler.
		/// </summary>
		public PluginCollection<IOutputModifier> OutputModifiers {
			get { return this.outputModifiers; }
		}

		#endregion
		
		#region String Encoding

		private readonly PluginCollection<IStringEncoder> stringEncoders;
		/// <summary>
		/// Gets the string encoders available to the compiler.
		/// </summary>
		public PluginCollection<IStringEncoder> StringEncoders {
			get { return this.stringEncoders; }
		}


		private bool customStringEncoderEnabled;
		/// <summary>
		/// Gets or sets whether the custom string encoder is enabled.
		/// </summary>
		public bool CustomStringEncoderEnabled {
			get { return this.customStringEncoderEnabled; }
			set { this.customStringEncoderEnabled = value; }
		}
		private IStringEncoder stringEncoder;
		/// <summary>
		/// Gets or sets the current string encoder being used by the compiler.
		/// </summary>
		public IStringEncoder StringEncoder {
			get {
				if (!this.CustomStringEncoderEnabled) {
					return new DefaultStringEncoder();
				} else {
					return this.stringEncoder ?? new DefaultStringEncoder();
				}
			}
			set { this.stringEncoder = value; }
		}

		#endregion

		#region Number Encoding

		private readonly PluginCollection<INumberEncoder> numberEncoders;
		/// <summary>
		/// Gets the number encoders available to the compiler.
		/// </summary>
		public PluginCollection<INumberEncoder> NumberEncoders {
			get { return this.numberEncoders; }
		}

		#endregion

		#region Misc

		private readonly PluginCollection<IPlugin> invisiblePlugins;
		/// <summary>
		/// Gets the "invisible" plugins loaded by the compiler.
		/// </summary>
		public PluginCollection<IPlugin> InvisiblePlugins {
			get { return this.invisiblePlugins; }
		}

		#endregion

		#endregion

		private readonly PluginCollection<IDataStructure> dataStructures;
		/// <summary>
		/// Gets the registered data structures.
		/// </summary>
		public PluginCollection<IDataStructure> DataStructures {
			get { return this.dataStructures; }
		}

		private Endianness endianness;
		/// <summary>
		/// Gets or sets the current endianness of the compiler.
		/// </summary>
		public Endianness Endianness {
			get { return this.endianness; }
			set { this.endianness = value; }
		}


		private readonly LabelCollection labels;
		/// <summary>
		/// Gets the labels being used by the compiler.
		/// </summary>
		public LabelCollection Labels {
			get { return this.labels; }
		}

	
		private readonly List<OutputData> output;
		/// <summary>
		/// Gets the output data from the assembler.
		/// </summary>
		public OutputData[] Output {
			get { return this.output.ToArray(); }
		}

		private bool switchedOn;
		/// <summary>
		/// Gets true if the compiler is currently switched on.
		/// </summary>
		public bool IsSwitchedOn {
			get { return this.switchedOn; }
		}

		private Type Reactivator = null;

		private Label labelEvaluationResult;
		/// <summary>
		/// Gets the result of the evaluation of the left half of the source statement containing the label.
		/// </summary>
		public Label LabelEvaluationResult {
			get { return this.labelEvaluationResult; }
		}

		private string sourceFile;
		/// <summary>
		/// Gets or sets the source file to start compiling from.
		/// </summary>
		public string SourceFile {
			get { return this.sourceFile; }
			set { this.sourceFile = value; }
		}

		/// <summary>
		/// Gets the name of the source file currently being compiled.
		/// </summary>
		public string CurrentFile {
			get {
				return (this.CurrentStatement == null || this.CurrentStatement.Value == null) ? null : this.CurrentStatement.Value.Filename;
			}
		}


		/// <summary>
		/// Gets the line number of the current statement being compiled, or zero.
		/// </summary>
		public int CurrentLineNumber {
			get {
				return (this.CurrentStatement == null || this.CurrentStatement.Value == null) ? 0 : this.CurrentStatement.Value.LineNumber;
			}

		}

		private string destinationFile;
		/// <summary>
		/// Gets or sets the destination file to compile to.
		/// </summary>
		public string DestinationFile {
			get { return this.destinationFile; }
			set { this.destinationFile = value; }
		}

		private byte emptyFill;
		/// <summary>
		/// Gets or sets the default fill value for padding directives.
		/// </summary>
		public byte EmptyFill {
			get { return this.emptyFill; }
			set { this.emptyFill = value; }
		}

		private bool writingToListFile;
		/// <summary>
		/// Gets or sets whether statements are written to the list file.
		/// </summary>
		public bool WritingToListFile {
			get { return this.writingToListFile; }
			set { this.writingToListFile = value; }
		}

		private readonly List<NotificationEventArgs> allErrors;
		/// <summary>
		/// Gets all errors generated by the compiler.
		/// </summary>
		public NotificationEventArgs[] AllErrors {
			get { return this.allErrors.ToArray(); }
		}

		private readonly List<NotificationEventArgs> allWarnings;
		/// <summary>
		/// Gets all warnings generated by the compiler.
		/// </summary>
		public NotificationEventArgs[] AllWarnings {
			get { return this.allWarnings.ToArray(); }
		}

		private readonly List<NotificationEventArgs> allInformation;
		/// <summary>
		/// Gets all information generated by the compiler.
		/// </summary>
		public NotificationEventArgs[] AllInformation {
			get { return this.allInformation.ToArray(); }
		}

		/// <summary>
		/// Gets the list of <see cref="Breakpoint"/> created during the compilation.
		/// </summary>
		public List<Breakpoint> Breakpoints {
			get;
			private set;
		}

	}
}
