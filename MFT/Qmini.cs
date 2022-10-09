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

        public virtual SpectrometerTypes GetDeviceType()
        {
            return SpectrometerTypes.BROADCOM;
        }

        public string GetDeviceDescription()
        {
            if (spectrometer == null)
                return "No device connected.";
            return $"Name: {spectrometer.ModelName}, Mfg: {spectrometer.Manufacturer}";
    
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
            return new List<double>(spectrometer.GetWavelengths()); ;
        }

        public Exposure CollectSpectrum(float TimeSeconds, int Averaging, out string ErrMsg)
        {
            lock (collectLock)
            {
                ErrMsg = string.Empty;
                if (!PerformExposure(TimeSeconds, Averaging, out ErrMsg))
                {
                    return null;
                }
                var Spectrum = spectrometer.GetSpectrum().ToList();
                var TimeStamp = GetTimeStamp();
                return new Exposure(this, Spectrum.Select(x => (double)x), TimeStamp, false);
            }
        }

        private static readonly object collectLock = new object();

        public bool CollectWhiteReferenceExposure(float TimeSeconds, int Averaging, out string ErrMsg)
        {
            WhiteReference = Exposure.GetExposure(this, TimeSeconds, Averaging, false, out ErrMsg);
            UpdateNormalizeAllowed();
            if (WhiteReference != null)
            {
                if (!WhiteReference.Name.Contains("White Reference: "))
                    WhiteReference.Name = "White Reference: " + WhiteReference.Name;
                SpectrometerChanged?.Invoke(this, new EventArgs());
            }
            return WhiteReference != null;
        }

        public bool CollectDarkReferenceExposure(float TimeSeconds, int Averaging, out string ErrMsg)
        {
            TriedToGetDarkReference = true;
            DarkReference = Exposure.GetExposure(this, TimeSeconds, Averaging, false, out ErrMsg);
            UpdateNormalizeAllowed();
            if (DarkReference != null)
            {
                if (!DarkReference.Name.Contains("Dark Reference: "))
                    DarkReference.Name = "Dark Reference: " + DarkReference.Name;
                SpectrometerChanged?.Invoke(this, new EventArgs());
            }
            return DarkReference != null;
        }

        public Exposure WhiteReference { get; set; }
        public Exposure DarkReference { get; set; }

        public bool NormalizeAllowed { get; private set; }
        public event EventHandler SpectrometerChanged;

        protected bool TriedToGetDarkReference { get; set; }

        protected void UpdateNormalizeAllowed()
        {
            bool oldValue = NormalizeAllowed;
            if (WhiteReference == null
                || (TriedToGetDarkReference && DarkReference == null))
                NormalizeAllowed = false;
            else
                NormalizeAllowed = true;

            if (NormalizeAllowed != oldValue)
            {
                SpectrometerChanged?.Invoke(this, new EventArgs());
            }
        }

        protected Spectrometer spectrometer;
        protected List<double> wavelengths;

        protected bool PerformExposure(float TimeSeconds, int Averaging, out string ErrMsg)
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

        protected List<double> GetSpectrum()
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
