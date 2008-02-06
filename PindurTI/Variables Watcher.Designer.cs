namespace PindurTI {
    partial class Variables_Watcher {
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
            this.VariableViewer = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // VariableViewer
            // 
            this.VariableViewer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.VariableViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VariableViewer.FullRowSelect = true;
            this.VariableViewer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.VariableViewer.HideSelection = false;
            this.VariableViewer.Location = new System.Drawing.Point(0, 0);
            this.VariableViewer.MultiSelect = false;
            this.VariableViewer.Name = "VariableViewer";
            this.VariableViewer.Size = new System.Drawing.Size(292, 216);
            this.VariableViewer.TabIndex = 0;
            this.VariableViewer.UseCompatibleStateImageBehavior = false;
            this.VariableViewer.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 163;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 76;
            // 
            // Variables_Watcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 216);
            this.Controls.Add(this.VariableViewer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Variables_Watcher";
            this.ShowInTaskbar = false;
            this.Text = "Variables";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Variables_Watcher_FormClosing);
            this.LocationChanged += new System.EventHandler(this.Variables_Watcher_LocationChanged);
            this.ResizeEnd += new System.EventHandler(this.Variables_Watcher_ResizeEnd);
            this.Load += new System.EventHandler(this.Variables_Watcher_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView VariableViewer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;




    }
}