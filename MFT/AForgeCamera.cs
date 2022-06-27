using System;
using AForge.Video.DirectShow;

namespace MFT
{
    internal class AForgeCamera : ICamera
    {
        public AForgeCamera(VideoCaptureDevice camera, string name)
        {
            Camera = camera;
            Camera.NewFrame += InnerCameraNewFrameHandler;
            Name = name;
        }

        ~AForgeCamera()
        {
            Stop();
            Camera.NewFrame -= InnerCameraNewFrameHandler;
            Camera = null;
        }

        public string Name { get; private set; }

        public event EventHandler<NewFrameEventArgs> NewFrame;

        public void Start()
        {
            if (!Camera.IsRunning)
                Camera.Start();
        }

        public void Stop()
        {
            if (Camera.IsRunning)
            {
                Camera.SignalToStop();
                Camera.WaitForStop();
            }
        }

        VideoCaptureDevice Camera { get; set; }

        void InnerCameraNewFrameHandler(object sender, AForge.Video.NewFrameEventArgs e)
        {
            NewFrame?.Invoke(this, new NewFrameEventArgs(e.Frame));
        }
    }
}
