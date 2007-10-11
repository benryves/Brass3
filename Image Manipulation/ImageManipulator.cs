using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageManipulation {
	class ImageManipulator {

		private readonly int[] Data;

		private readonly int width;
		public int Width { get { return this.width; } }

		private readonly int height;
		public int Height { get { return this.height; } }

		private readonly int StrideWidth;

		public ImageManipulator(string filename) {
			using (Bitmap B = new Bitmap(filename)) {
				this.width = B.Width;
				this.height = B.Height;
				BitmapData BD = B.LockBits(new Rectangle(0, 0, this.Width, this.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				this.StrideWidth = BD.Stride / 4;
				this.Data = new int[BD.Height * this.StrideWidth];
				Marshal.Copy(BD.Scan0, this.Data, 0, this.Data.Length);
				B.UnlockBits(BD);
			}
		}

		public int GetPixel(int x, int y) {
			x %= this.Width;
			y %= this.Height;
			return this.Data[x + y * this.StrideWidth];
		}

		public double GetPixel(int x, int y, string format) {
			int Source = this.GetPixel(x, y);

			List<KeyValuePair<char, int>> DecodedFormat = new List<KeyValuePair<char, int>>();
			foreach (char c in format.ToLowerInvariant()) {
				switch (c) {
					case 'r':
					case 'g':
					case 'b':
					case 'a':
					case 'l':
						DecodedFormat.Add(new KeyValuePair<char, int>(c, -1));
						break;
					default:
						if (c >= '0' && c <= '9') {
							int NumericValue = (c - '0');
							if (DecodedFormat.Count == 0) throw new FormatException("No colour component to set width.");
							KeyValuePair<char, int> LastFormatCharacter = DecodedFormat[DecodedFormat.Count - 1];
							LastFormatCharacter = new KeyValuePair<char, int>(LastFormatCharacter.Key,
								(LastFormatCharacter.Value == -1 ? 0 : (LastFormatCharacter.Value * 10)) + NumericValue);
							DecodedFormat[DecodedFormat.Count - 1] = LastFormatCharacter;
						} else {
							throw new FormatException("Colour component '" + c + "' not recognised.");
						}
						break;
				}
			}

			if (DecodedFormat.Count == 0) throw new FormatException("No format specified.");

			for (int i = 0; i < DecodedFormat.Count; ++i) {
				if (DecodedFormat[i].Value == -1) {
					DecodedFormat[i] = new KeyValuePair<char, int>(DecodedFormat[i].Key, 8);
				}
			}

			double Result = 0;
			
			DecodedFormat.Reverse();
			
			foreach (KeyValuePair<char, int> Component in DecodedFormat) {

				Result *= Math.Pow(2, Component.Value);

				double ComponentValue = 0;
				switch (Component.Key) {
					case 'a':
						ComponentValue = (Source >> 24) & 0xFF;
						break;
					case 'r':
						ComponentValue = (Source >> 16) & 0xFF;
						break;
					case 'g':
						ComponentValue = (Source >> 8) & 0xFF;
						break;
					case 'b':
						ComponentValue = (Source >> 0) & 0xFF;
						break;
					case 'l':
						ComponentValue = (double)((((Source >> 0) & 0xFF) + ((Source >> 8) & 0xFF) + ((Source >> 16) & 0xFF)) / 3d) * (double)((Source >> 24) & 0xFF) / 256d;
						break;
					default:
						throw new InvalidOperationException();
				}

				// 0 <= ComponentValue < 256

				ComponentValue *= Math.Pow(2, Component.Value - 8);

				Result += (int)ComponentValue;

			}

			return Result;

		}

	}
}
