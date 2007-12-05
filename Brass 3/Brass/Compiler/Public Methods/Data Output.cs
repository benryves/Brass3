using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Increment the program counter and output counters by an amount.
		/// </summary>
		/// <param name="amount">The amount to increment the counters by.</param>
		public void IncrementProgramAndOutputCounters(int amount) {
			this.Labels.ProgramCounter.NumericValue += amount;
			this.Labels.OutputCounter.NumericValue += amount;
		}

		List<OutputData> WorkingOutputData = new List<OutputData>();

		/// <summary>
		/// Gets or sets whether currently output data is sent to the background or foreground.
		/// </summary>
		/// <remarks>Background data can be overwritten by foreground data.</remarks>
		public bool DataWrittenToBackground { get; set; }

		#region WriteStaticOutput

		/// <summary>
		/// Write an array of <c>byte</c>s of data to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteStaticOutput(byte[] data) {
		
			this.WorkingOutputData.Add(new StaticOutputData(this.CurrentStatement.Value,
				this.labels.ProgramCounter.Page, (int)this.labels.OutputCounter.NumericValue,
				(int)this.labels.OutputCounter.NumericValue, data, this.DataWrittenToBackground));

			this.Labels.ProgramCounter.NumericValue += data.Length;
			this.Labels.OutputCounter.NumericValue += data.Length;

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

		
		#endregion

		#region WriteDynamicOutput

		/// <summary>
		/// Writes a block of dynamic output data.
		/// </summary>
		/// <param name="dataSize">The size of the dynamic data block.</param>
		/// <param name="generator">The delegate that will be called to populate the dynamic data when required.</param>
		public void WriteDynamicOutput(int dataSize, DynamicOutputData.DynamicDataGenerator generator) {
			this.WorkingOutputData.Add(new DynamicOutputData(this.CurrentStatement.Value,
				this.labels.ProgramCounter.Page, (int)this.labels.OutputCounter.NumericValue,
				(int)this.labels.OutputCounter.NumericValue, dataSize, generator, this.DataWrittenToBackground));
			this.Labels.ProgramCounter.NumericValue += dataSize;
			this.Labels.OutputCounter.NumericValue += dataSize;
		}

		#endregion

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
				TestAddress = ExistingData[i].OutputCounter + 1;
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
			Dictionary<int, OutputData> CleanPage = new Dictionary<int,OutputData>();

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

	}
}
