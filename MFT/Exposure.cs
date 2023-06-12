using System;
using System.Linq;

namespace MFT
{
    public class Exposure
    {
        public Exposure(Spectrum spectrum, DateTime timeStamp, bool normalized, ProcessorChain chain = null)
        {
            RawSpectrum = spectrum;
            TimeStamp = timeStamp;
            Normalized = normalized;
            Chain = chain;
        }

        // constructs an empty Exposure:
        public Exposure()
            : this(new Spectrum(), DateTime.MinValue, false) { }

        Spectrum RawSpectrum;

        public Spectrum Spectrum { get => ProcessWithChain(RawSpectrum); }
        public double MaxValue { get => Spectrum.Values.Max(); }
        public double MinValue { get => Spectrum.Values.Min(); }
        public DateTime TimeStamp { get; private set; }
        public float IntegrationTimeSeconds { get; internal set; }
        public int AveragingNum { get; internal set; }
        public bool Normalized { get; internal set; }
        public ProcessorChain Chain { get; internal set; } = null;
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

        public Spectrum ProcessWithChain(Spectrum input)
        {
            if (Chain == null)
                return input;
            else
                return Chain.Process(input);
        }

        public static Exposure operator /(Exposure A, Exposure B)
        {
            if (A == null)
                return null;
            if (B == null)
                throw new ArgumentNullException(nameof(B));
            var output = new Exposure();
            output.TimeStamp = A.TimeStamp;
            output.Normalized = A.Normalized && B.Normalized; // if either is unnormalized, quotient is unnormalized
            if ((A.Chain != null) && (B.Chain == null))
                output.Chain = A.Chain;
            else if ((A.Chain == null) && (B.Chain != null))
                output.Chain = B.Chain;
            else if ((A.Chain == null) && (B.Chain == null))
                output.Chain = null;
            else if ((A.Chain != null ) && (B.Chain != null))
            {
                if (A.Chain == B.Chain)
                    output.Chain = A.Chain;
                else
                    throw new Exception("A.Chain not equal to B.Chain and neither null.");
            }
            output.RawSpectrum = A.RawSpectrum / B.RawSpectrum;
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
            if ((A.Chain != null) && (B.Chain == null))
                output.Chain = A.Chain;
            else if ((A.Chain == null) && (B.Chain != null))
                output.Chain = B.Chain;
            else if ((A.Chain == null) && (B.Chain == null))
                output.Chain = null;
            else if ((A.Chain != null) && (B.Chain != null))
            {
                if (A.Chain == B.Chain)
                    output.Chain = A.Chain;
                else
                    throw new Exception("A.Chain not equal to B.Chain and neither null.");
            }
            output.RawSpectrum = A.RawSpectrum - B.RawSpectrum;
            return output;
        }
    }
}
