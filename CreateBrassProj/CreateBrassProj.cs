using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;

namespace CreateBrassProj {
	class CreateBrassProj {

		enum NextArgumentType {
			SourceFile,
			BinaryFile,
			ExportFile,
			ListFile,
			TableFile,
		}

		static void Main(string[] args) {

			try {

				if (args.Length < 2) throw new Exception("Usage: CreateBrassProj <projectname.brassproj> <Brass 1 arguments>");

				Brass3.Project AutoProject = new Brass3.Project();

				AutoProject.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CreateBrassProj.brassproj"));

				StringDictionary CurrentEnvironmentVariables = Process.GetCurrentProcess().StartInfo.EnvironmentVariables;

				NextArgumentType AutoArgument = NextArgumentType.SourceFile;
				NextArgumentType ExplicitArgument = (NextArgumentType)(-1);

				for (int i = 1; i < args.Length; ++i) {
					if (args[i].Length == 2 && args[i][0] == '-') {
						switch (char.ToLowerInvariant(args[i][1])) {
							case 's':
								Console.WriteLine("Brass 3 is case-insensitive ONLY.");
								break;
							case 'x':
								if (CurrentEnvironmentVariables.ContainsKey("error_log")) {
									AutoProject.ListingFiles.Add(CurrentEnvironmentVariables["error_log"], "latenite1errors");
								} else {
									Console.WriteLine("Environment variable ERROR_LOG not set.");
								}
								break;
							case 'd':
								if (CurrentEnvironmentVariables.ContainsKey("debug_log")) {
									AutoProject.ListingFiles.Add(CurrentEnvironmentVariables["debug_log"], "latenite1debug");
								} else {
									Console.WriteLine("Environment variable DEBUG_LOG not set.");
								}
								break;
							case 'o':
								Console.WriteLine("Incomplete XML logs aren't supported.");
								break;
							case 'e':
								Console.WriteLine("Strict mode doesn't exist.");
								break;
							case 'l':
								ExplicitArgument = NextArgumentType.ListFile;
								break;
							case 't':
								ExplicitArgument = NextArgumentType.TableFile;
								break;
							case 'p':
								Console.WriteLine("You'll just have to imagine that pause.");
								break;
							default:
								Console.WriteLine("Unrecognised switch -{0}", args[i]);
								break;
						}
					} else {

						NextArgumentType FileToSet = AutoArgument;
						if ((int)ExplicitArgument != -1) {
							FileToSet = ExplicitArgument;
							ExplicitArgument = (NextArgumentType)(-1);
						} else {
							AutoArgument = (NextArgumentType)((int)AutoArgument + 1);
						}

						string Filename = args[i];

						switch (FileToSet) {
							case NextArgumentType.SourceFile:
								AutoProject.SourceFile = Filename;
								break;
							case NextArgumentType.BinaryFile:
								AutoProject.DestinationFile = Filename;
								break;
						}
					}
				}

				//AutoProject.Save(args[0]);
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}

		}
	}
}
