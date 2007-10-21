using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3;
using Brass3.Plugins;
using System.ComponentModel;
using Brass3.Attributes;

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
			foreach (OutputData Output in compiler.Output) {
				if (Output.Data.Length > 0) {
					Writer.Write((ushort)Output.OutputCounter, (ushort)Output.Page, Output.Data[0]);
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
				if (!LastWrittenPage.HasValue || CurrentPage != LastWrittenPage) {
					this.Write(Record.ExtendedSegmentAddress, 0, new byte[] { (byte)CurrentPage, (byte)(CurrentPage >> 8) });
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
					throw new ArgumentException(record + " is not a valid record type.");
			}

			byte[] ChunkedData = new byte[16];

			if (data == null) data = new byte[] { };

			for (int i = 0; i < data.Length; i += ChunkedData.Length) {
				int ActualDataLength = Math.Min(ChunkedData.Length, data.Length - i);

				byte Checksum = (byte)((byte)ActualDataLength + (byte)record + (byte)address);//+ (byte)(address >> 8));

				base.Write(":{0:X2}{1:X4}{2:X2}", ActualDataLength, address, (int)record);

				for (int j = i; j < i + ActualDataLength; ++j) {
					base.Write(data[j].ToString("X2"));
					Checksum += data[j];
				}

				base.WriteLine("{0:X2}", (byte)(~Checksum));

				address += (ushort)ActualDataLength;
			}
		}


	}

}