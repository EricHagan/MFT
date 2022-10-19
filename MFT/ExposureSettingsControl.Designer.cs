namespace MFT
{
    partial class ExposureSettingsControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.dwellTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.normalizedCheckBox = new System.Windows.Forms.CheckBox();
            this.integrationTimeMsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.averagingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dwellTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationTimeMsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagingNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 101);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Dwell time [ms]";
            // 
            // dwellTimeNumericUpDown
            // 
            this.dwellTimeNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dwellTimeNumericUpDown.Location = new System.Drawing.Point(100, 100);
            this.dwellTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.dwellTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dwellTimeNumericUpDown.Name = "dwellTimeNumericUpDown";
            this.dwellTimeNumericUpDown.Size = new System.Drawing.Size(63, 20);
            this.dwellTimeNumericUpDown.TabIndex = 18;
            this.dwellTimeNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dwellTimeNumericUpDown.ValueChanged += new System.EventHandler(this.dwellTimeNumericUpDown_ValueChanged);
            // 
            // normalizedCheckBox
            // 
            this.normalizedCheckBox.AutoSize = true;
            this.normalizedCheckBox.Location = new System.Drawing.Point(45, 130);
            this.normalizedCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.normalizedCheckBox.Name = "normalizedCheckBox";
            this.normalizedCheckBox.Size = new System.Drawing.Size(118, 17);
            this.normalizedCheckBox.TabIndex = 16;
            this.normalizedCheckBox.Text = "Normalize spectrum";
            this.normalizedCheckBox.UseVisualStyleBackColor = true;
            this.normalizedCheckBox.CheckedChanged += new System.EventHandler(this.normalizedCheckBox_CheckedChanged);
            // 
            // integrationTimeMsNumericUpDown
            // 
            this.integrationTimeMsNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.integrationTimeMsNumericUpDown.Location = new System.Drawing.Point(117, 73);
            this.integrationTimeMsNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.integrationTimeMsNumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.integrationTimeMsNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.integrationTimeMsNumericUpDown.Name = "integrationTimeMsNumericUpDown";
            this.integrationTimeMsNumericUpDown.Size = new System.Drawing.Size(47, 20);
            this.integrationTimeMsNumericUpDown.TabIndex = 15;
            this.integrationTimeMsNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.integrationTimeMsNumericUpDown.ValueChanged += new System.EventHandler(this.integrationTimeMsNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Averaging";
            // 
            // averagingNumericUpDown
            // 
            this.averagingNumericUpDown.Location = new System.Drawing.Point(117, 47);
            this.averagingNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.averagingNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.averagingNumericUpDown.Name = "averagingNumericUpDown";
            this.averagingNumericUpDown.Size = new System.Drawing.Size(47, 20);
            this.averagingNumericUpDown.TabIndex = 12;
            this.averagingNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.averagingNumericUpDown.ValueChanged += new System.EventHandler(this.averagingNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Integration time [ms]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(64, 16);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(202, 20);
            this.nameTextBox.TabIndex = 20;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(19, 157);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(139, 23);
            this.applyButton.TabIndex = 21;
            this.applyButton.Text = "Apply to Spectrometer";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // ExposureSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dwellTimeNumericUpDown);
            this.Controls.Add(this.normalizedCheckBox);
            this.Controls.Add(this.integrationTimeMsNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.averagingNumericUpDown);
            this.Controls.Add(this.label2);
            this.Name = "ExposureSettingsControl";
            this.Size = new System.Drawing.Size(288, 191);
            ((System.ComponentModel.ISupportInitialize)(this.dwellTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationTimeMsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagingNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown dwellTimeNumericUpDown;
        private System.Windows.Forms.CheckBox normalizedCheckBox;
        private System.Windows.Forms.NumericUpDown integrationTimeMsNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown averagingNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button applyButton;
    }
}
