using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PindurTI {
    public partial class Registers : Form {

        public class SensiblePropertyGrid : PropertyGrid {            
        }
        
        public Registers() {
            InitializeComponent();
        }

        private void Registers_FormClosing(object sender, FormClosingEventArgs e) {
            if (!this.MdiParent.Disposing) {
                e.Cancel = true;
                this.Hide();
                if (e.CloseReason != CloseReason.MdiFormClosing) Properties.Settings.Default.RegistersVisible = false;
            }
        }
        public void RefreshGrid() {
            RegisterPropertyGrid.Refresh();
            this.RegisterPropertyGrid.Width = this.ClientSize.Width - 1;
            
        }
        
        protected override void OnResize(EventArgs e) {
            this.RegisterPropertyGrid.Width = this.ClientSize.Width;
            base.OnResize(e);
            if (this.MdiParent != null) ((CalcWindow)this.MdiParent).Calc.ClearRunning();
        }

        bool CanUpdatePosition = false;
        private void Registers_Load(object sender, EventArgs e) {
            try { this.Location = Properties.Settings.Default.RegistersPos; } catch { }
            try { this.Size = Properties.Settings.Default.RegistersSize; } catch { }
            this.Show();
            this.CanUpdatePosition = true;
        }

        private void Registers_LocationChanged(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.RegistersPos = this.Location;
            }
        }

        private void Registers_ResizeEnd(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.RegistersSize = this.Size;
            }
        }

    }
}