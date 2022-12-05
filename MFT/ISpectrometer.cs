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
        ExposureSettings Settings { get; set; }
        Exposure CollectExposure(ExposureSettings Setttings, out string ErrMsg);
        bool CollectWhiteReferenceExposure(ExposureSettings Setttings, out string ErrMsg);
        bool CollectDarkReferenceExposure(ExposureSettings Setttings, out string ErrMsg);
        Exposure WhiteReference { get; set; }
        Exposure DarkReference { get; set; }
        bool NormalizeAllowed { get; }
        Exposure Normalize(Exposure input);
    }
}
