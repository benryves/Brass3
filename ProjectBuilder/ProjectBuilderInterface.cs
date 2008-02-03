using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BeeDevelopment.Brass3;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Drawing;

namespace ProjectBuilder {
	public partial class ProjectBuilderInterface : Form {

		/// <summary>
		/// Loaded working project.
		/// </summary>
		private Project WorkingProject;

		/// <summary>
		/// Name of the current configuration as a string (eg TI8X.Ion).
		/// </summary>
		private string CurrentConfiguration;

		/// <summary>
		/// Dictionary mapping temporary image identifiers to filenames.
		/// </summary>
		private Dictionary<string, string> TemporaryImages;

		public ProjectBuilderInterface() {
			InitializeComponent();
			this.Text = Application.ProductName;

			this.TemporaryImages = new Dictionary<string, string>();
			this.StoreTemporaryImage("UnderlineError", Properties.Resources.UnderlineError);
			this.StoreTemporaryImage("UnderlineWarning", Properties.Resources.UnderlineWarning);
			this.StoreTemporaryImage("IconError", Properties.Resources.IconError);
			this.StoreTemporaryImage("IconWarning", Properties.Resources.IconWarning);
			this.StoreTemporaryImage("IconMessages", Properties.Resources.IconMessage);

			this.Disposed += (sender, e) => this.CleanUpTemporaryImages();

			this.WorkingProject = null;
			this.WorkingProject = null;
		}

		private void MenuExit_Click(object sender, EventArgs e) {
			this.Close();
		}


		#region Project Loading and Editing

		private void MenuProject_DropDownOpening(object sender, EventArgs e) {
			foreach (ToolStripItem SubItem in MenuProject.DropDownItems) SubItem.Enabled = WorkingProject != null;
		}

