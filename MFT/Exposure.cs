using System;
using System.Collections.Generic;
using System.Linq;

namespace MFT
{
    public class Exposure
    {
        public Exposure(ISpectrometer s, IEnumerable<double> spectrum, DateTime timeStamp, bool normalized)
        {
            Spectrometer = s;
            Spectrum = spectrum.ToList();
            TimeStamp = timeStamp;
            Normalized = normalized;
        }

        // constructs an empty Exposure:
        public Exposure(ISpectrometer s)
            : this(s, new List<double>(), DateTime.MinValue, false) { }

        public ISpectrometer Spectrometer { get; set; }
        public List<double> Spectrum { get; private set; }
        public double MaxReflectance { get => Spectrum.Max(); }
        public double MinReflectance { get => Spectrum.Min(); }
        public DateTime TimeStamp { get; private set; }
        public float IntegrationTimeSeconds { get; internal set; }
        public int AveragingNum { get; internal set; }
        public bool Normalized { get; private set; }
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(name))
                    return string.Format("Averaging = {0}, Int. Time = {1} s, Time = {2}{3}",
                        AveragingNum, IntegrationTimeSeconds, TimeStamp.ToString(), Normalized ? " (Normalized)" : "");
                else
                    return name;
            }
            set
            {
                name = value;
            }
    }
        string name;

        public Exposure GetNormalized()
        {
            Exposure whiteReference = Spectrometer.WhiteReference;
            Exposure darkReference = Spectrometer.DarkReference;
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
