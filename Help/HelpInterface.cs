using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Brass3;
using System.IO;
using System.Reflection;

namespace Help {
	public partial class HelpInterface : Form {

		public HelpInterface() {
			InitializeComponent();
			this.Text = Application.ProductName;

		}

		private void HelpInterface_Load(object sender, EventArgs e) {
			Compiler Brass = new Compiler();
			foreach (string Plugin in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")) {
				Brass.LoadPluginsFromAssembly(Plugin);
			}
			this.HelpViewer.HelpProvider = new HelpProvider(Brass);
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e) {
			this.HelpViewer.ShowPrintDialog();
		}

		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) {
			this.HelpViewer.ShowPrintPreviewDialog();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void exportAsHTMLToolStripMenuItem_Click(object sender, EventArgs e) {
			if (this.ExportHtmlDialog.ShowDialog(this) == DialogResult.OK) {
				this.HelpViewer.ExportWebsite(this.ExportHtmlDialog.FileName);
			}
		}

		private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
			this.backToolStripMenuItem.Enabled = this.HelpViewer.CanGoBack;
			this.forwardsToolStripMenuItem.Enabled = this.HelpViewer.CanGoForwards;
		}

		private void backToolStripMenuItem_Click(object sender, EventArgs e) {
			this.HelpViewer.GoBack();
		}

		private void forwardsToolStripMenuItem_Click(object sender, EventArgs e) {
			this.HelpViewer.GoForwards();
		}

		private void exportAsLatenite1XMLToolStripMenuItem_Click(object sender, EventArgs e) {
			if (this.ExportLateniteDialog.ShowDialog(this) == DialogResult.OK) {
				this.HelpViewer.ExportLatenite1Xml(this.ExportLateniteDialog.SelectedPath);
			}
		}
	}
}