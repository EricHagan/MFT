using System;
using System.Collections.Generic;
using System.Linq;
using RgbDriverKit;

namespace MFT
{
    internal class Qmini : ISpectrometer
    {
        public virtual bool Connect(out string ErrMsg)
        {
            ErrMsg = string.Empty;
            try
            {
                var devices = new List<Spectrometer>();
                foreach (var device in RgbSpectrometer.SearchDevices())
                    devices.Add(device);
                foreach (var device in Qseries.SearchDevices())
                    devices.Add(device);
                foreach (var device in Qstick.SearchDevices())
                    devices.Add(device);
                if (devices.Count() == 0)
                {
                    ErrMsg = "SearchDevices returned empty lists.";
                    return false;
                }
                spectrometer = devices[0];
                spectrometer.Open();
                GetWavelengths();
                return true;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return false;
            }
        }

        public string GetDeviceDescription()
        {
            if (spectrometer == null)
                return "No device connected.";
            return $"Name: {spectrometer.ModelName}, Mfg: {spectrometer.Manufacturer}";
    
        }

        public List<double> GetSpectrum()
        {
            var output = new List<double>();
            foreach(var value in spectrometer.GetSpectrum())
                output.Add(value);
            return output;
        }

        public DateTime GetTimeStamp()
        {
            if (spectrometer == null)
            {
                return DateTime.MinValue;
            }
            return spectrometer.TimeStamp;
        }

        public List<double> GetWavelengths()
        {
            if (spectrometer == null)
            {
                wavelengths = null;
                return new List<double>();
            }
            if (wavelengths != null)
                return wavelengths;
            var rawWavelengths = new List<double>(spectrometer.GetWavelengths());
            (startwaveindex, endwaveindex) = WavelengthMasker.GetStartAndEndIndex(rawWavelengths);
            wavelengths = rawWavelengths.GetRange(startwaveindex, endwaveindex - startwaveindex);
            return wavelengths;
        }

        public int StartWavelengthIndex { get => startwaveindex; }
        int startwaveindex;
        public int EndWavelengthIndex { get => endwaveindex; }
        int endwaveindex;

        public bool PerformExposure(float TimeSeconds, int Averaging, out string ErrMsg)
        {
            ErrMsg = string.Empty;
            if (spectrometer == null)
            {
                ErrMsg = "No device connected.";
                return false;
            }

            try
            {
                spectrometer.ExposureTime = TimeSeconds;
                spectrometer.Averaging = Averaging;
                spectrometer.StartExposure();
                int waitTimeMs = Math.Max(10, (int)(1000 * TimeSeconds / 10));
                while (spectrometer.Status > SpectrometerStatus.Idle)
                    System.Threading.Thread.Sleep(waitTimeMs);
                return true;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return false;
            }
        }
        protected Spectrometer spectrometer;
        protected List<double> wavelengths;
    }
}
