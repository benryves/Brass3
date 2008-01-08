using System;
using System.Collections.Generic;

namespace BeeDevelopment.Brass3 {
	public partial class Compiler {

		#region Types


		/// <summary>
		/// Defines an abstract base class representing output data.
		/// </summary>
		/// <remarks>Output data are later turned into binaries by an <see cref="Plugins.IOutputWriter"/> plugin.</remarks>
		public abstract class OutputData : IComparable {

			#region Properties

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
			/// Gets the endianness of the compiler at the point the data was written.
			/// </summary>
			public Endianness Endianess { get; private set; }

			#endregion

			#region Constructor

			/// <summary>
			/// Creates an instance of the <see cref="OutputData"/> class.
			/// </summary>
			/// <param name="source">The <see cref="Compiler.SourceStatement"/> that generated this output.</param>
			/// <param name="data">The raw data that the class is to represent.</param>
			/// <param name="background">True if the data can be overwritten.</param>
			public OutputData(Compiler.SourceStatement source, byte[] data, bool background) {
				this.Source = source;
				this.ProgramCounter = (int)source.Compiler.Labels.ProgramCounter.NumericValue;
				this.OutputCounter = (int)source.Compiler.Labels.OutputCounter.NumericValue;
				this.Page = (int)source.Compiler.Labels.OutputCounter.Page;
				this.Data = data;
				this.Background = background;
				this.StoredData = new Dictionary<Type, object>();
				this.Endianess = source.Compiler.Endianness;
			}

			#endregion

			#region Private Fields

			/// <summary>
			/// Stores data (for example, state information for external plugins).
			/// </summary>
			private Dictionary<Type, object> StoredData;

			#endregion

			#region Public Methods

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

			/// <summary>
			/// Gets data stored in the <see cref="OutputData"/> object by an external plugin.
			/// </summary>
			/// <param name="storingPluginType">The <see cref="Type"/> of the plugin that stored the data.</param>
			/// <returns>The stored data, or null if no stored data could be found.</returns>
			public object GetStoredData(Type storingPluginType) {
				object Output;
				return (this.StoredData.TryGetValue(storingPluginType, out Output)) ? Output : null;
			}

			/// <summary>
			/// Sets data stored in the <see cref="OutputData"/> object by an external plugin.
			/// </summary>
			/// <param name="storingPluginType">The <see cref="Type"/> of the plugin that is storing the data.</param>
			/// <param name="dataToStore">The data to store.</param>
			public void SetStoredData(Type storingPluginType, object dataToStore) {
				if (this.StoredData.ContainsKey(storingPluginType)) this.StoredData.Remove(storingPluginType);
				this.StoredData.Add(storingPluginType, dataToStore);
			}

			#endregion

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
			public StaticOutputData(Compiler.SourceStatement source, byte[] data, bool background)
				: base(source, data, background) {
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
			/// Gets the number of statements compiled up to this point.
			/// </summary>
			internal int CompiledStatements { get; private set; }

			/// <summary>
			/// Creates an instance of the <see cref="DynamicOutputData"/> class.
			/// </summary>
			/// <param name="source">The <see cref="Compiler.SourceStatement"/> that generated this output.</param>
			/// <param name="page">The page number that the output data resides on.</param>
			/// <param name="programCounter">The program counter that the output data starts at.</param>
			/// <param name="outputCounter">The output counter that the output data starts at.</param>
			/// <param name="dataSize">The size of the dynamic data.</param>
			/// <param name="background">True if the data can be overwritten.</param>
			public DynamicOutputData(Compiler.SourceStatement source, int dataSize, DynamicDataGenerator generator, bool background)
				: base(source, new byte[dataSize], background) {
				this.Generator = generator;
				this.Module = source.Compiler.Labels.CurrentModule;
				this.CompiledStatements = source.Compiler.compiledStatements;
			}

		}


		#endregion

		#region Events

		/// <summary>
		/// Defines data associated with events related to <see cref="OutputData"/>.
		/// </summary>
		public class OutputDataEventArgs : EventArgs {
			/// <summary>
			/// Gets the <see cref="OutputData"/> object that this event refers to.
			/// </summary>
			public OutputData Data { get; private set; }

			/// <summary>
			/// Creates an instance of the <see cref="OutputDataEventArgs"/> class.
			/// </summary>
			/// <param name="data">The <see cref="OutputData"/> object that this event refers to.</param>
			public OutputDataEventArgs(OutputData data) {
				this.Data = data;				
			}

		}

