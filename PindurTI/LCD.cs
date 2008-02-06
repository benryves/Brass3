using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PindurTI {
    public partial class LCD : Form {
        public LCD() {
            InitializeComponent();
  
        }
        private void LCD_BackgroundImageChanged(object sender, EventArgs e) {
            if (this.BackgroundImage != null && SnapSizeRequest) {
                this.ClientSize = this.BackgroundImage.Size;
                LCD_ResizeEnd(null, null);
            }
        }

        private bool SnapSizeRequest = false;
        public void SnapSize() {
            SnapSizeRequest = true;
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e) {
            if (this.BackgroundImage != null) Clipboard.SetImage(this.BackgroundImage);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (SaveImage.ShowDialog() == DialogResult.OK) {
                try {
                    ImageFormat SaveFormat = ImageFormat.Png;
                    switch (SaveImage.FilterIndex) {      
                        case 1:
                            SaveFormat = ImageFormat.Png;
                            break;
                        case 2:
                            SaveFormat = ImageFormat.Jpeg;
                            break;
                        case 3:
                            SaveFormat = ImageFormat.Bmp;
                            break;
                    }
                    if (this.BackgroundImage != null) this.BackgroundImage.Save(SaveImage.FileName, SaveFormat);
                } catch (Exception ex) {
                    MessageBox.Show(this, "Error saving image: " + ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        bool CanUpdatePosition = false;
        private void LCD_Load(object sender, EventArgs e) {
            try { this.Location = Properties.Settings.Default.LcdPos; } catch { }
            try { this.ClientSize = Properties.Settings.Default.LcdSize; } catch { }
            this.Show();
            CanUpdatePosition = true;

        }

        private void LCD_LocationChanged(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) Properties.Settings.Default.LcdPos = this.Location;
        }

        private void LCD_ResizeEnd(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) Properties.Settings.Default.LcdSize = this.ClientSize;
        }        
    }
}