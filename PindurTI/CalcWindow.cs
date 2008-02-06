using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Threading;

namespace PindurTI {
    public partial class CalcWindow : Form {

        private Emulator Pindur = null;
        public Calculator Calc = null;

        private LCD CalculatorScreen;



        public Memory MemoryViewerWindow = null;
        public Breakpoints BreakpointsWindow = null;
        public Registers RegistersWindow = null;
        public Keypad KeypadWindow = null;
        public Variables_Watcher WatchWindow = null;

        public CalcWindow(Emulator Pindur, string RomFile) {

            this.Pindur = Pindur;
            try {
                this.Calc = new Calculator(this.Pindur, RomFile, false);
                this.Calc.Breakpoints.BreakOnBreakpoints = false;
                this.Calc.Reset();
            } catch (Exception ex) {
                MessageBox.Show(this, "Could not load: " + ex.Message, "Error starting calculator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            


            InitializeComponent();

            this.Text = Calc.Model + " - PindurTI";

            MemoryEditWindow = new MemoryEditor();
            MemoryViewerWindow = new Memory(MemoryEditWindow);
            MemoryViewerWindow.MdiParent = this;

            BreakpointsWindow = new Breakpoints();
            BreakpointsWindow.MdiParent = this;

            RegistersWindow = new Registers();
            RegistersWindow.MdiParent = this;
            RegistersWindow.RegisterPropertyGrid.SelectedObject = this.Calc.Registers;

            WatchWindow = new Variables_Watcher();
            WatchWindow.MdiParent = this;

            this.CalculatorScreen = new LCD();
            CalculatorScreen.MdiParent = this;
            CalculatorScreen.Show();

            this.KeypadWindow = new Keypad(this);

            Program.RunningCalcs.Add(this);

            Calc.BreakpointHit += new Calculator.BreakpointHitEventHandler(Calc_BreakpointHit);
            
        }

        void Calc_BreakpointHit(object sender, Emulator.BreakpointArgs e) {
            if (!Calc.Breakpoints.BreakOnBreakpoints) {
                Calc.Step();
            } else {
                if (!BreakpointsWindow.Visible) BreakpointsWindow.Show();
                BreakpointsWindow.HighlightBreakpoint(Calc.Registers.RegisterValues[6]);
                
            }
           
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Calc.ClearRunning();
            this.Calc.Running = runToolStripMenuItem.Checked;
        }

        private void sendFileToolStripMenuItem_Click(object sender, EventArgs e) {
            this.OpenFile.Filter = "TI-82/TI-83/TI-83+ Files (*.82?;*.83?;*.8x?)|*.82?;*.83?;*.8x?|All Files (*.*)|*.*";
            this.OpenFile.Multiselect = true;
            if (this.OpenFile.ShowDialog(this) == DialogResult.OK) {
                try {
                    foreach (string s in this.OpenFile.FileNames) {
                        SendFile(s);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Could not send file to calculator: " + ex.Message, "Send File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Program.Pindur.ClearRunning();            
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Calc.Reset();
        }

        private void stepToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Calc.Step();
            this.CalculatorScreen.BackgroundImage = Calc.ScreenImage;
            this.CalculatorScreen.Invalidate();
            MemoryEditWindow.Memory = this.Calc.Memory;
            BreakpointsWindow.UnhighlightBreakpoint();
            if (RegistersWindow.Visible) RegistersWindow.RefreshGrid();
            if (WatchWindow.Visible) WatchWindow.RefreshVariables();
        }

        private void SendFile(string Filename) {
            try {
                this.Calc.SendFile(Filename);
            } catch (Exception ex) {
                MessageBox.Show(this, "Error sending file: " + ex.Message, "Send file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcWindow_KeyDown(object sender, KeyEventArgs e) {
            string Key;
            if (Program.KeyMap.TryGetValue((int)e.KeyCode, out Key)) {
                e.Handled = true;
                this.Calc.KeyDown(Key);
            }
        }

        private void CalcWindow_KeyUp(object sender, KeyEventArgs e) {
            string Key;
            if (Program.KeyMap.TryGetValue((int)e.KeyCode, out Key)) {
                e.Handled = true;
                this.Calc.KeyUp(Key);
            }
        }

        private void keysToolStripMenuItem_Click(object sender, EventArgs e) {
            CustomiseKeys K = new CustomiseKeys();
            K.ShowDialog(this);
        }

        MemoryEditor MemoryEditWindow = null;
        private void CalcWindow_Load(object sender, EventArgs e) {

            try { this.Location = Properties.Settings.Default.CalcWindowPos; } catch { }
            try { this.ClientSize = Properties.Settings.Default.CalcWindowSize; } catch { }
            this.TopMost = Properties.Settings.Default.AlwaysOnTop;
            this.Calc.UseLcdMemory = Properties.Settings.Default.UseLcdMemory;
            this.Show();
            if (Properties.Settings.Default.KeypadVisible) this.showOnScreenKeypadToolStripMenuItem.PerformClick();
            if (Properties.Settings.Default.MemoryVisible) this.memoryToolStripMenuItem.PerformClick();
            if (Properties.Settings.Default.RegistersVisible) this.registersToolStripMenuItem.PerformClick();
            if (Properties.Settings.Default.BreakpointsVisible) this.breakpointsToolStripMenuItem.PerformClick();
            if (Properties.Settings.Default.VariablesVisible) this.variablesToolStripMenuItem.PerformClick();
            CalculatorScreen.BringToFront();
            
        }

        public void Tick() {
            if (this.Calc.Running) {
                this.Calc.Run();
                this.CalculatorScreen.BackgroundImage = Calc.LcdOn ? Calc.ScreenImage : null;
                this.CalculatorScreen.Invalidate();
                this.MemoryEditWindow.Memory = Calc.Memory;
                if (RegistersWindow.Visible) RegistersWindow.RefreshGrid();
                if (WatchWindow.Visible) WatchWindow.RefreshVariables();
            }
        }

        private void memoryToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!this.MemoryViewerWindow.Visible) {
                this.MemoryViewerWindow.Show();
            } else {
                this.MemoryViewerWindow.BringToFront();
            }
            Properties.Settings.Default.MemoryVisible = true;
        }

        

  
       
        private void runScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            this.OpenFile.Filter = "Debug scripts (*.debug)|*.debug|All Files (*.*)|*.*";
            this.OpenFile.Multiselect = false;
            if (this.OpenFile.ShowDialog(this) == DialogResult.OK) {
                LoadAndRunScript(this.OpenFile.FileName);
            }
        }
        
    

        private void breakOnBreakpointsToolStripMenuItem_Click(object sender, EventArgs e) {
            Calc.Breakpoints.BreakOnBreakpoints = breakOnBreakpointsToolStripMenuItem.Checked;
        }

        private void clearAllBreakpointsToolStripMenuItem_Click(object sender, EventArgs e) {
            Calc.Breakpoints.Clear();
            BreakpointsWindow.RefreshBreakpoints();
        }

        private void loadDebuggingInformationToolStripMenuItem_Click(object sender, EventArgs e) {
            this.OpenFile.Filter = "Debug information files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.OpenFile.Multiselect = false;
            if (this.OpenFile.ShowDialog(this) == DialogResult.OK) {
                LoadDebug(this.OpenFile.FileName);
            }
        }

        private void debugToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            breakOnBreakpointsToolStripMenuItem.Checked = Calc.Breakpoints.BreakOnBreakpoints;
            runScriptToolStripMenuItem.Checked = Calc.Running;
        }

        private void CalcWindow_FormClosing(object sender, FormClosingEventArgs e) {
            
            this.Dispose();
            
        }

     
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e) {
			new HelpForm().Show(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void newCalculatorToolStripMenuItem_Click(object sender, EventArgs e) {
            this.OpenFile.Filter = "Calculator ROM (*.rom)|*.rom|All Files (*.*)|*.*";
            this.OpenFile.Multiselect = false;
            if (this.OpenFile.ShowDialog(this) == DialogResult.OK) {
                try {
                    CalcWindow C = new CalcWindow(this.Pindur, this.OpenFile.FileName);
                    C.Show();
                } catch (Exception ex) {
                    MessageBox.Show(this, "Could not start new calculator: " + ex.Message, "New Calculator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CalcWindow_FormClosed(object sender, FormClosedEventArgs e) {
            Program.RunningCalcs.Remove(this);
        }


        List<ToolStripMenuItem> VideoModeSelections = new List<ToolStripMenuItem>();
        private void videoToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            string[] VideoModes = new string[] { "Unscaled", "Zoom 2x", "Zoom 3x", "Zoom 4x", "Smooth 2x", "Smooth 3x" };

            foreach (ToolStripMenuItem M in VideoModeSelections) {
                videoToolStripMenuItem.DropDownItems.Remove(M);                
            }
            VideoModeSelections.Clear();
            for (int i = 0; i < VideoModes.Length; ++i) {
                ToolStripMenuItem M = new ToolStripMenuItem(VideoModes[i]);
                M.Tag = i;
                M.Click += new EventHandler(M_Click);
                if ((int)Calc.VideoMode == i) M.Checked = true;
                videoToolStripMenuItem.DropDownItems.Add(M);
                VideoModeSelections.Add(M);
            }

            useLCDMemoryToolStripMenuItem.Checked = Properties.Settings.Default.UseLcdMemory;
            
        }

        void M_Click(object sender, EventArgs e) {
            Calc.VideoMode = (Calculator.VideoModes)(((ToolStripMenuItem)sender).Tag);
            CalculatorScreen.SnapSize();
        }

        private void setLCDColoursToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            if (darkestOnToolStripMenuItem.Image == null) {
                darkestOnToolStripMenuItem.Image = new Bitmap(16, 16);
            }
            if (brightestOffToolStripMenuItem.Image == null) {
                brightestOffToolStripMenuItem.Image = new Bitmap(16, 16);
            }
            Graphics Dark = Graphics.FromImage(darkestOnToolStripMenuItem.Image);
            Graphics Bright = Graphics.FromImage(brightestOffToolStripMenuItem.Image);
            Dark.Clear(this.Calc.DarkColour);
            Dark.DrawRectangle(Pens.Black, 0, 0, 15, 15);
            Bright.Clear(this.Calc.BrightColour);
            Bright.DrawRectangle(Pens.Black, 0, 0, 15, 15);
        }

        private void brightestOffToolStripMenuItem_Click(object sender, EventArgs e) {
            Color C = Calc.BrightColour;
            ChangeColour(ref C);
            if (C != Calc.BrightColour) Calc.BrightColour = C;
        }

        private void darkestOnToolStripMenuItem_Click(object sender, EventArgs e) {
            Color C = Calc.DarkColour;
            ChangeColour(ref C);
            if (C != Calc.DarkColour) Calc.DarkColour = C;
        }
        private void ChangeColour(ref Color ToSet) {
            this.SetColour.Color = ToSet;
            if (this.SetColour.ShowDialog(this) == DialogResult.OK) {
                ToSet = this.SetColour.Color;
            }
        }

        private void resetToolStripMenuItem1_Click(object sender, EventArgs e) {
            Calc.BrightColour = Color.FromArgb(unchecked((int)0xFFCCE0CF));
            Calc.DarkColour = Color.FromArgb(unchecked((int)0xFF202020));
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutBox().ShowDialog(this);
            Program.Pindur.ClearRunning();
        }

        private void breakpointsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!this.BreakpointsWindow.Visible) {
                this.BreakpointsWindow.Show();
            } else {
                this.BreakpointsWindow.BringToFront();
            }
            Properties.Settings.Default.BreakpointsVisible = true;
        }

        private void registersToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!this.RegistersWindow.Visible) {
                this.RegistersWindow.Show();
            } else {
                this.RegistersWindow.BringToFront();
            }
            Properties.Settings.Default.RegistersVisible = true;
        }

