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
    public partial class CameraDialog : UserControl, ISecondaryControlsHolder
    {
        public CameraDialog()
        {
            InitializeComponent();
        }

        private void CameraDialog_Load(object sender, EventArgs e)
        {
            if (!DesignTimeHelper.IsInDesignMode)
                Dock = DockStyle.Fill;
        }

        public void SetCamera(ICamera camera)
        {
            Camera = camera;
            Camera.NewFrame += NewFrameHandler;
        }

        ICamera Camera;

        public List<Control> SecondaryControls => throw new NotImplementedException();

        void NewFrameHandler(object sender, NewFrameEventArgs e)
        {
            pictureBox1.Image = (Bitmap)e.Frame.Clone();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Camera.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Camera.Stop();
        }
    }
}
