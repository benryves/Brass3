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

		/// <summary>
		/// Write a <c>byte</c> of data to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteOutput(byte data) {


			byte[] TranslatedData = new byte[] { data };

			foreach (IOutputModifier M in this.outputModifiers) {
				List<byte> NewOutput = new List<byte>(TranslatedData.Length);
				NewOutput.AddRange(M.ModifyOutput(this, data));
				TranslatedData = NewOutput.ToArray();
			}

			this.output.Add(new OutputData(this.statements[CurrentStatement - 1], 
				this.labels.ProgramCounter.Page, (int)this.labels.ProgramCounter.NumericValue,
				(int)this.labels.OutputCounter.NumericValue, TranslatedData));

			++this.Labels.ProgramCounter.NumericValue;
			++this.Labels.OutputCounter.NumericValue;
		}

		/// <summary>
		/// Write an array of <c>byte</c>s of data to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteOutput(byte[] data) {
			foreach (byte b in data) this.WriteOutput(b);
		}

		/// <summary>
		/// Write a <c>short</c> of data to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteOutput(short data) {
			switch (this.Endianness) {
				case Endianness.Little:
					this.WriteOutput((byte)(data));
					this.WriteOutput((byte)(data >> 8));
					break;
				case Endianness.Big:
					this.WriteOutput((byte)(data >> 8));
					this.WriteOutput((byte)(data));
					break;
				default:
					throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Write an <c>int</c> of data to the output.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public void WriteOutput(int data) {
			switch (this.Endianness) {
				case Endianness.Little:
					this.WriteOutput((byte)(data));
					this.WriteOutput((byte)(data >> 8));
					this.WriteOutput((byte)(data >> 16));
					this.WriteOutput((byte)(data >> 24));
					break;
				case Endianness.Big:
					this.WriteOutput((byte)(data >> 24));
					this.WriteOutput((byte)(data >> 16));
					this.WriteOutput((byte)(data >> 8));
					this.WriteOutput((byte)(data));
					break;
				default:
					throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Writes the empty fill value.
		/// </summary>
		/// <param name="amount">The number of bytes to write.</param>
		public void WriteEmptyFill(int amount) {
			for (int i = 0; i < amount; ++i) { this.WriteOutput(this.EmptyFill); }
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

	}
}
