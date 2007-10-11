using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEditor {
	class CaptionedFileTextBox : CaptionedControl<FileTextBox> {

		private FileTextBox ftb;

		public CaptionedFileTextBox()
			: base(new FileTextBox()) {
			this.ftb = base.ControlToCaption;
			this.Height = this.ftb.Height;
		}
	}
}
