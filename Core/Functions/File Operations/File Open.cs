using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fopen(\"filename\")")]
	[Description("Opens a file and returns a handle for subsequent file operations.")]
	[Remarks("Files are implicitly closed after each pass.")]
	[Category("File Operations")]
	public class FOpen : IFunction {

		/// <summary>
		/// Stores file handle mappings.
		/// </summary>
		private Dictionary<double, FileStream> FileHandles;

		private double FileHandleAllocation;


		public FOpen(Compiler compiler) {
			this.FileHandles = new Dictionary<double, FileStream>();
			compiler.CompilationBegun += new EventHandler(delegate(object sender, EventArgs e) { FileHandles.Clear(); FileHandleAllocation = 1; });
			compiler.CompilationEnded += new EventHandler(delegate(object sender, EventArgs e) { foreach (KeyValuePair<double, FileStream> KVP in FileHandles) { KVP.Value.Dispose(); } });
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			string Filename = (source.GetExpressionStringConstant(compiler, Args[0], false));

			if (!File.Exists(Filename)) throw new CompilerException(source, string.Format(Strings.ErrorFileNotFound, Filename));

			double FileHandle = FileHandleAllocation++;
			this.FileHandles.Add(FileHandle, File.OpenRead(Filename));
			return new Label(compiler.Labels, FileHandle);

		}

		internal static FileStream GetFilestreamFromHandle(Compiler compiler, double handle) {
			FOpen Handles = compiler.GetPluginInstanceFromType<FOpen>();
			if (Handles == null) throw new InvalidOperationException(string.Format(Strings.ErrorPluginNotLoaded, "fopen"));
			FileStream Result;
			if (!Handles.FileHandles.TryGetValue(handle, out Result)) throw new InvalidOperationException(string.Format(Strings.ErrorFileHandleInvalid, handle));
			return Result;
		}

		internal static FileStream GetFilestreamFromHandle(Compiler compiler, TokenisedSource source) {
			int[] Args = source.GetCommaDelimitedArguments(0);
			if (Args.Length < 1) throw new DirectiveArgumentException(source, Strings.ErrorFileHandleNotSpecified);
			return GetFilestreamFromHandle(compiler, source.EvaluateExpression(compiler, Args[0]).NumericValue);
		}
	}
}
