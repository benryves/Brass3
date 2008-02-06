using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace PindurTI {
    static class Program {

        public static Emulator Pindur = null;
        public static Dictionary<int, string> KeyMap;
        public static LateniteInteraction Latenite;
		public static Dictionary<string, string> ExtraVariables = new Dictionary<string, string>();

        private enum WaitingForWhat {
            Nothing,
            Script,
            Debug
        }

        public static List<CalcWindow> RunningCalcs = new List<CalcWindow>();
        public static CalcWindow InitialWindow;
        public static bool InteractingWithLatenite = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Arguments) {

            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();

            try {
                Pindur = new Emulator();
            } catch (Exception ex) {
                MessageBox.Show("Could not initialise PindurTI: " + ex.Message, "PindurTI error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            

            if (Arguments.Length == 0) {
                OpenFileDialog RomFile = new OpenFileDialog();
                RomFile.Filter = "Calculator ROM (*.rom)|*.rom|All Files (*.*)|*.*";
                DialogResult D = RomFile.ShowDialog();
                if (D == DialogResult.Cancel) return;
                Arguments = new string[] { RomFile.FileName };
            }

            InitialWindow = new CalcWindow(Pindur, Arguments[0].Trim('"'));

            Latenite = new LateniteInteraction();

            if (InitialWindow.IsDisposed) return;
            

            WaitingForWhat W = WaitingForWhat.Nothing;
            for (int i = 1; i < Arguments.Length; ++i) {
                switch (W) {
                    case WaitingForWhat.Nothing:
                        switch (Arguments[i].ToLower()) {
                            case "-d":
                                W = WaitingForWhat.Debug;
                                break;
                            case "-s":
                                W = WaitingForWhat.Script;
                                break;
                            case "-l":
                                InteractingWithLatenite = true;
                                break;
                        }
                        break;
                    case WaitingForWhat.Script:
                        InitialWindow.LoadAndRunScript(Arguments[i].Trim('"'));
                        W = WaitingForWhat.Nothing;
                        break;
                    case WaitingForWhat.Debug:
                        InitialWindow.LoadDebug(Arguments[i].Trim('"'));
                        W = WaitingForWhat.Nothing;
                        break;
                }
                
            }

            
            #region VTI-default keymap
            /*
            KeyMap[(int)Keys.Menu] = "2nd";
            KeyMap[(int)Keys.F1] = "y=";
            KeyMap[(int)Keys.F2] = "window";
            KeyMap[(int)Keys.F3] = "zoom";
            KeyMap[(int)Keys.F4] = "trace";
            KeyMap[(int)Keys.F5] = "graph";
            KeyMap[(int)Keys.Escape] = "mode";
            KeyMap[(int)Keys.CapsLock] = "alpha";
            KeyMap[(int)Keys.Back] = "del";
            KeyMap[(int)Keys.Delete] = "del";
            KeyMap[(int)Keys.Oemplus] = "stat";
            KeyMap[(int)Keys.F6] = "math";
            KeyMap[(int)Keys.F7] = "apps";
            KeyMap[(int)Keys.F8] = "prgm";
            KeyMap[(int)Keys.F9] = "vars";
            KeyMap[(int)Keys.PageDown] = "clear";
            KeyMap[(int)Keys.End] = "x^-1";
            KeyMap[(int)Keys.Insert] = "sin";
            KeyMap[(int)Keys.Home] = "cos";
            KeyMap[(int)Keys.PageUp] = "tan";
            KeyMap[(int)Keys.NumLock] = "^";
            KeyMap[(int)Keys.OemSemicolon] = "x^2";
            KeyMap[(int)Keys.OemOpenBrackets] = "(";
            KeyMap[(int)Keys.OemCloseBrackets] = ")";
            KeyMap[(int)Keys.OemCloseBrackets] = ")";
            KeyMap[(int)Keys.Oemtilde] = "x";
            KeyMap[(int)Keys.OemBackslash] = "ln";
            KeyMap[(int)Keys.Tab] = "sto";
            KeyMap[(int)Keys.Scroll] = "on";
            KeyMap[(int)Keys.D0] = "0";
            KeyMap[(int)Keys.D1] = "1";
            KeyMap[(int)Keys.D2] = "2";
            KeyMap[(int)Keys.D3] = "3";
            KeyMap[(int)Keys.D4] = "4";
            KeyMap[(int)Keys.D5] = "5";
            KeyMap[(int)Keys.D6] = "6";
            KeyMap[(int)Keys.D7] = "7";
            KeyMap[(int)Keys.D8] = "8";
            KeyMap[(int)Keys.D9] = "9";
            KeyMap[(int)Keys.Up] = "up";
            KeyMap[(int)Keys.Down] = "down";
            KeyMap[(int)Keys.Left] = "left";
            KeyMap[(int)Keys.Right] = "right";
            KeyMap[(int)Keys.Enter] = "enter";

            KeyMap[(int)Keys.A] = "math";
            KeyMap[(int)Keys.B] = "apps";
            KeyMap[(int)Keys.C] = "prgm";
            KeyMap[(int)Keys.D] = "x^-1";
            KeyMap[(int)Keys.E] = "sin";
            KeyMap[(int)Keys.F] = "cos";
            KeyMap[(int)Keys.G] = "tan";
            KeyMap[(int)Keys.H] = "^";
            KeyMap[(int)Keys.I] = "x^2";
            KeyMap[(int)Keys.J] = ",";
            KeyMap[(int)Keys.K] = "(";
            KeyMap[(int)Keys.L] = ")";
            KeyMap[(int)Keys.M] = "/";
            KeyMap[(int)Keys.N] = "log";
            KeyMap[(int)Keys.O] = "7";
            KeyMap[(int)Keys.P] = "8";
            KeyMap[(int)Keys.Q] = "9";
            KeyMap[(int)Keys.R] = "*";
            KeyMap[(int)Keys.S] = "ln";
            KeyMap[(int)Keys.T] = "4";
            KeyMap[(int)Keys.U] = "5";
            KeyMap[(int)Keys.V] = "6";
            KeyMap[(int)Keys.W] = "-";
            KeyMap[(int)Keys.X] = "sto";
            KeyMap[(int)Keys.Y] = "1";
            KeyMap[(int)Keys.Z] = "2";
            KeyMap[(int)Keys.Space] = "0";

            KeyMap[(int)Keys.NumPad0] = "0";
            KeyMap[(int)Keys.NumPad1] = "1";
            KeyMap[(int)Keys.NumPad2] = "2";
            KeyMap[(int)Keys.NumPad3] = "3";
            KeyMap[(int)Keys.NumPad4] = "4";
            KeyMap[(int)Keys.NumPad5] = "5";
            KeyMap[(int)Keys.NumPad6] = "6";
            KeyMap[(int)Keys.NumPad7] = "7";
            KeyMap[(int)Keys.NumPad8] = "8";
            KeyMap[(int)Keys.NumPad9] = "9";

            KeyMap[188] = ",";
            KeyMap[190] = ".";

            KeyMap[222] = "log";

            KeyMap[110] = ".";

            KeyMap[107] = "+";
            KeyMap[109] = "-";
            KeyMap[106] = "*";
            KeyMap[111] = "/";

            KeyMap[191] = "/";
            KeyMap[189] = "-";
            */
            #endregion

            ReloadKeymap(Properties.Settings.Default.KeyMap);
            
            InitialWindow.Show();
            while (RunningCalcs.Count > 0) {
                List<CalcWindow> ToRemove = new List<CalcWindow>();
                foreach (CalcWindow C in RunningCalcs) {
                    if (!C.Visible) {
                        ToRemove.Add(C);
                    } else {
                        C.Tick();
                    }
                }
                foreach (CalcWindow C in ToRemove) {
                    if (RunningCalcs.Contains(C)) RunningCalcs.Remove(C);  
                }

                if (InteractingWithLatenite) Latenite.Update();

                Thread.Sleep(10);
                Application.DoEvents();
            }

            string KeyMapSerialise = "";
            bool First = true;
            foreach (KeyValuePair<int, string> KVP in KeyMap) {
                if (First) {
                    First = false;
                } else {
                    KeyMapSerialise += "|";
                }
                KeyMapSerialise += KVP.Key + ";" + KVP.Value;
            }
            Properties.Settings.Default.KeyMap = KeyMapSerialise;
            Properties.Settings.Default.Save();
        }

        internal static void ReloadKeymap(string SerialisedKeymap) {
            KeyMap = new Dictionary<int, string>();
            string[] KeyMappings = SerialisedKeymap.Split('|');
            foreach (string S in KeyMappings) {
                string[] KVP = S.Split(';');
                KeyMap.Add(Convert.ToInt32(KVP[0]), KVP[1]);
            }
        }
    }
}