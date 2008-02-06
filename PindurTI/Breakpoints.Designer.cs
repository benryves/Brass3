namespace PindurTI {
    partial class Breakpoints {
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
            this.BreakpointList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jumpToinMemoryEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BreakpointList
            // 
            this.BreakpointList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.BreakpointList.ContextMenuStrip = this.contextMenuStrip1;
            this.BreakpointList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BreakpointList.FullRowSelect = true;
            this.BreakpointList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.BreakpointList.HideSelection = false;
            this.BreakpointList.Location = new System.Drawing.Point(0, 0);
            this.BreakpointList.MultiSelect = false;
            this.BreakpointList.Name = "BreakpointList";
            this.BreakpointList.Size = new System.Drawing.Size(392, 192);
            this.BreakpointList.TabIndex = 0;
            this.BreakpointList.UseCompatibleStateImageBehavior = false;
            this.BreakpointList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Label";
            this.columnHeader1.Width = 153;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Address";
            this.columnHeader2.Width = 62;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "File";
            this.columnHeader3.Width = 109;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Line";
            this.columnHeader4.Width = 57;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.jumpToinMemoryEditorToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // jumpToinMemoryEditorToolStripMenuItem
            // 
            this.jumpToinMemoryEditorToolStripMenuItem.Name = "jumpToinMemoryEditorToolStripMenuItem";
            this.jumpToinMemoryEditorToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.jumpToinMemoryEditorToolStripMenuItem.Text = "&Jump To";
            this.jumpToinMemoryEditorToolStripMenuItem.Click += new System.EventHandler(this.jumpToinMemoryEditorToolStripMenuItem_Click);
            // 
            // Breakpoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 192);
            this.Controls.Add(this.BreakpointList);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Breakpoints";
            this.ShowInTaskbar = false;
            this.Text = "Breakpoints";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Breakpoints_FormClosing);
            this.LocationChanged += new System.EventHandler(this.Breakpoints_LocationChanged);
            this.ResizeEnd += new System.EventHandler(this.Breakpoints_ResizeEnd);
            this.Load += new System.EventHandler(this.Breakpoints_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView BreakpointList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpToinMemoryEditorToolStripMenuItem;

    }
}