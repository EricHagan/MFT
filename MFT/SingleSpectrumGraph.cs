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
    public partial class SingleSpectrumGraph : UserControl
    {
        public SingleSpectrumGraph()
        {
            InitializeComponent();
        }

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
