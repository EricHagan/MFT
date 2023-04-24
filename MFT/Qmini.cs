using System;
using System.Collections.Generic;
using System.Linq;
using RgbDriverKit;

namespace MFT
{
    internal class Qmini : SpectrometerBase
    {
        public override bool Connect(out string ErrMsg)
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
        
        public override SpectrometerTypes GetDeviceType()
        {
            return SpectrometerTypes.BROADCOM;
        }

        public override string GetDeviceDescription()
        {
            if (spectrometer == null)
                return "No device connected.";
            return $"Name: {spectrometer.ModelName}, Mfg: {spectrometer.Manufacturer}";
    
        }

        public override List<double> GetWavelengths()
        {
            if (spectrometer == null)
            {
                wavelengths = null;
                return new List<double>();
            }
            if (wavelengths != null)
                return wavelengths;
            return new List<double>(spectrometer.GetWavelengths()); ;
        }

        public override Exposure CollectExposure(ExposureSettings settings, SpectrumProcessorChain Chain, out string ErrMsg)
        {
            lock (collectLock)
            {
                ErrMsg = string.Empty;
                if (!PerformExposure(settings, out ErrMsg))
                {
                    return null;
                }
                var spectrum = new Spectrum(GetWavelengths(),
                    spectrometer.GetSpectrum().Select(x => (double)x).ToList());
                var TimeStamp = GetTimeStamp();
                var exposure = new Exposure(spectrum, TimeStamp, false);
                if (settings.Normalized)
                    exposure = Normalize(exposure);
                exposure.IntegrationTimeSeconds = settings.IntegrationTimeMs / 1000;
                exposure.AveragingNum = settings.Averaging;
                exposure = ProcessWithChain(exposure);
                return exposure;
            }
        }

        private static readonly object collectLock = new object();

        protected Spectrometer spectrometer;
        protected List<double> wavelengths;

        protected bool PerformExposure(ExposureSettings settings, out string ErrMsg)
        {
            ErrMsg = string.Empty;
            if (spectrometer == null)
            {
                ErrMsg = "No device connected.";
                return false;
            }

            try
            {
                spectrometer.ExposureTime = settings.IntegrationTimeMs/1000f;
                spectrometer.Averaging = settings.Averaging;
                spectrometer.StartExposure();
                int waitTimeMs = Math.Max(10, (int)(settings.IntegrationTimeMs / 10));
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

        protected List<double> GetSpectrumValues()
        {
            var output = new List<double>();
            foreach (var value in spectrometer.GetSpectrum())
                output.Add(value);
            return output;
        }

        protected DateTime GetTimeStamp()
        {
            if (spectrometer == null)
            {
                return DateTime.MinValue;
            }
            return spectrometer.TimeStamp;
        }
    }
}
