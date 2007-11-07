using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GuiBuilder {
	public partial class SelectConfiguration : Form {

		private string selectedConfiguration;
		public string Configuration {
			get { return this.selectedConfiguration; }
		}

		public SelectConfiguration(string[] options) {
			InitializeComponent();
			this.ListBuildConfigurations.Items.AddRange(options);
		}

		private void ListBuildConfigurations_DoubleClick(object sender, EventArgs e) {
			this.ButtonOK.PerformClick();
		}

		private void ListBuildConfigurations_SelectedIndexChanged(object sender, EventArgs e) {
			if (this.ListBuildConfigurations.SelectedIndex == -1) {
				this.selectedConfiguration = null;
				this.ButtonOK.Enabled = false;
			} else {
				this.selectedConfiguration = this.ListBuildConfigurations.SelectedItem as string;
				this.ButtonOK.Enabled = true;
			}
		}
	}
}