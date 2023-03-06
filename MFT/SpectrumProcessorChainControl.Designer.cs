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
            this.chainFlowLayoutPanel.Location = new System.Drawing.Point(4, 53);
            this.chainFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.chainFlowLayoutPanel.Name = "chainFlowLayoutPanel";
            this.chainFlowLayoutPanel.Size = new System.Drawing.Size(359, 516);
            this.chainFlowLayoutPanel.TabIndex = 0;
            this.chainFlowLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.chainFlowLayoutPanel_Paint);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(404, 177);
            this.addButton.Margin = new System.Windows.Forms.Padding(4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 28);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // availableProcessorsListBox
            // 
            this.availableProcessorsListBox.FormattingEnabled = true;
            this.availableProcessorsListBox.ItemHeight = 16;
            this.availableProcessorsListBox.Location = new System.Drawing.Point(371, 53);
            this.availableProcessorsListBox.Margin = new System.Windows.Forms.Padding(4);
            this.availableProcessorsListBox.Name = "availableProcessorsListBox";
            this.availableProcessorsListBox.Size = new System.Drawing.Size(159, 116);
            this.availableProcessorsListBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(71, 17);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(459, 22);
            this.nameTextBox.TabIndex = 4;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // SpectrumProcessorChainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.availableProcessorsListBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.chainFlowLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SpectrumProcessorChainControl";
            this.Size = new System.Drawing.Size(547, 585);
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
