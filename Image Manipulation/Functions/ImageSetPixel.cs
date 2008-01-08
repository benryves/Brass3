using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace ImageManipulation {

	[Category("Image Manipulation")]
	[SatisfiesAssignmentRequirement(true)]
	public class ImgSetPixel : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			// Get arguments:
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.Value,
				TokenisedSource.ArgumentType.Value,
				TokenisedSource.ArgumentType.Value,
				TokenisedSource.ArgumentType.String | TokenisedSource.ArgumentType.SingleToken,
				TokenisedSource.ArgumentType.Value,
			});

			ImageManipulator Image = compiler.GetPluginInstanceFromType<ImgOpen>().GetImage((double)Args[0]);

			Image.SetPixel((int)(double)Args[1], (int)(double)Args[2], (string)Args[3], (double)Args[4]);

			return new Label(compiler.Labels, double.NaN);
		}
	}
	
}
