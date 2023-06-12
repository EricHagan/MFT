namespace MFT
{
    partial class ResampleControl
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
            this.label4 = new System.Windows.Forms.Label();
            this.IncrementNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MinWavelengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MaxWavelengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.IncrementNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinWavelengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWavelengthNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resample";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum Wavelength (nm)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Maximum Wavelength (nm)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Increment (nm)";
            // 
            // IncrementNumericUpDown
            // 
            this.IncrementNumericUpDown.Location = new System.Drawing.Point(154, 92);
            this.IncrementNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.IncrementNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.IncrementNumericUpDown.Name = "IncrementNumericUpDown";
            this.IncrementNumericUpDown.Size = new System.Drawing.Size(98, 20);
            this.IncrementNumericUpDown.TabIndex = 4;
            this.IncrementNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.IncrementNumericUpDown.ValueChanged += new System.EventHandler(this.IncrementNumericUpDown_ValueChanged);
            // 
            // MinWavelengthNumericUpDown
            // 
            this.MinWavelengthNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.Location = new System.Drawing.Point(154, 37);
            this.MinWavelengthNumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.Name = "MinWavelengthNumericUpDown";
            this.MinWavelengthNumericUpDown.Size = new System.Drawing.Size(98, 20);
            this.MinWavelengthNumericUpDown.TabIndex = 5;
            this.MinWavelengthNumericUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.MinWavelengthNumericUpDown.ValueChanged += new System.EventHandler(this.MinWavelengthNumericUpDown_ValueChanged);
            // 
            // MaxWavelengthNumericUpDown
            // 
            this.MaxWavelengthNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.Location = new System.Drawing.Point(154, 64);
            this.MaxWavelengthNumericUpDown.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.Name = "MaxWavelengthNumericUpDown";
            this.MaxWavelengthNumericUpDown.Size = new System.Drawing.Size(98, 20);
            this.MaxWavelengthNumericUpDown.TabIndex = 6;
            this.MaxWavelengthNumericUpDown.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.MaxWavelengthNumericUpDown.ValueChanged += new System.EventHandler(this.MaxWavelengthNumericUpDown_ValueChanged);
            // 
            // ResampleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MaxWavelengthNumericUpDown);
            this.Controls.Add(this.MinWavelengthNumericUpDown);
            this.Controls.Add(this.IncrementNumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ResampleControl";
            this.Size = new System.Drawing.Size(276, 135);
            ((System.ComponentModel.ISupportInitialize)(this.IncrementNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinWavelengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWavelengthNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown IncrementNumericUpDown;
        private System.Windows.Forms.NumericUpDown MinWavelengthNumericUpDown;
        private System.Windows.Forms.NumericUpDown MaxWavelengthNumericUpDown;
    }
}
