using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace ImageManipulation {
	[Syntax("imgheight(handle)")]
	[Description("Gets the height of an image from its handle.")]
	[Category("Image Manipulation")]
	[SeeAlso(typeof(ImgWidth))]
	public class ImgHeight : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels,
				((ImgOpen)compiler.GetPluginInstanceFromType(typeof(ImgOpen))).GetImage(
					source.EvaluateExpression(compiler,source.GetCommaDelimitedArguments(0, 1)[0]).NumericValue
				).Height
			);
		}		

	}
	
}
