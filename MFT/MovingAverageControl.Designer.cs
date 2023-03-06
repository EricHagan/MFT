namespace MFT
{
    partial class MovingAverageControl
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
            this.PointsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.iterationsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PointsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Points";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Iterations";
            // 
            // PointsNumericUpDown
            // 
            this.PointsNumericUpDown.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.PointsNumericUpDown.Location = new System.Drawing.Point(144, 44);
            this.PointsNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PointsNumericUpDown.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            0});
            this.PointsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PointsNumericUpDown.Name = "PointsNumericUpDown";
            this.PointsNumericUpDown.Size = new System.Drawing.Size(95, 22);
            this.PointsNumericUpDown.TabIndex = 2;
            this.PointsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PointsNumericUpDown.ValueChanged += new System.EventHandler(this.PointsNumericUpDown_ValueChanged);
            // 
            // iterationsNumericUpDown
            // 
            this.iterationsNumericUpDown.Location = new System.Drawing.Point(144, 75);
            this.iterationsNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iterationsNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.iterationsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iterationsNumericUpDown.Name = "iterationsNumericUpDown";
            this.iterationsNumericUpDown.Size = new System.Drawing.Size(95, 22);
            this.iterationsNumericUpDown.TabIndex = 3;
            this.iterationsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Moving Average";
            // 
            // MovingAverageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.iterationsNumericUpDown);
            this.Controls.Add(this.PointsNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MovingAverageControl";
            this.Size = new System.Drawing.Size(272, 115);
            ((System.ComponentModel.ISupportInitialize)(this.PointsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PointsNumericUpDown;
        private System.Windows.Forms.NumericUpDown iterationsNumericUpDown;
        private System.Windows.Forms.Label label3;
    }
}
