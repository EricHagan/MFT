﻿namespace MFT
{
    partial class SpectrometerControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.saveExposureSettingsButton = new System.Windows.Forms.Button();
            this.whiteRefButton = new System.Windows.Forms.Button();
            this.darkRefButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dwellTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.singleSpectrumButton = new System.Windows.Forms.Button();
            this.normalizedCheckBox = new System.Windows.Forms.CheckBox();
            this.integrationTimeMsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ContinuousButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.averagingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dwellTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationTimeMsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagingNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.saveExposureSettingsButton);
            this.splitContainer1.Panel2.Controls.Add(this.whiteRefButton);
            this.splitContainer1.Panel2.Controls.Add(this.darkRefButton);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.dwellTimeNumericUpDown);
            this.splitContainer1.Panel2.Controls.Add(this.singleSpectrumButton);
            this.splitContainer1.Panel2.Controls.Add(this.normalizedCheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.integrationTimeMsNumericUpDown);
            this.splitContainer1.Panel2.Controls.Add(this.ContinuousButton);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.averagingNumericUpDown);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1077, 658);
            this.splitContainer1.SplitterDistance = 449;
            this.splitContainer1.TabIndex = 0;
            // 
            // saveExposureSettingsButton
            // 
            this.saveExposureSettingsButton.Location = new System.Drawing.Point(15, 161);
            this.saveExposureSettingsButton.Name = "saveExposureSettingsButton";
            this.saveExposureSettingsButton.Size = new System.Drawing.Size(149, 23);
            this.saveExposureSettingsButton.TabIndex = 16;
            this.saveExposureSettingsButton.Text = "Save Exposure Settings";
            this.saveExposureSettingsButton.UseVisualStyleBackColor = true;
            this.saveExposureSettingsButton.Click += new System.EventHandler(this.saveExposureSettingsButton_Click);
            // 
            // whiteRefButton
            // 
            this.whiteRefButton.Location = new System.Drawing.Point(181, 42);
            this.whiteRefButton.Name = "whiteRefButton";
            this.whiteRefButton.Size = new System.Drawing.Size(113, 23);
            this.whiteRefButton.TabIndex = 13;
            this.whiteRefButton.Text = "Collect White Ref";
            this.whiteRefButton.UseVisualStyleBackColor = true;
            this.whiteRefButton.Click += new System.EventHandler(this.whiteRefButton_Click);
            // 
            // darkRefButton
            // 
            this.darkRefButton.Location = new System.Drawing.Point(181, 12);
            this.darkRefButton.Name = "darkRefButton";
            this.darkRefButton.Size = new System.Drawing.Size(113, 23);
            this.darkRefButton.TabIndex = 12;
            this.darkRefButton.Text = "Collect Dark Ref";
            this.darkRefButton.UseVisualStyleBackColor = true;
            this.darkRefButton.Click += new System.EventHandler(this.darkRefButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Dwell time [ms]";
            // 
            // dwellTimeNumericUpDown
            // 
            this.dwellTimeNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dwellTimeNumericUpDown.Location = new System.Drawing.Point(97, 103);
            this.dwellTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.dwellTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dwellTimeNumericUpDown.Name = "dwellTimeNumericUpDown";
            this.dwellTimeNumericUpDown.Size = new System.Drawing.Size(63, 20);
            this.dwellTimeNumericUpDown.TabIndex = 11;
            this.dwellTimeNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dwellTimeNumericUpDown.ValueChanged += new System.EventHandler(this.dwellTimeNumericUpDown_ValueChanged);
            // 
            // singleSpectrumButton
            // 
            this.singleSpectrumButton.Location = new System.Drawing.Point(15, 12);
            this.singleSpectrumButton.Margin = new System.Windows.Forms.Padding(2);
            this.singleSpectrumButton.Name = "singleSpectrumButton";
            this.singleSpectrumButton.Size = new System.Drawing.Size(63, 25);
            this.singleSpectrumButton.TabIndex = 7;
            this.singleSpectrumButton.Text = "Single";
            this.singleSpectrumButton.UseVisualStyleBackColor = true;
            this.singleSpectrumButton.Click += new System.EventHandler(this.singleSpectrumButton_Click);
            // 
            // normalizedCheckBox
            // 
            this.normalizedCheckBox.AutoSize = true;
            this.normalizedCheckBox.Location = new System.Drawing.Point(42, 133);
            this.normalizedCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.normalizedCheckBox.Name = "normalizedCheckBox";
            this.normalizedCheckBox.Size = new System.Drawing.Size(118, 17);
            this.normalizedCheckBox.TabIndex = 9;
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
            this.integrationTimeMsNumericUpDown.Location = new System.Drawing.Point(114, 76);
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
            this.integrationTimeMsNumericUpDown.TabIndex = 6;
            this.integrationTimeMsNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.integrationTimeMsNumericUpDown.ValueChanged += new System.EventHandler(this.integrationTimeMsNumericUpDown_ValueChanged);
            // 
            // ContinuousButton
            // 
            this.ContinuousButton.Location = new System.Drawing.Point(82, 12);
            this.ContinuousButton.Margin = new System.Windows.Forms.Padding(2);
            this.ContinuousButton.Name = "ContinuousButton";
            this.ContinuousButton.Size = new System.Drawing.Size(79, 25);
            this.ContinuousButton.TabIndex = 8;
            this.ContinuousButton.Text = "Continuous";
            this.ContinuousButton.UseVisualStyleBackColor = true;
            this.ContinuousButton.Click += new System.EventHandler(this.ContinuousButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Averaging";
            // 
            // averagingNumericUpDown
            // 
            this.averagingNumericUpDown.Location = new System.Drawing.Point(114, 50);
            this.averagingNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.averagingNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.averagingNumericUpDown.Name = "averagingNumericUpDown";
            this.averagingNumericUpDown.Size = new System.Drawing.Size(47, 20);
            this.averagingNumericUpDown.TabIndex = 3;
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
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Integration time [ms]";
            // 
            // SpectrometerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SpectrometerControl";
            this.Size = new System.Drawing.Size(1077, 658);
            this.Load += new System.EventHandler(this.SpectrometerDialog_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dwellTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationTimeMsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagingNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown dwellTimeNumericUpDown;
        private System.Windows.Forms.Button singleSpectrumButton;
        private System.Windows.Forms.CheckBox normalizedCheckBox;
        private System.Windows.Forms.NumericUpDown integrationTimeMsNumericUpDown;
        private System.Windows.Forms.Button ContinuousButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown averagingNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button whiteRefButton;
        private System.Windows.Forms.Button darkRefButton;
        private System.Windows.Forms.Button saveExposureSettingsButton;
    }
}
