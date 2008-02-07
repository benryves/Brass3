namespace PindurTI {
	partial class HelpForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
			this.HelpBrowser = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// HelpBrowser
			// 
			this.HelpBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HelpBrowser.IsWebBrowserContextMenuEnabled = false;
			this.HelpBrowser.Location = new System.Drawing.Point(0, 0);
			this.HelpBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.HelpBrowser.Name = "HelpBrowser";
			this.HelpBrowser.ScriptErrorsSuppressed = true;
			this.HelpBrowser.Size = new System.Drawing.Size(479, 323);
			this.HelpBrowser.TabIndex = 0;
			this.HelpBrowser.WebBrowserShortcutsEnabled = false;
			// 
			// HelpForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(479, 323);
			this.Controls.Add(this.HelpBrowser);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "HelpForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Help";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser HelpBrowser;
	}
}