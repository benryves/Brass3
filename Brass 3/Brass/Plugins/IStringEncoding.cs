using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {

	/// <summary>
	/// Defines the interface for plugins with that handle string encoding.
	/// </summary>
	public interface IStringEncoder : IPlugin {

		/// <summary>
		/// Encode a string using the particular string encoding.
		/// </summary>
		byte[] GetData(string toEncode);

	}


}
