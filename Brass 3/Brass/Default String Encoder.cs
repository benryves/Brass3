using System;
using System.Collections.Generic;
using System.Text;
using Brass3.Plugins;
namespace Brass3 {
	class DefaultStringEncoder : IStringEncoder {

		public byte[] GetData(string toEncode) {
			return Encoding.ASCII.GetBytes(toEncode);
		}

		public string Name { get { return null; } }
	}
}
