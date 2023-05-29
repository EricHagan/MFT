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
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chainFlowLayoutPanel
            // 
            this.chainFlowLayoutPanel.AutoScroll = true;
            this.chainFlowLayoutPanel.Location = new System.Drawing.Point(4, 66);
            this.chainFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chainFlowLayoutPanel.Name = "chainFlowLayoutPanel";
            this.chainFlowLayoutPanel.Size = new System.Drawing.Size(404, 645);
            this.chainFlowLayoutPanel.TabIndex = 0;
            this.chainFlowLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.chainFlowLayoutPanel_Paint);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(454, 221);
            this.addButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(112, 35);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // availableProcessorsListBox
            // 
            this.availableProcessorsListBox.FormattingEnabled = true;
            this.availableProcessorsListBox.ItemHeight = 20;
            this.availableProcessorsListBox.Location = new System.Drawing.Point(417, 66);
            this.availableProcessorsListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.availableProcessorsListBox.Name = "availableProcessorsListBox";
            this.availableProcessorsListBox.Size = new System.Drawing.Size(178, 144);
            this.availableProcessorsListBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(80, 21);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(516, 26);
            this.nameTextBox.TabIndex = 4;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // SpectrumProcessorChainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.availableProcessorsListBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.chainFlowLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SpectrumProcessorChainControl";
            this.Size = new System.Drawing.Size(615, 731);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel chainFlowLayoutPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListBox availableProcessorsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
    }
}
