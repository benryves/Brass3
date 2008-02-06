using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PindurTI {
    public partial class CustomiseKeys : Form {

        string[] CalcKeys = new string[] {
            "on",
            "up","right","left","down",
            "0","1","2","3","4","5","6","7","8","9",
            "2nd","alpha","del","mode",
            "y=","window","zoom","trace","graph",
            "+","-","*","/","(",")","(-)",".",",",
            "sin","cos","tan",
            "enter","clear",
            "vars",
            "stat","prgm",
            "x","matrx","apps",
            "math","x^-1","x^2","log","ln","sto",
        };

        public CustomiseKeys() {
            InitializeComponent();
            this.KeyMapView.Nodes.Clear();
            foreach (string S in CalcKeys) {
                TreeNode CalcKey = new TreeNode("[" + S + "]");
                CalcKey.Tag = S;
                foreach (KeyValuePair<int, string> KM in Program.KeyMap) {
                    if (KM.Value == S) {
                        TreeNode Binding = new TreeNode(((Keys)KM.Key).ToString());
                        Binding.Tag = KM.Key;
                        CalcKey.Nodes.Add(Binding);
                    }
                }
                this.KeyMapView.Nodes.Add(CalcKey);
            }
            this.KeyMapView.ExpandAll();
        }


        private void KeyMapView_MouseDown(object sender, MouseEventArgs e) {
            TreeNode S =  KeyMapView.GetNodeAt(e.X, e.Y);
            if (S != null) {
                KeyMapView.SelectedNode = S;
            }
        }

        private void addNewKeyboardBindingToolStripMenuItem_Click(object sender, EventArgs e) {
            KeyMapView.Enabled = false;
        }

        private void removeAllBindingsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (KeyMapView.SelectedNode == null || KeyMapView.SelectedNode.Parent != null) {
                return;
            }
            foreach (TreeNode N in KeyMapView.SelectedNode.Nodes) {
                Program.KeyMap.Remove((int)N.Tag);
                N.Remove();
            }

        }

        private void removeThisBindingToolStripMenuItem_Click(object sender, EventArgs e) {
            if (KeyMapView.SelectedNode == null || KeyMapView.SelectedNode.Parent == null) {
                return;
            }
            Program.KeyMap.Remove((int)KeyMapView.SelectedNode.Tag);
            KeyMapView.SelectedNode.Remove();            
        }

        private void ManageContext_Opening(object sender, CancelEventArgs e) {
            if (KeyMapView.SelectedNode == null) {
                e.Cancel = true;
                return;
            } else {
                bool IsCalcKeyGroup = (KeyMapView.SelectedNode.Parent == null);
                addNewKeyboardBindingToolStripMenuItem.Enabled = IsCalcKeyGroup;
                removeAllBindingsToolStripMenuItem.Enabled = IsCalcKeyGroup;
                removeThisBindingToolStripMenuItem.Enabled = !IsCalcKeyGroup;
            }
        }

        private void CustomiseKeys_KeyDown(object sender, KeyEventArgs e) {
            e.Handled = true;
            if (!KeyMapView.Enabled) {
                if (KeyMapView.SelectedNode != null && KeyMapView.SelectedNode.Parent == null) {
                    TreeNode N = KeyMapView.SelectedNode;
                    string CalcButton = (string)N.Tag;
                    int PCKey = (int)e.KeyCode;
                    if (Program.KeyMap.ContainsKey(PCKey) &&
                        (CalcButton == Program.KeyMap[PCKey] ||
                        MessageBox.Show(this, "The key '" + e.KeyCode.ToString() + "' is already bound to [" + Program.KeyMap[PCKey] + "].\nWould you like to reassign it to [" + CalcButton + "]?", "Key already bound", MessageBoxButtons.YesNo,  MessageBoxIcon.Question) == DialogResult.Yes)
                        ) {
                        string AlreadyMappedTo = Program.KeyMap[PCKey];
                        foreach (TreeNode FN in KeyMapView.Nodes) {
                            if ((string)FN.Tag == AlreadyMappedTo) {
                                foreach (TreeNode C in FN.Nodes) {
                                    if ((int)C.Tag == PCKey) {
                                        C.Remove();
                                    }
                                }
                                break;
                            }
                        }
                        Program.KeyMap.Remove(PCKey);
                    }
                    if (!Program.KeyMap.ContainsKey(PCKey)) {
                        TreeNode NewBinding = new TreeNode(e.KeyCode.ToString());
                        NewBinding.Tag = PCKey;
                        N.Nodes.Add(NewBinding);
                        Program.KeyMap.Add(PCKey, (string)N.Tag);

                    }
                }
                KeyMapView.Enabled = true;
            }
        }

        private void CustomiseKeys_FormClosing(object sender, FormClosingEventArgs e) {
            Program.Pindur.ClearRunning();
        }

    }
}