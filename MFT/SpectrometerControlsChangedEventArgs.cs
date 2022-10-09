using System;

namespace MFT
{
    public class SpectrometerControlsChangedEventArgs : EventArgs
    {
        public ExposureSettings Settings { get; set; }
    }
}
