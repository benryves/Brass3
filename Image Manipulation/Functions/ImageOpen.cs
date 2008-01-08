using System;
using System.Collections.Generic;
using System.Text;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace ImageManipulation {

	[Syntax("handle = fopen(\"filename\")")]
	[Description("Opens an image from a file and returns the image handle for subsequent image operations.")]
	[Category("Image Manipulation")]
	public class ImgOpen : IFunction {

		internal Dictionary<double, ImageManipulator> OpenedImages;
		
		internal ImageManipulator GetImage(double handle) {
			ImageManipulator Result;
			if (this.OpenedImages.TryGetValue(handle, out Result)) return Result;
			throw new ArgumentException("Invalid handle '" + handle + "'.");
		}


		private double CurrentHandle = 0;

		public ImgOpen(Compiler c) {
			this.OpenedImages = new Dictionary<double, ImageManipulator>();
			c.CompilationBegun += delegate(object sender, EventArgs e) {
				this.CurrentHandle = 0;
				this.OpenedImages.Clear();
			};
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			string Filename = compiler.ResolveFilename(source.GetExpressionStringConstant(compiler, source.GetCommaDelimitedArguments(0, 1)[0], false));
			double ImageHandle = ++CurrentHandle;
			this.OpenedImages.Add(ImageHandle, new ImageManipulator(Filename));
			return new Label(compiler.Labels, ImageHandle);
		}

		public Label Create(Compiler compiler, int width, int height) {
			double ImageHandle = ++CurrentHandle;
			this.OpenedImages.Add(ImageHandle, new ImageManipulator(width, height));
			return new Label(compiler.Labels, ImageHandle);
		}

	}
	
}
