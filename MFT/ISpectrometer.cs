using System;
using System.Collections.Generic;

namespace MFT
{
    public interface ISpectrometer
    {
        bool Connect(out string ErrMsg);
        string GetDeviceDescription();
        List<double> GetWavelengths();
        int StartWavelengthIndex { get; }
        int EndWavelengthIndex { get; }       
        List<double> GetSpectrum();
        DateTime GetTimeStamp();
        bool PerformExposure(float TimeSeconds, int Averaging, out string ErrMsg);
        Exposure WhiteReference { get; set; }
        Exposure DarkReference { get; set; }
    }
}
