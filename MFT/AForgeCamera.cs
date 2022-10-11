using System;
using AForge.Video.DirectShow;

namespace MFT
{
    internal class AForgeCamera : ICamera
    {
        public AForgeCamera(VideoCaptureDevice camera, string name)
        {
            this.camera = camera;
            this.camera.NewFrame += InnerCameraNewFrameHandler;
            Name = name;
        }

        ~AForgeCamera()
        {
            Stop();
            camera.NewFrame -= InnerCameraNewFrameHandler;
            camera = null;
        }

        public string Name { get; private set; }

        public event EventHandler<NewFrameEventArgs> NewFrame;

        public void Start()
        {
            if (!camera.IsRunning)
                camera.Start();
        }

        public void Stop()
        {
            if (camera.IsRunning)
            {
                camera.SignalToStop();
                camera.WaitForStop();
            }
        }

        public bool IsRunning => camera.IsRunning;

        VideoCaptureDevice camera { get; set; }

        void InnerCameraNewFrameHandler(object sender, AForge.Video.NewFrameEventArgs e)
        {
            NewFrame?.Invoke(this, new NewFrameEventArgs(e.Frame));
        }
    }
}
