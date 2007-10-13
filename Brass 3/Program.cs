using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Brass3 {
	class Program {
		static void Main(string[] args) {

			Compiler C = new Compiler();

			C.ErrorRaised += new Compiler.CompilerNotificationEventHandler(delegate(object sender, Compiler.NotificationEventArgs e) {
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error ");
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write("Pass " + (int)C.CurrentPass);
				Console.WriteLine(" [" + C.CurrentFile + "]: " + e.Message);
				if (e.SourceException != null && e.SourceException.SourceStatement != null) {

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.Write("{0,5}", C.CurrentLineNumber);
					RestoreConsoleColours();
					e.SourceException.SourceStatement.WriteColouredConsoleOutput(true, e.SourceException.Token, true);
					Console.WriteLine();
				}
				
			});

			C.InformationRaised += delegate(object sender, Compiler.NotificationEventArgs e) {
				Console.Write(e.Message);
			};

			if (args.Length == 1) {
				try {
					Project P = new Project();
					P.Load(args[0]);
					C.LoadProject(P);
					C.Compile(true);
				} catch (Exception ex) {
					Console.Error.WriteLine("Fatal error! " + ex.Message);
				}
			} else {

				Console.WriteLine("Usage: Brass ProjectFile");
				Console.WriteLine("Running in interactive calculator mode. Type exit to quit.");
				Console.WriteLine();

				foreach (string s in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")) {
					try {
						C.LoadPluginsFromAssembly(s);
					} catch { }
				}
				

				for (; ; ) {
					Console.Write("> ");
					string StringInput = Console.ReadLine();
					switch (StringInput.ToLowerInvariant()) {
						case "exit":
						case "quit":
							return;
						case "list":
							List<Label> ToSort = new List<Label>(C.Labels);
							ToSort.Sort(delegate(Label a, Label b) { return a.Name.CompareTo(b.Name); });
							foreach (Label L in ToSort) {
								Console.WriteLine(L.Name + "\t" + L.NumericValue);								
							}
							break;
						default:
							try {
								foreach (TokenisedSource TS in TokenisedSource.FromString(C, StringInput)) {
									Label L = TS.GetCode().EvaluateExpression(C);
									try {
										Console.WriteLine(L.NumericValue);
									} catch (CompilerExpection cex) {
										Console.WriteLine(cex.Message);
										cex.SourceStatement.WriteColouredConsoleOutput(true, cex.Token, false);
										Console.WriteLine();
									} catch (Exception ex) {
										Console.WriteLine(ex.Message);
									}
								}
							} catch (CompilerExpection cex) {
								Console.WriteLine(cex.Message);
								cex.SourceStatement.WriteColouredConsoleOutput(true, cex.Token, false);
								Console.WriteLine();
							} catch (Exception ex) {
								Console.WriteLine(ex.Message);
							}
							break;
					}
				}
				
			}

		}

		private static ConsoleColor[] OriginalConsoleColours = new ConsoleColor[] { Console.ForegroundColor, Console.BackgroundColor };
		private static void RestoreConsoleColours() {
			Console.ForegroundColor = OriginalConsoleColours[0];
			Console.BackgroundColor = OriginalConsoleColours[1];
		}
	}
}
