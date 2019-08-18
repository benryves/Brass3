using System.IO;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using System;

namespace Z80 {
	public class IncM80Rel : IDirective {

		#region Types

		enum AddressType {
			Absolute = 0,
			ProgramRelative = 1,
			DataRelative = 2,
			CommonRelative = 3,
		}

		struct LinkerOutput {
			private byte data;
			public byte Data { get { return this.data; } set { this.data = value; } }
			private bool writtenTo;
			public bool WrittenTo { get { return this.writtenTo; } set { this.writtenTo = value; } }
			public LinkerOutput(byte data) {
				this.data = data;
				this.writtenTo = true;
			}
		}

		#endregion

		#region IDirective Members

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			var Filename = (string)source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.FilenameArgument)[0];


			ushort ProgramBase = (ushort)compiler.Labels.OutputCounter.NumericValue;
			ushort DataBase = ProgramBase;
			ushort CommonBase = ProgramBase;

			LinkerOutput[] Segment = new LinkerOutput[0x10000];
			ushort? ProgramSegmentSize = null;
			ushort? DataSegmentSize = null;

			ushort LocationCounter = ProgramBase;

			using (var Module = new BitStreamReader(File.OpenRead(Filename))) {
				var Finished = false;
				while (!Finished) {
					if (Module.ReadBit()) {
						// Instruction.
						switch (Module.ReadValue(2)) {
							case 0: // Special link item.
							{
									var SpecialType = Module.ReadValue(4);
									if (SpecialType == 15) {
										Finished = true;
										break;
									}


									// "A" field.
									AddressType AddressMode = AddressType.Absolute;
									ushort Address = 0xDEAD;

									// "B" field.
									string Name = null;

									switch (SpecialType) {
										case 0:
										case 1:
										case 2:
										case 3:
										case 4:
											Name = Module.ReadString();
											break;
										case 5:
										case 6:
										case 7:
										case 8:
											AddressMode = (AddressType)Module.ReadValue(2);
											Address = Module.ReadUInt16();
											Name = Module.ReadString();
											break;
										case 9:
										case 10:
										case 11:
										case 12:
										case 13:
										case 14:
											AddressMode = (AddressType)Module.ReadValue(2);
											Address = Module.ReadUInt16();
											break;
									}


									ushort AdjustedAddress = 0;

									switch (AddressMode) {
										case AddressType.Absolute:
											AdjustedAddress = (ushort)(Address);
											break;
										case AddressType.ProgramRelative:
											AdjustedAddress = (ushort)(Address + ProgramBase);
											break;
										case AddressType.DataRelative:
											AdjustedAddress = (ushort)(Address + DataBase);
											break;
										case AddressType.CommonRelative:
											AdjustedAddress = (ushort)(Address + CommonBase);
											break;
									}

									switch (SpecialType) {

										#region Name field only

										case 0: // Entry symbol ("public").
											Console.WriteLine(@"Public ""{0}""", Name);
											break;
										
										case 2: // Program name.
											Console.WriteLine(@"Program name ""{0}""", Name);
											break;

										#endregion

										#region Name and address

										
										case 6: // Chain external.
											Console.WriteLine(@"Chain external ""{0}"" head = &{1:X4}", Name, AdjustedAddress);
											compiler.WriteDynamicOutput(0, g => {
												Label External;
												if (!compiler.Labels.TryParse(new TokenisedSource.Token(Name), out External)) {
													//throw new CompilerException(source, "External label " + Name + " not defined.");
													//Console.WriteLine(Name + "?");
												}
											});
											break;
										
										case 7: // Define entry point ("public").
											Console.WriteLine(@"Define entry point ""{0}"" = &{1:X4}", Name, AdjustedAddress);
											var L = compiler.Labels.Create(new TokenisedSource.Token(Name));
											L.NumericValue = AdjustedAddress;
											L.SetConstant();
											break;

										#endregion

										#region Address only

										case 10: // Define data size.
											Console.WriteLine(@"Define data size = {0}", Address);
											if (DataSegmentSize.HasValue) throw new InvalidOperationException("Data size already defined.");
											DataSegmentSize = Address;
											break;

										case 11: // Set location counter.
											Console.WriteLine(@"Set location counter = &{0:X4}", AdjustedAddress);
											LocationCounter = AdjustedAddress;
											break;
										
										case 13: // Define program size.
											if (ProgramSegmentSize.HasValue) throw new InvalidOperationException("Program size already defined.");
											ProgramSegmentSize = Address;
											break;

										case 14: // End module.
											Console.WriteLine(@"End module = &{0:X4}", Address);
											Module.AlignToNextByte();
											break;

										#endregion
										
										default:
											throw new NotSupportedException(string.Format("Special item type {0} not supported.", SpecialType));
									}
								}
							break;
							case 1: // Program relative.
							{
								var Data = Module.ReadUInt16();
								Segment[LocationCounter++] = new LinkerOutput((byte)(ProgramBase + Data));
								Segment[LocationCounter++] = new LinkerOutput((byte)((ProgramBase + Data) >> 8));
							}
							break;
							case 2: // Data relative.
							{
								var Data = Module.ReadUInt16();
								Segment[LocationCounter++] = new LinkerOutput((byte)(DataBase + Data));
								Segment[LocationCounter++] = new LinkerOutput((byte)((DataBase + Data) >> 8));
							}
							break;
							case 3: // Common relative.
							{
								var Data = Module.ReadUInt16();
								Segment[LocationCounter++] = new LinkerOutput((byte)(CommonBase + Data));
								Segment[LocationCounter++] = new LinkerOutput((byte)((CommonBase + Data) >> 8));
							}
							break;
						}
					} else {
						// Data.
						var Data = Module.ReadByte();
						Segment[LocationCounter++] = new LinkerOutput(Data);
					}
				}
			}
			//TokenisedSource.Token T
			//	compiler.Labels[T.ToString()]
			
			int TotalSize = (ProgramSegmentSize ?? 0) + (DataSegmentSize ?? 0);
			compiler.WriteDynamicOutput(TotalSize, g => {
				g.Data = new byte[TotalSize];
			});

		}


		#endregion
	}
}
