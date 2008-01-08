using System;
using System.Collections.Generic;
using System.Text;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace ImageManipulation {
	[Syntax("imgwidth(handle)")]
	[Description("Gets the width of an image from its handle.")]
	[Category("Image Manipulation")]
	[SeeAlso(typeof(ImgHeight))]
	public class ImgWidth : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels,
				((ImgOpen)compiler.GetPluginInstanceFromType(typeof(ImgOpen))).GetImage(
					source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(0, 1)[0]).NumericValue
				).Width
			);
		}		

	}
	
}
