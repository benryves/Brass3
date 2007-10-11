using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ProjectEditor {
	public partial class FileTextBox : UserControl {

		public string Path {
			get { return this.PathTextBox.Text; }
			set { this.PathTextBox.Text = value; }
		}

		private string filter;
		public string Filter {
			get { return this.filter; }
			set { this.filter = value; }
		}

		public FileTextBox() {
			InitializeComponent();
			this.Height = this.PathTextBox.Height;
		}

		private void BrowseButton_Click(object sender, EventArgs e) {
			this.BrowseFileDialog.FileName = this.Path;
			this.BrowseFileDialog.Filter = this.Filter;
			if (this.BrowseFileDialog.ShowDialog(this) == DialogResult.OK) {
				this.Path = this.BrowseFileDialog.FileName;
			}
		}
	}
}
