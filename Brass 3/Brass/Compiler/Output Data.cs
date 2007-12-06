using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Defines an abstract base class representing output data.
	/// </summary>
	/// <remarks>Output data are later turned into binaries by an <see cref="Plugins.IOutputWriter"/> plugin.</remarks>
	public abstract class OutputData : IComparable {

		/// <summary>
		/// Gets the program counter value that the output data starts at.
		/// </summary>
		public int ProgramCounter { get; private set; }

		/// <summary>
		/// Gets the output counter value that the output data starts at.
		/// </summary>
		public int OutputCounter { get; private set; }

		/// <summary>
		/// Gets the page number that the output data resides on.
		/// </summary>
		public int Page { get; private set; }

		/// <summary>
		/// Gets the raw data that this structure represents.
		/// </summary>
		public byte[] Data { get; set; }

		/// <summary>
		/// Gets the source statement that generated the output data.
		/// </summary>
		public Compiler.SourceStatement Source { get; private set; }

		/// <summary>
		/// Gets whether the output data can be overwritten.
		/// </summary>
		public bool Background { get; private set; }

		/// <summary>
		/// Creates an instance of the <see cref="OutputData"/> class.
		/// </summary>
		/// <param name="source">The <see cref="Compiler.SourceStatement"/> that generated this output.</param>
		/// <param name="page">The page number that the output data resides on.</param>
		/// <param name="programCounter">The program counter that the output data starts at.</param>
		/// <param name="outputCounter">The output counter that the output data starts at.</param>
		/// <param name="data">The raw data that the class is to represent.</param>
		/// <param name="background">True if the data can be overwritten.</param>
		public OutputData(Compiler.SourceStatement source, int page, int programCounter, int outputCounter, byte[] data, bool background) {
			this.Source = source;
			this.ProgramCounter = programCounter;
			this.OutputCounter = outputCounter;
			this.Page = page;
			this.Data = data;
			this.Background = background;
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
			} else if (this.OutputCounter != other.OutputCounter) {
				return this.OutputCounter.CompareTo(other.OutputCounter);
			} else {
				return ((int)(this.Background ? 0 : 1)).CompareTo(other.Background ? 0 : 1);
			}
		}

		/// <summary>
		/// Returns a formatted string describing the data.
		/// </summary>
		public override string ToString() {
			string s = string.Format("{0}:{1} {2}", this.Page, this.OutputCounter, (this.Source == null ? "?" : this.Source.ToString()));
			if (this.Background) {
				s = "(" + s + ")";
			}
			return s;
		}
	}



	/// <summary>
	/// Defines static output data.
	/// </summary>
	public class StaticOutputData : OutputData {

		/// <summary>
		/// Creates an instance of the <see cref="StaticOutputData"/> class.
		/// </summary>
		/// <param name="source">The <see cref="Compiler.SourceStatement"/> that generated this output.</param>
		/// <param name="page">The page number that the output data resides on.</param>
		/// <param name="programCounter">The program counter that the output data starts at.</param>
		/// <param name="outputCounter">The output counter that the output data starts at.</param>
		/// <param name="data">The raw data that the class is to represent.</param>
		/// <param name="background">True if the data can be overwritten.</param>
		public StaticOutputData(Compiler.SourceStatement source, int page, int programCounter, int outputCounter, byte[] data, bool background)
			: base(source, page, programCounter, outputCounter, data, background) {
		}

	}

	/// <summary>
	/// Defines dynamic output data.
	/// </summary>
	public class DynamicOutputData : OutputData {

		/// <summary>
		/// Defines a delegate called to populate the dynamic data.
		/// </summary>
		/// <param name="data">The <see cref="DynamicOutputData"/> object that needs its dynamic data populated.</param>
		public delegate void DynamicDataGenerator(DynamicOutputData data);

		/// <summary>
		/// Gets the <see cref="DynamicDataGenerator"/> used to populate this <see cref="DynamicOutputData"/> object's dynamic data.
		/// </summary>
		public DynamicDataGenerator Generator { get; private set; }

		/// <summary>
		/// Gets the module that the output data was created in.
		/// </summary>
		public string Module { get; private set; }

		/// <summary>
		/// Creates an instance of the <see cref="DynamicOutputData"/> class.
		/// </summary>
		/// <param name="source">The <see cref="Compiler.SourceStatement"/> that generated this output.</param>
		/// <param name="page">The page number that the output data resides on.</param>
		/// <param name="programCounter">The program counter that the output data starts at.</param>
		/// <param name="outputCounter">The output counter that the output data starts at.</param>
		/// <param name="dataSize">The size of the dynamic data.</param>
		/// <param name="background">True if the data can be overwritten.</param>
		public DynamicOutputData(Compiler.SourceStatement source, int page, int programCounter, int outputCounter, int dataSize, DynamicDataGenerator generator, bool background)
			: base(source, page, programCounter, outputCounter, new byte[dataSize], background) {
			this.Generator = generator;
			this.Module = source.Compiler.Labels.CurrentModule;
		}

	}

}