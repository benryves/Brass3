using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;

namespace Core.StringEncoding {
	[Description("Provides an encoder for the UTF-7 format.")]
	[SeeAlso(typeof(UTF8))]
	[SeeAlso(typeof(UTF16))]
	[SeeAlso(typeof(UTF32))]
	[Category("Unicode")]
	public class UTF7 : IStringEncoder {
		public byte[] GetData(string toEncode) {
			return Encoding.UTF7.GetBytes(toEncode);
		}
		public string GetString(byte[] data) {
			return Encoding.UTF7.GetString(data);
		}
		public char GetChar(int value) {
			return (char)value;
		}
		public string Name {
			get { return "utf7"; }
		}
	}
}
