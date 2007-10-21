using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageManipulation {
	
	/// <summary>
	/// Provides a class to manipulate images with.
	/// </summary>
	class ImageManipulator {

		private readonly int[] Data;

		private readonly int width;
		/// <summary>
		/// Gets the width of the image.
		/// </summary>
		public int Width { get { return this.width; } }

		private readonly int height;
		/// <summary>
		/// Gets the height of the image.
		/// </summary>
		public int Height { get { return this.height; } }

		private readonly int StrideWidth;

		/// <summary>
		/// Creates an instance of the <see cref="ImageManipulator"/> class from a file.
		/// </summary>
		/// <param name="filename">The name of the file to load.</param>
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

		/// <summary>
		/// Creates an instance of the <see cref="ImageManipulator"/> class from a width and height.
		/// </summary>
		public ImageManipulator(int width, int height) {
			using (Bitmap B = new Bitmap(width, height)) {
				this.width = B.Width;
				this.height = B.Height;
				BitmapData BD = B.LockBits(new Rectangle(0, 0, this.Width, this.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				this.StrideWidth = BD.Stride / 4;
				this.Data = new int[BD.Height * this.StrideWidth];
				Marshal.Copy(BD.Scan0, this.Data, 0, this.Data.Length);
				B.UnlockBits(BD);
			}
		}

		/// <summary>
		/// Save the image in a particular format.
		/// </summary>
		/// <param name="filename">The filename to save to.</param>
		/// <param name="format">The format of the image.</param>
		public void Save(string filename, ImageFormat format) {
			using (Bitmap B = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb)) {
				BitmapData BD = B.LockBits(new Rectangle(0, 0, this.Width, this.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
				Marshal.Copy(this.Data, 0, BD.Scan0, this.Data.Length);
				B.UnlockBits(BD);
				B.Save(filename, format);
			}
		}

		/// <summary>
		/// Save the image as a PNG.
		/// </summary>
		/// <param name="filename">The filename to save to.</param>
		public void Save(string filename) {
			this.Save(filename, ImageFormat.Png);
		}

		/// <summary>
		/// Gets a pixel at a particular position.
		/// </summary>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		/// <returns>The pixel data as a 32-bit ARGB value.</returns>
		public int GetPixel(int x, int y) {
			while (x < 0) x += this.Width;
			x %= this.Width;
			while (y < 0) y += this.Height;
			y %= this.Height;
			return this.Data[x + y * this.StrideWidth];
		}

		/// <summary>
		/// Gets a pixel at a particular position in a particular format.
		/// </summary>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		/// <param name="format">Format string representing the format to get the pixel in.</param>
		/// <returns>The pixel in the format specified.</returns>
		public double GetPixel(int x, int y, string format) {
			int Source = this.GetPixel(x, y);
			PixelFormatString Format = new PixelFormatString(format);
			return Format.FromArgb(Source);
		}


		/// <summary>
		/// Sets a pixel at a particular position.
		/// </summary>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		/// <param name="value">The pixel data as a 32-bit ARGB value.</param>
		public void SetPixel(int x, int y, int value) {
			while (x < 0) x += this.Width;
			x %= this.Width;
			while (y < 0) y += this.Height;
			y %= this.Height;
			this.Data[x + y * this.StrideWidth] = value;
		}

		/// <summary>
		/// Sets a pixel at a particular position.
		/// </summary>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		/// <param name="format">Format string representing the format to set the pixel in.</param>
		/// <param name="value">The pixel in the format specified.</param>
		public void SetPixel(int x, int y, string format, double value) {
			PixelFormatString Format = new PixelFormatString(format);
			this.SetPixel(x, y, Format.ToArgb(value));
		}

		private class PixelFormatString {

			private readonly KeyValuePair<char, int>[] DecodedFormatForwards;
			private readonly KeyValuePair<char, int>[] DecodedFormatBackwards;

			public double FromArgb(int argb) {

				double Result = 0;


				foreach (KeyValuePair<char, int> Component in DecodedFormatBackwards) {

					Result *= Math.Pow(2, Component.Value);

					double ComponentValue = 0;
					switch (Component.Key) {
						case 'a':
							ComponentValue = (argb >> 24) & 0xFF;
							break;
						case 'r':
							ComponentValue = (argb >> 16) & 0xFF;
							break;
						case 'g':
							ComponentValue = (argb >> 8) & 0xFF;
							break;
						case 'b':
							ComponentValue = (argb >> 0) & 0xFF;
							break;
						case 'l':
							ComponentValue = (double)((((argb >> 0) & 0xFF) + ((argb >> 8) & 0xFF) + ((argb >> 16) & 0xFF)) / 3d) * (double)((argb >> 24) & 0xFF) / 256d;
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

			public int ToArgb(double value) {
				int a = 255, r = 0, g = 0, b = 0;

				foreach (KeyValuePair<char, int> Component in DecodedFormatForwards) {
					
					int ComponentValue = ((int)value) & ((int)Math.Pow(2d, Component.Value) - 1);

					if (Component.Value > 8) {
						ComponentValue >>= (Component.Value - 8);
					} else {
						ComponentValue <<= (8 - Component.Value);
					}

					ComponentValue = Math.Min(255, Math.Max(0, ComponentValue));

					switch (Component.Key) {
						case 'a':
							a = ComponentValue;
							break;
						case 'r':
							r = ComponentValue;
							break;
						case 'g':
							g = ComponentValue;
							break;
						case 'b':
							b = ComponentValue;
							break;
					}

					value = Math.Truncate(value / Math.Pow(2d, Component.Value));
				}

				return (a << 24) | (r << 16) | (g << 8) | (b << 0);

			}

			public PixelFormatString(string formatString) {

				List<KeyValuePair<char, int>> DecodedFormat = new List<KeyValuePair<char, int>>();
				foreach (char c in formatString.ToLowerInvariant()) {
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

				this.DecodedFormatForwards = DecodedFormat.ToArray();

				DecodedFormat.Reverse();

				this.DecodedFormatBackwards = DecodedFormat.ToArray();

			}

		}

	}
}
