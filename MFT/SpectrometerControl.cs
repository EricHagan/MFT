using System;
using System.Windows.Forms;

namespace MFT
{
    public partial class SpectrometerControl : UserControl
    {
        public SpectrometerControl(ISpectrometer _spectrometer)
        {
            InitializeComponent();
            spectrometer = _spectrometer;
            spectrometer.SpectrometerChanged += HandleSpectrometerChanged;
            UpdateFromSpectrometer();
        }

        ISpectrometer spectrometer;
        public event EventHandler<SpectrometerControlsChangedEventArgs> ControlsChanged;

        private void SpectrometerDialog_Load(object sender, EventArgs e)
        {
            averagingNumericUpDown.Value = 10;
            integrationTimeMsNumericUpDown.Value = 50;
            dwellTimeNumericUpDown.Value = 100;
        }

        void AllowNormalized()
        {
            normalizedCheckBox.Enabled = true;
        }

        void DisallowNormalized()
        {
            normalizedCheckBox.Checked = false;
            normalizedCheckBox.Enabled = false;
        }

        private void singleSpectrumButton_Click(object sender, EventArgs e)
        {
            var exposure = Exposure.GetExposure(spectrometer, (float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, normalizedCheckBox.Checked, out string errMsg);
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
            var graph = new ContinuousSpectrumGraph();
            graph.ExposureStream = exposureStream;

            var tabPage = new TabPage("Continuous");
            SetMainControl(graph);

            exposureStream.Start();
        }

        void RaiseSpectrometerControlsChangedEvent(object sender)
        {
            if (ControlsChanged == null)
                return;
            ControlsChanged(sender, new SpectrometerControlsChangedEventArgs()
            {
                Averaging = (int)averagingNumericUpDown.Value,
                DwellTimeMs = (int)dwellTimeNumericUpDown.Value,
                IntegrationTimeS = (float)(integrationTimeMsNumericUpDown.Value / 1000),
                Normalize = normalizedCheckBox.Checked,
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
            spectrometer.CollectDarkReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out string errMsg);
            if (spectrometer.DarkReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void whiteRefButton_Click(object sender, EventArgs e)
        {
            spectrometer.CollectWhiteReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out string errMsg);
            if (spectrometer.WhiteReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
