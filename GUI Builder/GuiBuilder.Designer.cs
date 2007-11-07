namespace GuiBuilder {
	partial class GuiBuilder {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuiBuilder));
			this.BrowserOutput = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// BrowserOutput
			// 
			this.BrowserOutput.AllowWebBrowserDrop = false;
			this.BrowserOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserOutput.IsWebBrowserContextMenuEnabled = false;
			this.BrowserOutput.Location = new System.Drawing.Point(0, 0);
			this.BrowserOutput.MinimumSize = new System.Drawing.Size(20, 20);
			this.BrowserOutput.Name = "BrowserOutput";
			this.BrowserOutput.ScriptErrorsSuppressed = true;
			this.BrowserOutput.Size = new System.Drawing.Size(495, 212);
			this.BrowserOutput.TabIndex = 0;
			this.BrowserOutput.WebBrowserShortcutsEnabled = false;
			this.BrowserOutput.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.BrowserOutput_Navigating);
			// 
			// GuiBuilder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(495, 212);
			this.Controls.Add(this.BrowserOutput);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GuiBuilder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.GuiBuilder_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser BrowserOutput;
	}
}

