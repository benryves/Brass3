using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace PindurTI {
    public partial class CalcWindow {

        public void LoadDebug(string Filename) {
            XmlDocument X = new XmlDocument();
            try {
                X.Load(Filename);
            } catch (Exception ex) {
                MessageBox.Show(this, "Could not load debug information:\n" + ex.Message, "Debug", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlNodeList D = X.GetElementsByTagName("debug");
            foreach (XmlNode N in D) {
                foreach (XmlAttribute A in N.Attributes) {
                    try {
                        Environment.SetEnvironmentVariable("DEBUG_" + A.Name, A.Value);
                    } catch { }
                }
            }

            XmlNodeList Breakpoints = X.GetElementsByTagName("breakpoint");
            Calc.Breakpoints.Clear();
            foreach (XmlNode N in Breakpoints) {
                int Address = -1;
                int Page = 0;
                string File = "";
                int Line = 0;
                string Description = "";
                foreach (XmlAttribute A in N.Attributes) {

                    switch (A.Name.ToLower()) {
                        case "address":
                            Address = Convert.ToInt32(A.Value) & 0xFFFF;
                            break;
                        case "page":
                            Page = Convert.ToInt32(A.Value);
                            break;
                        case "file":
                            File = A.Value;
                            break;
                        case "line":
                            Line = Convert.ToInt32(A.Value);
                            break;
                        case "description":
                            Description = A.Value;
                            break;
                    }

                }

                if (Address != -1) {
                    Calc.Breakpoints.Add(Address, Page, File, Line, Description);
                }
            }
            if (BreakpointsWindow != null) BreakpointsWindow.RefreshBreakpoints();

            LabelLookup = new Dictionary<int, Dictionary<string, string>>();
            foreach (XmlNode N in X.DocumentElement.ChildNodes) {
                if (N.Name.ToLower() == "module") {
                    LoadModule(N, ref Modules);
                }
            }
        }

        public class LabelDetails {
            public readonly string Name;
            public readonly double Value;
            public readonly int Page;
            public readonly bool Exported;
            public readonly string Filename;
            public readonly int LineNumber;
            public readonly string Fullname;
            public readonly IVariable VariableType = null;
            public LabelDetails(string Name, double Value, int Page, bool Exported, string Filename, int LineNumber, string Fullname, string Type) {
                this.Name = Name;
                this.Value = Value;
                this.Page = Page;
                this.Exported = Exported;
                this.Filename = Filename;
                this.LineNumber = LineNumber;
                this.Fullname = Fullname;
                Type = Type.ToLower();
                if (Type != "none") {
                    switch (Type.ToLower()) {
                        case "byte":
                        case "ubyte":
                            VariableType = new IntegerType(1, Type[0] == 'u');
                            break;
                        case "word":
                        case "uword":
                            VariableType = new IntegerType(2, Type[0] == 'u');
                            break;
                        case "int":
                        case "uint":
                            VariableType = new IntegerType(4, Type[0] == 'u');
                            break;
                        case "tifloat":
                            VariableType = new TiFloat();
                            break;
                        case "asc":
                            VariableType = new AsciiType();
                            break;
                        default:
                            if ((Type.StartsWith("fp") || Type.StartsWith("ufp")) && Type.Contains(".")) {
                                bool Unsigned = Type[0] == 'u';
                                if (Unsigned) {
                                    Type = Type.Substring(3);
                                } else {
                                    Type = Type.Substring(2);
                                }
                                string[] Args = Type.Split('.');
                                int A;
                                int B;
                                if (Args.Length == 2 && int.TryParse(Args[0], out A) && int.TryParse(Args[1], out B)) {
                                    VariableType = new FixedPointType(A, B, Unsigned);
                                }
                            }
                            break;
                    }
                }
                if (VariableType != null) {
                    VariableType.Address = ((int)this.Value);
                }
                
            }
        }

        private void LoadModule(XmlNode ModuleNode, ref Module ModuleListToAddTo) {
            string ModuleName = "<?>";
            foreach (XmlAttribute A in ModuleNode.Attributes) {
                if (A.Name.ToLower() == "name") {
                    ModuleName = A.Value;
                    break;
                }
            }
            bool IsRoot = ModuleListToAddTo.Equals(Modules) && ModuleName == "root";
            bool IsGlobal = ModuleListToAddTo.Equals(Modules) && ModuleName == "global";

            Module M = null;

            if (IsGlobal) {
                M = Modules;
            } else if (IsRoot) {
                //
            } else {
                M = new Module(ModuleName);
                ModuleListToAddTo.ChildModules.Add(M);
            }

            foreach (XmlNode N in ModuleNode.ChildNodes) {
                switch (N.Name.ToLower()) {
                    case "module":
                        if (IsRoot) {
                            LoadModule(N, ref Modules);
                        } else {
                            LoadModule(N, ref M);
                        }
                        break;
                    case "label":
                        string Name = "";
                        double Value = 0.0d;
                        int Page = -1;
                        bool Exported = false;
                        string Filename = "";
                        string Fullname = null;
                        string Type = "none";
                        int LineNumber = 0;
                        foreach (XmlAttribute A in N.Attributes) {
                            try {
                                switch (A.Name.ToLower()) {
                                    case "name":
                                        Name = A.Value;
                                        break;
                                    case "value":
                                        Value = Convert.ToDouble(A.Value);
                                        break;
                                    case "page":
                                        Page = Convert.ToInt32(A.Value);
                                        break;
                                    case "exported":
                                        Exported = Convert.ToBoolean(A.Value);
                                        break;
                                    case "file":
                                        Filename = A.Value;
                                        break;
                                    case "line":
                                        LineNumber = Convert.ToInt32(A.Value);
                                        break;
                                    case "fullname":
                                        Fullname = A.Value;
                                        break;
                                    case "type":
                                        Type = A.Value;
                                        break;
                                }
                            } catch (Exception ex) {
                                MessageBox.Show(this, "Error in log: (" + A.Name + " = " + A.Value + ") " + ex.Message, "Debug log error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        if (Fullname == null) Fullname = Name;
                        LabelDetails L = new LabelDetails(Name, Value, Page, Exported, Filename, LineNumber, Fullname, Type);
                        M.Labels.Add(L);

                        if (L.VariableType != null) {
                            L.VariableType.Owner = this;
                            if (L.Exported) WatchWindow.WatchedVariables.Add(L);
                        }

                        Dictionary<string, string> TryAddLabelLookup;
                        if (!LabelLookup.TryGetValue((int)L.Value, out TryAddLabelLookup)) {
                            TryAddLabelLookup = new Dictionary<string, string>();
                            LabelLookup.Add((int)L.Value, TryAddLabelLookup);
                        }
                        string TryGetValue;
                        string FilenameKey = L.Filename.ToLower();
                        if (TryAddLabelLookup.TryGetValue(FilenameKey, out TryGetValue)) {
                            if (L.Exported) {
                                TryGetValue += ", " + L.Fullname;
                                TryAddLabelLookup[FilenameKey] = TryGetValue;
                            }
                        } else {
                            TryAddLabelLookup.Add(FilenameKey, L.Fullname);
                        }
                        break;
                }
            }
        }

        public Dictionary<int, Dictionary<string, string>> LabelLookup = new Dictionary<int, Dictionary<string, string>>();

        public class Module {
            public List<LabelDetails> Labels = new List<LabelDetails>();
            public List<Module> ChildModules = new List<Module>();
            public readonly string Name;
            public Module(string Name) {
                this.Name = Name;
            }
        }
        public Module Modules = new Module("Global");

    }
}
