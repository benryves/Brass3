using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Help {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (Skybound.VisualStyles.VisualStyleContext.Create()) {
				Application.Run(new HelpInterface());
			}
		}
	}
}