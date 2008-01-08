using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace SegaMasterSystem.Output {
	
	[Description("Outputs a Sega Master System ROM file with a Sega and SDSC header.")]
	[Remarks("This plugin is based on the raw pages output plugin, so the rules between the two are roughly the same.")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(GGRom))]
	[SeeAlso(typeof(Core.Output.RawPages))]
	public class SmsRom : Core.Output.RawPages {

		#region Dull inherited stuff

		public override string DefaultExtension {
			get { return "sms"; }
		}

		public SmsRom(Compiler c)
			: base(c) {
		}

		#endregion

		#region Inherit this class and override this to true and you get a free Game Gear writer.

		protected virtual bool IsGameGear {
			get { return false; }
		}

		#endregion

		#region Header Writing Magic

		public override void WriteOutput(Compiler compiler, Stream stream) {

			Core.Output.RawPages BasePlugin = compiler.GetPluginInstanceFromType(typeof(Core.Output.RawPages)) as Core.Output.RawPages;
			if (BasePlugin == null) throw new InvalidOperationException("Raw page output plugin not loaded.");


			// Grab the source data.
			byte[] Data = BasePlugin.CreateOutputData(compiler);
			if (Data.Length < 0x8000) {
				compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, "Complete output binary is smaller than 32KB."));
			} else {

				// Track down the SDSC tag directive plugin
				Directives.SdscTag SdscTagDirective = compiler.GetPluginInstanceFromType(typeof(Directives.SdscTag)) as Directives.SdscTag;

				// Check whether to write an SDSC header;
				int MinAddress = SdscTagDirective != null ? 0x7FE0 : 0x7FF0;

				// Create SMS stuff:
				int PageIndex;
				if (!BasePlugin.TryGetPageContainingRange(MinAddress, 0x7FFF, out PageIndex)) {
					compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, string.Format("No page has been declared that covers the ${0:X4}..$7FFF range to insert the Sega header into.", MinAddress)));
				} else {
					// PageIndex = the page to write to :)
					// First, check the address range is free;
					bool HeaderRangeIsFree = true;
					foreach (Compiler.OutputData CheckNotOverwriting in compiler.GetOutputDataOnPage(PageIndex)) {
						if (CheckNotOverwriting.OutputCounter >= MinAddress && CheckNotOverwriting.OutputCounter <= 0x7FFF) {
							compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, string.Format("There is output data in the ${0:X4}..$7FFF range that would get overwritten by the Sega header.", MinAddress)));
							HeaderRangeIsFree = false;
							break;
						}
					}

					if (HeaderRangeIsFree) { // Now we go and write the SMS header!

						#region SDSC Header

						if (SdscTagDirective != null) {

							// Dirty the SDSC tag area and Sega header;
							compiler.Labels.ProgramCounter.Page = PageIndex;
							compiler.Labels.OutputCounter.NumericValue = 0x7FE0;
							compiler.WriteStaticOutput(new byte[32]);

							// "SDSC"
							byte[] Sdsc = Encoding.ASCII.GetBytes("SDSC");
							Array.Copy(Sdsc, 0, Data, 0x7FE0, 4);

							// Version:
							Data[0x7FE4] = ToByteBcd(SdscTagDirective.MajorVersion);
							Data[0x7FE5] = ToByteBcd(SdscTagDirective.MinorVersion);

							// Date:
							Data[0x7FE6] = ToByteBcd(SdscTagDirective.Date.Day);
							Data[0x7FE7] = ToByteBcd(SdscTagDirective.Date.Month);
							ushort Year = ToUShortBcd(SdscTagDirective.Date.Year);
							Data[0x7FE8] = (byte)(Year & 0xFF);
							Data[0x7FE9] = (byte)(Year >> 8);

							
							// Allocate strings;
							ushort[] TagStrings = new ushort[]{
								AllocateSdscString(compiler, SdscTagDirective.Author, Data),
								AllocateSdscString(compiler, SdscTagDirective.ProgramName, Data),
								AllocateSdscString(compiler, SdscTagDirective.ReleaseNotes, Data)
							};

							for (int i = 0; i < TagStrings.Length; ++i) {
								Data[0x7FEA + i * 2] = (byte)TagStrings[i];
								Data[0x7FEB + i * 2] = (byte)(TagStrings[i] >> 8);
							}

							
						}

						#endregion

						#region Sega Header

						// "TMR SEGA"
						byte[] TmrSega = Encoding.ASCII.GetBytes("TMR SEGA");
						Array.Copy(TmrSega, 0, Data, 0x7FF0, 8);

						// Unknown.
						Data[0x7FF8] = 0xFF;
						Data[0x7FF9] = 0xFF;

						// Checksum:
						int ChecksumRange = Math.Min(0x40000, Data.Length); // Limit to 256KB
						if (ChecksumRange < 0x8000) ChecksumRange = 0x8000;
						if (ChecksumRange > 0x8000 && ChecksumRange < 0x20000) ChecksumRange = 0x8000; // 32KB
						if (ChecksumRange > 0x20000 && ChecksumRange < 0x40000) ChecksumRange = 0x40000; // 128KB

						ushort ChecksumValue = 0x0000;
						for (int i = 0; i < ChecksumRange; ++i) {
							if (i < 0x7FF0 || i >= 0x8000) ChecksumValue += Data[i];
						}

						Data[0x7FFA] = (byte)(ChecksumValue & 0xFF);
						Data[0x7FFB] = (byte)(ChecksumValue >> 0x8);


						// Part number and version

						int[] ExpandedVersionNumber = new int[6];


						int Divisor = 100000;
						int Part = 0;

						Directives.SegaProduct Product = compiler.GetPluginInstanceFromType<Directives.SegaProduct>();
						if (Product != null) Part = Product.ProductNumber;

						int VersionNumber = 0;
						Directives.SegaVersion Version = compiler.GetPluginInstanceFromType<Directives.SegaVersion>();
						if (Version != null) VersionNumber = Version.Version;

						for (int i = 0; i < 6; ++i) {
							ExpandedVersionNumber[i] = Part / Divisor;
							Part -= ExpandedVersionNumber[i] * Divisor;
							Divisor /= 10;
						}

						Data[0x7FFC] = (byte)((ExpandedVersionNumber[4] << 4) | (ExpandedVersionNumber[5]));
						Data[0x7FFD] = (byte)((ExpandedVersionNumber[2] << 4) | (ExpandedVersionNumber[3]));
						Data[0x7FFE] = (byte)(((ExpandedVersionNumber[0] * 10 + ExpandedVersionNumber[1]) << 4) | (VersionNumber));

						// Checksum range
						switch (ChecksumRange) {
							case 0x8000:
								Data[0x7FFF] = 0x0C;
								break;
							case 0x20000:
								Data[0x7FFF] = 0x0F;
								break;
							case 0x40000:
								Data[0x7FFF] = 0x00;
								break;
						}

						Directives.SegaRegion.Regions Region = Directives.SegaRegion.Regions.Export;
						Directives.SegaRegion RegionPlugin = compiler.GetPluginInstanceFromType<Directives.SegaRegion>();
						if (RegionPlugin != null) Region = RegionPlugin.Region;

						// Model/region:
						if (this.IsGameGear) {
							switch (Region) {
								case Directives.SegaRegion.Regions.Japan:
									Data[0x7FFF] |= 0x50;
									break;
								case Directives.SegaRegion.Regions.Export:
									Data[0x7FFF] |= 0x60;
									break;
								case Directives.SegaRegion.Regions.International:
									Data[0x7FFF] |= 0x70;
									break;
							}
						} else {
							switch (Region) {
								case Directives.SegaRegion.Regions.Japan:
									Data[0x7FFF] |= 0x30;
									break;
								case Directives.SegaRegion.Regions.Export:
									Data[0x7FFF] |= 0x40;
									break;
								case Directives.SegaRegion.Regions.International:
									Data[0x7FFF] |= 0x40;
									compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, "'International' region invalid for Sega Master System ROMs (amended to 'Export')."));
									break;
							}
						}
						
						#endregion

	
					}
				}
			}

			BinaryWriter Out = new BinaryWriter(stream);
			Out.Write(Data);
			Out.Flush();
		}


		#endregion

		#region Helpers

		private static byte ToByteBcd(int value) {
			int Tens = ((value % 100) / 10);
			int Units = value % 10;
			return (byte)((Tens << 4) | Units);
		}

		private static ushort ToUShortBcd(int value) {
			int Thousands = ((value % 10000) / 1000);
			int Hundreds = ((value % 1000) / 100);
			int Tens = ((value % 100) / 10);
			int Units = value % 10;
			return (ushort)((Thousands << 12) | (Hundreds << 8) | (Tens << 4) | Units);
		}

		private static ushort AllocateSdscString(Compiler c, Directives.SdscTag.SdscString str, byte[] binary) {
			if (str.Value != null) {
				byte[] Data = Encoding.ASCII.GetBytes(str.Value + '\0');
				int Address = c.FindFreeMemoryBlock(c.Labels.ProgramCounter.Page, Data.Length);
				if (Address < 0x7FE0) {
					c.Labels.OutputCounter.NumericValue = Address;
					Array.Copy(Data, 0, binary, Address, Data.Length); // Copy in the string!
					c.WriteStaticOutput(Data);
					return (ushort)Address;
				}
				c.OnWarningRaised(new Compiler.NotificationEventArgs(c, "No room to store SDSC tag '" + str.Value + "'."));
				return 0xFFFF;
			} else {
				return (ushort)str.Address;
			}
		}

		#endregion


	}
}
