using MFT.Properties;
using RgbDriverKit;
using System;
using System.Windows.Forms;

namespace MFT
{
    public partial class SpectrometerControl : UserControl
    {
        public SpectrometerControl(ISpectrometer _spectrometer)
        {
            InitializeComponent();
            Messenger.MessageAvailable += OnMessageReceived;
            SetSpectrometer(_spectrometer);
            UpdateForm();
        }

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.SPECTROMETER_UPDATED:
                    UpdateSpectrometer(sender, msg.Object as ISpectrometer);
                    break;
            }
        }

        void UpdateSpectrometer(object sender, ISpectrometer spectrometer)
        {
            if (this.spectrometer != spectrometer)
                return;
            UpdateForm();
        }

        public void SetSpectrometer(ISpectrometer s)
        {
            spectrometer = s;
        }

        ISpectrometer spectrometer;

        public void UpdateForm()
        {
            averagingNumericUpDown.Value = spectrometer.Settings.Averaging;
            integrationTimeMsNumericUpDown.Value = spectrometer.Settings.IntegrationTimeMs;
            dwellTimeNumericUpDown.Value = spectrometer.Settings.DwellTimeMs;
            normalizedCheckBox.Checked = spectrometer.Settings.Normalized;

            if (spectrometer.NormalizeAllowed)
                AllowNormalized();
            else
                DisallowNormalized();
        }

        public void UpdateFromForm()
        {
            spectrometer.Settings.Averaging = (int)averagingNumericUpDown.Value;
            spectrometer.Settings.IntegrationTimeMs = (int)integrationTimeMsNumericUpDown.Value;
            spectrometer.Settings.DwellTimeMs = (int)dwellTimeNumericUpDown.Value;
            spectrometer.Settings.Normalized = normalizedCheckBox.Checked;
        }

        private void SpectrometerDialog_Load(object sender, EventArgs e)
        {
            averagingNumericUpDown.Value = 10;
            integrationTimeMsNumericUpDown.Value = 50;
            dwellTimeNumericUpDown.Value = 100;
        }

        void AllowNormalized()
        {
            bool oldValue = normalizedCheckBox.Enabled;
            normalizedCheckBox.Enabled = true;
            if (normalizedCheckBox.Enabled != oldValue)
                Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_UPDATED, spectrometer.Settings);
        }

        void DisallowNormalized()
        {
            bool oldValue = normalizedCheckBox.Enabled;
            normalizedCheckBox.Enabled = false;
            if (normalizedCheckBox.Enabled != oldValue)
                Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_UPDATED, spectrometer.Settings);
        }

        private void singleSpectrumButton_Click(object sender, EventArgs e)
        {
            var exposure = spectrometer.CollectExposure(spectrometer.Settings, spectrometer.Chain, out string errMsg);
            if (exposure != null)
            {
                var singleGraph = new ExposureControl();
                singleGraph.Exposure = exposure;

                SetMainControl(singleGraph);
                singleGraph.Dock = DockStyle.Fill;
            }
            else
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void SetMainControl(Control control)
        {
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel1.Controls.Add(control);
        }

        private void ContinuousButton_Click(object sender, EventArgs e)
        {
            var exposureStream = new ExposureStream(spectrometer);
            RaiseSpectrometerControlsChangedEvent(ContinuousButton);
            var graph = new ContinuousExposureControl();
            graph.ExposureStream = exposureStream;

            var tabPage = new TabPage("Continuous");
            SetMainControl(graph);

            exposureStream.Start();
        }

        void RaiseSpectrometerControlsChangedEvent(object sender)
        {
            UpdateFromForm();
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_UPDATED, spectrometer.Settings);
        }

        private void averagingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseSpectrometerControlsChangedEvent(averagingNumericUpDown);
        }

        private void integrationTimeMsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseSpectrometerControlsChangedEvent(integrationTimeMsNumericUpDown);
        }

        private void dwellTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseSpectrometerControlsChangedEvent(dwellTimeNumericUpDown);
        }

        private void normalizedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RaiseSpectrometerControlsChangedEvent(normalizedCheckBox);
        }

        private void darkRefButton_Click(object sender, EventArgs e)
        {
            spectrometer.CollectDarkReferenceExposure(spectrometer.Settings.Unnormalized(), out string errMsg);
            if (spectrometer.DarkReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            { 
                Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, spectrometer);
            }
        }

        private void whiteRefButton_Click(object sender, EventArgs e)
        {
            spectrometer.CollectWhiteReferenceExposure(spectrometer.Settings.Unnormalized(), out string errMsg);
            if (spectrometer.WhiteReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, spectrometer);
            }
        }

        private void saveExposureSettingsButton_Click(object sender, EventArgs e)
        {
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_CREATE,
                new ExposureSettings(
                    (int)averagingNumericUpDown.Value,
                    (int)integrationTimeMsNumericUpDown.Value,
                    (int)dwellTimeNumericUpDown.Value,
                    normalizedCheckBox.Checked,
                    ""));
        }
    }
}
