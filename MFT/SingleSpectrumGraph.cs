using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MFT
{
    public partial class SingleSpectrumGraph : UserControl, ISecondaryControlsHolder
    {
        public SingleSpectrumGraph()
        {
            InitializeComponent();
            ExposureSettings = new ExposureSettingsControl();
        }

        protected void SingleSpectrumGraph_Load(object sender, EventArgs e)
        {
            if (!DesignTimeHelper.IsInDesignMode)
                Dock = DockStyle.Fill;
        }

        const int YAxisTicks = 10;

        public Exposure Exposure
        {
            get
            {
                return exposure;
            }
            set
            {
                exposure = value;
                UpdateValues();
            }
        }
        Exposure exposure;

        public void ExposureResampledHandler(object sender, ExposureResampledEventArgs e)
        {
            Exposure = e.ResampledExposure;
        }

        public ExposureSettingsControl ExposureSettings { get; set; }

        public virtual List<Control> SecondaryControls
        {
            get
            {
                return new List<Control>() { ExposureSettings };
            }
        }

        public void UpdateValues()
        {
            if (chart1.InvokeRequired)
            {
                Action safeUpdate = delegate { UpdateValues(); };
                chart1.Invoke(safeUpdate);
            }
            else
            {
                if (exposure == null)
                    return;

                chart1.Titles.Clear();
                chart1.Titles.Add(exposure.Name);

                var series = new Series();
                series.ChartType = SeriesChartType.Line;
                var wavelengths = exposure.Spectrometer.GetWavelengths();
                if (exposure.Spectrum.Count != wavelengths.Count)
                    throw new Exception($"Spectrum '{exposure.Name}' is a different length" +
                        $" ({exposure.Spectrum.Count}) than its spectrometer's wavelength array ({wavelengths.Count})");
                for (int i = 0; i < wavelengths.Count; i++)
                    series.Points.AddXY(wavelengths[i], exposure.Spectrum[i]);

                chart1.Series.Clear();
                chart1.Series.Add(series);

                chart1.Legends[0].Enabled = false;

                if (exposure.Normalized)
                    SetYAxisScale(-0.1, 1.1, 0.1);
                else
                {
                    SetYAxisScale(exposure.MinReflectance, exposure.MaxReflectance,
                                        (exposure.MaxReflectance - exposure.MinReflectance) / YAxisTicks);
                }

                chart1.Update();
            }
        }

        public void SetYAxisScale(double yMin, double yMax, double interval)
        {
            if (chart1 == null)
                return;
            chart1.ChartAreas[0].AxisY.Minimum = yMin;
            chart1.ChartAreas[0].AxisY.Maximum = yMax;
            chart1.ChartAreas[0].AxisY.Interval = interval;
        }


    }
}
