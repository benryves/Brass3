using System;
using System.Collections.Generic;
using System.Text;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;

namespace Core.StringEncoding {
	[Description("Provides an encoder for the UTF-32 format.")]
	[Remarks("This encoding works with the compiler's little or big endian setting.")]
	[SeeAlso(typeof(UTF7))]
	[SeeAlso(typeof(UTF8))]
	[SeeAlso(typeof(UTF16))]
	[SeeAlso(typeof(Core.Directives.Endian))]
	[Category("Unicode")]
	public class UTF32 : IStringEncoder {
		
		private readonly Compiler Compiler;

		public byte[] GetData(string toEncode) {
			switch (this.Compiler.Endianness) {
				case Endianness.Little:
					return Encoding.UTF32.GetBytes(toEncode);
				case Endianness.Big:
					byte[] ToFlip = Encoding.UTF32.GetBytes(toEncode);
					byte[] Destination = new byte[ToFlip.Length];
					for (int i = 0; i < ToFlip.Length; i += 4) {
						for (int j0 = 0, j1 = 3; j0 < 4; ++j0, --j1) {
							Destination[i + j0] = ToFlip[i + j1];
						}
					}
					return ToFlip;
				default:
					throw new InvalidOperationException();
			}
			
		}
		public string Name {
			get { return "utf32"; }
		}
		public UTF32(Compiler c) {
			this.Compiler = c;
		}
	}
}
