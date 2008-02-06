using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PindurTI {
    public partial class Breakpoints : Form {
        public Breakpoints() {
            InitializeComponent();
        }

        private void Breakpoints_Load(object sender, EventArgs e) {
            RefreshBreakpoints();
            try { this.Location = Properties.Settings.Default.BreakpointsPos; } catch { }
            try { this.Size = Properties.Settings.Default.BreakpointsSize; } catch { }
            this.Show();
            this.CanUpdatePosition = true;
        }

        private void Breakpoints_FormClosing(object sender, FormClosingEventArgs e) {
            if (!this.MdiParent.Disposing) {
                e.Cancel = true;
                this.Hide();
                if (e.CloseReason != CloseReason.MdiFormClosing) Properties.Settings.Default.BreakpointsVisible = false;
            }
        }

        public void RefreshBreakpoints() {
            BreakpointList.Items.Clear();
            CalcWindow C = (CalcWindow)this.MdiParent;
            foreach (Calculator.Breakpoint B in C.Calc.Breakpoints) {

                string Address = "$" + B.Address.ToString("X4") + ":" + B.Page.ToString();

                string BreakpointName = B.Description;

                Dictionary<string,string> TryGetBreakpointName;

                if (C.LabelLookup.TryGetValue(B.Address, out TryGetBreakpointName)) {
                    string GetBreakpointName;
                    if (TryGetBreakpointName.TryGetValue(B.Filename.ToLower(), out GetBreakpointName)) {
                        if (BreakpointName == "") {
                            BreakpointName = GetBreakpointName;
                        } else {
                            BreakpointName += " (" + GetBreakpointName + ")";
                        }
                    }
                }
                if (BreakpointName == "") BreakpointName = Address;

                ListViewItem Breakpoint = new ListViewItem(BreakpointName);
                Breakpoint.SubItems.Add(Address);
                Breakpoint.SubItems.Add(Path.GetFileName(B.Filename));
                Breakpoint.SubItems.Add(B.Line.ToString());

                if (IsHighlighted && B.Address == this.HighlightedBreakpointAddress) {
                    Breakpoint.Font = new Font(this.Font, FontStyle.Bold);
                    if (this.MdiParent.Equals(Program.InitialWindow) && Program.InteractingWithLatenite && WantsToHighlightBreakpoint) {
                        Console.WriteLine("HIGHLIGHT BREAKPOINT " + B.Line.ToString() + " " + B.Filename);
                        WantsToHighlightBreakpoint = false;
                    }
                }

                Breakpoint.Tag = B;
                this.BreakpointList.Items.Add(Breakpoint);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
            e.Cancel = this.BreakpointList.SelectedItems.Count != 1;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            CalcWindow C = (CalcWindow)this.MdiParent;
            ListViewItem L = this.BreakpointList.SelectedItems[0];
            C.Calc.Breakpoints.Remove((Calculator.Breakpoint)L.Tag);
            this.BreakpointList.Items.Remove(L);
        }

        private int HighlightedBreakpointAddress = -1;
        private bool IsHighlighted = false;

        public void UnhighlightBreakpoint() {
            IsHighlighted = false;
            RefreshBreakpoints();
        }

        bool WantsToHighlightBreakpoint = false;
        public void HighlightBreakpoint(int Address) {
            IsHighlighted = true;
            HighlightedBreakpointAddress = Address;
            WantsToHighlightBreakpoint = true;
            this.RefreshBreakpoints();
            
            /*foreach (ListViewItem L in this.BreakpointList.Items) {
                Calculator.Breakpoint B = (Calculator.Breakpoint)L.Tag;
                if (B.Address == Address) {
                    L.Font = new Font(this.BreakpointList.Font, FontStyle.Bold);
                    L.Selected = true;
                    return;
                }
            }*/
        }

        private void jumpToinMemoryEditorToolStripMenuItem_Click(object sender, EventArgs e) {
            CalcWindow C = (CalcWindow)this.MdiParent;
            if (!C.MemoryViewerWindow.Visible) C.MemoryViewerWindow.Show();
            C.MemoryViewerWindow.SelectedAddress = ((Calculator.Breakpoint)this.BreakpointList.SelectedItems[0].Tag).Address;
        }

        bool CanUpdatePosition = false;
        private void Breakpoints_ResizeEnd(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.BreakpointsSize = this.Size;
                
            }
        }

        private void Breakpoints_LocationChanged(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.BreakpointsPos = this.Location;
            }
        }

    }
}