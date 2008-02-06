using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BeeDevelopment.Brass3;

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
		/// Gets the most recently used compiler instance.
		/// </summary>
		private Compiler LastCompiler;

		/// <summary>
		/// Dictionary mapping temporary image identifiers to filenames.
		/// </summary>
		private Dictionary<string, string> TemporaryImages;

		public ProjectBuilderInterface(string[] args) {
			InitializeComponent();
			this.LoadSettings();
			this.Text = Application.ProductName;
			this.MenuHelp_DropDownOpening(this, null);

			this.TemporaryImages = new Dictionary<string, string>();
			this.StoreTemporaryImage("UnderlineError", Properties.Resources.UnderlineError);
			this.StoreTemporaryImage("UnderlineWarning", Properties.Resources.UnderlineWarning);
			this.StoreTemporaryImage("IconError", Properties.Resources.IconError);
			this.StoreTemporaryImage("IconWarning", Properties.Resources.IconWarning);
			this.StoreTemporaryImage("IconMessages", Properties.Resources.IconMessage);


			string ErrorText = Properties.Resources.OutputFormatting;
			foreach (KeyValuePair<string, string> Image in this.TemporaryImages) {
				ErrorText = ErrorText.Replace(string.Format("$({0})", Image.Key), Image.Value);
			}
			this.BrowserOutput.DocumentText = ErrorText;

			this.Disposed += (sender, e) => this.CleanUpTemporaryImages();
			this.FormClosing += (sender, e) => this.SaveSettings();

			this.WorkingProject = null;
			this.WorkingProject = null;

			if (args.Length >= 1) {
				LoadProject(args[0]);
			}
			if (args.Length >= 2) {
				this.CurrentConfiguration = args[1];
			}
		}

		private void MenuExit_Click(object sender, EventArgs e) {
			this.Close();
		}


		#region Project Loading and Editing

		private void MenuOpenProject_Click(object sender, EventArgs e) {
			if (this.OpenProjectDialog.ShowDialog(this) == DialogResult.OK) {
				LoadProject(this.OpenProjectDialog.FileName);
			}
		}

		private void LoadProject(string filename) {
			this.CurrentConfiguration = null;
			this.WorkingProject = new Project();
			try {
				this.WorkingProject.Load(filename);
			} catch (Exception ex) {
				MessageBox.Show(this, "Error loading project:" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.WorkingProject = null;
			} finally {
				this.MenuBuild_DropDownOpening(this, null);
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
				var Configurations = this.WorkingProject.GetBuildConfigurationNames();
				if (Configurations.Length == 0) {
					MenuRebuild.Enabled = true;
					this.CurrentConfiguration = "";
				} else {
					MenuBuild.DropDownItems.Add(new ToolStripSeparator());
					foreach (var Configuration in Configurations) {
						if (this.CurrentConfiguration == null) this.CurrentConfiguration = Configuration.Key;
						ToolStripMenuItem ConfigurationButton = new ToolStripMenuItem(Configuration.Value);
						ConfigurationButton.Tag = Configuration.Key;
						ConfigurationButton.Checked = this.CurrentConfiguration == (ConfigurationButton.Tag as string);
						ConfigurationButton.Click += (ls, le) => {
							this.CurrentConfiguration = ConfigurationButton.Tag as string;
							this.MenuBuild_DropDownOpening(this, null);
							//this.Build();
						};
						MenuBuild.DropDownItems.Add(ConfigurationButton);
						MenuRebuild.Enabled = CurrentConfiguration != null;
					}
				}
			}

			this.MenuStartDebugging.Enabled = this.MenuRebuild.Enabled;

		}

		#endregion



		/// <summary>
		/// Builds the currently selected configuration.
		/// </summary>
		private bool Build() {

			if (this.CurrentConfiguration == null) {
				MessageBox.Show(this, "No build configuration selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			this.BrowserOutput.Document.Body.InnerHtml = "";
			Application.DoEvents();

			this.LastCompiler = new Compiler();
			this.LastCompiler.LoadProject(string.IsNullOrEmpty(this.CurrentConfiguration) ? this.WorkingProject : this.WorkingProject.GetBuildConfiguration(this.CurrentConfiguration));

			int WarningCount = 0, ErrorCount = 0;
			var OutputMessages = new StringBuilder(1024);
			var OutputErrors = new StringBuilder(1024);

			this.LastCompiler.WarningRaised += delegate(object sender, Compiler.NotificationEventArgs e) { ++WarningCount; this.AppendMessage("warning", e); };
			this.LastCompiler.ErrorRaised += delegate(object sender, Compiler.NotificationEventArgs e) { ++ErrorCount; this.AppendMessage("error", e); };
			this.LastCompiler.MessageRaised += delegate(object sender, Compiler.NotificationEventArgs e) { OutputMessages.Append(e.Message); };

			this.LastCompiler.Compile(true);


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

					this.BrowserOutput.Document.Body.InnerHtml += Encoding.UTF8.GetString(Data);
				}
			}


			this.BrowserOutput.Document.Body.InnerHtml += string.Format("<p><b>Build completed:</b> {0} error{1}, {2} warning{3}.</p>", ErrorCount, ErrorCount == 1 ? "" : "s", WarningCount, WarningCount == 1 ? "" : "s");

			// Sound effects:
			if (Properties.Settings.Default.SoundEnabled) {
				if (ErrorCount + WarningCount > 0) {
					SystemSounds.Hand.Play();
				} else if (OutputMessages.Length > 0) {
					SystemSounds.Beep.Play();
				} else {
					SystemSounds.Asterisk.Play();
				}
			}

			return ErrorCount == 0;

		}

		/// <summary>
		/// Turns an error message or warning into HTML and appends it to a <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="toAppendTo">The <see cref="StringBuilder"/> to append the error message to.</param>
		/// <param name="type">The type (error or warning).</param>
		/// <param name="message">The message to append.</param>
		private void AppendMessage(string type, Compiler.NotificationEventArgs message) {

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

				string ErrorOut = Encoding.UTF8.GetString(Data);
				this.BrowserOutput.Document.Body.InnerHtml += ErrorOut;
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

		#region Options

		/// <summary>
		/// Load settings from settings file.
		/// </summary>
		private void LoadSettings() {
			this.TopMost = Properties.Settings.Default.WindowTopmost;

			if (Properties.Settings.Default.WindowWidth == -1 || Properties.Settings.Default.WindowHeight == -1) {
				this.CenterToScreen();
				this.ProjectBuilderInterface_Move(this, null);
				this.ProjectBuilderInterface_Resize(this, null);
			} else {
				this.ClientSize = new Size(Properties.Settings.Default.WindowWidth, Properties.Settings.Default.WindowHeight);
				this.Location = new Point(Properties.Settings.Default.WindowLeft, Properties.Settings.Default.WindowTop);
				this.WindowState = Properties.Settings.Default.WindowState;
			}

		}

		/// <summary>
		/// Save settings to settings file.
		/// </summary>
		private void SaveSettings() {
			if (this.WindowState != FormWindowState.Minimized) Properties.Settings.Default.WindowState = this.WindowState;
			Properties.Settings.Default.Save();
		}

		private void MenuAlwaysOnTop_Click(object sender, EventArgs e) {
			this.TopMost ^= true;
			Properties.Settings.Default.WindowTopmost = this.TopMost;
		}

		private void MenuSound_Click(object sender, EventArgs e) {
			Properties.Settings.Default.SoundEnabled ^= true;
		}

		private void MenuOptions_DropDownOpening(object sender, EventArgs e) {
			this.MenuAlwaysOnTop.Checked = this.TopMost;
			this.MenuSound.Image = Properties.Settings.Default.SoundEnabled ? Properties.Resources.IconSound : Properties.Resources.IconSoundMute;
		}
		
		private void ProjectBuilderInterface_Resize(object sender, EventArgs e) {
			if (this.WindowState == FormWindowState.Normal) {
				Properties.Settings.Default.WindowWidth = this.ClientSize.Width;
				Properties.Settings.Default.WindowHeight = this.ClientSize.Height;
			}
		}

		private void ProjectBuilderInterface_Move(object sender, EventArgs e) {
			if (this.WindowState == FormWindowState.Normal) {
				Properties.Settings.Default.WindowTop = this.Top;
				Properties.Settings.Default.WindowLeft = this.Left;
			}
		}

		#endregion

		private void BrowserOutput_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			if (e.Url.LocalPath.StartsWith("file_")) {
				try {
					Process.Start(e.Url.LocalPath.Substring(5));
				} catch (Exception ex) {
					MessageBox.Show(this, "Could not open file:" + Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				e.Cancel = true;
			}
		}

		private void MenuAbout_Click(object sender, EventArgs e) {
			this.BrowserOutput.Document.Body.InnerHtml = Properties.Resources.AboutDialog.Replace("$(AppName)", Application.ProductName).Replace("$(AppVersion)", Application.ProductVersion);
		}

		private void MenuDebug_DropDownOpening(object sender, EventArgs e) {
			this.MenuBuild_DropDownOpening(sender, e);
		}

		private void MenuStartDebugging_Click(object sender, EventArgs e) {
			if (this.Build()) {
				if (this.LastCompiler.Debugger == null) {
					MessageBox.Show(this, "No debugger is available for this project.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.LastCompiler.Debugger.Start(this.LastCompiler, true);
			}
		}

		private void BrowserOutput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			foreach (ToolStripMenuItem Menu in this.Menus.Items) {
				foreach (var SubMenu in Menu.DropDownItems) {
					if (SubMenu is ToolStripMenuItem) {
						if ((SubMenu as ToolStripMenuItem).ShortcutKeys == e.KeyData) {
							e.IsInputKey = false;
							(SubMenu as ToolStripMenuItem).PerformClick();
						}
					}
				}
			}
		}
	}
}
