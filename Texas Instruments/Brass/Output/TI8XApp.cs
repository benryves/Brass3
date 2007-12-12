using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

using TexasInstruments.Types;
using TexasInstruments.Types.Variables;


namespace TexasInstruments.Brass.Output {

	[Category("Texas Instruments")]
	[Description("Writes and signs a TI-83 Plus application (*.8xk).")]
	[Remarks(TI8XApp.Remarks)]
	[SeeAlso(typeof(TI73App)), SeeAlso(typeof(Directives.AppHeader)), SeeAlso(typeof(Directives.TIVariableName))]
	public class TI8XApp : IOutputWriter {

		internal const string Remarks = @"
This output writer uses the <see cref=""intelhex""/> output writer plugin to generate an output binary. It then uses Wappsign (from the TI-83 Plus SDK) to sign the application.
For this reason, you must configure Wappsign first. Run Wappsign from the Start Menu or from the <i>Utils</i> subdirectory of the SDK's installation directory. Click the [&hellip;] button next to the <i>Key file</i> field, then browse for the <c>0104.key</c> file. Answer <i>Yes</i> to the question <i>""This directory is not in your search path. Add it now?""</i>. Tick the <i>Save settings on exit</i> box, then click <i>Close</i>.
If signing the application fails for any reason, A warning message is displayed and the output file is left as an unsigned Intel HEX file. You will need to sign the application yourself manually.";


		public virtual string DefaultExtension {
			get { return "8xk"; }
		}

		public virtual void WriteOutput(Compiler compiler, Stream stream) {
			Sign(compiler, stream);
		}

		internal void Sign(Compiler compiler, Stream stream) {
			
			Core.Output.IntelHex HexWriter = compiler.GetPluginInstanceFromType<Core.Output.IntelHex>();
			if (HexWriter == null) throw new CompilerException((TokenisedSource)null, "Intel HEX plugin not available.");

			Utility.Wappsign Wappsign;
			List<string> TempFiles = new List<string>();
			
			try {
				
				Wappsign = new TexasInstruments.Utility.Wappsign(TexasInstruments.Utility.Wappsign.Mode.DetectType);

				TempFiles.Add(Path.GetTempFileName() + ".hex");
				using (FileStream TempStream = File.OpenWrite(TempFiles[0])) {
					HexWriter.WriteOutput(compiler, TempStream);
				}


				string KeyFile = Wappsign.GetKeyFile(TempFiles[0]);
				if (string.IsNullOrEmpty(KeyFile) || !File.Exists(KeyFile)) throw new CompilerException((TokenisedSource)null, "Invalid key name.");


				TempFiles.Add(Path.GetTempFileName());
				int SignError = Wappsign.Sign(TempFiles[0], KeyFile, TempFiles[1]);
				if (SignError != 0) throw new CompilerException((TokenisedSource)null, Wappsign.GetErrorMessage(SignError));

				byte[] SignedOutput = File.ReadAllBytes(TempFiles[1]);

				stream.Write(SignedOutput, 0, SignedOutput.Length);
				stream.Flush();


			} catch (Exception ex) {
				SkipSign(compiler, stream, ex, HexWriter);
				return;
			} finally {

				// Clean up temp files:
				foreach (string s in TempFiles) {
					if (File.Exists(s)) File.Delete(s);
				}
				
			}
			
		}

		internal void SkipSign(Compiler compiler, Stream stream, Exception reasonForSkipping, Core.Output.IntelHex hexWriter) {
			compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, "Couldn't sign application: " + reasonForSkipping.Message));
			hexWriter.WriteOutput(compiler, stream);
		}


	}
}
