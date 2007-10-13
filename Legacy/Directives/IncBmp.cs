using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Legacy.Directives {
	public class IncBmp : IDirective {
		public string Name { get { return Names[0]; } }
		public string[] Names { get { return new string[] { "incbmp" }; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			object[] Arguments;
			Label[] Parms;
			compiler.Labels.EnterTemporaryModule();
			try {
				Arguments = source.GetCommaDelimitedArguments(
					compiler,
					index + 1,
					new TokenisedSource.ArgumentType[] {
					TokenisedSource.ArgumentType.Filename,                                      // Filename.
					TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional, // Threshold.
					TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional | TokenisedSource.ArgumentType.ImplicitLabelCreation, // Rle.
					TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional, // Width.
					TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional  // Height.
				}
				);
			} finally {
				Parms = compiler.Labels.LeaveTemporaryModule();
			}

			string BmpFilename = Arguments[0] as string;

			if (!File.Exists(BmpFilename)) throw new DirectiveArgumentException(source, "File '" + BmpFilename + "' not found.");

			using (Bitmap B = new Bitmap(BmpFilename)) {

				bool CanRle = false;

				int BmpWidth = B.Width;
				int BmpHeight = B.Height;

				if (BmpWidth != 0) {

					int BrightnessLimiter = 127;

					foreach (Label L in Parms) {
						switch (LabelCollection.ModuleGetName(L.Name).ToLowerInvariant()) {
							case "width":
								BmpWidth = (int)L.Value;
								break;
							case "height":
								BmpHeight = (int)L.Value;
								break;
							case "rle":
								CanRle = true;
								break;
							default:
								BrightnessLimiter = (int)L.Value;
								break;
						}
					}


					int ByteWidth = 1 + ((BmpWidth - 1) >> 3);
					int RBmpHeight = B.Height;
					int RBmpWidth = B.Width;
					byte[] ToAdd = new byte[BmpHeight * ByteWidth];
					int AddIndex = 0;
					for (int y = 0; y < BmpHeight; ++y) {
						for (int x = 0; x < ByteWidth; ++x) {
							byte Row = 0x00;
							for (int i = 0; i < 8; ++i) {
								Row <<= 1;
								if (i + x * 8 < B.Width) {
									int Pixel = (x >= 0 && x < RBmpWidth && y >= 0 && y < RBmpHeight) ? B.GetPixel(i + x * 8, y).ToArgb() : 0;
									int ComparePixel = Pixel & 0xFF;
									Pixel >>= 8; ComparePixel += Pixel & 0xFF;
									Pixel >>= 8; ComparePixel += Pixel & 0xFF;
									ComparePixel /= 3;
									if (ComparePixel < BrightnessLimiter) {
										Row |= 0x01;
									}
								}
							}
							ToAdd[AddIndex++] = Row;
						}
					}
					//if (CanRle) ToAdd = RLE(ToAdd);
					if (compiler.CurrentPass  ==  AssemblyPass.Pass1) {
						compiler.IncrementProgramAndOutputCounters(ToAdd.Length);
					} else {
						for (int i = 0; i < ToAdd.Length; i++) {
							compiler.WriteOutput(ToAdd[i]);
						}
					}
				}
			}




			Console.WriteLine();

		}
	}
}
