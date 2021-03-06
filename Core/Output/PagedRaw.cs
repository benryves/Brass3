using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using System.ComponentModel;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Output {

	[Description("Writes output as a raw binary file of consecutive, padded pages.\r\nPages are output sequentially in the order of their indices, and each page in the binary is output at its declared size.\r\nUnused regions of pages will be filled with the last defined empty fill value.")]
	[Warning("This output writer is incompatible with output modifiers that increase the size of data.")]
	[CodeExample("Sega Master System Mega Cartridge.", "/*\r\nThe Sega Master System's pages are 16KB in size.\r\nWindows 0, 1 and 2 can be swapped at will (excluding the\r\nfirst 1KB of window 0), but the official documentation\r\nrecommends using a fixed 32KB in windows 0 and 1 and only\r\nvarying window 2 (16KB).\r\n$C000..$FFFF contains 8KB of RAM (mirrored) and the paging\r\nregisters.\r\nHere is a sample that matches the \"Mega\" cartridge layout.\r\n*/\r\n\r\n; Page 0 is one large 32KB page.\r\n.defpage 0, kb(32), $0000\r\n\r\n; Pages 1 to 6 are all 16KB with a base address of $8000.\r\n.for p is 1 to 6\r\n\t.defpage p + 1, kb(16), $8000\r\n.loop\r\n\r\n/*\r\nFor practicality, page indices are offset by 1.\r\nThis is so I can directly write a label's page value to\r\n$FFFF and swap the page in automatically.\r\n*/")]
	[SeeAlso(typeof(Directives.DefPage))]
	[SeeAlso(typeof(Directives.EmptyFill))]
	[SeeAlso(typeof(Directives.Page))]
	public class RawPages : IOutputWriter {

		public virtual string DefaultExtension {
			get { return "bin"; }
		}

		public struct PageDeclaration {
			public int Address;
			public int Size;
			public PageDeclaration(int size, int address) {
				this.Address = address;
				this.Size = size;
			}
		}

		internal Dictionary<int, PageDeclaration> pageDeclarations;

		public Dictionary<int, PageDeclaration> PageDeclarations {
			get {
				return this.pageDeclarations;
			}
		}

		internal bool DisplayPageFreeSpace { get; set; }

		/// <summary>
		/// Try and get the page number that contains a particular range of addresses.
		/// </summary>
		/// <param name="startAddress">The start of the address range.</param>
		/// <param name="endAddress">The end of the address range (inclusive).</param>
		/// <param name="page">The page containing the range.</param>
		/// <returns>True if found, false otherwise.</returns>
		public bool TryGetPageContainingRange(int startAddress, int endAddress, out int page) {
			page = 0;
			foreach (KeyValuePair<int, PageDeclaration> PD in this.pageDeclarations) {
				if (PD.Value.Address <= startAddress && PD.Value.Address + PD.Value.Size > endAddress) {
					page = PD.Key;
					return true;
				}
			}
			return false;		
		}

		public byte[] CreateOutputData(Compiler compiler) {

			List<int> DeclaredPages = new List<int>(this.pageDeclarations.Keys);
			DeclaredPages.Sort();

			foreach (int Page in compiler.GetUniquePageIndices()) {
				if (!DeclaredPages.Contains(Page)) {
					compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, string.Format(Strings.ErrorRawPagesUsedPageNotDefined, Page)));
				}
			}

			List<byte> Output = new List<byte>(512 * 1024);

			int TotalFreeSpace = 0, TotalAvailableSpace = 0;

			foreach (int Page in DeclaredPages) {

				PageDeclaration PD = this.pageDeclarations[Page];

				byte[] PageData = new byte[PD.Size];
				bool[] WrittenPageData = new bool[PD.Size];

				TotalAvailableSpace += PD.Size;

				if (compiler.EmptyFill != 0) {
					for (int i = 0; i < PageData.Length; ++i) PageData[i] = compiler.EmptyFill;
				}


				int OutOfPageBounds = 0;
				foreach (Compiler.OutputData DataToWrite in compiler.GetOutputDataOnPage(Page)) {
					if (DataToWrite.Data == null || DataToWrite.Data.Length < 1) continue;
					int DestinationAddress = DataToWrite.OutputCounter - PD.Address;

					for (int i = 0; i < DataToWrite.Data.Length; i++) {
						if (DestinationAddress < 0 || DestinationAddress >= PD.Size) {
							++OutOfPageBounds;
						} else {
							PageData[DestinationAddress] = DataToWrite.Data[i];
							WrittenPageData[DestinationAddress] = true;
						}
						++DestinationAddress;
					}					
				}

				if (OutOfPageBounds > 0) {
					compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, string.Format(Strings.ErrorRawPagesDataOutOfBounds, OutOfPageBounds, Page)));
				}

				int PageFreeSpace = 0;
				foreach (var Written in WrittenPageData) PageFreeSpace += Written ? 0 : 1;
				TotalFreeSpace += PageFreeSpace;

				if (this.DisplayPageFreeSpace) compiler.OnMessageRaised(new Compiler.NotificationEventArgs(compiler, string.Format(Strings.FormatFreePageSpace + Environment.NewLine, Page, PageFreeSpace, (double)PageFreeSpace / (double)PD.Size)));

				Output.AddRange(PageData);
			}

			if (this.DisplayPageFreeSpace) compiler.OnMessageRaised(new Compiler.NotificationEventArgs(compiler, string.Format(Strings.FormatFreeRomSpace + Environment.NewLine, TotalFreeSpace, TotalAvailableSpace, (double)TotalFreeSpace / (double)TotalAvailableSpace)));

			return Output.ToArray();

		}

		public virtual void WriteOutput(Compiler compiler, Stream stream) {
			BinaryWriter PagedOutputWriter = new BinaryWriter(stream);
			PagedOutputWriter.Write(this.CreateOutputData(compiler));
		}

		public RawPages(Compiler compiler) {
			compiler.CompilationBegun += (sender, e) => {
				this.pageDeclarations = new Dictionary<int, PageDeclaration>();
				this.DisplayPageFreeSpace = false;
			};
		}


	
	}
}