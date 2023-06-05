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
            this.MinWavelengthTextBox = new System.Windows.Forms.TextBox();
            this.MaxWavelengthTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.IncrementNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resample";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum Wavelength (nm)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Maximum Wavelength (nm)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 145);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Increment (nm)";
            // 
            // IncrementNumericUpDown
            // 
            this.IncrementNumericUpDown.Location = new System.Drawing.Point(231, 142);
            this.IncrementNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IncrementNumericUpDown.Name = "IncrementNumericUpDown";
            this.IncrementNumericUpDown.Size = new System.Drawing.Size(147, 26);
            this.IncrementNumericUpDown.TabIndex = 4;
            // 
            // MinWavelengthTextBox
            // 
            this.MinWavelengthTextBox.Location = new System.Drawing.Point(231, 55);
            this.MinWavelengthTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinWavelengthTextBox.Name = "MinWavelengthTextBox";
            this.MinWavelengthTextBox.Size = new System.Drawing.Size(148, 26);
            this.MinWavelengthTextBox.TabIndex = 5;
            this.MinWavelengthTextBox.TextChanged += new System.EventHandler(this.MinWavelengthTextBox_TextChanged);
            // 
            // MaxWavelengthTextBox
            // 
            this.MaxWavelengthTextBox.Location = new System.Drawing.Point(231, 97);
            this.MaxWavelengthTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaxWavelengthTextBox.Name = "MaxWavelengthTextBox";
            this.MaxWavelengthTextBox.Size = new System.Drawing.Size(148, 26);
            this.MaxWavelengthTextBox.TabIndex = 6;
            // 
            // ResampleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MaxWavelengthTextBox);
            this.Controls.Add(this.MinWavelengthTextBox);
            this.Controls.Add(this.IncrementNumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "ResampleControl";
            this.Size = new System.Drawing.Size(414, 208);
            ((System.ComponentModel.ISupportInitialize)(this.IncrementNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown IncrementNumericUpDown;
        private System.Windows.Forms.TextBox MinWavelengthTextBox;
        private System.Windows.Forms.TextBox MaxWavelengthTextBox;
    }
}
