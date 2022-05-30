﻿using System;
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
            Camera.SignalToStop();
            Camera.NewFrame -= InnerCameraNewFrameHandler;
            Camera = null;
        }

        public string Name { get; private set; }

        public event EventHandler<NewFrameEventArgs> NewFrame;

        public void Start()
        {
            Camera.Start();
        }

        public void Stop()
        {
            Camera.SignalToStop();
            Camera.WaitForStop();
        }

        VideoCaptureDevice Camera { get; set; }

        void InnerCameraNewFrameHandler(object sender, AForge.Video.NewFrameEventArgs e)
        {
            NewFrame?.Invoke(this, new NewFrameEventArgs(e.Frame));
        }
    }
}
