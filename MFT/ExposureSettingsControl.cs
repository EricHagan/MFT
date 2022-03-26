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
    public partial class ExposureSettingsControl : UserControl
    {
        public ExposureSettingsControl()
        {
            InitializeComponent();
        }

        public ISpectrometer Spectrometer { get; set; }

        public void HandleSpectrometerChanged(object sender, SpectrometerChangedEventArgs e)
        {
            Spectrometer = e.Spectrometer;
            if (Spectrometer == null)
            {
                AllowNormalize = false;
                return;
            }
            Spectrometer.NormalizeAllowedChanged += HandleNormalizeAllowedChanged;
            AllowNormalize = Spectrometer.NormalizeAllowed;
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

        public bool ForbidNormalizing
        {
            get => forbidNormalizing;
            set
            {
                forbidNormalizing = value;
                if (forbidNormalizing)
                    AllowNormalize = false;
            }
        }
        bool forbidNormalizing;

        public bool AllowNormalize
        {
            get => normalizeCheckBox.Enabled;
            set
            {
                if (ForbidNormalizing)
                    normalizeCheckBox.Enabled = false;
                else
                {
                    normalizeCheckBox.Enabled = value;
                    if (normalizeCheckBox.Enabled == false)
                        normalizeCheckBox.Checked = false;
                }
            }
        }

        public bool Normalize
        {
            get => normalizeCheckBox.Checked;
            set => normalizeCheckBox.Checked = value;
        }

        public void HandleNormalizeAllowedChanged(object sender, NormalizeAllowedChangedEventArgs e)
        {
            AllowNormalize = e.NormalizeAllowed;
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
