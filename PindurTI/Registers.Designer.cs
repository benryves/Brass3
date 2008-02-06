namespace PindurTI {
    partial class Registers {
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
            this.RegisterPropertyGrid = new PindurTI.Registers.SensiblePropertyGrid();
            this.SuspendLayout();
            // 
            // RegisterPropertyGrid
            // 
            this.RegisterPropertyGrid.HelpVisible = false;
            this.RegisterPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.RegisterPropertyGrid.Name = "RegisterPropertyGrid";
            this.RegisterPropertyGrid.Size = new System.Drawing.Size(162, 607);
            this.RegisterPropertyGrid.TabIndex = 0;
            this.RegisterPropertyGrid.ToolbarVisible = false;
            // 
            // Registers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(213, 273);
            this.Controls.Add(this.RegisterPropertyGrid);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Registers";
            this.ShowInTaskbar = false;
            this.Text = "Registers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Registers_FormClosing);
            this.LocationChanged += new System.EventHandler(this.Registers_LocationChanged);
            this.ResizeEnd += new System.EventHandler(this.Registers_ResizeEnd);
            this.Load += new System.EventHandler(this.Registers_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public SensiblePropertyGrid RegisterPropertyGrid;
    }
}