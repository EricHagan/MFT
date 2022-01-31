using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFT
{
    public partial class ExposureSettings : UserControl
    {
        public ExposureSettings()
        {
            InitializeComponent();
        }

        private void ExposureSettings_Load(object sender, EventArgs e)
        {

        }

        public int Averaging
        {
            get => (int)averagingNumericUpDown.Value;
            set
            {
                if (value < averagingNumericUpDown.Minimum)
                    return;
                if (value > averagingNumericUpDown.Maximum)
                    return;
                averagingNumericUpDown.Value = value;
            
            }
        }

        public int IntegrationTimeMs
        {
            get => (int)integrationTimeMsNumericUpDown.Value;
            set
            {
                if (value < integrationTimeMsNumericUpDown.Minimum)
                    return;
                if (value > integrationTimeMsNumericUpDown.Maximum)
                    return;
                integrationTimeMsNumericUpDown.Value = value;

            }
        }

        public event EventHandler<EventArgs> ResampleClick;

        private void resampleButton_Click(object sender, EventArgs e)
        {
            ResampleClick?.Invoke(sender, e);
        }
    }
}
