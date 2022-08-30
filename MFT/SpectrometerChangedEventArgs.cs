using System;

namespace MFT
{
    public class SpectrometerChangedEventArgs : EventArgs
    {
        public float IntegrationTimeS { get; set; }
        public int Averaging { get; set; }
        public int DwellTimeMs { get; set; }
        public ISpectrometer Spectrometer { get; set; }
    }
}
