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
            if (spectrometer.NormalizeAllowed)
                AllowNormalized();
            else
                DisallowNormalized();
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

        private void darkRefButton_Click(object sender, EventArgs e)
        {
            spectrometer.CollectDarkReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out string errMsg);
            RaiseSpectrometerControlsChangedEvent(darkRefButton);
            if (spectrometer.DarkReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
