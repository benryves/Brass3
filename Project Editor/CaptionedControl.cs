using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ProjectEditor {
	public partial class CaptionedControl<T> : UserControl  where T : UserControl {

		private readonly T controlToCaption;
		public T ControlToCaption {
			get { return this.controlToCaption;}
		}

		private readonly Label CaptionLabel;
		public string Caption {
			get { return this.CaptionLabel.Text; }
			set { this.CaptionLabel.Text = value; }
		}

		public CaptionedControl(T control) {
			this.controlToCaption = control;

			this.ControlToCaption.Dock = DockStyle.Fill;
			this.Controls.Add(ControlToCaption);
			
			this.CaptionLabel = new Label();
			this.CaptionLabel.Dock = DockStyle.Left;
			this.CaptionLabel.AutoSize = false;
			this.CaptionLabel.Width = 100;
			this.Controls.Add(CaptionLabel);

			this.Height = this.ControlToCaption.Height;
		}
	}
}
