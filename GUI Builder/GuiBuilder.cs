using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Brass3;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Media;

namespace GuiBuilder {
	public partial class GuiBuilder : Form {

		private readonly Compiler Compiler;

		private StringBuilder OutputMessages;

		private MemoryStream OutputErrors;
		private XmlWriter OutputErrorsWriter;

		private int ErrorCount = 0;
		private int WarningCount = 0;

		private Dictionary<string, string> TemporaryImages;

		

		public GuiBuilder(Compiler compiler) {

			this.Compiler = compiler;

			InitializeComponent();
			this.Text = Application.ProductName;

			this.TemporaryImages = new Dictionary<string, string>();
			this.StoreTemporaryImage("UnderlineError", Properties.Resources.UnderlineError);
			this.StoreTemporaryImage("UnderlineWarning", Properties.Resources.UnderlineWarning);
			this.StoreTemporaryImage("IconError", Properties.Resources.IconError);
			this.StoreTemporaryImage("IconWarning", Properties.Resources.IconWarning);
			this.StoreTemporaryImage("IconMessages", Properties.Resources.IconMessage);


			this.Compiler.WarningRaised += delegate(object sender, Compiler.NotificationEventArgs e) { ++this.WarningCount; this.AppendMessage("warning", e); };
			this.Compiler.ErrorRaised += delegate(object sender, Compiler.NotificationEventArgs e) { ++this.ErrorCount; this.AppendMessage("error", e); };
			this.Compiler.MessageRaised += delegate(object sender, Compiler.NotificationEventArgs e) { this.OutputMessages.Append(e.Message); };

			this.Disposed += delegate(object sender, EventArgs e) {
				this.CleanUpTemporaryImages();
			};
		}

		#region Temporary Images

		private void StoreTemporaryImage(string name, Bitmap image) {
			string TempImage = Path.GetTempFileName();
			image.Save(TempImage, image.RawFormat);
			this.TemporaryImages.Add(name, TempImage);
		}

		private void CleanUpTemporaryImages() {
			foreach (KeyValuePair<string, string> Image in this.TemporaryImages) {
				try {
					File.Delete(Image.Value);
				} catch { }
			}
		}

		#endregion

	

		private void GuiBuilder_Load(object sender, EventArgs e) {
			try {
				this.OutputMessages = new StringBuilder(1024);
				this.OutputErrors = new MemoryStream(1024);
				XmlWriterSettings Settings = new  XmlWriterSettings();
				Settings.Indent = false;
				Settings.Encoding = Encoding.UTF8;
				Settings.OmitXmlDeclaration = true;

				this.OutputErrorsWriter = XmlWriter.Create(OutputErrors, Settings);
				this.OutputErrorsWriter.WriteStartElement("div");
				this.Compiler.Compile(true);

				if (this.OutputMessages.Length != 0) {
					this.OutputErrorsWriter.WriteStartElement("div");
					this.OutputErrorsWriter.WriteAttributeString("class", "messages");
					this.OutputErrorsWriter.WriteElementString("h1", "Messages.");
					this.OutputErrorsWriter.WriteElementString("pre", OutputMessages.ToString());
					this.OutputErrorsWriter.WriteEndElement();
				}

				this.OutputErrorsWriter.WriteEndElement();
				this.OutputErrorsWriter.Flush();

				this.BrowserOutput.DocumentText = "";
				string OutputHtml = Properties.Resources.OutputFormatting;
				foreach (KeyValuePair<string, string> Image in this.TemporaryImages) {
					OutputHtml = OutputHtml.Replace(string.Format("$({0})", Image.Key), Image.Value);
				}
				string OutputData = Encoding.UTF8.GetString(this.OutputErrors.ToArray());
				OutputHtml = OutputHtml.Replace("$(Errors)", OutputData.Trim());
				this.BrowserOutput.Document.Write(OutputHtml);

			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}

			if (this.ErrorCount + this.WarningCount > 0) {
				SystemSounds.Hand.Play();
			} else if (this.OutputMessages.Length > 0) {
				SystemSounds.Beep.Play();
			} else {
				this.Close();
				SystemSounds.Asterisk.Play();
			}
		}
		
		private void AppendMessage(string type, Compiler.NotificationEventArgs message) {

			this.OutputErrorsWriter.WriteStartElement("div");
			this.OutputErrorsWriter.WriteAttributeString("class", type);

			this.OutputErrorsWriter.WriteElementString("h1", message.Message);

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

				this.OutputErrorsWriter.WriteStartElement("pre");
				this.OutputErrorsWriter.WriteAttributeString("class", "code");

				for (int i = 0; i < OuterSource.Tokens.Length; ++i) {
					TokenisedSource.Token Token = OuterSource.Tokens[i];

					if (i == FirstHighlightedToken) {
						this.OutputErrorsWriter.WriteStartElement("span");
						this.OutputErrorsWriter.WriteAttributeString("class", "highlighted " + type);
					}

					if (!(Token.Type == TokenisedSource.Token.TokenTypes.Seperator && Token.Data.Trim() == "")) {
						this.OutputErrorsWriter.WriteStartElement("span");
						this.OutputErrorsWriter.WriteAttributeString("class", Token.Type.ToString().ToLowerInvariant());
						this.OutputErrorsWriter.WriteValue(Token.Data);
						this.OutputErrorsWriter.WriteEndElement();
					}

					if (i == LastHighlightedToken) {
						this.OutputErrorsWriter.WriteEndElement();
					}
				}

				this.OutputErrorsWriter.WriteEndElement(); // </pre>
			}
			if (!string.IsNullOrEmpty(message.Filename)) {
				string ErrorSource = string.Format(@"<a href=""file_{0}"">{1}</a>", message.Filename, Compiler.GetRelativeFilename(message.Filename));
				if (message.LineNumber > 0) ErrorSource += ", line " + message.LineNumber.ToString();
				this.OutputErrorsWriter.WriteRaw("<p>Source: " + ErrorSource + ".</p>");
			}

			this.OutputErrorsWriter.WriteEndElement(); // </div>
		}

		private void BrowserOutput_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			if (e.Url.LocalPath.StartsWith("file_")) {
				try {
					Process.Start(e.Url.LocalPath.Substring(5));
				} catch (Exception ex) {
					MessageBox.Show(this, "Could not open file:" + Environment.NewLine + ex.Message, "File Open", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				e.Cancel = true;
			}
		}
	}
}