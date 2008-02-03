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
			this.MenuHelpSep0 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this.BrowserOutputBorder = new System.Windows.Forms.Panel();
			this.BrowserOutput = new System.Windows.Forms.WebBrowser();
			this.Menus.SuspendLayout();
			this.BrowserOutputBorder.SuspendLayout();
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
			this.Menus.Size = new System.Drawing.Size(412, 24);
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
			this.MenuRebuild.Size = new System.Drawing.Size(133, 22);
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
            this.MenuBrassHelp,
            this.MenuHelpSep0,
            this.MenuAbout});
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
			this.MenuBrassHelp.Size = new System.Drawing.Size(152, 22);
			this.MenuBrassHelp.Text = "&Brass Help";
			this.MenuBrassHelp.Click += new System.EventHandler(this.MenuBrassHelp_Click);
			// 
			// MenuHelpSep0
			// 
			this.MenuHelpSep0.Name = "MenuHelpSep0";
			this.MenuHelpSep0.Size = new System.Drawing.Size(149, 6);
			// 
			// MenuAbout
			// 
			this.MenuAbout.Name = "MenuAbout";
			this.MenuAbout.Size = new System.Drawing.Size(152, 22);
			this.MenuAbout.Text = "About";
			this.MenuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
			// 
			// OpenProjectDialog
			// 
			this.OpenProjectDialog.Filter = "Brass Projects (*.brassproj)|*.brassproj|All Files (*.*)|*.*";
			// 
			// BrowserOutputBorder
			// 
			this.BrowserOutputBorder.BackColor = System.Drawing.SystemColors.ControlDark;
			this.BrowserOutputBorder.Controls.Add(this.BrowserOutput);
			this.BrowserOutputBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserOutputBorder.Location = new System.Drawing.Point(0, 24);
			this.BrowserOutputBorder.Name = "BrowserOutputBorder";
			this.BrowserOutputBorder.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.BrowserOutputBorder.Size = new System.Drawing.Size(412, 213);
			this.BrowserOutputBorder.TabIndex = 2;
			// 
			// BrowserOutput
			// 
			this.BrowserOutput.AllowWebBrowserDrop = false;
			this.BrowserOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserOutput.IsWebBrowserContextMenuEnabled = false;
			this.BrowserOutput.Location = new System.Drawing.Point(0, 1);
			this.BrowserOutput.MinimumSize = new System.Drawing.Size(20, 20);
			this.BrowserOutput.Name = "BrowserOutput";
			this.BrowserOutput.ScriptErrorsSuppressed = true;
			this.BrowserOutput.Size = new System.Drawing.Size(412, 212);
			this.BrowserOutput.TabIndex = 2;
			// 
			// ProjectBuilderInterface
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(412, 237);
			this.Controls.Add(this.BrowserOutputBorder);
			this.Controls.Add(this.Menus);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.Menus;
			this.Name = "ProjectBuilderInterface";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Move += new System.EventHandler(this.ProjectBuilderInterface_Move);
			this.Resize += new System.EventHandler(this.ProjectBuilderInterface_Resize);
			this.Menus.ResumeLayout(false);
			this.Menus.PerformLayout();
			this.BrowserOutputBorder.ResumeLayout(false);
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
		private System.Windows.Forms.OpenFileDialog OpenProjectDialog;
		private System.Windows.Forms.ToolStripMenuItem MenuOptions;
		private System.Windows.Forms.ToolStripMenuItem MenuAlwaysOnTop;
		private System.Windows.Forms.ToolStripMenuItem MenuSound;
		private System.Windows.Forms.Panel BrowserOutputBorder;
		private System.Windows.Forms.WebBrowser BrowserOutput;
		private System.Windows.Forms.ToolStripSeparator MenuHelpSep0;
		private System.Windows.Forms.ToolStripMenuItem MenuAbout;
	}
}

