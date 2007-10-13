using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TexasInstruments.Types.Variables {
	public class Variable {

		private CalculatorModel model;
		/// <summary>
		/// Gets or sets the calculator model of this group file.
		/// </summary>
		public CalculatorModel Model {
			get { return this.model; }
			set {
				if (!Enum.IsDefined(typeof(CalculatorModel), value)) throw new ArgumentException("Unsupported model.");
				this.model = value;
			}
		}

		private byte[] data;
		public virtual byte[] RawData {
			get { return this.data; }
		}

		public byte[] rawHeader;
		public virtual byte[] RawHeader {
			get { return new byte[] { }; }
		}

		public Variable() {
			this.data = new byte[] { };
		}

		/// <summary>
		/// Saves the variable to a stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		public virtual void Save(Stream stream) {
			BinaryWriter VariableWriter = new BinaryWriter(stream);

			List<byte> OutputData = new List<byte>(1024);

			byte[] SourceData = this.RawData;
			byte[] SourceHeader = this.RawHeader;

			switch (this.Model) {
				case CalculatorModel.TI83Plus:
					VariableWriter.Write((ushort)0x000D);
					break;
				case CalculatorModel.TI82:
				case CalculatorModel.TI83:
				case CalculatorModel.TI73:
					VariableWriter.Write((ushort)0x000C);
					break;

				case CalculatorModel.TI85:
					throw new NotImplementedException("No TI-85 support.");
					break;
			}
			// Size:
			VariableWriter.Write((ushort)(SourceData.Length));

			// Header:
			VariableWriter.Write(SourceHeader);

			// Version+Archived flags for 83+
			if (this.Model == CalculatorModel.TI83Plus) {
				VariableWriter.Write((ushort)0x0000);
			}

			// Size (nothing like a bit of redundant redundancy):
			VariableWriter.Write((ushort)(SourceData.Length));

			// Actual data (about time too...)
			VariableWriter.Write(SourceData);


		}

	}
}
