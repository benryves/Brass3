using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using System.ComponentModel;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Output {

	[Description("Writes an 8-bit Intel HEX format object file.")]
	[Remarks("Pages are defined in segment address records.")]
	public class IntelHex : IOutputWriter {

		public string DefaultExtension {
			get { return "hex"; }
		}

		public void WriteOutput(Compiler compiler, Stream stream) {
			if (compiler.Output.Length == 0) return;
			IntelHexWriter Writer = new IntelHexWriter(stream);

			Writer.WritePageNumbers = compiler.GetUniquePageIndices().Length > 1;

			foreach (Compiler.OutputData Output in compiler.Output) {
				for (int i = 0; i < Output.Data.Length; ++i) {
					Writer.Write((ushort)(Output.OutputCounter + i), (ushort)Output.Page, Output.Data[i]);
				}
			}
			Writer.Flush();
			Writer.Write(IntelHexWriter.Record.EndOfFile, 0, null);
			Writer.Flush();
		}

	}


	/// <summary>
	/// Implements a TextWriter for writing Intel Hex records to a stream.
	/// </summary>
	class IntelHexWriter : StreamWriter {

		public IntelHexWriter(Stream s)
			: base(s, Encoding.ASCII) {
		}

		public IntelHexWriter(string s)
			: base(s, false, Encoding.ASCII) {
		}

		public enum Record {
			Data = 0,
			EndOfFile = 1,
			ExtendedSegmentAddress = 2,
		}


		List<byte> WorkingData = new List<byte>();
		ushort CurrentPage;
		ushort CurrentAddress = 0;
		ushort? LastWrittenPage = null;

		private bool writePageNumbers;
		/// <summary>
		/// Gets or sets whether to write page numbers in extended segment address records.
		/// </summary>
		public bool WritePageNumbers {
			get { return this.writePageNumbers; }
			set { this.writePageNumbers = value; }
		}

		public void Write(ushort address, ushort page, byte data) {
			if (address != (CurrentAddress + WorkingData.Count) || page != CurrentPage) {
				this.Flush();
				this.CurrentPage = page;
				this.CurrentAddress = address;
			}
			this.WorkingData.Add(data);
		}

		public override void Flush() {
			if (this.WorkingData.Count > 0) {
				if (this.WritePageNumbers && (!LastWrittenPage.HasValue || CurrentPage != LastWrittenPage)) {
					this.Write(Record.ExtendedSegmentAddress, 0, new byte[] { (byte)(CurrentPage >> 8), (byte)CurrentPage });
					LastWrittenPage = CurrentPage;
				}
				this.Write(Record.Data, this.CurrentAddress, this.WorkingData.ToArray());
				this.WorkingData.Clear();
			}
			base.Flush();
		}

		public void Write(Record record, ushort address, byte[] data) {

			switch (record) {
				case Record.EndOfFile:
				case Record.ExtendedSegmentAddress:
					break;
				case Record.Data:
					if (data == null || data.Length == 0) return;
					break;
				default:
					throw new ArgumentException();
			}

			byte[] ChunkedData = new byte[32];

			if (data == null) data = new byte[] { };

			if (data.Length > 0) {

				for (int i = 0; i < data.Length; i += ChunkedData.Length) {
					int ActualDataLength = Math.Min(ChunkedData.Length, data.Length - i);

					byte Checksum = (byte)((byte)ActualDataLength + (byte)record + (byte)address + (byte)(address >> 8));

					base.Write(":{0:X2}{1:X4}{2:X2}", ActualDataLength, address, (int)record);

					for (int j = i; j < i + ActualDataLength; ++j) {
						base.Write(data[j].ToString("X2"));
						Checksum += data[j];
					}

					base.WriteLine("{0:X2}", (byte)(-Checksum));

					address += (ushort)ActualDataLength;
				}
			} else {
				base.WriteLine(":00{0:X4}{1:X2}FF", address, (int)record);

			}
		}


	}

}