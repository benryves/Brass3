namespace Help {
	partial class HelpInterface {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpInterface));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportAsHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.forwardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExportHtmlDialog = new System.Windows.Forms.SaveFileDialog();
			this.HelpViewer = new Help.HelpViewer();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(693, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsHTMLToolStripMenuItem,
            this.toolStripMenuItem2,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exportAsHTMLToolStripMenuItem
			// 
			this.exportAsHTMLToolStripMenuItem.Image = global::Help.Properties.Resources.Icon_PageCode;
			this.exportAsHTMLToolStripMenuItem.Name = "exportAsHTMLToolStripMenuItem";
			this.exportAsHTMLToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.exportAsHTMLToolStripMenuItem.Text = "&Export as HTML...";
			this.exportAsHTMLToolStripMenuItem.Click += new System.EventHandler(this.exportAsHTMLToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Image = global::Help.Properties.Resources.Icon_Printer;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.printToolStripMenuItem.Text = "&Print";
			this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view...";
			this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.forwardsToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			this.viewToolStripMenuItem.DropDownOpening += new System.EventHandler(this.viewToolStripMenuItem_DropDownOpening);
			// 
			// backToolStripMenuItem
			// 
			this.backToolStripMenuItem.Image = global::Help.Properties.Resources.Icon_Back;
			this.backToolStripMenuItem.Name = "backToolStripMenuItem";
			this.backToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.backToolStripMenuItem.Text = "&Back";
			this.backToolStripMenuItem.Click += new System.EventHandler(this.backToolStripMenuItem_Click);
			// 
			// forwardsToolStripMenuItem
			// 
			this.forwardsToolStripMenuItem.Image = global::Help.Properties.Resources.Icon_Forwards;
			this.forwardsToolStripMenuItem.Name = "forwardsToolStripMenuItem";
			this.forwardsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.forwardsToolStripMenuItem.Text = "&Forwards";
			this.forwardsToolStripMenuItem.Click += new System.EventHandler(this.forwardsToolStripMenuItem_Click);
			// 
			// ExportHtmlDialog
			// 
			this.ExportHtmlDialog.FileName = "index.html";
			this.ExportHtmlDialog.Filter = "HTML Files (*.html)|*.html";
			// 
			// HelpViewer
			// 
			this.HelpViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HelpViewer.HelpProvider = null;
			this.HelpViewer.Location = new System.Drawing.Point(0, 24);
			this.HelpViewer.Name = "HelpViewer";
			this.HelpViewer.Size = new System.Drawing.Size(693, 388);
			this.HelpViewer.TabIndex = 0;
			// 
			// HelpInterface
			// 
			this.ClientSize = new System.Drawing.Size(693, 412);
			this.Controls.Add(this.HelpViewer);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "HelpInterface";
			this.Load += new System.EventHandler(this.HelpInterface_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private HelpViewer HelpViewer;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportAsHTMLToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog ExportHtmlDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem forwardsToolStripMenuItem;

	}
}

