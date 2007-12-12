using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace Brass3 {
	class Program {
		static void Main(string[] args) {

			Compiler C = new Compiler();


			C.ErrorRaised += (sender, e) => DisplayError(C, e, Strings.CommandLineError, ConsoleColor.Red);
			C.WarningRaised += (sender, e) => DisplayError(C, e, Strings.CommandLineWarning, ConsoleColor.Yellow);

			C.MessageRaised += (sender, e) => Console.Write(e.Message);

			if (args.Length == 1 || args.Length == 2) {
				try {
					Project P = new Project();
					P.Load(args[0]);
					if (args.Length == 2) P = P.GetBuildConfiguration(args[1]);
					C.LoadProject(P);
					C.Compile(true);
				} catch (Exception ex) {
					Console.Error.WriteLine(Strings.CommandLineFatalError + " " + ex.Message);
				}
			} else {

				Console.WriteLine(Strings.CommandLineSyntax);
				Console.WriteLine(Strings.CommandLineCalculatorMode);
				Console.WriteLine();


				foreach (string s in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")) {
					try {
						C.LoadPluginsFromAssembly(s);
					} catch { }
				}

				C.SwitchOn();

				for (; ; ) {
					Console.Write("> ");
					string StringInput = Console.ReadLine();
					switch (StringInput.ToLowerInvariant()) {
						case "exit":
						case "quit":
							return;
						case "list":
							List<Label> ToSort = new List<Label>(C.Labels);
							ToSort.Sort((a, b) => a.Name.CompareTo(b.Name));
							foreach (Label L in ToSort) {
								if (L.Created) {
									Console.WriteLine(L.Name + "\t" + L.NumericValue);
								}
							}
							break;
						default:
							try {
								foreach (TokenisedSource TS in TokenisedSource.FromString(C, StringInput)) {
									Label L = TS.GetCode().EvaluateExpression(C, 0, false, true);
									try {
										if (L.IsString) {
											Console.WriteLine(L.StringValue);
										} else {
											Console.WriteLine(L.NumericValue);
										}
									} catch (CompilerException cex) {
										Console.WriteLine(cex.Message);
										cex.SourceStatement.WriteColouredConsoleOutput(true, cex.Token, false);
										Console.WriteLine();
									} catch (Exception ex) {
										Console.WriteLine(ex.Message);
									}
								}
							} catch (CompilerException cex) {
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

		static void DisplayError(Compiler c, Compiler.NotificationEventArgs e, string errorType, ConsoleColor errorColour) {
			Console.WriteLine(string.Format(errorType, e.Message, string.IsNullOrEmpty(e.Filename) ? "?" : c.GetRelativeFilename(e.Filename), e.LineNumber, e.SourceToken == null ? 0 : e.SourceToken.SourcePosition - e.SourceStatement.OutermostTokenisedSource.Tokens[0].SourcePosition));
			if (e.SourceStatement != null) {
				Console.WriteLine(e.SourceStatement.OutermostTokenisedSource.ToString().Trim());
			}
			Console.WriteLine();
		}

		private static ConsoleColor[] OriginalConsoleColours = new ConsoleColor[] { Console.ForegroundColor, Console.BackgroundColor };
		private static void RestoreConsoleColours() {
			Console.ForegroundColor = OriginalConsoleColours[0];
			Console.BackgroundColor = OriginalConsoleColours[1];
		}
	}
}
