namespace ProjectEditor {
	partial class Editor {
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
			this.ProjectTabs = new System.Windows.Forms.TabControl();
			this.ProjectTabsSource = new System.Windows.Forms.TabPage();
			this.ProjectTabsPlugins = new System.Windows.Forms.TabPage();
			this.PluginCollectionListPadding = new System.Windows.Forms.Panel();
			this.PluginCollectionList = new System.Windows.Forms.TreeView();
			this.CheckBoxImageList = new System.Windows.Forms.ImageList(this.components);
			this.AddPluginCollection = new System.Windows.Forms.Button();
			this.ProjectTabsOutput = new System.Windows.Forms.TabPage();
			this.IconImageList = new System.Windows.Forms.ImageList(this.components);
			this.OpenPluginDialog = new System.Windows.Forms.OpenFileDialog();
			this.captionedFileTextBox1 = new ProjectEditor.CaptionedFileTextBox();
			this.ProjectTabs.SuspendLayout();
			this.ProjectTabsSource.SuspendLayout();
			this.ProjectTabsPlugins.SuspendLayout();
			this.PluginCollectionListPadding.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProjectTabs
			// 
			this.ProjectTabs.Controls.Add(this.ProjectTabsSource);
			this.ProjectTabs.Controls.Add(this.ProjectTabsPlugins);
			this.ProjectTabs.Controls.Add(this.ProjectTabsOutput);
			this.ProjectTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProjectTabs.ImageList = this.IconImageList;
			this.ProjectTabs.Location = new System.Drawing.Point(0, 0);
			this.ProjectTabs.Name = "ProjectTabs";
			this.ProjectTabs.SelectedIndex = 0;
			this.ProjectTabs.Size = new System.Drawing.Size(460, 309);
			this.ProjectTabs.TabIndex = 0;
			// 
			// ProjectTabsSource
			// 
			this.ProjectTabsSource.Controls.Add(this.captionedFileTextBox1);
			this.ProjectTabsSource.Location = new System.Drawing.Point(4, 23);
			this.ProjectTabsSource.Name = "ProjectTabsSource";
			this.ProjectTabsSource.Size = new System.Drawing.Size(452, 282);
			this.ProjectTabsSource.TabIndex = 0;
			this.ProjectTabsSource.Text = "Source";
			this.ProjectTabsSource.UseVisualStyleBackColor = true;
			// 
			// ProjectTabsPlugins
			// 
			this.ProjectTabsPlugins.Controls.Add(this.PluginCollectionListPadding);
			this.ProjectTabsPlugins.Controls.Add(this.AddPluginCollection);
			this.ProjectTabsPlugins.Location = new System.Drawing.Point(4, 23);
			this.ProjectTabsPlugins.Name = "ProjectTabsPlugins";
			this.ProjectTabsPlugins.Padding = new System.Windows.Forms.Padding(0, 2, 1, 0);
			this.ProjectTabsPlugins.Size = new System.Drawing.Size(452, 282);
			this.ProjectTabsPlugins.TabIndex = 1;
			this.ProjectTabsPlugins.Text = "Plugins";
			this.ProjectTabsPlugins.UseVisualStyleBackColor = true;
			// 
			// PluginCollectionListPadding
			// 
			this.PluginCollectionListPadding.Controls.Add(this.PluginCollectionList);
			this.PluginCollectionListPadding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PluginCollectionListPadding.Location = new System.Drawing.Point(0, 2);
			this.PluginCollectionListPadding.Name = "PluginCollectionListPadding";
			this.PluginCollectionListPadding.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.PluginCollectionListPadding.Size = new System.Drawing.Size(451, 257);
			this.PluginCollectionListPadding.TabIndex = 2;
			// 
			// PluginCollectionList
			// 
			this.PluginCollectionList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PluginCollectionList.ImageIndex = 0;
			this.PluginCollectionList.ImageList = this.CheckBoxImageList;
			this.PluginCollectionList.Location = new System.Drawing.Point(0, 0);
			this.PluginCollectionList.Name = "PluginCollectionList";
			this.PluginCollectionList.SelectedImageIndex = 0;
			this.PluginCollectionList.ShowPlusMinus = false;
			this.PluginCollectionList.ShowRootLines = false;
			this.PluginCollectionList.Size = new System.Drawing.Size(451, 254);
			this.PluginCollectionList.TabIndex = 1;
			this.PluginCollectionList.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.PluginCollectionList_BeforeCollapse);
			this.PluginCollectionList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PluginCollectionList_MouseUp);
			// 
			// CheckBoxImageList
			// 
			this.CheckBoxImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.CheckBoxImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.CheckBoxImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// AddPluginCollection
			// 
			this.AddPluginCollection.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.AddPluginCollection.Location = new System.Drawing.Point(0, 259);
			this.AddPluginCollection.Name = "AddPluginCollection";
			this.AddPluginCollection.Size = new System.Drawing.Size(451, 23);
			this.AddPluginCollection.TabIndex = 1;
			this.AddPluginCollection.Text = "&Add Plugin Collection";
			this.AddPluginCollection.UseVisualStyleBackColor = true;
			this.AddPluginCollection.Click += new System.EventHandler(this.AddPluginCollection_Click);
			// 
			// ProjectTabsOutput
			// 
			this.ProjectTabsOutput.Location = new System.Drawing.Point(4, 23);
			this.ProjectTabsOutput.Name = "ProjectTabsOutput";
			this.ProjectTabsOutput.Size = new System.Drawing.Size(452, 282);
			this.ProjectTabsOutput.TabIndex = 2;
			this.ProjectTabsOutput.Text = "Output";
			this.ProjectTabsOutput.UseVisualStyleBackColor = true;
			// 
			// IconImageList
			// 
			this.IconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.IconImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.IconImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// OpenPluginDialog
			// 
			this.OpenPluginDialog.Filter = "Plugin Collections (*.dll)|*.dll|All Files (*.*)|*.*";
			this.OpenPluginDialog.Multiselect = true;
			// 
			// captionedFileTextBox1
			// 
			this.captionedFileTextBox1.Caption = "Source File";
			this.captionedFileTextBox1.Location = new System.Drawing.Point(39, 89);
			this.captionedFileTextBox1.Name = "captionedFileTextBox1";
			this.captionedFileTextBox1.Size = new System.Drawing.Size(348, 21);
			this.captionedFileTextBox1.TabIndex = 3;
			// 
			// Editor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ProjectTabs);
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(460, 309);
			this.Load += new System.EventHandler(this.ProjectEditor_Load);
			this.ProjectTabs.ResumeLayout(false);
			this.ProjectTabsSource.ResumeLayout(false);
			this.ProjectTabsPlugins.ResumeLayout(false);
			this.PluginCollectionListPadding.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl ProjectTabs;
		private System.Windows.Forms.TabPage ProjectTabsSource;
		private System.Windows.Forms.TabPage ProjectTabsPlugins;
		private System.Windows.Forms.TabPage ProjectTabsOutput;
		private System.Windows.Forms.Button AddPluginCollection;
		private System.Windows.Forms.OpenFileDialog OpenPluginDialog;
		private System.Windows.Forms.ImageList IconImageList;
		private System.Windows.Forms.Panel PluginCollectionListPadding;
		private System.Windows.Forms.TreeView PluginCollectionList;
		private System.Windows.Forms.ImageList CheckBoxImageList;
		private CaptionedFileTextBox captionedFileTextBox1;
	}
}
