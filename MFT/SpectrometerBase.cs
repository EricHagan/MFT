using System;
using System.Collections.Generic;

namespace MFT
{
    internal abstract class SpectrometerBase : ISpectrometer
    {
        #region ISpectrometer

        public Exposure WhiteReference { get; set; }
        public Exposure DarkReference { get; set; }

        public bool NormalizeAllowed { get; protected set; }

        public event EventHandler SpectrometerChanged;

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

        public abstract Exposure CollectSpectrum(float TimeSeconds, int Averaging, out string ErrMsg);
        public abstract bool Connect(out string ErrMsg);
        public abstract string GetDeviceDescription();
        public abstract SpectrometerTypes GetDeviceType();
        public abstract List<double> GetWavelengths();

        #endregion

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
    }
}
