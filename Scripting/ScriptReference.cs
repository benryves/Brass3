using System;
using System.Collections.Generic;
using System.Text;

using System.CodeDom.Compiler;
using System.Reflection;
using System.ComponentModel;
using System.IO;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.Globalization;

namespace Scripting {

	[Category("Scripting")]
	[Syntax(".scriptreference \"reference\" [, \"reference\" [, ...]]")]
	[Description("Adds references to included scripts.")]
	[Remarks(
@"When script files (loaded with <see cref=""incscript""/>) are compiled you need to explicitly reference any assemblies containing the functionality that your source references.
By default, only <c>System.dll</c> and <c>Brass.exe</c> are referenced.")]
	
	[SeeAlso(typeof(IncScript))]
	public class ScriptReference : IDirective {

		internal List<string> References;

		public ScriptReference(Compiler compiler) {
			this.References = new List<string>();
			compiler.CompilationBegun += delegate(object sender, EventArgs e) {
				this.References.Clear();
				this.References.Add(ResolveAssemblyName("System.dll"));
				this.References.Add(ResolveAssemblyName("Brass.exe"));
			};
		}

		private static string ResolveAssemblyName(string name) {
			string LocalPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), name);
			return (File.Exists(LocalPath)) ? LocalPath : name;
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			foreach (object o in source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.String | TokenisedSource.ArgumentType.RepeatForever })) {
				this.References.Add(ResolveAssemblyName(o as string));
			}
		}
	}
}
