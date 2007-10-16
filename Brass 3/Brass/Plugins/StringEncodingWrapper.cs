using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Plugins {
	public class StringEncodingWrapper : IStringEncoder {

		private readonly Compiler compiler;
		public Compiler Compiler { get { return this.compiler; } }

		private readonly string name;
		public string Name { get { return this.name; } }

		private readonly Encoding encoding;
		public Encoding Encoding { get { return this.encoding; } }

		public StringEncodingWrapper(Compiler compiler, string name, Encoding encoding) {
			this.compiler = compiler;
			this.name = name;
			this.encoding = encoding;
		}


		public virtual byte[] GetData(string toEncode) {
			return this.encoding.GetBytes(toEncode);
		}

		public virtual string GetString(byte[] data) {
			return this.Encoding.GetString(data);
		}

		public virtual char GetChar(int value) {
			if (this.encoding.IsSingleByte) {
				return this.encoding.GetString(new byte[] { (byte)value })[0];
			} else {
				List<byte> Character = new List<byte>(new byte[] { 
					(byte)(value),
					(byte)(value >> 8),
					(byte)(value >> 16),
					(byte)(value >> 24),
				});
				if (this.compiler.Endianness == Endianness.Big) {
					Character.Reverse();
				}
				string Test = null;
				while (Test == null || (Test.Length > 1 && Character.Count > 0)) {
					Test = this.Encoding.GetString(Character.ToArray());
					Character.RemoveAt(compiler.Endianness == Endianness.Little ? 0 : Character.Count - 1);					
				}
				if (Test.Length == 1) return Test[0];
				return '?';
				
			}
		}


	}
}
