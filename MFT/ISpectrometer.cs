using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    public interface ISpectrometer
    {
        bool Connect(out string ErrMsg);
        string GetDeviceDescription();
        List<double> GetWavelengths();
        int StartWavelengthIndex { get; }
        int EndWavelengthIndex { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<double> GetSpectrum();
        DateTime GetTimeStamp();
        bool PerformExposure(float TimeSeconds, int Averaging, out string ErrMsg);

    }
}