        private void showOnScreenKeypadToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!this.KeypadWindow.Visible) {
                this.KeypadWindow.Show();
            } else {
                this.KeypadWindow.BringToFront();
            }
            Properties.Settings.Default.KeypadVisible = true;
        }

        private void variablesToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!this.WatchWindow.Visible) {
                this.WatchWindow.Show();
            } else {
                this.WatchWindow.BringToFront();
            }
            Properties.Settings.Default.VariablesVisible = true;
        }

        private void CalcWindow_ResizeEnd(object sender, EventArgs e) {
            if (this.Visible && this.WindowState == FormWindowState.Normal) {
                Properties.Settings.Default.CalcWindowSize = this.ClientSize;
            }
        }

        private void CalcWindow_LocationChanged(object sender, EventArgs e) {
            if (this.Visible && this.WindowState == FormWindowState.Normal) {
                Properties.Settings.Default.CalcWindowPos = this.Location;
            }
        }

        private void optionsToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            this.alwaysOnTopToolStripMenuItem.Checked = this.TopMost;
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e) {
            this.TopMost = !this.TopMost;
            Properties.Settings.Default.AlwaysOnTop = this.TopMost;
        }

        private void useLCDMemoryToolStripMenuItem_Click(object sender, EventArgs e) {
            Properties.Settings.Default.UseLcdMemory ^= true;
            this.Calc.UseLcdMemory = Properties.Settings.Default.UseLcdMemory;
        }

 

    }
}