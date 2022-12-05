using System;
using System.Collections.Generic;

namespace MFT
{
    internal abstract class SpectrometerBase : ISpectrometer
    {
        #region ISpectrometer

        public ExposureSettings Settings { get; set; } = new ExposureSettings();

        public Exposure WhiteReference { get; set; }
        public Exposure DarkReference { get; set; }

        public bool NormalizeAllowed { get; protected set; }

        public bool CollectDarkReferenceExposure(ExposureSettings settings, out string ErrMsg)
        {
            TriedToGetDarkReference = true;
            if (settings.Normalized == true)
                throw new Exception("settings.Normalized must be false for a reference exposure.");
            DarkReference = CollectExposure(settings, out ErrMsg);
            UpdateNormalizeAllowed();
            if (DarkReference != null)
            {
                if (!DarkReference.Name.Contains("Dark Reference: "))
                    DarkReference.Name = "Dark Reference: " + DarkReference.Name;
                Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, this);
            }
            return DarkReference != null;
        }

        public bool CollectWhiteReferenceExposure(ExposureSettings settings, out string ErrMsg)
        {
            if (settings.Normalized == true)
                throw new Exception("settings.Normalized must be false for a reference exposure.");
            WhiteReference = CollectExposure(settings, out ErrMsg);
            UpdateNormalizeAllowed();
            if (WhiteReference != null)
            {
                if (!WhiteReference.Name.Contains("White Reference: "))
                    WhiteReference.Name = "White Reference: " + WhiteReference.Name;
                Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, this);
            }
            return WhiteReference != null;
        }

        public Exposure Normalize(Exposure input)
        {
            Exposure normalized;
            if (DarkReference != null)
                normalized = (input - DarkReference) / (WhiteReference - DarkReference);
            else
                normalized = input / WhiteReference;
            normalized.Name = input.Name + " (Normalized)";
            normalized.Normalized = true;
            return normalized;
        }


        public abstract Exposure CollectExposure(ExposureSettings settings, out string ErrMsg);
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
                Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, this);
            }
        }
    }
}
