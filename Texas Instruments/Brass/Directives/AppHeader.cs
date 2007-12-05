using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
namespace TexasInstruments.Brass.Directives {
	
	[Category("Texas Instruments")]
	[Description("Inserts an application header.")]
	[Remarks("This directive inserts a 128-byte application header for TI-83 Plus and TI-73 applications.")]
	[CodeExample("A skeleton TI-83 Plus appliction.", @".include ""TI/ti83plus.inc""
.defpage 0, kb(16), $4000

.page 0
	
	.appheader

	.bjump _JForceCmdNoChar")]
	[SeeAlso(typeof(AppField))]
	public class AppHeader : IDirective {

		#region Types

		/// <summary>Defines the ID for a field in an application's header.</summary>
		private enum AppField : byte {
			ProgramLength = 0x0F,
			DeveloperKey = 0x12,
			ProgramRevision = 0x21,
			BuildNumber = 0x31,
			Name = 0x48,
			PageCount = 0x81,
			DisableTISplashScreen = 0x90,
			ImageLength = 0x7F,
			OveruseCount = 0x61,
			Hardware = 0xA1,
			Basecode = 0xC2,
		};

		#endregion

		#region Properties

		private string name;
		/// <summary>
		/// Gets or sets the name of the application.
		/// </summary>
		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}

		private ushort? developerKey;
		/// <summary>
		/// Gets or sets the developer's key ID.
		/// Set to null to use the shareware key.
		/// </summary>
		public ushort? DeveloperKey {
			get { return this.developerKey; }
			set { this.developerKey = value; }
		}

		private byte programRevision;
		/// <summary>Gets or sets the version of the application.</summary>
		public byte ProgramRevision {
			get { return this.programRevision; }
			set { this.programRevision = value; }
		}
	
		private byte buildNumber;
		/// <summary>Gets or sets the build number.</summary>
		/// <remarks>Often used a sub-version. For example: v1.0 Build 10.</remarks>
		public byte BuildNumber {
			get { return this.buildNumber; }
			set { this.buildNumber = value; }
		}

		private DateTime? programExpirationDate;
		/// <summary>
		/// Gets or sets the date the application is valid until.
		/// Set to null to have no expiration date.
		/// </summary>
		public DateTime? ProgramExpirationDate {
			get { return this.programExpirationDate; }
			set {
				if (value.HasValue) {
					if (value < new DateTime(1997, 1, 1)) throw new ArgumentOutOfRangeException("Expiration dates cannot be before 1997-07-01.");
				}
				this.programExpirationDate = value; 
			}
		}

		private byte? overuseCount;
		/// <summary>
		/// Gets or sets the maximum number of times the application can be run.
		/// This has to be at least 16 times; set to null for unlimited use.
		/// </summary>
		public byte? OveruseCount {
			get { return this.overuseCount; }
			set {
				if (value.HasValue) {
					if (value < 16) throw new ArgumentOutOfRangeException("Overuse count must be at least 16.");
				}
				this.overuseCount = value; 
			}
		}

		private bool disableTISplashScreen;
		/// <summary>Set to true to disable the default TI splash screen.</summary>
		public bool DisableTISplashScreen {
			get { return this.disableTISplashScreen; }
			set { this.disableTISplashScreen = value; }
		}

		private byte? maximumHardwareRevision;
		/// <summary>
		/// Gets or sets the maximum hardware revision the application is valid on.
		/// Set to null to not impose a hardware revision limit.
		/// </summary>
		public byte? MaximumHardwareRevision {
			get { return this.maximumHardwareRevision; }
			set { this.maximumHardwareRevision = value; }
		}

		private decimal? lowestBasecode;
		/// <summary>
		/// Gets or sets the minimum basecode that the application can run on.
		/// Set to null to not impose a basecode limit.
		/// </summary>
		public decimal? LowestBasecode {
			get { return this.lowestBasecode; }
			set { this.lowestBasecode = value; }
		}

		#endregion


		#region Constructor

		public AppHeader(Compiler compiler) {

			// Reset state.
			compiler.CompilationBegun += delegate(object sender, EventArgs e) {
				this.ProgramRevision = 1;
				this.BuildNumber = 1;
				this.ProgramExpirationDate = null;
				this.OveruseCount = null;
				this.DisableTISplashScreen = true;
				this.MaximumHardwareRevision = null;
				this.LowestBasecode = null;
				this.Name = "MYAPP";
				this.Invoked = false;
				this.ProgramCounter = null;
				this.OutputCounter = null;
			};

			compiler.CompilationEnded += delegate(object sender, EventArgs e) {

				if (this.Invoked) {

					compiler.Labels.ProgramCounter.NumericValue = this.ProgramCounter.NumericValue;
					compiler.Labels.ProgramCounter.Page = this.ProgramCounter.Page;
					compiler.Labels.OutputCounter.NumericValue = this.OutputCounter.NumericValue;
					compiler.Labels.OutputCounter.Page = this.OutputCounter.Page;

					TIVariableName VariableName = compiler.GetPluginInstanceFromType<TIVariableName>();
					if (VariableName != null) {
						string AppName;
						if (VariableName.VariableNames.TryGetValue(0, out AppName)) {
							this.Name = AppName;
						}
					}

					List<byte> HeaderData = new List<byte>(128);

					AddAppField(HeaderData, AppField.ProgramLength, (int)0);
					AddAppField(HeaderData, AppField.DeveloperKey, (ushort)(this.DeveloperKey.HasValue ? this.DeveloperKey : (ushort)(
						compiler.OutputWriter.GetType() == typeof(Output.TI73App) ? 0x0102 : 0x0104
					)));
					AddAppField(HeaderData, AppField.ProgramRevision, this.ProgramRevision);
					AddAppField(HeaderData, AppField.BuildNumber, this.BuildNumber);

					if (this.Name.Length > 8) compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, "Application name truncated to 8 characters."));
					byte[] NameData = new Large8X().GetData(this.Name);
					Array.Resize<byte>(ref NameData, 8);
					AddAppField(HeaderData, AppField.Name, NameData);


					int PageCount = (compiler.GetUniquePageIndices().Length);
					AddAppField(HeaderData, AppField.PageCount, (byte)PageCount);

					if (this.DisableTISplashScreen) AddAppField(HeaderData, AppField.DisableTISplashScreen);

					if (this.LowestBasecode.HasValue && this.LowestBasecode != 0) {
						AddAppField(HeaderData, AppField.Basecode, new byte[] { (byte)this.LowestBasecode, (byte)((this.LowestBasecode * 100m) % 100m) });
					}


					/*int DateTimestamp = (int)(((TimeSpan)(DateTime.Now - new DateTime(1997, 1, 1))).TotalSeconds);
					HeaderData.AddRange(new byte[] { 0x03, 0x26, 0x09, 0x04 });
					for (int i = 0; i < 4; ++i) HeaderData.Add((byte)(DateTimestamp >> (3 - i) * 8));
					 */
					HeaderData.AddRange(new byte[] { 0x003, 0x026, 0x009, 0x004, 0x004, 0x06f, 0x01b, 0x080 });

					// Dummy encrypted TI date stamp signature
					HeaderData.AddRange(new byte[] { 0x002, 0x00d, 0x040, 0x0a1, 0x06b, 0x099, 0x0f6, 0x059, 0x0bc, 0x067, 0x0f5, 0x085, 0x09c, 0x009, 0x06c, 0x00f, 0x0b4, 0x003, 0x09b, 0x0c9, 0x003, 0x032, 0x02c, 0x0e0, 0x003, 0x020, 0x0e3, 0x02c, 0x0f4, 0x02d, 0x073, 0x0b4, 0x027, 0x0c4, 0x0a0, 0x072, 0x054, 0x0b9, 0x0ea, 0x07c, 0x03b, 0x0aa, 0x016, 0x0f6, 0x077, 0x083, 0x07a, 0x0ee, 0x01a, 0x0d4, 0x042, 0x04c, 0x06b, 0x08b, 0x013, 0x01f, 0x0bb, 0x093, 0x08b, 0x0fc, 0x019, 0x01c, 0x03c, 0x0ec, 0x04d, 0x0e5, 0x075 });

					AddAppField(HeaderData, AppField.ImageLength, (int)0);

					if (this.MaximumHardwareRevision.HasValue && this.MaximumHardwareRevision != 0) AddAppField(HeaderData, AppField.Hardware, this.MaximumHardwareRevision.Value);
					if (this.OveruseCount.HasValue && this.OveruseCount != 0) AddAppField(HeaderData, AppField.OveruseCount, this.OveruseCount.Value);

					if (HeaderData.Count > 128) compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, "Application header has been truncated."));

					byte[] RawHeaderData = HeaderData.ToArray();
					Array.Resize<byte>(ref RawHeaderData, 128);

					compiler.WriteStaticOutput(RawHeaderData);
				}

			};
		}

		#endregion

		#region Public Methods

		private Label OutputCounter;
		private Label ProgramCounter;
		private bool Invoked = false;


		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			Invoked = true;
			this.OutputCounter = compiler.Labels.OutputCounter.Clone() as Label;
			this.ProgramCounter = compiler.Labels.ProgramCounter.Clone() as Label;
			bool WasBackground = compiler.DataWrittenToBackground;
			compiler.DataWrittenToBackground = true;
			compiler.WriteStaticOutput(new byte[128]); // 128 bytes of dud data.
			compiler.DataWrittenToBackground = WasBackground;
		}

		#endregion

		#region Private Methods


		private static void AddAppField(List<byte> header, AppField field) {
			header.Add(0x80);
			header.Add((byte)field);
		}

		private static void AddAppField(List<byte> header, AppField field, byte value) {
			AddAppField(header, field);
			header.Add(value);
		}

		private static void AddAppField(List<byte> header, AppField field, ushort value) {
			AddAppField(header, field);
			for (int i = 0; i < 2; ++i) {
				header.Add((byte)(value >> 8));
				value <<= 8;
			}
		}

		private static void AddAppField(List<byte> header, AppField field, int value) {
			AddAppField(header, field);
			for (int i = 0; i < 4; ++i) {
				header.Add((byte)(value >> 24));
				value <<= 8;
			}
		}

		private static void AddAppField(List<byte> header, AppField field, byte[] value) {
			AddAppField(header, field);
			header.AddRange(value);
		}

		#endregion
	}
}
