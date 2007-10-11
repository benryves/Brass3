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
				C.Compile(true);
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
				} else {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("OK");
				}
				Console.ForegroundColor = ConsoleColor.Gray;

			}

		}
	}
}
