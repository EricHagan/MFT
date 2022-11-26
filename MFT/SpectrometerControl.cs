using MFT.Properties;
using System;
using System.Windows.Forms;

namespace MFT
{
    public partial class SpectrometerControl : UserControl
    {
        public SpectrometerControl(ISpectrometer _spectrometer)
        {
            InitializeComponent();
            SetSpectrometer(_spectrometer);
            spectrometer.SpectrometerChanged += HandleSpectrometerChanged;
            UpdateFromSpectrometer();
        }

        public void SetSpectrometer(ISpectrometer s)
        {
            spectrometer = s;
            ExposureControls = s.Settings;
        }

        ISpectrometer spectrometer;

        // todo: rethink this with Messages:
        public event EventHandler<SpectrometerControlsChangedEventArgs> ControlsChanged;

        string ExposureSettingsName = null;
        public ExposureSettings ExposureControls
        {
            get
            {
                return settings;
            }
            internal set
            {
                settings = value;
                if (settings != null)
                    UpdateForm();
            }
        }
        ExposureSettings settings;

        public void UpdateForm()
        {
            averagingNumericUpDown.Value = ExposureControls.Averaging;
            integrationTimeMsNumericUpDown.Value = ExposureControls.IntegrationTimeMs;
            dwellTimeNumericUpDown.Value = ExposureControls.DwellTimeMs;
            normalizedCheckBox.Checked = ExposureControls.Normalized;
        }

        public void UpdateFromForm()
        {
            ExposureControls.Averaging = (int)averagingNumericUpDown.Value;
            ExposureControls.IntegrationTimeMs = (int)integrationTimeMsNumericUpDown.Value;
            ExposureControls.DwellTimeMs = (int)dwellTimeNumericUpDown.Value;
            ExposureControls.Normalized = normalizedCheckBox.Checked;
        }

        // same as ExposureControls, except doesn't care if normalized is allowed
        ExposureSettings ExposureControlsRawNormalized
        {
            get
            {
                return new ExposureSettings(
                    (int)averagingNumericUpDown.Value,
                    (int)integrationTimeMsNumericUpDown.Value,
                    (int)dwellTimeNumericUpDown.Value,
                    normalizedCheckBox.Checked,
                    ExposureSettingsName);
            }
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
                Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_UPDATED, ExposureControls);
        }

        void DisallowNormalized()
        {
            bool oldValue = normalizedCheckBox.Enabled;
            normalizedCheckBox.Enabled = false;
            if (normalizedCheckBox.Enabled != oldValue)
                Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_UPDATED, ExposureControls);
        }

        private void singleSpectrumButton_Click(object sender, EventArgs e)
        {
            var exposure = spectrometer.CollectSpectrum(ExposureControls, out string errMsg);
            if (exposure != null)
            {
                var singleGraph = new SingleSpectrumGraph();
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
            ControlsChanged += exposureStream.ControlsAdjustedEventHandler;
            RaiseSpectrometerControlsChangedEvent(ContinuousButton);
            var graph = new ContinuousSpectrumGraph();
            graph.ExposureStream = exposureStream;

            var tabPage = new TabPage("Continuous");
            SetMainControl(graph);

            exposureStream.Start();
        }

        void RaiseSpectrometerControlsChangedEvent(object sender)
        {
            UpdateFromForm();
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_UPDATED, ExposureControls);

            // this is just used by ExposureStream; need to refactor to use messages:
            if (ControlsChanged == null)
                return;
            ControlsChanged(sender, new SpectrometerControlsChangedEventArgs()
            {
                Settings = ExposureControls
            });
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
            spectrometer.CollectDarkReferenceExposure(ExposureControls.Unnormalized(), out string errMsg);
            if (spectrometer.DarkReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            { 
                showDarkRefButton_Click(sender, e);
                Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, spectrometer);
            }
        }

        private void whiteRefButton_Click(object sender, EventArgs e)
        {
            spectrometer.CollectWhiteReferenceExposure(ExposureControls.Unnormalized(), out string errMsg);
            if (spectrometer.WhiteReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                showWhiteRefButton_Click(sender, e);
                Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, spectrometer);
            }
        }

        private void showDarkRefButton_Click(object sender, EventArgs e)
        {
            var singleGraph = new SingleSpectrumGraph();
            singleGraph.Exposure = spectrometer.DarkReference;
            SetMainControl(singleGraph);
            singleGraph.Dock = DockStyle.Fill;
        }

        private void showWhiteRefButton_Click(object sender, EventArgs e)
        {
            var singleGraph = new SingleSpectrumGraph();
            singleGraph.Exposure = spectrometer.WhiteReference;
            SetMainControl(singleGraph);
            singleGraph.Dock = DockStyle.Fill;
        }

        private void HandleSpectrometerChanged(object sender, EventArgs e)
        {
            var sentSpectrometer = sender as ISpectrometer;
            if (sentSpectrometer == null)
                throw new Exception("Internal error. Sender was not an ISpectrometer.");
            if (sentSpectrometer != spectrometer)
                throw new Exception("Internal error. Sender ISpectrometer doesn't match the existing spectrometer.");
            UpdateFromSpectrometer();
        }

        private void UpdateFromSpectrometer()
        {
            if (spectrometer.NormalizeAllowed)
                AllowNormalized();
            else
                DisallowNormalized();

            showDarkRefButton.Enabled = spectrometer.DarkReference != null;
            showWhiteRefButton.Enabled = spectrometer.WhiteReference != null;
        }

        private void saveExposureSettingsButton_Click(object sender, EventArgs e)
        {
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_CREATE, ExposureControlsRawNormalized);
        }
    }
}
