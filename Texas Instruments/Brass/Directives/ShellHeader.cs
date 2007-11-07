using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace TexasInstruments.Brass.Directives {

	[PluginName("ionheader"), PluginName("mirageosheader"), PluginName("venusheader")]
	[Syntax(".ionheader \"Description\"")]
	[Syntax(".mirageosheader \"Description\" [, \"Icon.gif\"]")]
	[Syntax(".venusheader \"Description\" [, \"Icon.gif\"]")]
	[Description("Generates a header for popular calculator shells.")]
	public class ShellHeader : IDirective {


		enum ShellType {
			Ion,
			MirageOS,
			Venus,
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			// Determine current machine type.
			bool Is83Plus = false;

		

			int HeaderSize = 0;

			ShellType Shell = ShellType.Ion;
			switch (directive) {
				case "ionheader":
					Shell = ShellType.Ion;
					HeaderSize = 4;
					break;
				case "mirageosheader":
					Shell = ShellType.MirageOS;
					HeaderSize = 33;
					break;
				case "venusheader":
					Shell = ShellType.Venus;
					HeaderSize = 42;
					break;
			}

			TokenisedSource.ArgumentType[] Arguments = Shell == ShellType.Ion ?
				TokenisedSource.StringArgument :
				new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.String, TokenisedSource.ArgumentType.Filename | TokenisedSource.ArgumentType.Optional };

			object[] ParsedArguments = source.GetCommaDelimitedArguments(compiler, index + 1, Arguments);

			if (Shell == ShellType.Venus) {
				if (ParsedArguments.Length < 2) HeaderSize -= 32; // No icon. :(
			}

			if (compiler.OutputWriter.GetType() == typeof(TexasInstruments.Brass.Output.TI8X)) {
				Is83Plus = true;
			} else if (!(compiler.OutputWriter is TexasInstruments.Brass.Output.TI83)) {
				compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new DirectiveArgumentException(source, "Output writer is neither TI83 nor TI8X; assuming TI-83.")));
			}

			if (compiler.CurrentPass == AssemblyPass.Pass2) {
				if (Is83Plus) {
					if (compiler.GetOutputDataOnPage(compiler.Labels.ProgramCounter.Page).Length != 2) {
						compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new DirectiveArgumentException(source, "Invalid data before the the header.")));
					}
				} else {
					if (compiler.GetOutputDataOnPage(compiler.Labels.ProgramCounter.Page).Length > 0) {
						compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new DirectiveArgumentException(source, "Data has already been output before the the header.")));
					}
				}
			}


			byte[] ProgramName = compiler.StringEncoder.GetData(ParsedArguments[0] as string);

			/* 
			 * Ion:
			 * ret \ jr nc,+         // 3 bytes.
			 * .db "Description", 0  // Length of description + 1 bytes.
			 * 
			 * MirageOS:
			 * ret \ .db 1           // 2 bytes.
			 * .db [icon]            // 30 bytes.
			 * .db "Description", 0  // Length of description + 1 bytes.
			 * 
			 * Venus:
			 * .db $E7,$39,$5F,$5B
			 * .db $56,$3F,$00       // 7 bytes.
			 * jr nc,+               // 2 bytes.
			 * .db "Description", 0  // Length of description + 1 bytes.
			 * .db [icon]            // 32 bytes.
			 */

			switch (compiler.CurrentPass) {
				case AssemblyPass.Pass1:
					compiler.IncrementProgramAndOutputCounters(ProgramName.Length + HeaderSize);
					break;
				case AssemblyPass.Pass2:
					switch (Shell) {
						case ShellType.Ion: {
								compiler.WriteOutput((byte)Functions.BCall.Z80Instruction.Ret);
								compiler.WriteOutput((byte)Functions.BCall.Z80Instruction.JrNC);
								compiler.WriteOutput((byte)(ProgramName.Length + 1));
								compiler.WriteOutput(ProgramName);
								compiler.WriteOutput((byte)0x00);
							} break;
						case ShellType.MirageOS: {
								if (!Is83Plus) compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new CompilerExpection(source, "MirageOS header is only valid with the TI-83 Plus output writer.")));
								compiler.WriteOutput((byte)Functions.BCall.Z80Instruction.Ret);
								compiler.WriteOutput((byte)0x01);
								byte[] Icon = new byte[30];
								if (ParsedArguments.Length == 2) Icon = GetIcon(ParsedArguments[1] as string, 15);
								compiler.WriteOutput(Icon);
								compiler.WriteOutput(ProgramName);
								compiler.WriteOutput((byte)0x00);
							} break;
						case ShellType.Venus: {
								if (Is83Plus) compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new CompilerExpection(source, "Venus header is only valid with the TI-83 output writer.")));
								compiler.WriteOutput(new byte[] { 0xE7, 0x39, 0x5F, 0x5B, 0x56, 0x3F, 0x00 });
								compiler.WriteOutput((byte)Functions.BCall.Z80Instruction.JrNC);
								compiler.WriteOutput((byte)(ProgramName.Length + (HeaderSize - 9)));
								compiler.WriteOutput(ProgramName);
								compiler.WriteOutput((byte)0x00);
								if (ParsedArguments.Length == 2) {
									byte[] Icon = GetIcon(ParsedArguments[1] as string, 16);
									compiler.WriteOutput(Icon);
								}
							} break;
					}					
					break;
			}			
		}

		private byte[] GetIcon(string iconName, int size) {

			byte[] Icon = new byte[2 * size];

			using (Bitmap Source = new Bitmap(iconName)) {
				using (Bitmap Dest = new Bitmap(16, size)) {
					using (Graphics Blitter = Graphics.FromImage(Dest)) {
						Blitter.Clear(Color.White);
						Blitter.PixelOffsetMode = PixelOffsetMode.Half;
						Blitter.DrawImageUnscaled(Source, 0, 0);
					}
					BitmapData DestData = Dest.LockBits(new Rectangle(0, 0, 16, size), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
					int[] DestDataPixels = new int[16 * size];
					Marshal.Copy(DestData.Scan0, DestDataPixels, 0, DestDataPixels.Length);
					for (int IconPtr = 0, SourcePtr = 0, y = 0; y < size; ++y, SourcePtr+=(16-size), IconPtr += 2) {
						for (int x = 0; x < size; ++x, ++SourcePtr) {
							if (((DestDataPixels[SourcePtr] & 0xFF) + ((DestDataPixels[SourcePtr] >> 8) & 0xFF) + ((DestDataPixels[SourcePtr] >> 16) & 0xFF)) < 127 * 3) {
								Icon[IconPtr + (x / 8)] |= (byte)(0x80 >> (x & 7));
							}
						}
					}
				}
			}

			return Icon;

		}

	}
}
