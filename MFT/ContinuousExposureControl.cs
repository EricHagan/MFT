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
    public partial class ContinuousExposureControl : ExposureControl
    {
        public ContinuousExposureControl()
        {
            InitializeComponent();
        }

        public ExposureStream ExposureStream
        { 
            get => exposureStream;
            set
            {
                if (exposureStream != null)
                {
                    exposureStream.ExposureAvailable -= ExposureAvailableEventHandler;
                }
                if (value != null)
                {
                    exposureStream = value;
                    exposureStream.ExposureAvailable += ExposureAvailableEventHandler;
                }
            }             
        }
        ExposureStream exposureStream;

        void ExposureAvailableEventHandler(object sender, ExposureEventArgs e)
        {
            Exposure = e.Exposure; // this will trigger Update() in SingleSpectrumGraph
        }

        private void ContinuousExposureControl_Load(object sender, EventArgs e)
        {
            ExposureControl_Load(sender, e);
            Dock = DockStyle.Fill;
        }
    }
}
