using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;

namespace Core.StringEncoding {
	[Description("Provides an encoder for the ASCII (7-bit) character set.")]
	[Remarks("ASCII characters are limited to the lowest 128 Unicode characters, from U+0000 to U+007F.")]
	public class ASCII : IStringEncoder {
		public byte[] GetData(string toEncode) {
			return Encoding.ASCII.GetBytes(toEncode);
		}
		public string Name {
			get { return "ascii"; }
		}
	}
}
