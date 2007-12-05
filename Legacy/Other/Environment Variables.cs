using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Legacy.Other {

	[Description("Inserts environment variables into source files.")]
	[Remarks("Brass 1 would perform a replacement of everything inside <c>[%tags%]</c> with the corresponding environment variable. Brass 3's parser doesn't support environment variable substitution, so this plugin tries to help port over old source code.\r\nThis plugin registers macros before assembling that will replace environment variables matching <c>\"[%key%]\"</c> with the corresponding values.\r\nThe macro preprocessor deals with individual tokens, and so can't perform substitution on environment variables not declared as a string.\r\nIf you need to pass a numeric value to your source code via an environment variable, use the <c>eval()</c> function.")]
	[Warning("You can only insert environment variables into strings. Use <c>eval()</c> to evaluate a passed string into a numeric value.")]
	[CodeExample("Porting Brass 1 code to Brass 3", "/* The following definitions are required */\r\n\r\nNone     = 0 ; No shell - All\r\nIon      = 1 ; Ion      - 83, 83+\r\nMirageOS = 2 ; MirageOS - 83+\r\nVenus    = 3 ; Venus    - 83\r\n\r\nTI8X     = 1 ; TI-83 Plus\r\nTI83     = 2 ; TI-83\r\n\r\n/*\r\nThe build script passes in a value for \r\nSHELL and PLATFORM. We need these to be\r\nnumeric values. The Brass 1 source code\r\nwas as follows:\r\n\r\nShell    = [%SHELL%]\r\nPlatform = [%PLATFORM%]\r\n\r\nThis will not work in Brass 3!\r\nThe following code demonstrates a way to\r\nwork around Brass 3's stricter parser.\r\n*/\r\n\r\nShell    = eval(\"[%SHELL%]\")\r\nPlatform = eval(\"[%PLATFORM%]\")\r\n\r\n/*\r\nThe following used a string anyway, so\r\nno code needed to be changed.\r\nThere is no functional difference between\r\n[%var%] and [%$var$%].\r\n*/\r\n\r\n.if Shell != None\r\n\t.db \"[%$PROJECT_NAME$%]\", 0\r\n.endif")]
	[SeeAlso(typeof(Core.Functions.Eval))]
	public class EnvVars : IPlugin {

		public EnvVars(Compiler compiler) {
			compiler.CompilationBegun += delegate(object sender, EventArgs e) {

				foreach (DictionaryEntry Variable in Process.GetCurrentProcess().StartInfo.EnvironmentVariables) {
					string Key = Variable.Key as string;
					string Value = Variable.Value as string;

					TokenisedSource.Token[] ReplacementString = TokenisedSource.Join(TokenisedSource.FromString(compiler, "\"" + Value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\""));

					Compiler.PreprocessMacro ReplacementMacro = delegate(Compiler c, ref TokenisedSource source, int index) {
						source.ReplaceToken(index, ReplacementString);
					};

					compiler.RegisterMacro("\"[%" + Key + "%]\"", ReplacementMacro);
					compiler.RegisterMacro("\"[%$" + Key + "$%]\"", ReplacementMacro);
				}
			};
		}

	}
}
