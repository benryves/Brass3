using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;

namespace Core.StringEncoding {
	[Description("Provides an encoder for the UTF-16 format.")]
	[Remarks("This encoding works with the compiler's little or big endian setting.")]
	[SeeAlso(typeof(UTF7))]
	[SeeAlso(typeof(UTF8))]
	[SeeAlso(typeof(UTF32))]
	[SeeAlso(typeof(Core.Directives.Endian))]
	[CodeExample("Endianness", ".stringencoder utf16\r\n\r\n.little \r\n.db \"abc \\u266B\"\r\n; Outputs $61, $00, $62, $00, $63, $00, $20, $00, $6B, $26\r\n\r\n\r\n.big\r\n.db \"abc \\u266B\"\r\n; Outputs $00, $61, $00, $62, $00, $63, $00, $20, $26, $6B")]
	[Category("Unicode")]
	public class UTF16 : IStringEncoder {
		
		private readonly Compiler Compiler;

		public byte[] GetData(string toEncode) {
			switch (this.Compiler.Endianness) {
				case Endianness.Little:
					return Encoding.Unicode.GetBytes(toEncode);
				case Endianness.Big:
					return Encoding.BigEndianUnicode.GetBytes(toEncode);
				default:
					throw new InvalidOperationException();
			}
			
		}
		public string Name {
			get { return "utf16"; }
		}
		public UTF16(Compiler c) {
			this.Compiler = c;
		}
	}
}
