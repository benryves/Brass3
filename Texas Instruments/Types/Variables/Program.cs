using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TexasInstruments.Types.Variables {
	public class Program : Variable {

		private string name = "";
		/// <summary>
		/// Gets or sets the name of the program.
		/// </summary>
		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}

		private byte[] data = new byte[] { };
		/// <summary>
		/// Gets or sets the data of the program.
		/// </summary>
		public byte[] Data {
			get { return this.data; }
			set { this.data = value; }
		}

		public override byte[] RawHeader {
			get { 
				byte[] Result = Encoding.ASCII.GetBytes("\0" + this.Name.PadRight(9, '\0').Remove(8)); 
				Result[0] = 6;
				return Result;
			}
		}

		public override byte[] RawData {
			get {
				byte[] Result = new byte[this.data.Length + 2];
				Array.Copy(this.data, 0, Result, 2, this.data.Length);
				Result[0] = (byte)this.data.Length;
				Result[1] = (byte)(this.data.Length >> 8);
				return Result;
			}
		}

	}
}
