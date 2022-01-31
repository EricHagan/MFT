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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int YAxisTicks = 10;

        ISpectrometer spectrometer { get; set; }
        public Exposure DarkReference { get; set; }
        public Exposure WhiteReference { get; set; }
        public event EventHandler<ControlsAdjustedEventArgs> ControlsAdjusted;

        private TabPage DarkSpectrum { get; set; }
        private TabPage WhiteSpectrum { get; set; }

        private void connectButton_Click(object sender, EventArgs e)
        {
            ResetSpectrometer();
            var selected = (SpectrometerDescription)spectrometerComboBox.SelectedItem;
            try
            {
                spectrometer = SpectrometerFactory.GetSpectrometer(selected.Type);
            }
            catch (NotImplementedException)
            {
                MessageBox.Show(this, $"Internal error: unknown device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string ErrMsg;
            if (!spectrometer.Connect(out ErrMsg))
                MessageBox.Show(this, $"Problem connecting: {ErrMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                spectrometerLabel.Text = spectrometer.GetDeviceDescription();
        }

        void ResetSpectrometer()
        {
            spectrometer = null;
            spectrometerLabel.Text = string.Empty;
            DarkReference = null;
            WhiteReference = null;
            DisallowNormalized();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            spectrometerComboBox.Items.Clear();
            foreach(var t in Enum.GetValues(typeof(SpectrometerTypes)))
            {
                var d = new SpectrometerDescription();
                d.Type = (SpectrometerTypes)t; //explicit cast
                spectrometerComboBox.Items.Add(d);
            }
            spectrometerComboBox.SelectedIndex = 0;
            averagingNumericUpDown.Value = 10;
            integrationTimeMsNumericUpDown.Value = 50;
            dwellTimeNumericUpDown.Value = 100;
            DisallowNormalized();
        }

        private void singleSpectrumButton_Click(object sender, EventArgs e)
        {
            var exposure = GetExposure((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out string errMsg);
            if (exposure != null)
            {
                AddSingleSpectrumTab(exposure);
            }
            else
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Exposure GetExposure(float TimeSeconds, int Averaging, out string ErrMsg)
        {        
            if (spectrometer == null)
            {
                ErrMsg = "Spectrometer not connected.";
                return null;
            }
            var exposure = new Exposure(spectrometer);
            if (!exposure.CollectSpectrum((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out ErrMsg))
                return null;
            else
                return exposure;
        }

        TabPage AddSingleSpectrumTab(Exposure exposure, bool allowNormalized = true, string tabName = "")
        {
            if (tabName == "")
                tabName = exposure.Name;
            var singleGraph = new SingleSpectrumGraph();
            if (!allowNormalized || !normalizedCheckBox.Checked)
            {
                singleGraph.Exposure = exposure;
                singleGraph.SetYAxisScale(exposure.MinReflectance, exposure.MaxReflectance,
                    (exposure.MaxReflectance - exposure.MinReflectance) / YAxisTicks);
            }
            else
            {
                singleGraph.Exposure = exposure.GetNormalized(WhiteReference, DarkReference);
                singleGraph.SetYAxisScale(-0.1, 1.1, 0.1);
            }
            var tabPage = new TabPage(tabName);
            tabPage.Controls.Add(singleGraph);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
            return tabPage;
        }

        private void darkRefButton_Click(object sender, EventArgs e)
        {
            var exposure = GetExposure((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out string errMsg);
            if (exposure != null)
            {
                DarkReference = exposure;
                if (DarkSpectrum != null)
                    tabControl1.TabPages.Remove(DarkSpectrum);
                DarkSpectrum = AddSingleSpectrumTab(DarkReference, false, "Dark");
                if (WhiteReference != null)
                    AllowNormalized();
            }
            else
            {
                DisallowNormalized();
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void whiteRefButton_Click(object sender, EventArgs e)
        {
            var exposure = GetExposure((float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, out string errMsg);
            if (exposure != null)
            {
                WhiteReference = exposure;
                if (WhiteSpectrum != null)
                    tabControl1.TabPages.Remove(WhiteSpectrum);
                WhiteSpectrum = AddSingleSpectrumTab(WhiteReference, false, "White");
                AllowNormalized();
            }
            else
            {
                DisallowNormalized();
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        string GetNextContinuousName()
        {
            return "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var exposureStream = new ExposureStream(spectrometer);
            ControlsAdjusted += exposureStream.ControlsAdjustedEventHandler;
            var graph = new ContinuousSpectrumGraph();
            graph.ExposureStream = exposureStream;

            var tabPage = new TabPage("Continuous");
            tabPage.Controls.Add(graph);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;

            exposureStream.Start();
        }

        private void averagingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseControlsAdjustedEvent(averagingNumericUpDown);
        }

        private void integrationTimeMsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseControlsAdjustedEvent(integrationTimeMsNumericUpDown);
        }

        private void dwellTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseControlsAdjustedEvent(dwellTimeNumericUpDown);
        }

        void RaiseControlsAdjustedEvent(object sender)
        {
            if (ControlsAdjusted == null)
                return;
            ControlsAdjusted(sender, new ControlsAdjustedEventArgs()
            {
                Averaging = (int)averagingNumericUpDown.Value,
                DwellTimeMs = (int)dwellTimeNumericUpDown.Value,
                IntegrationTimeS = (float)(integrationTimeMsNumericUpDown.Value / 1000),
            });
        }
    }

    public class ControlsAdjustedEventArgs : EventArgs
    {
        public float IntegrationTimeS { get; set; }
        public int Averaging { get; set; }
        public int DwellTimeMs { get; set; }
    }
}
