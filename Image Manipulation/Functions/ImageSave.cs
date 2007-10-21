using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;

namespace ImageManipulation {


	[Category("Image Manipulation")]
	[Description("Saves an image to a file.")]
	[Syntax("imgsave(handle, filename [, format])")]
	[Remarks(@"If no format is specified, the format is guessed from the extension of the file, and defaults to PNG.
Valid formats are as follows:
<table>
	<tr>
		<th>PNG</th>
		<td>The W3C Portable Network Graphics (PNG) image format.</td>
	</tr>
	<tr>
		<th>GIF</th>
		<td>The Graphics Interchange Format (GIF) image format.</td>
	</tr>
	<tr>
		<th>BMP</th>
		<td>The bitmap image format (BMP).</td>
	</tr>
	<tr>
		<th>JPG/JPEG</th>
		<td>The Joint Photographic Experts Group (JPEG) image format.</td>
	</tr>
</table>")]
	[SatisfiesAssignmentRequirement(true)]
	public class ImgSave : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			// Get arguments:
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.Value,
				TokenisedSource.ArgumentType.Filename,
				TokenisedSource.ArgumentType.String | TokenisedSource.ArgumentType.SingleToken | TokenisedSource.ArgumentType.Optional
			});

			ImageManipulator Image = compiler.GetPluginInstanceFromType<ImgOpen>().GetImage((double)Args[0]);

			ImageFormat SaveFormat = ImageFormat.Png;

			string FormatExtension = (Args.Length > 2 ? (Args[2] as string) : Path.GetExtension(Args[1] as string).TrimStart('.'));

			switch (FormatExtension.ToLowerInvariant()) {
				case "png":
					SaveFormat = ImageFormat.Png;
					break;
				case "bmp":
					SaveFormat = ImageFormat.Bmp;
					break;
				case  "gif":
					SaveFormat = ImageFormat.Gif;
					break;
				case "jpg":
				case "jpeg":
					SaveFormat = ImageFormat.Jpeg;
					break;
				default:
					if (Args.Length > 2) throw new DirectiveArgumentException(source, "Image format '" + FormatExtension + "' not recognised.");
					break;
			}
			
			Image.Save(Args[1] as string, SaveFormat);

			return new Label(compiler.Labels, double.NaN);

		}
	}
	
}
