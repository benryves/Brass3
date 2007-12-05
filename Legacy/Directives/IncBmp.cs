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
	[Description("Loads a monochrome image and includes it directly into the output.")]
	[Remarks(
@"This directive can be used to load a monochrome bitmap image (BMP, PNG, GIF, JPEG...) and include it directly into your program.
The bitmap is padded with 0s to make it a multiple of 8 bits wide, and a 1 corresponds to a black pixel.
Any pixel which is darker than the threshold (which defaults to 127) is considered black, any pixel brighter is considered white.
Specifying the flag <c>RLE</c> compresses the data after conversion.
Using the width/height flags forces the bitmap data to a particular size; note that the image is not scaled or repositioned in any way (merely aligned top-left and cropped to the final size).")]
	[CodeExample("; Load test.gif;\r\n; force width to 32 and apply RLE.\r\n.incbmp \"test.gif\", rle, width = 32")]
	[Category("Data")]
	[SeeAlso(typeof(RleMode))]
	public class IncBmp : IDirective {

		public byte RLE_Flag; // = 0x91;
		public bool RLE_ValueFirst;

		/// <summary>
		/// Compress a block of data using RLE (run-length encoding)
		/// </summary>
		/// <param name="data">Data to compress</param>
		/// <returns>Compressed data</returns>
		private byte[] RLE(byte[] data) {
			List<byte> Return = new List<byte>();

			for (int i = 0; i < data.Length; ++i) {
				if (data[i] == RLE_Flag || (i < data.Length - 3 && data[i] == data[i + 1] && data[i] == data[i + 2] && data[i] == data[i + 3])) {
					// We have a run!
					Return.Add(RLE_Flag);

					int DataLengthCount = 0;
					byte CurrentByte = data[i];
					int FinalPosition = Math.Min(i + 0xFF, data.Length);

					for (; i < FinalPosition; ++i) {
						if (data[i] == CurrentByte) {
							++DataLengthCount;
						} else {
							break;
						}
					}
					--i;

					if (RLE_ValueFirst) Return.Add(CurrentByte);
					Return.Add((byte)DataLengthCount);
					if (!RLE_ValueFirst) Return.Add(CurrentByte);
				} else {
					Return.Add(data[i]);
				}
			}

			return Return.ToArray();
		}

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
								BmpWidth = (int)L.NumericValue;
								break;
							case "height":
								BmpHeight = (int)L.NumericValue;
								break;
							case "rle":
								CanRle = true;
								break;
							default:
								BrightnessLimiter = (int)L.NumericValue;
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
					if (CanRle) ToAdd = RLE(ToAdd);

					for (int i = 0; i < ToAdd.Length; i++) {
						compiler.WriteStaticOutput(ToAdd[i]);
					}
				}
			}
		}

		public IncBmp(Compiler c) {
			c.CompilationBegun += delegate(object sender, EventArgs e) {
				RLE_Flag = 0x91;
				RLE_ValueFirst = true;
			};

		}

	}
}
