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
			string BuildConfiguration = null;

			if (args.Length >= 1 && args.Length <= 2) {
				ProjectPath = Path.GetFullPath(args[0]);
				if (args.Length == 2) BuildConfiguration = args[1];
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

			BeeDevelopment.Brass3.Compiler Compiler;
			BeeDevelopment.Brass3.Project Project = new BeeDevelopment.Brass3.Project();

			try {
				Project.Load(ProjectPath);

				if (BuildConfiguration == null) {
					KeyValuePair<string, string>[] PossibleConfigurations = Project.GetBuildConfigurationNames();
					if (PossibleConfigurations.Length > 0) {
						using (SelectConfiguration SC = new SelectConfiguration(PossibleConfigurations)) {
							if (SC.ShowDialog() != DialogResult.OK) {
								return;
							} else {
								BuildConfiguration = SC.Configuration;
							}
						}
					}
				}

				if (BuildConfiguration != null) Project = Project.GetBuildConfiguration(BuildConfiguration);

				Directory.SetCurrentDirectory(Path.GetDirectoryName(ProjectPath));
				Compiler = new BeeDevelopment.Brass3.Compiler();
				Compiler.LoadProject(Project);
			} catch (Exception ex) {
				MessageBox.Show("Error loading project:" + Environment.NewLine + ex.Message, "Project Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			

			Application.Run(new GuiBuilder(Compiler));
		}
	}
}