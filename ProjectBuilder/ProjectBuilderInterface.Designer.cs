namespace ProjectBuilder {
	partial class ProjectBuilderInterface {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectBuilderInterface));
			this.Menus = new System.Windows.Forms.MenuStrip();
			this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuOpenProject = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuFileSep0 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBuild = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuRebuild = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuSound = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBrassHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.BrowserOutput = new System.Windows.Forms.WebBrowser();
			this.OpenProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this.Menus.SuspendLayout();
			this.SuspendLayout();
			// 
			// Menus
			// 
			this.Menus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuBuild,
            this.MenuOptions,
            this.MenuHelp});
			this.Menus.Location = new System.Drawing.Point(0, 0);
			this.Menus.Name = "Menus";
			this.Menus.Size = new System.Drawing.Size(284, 24);
			this.Menus.TabIndex = 0;
			// 
			// MenuFile
			// 
			this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpenProject,
            this.MenuFileSep0,
            this.MenuExit});
			this.MenuFile.Name = "MenuFile";
			this.MenuFile.Size = new System.Drawing.Size(37, 20);
			this.MenuFile.Text = "&File";
			// 
			// MenuOpenProject
			// 
			this.MenuOpenProject.Image = global::ProjectBuilder.Properties.Resources.IconFolder;
			this.MenuOpenProject.Name = "MenuOpenProject";
			this.MenuOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.MenuOpenProject.Size = new System.Drawing.Size(195, 22);
			this.MenuOpenProject.Text = "&Open Project...";
			this.MenuOpenProject.Click += new System.EventHandler(this.MenuOpenProject_Click);
			// 
			// MenuFileSep0
			// 
			this.MenuFileSep0.Name = "MenuFileSep0";
			this.MenuFileSep0.Size = new System.Drawing.Size(192, 6);
			// 
			// MenuExit
			// 
			this.MenuExit.Name = "MenuExit";
			this.MenuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.MenuExit.Size = new System.Drawing.Size(195, 22);
			this.MenuExit.Text = "E&xit";
			this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
			// 
			// MenuBuild
			// 
			this.MenuBuild.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuRebuild});
			this.MenuBuild.Name = "MenuBuild";
			this.MenuBuild.Size = new System.Drawing.Size(46, 20);
			this.MenuBuild.Text = "&Build";
			this.MenuBuild.DropDownOpening += new System.EventHandler(this.MenuBuild_DropDownOpening);
			// 
			// MenuRebuild
			// 
			this.MenuRebuild.Enabled = false;
			this.MenuRebuild.Image = global::ProjectBuilder.Properties.Resources.IconBuild;
			this.MenuRebuild.Name = "MenuRebuild";
			this.MenuRebuild.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.MenuRebuild.Size = new System.Drawing.Size(152, 22);
			this.MenuRebuild.Text = "&Rebuild";
			this.MenuRebuild.Click += new System.EventHandler(this.MenuRebuild_Click);
			// 
			// MenuOptions
			// 
			this.MenuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAlwaysOnTop,
            this.MenuSound});
			this.MenuOptions.Name = "MenuOptions";
			this.MenuOptions.Size = new System.Drawing.Size(61, 20);
			this.MenuOptions.Text = "&Options";
			this.MenuOptions.DropDownOpening += new System.EventHandler(this.MenuOptions_DropDownOpening);
			// 
			// MenuAlwaysOnTop
			// 
			this.MenuAlwaysOnTop.Name = "MenuAlwaysOnTop";
			this.MenuAlwaysOnTop.Size = new System.Drawing.Size(152, 22);
			this.MenuAlwaysOnTop.Text = "&Always on Top";
			this.MenuAlwaysOnTop.Click += new System.EventHandler(this.MenuAlwaysOnTop_Click);
			// 
			// MenuSound
			// 
			this.MenuSound.Name = "MenuSound";
			this.MenuSound.Size = new System.Drawing.Size(152, 22);
			this.MenuSound.Text = "Status &Sounds";
			this.MenuSound.Click += new System.EventHandler(this.MenuSound_Click);
			// 
			// MenuHelp
			// 
			this.MenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBrassHelp});
			this.MenuHelp.Name = "MenuHelp";
			this.MenuHelp.Size = new System.Drawing.Size(44, 20);
			this.MenuHelp.Text = "&Help";
			this.MenuHelp.DropDownOpening += new System.EventHandler(this.MenuHelp_DropDownOpening);
			// 
			// MenuBrassHelp
			// 
			this.MenuBrassHelp.Enabled = false;
			this.MenuBrassHelp.Image = global::ProjectBuilder.Properties.Resources.IconHelp;
			this.MenuBrassHelp.Name = "MenuBrassHelp";
			this.MenuBrassHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.MenuBrassHelp.Size = new System.Drawing.Size(148, 22);
			this.MenuBrassHelp.Text = "&Brass Help";
			this.MenuBrassHelp.Click += new System.EventHandler(this.MenuBrassHelp_Click);
			// 
			// BrowserOutput
			// 
			this.BrowserOutput.AllowWebBrowserDrop = false;
			this.BrowserOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserOutput.IsWebBrowserContextMenuEnabled = false;
			this.BrowserOutput.Location = new System.Drawing.Point(0, 24);
			this.BrowserOutput.MinimumSize = new System.Drawing.Size(20, 20);
			this.BrowserOutput.Name = "BrowserOutput";
			this.BrowserOutput.ScriptErrorsSuppressed = true;
			this.BrowserOutput.Size = new System.Drawing.Size(284, 240);
			this.BrowserOutput.TabIndex = 1;
			this.BrowserOutput.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.BrowserOutput_Navigating);
			// 
			// OpenProjectDialog
			// 
			this.OpenProjectDialog.Filter = "Brass Projects (*.brassproj)|*.brassproj|All Files (*.*)|*.*";
			// 
			// ProjectBuilderInterface
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.BrowserOutput);
			this.Controls.Add(this.Menus);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.Menus;
			this.Name = "ProjectBuilderInterface";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Move += new System.EventHandler(this.ProjectBuilderInterface_Move);
			this.Resize += new System.EventHandler(this.ProjectBuilderInterface_Resize);
			this.Menus.ResumeLayout(false);
			this.Menus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip Menus;
		private System.Windows.Forms.ToolStripMenuItem MenuFile;
		private System.Windows.Forms.ToolStripMenuItem MenuOpenProject;
		private System.Windows.Forms.ToolStripSeparator MenuFileSep0;
		private System.Windows.Forms.ToolStripMenuItem MenuExit;
		private System.Windows.Forms.ToolStripMenuItem MenuHelp;
		private System.Windows.Forms.ToolStripMenuItem MenuBrassHelp;
		private System.Windows.Forms.ToolStripMenuItem MenuBuild;
		private System.Windows.Forms.ToolStripMenuItem MenuRebuild;
		private System.Windows.Forms.WebBrowser BrowserOutput;
		private System.Windows.Forms.OpenFileDialog OpenProjectDialog;
		private System.Windows.Forms.ToolStripMenuItem MenuOptions;
		private System.Windows.Forms.ToolStripMenuItem MenuAlwaysOnTop;
		private System.Windows.Forms.ToolStripMenuItem MenuSound;
	}
}

