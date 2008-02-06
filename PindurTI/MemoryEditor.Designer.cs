namespace PindurTI {
    partial class MemoryEditor {
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
            this.CellsImage = new System.Windows.Forms.PictureBox();
            this.MemoryOffset = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.CellsImage)).BeginInit();
            this.SuspendLayout();
            // 
            // CellsImage
            // 
            this.CellsImage.BackColor = System.Drawing.SystemColors.Window;
            this.CellsImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CellsImage.Location = new System.Drawing.Point(0, 0);
            this.CellsImage.Name = "CellsImage";
            this.CellsImage.Size = new System.Drawing.Size(133, 150);
            this.CellsImage.TabIndex = 0;
            this.CellsImage.TabStop = false;
            // 
            // MemoryOffset
            // 
            this.MemoryOffset.Dock = System.Windows.Forms.DockStyle.Right;
            this.MemoryOffset.Location = new System.Drawing.Point(133, 0);
            this.MemoryOffset.Maximum = 4096;
            this.MemoryOffset.Name = "MemoryOffset";
            this.MemoryOffset.Size = new System.Drawing.Size(17, 150);
            this.MemoryOffset.TabIndex = 1;
            // 
            // MemoryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CellsImage);
            this.Controls.Add(this.MemoryOffset);
            this.Name = "MemoryEditor";
            ((System.ComponentModel.ISupportInitialize)(this.CellsImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CellsImage;
        private System.Windows.Forms.VScrollBar MemoryOffset;

    }
}
