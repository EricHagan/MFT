namespace MFT
{
    partial class SpectrumWindowControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MinWavelengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MaxWavelengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.MinWavelengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWavelengthNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Window";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum Wavelength (nm)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Maximum Wavelength (nm)";
            // 
            // MinWavelengthNumericUpDown
            // 
            this.MinWavelengthNumericUpDown.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.Location = new System.Drawing.Point(152, 38);
            this.MinWavelengthNumericUpDown.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.Name = "MinWavelengthNumericUpDown";
            this.MinWavelengthNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.MinWavelengthNumericUpDown.TabIndex = 3;
            this.MinWavelengthNumericUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.ValueChanged += new System.EventHandler(this.MinWavelengthNumericUpDown_ValueChanged);
            // 
            // MaxWavelengthNumericUpDown
            // 
            this.MaxWavelengthNumericUpDown.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.Location = new System.Drawing.Point(152, 66);
            this.MaxWavelengthNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.Minimum = new decimal(new int[] {
            650,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.Name = "MaxWavelengthNumericUpDown";
            this.MaxWavelengthNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.MaxWavelengthNumericUpDown.TabIndex = 4;
            this.MaxWavelengthNumericUpDown.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.ValueChanged += new System.EventHandler(this.MaxWavelengthNumericUpDown_ValueChanged);
            // 
            // SpectrumWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MaxWavelengthNumericUpDown);
            this.Controls.Add(this.MinWavelengthNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SpectrumWindowControl";
            this.Size = new System.Drawing.Size(286, 103);
            ((System.ComponentModel.ISupportInitialize)(this.MinWavelengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWavelengthNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MinWavelengthNumericUpDown;
        private System.Windows.Forms.NumericUpDown MaxWavelengthNumericUpDown;
    }
}
