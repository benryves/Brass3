using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Defines output data.
	/// </summary>
	public struct OutputData : IComparable {

		public readonly int ProgramCounter;
		public readonly int OutputCounter;

		public readonly int Page;
		public readonly byte[] Data;
		public readonly Compiler.SourceStatement Source;

		public OutputData(Compiler.SourceStatement source, int page, int programCounter, int outputCounter, byte[] data) {
			this.Source = source;
			this.ProgramCounter = programCounter;
			this.OutputCounter = outputCounter;
			this.Page = page;
			this.Data = data;
		}


		public int CompareTo(object obj) {
			OutputData other = (OutputData)obj;
			if (this.Page != other.Page) {
				return this.Page.CompareTo(other.Page);
			} else {
				return this.OutputCounter.CompareTo(other.OutputCounter);
			}
		}
	}
}