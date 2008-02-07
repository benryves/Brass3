using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PindurTI {
	public partial class HelpForm : Form {
		public HelpForm() {
			InitializeComponent();
			this.Text = Application.ProductName + " - Help";
			this.HelpBrowser.DocumentText = Properties.Resources.TextHelp;
		}
	}
}
