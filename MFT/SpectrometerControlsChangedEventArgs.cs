using System;

namespace MFT
{
    internal class SpectrometerControlsChangedEventArgs : EventArgs
    {
        public ExposureSettings Settings { get; set; }
    }
}
