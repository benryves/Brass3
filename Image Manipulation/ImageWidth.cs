using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace ImageManipulation {
	[Syntax("imgwidth(handle)")]
	[Description("Gets the width of an image from its handle.")]
	[Category("Image Manipulation")]
	[SeeAlso(typeof(ImageHeight))]
	public class ImageWidth : IFunction {

		public string Name { get { return Names[0]; } }
		public string[] Names { get { return new string[] { "imgwidth" }; } }

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels,
				((ImageOpen)compiler.GetPluginInstanceFromType(typeof(ImageOpen))).GetImage(
					source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(0, 1)[0]).NumericValue
				).Width
			);
		}		

	}
	
}
