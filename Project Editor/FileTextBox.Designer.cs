namespace ProjectEditor {
	partial class FileTextBox {
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
			this.PathTextBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.BrowseFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// PathTextBox
			// 
			this.PathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PathTextBox.Location = new System.Drawing.Point(0, 0);
			this.PathTextBox.Name = "PathTextBox";
			this.PathTextBox.Size = new System.Drawing.Size(125, 20);
			this.PathTextBox.TabIndex = 0;
			// 
			// BrowseButton
			// 
			this.BrowseButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.BrowseButton.Location = new System.Drawing.Point(125, 0);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(25, 150);
			this.BrowseButton.TabIndex = 1;
			this.BrowseButton.Text = "...";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// FileTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PathTextBox);
			this.Controls.Add(this.BrowseButton);
			this.Name = "FileTextBox";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox PathTextBox;
		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.OpenFileDialog BrowseFileDialog;
	}
}
