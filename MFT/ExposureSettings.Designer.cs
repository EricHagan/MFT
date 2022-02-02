namespace MFT
{
    partial class ExposureSettings
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
            this.averagingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.integrationTimeMsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.resampleButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.normalizeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.averagingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationTimeMsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // averagingNumericUpDown
            // 
            this.averagingNumericUpDown.Location = new System.Drawing.Point(121, 27);
            this.averagingNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.averagingNumericUpDown.Name = "averagingNumericUpDown";
            this.averagingNumericUpDown.Size = new System.Drawing.Size(47, 20);
            this.averagingNumericUpDown.TabIndex = 0;
            this.averagingNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // integrationTimeMsNumericUpDown
            // 
            this.integrationTimeMsNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.integrationTimeMsNumericUpDown.Location = new System.Drawing.Point(202, 27);
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
            this.integrationTimeMsNumericUpDown.Size = new System.Drawing.Size(44, 20);
            this.integrationTimeMsNumericUpDown.TabIndex = 1;
            this.integrationTimeMsNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // resampleButton
            // 
            this.resampleButton.Location = new System.Drawing.Point(19, 24);
            this.resampleButton.Name = "resampleButton";
            this.resampleButton.Size = new System.Drawing.Size(75, 23);
            this.resampleButton.TabIndex = 2;
            this.resampleButton.Text = "Resample";
            this.resampleButton.UseVisualStyleBackColor = true;
            this.resampleButton.Click += new System.EventHandler(this.resampleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Averaging";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Integration Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ms";
            // 
            // normalizeCheckBox
            // 
            this.normalizeCheckBox.AutoSize = true;
            this.normalizeCheckBox.Location = new System.Drawing.Point(121, 57);
            this.normalizeCheckBox.Name = "normalizeCheckBox";
            this.normalizeCheckBox.Size = new System.Drawing.Size(120, 17);
            this.normalizeCheckBox.TabIndex = 6;
            this.normalizeCheckBox.Text = "Normalize Spectrum";
            this.normalizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExposureSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.normalizeCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resampleButton);
            this.Controls.Add(this.integrationTimeMsNumericUpDown);
            this.Controls.Add(this.averagingNumericUpDown);
            this.Name = "ExposureSettings";
            this.Size = new System.Drawing.Size(302, 85);
            this.Load += new System.EventHandler(this.ExposureSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.averagingNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationTimeMsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown averagingNumericUpDown;
        private System.Windows.Forms.NumericUpDown integrationTimeMsNumericUpDown;
        private System.Windows.Forms.Button resampleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox normalizeCheckBox;
    }
}
