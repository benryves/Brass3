using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;
using System.Globalization;

namespace SegaMasterSystem.Directives {
	[Description("Adds an SDSC tag to an SMS or Game Gear ROM file.")]
	[Remarks("If you set the string values to <c>\"\"</c>, 0 or $FFFF they are ignored. You can specify addresses of strings if you prefer.")]
	[Warning("The strings will be allocated somewhere within the first 16KB page, so do not expect unused areas to be set to the empty fill value and manually fill empty spaces you expect to have certain values.")]
	[Syntax(".sdsctag version, name, notes, author")]
	[CodeExample("Using string literals to populate tags.", ".sdsctag 3.0, \"Brass\", \"A user-extendable assembler.\", \"Bee Development\"")]
	[CodeExample("Using addresses of predefined strings.", ".sdsctag 1.02, Name, \"\", Author\r\n\r\nName   .db \"Tag example.\",0\r\nAuthor .db \"Ben Ryves\",0")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(Output.SmsRom))]
	[SeeAlso(typeof(Output.GGRom))]
	public class SdscTag : IDirective {

		#region Exciting SDSC Properties

		public struct SdscString {
			public int Address;
			public string Value;

			public SdscString(int address) {
				Value = null;
				Address = address;
			}

			public SdscString(string value) {
				Address = 0xFFFF;
				Value = value;
			}

			public SdscString(Compiler compiler, object argument) {
				this.Address = 0xFFFF;
				this.Value = null;
				if (argument is string) {
					this.Value = argument as string;
					if (string.IsNullOrEmpty(this.Value)) this.Value = null;
				} else {
					this.Address = (int)(double)argument;
					if (this.Address == 0) this.Address = 0xFFFF;
				}
			}
		}

		private SdscString author;
		/// <summary>
		/// Gets or sets SDSC tag author.
		/// </summary>
		public SdscString Author {
			get { return this.author; }
			set { this.author = value; }
		}

		private SdscString programName;
		/// <summary>
		/// Gets or sets SDSC tag author.
		/// </summary>
		public SdscString ProgramName {
			get { return this.programName; }
			set { this.programName = value; }
		}

		private SdscString releaseNotes;
		/// <summary>
		/// Gets or sets SDSC tag author.
		/// </summary>
		public SdscString ReleaseNotes {
			get { return this.releaseNotes; }
			set { this.releaseNotes = value; }
		}

		private DateTime date;
		/// <summary>
		/// Gets or sets the SDSC tag date.
		/// </summary>
		public DateTime Date {
			get { return this.date; }
			set { this.date = value; }
		}

		private int minorVersion;
		/// <summary>
		/// Gets or sets the minor version in the SDSC tag.
		/// </summary>
		public int MinorVersion {
			get { return this.minorVersion; }
			set {
				if (value < 0 || value > 99) throw new CompilerExpection((TokenisedSource)null, "Minor versions must be between 0 and 99.");
				this.minorVersion = value; 
			}
		}

		private int majorVersion;
		/// <summary>
		/// Gets or sets the major version in the SDSC tag.
		/// </summary>
		public int MajorVersion {
			get { return this.majorVersion; }
			set {
				if (value < 0 || value > 99) throw new CompilerExpection((TokenisedSource)null, "Major versions must be between 0 and 99."); 
				this.majorVersion = value;
			}
		}

		#endregion


		public SdscTag(Compiler c) {
			c.PassBegun += delegate(object sender, EventArgs e) {
				if (c.CurrentPass == AssemblyPass.CreatingLabels) {
					this.Date = DateTime.Now;
					this.Author = new SdscString("");
					this.ProgramName = new SdscString(0xFFFF);
					this.ReleaseNotes = new SdscString(0xFFFF);
				}
			};
			
		}

		public string Name { get { return this.Names[0]; } }
		public string[] Names { get { return new string[] { "sdsctag" }; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass == AssemblyPass.WritingOutput) {

				object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { 
					TokenisedSource.ArgumentType.Value,
					TokenisedSource.ArgumentType.String,
					TokenisedSource.ArgumentType.String,
					TokenisedSource.ArgumentType.String,
				});

				double Version = (double)Args[0];
				try{
					this.MajorVersion = (int)Version;
				} catch (CompilerExpection ex) { compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, ex)); }

				try {
					this.MinorVersion = ((int)(Math.Abs(Version - Math.Truncate(Version)) * 100));
				} catch (CompilerExpection ex) { compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, ex)); }

				this.ProgramName = new SdscString(compiler, Args[1]);
				this.ReleaseNotes = new SdscString(compiler, Args[2]);
				this.Author = new SdscString(compiler, Args[3]);

			}
		}


	}
}
