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

        public bool Quiet { get; set; }

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

        void OnSettingsChanged()
        {
            if (!Quiet)
                Messenger.SendMessage(this, new Message(Message.Types.EXPOSURE_SETTINGS_UPDATED, Settings));
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            OnSettingsChanged();
        }

        private void averagingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            OnSettingsChanged();
        }

        private void integrationTimeMsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            OnSettingsChanged();
        }

        private void dwellTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            OnSettingsChanged();
        }

        private void normalizedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OnSettingsChanged();
        }
    }
}
