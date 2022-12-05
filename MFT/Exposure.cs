using System;
using System.Linq;

namespace MFT
{
    public class Exposure
    {
        public Exposure(Spectrum spectrum, DateTime timeStamp, bool normalized)
        {
            Spectrum = spectrum;
            TimeStamp = timeStamp;
            Normalized = normalized;
        }

        // constructs an empty Exposure:
        public Exposure()
            : this(new Spectrum(), DateTime.MinValue, false) { }

        public Spectrum Spectrum { get; protected set; }
        public double MaxValue { get => Spectrum.Values.Max(); }
        public double MinValue { get => Spectrum.Values.Min(); }
        public DateTime TimeStamp { get; private set; }
        public float IntegrationTimeSeconds { get; internal set; }
        public int AveragingNum { get; internal set; }
        public bool Normalized { get; internal set; }
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

        public static Exposure operator /(Exposure A, Exposure B)
        {
            if (A == null)
                return null;
            if (B == null)
                throw new ArgumentNullException(nameof(B));
            var output = new Exposure();
            output.TimeStamp = A.TimeStamp;
            output.Normalized = A.Normalized && B.Normalized; // if either is unnormalized, quotient is unnormalized
            output.Spectrum = A.Spectrum / B.Spectrum;
            return output;
        }

        public static Exposure operator -(Exposure A, Exposure B)
        {
            if (A == null)
                return null;
            if (B == null)
                throw new ArgumentNullException(nameof(B));
            if (A.Normalized != B.Normalized)
                throw new Exception($"A.Normalized ({A.Normalized})" +
                    $" is not the same as B.Normalized ({B.Normalized})");
            var output = new Exposure();
            output.TimeStamp = A.TimeStamp;
            output.Normalized = A.Normalized;
            output.Spectrum = A.Spectrum - B.Spectrum;
            return output;
        }
    }
}
