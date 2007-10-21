using System;
using System.Collections.Generic;
using System.Text;
using Brass3.Plugins;
namespace Brass3 {
	class DefaultStringEncoder : IStringEncoder {

		public byte[] GetData(string toEncode) {
			return Encoding.ASCII.GetBytes(toEncode);
		}

		public string GetString(byte[] data) {
			return Encoding.ASCII.GetString(data);
		}

		public char GetChar(int data) {
			return Encoding.ASCII.GetString(new byte[] { (byte)(data) })[0];
		}

	}
}
