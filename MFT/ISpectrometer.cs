using System;
using System.Collections.Generic;

namespace MFT
{
    public interface ISpectrometer
    {
        bool Connect(out string ErrMsg);
        SpectrometerTypes GetDeviceType();
        string GetDeviceDescription();
        List<double> GetWavelengths();    
        Exposure CollectSpectrum(float TimeSeconds, int Averaging, out string ErrMsg);
        bool CollectWhiteReferenceExposure(float TimeSeconds, int Averaging, out string ErrMsg);
        bool CollectDarkReferenceExposure(float TimeSeconds, int Averaging, out string ErrMsg);
        Exposure WhiteReference { get; set; }
        Exposure DarkReference { get; set; }
        bool NormalizeAllowed { get; }
        event EventHandler<NormalizeAllowedChangedEventArgs> NormalizeAllowedChanged;
    }

    public class NormalizeAllowedChangedEventArgs : EventArgs
    {
        public bool NormalizeAllowed { get; set; }
    }
}
