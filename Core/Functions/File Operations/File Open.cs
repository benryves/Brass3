using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fopen(\"filename\")")]
	[Description("Opens a file and returns a handle for subsequent file operations.")]
	[Remarks("Files are implicitly closed after each pass.")]
	[Category("File Operations")]
	public class FileOpen : IFunction {

		/// <summary>
		/// Stores file handle mappings.
		/// </summary>
		private Dictionary<double, FileStream> FileHandles;

		private double FileHandleAllocation;

		public string[] Names { get { return new string[] { "fopen" }; } }
		public string Name { get { return this.Names[0]; } }

		public FileOpen(Compiler compiler) {
			this.FileHandles = new Dictionary<double, FileStream>();
			compiler.PassBegun += new EventHandler(delegate(object sender, EventArgs e) { FileHandles.Clear(); FileHandleAllocation = 1; });
			compiler.PassEnded += new EventHandler(delegate(object sender, EventArgs e) { foreach (KeyValuePair<double, FileStream> KVP in FileHandles) { KVP.Value.Dispose(); } });
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			if (!source.ExpressionIsStringConstant(Args[0])) throw new DirectiveArgumentException(source, "Expected a filename.");
			string Filename = (source.GetExpressionStringConstant(Args[0], false));

			if (!File.Exists(Filename)) throw new CompilerExpection(source, "File '" + Filename + "' not found.");

			double FileHandle = FileHandleAllocation++;
			this.FileHandles.Add(FileHandle, File.OpenRead(Filename));
			return new Label(compiler.Labels, FileHandle);

		}

		internal static FileStream GetFilestreamFromHandle(Compiler compiler, double handle) {
			FileOpen Handles = compiler.GetPluginInstanceFromType(typeof(FileOpen)) as FileOpen;
			if (Handles == null) throw new InvalidOperationException("fopen() plugin not loaded.");
			FileStream Result;
			if (!Handles.FileHandles.TryGetValue(handle, out Result)) throw new InvalidOperationException("File handle " + handle + " is invalid.");
			return Result;
		}

		internal static FileStream GetFilestreamFromHandle(Compiler compiler, TokenisedSource source) {
			int[] Args = source.GetCommaDelimitedArguments(0);
			if (Args.Length < 1) throw new DirectiveArgumentException(source, "File handle not specified.");
			return GetFilestreamFromHandle(compiler, source.EvaluateExpression(compiler, Args[0]).NumericValue);
		}
	}
}
