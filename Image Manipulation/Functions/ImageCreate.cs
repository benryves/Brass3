using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace ImageManipulation {

	[Syntax("handle = imgcreate(width, height)")]
	[Category("Image Manipulation")]
	public class ImgCreate : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.Value,
				TokenisedSource.ArgumentType.Value
			});

			return compiler.GetPluginInstanceFromType<ImgOpen>().Create(compiler, (int)(double)Args[0], (int)(double)Args[1]);

		}		

	}
	
}
