namespace Help {
	partial class HelpViewer {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.BrowserViewerSplit = new System.Windows.Forms.SplitContainer();
			this.Contents = new System.Windows.Forms.TreeView();
			this.IconImages = new System.Windows.Forms.ImageList(this.components);
			this.ViewerBorder = new System.Windows.Forms.Panel();
			this.Viewer = new System.Windows.Forms.WebBrowser();
			this.ViewerContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ViewerContextCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.BrowserViewerSplit.Panel1.SuspendLayout();
			this.BrowserViewerSplit.Panel2.SuspendLayout();
			this.BrowserViewerSplit.SuspendLayout();
			this.ViewerBorder.SuspendLayout();
			this.ViewerContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// BrowserViewerSplit
			// 
			this.BrowserViewerSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserViewerSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.BrowserViewerSplit.Location = new System.Drawing.Point(0, 0);
			this.BrowserViewerSplit.Name = "BrowserViewerSplit";
			// 
			// BrowserViewerSplit.Panel1
			// 
			this.BrowserViewerSplit.Panel1.Controls.Add(this.Contents);
			// 
			// BrowserViewerSplit.Panel2
			// 
			this.BrowserViewerSplit.Panel2.Controls.Add(this.ViewerBorder);
			this.BrowserViewerSplit.Size = new System.Drawing.Size(320, 150);
			this.BrowserViewerSplit.SplitterDistance = 175;
			this.BrowserViewerSplit.TabIndex = 0;
			// 
			// Contents
			// 
			this.Contents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Contents.HideSelection = false;
			this.Contents.ImageIndex = 0;
			this.Contents.ImageList = this.IconImages;
			this.Contents.ItemHeight = 18;
			this.Contents.Location = new System.Drawing.Point(0, 0);
			this.Contents.Name = "Contents";
			this.Contents.SelectedImageIndex = 0;
			this.Contents.Size = new System.Drawing.Size(175, 150);
			this.Contents.TabIndex = 1;
			this.Contents.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.Contents_AfterCollapse);
			this.Contents.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Contents_AfterSelect);
			this.Contents.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.Contents_AfterExpand);
			// 
			// IconImages
			// 
			this.IconImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.IconImages.ImageSize = new System.Drawing.Size(16, 16);
			this.IconImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ViewerBorder
			// 
			this.ViewerBorder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ViewerBorder.Controls.Add(this.Viewer);
			this.ViewerBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ViewerBorder.Location = new System.Drawing.Point(0, 0);
			this.ViewerBorder.Name = "ViewerBorder";
			this.ViewerBorder.Padding = new System.Windows.Forms.Padding(1);
			this.ViewerBorder.Size = new System.Drawing.Size(141, 150);
			this.ViewerBorder.TabIndex = 1;
			// 
			// Viewer
			// 
			this.Viewer.ContextMenuStrip = this.ViewerContext;
			this.Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Viewer.Location = new System.Drawing.Point(1, 1);
			this.Viewer.MinimumSize = new System.Drawing.Size(20, 20);
			this.Viewer.Name = "Viewer";
			this.Viewer.ScriptErrorsSuppressed = true;
			this.Viewer.Size = new System.Drawing.Size(139, 148);
			this.Viewer.TabIndex = 0;
			this.Viewer.WebBrowserShortcutsEnabled = false;
			this.Viewer.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Viewer_Navigating);
			// 
			// ViewerContext
			// 
			this.ViewerContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewerContextCopy});
			this.ViewerContext.Name = "ViewerContext";
			this.ViewerContext.Size = new System.Drawing.Size(103, 26);
			this.ViewerContext.Opening += new System.ComponentModel.CancelEventHandler(this.ViewerContext_Opening);
			// 
			// ViewerContextCopy
			// 
			this.ViewerContextCopy.Image = global::Help.Properties.Resources.Icon_PageCopy;
			this.ViewerContextCopy.Name = "ViewerContextCopy";
			this.ViewerContextCopy.Size = new System.Drawing.Size(102, 22);
			this.ViewerContextCopy.Text = "&Copy";
			this.ViewerContextCopy.Click += new System.EventHandler(this.ViewerContextCopy_Click);
			// 
			// HelpViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.BrowserViewerSplit);
			this.Name = "HelpViewer";
			this.Size = new System.Drawing.Size(320, 150);
			this.BrowserViewerSplit.Panel1.ResumeLayout(false);
			this.BrowserViewerSplit.Panel2.ResumeLayout(false);
			this.BrowserViewerSplit.ResumeLayout(false);
			this.ViewerBorder.ResumeLayout(false);
			this.ViewerContext.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer BrowserViewerSplit;
		private System.Windows.Forms.TreeView Contents;
		private System.Windows.Forms.WebBrowser Viewer;
		private System.Windows.Forms.ImageList IconImages;
		private System.Windows.Forms.Panel ViewerBorder;
		private System.Windows.Forms.ContextMenuStrip ViewerContext;
		private System.Windows.Forms.ToolStripMenuItem ViewerContextCopy;

	}
}
