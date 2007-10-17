using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Utility {
	public class ZipWriter : IDisposable {

		private readonly Stream BaseStream;

		#region Constructors

		public ZipWriter(Stream stream) {
			this.BaseStream = stream;
		}

		public ZipWriter(string filename) {
			this.BaseStream = File.OpenRead(filename);
		}

		#endregion

		#region Methods

		public void Flush() {

		}

		private void WriteHeader() {

		}

		#endregion


		#region IDisposable Members

		public void Dispose() {
			if (this.BaseStream != null) {
				this.BaseStream.Flush();
			}
		}

		#endregion
	}
}
