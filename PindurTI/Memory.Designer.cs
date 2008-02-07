namespace PindurTI {
    partial class Memory {
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
			this.components = new System.ComponentModel.Container();
			this.MemoryWindowMenu = new System.Windows.Forms.MenuStrip();
			this.JumpToLabelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.AddressJump = new System.Windows.Forms.ToolStripTextBox();
			this.aSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addBreakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditorPanel = new System.Windows.Forms.Panel();
			this.MemoryWindowMenu.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MemoryWindowMenu
			// 
			this.MemoryWindowMenu.AllowMerge = false;
			this.MemoryWindowMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.MemoryWindowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JumpToLabelMenu,
            this.AddressJump,
            this.aSCIIToolStripMenuItem});
			this.MemoryWindowMenu.Location = new System.Drawing.Point(0, 182);
			this.MemoryWindowMenu.Name = "MemoryWindowMenu";
			this.MemoryWindowMenu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.MemoryWindowMenu.Size = new System.Drawing.Size(352, 24);
			this.MemoryWindowMenu.TabIndex = 3;
			this.MemoryWindowMenu.Text = "menuStrip1";
			// 
			// JumpToLabelMenu
			// 
			this.JumpToLabelMenu.Name = "JumpToLabelMenu";
			this.JumpToLabelMenu.Size = new System.Drawing.Size(93, 22);
			this.JumpToLabelMenu.Text = "&Jump to Label";
			this.JumpToLabelMenu.DropDownOpening += new System.EventHandler(this.JumpToLabelMenu_DropDownOpening);
			// 
			// AddressJump
			// 
			this.AddressJump.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.AddressJump.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AddressJump.MaxLength = 4;
			this.AddressJump.Name = "AddressJump";
			this.AddressJump.Size = new System.Drawing.Size(36, 22);
			this.AddressJump.Text = "0000";
			this.AddressJump.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.AddressJump.Leave += new System.EventHandler(this.AddressJump_Leave);
			this.AddressJump.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddressJump_KeyDown);
			this.AddressJump.Enter += new System.EventHandler(this.AddressJump_Enter);
			// 
			// aSCIIToolStripMenuItem
			// 
			this.aSCIIToolStripMenuItem.Checked = true;
			this.aSCIIToolStripMenuItem.CheckOnClick = true;
			this.aSCIIToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.aSCIIToolStripMenuItem.Name = "aSCIIToolStripMenuItem";
			this.aSCIIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.A)));
			this.aSCIIToolStripMenuItem.Size = new System.Drawing.Size(47, 22);
			this.aSCIIToolStripMenuItem.Text = "&ASCII";
			this.aSCIIToolStripMenuItem.Click += new System.EventHandler(this.aSCIIToolStripMenuItem_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBreakpointToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(157, 48);
			// 
			// addBreakpointToolStripMenuItem
			// 
			this.addBreakpointToolStripMenuItem.Image = global::PindurTI.Properties.Resources.IconAsterisk;
			this.addBreakpointToolStripMenuItem.Name = "addBreakpointToolStripMenuItem";
			this.addBreakpointToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.addBreakpointToolStripMenuItem.Text = "Add &Breakpoint";
			this.addBreakpointToolStripMenuItem.Click += new System.EventHandler(this.addBreakpointToolStripMenuItem_Click);
			// 
			// EditorPanel
			// 
			this.EditorPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EditorPanel.Location = new System.Drawing.Point(0, 0);
			this.EditorPanel.Name = "EditorPanel";
			this.EditorPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.EditorPanel.Size = new System.Drawing.Size(352, 182);
			this.EditorPanel.TabIndex = 4;
			// 
			// Memory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 206);
			this.Controls.Add(this.EditorPanel);
			this.Controls.Add(this.MemoryWindowMenu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(300, 34);
			this.Name = "Memory";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Memory";
			this.Load += new System.EventHandler(this.Memory_Load);
			this.Shown += new System.EventHandler(this.Memory_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Memory_FormClosing);
			this.LocationChanged += new System.EventHandler(this.Memory_LocationChanged);
			this.ResizeEnd += new System.EventHandler(this.Memory_ResizeEnd);
			this.MemoryWindowMenu.ResumeLayout(false);
			this.MemoryWindowMenu.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MemoryWindowMenu;
        private System.Windows.Forms.ToolStripMenuItem JumpToLabelMenu;
        private System.Windows.Forms.ToolStripTextBox AddressJump;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addBreakpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSCIIToolStripMenuItem;
        private System.Windows.Forms.Panel EditorPanel;


    }
}