		/// <summary>
		/// Represents the method that will handle an event that refers to <see cref="OutputData"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="OutputDataEventArgs"/> that contains <see cref="OutputData"/> event data.</param>
		public delegate void OutputDataEventHandler(object sender, OutputDataEventArgs e);

		/// <summary>
		/// Represents the method that will handle the output data written event.
		/// </summary>
		public event OutputDataEventHandler OutputDataWritten;

		/// <summary>
		/// Represents the method that will handle the output data written event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="OutputDataEventArgs"/> that contains <see cref="OutputData"/> event data.</param>
		protected void OnOutputDataWritten(object sender, OutputDataEventArgs e) {
			if (this.OutputDataWritten != null) this.OutputDataWritten(sender, e);
		}

		/// <summary>
		/// Represents the method that will handle the event fired immediately before output data are run through any loaded <see cref="Plugins.IOutputModifier"/> plugins.
		/// </summary>
		public event OutputDataEventHandler BeforeOutputDataModified;

		/// <summary>
		/// Represents the method that will handle the event fired immediately before output data are run through any loaded <see cref="Plugins.IOutputModifier"/> plugins.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="OutputDataEventArgs"/> that contains <see cref="OutputData"/> event data.</param>
		protected void OnBeforeOutputDataModified(object sender, OutputDataEventArgs e) {
			if (this.BeforeOutputDataModified != null) this.BeforeOutputDataModified(sender, e);
		}

		#endregion

		#region Private Fields

		List<OutputData> WorkingOutputData = new List<OutputData>();

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets whether currently output data is sent to the background or foreground.
		/// </summary>
		/// <remarks>Background data can be overwritten by foreground data.</remarks>
		public bool DataWrittenToBackground { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Write an array of <c>byte</c>s of data to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(byte[] data) {

			var Data = new StaticOutputData(this.CurrentStatement.Value, data, this.DataWrittenToBackground);


			if (this.IsAssembling) {
				this.WorkingOutputData.Add(Data);
			} else {
				this.output.Add(Data);
			}

			this.Labels.ProgramCounter.NumericValue += data.Length;
			this.Labels.OutputCounter.NumericValue += data.Length;

			this.OnOutputDataWritten(this, new OutputDataEventArgs(Data));
		}


		/// <summary>
		/// Write a <see cref="Int32"/> to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(int data) {
			byte[] Data;
			switch (this.Endianness) {
				case Endianness.Little:
					Data = new[] { (byte)data, (byte)(data >> 8), (byte)(data >> 16), (byte)(data >> 24) };
					break;
				case Endianness.Big:
					Data = new[] { (byte)(data >> 24), (byte)(data >> 16), (byte)(data >> 8), (byte)data };
					break;
				default:
					throw new InvalidOperationException();
			}
			this.WriteStaticOutput(Data);
		}

		/// <summary>
		/// Write a <see cref="UInt32"/> to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(uint data) {
			this.WriteStaticOutput((int)data);
		}

		/// <summary>
		/// Write a <see cref="Int16"/> to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(short data) {
			byte[] Data;
			switch (this.Endianness) {
				case Endianness.Little:
					Data = new[] { (byte)data, (byte)(data >> 8) };
					break;
				case Endianness.Big:
					Data = new[] { (byte)(data >> 8), (byte)data };
					break;
				default:
					throw new InvalidOperationException();
			}
			this.WriteStaticOutput(Data);
		}

