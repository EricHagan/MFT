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
    public partial class SpectrometerControl : UserControl
    {
        public SpectrometerControl(ISpectrometer _spectrometer)
        {
            InitializeComponent();
            spectrometer = _spectrometer;
        }

        ISpectrometer spectrometer;
        public event EventHandler<SpectrometerChangedEventArgs> SpectrometerChanged;


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
            SpectrometerChanged += exposureStream.ControlsAdjustedEventHandler;
            var graph = new ContinuousSpectrumGraph();
            graph.ExposureStream = exposureStream;

            var tabPage = new TabPage("Continuous");
            SetMainControl(graph);

            exposureStream.Start();
        }

        void RaiseSpectrometerChangedEvent(object sender)
        {
            if (SpectrometerChanged == null)
                return;
            SpectrometerChanged(sender, new SpectrometerChangedEventArgs()
            {
                Averaging = (int)averagingNumericUpDown.Value,
                DwellTimeMs = (int)dwellTimeNumericUpDown.Value,
                IntegrationTimeS = (float)(integrationTimeMsNumericUpDown.Value / 1000),
                Spectrometer = spectrometer
            });
        }

        private void averagingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseSpectrometerChangedEvent(averagingNumericUpDown);
        }

        private void integrationTimeMsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseSpectrometerChangedEvent(integrationTimeMsNumericUpDown);
        }

        private void dwellTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseSpectrometerChangedEvent(dwellTimeNumericUpDown);
        }

        private void darkRefButton_Click(object sender, EventArgs e)
        {
            spectrometer.CollectDarkReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out string errMsg);
            RaiseSpectrometerChangedEvent(darkRefButton);
            if (spectrometer.DarkReference == null)
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