		private void MenuOpenProject_Click(object sender, EventArgs e) {
			if (this.OpenProjectDialog.ShowDialog(this) == DialogResult.OK) {
				this.CurrentConfiguration = null;
				this.WorkingProject = new Project();
				try {
					this.WorkingProject.Load(this.OpenProjectDialog.FileName);
				} catch (Exception ex) {
					MessageBox.Show(this, "Error loading project:" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.WorkingProject = null;
				}
			}
		}

		#endregion

		#region Help

		private void MenuHelp_DropDownOpening(object sender, EventArgs e) {
			MenuBrassHelp.Enabled = File.Exists(Path.Combine(Application.StartupPath, "Help.exe"));
		}

		private void MenuBrassHelp_Click(object sender, EventArgs e) {
			string BrassHelpPath = Path.Combine(Application.StartupPath, "Help.exe");
			if (File.Exists(BrassHelpPath)) {
				try {
					Process.Start(BrassHelpPath);
				} catch (Exception ex) {
					MessageBox.Show(this, "Error launching Brass help:" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		#endregion

		#region Building

		#region UI

		private void MenuRebuild_Click(object sender, EventArgs e) {
			this.Build();
		}

		private void MenuBuild_DropDownOpening(object sender, EventArgs e) {
			List<ToolStripItem> ToPurge = new List<ToolStripItem>();
			foreach (ToolStripItem SubItem in MenuBuild.DropDownItems) {
				if (SubItem != MenuRebuild) ToPurge.Add(SubItem);
			}
			foreach (ToolStripItem SubItem in ToPurge) {
				this.MenuBuild.DropDownItems.Remove(SubItem);
				SubItem.Dispose();
			}

			if (this.WorkingProject == null) {
				MenuRebuild.Enabled = false;
			} else {
				MenuRebuild.Enabled = CurrentConfiguration != null;
				var Configurations = new List<KeyValuePair<KeyValuePair<string, string>, Project>>();
				MenuBuild.DropDownItems.Add(new ToolStripSeparator());
				foreach (var Configuration in this.WorkingProject.GetBuildConfigurationNames()) {
					ToolStripMenuItem ConfigurationButton = new ToolStripMenuItem(Configuration.Value);
					ConfigurationButton.Tag = Configuration.Key;
					ConfigurationButton.Checked = this.CurrentConfiguration == (ConfigurationButton.Tag as string);
					ConfigurationButton.Click += (ls, le) => {
						this.CurrentConfiguration = ConfigurationButton.Tag as string;
						this.Build();
					};
					MenuBuild.DropDownItems.Add(ConfigurationButton);

				}
			}
		}

		#endregion



		/// <summary>
		/// Builds the currently selected configuration.
		/// </summary>
		private void Build() {
			if (this.CurrentConfiguration == null) {
				MessageBox.Show(this, "No build configuration selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Compiler C = new Compiler();
			C.LoadProject(this.WorkingProject.GetBuildConfiguration(this.CurrentConfiguration));

			int WarningCount = 0, ErrorCount = 0;
			var OutputMessages = new StringBuilder(1024);
			var OutputErrors = new StringBuilder(1024);

			C.WarningRaised += delegate(object sender, Compiler.NotificationEventArgs e) { ++WarningCount; this.AppendMessage(OutputErrors, "warning", e); };
			C.ErrorRaised += delegate(object sender, Compiler.NotificationEventArgs e) { ++ErrorCount; this.AppendMessage(OutputErrors, "error", e); };
			C.MessageRaised += delegate(object sender, Compiler.NotificationEventArgs e) { OutputMessages.Append(e.Message); };

			C.Compile(true);

			string ErrorText = OutputErrors.ToString();

			if (OutputMessages.Length != 0) {
				using (var OutputMessagesStream = new MemoryStream(1024)) {

					XmlWriterSettings Settings = new XmlWriterSettings() {
						Indent = false,
						Encoding = Encoding.UTF8,
						OmitXmlDeclaration = true,
					};

					var OutputMessagesWriter = XmlWriter.Create(OutputMessagesStream, Settings);

					OutputMessagesWriter.WriteStartElement("div");
					OutputMessagesWriter.WriteAttributeString("class", "messages");
					OutputMessagesWriter.WriteElementString("h1", "Messages.");
					OutputMessagesWriter.WriteElementString("pre", OutputMessages.ToString());
					OutputMessagesWriter.WriteEndElement();

					OutputMessagesWriter.Flush();
					var Data = OutputMessagesStream.ToArray();

					//HACK: Overwrite the BOM with spaces characters.
					Data[0] = 0x20;
					Data[1] = 0x20;
					Data[2] = 0x20;

					ErrorText += Encoding.UTF8.GetString(Data);
				}
			}

			ErrorText = Properties.Resources.OutputFormatting.Replace("$(Errors)", ErrorText);
			foreach (KeyValuePair<string, string> Image in this.TemporaryImages) {
				ErrorText = ErrorText.Replace(string.Format("$({0})", Image.Key), Image.Value);
			}

			this.BrowserOutput.DocumentText = ErrorText;

		}

		/// <summary>
		/// Turns an error message or warning into HTML and appends it to a <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="toAppendTo">The <see cref="StringBuilder"/> to append the error message to.</param>
		/// <param name="type">The type (error or warning).</param>
		/// <param name="message">The message to append.</param>
		private void AppendMessage(StringBuilder toAppendTo, string type, Compiler.NotificationEventArgs message) {

			using (var OutputMessage = new MemoryStream(1024)) {

				XmlWriterSettings Settings = new XmlWriterSettings() {
					Indent = false,
					Encoding = Encoding.UTF8,
					OmitXmlDeclaration = true,
				};

				var OutputErrorsWriter = XmlWriter.Create(OutputMessage, Settings);

				OutputErrorsWriter.WriteStartElement("div");
				OutputErrorsWriter.WriteAttributeString("class", type);

				OutputErrorsWriter.WriteElementString("h1", message.Message);

				if (message.SourceStatement != null) {
					TokenisedSource OuterSource = message.SourceStatement.OutermostTokenisedSource;

					int FirstHighlightedToken = 0;
					int LastHighlightedToken = OuterSource.Tokens.Length - 1;

					if (message.SourceToken != null) {
						for (int i = 0; i < OuterSource.Tokens.Length; ++i) {
							if (OuterSource.Tokens[i] == message.SourceToken) {
								FirstHighlightedToken = i;
								LastHighlightedToken = i;
								break;
							}
						}
					} else if (message.SourceStatement.Tokens.Length > 0) {
						for (int i = 0; i < OuterSource.Tokens.Length; ++i) {
							if (OuterSource.Tokens[i] == message.SourceStatement.Tokens[0]) {
								FirstHighlightedToken = i;
							}
							if (OuterSource.Tokens[i] == message.SourceStatement.Tokens[message.SourceStatement.Tokens.Length - 1]) {
								LastHighlightedToken = i;
							}
						}
					}

					OutputErrorsWriter.WriteStartElement("pre");
					OutputErrorsWriter.WriteAttributeString("class", "code");

					for (int i = 0; i < OuterSource.Tokens.Length; ++i) {
						TokenisedSource.Token Token = OuterSource.Tokens[i];

						if (i == FirstHighlightedToken) {
							OutputErrorsWriter.WriteStartElement("span");
							OutputErrorsWriter.WriteAttributeString("class", "highlighted " + type);
						}

						if (!(Token.Type == TokenisedSource.Token.TokenTypes.Seperator && Token.Data.Trim() == "")) {
							OutputErrorsWriter.WriteStartElement("span");
							OutputErrorsWriter.WriteAttributeString("class", Token.Type.ToString().ToLowerInvariant());
							OutputErrorsWriter.WriteValue(Token.Data);
							OutputErrorsWriter.WriteEndElement();
						}

						if (i == LastHighlightedToken) {
							OutputErrorsWriter.WriteEndElement();
						}
					}

					OutputErrorsWriter.WriteEndElement(); // </pre>
				}
				if (!string.IsNullOrEmpty(message.Filename)) {
					string ErrorSource = string.Format(@"<a href=""file_{0}"">{1}</a>", message.Filename, message.Compiler.GetRelativeFilename(message.Filename));
					if (message.LineNumber > 0) ErrorSource += ", line " + message.LineNumber.ToString();
					OutputErrorsWriter.WriteRaw("<p>Source: " + ErrorSource + ".</p>");
				}

				OutputErrorsWriter.WriteEndElement(); // </div>
				OutputErrorsWriter.Flush();
				var Data = OutputMessage.ToArray();

				//HACK: Overwrite the BOM with spaces characters.
				Data[0] = 0x20;
				Data[1] = 0x20;
				Data[2] = 0x20;

				toAppendTo.Append(Encoding.UTF8.GetString(Data));
			}
		}

		
		#endregion

		#region Temporary Images

		/// <summary>
		/// Stores an image in the user's temporary directory.
		/// </summary>
		/// <param name="name">The identifying name of the image resource to save (key).</param>
		/// <param name="image">The image to store.</param>
		private void StoreTemporaryImage(string name, Bitmap image) {
			string TempImage = Path.GetTempFileName();
			image.Save(TempImage, image.RawFormat);
			this.TemporaryImages.Add(name, TempImage);
		}

		/// <summary>
		/// Deletes all images stored in the user's temporary directory stored there by <see cref="StoreTemporaryImage"/>.
		/// </summary>
		private void CleanUpTemporaryImages() {
			foreach (KeyValuePair<string, string> Image in this.TemporaryImages) {
				try {
					File.Delete(Image.Value);
				} catch { }
			}
		}

		#endregion
	}
}
