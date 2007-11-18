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
			this.BrowserModeTabs = new System.Windows.Forms.TabControl();
			this.BrowserModeContents = new System.Windows.Forms.TabPage();
			this.Contents = new System.Windows.Forms.TreeView();
			this.IconImages = new System.Windows.Forms.ImageList(this.components);
			this.BrowserModeIndex = new System.Windows.Forms.TabPage();
			this.IndexResults = new System.Windows.Forms.TreeView();
			this.IndexSpacer0 = new System.Windows.Forms.Panel();
			this.IndexSearch = new System.Windows.Forms.TextBox();
			this.IndexLookFor = new System.Windows.Forms.Label();
			this.ViewerBorder = new System.Windows.Forms.Panel();
			this.Viewer = new System.Windows.Forms.WebBrowser();
			this.ViewerContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ViewerContextCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.BrowserViewerSplit.Panel1.SuspendLayout();
			this.BrowserViewerSplit.Panel2.SuspendLayout();
			this.BrowserViewerSplit.SuspendLayout();
			this.BrowserModeTabs.SuspendLayout();
			this.BrowserModeContents.SuspendLayout();
			this.BrowserModeIndex.SuspendLayout();
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
			this.BrowserViewerSplit.Panel1.Controls.Add(this.BrowserModeTabs);
			// 
			// BrowserViewerSplit.Panel2
			// 
			this.BrowserViewerSplit.Panel2.Controls.Add(this.ViewerBorder);
			this.BrowserViewerSplit.Size = new System.Drawing.Size(485, 317);
			this.BrowserViewerSplit.SplitterDistance = 175;
			this.BrowserViewerSplit.TabIndex = 0;
			// 
			// BrowserModeTabs
			// 
			this.BrowserModeTabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.BrowserModeTabs.Controls.Add(this.BrowserModeContents);
			this.BrowserModeTabs.Controls.Add(this.BrowserModeIndex);
			this.BrowserModeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserModeTabs.ImageList = this.IconImages;
			this.BrowserModeTabs.Location = new System.Drawing.Point(0, 0);
			this.BrowserModeTabs.Name = "BrowserModeTabs";
			this.BrowserModeTabs.SelectedIndex = 0;
			this.BrowserModeTabs.Size = new System.Drawing.Size(175, 317);
			this.BrowserModeTabs.TabIndex = 1;
			// 
			// BrowserModeContents
			// 
			this.BrowserModeContents.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.BrowserModeContents.Controls.Add(this.Contents);
			this.BrowserModeContents.ImageIndex = 0;
			this.BrowserModeContents.Location = new System.Drawing.Point(4, 4);
			this.BrowserModeContents.Margin = new System.Windows.Forms.Padding(0);
			this.BrowserModeContents.Name = "BrowserModeContents";
			this.BrowserModeContents.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
			this.BrowserModeContents.Size = new System.Drawing.Size(167, 290);
			this.BrowserModeContents.TabIndex = 0;
			this.BrowserModeContents.Text = "Contents";
			// 
			// Contents
			// 
			this.Contents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Contents.HideSelection = false;
			this.Contents.ImageIndex = 0;
			this.Contents.ImageList = this.IconImages;
			this.Contents.ItemHeight = 18;
			this.Contents.Location = new System.Drawing.Point(0, 0);
			this.Contents.Margin = new System.Windows.Forms.Padding(0);
			this.Contents.Name = "Contents";
			this.Contents.SelectedImageIndex = 0;
			this.Contents.Size = new System.Drawing.Size(165, 288);
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
			// BrowserModeIndex
			// 
			this.BrowserModeIndex.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.BrowserModeIndex.Controls.Add(this.IndexResults);
			this.BrowserModeIndex.Controls.Add(this.IndexSpacer0);
			this.BrowserModeIndex.Controls.Add(this.IndexSearch);
			this.BrowserModeIndex.Controls.Add(this.IndexLookFor);
			this.BrowserModeIndex.ImageIndex = 2;
			this.BrowserModeIndex.Location = new System.Drawing.Point(4, 4);
			this.BrowserModeIndex.Name = "BrowserModeIndex";
			this.BrowserModeIndex.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
			this.BrowserModeIndex.Size = new System.Drawing.Size(167, 290);
			this.BrowserModeIndex.TabIndex = 1;
			this.BrowserModeIndex.Text = "Index";
			// 
			// IndexResults
			// 
			this.IndexResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.IndexResults.FullRowSelect = true;
			this.IndexResults.HideSelection = false;
			this.IndexResults.Indent = 10;
			this.IndexResults.Location = new System.Drawing.Point(0, 42);
			this.IndexResults.Name = "IndexResults";
			this.IndexResults.ShowLines = false;
			this.IndexResults.ShowPlusMinus = false;
			this.IndexResults.ShowRootLines = false;
			this.IndexResults.Size = new System.Drawing.Size(165, 246);
			this.IndexResults.TabIndex = 1;
			this.IndexResults.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.IndexResults_BeforeCollapse);
			this.IndexResults.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.IndexResults_AfterSelect);
			// 
			// IndexSpacer0
			// 
			this.IndexSpacer0.BackColor = System.Drawing.Color.Transparent;
			this.IndexSpacer0.Dock = System.Windows.Forms.DockStyle.Top;
			this.IndexSpacer0.Location = new System.Drawing.Point(0, 39);
			this.IndexSpacer0.Name = "IndexSpacer0";
			this.IndexSpacer0.Size = new System.Drawing.Size(165, 3);
			this.IndexSpacer0.TabIndex = 1;
			// 
			// IndexSearch
			// 
			this.IndexSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.IndexSearch.Location = new System.Drawing.Point(0, 19);
			this.IndexSearch.Name = "IndexSearch";
			this.IndexSearch.Size = new System.Drawing.Size(165, 20);
			this.IndexSearch.TabIndex = 1;
			this.IndexSearch.TextChanged += new System.EventHandler(this.IndexSearch_TextChanged);
			// 
			// IndexLookFor
			// 
			this.IndexLookFor.AutoSize = true;
			this.IndexLookFor.Dock = System.Windows.Forms.DockStyle.Top;
			this.IndexLookFor.Location = new System.Drawing.Point(0, 0);
			this.IndexLookFor.Name = "IndexLookFor";
			this.IndexLookFor.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.IndexLookFor.Size = new System.Drawing.Size(49, 19);
			this.IndexLookFor.TabIndex = 2;
			this.IndexLookFor.Text = "Look for:";
			// 
			// ViewerBorder
			// 
			this.ViewerBorder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ViewerBorder.Controls.Add(this.Viewer);
			this.ViewerBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ViewerBorder.Location = new System.Drawing.Point(0, 0);
			this.ViewerBorder.Name = "ViewerBorder";
			this.ViewerBorder.Padding = new System.Windows.Forms.Padding(1);
			this.ViewerBorder.Size = new System.Drawing.Size(306, 317);
			this.ViewerBorder.TabIndex = 1;
			// 
			// Viewer
			// 
			this.Viewer.AllowWebBrowserDrop = false;
			this.Viewer.ContextMenuStrip = this.ViewerContext;
			this.Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Viewer.IsWebBrowserContextMenuEnabled = false;
			this.Viewer.Location = new System.Drawing.Point(1, 1);
			this.Viewer.MinimumSize = new System.Drawing.Size(20, 20);
			this.Viewer.Name = "Viewer";
			this.Viewer.ScriptErrorsSuppressed = true;
			this.Viewer.Size = new System.Drawing.Size(304, 315);
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
			this.Size = new System.Drawing.Size(485, 317);
			this.BrowserViewerSplit.Panel1.ResumeLayout(false);
			this.BrowserViewerSplit.Panel2.ResumeLayout(false);
			this.BrowserViewerSplit.ResumeLayout(false);
			this.BrowserModeTabs.ResumeLayout(false);
			this.BrowserModeContents.ResumeLayout(false);
			this.BrowserModeIndex.ResumeLayout(false);
			this.BrowserModeIndex.PerformLayout();
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
		private System.Windows.Forms.TabControl BrowserModeTabs;
		private System.Windows.Forms.TabPage BrowserModeContents;
		private System.Windows.Forms.TabPage BrowserModeIndex;
		private System.Windows.Forms.TextBox IndexSearch;
		private System.Windows.Forms.Label IndexLookFor;
		private System.Windows.Forms.TreeView IndexResults;
		private System.Windows.Forms.Panel IndexSpacer0;

	}
}
