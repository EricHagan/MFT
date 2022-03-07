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

        private void Form1_Load(object sender, EventArgs e)
        {
            spectrometerComboBox.Items.Clear();
            foreach (var t in Enum.GetValues(typeof(SpectrometerTypes)))
            {
                var d = new SpectrometerSelectionView();
                d.Type = (SpectrometerTypes)t; //explicit cast
                spectrometerComboBox.Items.Add(d);
            }
            spectrometerComboBox.SelectedIndex = 0;
            averagingNumericUpDown.Value = 10;
            integrationTimeMsNumericUpDown.Value = 50;
            dwellTimeNumericUpDown.Value = 100;
            ResetSpectrometer();
        }

        ISpectrometer spectrometer { get; set; }
        public event EventHandler<ControlsAdjustedEventArgs> ControlsAdjusted;

        private TabPage DarkSpectrum { get; set; }
        private TabPage WhiteSpectrum { get; set; }

        private void connectButton_Click(object sender, EventArgs e)
        {
            ResetSpectrometer();
            var selected = (SpectrometerSelectionView)spectrometerComboBox.SelectedItem;
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
            {
                spectrometerLabel.Text = spectrometer.GetDeviceDescription();
                spectrometer.NormalizeAllowedChanged += HandleAllowNormalizedChanged;
                SpectrometerChanged?.Invoke(this, new SpectrometerChangedEventArgs() { Spectrometer = spectrometer});
            }
        }

        void ResetSpectrometer()
        {
            spectrometer = null;
            spectrometerLabel.Text = string.Empty;
            DisallowNormalized();
            if (DarkSpectrum != null)
                tabControl1.TabPages.Remove(DarkSpectrum);
            if (WhiteSpectrum != null)
                tabControl1.TabPages.Remove(WhiteSpectrum);
            SpectrometerChanged?.Invoke(this, new SpectrometerChangedEventArgs()); // null spectrometer
        }

        public event EventHandler<SpectrometerChangedEventArgs> SpectrometerChanged;

        private void singleSpectrumButton_Click(object sender, EventArgs e)
        {
            var exposure = Exposure.GetExposure(spectrometer, (float)integrationTimeMsNumericUpDown.Value / 1000,
                (int)averagingNumericUpDown.Value, normalizedCheckBox.Checked, out string errMsg);
            if (exposure != null)
            {
                AddSingleSpectrumTab(exposure);
            }
            else
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        TabPage AddSingleSpectrumTab(Exposure exposure, bool forbidNormalizing = false, string tabName = "")
        {
            if (tabName == "")
                tabName = exposure.Name;
            var singleGraph = new SingleSpectrumGraph();
            singleGraph.Exposure = exposure;
            singleGraph.ExposureSettings.Spectrometer = spectrometer;
            singleGraph.ExposureSettings.ExposureResampled += singleGraph.ExposureResampledHandler;
            singleGraph.ExposureSettings.IntegrationTimeMs = (int)integrationTimeMsNumericUpDown.Value;
            singleGraph.ExposureSettings.Averaging = (int)averagingNumericUpDown.Value;
            singleGraph.ExposureSettings.ForbidNormalizing = forbidNormalizing;
            singleGraph.ExposureSettings.AllowNormalize = normalizedCheckBox.Enabled;
            singleGraph.ExposureSettings.Normalize = normalizedCheckBox.Checked;
            if (!forbidNormalizing)
                spectrometer.NormalizeAllowedChanged += singleGraph.ExposureSettings.HandleNormalizeAllowedChanged;
            SpectrometerChanged += singleGraph.ExposureSettings.HandleSpectrometerChanged;
            var tabPage = new TabPage(tabName);
            tabPage.Controls.Add(singleGraph);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
            return tabPage;
        }

        private void darkRefButton_Click(object sender, EventArgs e)
        {
            if (DarkSpectrum != null)
                tabControl1.TabPages.Remove(DarkSpectrum);
            if (spectrometer == null)
            {
                MessageBox.Show(this, $"Problem collecting spectrum: Spectrometer not connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!spectrometer.CollectDarkReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000, (int)averagingNumericUpDown.Value, out string errMsg))
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DarkSpectrum = AddSingleSpectrumTab(spectrometer.DarkReference, forbidNormalizing: true, "Dark");
        }

        private void whiteRefButton_Click(object sender, EventArgs e)
        {
            if (WhiteSpectrum != null)
                tabControl1.TabPages.Remove(WhiteSpectrum);
            if (spectrometer == null)
            {
                MessageBox.Show(this, $"Problem collecting spectrum: Spectrometer not connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!spectrometer.CollectWhiteReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000, (int)averagingNumericUpDown.Value, out string errMsg))
                MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            WhiteSpectrum = AddSingleSpectrumTab(spectrometer.WhiteReference, forbidNormalizing: true, "White");
        }

        void HandleAllowNormalizedChanged(object sender, NormalizeAllowedChangedEventArgs e)
        {
            if (e.NormalizeAllowed)
                AllowNormalized();
            else
                DisallowNormalized();
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

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            SyncDataAndSettingsControls();
        }

        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            SyncDataAndSettingsControls();
        }

        void SyncDataAndSettingsControls()
        {
            flowLayoutPanel1.Controls.Clear();
            if (tabControl1 == null || tabControl1.TabPages.Count == 0)
                return;
            foreach (Control mainControl in tabControl1.SelectedTab.Controls)
            {
                if (mainControl is ISecondaryControlsHolder)
                {
                    foreach (Control secondaryControl in ((ISecondaryControlsHolder)mainControl).SecondaryControls)
                    {
                        flowLayoutPanel1.Controls.Add(secondaryControl);
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new AboutDialog();
            d.ShowDialog();
        }
    }

    public class ControlsAdjustedEventArgs : EventArgs
    {
        public float IntegrationTimeS { get; set; }
        public int Averaging { get; set; }
        public int DwellTimeMs { get; set; }
    }

    public class SpectrometerChangedEventArgs : EventArgs
    {
        public ISpectrometer Spectrometer { get; set; }
    }
}
