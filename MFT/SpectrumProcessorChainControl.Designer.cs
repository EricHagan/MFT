namespace MFT
{
    partial class SpectrumProcessorChainControl
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
            this.chainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addButton = new System.Windows.Forms.Button();
            this.availableProcessorsListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // chainFlowLayoutPanel
            // 
            this.chainFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.chainFlowLayoutPanel.Name = "chainFlowLayoutPanel";
            this.chainFlowLayoutPanel.Size = new System.Drawing.Size(269, 459);
            this.chainFlowLayoutPanel.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(325, 104);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // availableProcessorsListBox
            // 
            this.availableProcessorsListBox.FormattingEnabled = true;
            this.availableProcessorsListBox.Location = new System.Drawing.Point(300, 3);
            this.availableProcessorsListBox.Name = "availableProcessorsListBox";
            this.availableProcessorsListBox.Size = new System.Drawing.Size(120, 95);
            this.availableProcessorsListBox.TabIndex = 2;
            // 
            // SpectrumProcessorChainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.availableProcessorsListBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.chainFlowLayoutPanel);
            this.Name = "SpectrumProcessorChainControl";
            this.Size = new System.Drawing.Size(434, 475);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel chainFlowLayoutPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListBox availableProcessorsListBox;
    }
}
