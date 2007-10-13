using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace ImageManipulation {

	[Syntax("imggetpixel(handle, x, y, \"format\")")]
	[Description("Gets a pixel value at a particular position on the image.\r\nThe format string describes how you want to retrieve the data.")]
	[Remarks("If <c>x</c> or <c>y</c> are out of bounds then the coordinates are wrapped (so (-1,-1) refers to the bottom right hand corner of the image).")]
	[CodeExample("; Open the image:\r\nimg = imgopen(\"delete.png\")\r\n\r\n; Loop over each pixel in the image:\r\n.for y = 0, y < imgheight(img), ++y\r\n\t.for x = 0, x < imgwidth(img), ++x\r\n\t\r\n\t\t; Grab the luminosity of each pixel to two bits:\r\n\t\tl = imggetpixel(img, x, y, 'l2');\r\n\t\t\r\n\t\t; Convert to an 'ASCII-art' brightness:\r\n\t\tc = choose(l + 1, ' ', '.', '+', '#');\r\n\t\t.echochar c, c\r\n\t\t\r\n\t.loop\r\n\t.echoln\r\n.loop\r\n\r\n/*\r\n          ....++++....\r\n        ++++########++..\r\n      ++######++++++##++..\r\n    ++++##++++++++++++##++..\r\n  ..++##++++++++++++++++##..\r\n  ..##++++++++++++++++++++++..\r\n  ++##++++############++++++..\r\n  ++##++++############++++++..\r\n  ..++++++++++++++++++++++++..\r\n  ....##++++++++++++++++++..\r\n    ..++++++++++++++++++++..\r\n      ..++++++++++++++++..\r\n        ....++++++++....\r\n            ........\r\n*/")]
	[Category("Image Manipulation")]
	public class ImageGetPixel : IFunction {

		public string Name { get { return Names[0]; } }
		public string[] Names { get { return new string[] { "imggetpixel" }; } }

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 4);

			return new Label(compiler.Labels,
				((ImageOpen)compiler.GetPluginInstanceFromType(typeof(ImageOpen))).GetImage(
					source.EvaluateExpression(compiler, Args[0]).NumericValue
				).GetPixel(
					(int)source.EvaluateExpression(compiler, Args[1]).NumericValue,
					(int)source.EvaluateExpression(compiler, Args[2]).NumericValue,
					source.GetExpressionStringConstant(compiler, Args[3], false)
				)
			);
		}		

	}
	
}