		/// <summary>
		/// Write a <see cref="UInt16"/> to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(ushort data) {
			this.WriteStaticOutput((short)data);
		}

		/// <summary>
		/// Write a <see cref="Byte"/> to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(byte data) {
			this.WriteStaticOutput(new[] { data });
		}

		/// <summary>
		/// Write a <see cref="SByte"/> to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(sbyte data) {
			this.WriteStaticOutput((byte)data);
		}

		/// <summary>
		/// Writes a block of dynamic output data.
		/// </summary>
		/// <param name="dataSize">The size of the dynamic data block.</param>
		/// <param name="generator">The delegate that will be called to populate the dynamic data when required.</param>
		public void WriteDynamicOutput(int dataSize, DynamicOutputData.DynamicDataGenerator generator) {

			if (!this.IsAssembling) throw new Exception(Strings.ErrorDynamicDataCannotBeWrittenOutsideMainPass);
			
			var Data = new DynamicOutputData(this.CurrentStatement.Value, dataSize, generator, this.DataWrittenToBackground);

			this.Labels.ProgramCounter.NumericValue += dataSize;
			this.Labels.OutputCounter.NumericValue += dataSize;

			this.WorkingOutputData.Add(Data);

			this.OnOutputDataWritten(this, new OutputDataEventArgs(Data));
		}

		
		/// <summary>
		/// Increment the program counter and output counters by an amount.
		/// </summary>
		/// <param name="amount">The amount to increment the counters by.</param>
		public void IncrementProgramAndOutputCounters(int amount) {
			this.Labels.ProgramCounter.NumericValue += amount;
			this.Labels.OutputCounter.NumericValue += amount;
		}

		/// <summary>
		/// Writes the empty fill value.
		/// </summary>
		/// <param name="amount">The number of bytes to write.</param>
		public void WriteEmptyFill(int amount) {
			for (int i = 0; i < amount; ++i) { this.WriteStaticOutput(this.EmptyFill); }
		}


		/// <summary>
		/// Try and get an address of a continuous free block of memory.
		/// </summary>
		/// <param name="page">The page to search on.</param>
		/// <param name="size">The size of the continuous block.</param>
		/// <returns>The lowest output address that meets the requirements.</returns>
		public int FindFreeMemoryBlock(int page, int size) {
			List<OutputData> ExistingData = new List<OutputData>(this.GetOutputDataOnPage(page));
			ExistingData.Sort();
			int TestAddress = 0;
			for (int i = 0; i < ExistingData.Count; ++i) {
				if (ExistingData[i].OutputCounter > (TestAddress + size)) break;
				TestAddress = ExistingData[i].OutputCounter + ExistingData[i].Data.Length;
			}
			return TestAddress;
		}

		/// <summary>
		/// Erase all output data on a particular page.
		/// </summary>
		/// <param name="page">The page to clear.</param>
		public void ClearPage(int page) {
			for (int i = 0; i < this.output.Count; ++i) {
				if (this.output[i].Page == page) {
					this.output.RemoveAt(i);
					--i;
				}
			}
		}

		/// <summary>
		/// Gets the total size of data on a particular page.
		/// </summary>
		/// <param name="page">The page to check.</param>
		/// <returns>The total size of data written to the page.</returns>
		public int SizeOfDataOnPage(int page) {
			int Result = 0;
			foreach (OutputData D in this.GetOutputDataOnPage(page)) {
				Result += D.Data.Length;
			}
			return Result;
		}

		/// <summary>
		/// Check if data exists at a particular output address on a page.
		/// </summary>
		/// <param name="page">The page to check.</param>
		/// <param name="address">The address to check.</param>
		/// <returns>True if data exists, false otherwise.</returns>
		public bool DataExists(int page, int address) {
			foreach (OutputData OD in this.output) {
				if (OD.Page == page && OD.OutputCounter == address) return true;
			}
			return false;
		}

		/// <summary>
		/// Removes redundant data (such as overwritten background data) from the output.
		/// </summary>
		public void RemoveRedundantOutputData() {

			this.output.Sort();

			List<OutputData> DataToPurge = new List<OutputData>();
			Dictionary<int, OutputData> CleanPage = new Dictionary<int, OutputData>();

			foreach (int Page in this.GetUniquePageIndices()) {
				CleanPage.Clear();
				OutputData[] DataOnPage = this.GetOutputDataOnPage(Page);

				for (int i = DataOnPage.Length - 1; i >= 0; i--) {
					if (CleanPage.ContainsKey(DataOnPage[i].OutputCounter)) {
						DataToPurge.Add(DataOnPage[i]);
					} else {
						CleanPage.Add(DataOnPage[i].OutputCounter, DataOnPage[i]);
					}
				}

			}

			foreach (OutputData Purging in DataToPurge) {
				this.output.Remove(Purging);
			}

		}

		/// <summary>
		/// Gets the unique page indices in the output data.
		/// </summary>
		public int[] GetUniquePageIndices() {
			//TODO: Raise error if accessed after bad build.
			List<int> PageIndices = new List<int>();
			foreach (OutputData O in this.Output) {
				if (!PageIndices.Contains(O.Page)) PageIndices.Add(O.Page);
			}
			return PageIndices.ToArray();
		}


		/// <summary>
		/// Gets the output data for a particular page.
		/// </summary>
		/// <param name="page">The page index to retrieve data from.</param>
		public OutputData[] GetOutputDataOnPage(int page) {
			//TODO: Raise error if accessed after bad build.
			List<OutputData> Data = new List<OutputData>(this.Output.Length);
			foreach (OutputData O in this.output) {
				if (O.Page == page) Data.Add(O);
			}
			return Data.ToArray();
		}

		#endregion

	}
}
