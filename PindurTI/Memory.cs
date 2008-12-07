using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PindurTI {
    public partial class Memory : Form {
        private MemoryEditor M;

        public int SelectedAddress {
            get { return M.SelectedAddress; }
            set { M.SelectedAddress = value; }
        }

        bool CanUpdatePosition = false;

        public Memory(MemoryEditor M) {
            InitializeComponent();
            this.M = M;
            M.Dock = DockStyle.Fill;
            M.ContextMenuStrip = this.contextMenuStrip1;
            M.BackColor = System.Drawing.SystemColors.Window;
            M.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditorPanel.Controls.Add(M);
            M.SelectedAddressChanged += new EventHandler(M_SelectedAddressChanged);
        }

        void M_SelectedAddressChanged(object sender, EventArgs e) {
            this.AddressJump.Text = M.SelectedAddress.ToString("X4");
        }

        private void Memory_FormClosing(object sender, FormClosingEventArgs e) {
            if (!this.MdiParent.Disposing) {
                e.Cancel = true;
                this.Hide();
                if (e.CloseReason != CloseReason.MdiFormClosing) Properties.Settings.Default.MemoryVisible = false;
            }
        }

        private void Memory_Shown(object sender, EventArgs e) {
            M.FullRedraw();
        }

        private void AddLabelsFromModule(CalcWindow.Module Module, ref ToolStripMenuItem MenuToAddTo) {
            foreach (CalcWindow.Module M in Module.ChildModules) {
                ToolStripMenuItem SubModule = new ToolStripMenuItem(M.Name);
                MenuToAddTo.DropDownItems.Add(SubModule);
                AddLabelsFromModule(M, ref SubModule);
            }
            List<ToolStripMenuItem> Labels = new List<ToolStripMenuItem>();
            foreach (CalcWindow.LabelDetails L in Module.Labels) {
                if (L.Exported) {
                    ToolStripMenuItem LJ = new ToolStripMenuItem(L.Name, null, MemoryJumpClick);
                    LJ.Tag = (int)L.Value;
                    LJ.ToolTipText = "$" + ((int)L.Value).ToString("X4") + ":" + L.Page + " (" + L.Value + ")\n" + Path.GetFileName(L.Filename) + " (line " + L.LineNumber + ")";
                    Labels.Add(LJ);
                }
            }
            if (Labels.Count != 0) {
                if (Module.ChildModules.Count != 0) {
                    MenuToAddTo.DropDownItems.Add(new ToolStripSeparator());
                }
                MenuToAddTo.DropDownItems.AddRange(Labels.ToArray());
            }
            
        }

        void MemoryJumpClick(object sender, EventArgs e) {
            ToolStripMenuItem T = (ToolStripMenuItem)sender;
            M.SelectedAddress = (int)T.Tag;
        }

        private void JumpToLabelMenu_DropDownOpening(object sender, EventArgs e) {
            CalcWindow C = (CalcWindow)this.MdiParent;
            JumpToLabelMenu.DropDownItems.Clear();
            AddLabelsFromModule(C.Modules, ref JumpToLabelMenu);
        }
    
        private void AddressJump_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                try {
                    M.SelectedAddress = Convert.ToUInt16(this.AddressJump.Text, 16);
                } catch {
                    this.AddressJump.Text = M.SelectedAddress.ToString("X4");
                }
            }
        }

        private void AddressJump_Enter(object sender, EventArgs e) {
            this.MdiParent.KeyPreview = false;
            this.AddressJump.SelectAll();
        }

        private void AddressJump_Leave(object sender, EventArgs e) {
            this.MdiParent.KeyPreview = true;
        }

        private void addBreakpointToolStripMenuItem_Click(object sender, EventArgs e) {
            CalcWindow C = (CalcWindow)this.MdiParent;
            string File = "";
            int Line = 0;
            int Page = 0;

            List<CalcWindow.LabelDetails> Potentials = new List<CalcWindow.LabelDetails>();
            GetLabelDetailsFromAddress(this.SelectedAddress, C.Modules, ref Potentials);


            foreach (CalcWindow.LabelDetails L in Potentials) {
                if (L.Exported) {
                    File = L.Filename;
                    Line = L.LineNumber;
                    Page = L.Page;
                    break;
                }
            }

            C.Calc.Breakpoints.Add(this.SelectedAddress, Page, File, Line, "");
            C.BreakpointsWindow.RefreshBreakpoints();
        }

        private void GetLabelDetailsFromAddress(int Address, CalcWindow.Module StartModule, ref List<CalcWindow.LabelDetails> Potentials) {
            foreach (CalcWindow.LabelDetails L in StartModule.Labels) {
                if (L.Value == Address) Potentials.Add(L);
            }
            foreach (CalcWindow.Module M in StartModule.ChildModules) {
                GetLabelDetailsFromAddress(Address, M, ref Potentials);
            }
        }

        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e) {
            M.DisplayASCII = aSCIIToolStripMenuItem.Checked;
            aSCIIToolStripMenuItem.Text = M.DisplayASCII ? "&Values" : "&ASCII";
        }

        private void Memory_Load(object sender, EventArgs e) {
            this.aSCIIToolStripMenuItem.Checked = M.DisplayASCII;
            aSCIIToolStripMenuItem.Text = M.DisplayASCII ? "&Values" : "&ASCII";

            try { this.Location = Properties.Settings.Default.MemoryPos; } catch { }
            try { this.Size = Properties.Settings.Default.MemorySize; } catch { }
            this.Show();
            this.CanUpdatePosition = true;


        }

        private void Memory_ResizeEnd(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.MemorySize = this.Size;
            }
        }

        private void Memory_LocationChanged(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.MemoryPos = this.Location;
            }
        }
    }
    
}