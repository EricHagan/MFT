using System;
using System.Drawing;

namespace MFT
{
    public interface ICamera
    {
        string Name { get; }
        void Start();
        void Stop();
        event EventHandler<NewFrameEventArgs> NewFrame;
    }

    public class NewFrameEventArgs : EventArgs
    {
        public NewFrameEventArgs(Bitmap frame)
        {
            Frame = frame;
        }
        public Bitmap Frame { get; set; }
    }
}
