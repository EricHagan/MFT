using System;
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

        public ExposureSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
                if (settings != null)
                    UpdateForm();
            }
        }
        ExposureSettings settings;

        public void UpdateForm()
        {
            averagingNumericUpDown.Value = Settings.Averaging;
            integrationTimeMsNumericUpDown.Value = Settings.IntegrationTimeMs;
            dwellTimeNumericUpDown.Value = Settings.DwellTimeMs;
            normalizedCheckBox.Checked = Settings.Normalized;
            nameTextBox.Text = Settings.Name;
        }

        public void UpdateFromForm()
        {
            Settings.Averaging = (int)averagingNumericUpDown.Value;
            Settings.IntegrationTimeMs = (int)integrationTimeMsNumericUpDown.Value;
            Settings.DwellTimeMs = (int)dwellTimeNumericUpDown.Value;
            Settings.Normalized = normalizedCheckBox.Checked;
            Settings.Name = nameTextBox.Text;
        }

        void OnSettingsChanged()
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_UPDATED, Settings);
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.Name = nameTextBox.Text;
            OnSettingsChanged();
        }

        private void averagingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            settings.Averaging = (int)averagingNumericUpDown.Value;
            OnSettingsChanged();
        }

        private void integrationTimeMsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            settings.IntegrationTimeMs = (int)integrationTimeMsNumericUpDown.Value;
            OnSettingsChanged();
        }

        private void dwellTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            settings.DwellTimeMs = (int)dwellTimeNumericUpDown.Value;
            OnSettingsChanged();
        }

        private void normalizedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings.Normalized = normalizedCheckBox.Checked;
            OnSettingsChanged();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_APPLY, Settings);
        }
    }
}
