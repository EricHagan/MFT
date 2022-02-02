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

        public ISpectrometer Spectrometer { get; set; }

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

        public bool Normalize
        {
            get => normalizeCheckBox.Checked;
            set => normalizeCheckBox.Checked = value;
        }

        public void NormalizeAllowedChangedHandler(object sender, NormalizeAllowedChangedEventArgs e)
        {
            if (e.NormalizeAllowed)
                normalizeCheckBox.Enabled = true;
            else
            {
                normalizeCheckBox.Checked = false;
                normalizeCheckBox.Enabled = false;
            }
        }

        public event EventHandler<ExposureResampledEventArgs> ExposureResampled;

        private void resampleButton_Click(object sender, EventArgs e)
        {
            var exposure = Exposure.GetExposure(Spectrometer, (float)IntegrationTimeMs / 1000, Averaging,
                Normalize, out string errMsg);
            if (exposure != null)
                ExposureResampled?.Invoke(sender, new ExposureResampledEventArgs() { ResampledExposure = exposure });
            else
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public class ExposureResampledEventArgs : EventArgs
    {
        public Exposure ResampledExposure { get; set; }
    }
}
