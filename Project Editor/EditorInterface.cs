using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjectEditor {
	public partial class EditorInterface : Form {
		public EditorInterface() {
			InitializeComponent();
			this.Text = Application.ProductName;
		}

		private void openProjectToolStripMenuItem_Click(object sender, EventArgs e) {
			if (this.OpenProjectDialog.ShowDialog(this) == DialogResult.OK) {
				try {
					this.Editor.Open(this.OpenProjectDialog.FileName);
				} catch (Exception ex) {
					MessageBox.Show(this, "Project Open", "Error opening project:" + Environment.NewLine + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Editor.Save();
		}
	}
}