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
                singleGraph.ExposureSettings.Spectrometer = spectrometer;
                singleGraph.ExposureSettings.ExposureResampled += singleGraph.ExposureResampledHandler;
                singleGraph.ExposureSettings.IntegrationTimeMs = (int)integrationTimeMsNumericUpDown.Value;
                singleGraph.ExposureSettings.Averaging = (int)averagingNumericUpDown.Value;
                singleGraph.ExposureSettings.ForbidNormalizing = false;
                singleGraph.ExposureSettings.AllowNormalize = true;
                singleGraph.ExposureSettings.Normalize = normalizedCheckBox.Checked;

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
            var graph = new ContinuousSpectrumGraph();
            graph.ExposureStream = exposureStream;

            var tabPage = new TabPage("Continuous");
            SetMainControl(graph);

            exposureStream.Start();
        }
    }
}
