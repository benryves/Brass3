using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace PindurTI {

    using LabelDetails = CalcWindow.LabelDetails;
    
    public partial class Variables_Watcher : Form {
        public Variables_Watcher() {
            InitializeComponent();
            WatchedVariables = new List<CalcWindow.LabelDetails>();
        }
        
        public List<LabelDetails> WatchedVariables = null;

        private void Variables_Watcher_FormClosing(object sender, FormClosingEventArgs e) {
            if (!this.MdiParent.Disposing) {
                e.Cancel = true;
                this.Hide();
                if (e.CloseReason != CloseReason.MdiFormClosing) Properties.Settings.Default.VariablesVisible = false;
            }
        }
        public void RefreshVariables() {
            if (this.VariableViewer.Items.Count != this.WatchedVariables.Count) {
                this.VariableViewer.Items.Clear();
                foreach (LabelDetails L in this.WatchedVariables) {
                    ListViewItem LVI = new ListViewItem(L.Fullname);
                    LVI.SubItems.Add("");
                    LVI.Tag = L;
                    this.VariableViewer.Items.Add(LVI);
                }
            }
            foreach (ListViewItem LVI in this.VariableViewer.Items) {
                string T = ((LabelDetails)LVI.Tag).VariableType.FormattedValue;
                if (LVI.SubItems[1].Text != T) LVI.SubItems[1].Text = T;
            }
            
        }

        bool CanUpdatePosition = false; 
        private void Variables_Watcher_Load(object sender, EventArgs e) {
            try { this.Location = Properties.Settings.Default.VariablesPos; } catch { }
            try { this.Size = Properties.Settings.Default.VariablesSize; } catch { }
            this.Show();
            this.CanUpdatePosition = true;
        }

        private void Variables_Watcher_ResizeEnd(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.VariablesSize = this.Size;
            }
        }

        private void Variables_Watcher_LocationChanged(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.VariablesPos = this.Location;
            }
        }

    }
}