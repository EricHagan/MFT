using System;

namespace MFT
{
    public class ControlsAdjustedEventArgs : EventArgs
    {
        public float IntegrationTimeS { get; set; }
        public int Averaging { get; set; }
        public int DwellTimeMs { get; set; }
    }
}
