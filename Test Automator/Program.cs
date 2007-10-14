using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using System.IO;

namespace Test_Automator {
	class Program {
		static void Main(string[] args) {



			foreach (string ToTest in Properties.Resources.ToTest.Split('\n')) {

				Compiler C = new Compiler();
				Project P = new Project();
				P.Load("Test.brassproj");
				P.OutputWriter = "raw";
				C.LoadProject(P);

				Console.WriteLine("Compiling " + ToTest);
				C.SourceFile = "Test/" + ToTest.Trim();
				C.DestinationFile = C.SourceFile + ".test.bin";
				C.ListingFiles.Add(C.SourceFile + ".test.txt", C.ListingWriters["text"]);
				try {
					C.Compile(true);
				} catch { }
				byte[] A = File.ReadAllBytes(C.SourceFile + ".test.bin");
				byte[] B = File.ReadAllBytes(C.SourceFile + ".obj");

				bool Failed = false;
				if (A.Length != B.Length) {
					Failed = true;
				} else {
					for (int i = 0; i < A.Length; ++i) {
						if (A[i] != B[i]) {
							Failed = true;
							break;
						}
					}
				}

				if (Failed) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("FAILED");
					//Console.WriteLine("$={0:X4} @={1:X4}", (int)C.Labels.ProgramCounter.NumericValue, (int)C.Labels.OutputCounter.NumericValue);
					Console.ForegroundColor = ConsoleColor.Gray;

					for (; ; ) {
						Console.Write("> ");
						string Command = Console.ReadLine();
						if (Command == "") break;
						try {
							Label L = new Compiler.SourceStatement(C, TokenisedSource.FromString(C, Command)[0].GetCode(), null, 0).Compile();
							Console.WriteLine("${0:X4} {1} \"{2}\"", (int)L.NumericValue, L.NumericValue, L.StringValue);
						} catch { }
					}

				} else {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("OK");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				

			}

		}
	}
}
