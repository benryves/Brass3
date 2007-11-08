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

		public struct BuildConfiguration {
			public string Name;
			public string DisplayName;
			public override string ToString() {
				return this.DisplayName;
			}
			public BuildConfiguration(string name, string displayName) {
				this.Name = name;
				this.DisplayName = displayName;
			}
		}

		public SelectConfiguration(KeyValuePair<string,string>[] options) {
			InitializeComponent();
			foreach (KeyValuePair<string, string> KVP in options) {
				this.ListBuildConfigurations.Items.Add(new BuildConfiguration(KVP.Key, KVP.Value)); ;
			}
			
		}

		private void ListBuildConfigurations_DoubleClick(object sender, EventArgs e) {
			this.ButtonOK.PerformClick();
		}

		private void ListBuildConfigurations_SelectedIndexChanged(object sender, EventArgs e) {
			if (this.ListBuildConfigurations.SelectedIndex == -1) {
				this.selectedConfiguration = null;
				this.ButtonOK.Enabled = false;
			} else {
				this.selectedConfiguration = ((BuildConfiguration)this.ListBuildConfigurations.SelectedItem).Name;
				this.ButtonOK.Enabled = true;
			}
		}
	}
}