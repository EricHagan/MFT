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

        long settingsHandle { get; set; }

        public ExposureSettings Settings
        {
            get
            {
                return new ExposureSettings(
                    (int)averagingNumericUpDown.Value,
                    (int)integrationTimeMsNumericUpDown.Value,
                    (int)dwellTimeNumericUpDown.Value,
                    normalizedCheckBox.Checked,
                    nameTextBox.Text,
                    settingsHandle);
            }
            set
            {
                averagingNumericUpDown.Value = value.Averaging;
                integrationTimeMsNumericUpDown.Value = value.IntegrationTimeMs;
                dwellTimeNumericUpDown.Value = value.DwellTimeMs;
                normalizedCheckBox.Checked = value.Normalized;
                nameTextBox.Text = value.Name;
                settingsHandle = value.Handle;
            }
        }
    }
}
