using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {

		private int outputCounter;
		/// <summary>
		/// Gets or sets the current output counter.
		/// </summary>
		public int OutputCounter {
			get { return this.outputCounter; }
			set { this.outputCounter = value; }
		}

		/// <summary>
		/// Increment the program counter and output counters by an amount.
		/// </summary>
		/// <param name="amount">The amount to increment the counters by.</param>
		public void IncrementProgramAndOutputCounters(int amount) {
			this.outputCounter += amount;
			this.Labels.ProgramCounter.Value += amount;
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

			this.output.Add(new OutputData(this.statements[CurrentStatement - 1], this.labels.ProgramCounter.Page, (int)this.labels.ProgramCounter.Value, this.outputCounter, TranslatedData));

			++this.Labels.ProgramCounter.Value;
			//this.OutputCounter += TranslatedData.Length; //TODO: Check this.
			++this.OutputCounter;
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

	}
}
