namespace PindurTI {
    partial class CustomiseKeys {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.KeyMapView = new System.Windows.Forms.TreeView();
            this.ManageContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewKeyboardBindingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllBindingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeThisBindingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.ManageContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // KeyMapView
            // 
            this.KeyMapView.ContextMenuStrip = this.ManageContext;
            this.KeyMapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KeyMapView.Location = new System.Drawing.Point(0, 0);
            this.KeyMapView.Name = "KeyMapView";
            this.KeyMapView.Size = new System.Drawing.Size(219, 203);
            this.KeyMapView.TabIndex = 0;
            this.KeyMapView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeyMapView_MouseDown);
            // 
            // ManageContext
            // 
            this.ManageContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewKeyboardBindingToolStripMenuItem,
            this.removeAllBindingsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeThisBindingToolStripMenuItem});
            this.ManageContext.Name = "ManageContext";
            this.ManageContext.Size = new System.Drawing.Size(214, 76);
            this.ManageContext.Opening += new System.ComponentModel.CancelEventHandler(this.ManageContext_Opening);
            // 
            // addNewKeyboardBindingToolStripMenuItem
            // 
            this.addNewKeyboardBindingToolStripMenuItem.Name = "addNewKeyboardBindingToolStripMenuItem";
            this.addNewKeyboardBindingToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.addNewKeyboardBindingToolStripMenuItem.Text = "Add new keyboard binding...";
            this.addNewKeyboardBindingToolStripMenuItem.Click += new System.EventHandler(this.addNewKeyboardBindingToolStripMenuItem_Click);
            // 
            // removeAllBindingsToolStripMenuItem
            // 
            this.removeAllBindingsToolStripMenuItem.Name = "removeAllBindingsToolStripMenuItem";
            this.removeAllBindingsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.removeAllBindingsToolStripMenuItem.Text = "Remove all bindings";
            this.removeAllBindingsToolStripMenuItem.Click += new System.EventHandler(this.removeAllBindingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 6);
            // 
            // removeThisBindingToolStripMenuItem
            // 
            this.removeThisBindingToolStripMenuItem.Name = "removeThisBindingToolStripMenuItem";
            this.removeThisBindingToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.removeThisBindingToolStripMenuItem.Text = "Remove this binding";
            this.removeThisBindingToolStripMenuItem.Click += new System.EventHandler(this.removeThisBindingToolStripMenuItem_Click);
            // 
            // HelpLabel
            // 
            this.HelpLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HelpLabel.Location = new System.Drawing.Point(0, 203);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(219, 58);
            this.HelpLabel.TabIndex = 1;
            this.HelpLabel.Text = "Each calculator button can have multiple computer keys assigned to it. Right clic" +
                "k items to manage.";
            this.HelpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomiseKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 261);
            this.Controls.Add(this.KeyMapView);
            this.Controls.Add(this.HelpLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CustomiseKeys";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customise Keys";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomiseKeys_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CustomiseKeys_KeyDown);
            this.ManageContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView KeyMapView;
        private System.Windows.Forms.Label HelpLabel;
        private System.Windows.Forms.ContextMenuStrip ManageContext;
        private System.Windows.Forms.ToolStripMenuItem addNewKeyboardBindingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllBindingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeThisBindingToolStripMenuItem;

    }
}