using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MFT
{
    public partial class ExposureControl : UserControl
    {
        public ExposureControl()
        {
            InitializeComponent();
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
                if (exposure.Spectrum == null)
                    return;
                if (!exposure.Spectrum.IsGood(out string errMsg))
                    throw new Exception($"Problem with exposure '{exposure.Name}': {errMsg}");

                chart1.Titles.Clear();
                chart1.Titles.Add(exposure.Name);

                var series = new Series();
                series.ChartType = SeriesChartType.Line;
                for (int i = 0; i < Exposure.Spectrum.WavelengthsNm.Count; i++)
                    series.Points.AddXY(Exposure.Spectrum.WavelengthsNm[i],
                        Exposure.Spectrum.Values[i]);

                chart1.Series.Clear();
                chart1.Series.Add(series);

                chart1.Legends[0].Enabled = false;

                if (exposure.Normalized)
                    SetYAxisScale(-0.1, 1.1, 0.1);
                else
                {
                    SetYAxisScale(exposure.MinValue, exposure.MaxValue,
                                        (exposure.MaxValue - exposure.MinValue) / YAxisTicks);
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
