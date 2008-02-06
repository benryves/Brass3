using System;
using System.ComponentModel;
using System.Diagnostics;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Core.Debuggers {

	[Description("resources://Core.Documentation/ExternalProgramDescription")]
	[Warning("resources://Core.Documentation/ExternalProgramWarning")]
	[Remarks("resources://Core.Documentation/ExternalProgramRemarks")]
	[CodeExample(@"<!-- Suitable for Kega Fusion, for example. -->
<input>
	<label
		name=""Debugger.Path""
		type=""string""
		value=""C:\Program Files\Fusion\Fusion.exe"" 
	/>
	<label
		name=""Debugger.Args""
		type=""string""
		value=""&amp;quot;$(TargetPath)&amp;quot;""
	/>
</input>")]
	public class ExternalProgram : IDebugger, IDisposable {

		private Process Debugger = null;

		public void Start(Compiler compiler, bool debuggingEnabled) {
			this.Debugger = new Process();
			
			Label L;
			if (!compiler.Labels.TryParse(new TokenisedSource.Token("Debugger.Path"), out L)) {
				throw new Exception("Debugger.Path not set.");
			}
			Debugger.StartInfo.FileName = this.EscapeConstants(compiler, L.StringValue);

			if (compiler.Labels.TryParse(new TokenisedSource.Token("Debugger.Args"), out L)) {
				Debugger.StartInfo.Arguments = this.EscapeConstants(compiler, L.StringValue);
			}

			Debugger.Start();
		}

		public void Dispose() {
			if (this.Debugger != null) {
				this.Debugger.Dispose();
				this.Debugger = null;
			}
		}

		private string EscapeConstants(Compiler compiler, string toEscape) {
			
			var Constants = new Dictionary<string, string>();

			Constants.Add("OutDir", Path.GetDirectoryName(compiler.DestinationFile));
			Constants.Add("ConfigurationName", compiler.Project.ConfigurationName);
			Constants.Add("ProjectName", Path.GetFileNameWithoutExtension(compiler.Project.ProjectFilename));
			Constants.Add("TargetName", Path.GetFileNameWithoutExtension(compiler.DestinationFile));
			Constants.Add("TargetPath", compiler.DestinationFile);
			Constants.Add("ProjectPath", compiler.Project.ProjectFilename);
			Constants.Add("ProjectFileName", Path.GetFileName(compiler.Project.ProjectFilename));
			Constants.Add("TargetExt", Path.GetExtension(compiler.DestinationFile));
			Constants.Add("TargetFileName", Path.GetFileName(compiler.DestinationFile));
			Constants.Add("DevEnvDir", Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
			Constants.Add("TargetDir", Path.GetDirectoryName(compiler.DestinationFile));
			Constants.Add("ProjectDir", Path.GetDirectoryName(compiler.Project.ProjectFilename));
			Constants.Add("ProjectExt", Path.GetExtension(compiler.Project.ProjectFilename));

			foreach (var Constant in Constants) {
				toEscape = toEscape.Replace("$(" + Constant.Key + ")", Constant.Value);
			}

			return toEscape;
		}

		~ExternalProgram() {
			this.Dispose();
		}
	}
}
