﻿namespace ProjectBuilder {
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
			this.Menus = new System.Windows.Forms.MenuStrip();
			this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuOpenProject = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuFileSep0 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuProject = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuProjectProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBuild = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuRebuild = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBrassHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.BrowserOutput = new System.Windows.Forms.WebBrowser();
			this.OpenProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// Menus
			// 
			this.Menus.Location = new System.Drawing.Point(0, 0);
			this.Menus.Name = "Menus";
			this.Menus.Size = new System.Drawing.Size(284, 24);
			this.Menus.TabIndex = 0;
			this.Menus.Text = "";
			this.Menus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.MenuFile, this.MenuProject, this.MenuBuild, this.MenuHelp });
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
			// MenuProject
			// 
			this.MenuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuProjectProperties});
			this.MenuProject.Name = "MenuProject";
			this.MenuProject.Size = new System.Drawing.Size(56, 20);
			this.MenuProject.Text = "&Project";
			this.MenuProject.DropDownOpening += new System.EventHandler(this.MenuProject_DropDownOpening);
			// 
			// MenuProjectProperties
			// 
			this.MenuProjectProperties.Name = "MenuProjectProperties";
			this.MenuProjectProperties.Size = new System.Drawing.Size(176, 22);
			this.MenuProjectProperties.Text = "Project &Properties...";
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
			this.MenuRebuild.Name = "MenuRebuild";
			this.MenuRebuild.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.MenuRebuild.Size = new System.Drawing.Size(133, 22);
			this.MenuRebuild.Text = "&Rebuild";
			this.MenuRebuild.Click += new System.EventHandler(this.MenuRebuild_Click);
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
			this.MenuBrassHelp.Name = "MenuBrassHelp";
			this.MenuBrassHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.MenuBrassHelp.Size = new System.Drawing.Size(152, 22);
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
			this.MainMenuStrip = this.Menus;
			this.Name = "ProjectBuilderInterface";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip Menus;
		private System.Windows.Forms.ToolStripMenuItem MenuFile;
		private System.Windows.Forms.ToolStripMenuItem MenuOpenProject;
		private System.Windows.Forms.ToolStripSeparator MenuFileSep0;
		private System.Windows.Forms.ToolStripMenuItem MenuExit;
		private System.Windows.Forms.ToolStripMenuItem MenuProject;
		private System.Windows.Forms.ToolStripMenuItem MenuProjectProperties;
		private System.Windows.Forms.ToolStripMenuItem MenuHelp;
		private System.Windows.Forms.ToolStripMenuItem MenuBrassHelp;
		private System.Windows.Forms.ToolStripMenuItem MenuBuild;
		private System.Windows.Forms.ToolStripMenuItem MenuRebuild;
		private System.Windows.Forms.WebBrowser BrowserOutput;
		private System.Windows.Forms.OpenFileDialog OpenProjectDialog;
	}
}
