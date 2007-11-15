using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Defines output data.
	/// </summary>
	/// <remarks>Output data is later turned into binaries by an <see cref="Plugins.IOutputWriter"/> plugin.</remarks>
	public struct OutputData : IComparable {

		/// <summary>
		/// Gets the program counter value that the output data starts at.
		/// </summary>
		public readonly int ProgramCounter;

		/// <summary>
		/// Gets the output counter value that the output data starts at.
		/// </summary>
		public readonly int OutputCounter;

		/// <summary>
		/// Gets the page number that the output data resides on.
		/// </summary>
		public readonly int Page;

		/// <summary>
		/// Gets the raw data that this structure represents.
		/// </summary>
		public readonly byte[] Data;

		/// <summary>
		/// Gets the source statement that generated the output data.
		/// </summary>
		public readonly Compiler.SourceStatement Source;

		/// <summary>
		/// Creates an instance of the <see cref="OutputData"/> structure.
		/// </summary>
		/// <param name="source">The <see cref="Compiler.SourceStatement"/> that generated this output.</param>
		/// <param name="page">The page number that the output data resides on.</param>
		/// <param name="programCounter">The program counter that the output data starts at.</param>
		/// <param name="outputCounter">The output counter that the output data starts at.</param>
		/// <param name="data">The raw data that the structure is to represent.</param>
		public OutputData(Compiler.SourceStatement source, int page, int programCounter, int outputCounter, byte[] data) {
			this.Source = source;
			this.ProgramCounter = programCounter;
			this.OutputCounter = outputCounter;
			this.Page = page;
			this.Data = data;
		}


		/// <summary>
		/// Compares two <see cref="OutputData"/> structures for the purpose of sorting.
		/// </summary>
		/// <param name="obj">The other structure to compare against.</param>
		/// <remarks>Data is ordered by page then by output counter.</remarks>
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