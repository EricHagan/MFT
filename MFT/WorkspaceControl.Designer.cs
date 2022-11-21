namespace MFT
{
    partial class WorkspaceControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView = new System.Windows.Forms.TreeView();
            this.spectrometerTitleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.camerasContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exposureSettingsTitleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exposureSettingsItemContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.applyToSpectrometerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exposureSettingsItemContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(512, 688);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // spectrometerTitleContextMenuStrip
            // 
            this.spectrometerTitleContextMenuStrip.Name = "spectrometerTitleContextMenuStrip";
            this.spectrometerTitleContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // camerasContextMenuStrip
            // 
            this.camerasContextMenuStrip.Name = "camerasContextMenuStrip";
            this.camerasContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // exposureSettingsTitleContextMenuStrip
            // 
            this.exposureSettingsTitleContextMenuStrip.Name = "exposureSettingsTitleContextMenuStrip";
            this.exposureSettingsTitleContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // exposureSettingsItemContextMenuStrip
            // 
            this.exposureSettingsItemContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyToSpectrometerToolStripMenuItem,
            this.setAsDefaultToolStripMenuItem});
            this.exposureSettingsItemContextMenuStrip.Name = "exposureSettingsItemContextMenuStrip";
            this.exposureSettingsItemContextMenuStrip.Size = new System.Drawing.Size(195, 48);
            // 
            // applyToSpectrometerToolStripMenuItem
            // 
            this.applyToSpectrometerToolStripMenuItem.Name = "applyToSpectrometerToolStripMenuItem";
            this.applyToSpectrometerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.applyToSpectrometerToolStripMenuItem.Text = "Apply To Spectrometer";
            this.applyToSpectrometerToolStripMenuItem.Click += new System.EventHandler(this.applyToSpectrometerToolStripMenuItem_Click);
            // 
            // setAsDefaultToolStripMenuItem
            // 
            this.setAsDefaultToolStripMenuItem.Name = "setAsDefaultToolStripMenuItem";
            this.setAsDefaultToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.setAsDefaultToolStripMenuItem.Text = "Set As Default";
            this.setAsDefaultToolStripMenuItem.Click += new System.EventHandler(this.setAsDefaultToolStripMenuItem_Click);
            // 
            // WorkspaceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "WorkspaceControl";
            this.Size = new System.Drawing.Size(512, 688);
            this.exposureSettingsItemContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip spectrometerTitleContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip camerasContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip exposureSettingsTitleContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip exposureSettingsItemContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem applyToSpectrometerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsDefaultToolStripMenuItem;
    }
}
