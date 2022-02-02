using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFT
{
    public class Exposure
    {
        public static Exposure GetExposure(ISpectrometer spectrometer, float TimeSeconds, int Averaging, bool Normalized,
             out string ErrMsg, Exposure WhiteReference = null, Exposure DarkReference = null)
        {
            if (spectrometer == null)
            {
                ErrMsg = "Spectrometer not connected.";
                return null;
            }
            var exposure = new Exposure(spectrometer);
            if (!exposure.CollectSpectrum(TimeSeconds, Averaging, out ErrMsg))
                return null;
            else
            {
                if (Normalized)
                    exposure = exposure.GetNormalized(WhiteReference, DarkReference);
                return exposure;
            }
        }

        public Exposure(ISpectrometer s) // constructor
        {
            Spectrometer = s;
            Spectrum = new List<double>();
            TimeStamp = DateTime.MinValue;
            Normalized = false;
        }

        public ISpectrometer Spectrometer { get; set; }
        public List<double> Spectrum { get; private set; }
        public double MaxReflectance { get => Spectrum.Max(); }
        public double MinReflectance { get => Spectrum.Min(); }
        public DateTime TimeStamp { get; private set; }
        public bool Normalized { get; private set; }
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(name))
                    return TimeStamp.ToString();
                else
                    return name;
            }
            set
            {
                name = value;
            }
        }
        string name;

        public bool CollectSpectrum(float TimeSeconds, int Averaging, out string ErrMsg)
        {
            lock (collectLock)
            {
                ErrMsg = string.Empty;
                Spectrum = new List<double>();
                TimeStamp = DateTime.MinValue;
                if (Spectrometer == null)
                {
                    ErrMsg = "Spectrum not collected.";
                    return false;
                }

                if (!Spectrometer.PerformExposure(TimeSeconds, Averaging, out ErrMsg))
                {
                    return false;
                }
                var RawSpectrum = Spectrometer.GetSpectrum();
                Spectrum = RawSpectrum.GetRange(Spectrometer.StartWavelengthIndex, Spectrometer.EndWavelengthIndex - Spectrometer.StartWavelengthIndex);
                TimeStamp = Spectrometer.GetTimeStamp();
                return true;
            }

        }

        /// <summary>
        /// We problably don't need this async method
        /// </summary>
        public Task<bool> CollectSpectrumAsync(float TimeSeconds, int Averaging, IProgress<string> progress)
        {
            return Task.Run(() =>
            {
                string errMsg;
                return CollectSpectrum(TimeSeconds, Averaging, out errMsg);
            });
        }

        private static readonly object collectLock = new object();

        public Exposure GetNormalized(Exposure whiteReference, Exposure darkReference)
        {
            Exposure normalized;
            if (darkReference != null)
                normalized = (this - darkReference) / (whiteReference - darkReference);
            else
                normalized = this / whiteReference;
            normalized.Name = Name + " (Normalized)";
            normalized.Normalized = true;
            return normalized;
        }

        public static Exposure operator /(Exposure A, Exposure B)
        {
            if (A == null)
                return null;
            if (B == null)
                throw new ArgumentNullException(nameof(B));
            if (A.Spectrum.Count != B.Spectrum.Count)
                throw new Exception($"A.Spectrum.Count ({A.Spectrum.Count})" +
                    $" is not the same as B.Spectrum.Count ({B.Spectrum.Count})");
            if (A.Spectrometer != B.Spectrometer)
                throw new Exception("A and B do not point to the same spectrometer.");
            var output = new Exposure(A.Spectrometer);
            output.TimeStamp = A.TimeStamp;
            output.Normalized = A.Normalized && B.Normalized; // if either is unnormalized, quotient is unnormalized
            for (int i = 0; i < A.Spectrum.Count; i++)
            {
                if (B.Spectrum[i] == 0.0)
                    output.Spectrum.Add(double.NaN);
                else
                    output.Spectrum.Add(A.Spectrum[i] / B.Spectrum[i]);
            }
            return output;
        }

        public static Exposure operator -(Exposure A, Exposure B)
        {
            if (A == null)
                return null;
            if (B == null)
                throw new ArgumentNullException(nameof(B));
            if (A.Spectrum.Count != B.Spectrum.Count)
                throw new Exception($"A.Spectrum.Count ({A.Spectrum.Count})" +
                    $" is not the same as B.Spectrum.Count ({B.Spectrum.Count})");
            if (A.Spectrometer != B.Spectrometer)
                throw new Exception("A and B do not point to the same spectrometer.");
            if (A.Normalized != B.Normalized)
                throw new Exception($"A.Normalized ({A.Normalized})" +
                    $" is not the same as B.Normalized ({B.Normalized})");
            var output = new Exposure(A.Spectrometer);
            output.TimeStamp = A.TimeStamp;
            output.Normalized = A.Normalized;
            for (int i = 0; i < A.Spectrum.Count; i++)
            {
                output.Spectrum.Add(A.Spectrum[i] - B.Spectrum[i]);
            }
            return output;
        }

    }
}
