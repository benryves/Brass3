using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PindurTI {
    public partial class CalcWindow {

        public bool ExecuteScript(string ScriptLine) {

            System.Collections.IDictionary Vars = Environment.GetEnvironmentVariables();

			foreach (var ExtraVar in Program.ExtraVariables) {
				if (Vars.Contains(ExtraVar.Key)) Vars.Remove(ExtraVar.Key);
				Vars.Add(ExtraVar.Key, ExtraVar.Value);				
			}

            foreach (System.Collections.DictionaryEntry D in Vars) {
                int Start = ScriptLine.ToUpper().IndexOf("%" + D.Key.ToString().ToUpper() + "%");
                if (Start != -1) {
                    ScriptLine = ScriptLine.Remove(Start) + D.Value.ToString() + ScriptLine.Substring(Start + D.Key.ToString().Length + 2);
                }
            }


            string[] ScriptParts = ScriptLine.Split(';');
            ScriptLine = ScriptParts[0].Trim();
            if (ScriptLine == "") return true;
            string[] Commands = ScriptLine.Split(' ');
            switch (Commands[0].ToLower().Trim()) {
                case "key-up":
                case "key-down":
                    if (Commands.Length < 2) throw new Exception(Commands[0] + " expects a key name.");
                    switch (Commands[0].ToLower().Trim()) {
                        case "key-up": Calc.KeyUp(Commands[1]); break;
                        case "key-down": Calc.KeyDown(Commands[1]); break;
                    }
                    if (Commands.Length == 3) {
                        Calc.RunAbsolute(Convert.ToInt32(Commands[2]));
                    }
                    break;
                case "key-press":
                    if (Commands.Length < 2) throw new Exception(Commands[0] + " expects a key name.");
                    int KeyDelay = 2000000;
                    if (Commands.Length == 3) {
                        KeyDelay = Convert.ToInt32(Commands[2]);
                    }
                    Calc.KeyDown(Commands[1]);
                    Calc.RunAbsolute(KeyDelay);
                    Calc.KeyUp(Commands[1]);
                    Calc.RunAbsolute(KeyDelay);
                    break;
                case "run":
                    if (Commands.Length < 2) throw new Exception(Commands[0] + " expects a cycle count.");
                    Calc.RunAbsolute(Convert.ToInt32(Commands[1]));
                    break;
                case "send-file":
                    if (Commands.Length < 2) throw new Exception(Commands[0] + " expects a file name.");
                    string FullFileName = Commands[1];
                    for (int i = 2; i < Commands.Length; ++i) {
                        FullFileName += " " + Commands[i];
                    }
                    Calc.SendFile(FullFileName.Trim('"'));
                    break;
                case "reset-calc":
                    Calc.Reset();
                    break;
                case "breakpoints-on":
                    Calc.Breakpoints.BreakOnBreakpoints = true;
                    break;
                case "breakpoints-off":
                    Calc.Breakpoints.BreakOnBreakpoints = false;
                    break;
                case "return":
                    return false;
                default:
                    throw new Exception("Command " + ScriptLine + " not understood.");
            }
            return true;
        }

        public void LoadAndRunScript(string Filename) {
            List<string> Errors = new List<string>();
            try {

                using (TextReader T = new StreamReader(Filename)) {
                    string S;
                    while ((S = T.ReadLine()) != null) {
                        try {
                            if (!ExecuteScript(S)) {
                                break;
                            }
                        } catch (Exception ex) {
                            Errors.Add(ex.Message);
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Could not load debug script: " + ex.Message, "Debug Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                if (Errors.Count != 0) {
                    string ErrorMessage = "";
                    foreach (string S in Errors) {
                        ErrorMessage += "\n" + S;
                    }
                    MessageBox.Show(this, Errors.Count + " error" + (Errors.Count > 1 ? "s" : "") + " in debug script:\n" + ErrorMessage, "Debug Script", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

    }
}
