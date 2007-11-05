using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace GuiBuilder {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);


			string ProjectPath = null;

			if (args.Length == 1) {
				ProjectPath = args[0];
			} else {
				using (OpenFileDialog OpenFile = new OpenFileDialog()) {
					OpenFile.Filter = "Brass Projects (*.brassproj)|*.brassproj";
					if (OpenFile.ShowDialog() != DialogResult.OK) {
						return;
					} else {
						ProjectPath = OpenFile.FileName;
					}
				}
			}

			Brass3.Compiler Compiler;
			Brass3.Project Project = new Brass3.Project();

			try {
				Project.Load(ProjectPath);
				Directory.SetCurrentDirectory(Path.GetDirectoryName(ProjectPath));
				Compiler = new Brass3.Compiler();
				Compiler.LoadProject(Project);
			} catch (Exception ex) {
				MessageBox.Show("Error loading project:" + Environment.NewLine + ex.Message, "Project Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			

			Application.Run(new GuiBuilder(Compiler));
		}
	}
}