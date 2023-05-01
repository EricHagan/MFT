using System;
using System.Linq;

namespace MFT
{
    internal class SpectrumWindow : ISpectrumProcessor
    {
        public static double DefaultMinWavelengthNm { get; internal set; } = 380.0;
        public static double DefaultMaxWavelengthNm { get; internal set; } = 840.0;

        public double MinWavelengthNm { get; set; } = DefaultMinWavelengthNm;
        public double MaxWavelengthNm { get; set; } = DefaultMaxWavelengthNm;

        public Spectrum Process(Spectrum data)
        {
            if (!data.IsGood(out string errMsg))
                throw new Exception("Problem with input spectrum: " + errMsg);

            double startWaveLength, endWaveLength;
            int startIndex, endIndex;
            try
            {
                startWaveLength = data.WavelengthsNm.First(w => w > MinWavelengthNm);
                startIndex = data.WavelengthsNm.IndexOf(startWaveLength) - 1;
            }
            catch (InvalidOperationException)
            {
                startIndex = 0;
            }
            if (startIndex < 0 || startIndex > data.WavelengthsNm.Count - 1)
                throw new Exception($"startIndex ({startIndex}) out of bounds");
            try
            {
                endWaveLength = data.WavelengthsNm.First(w => w > MaxWavelengthNm);
                endIndex = data.WavelengthsNm.IndexOf(endWaveLength);
            }
            catch (InvalidOperationException)
            {
                endIndex = data.WavelengthsNm.Count - 1;
            }
            if (endIndex < 0 || endIndex > data.WavelengthsNm.Count - 1)
                throw new Exception($"endIndex ({endIndex}) out of bounds");
            
            int span = endIndex - startIndex;
            if (span < 0)
                throw new Exception($"startIndex ({startIndex}) is greater than endIndex ({endIndex})");
            var newWavelengths = data.WavelengthsNm.GetRange(startIndex, span);
            var newValues = data.Values.GetRange(startIndex, span);
            return new Spectrum(newWavelengths, newValues);
        }

        public string GetDescription() => "Window";

        public SpectrumProcessorFactory.Types Type => SpectrumProcessorFactory.Types.WINDOW;
    }
}
