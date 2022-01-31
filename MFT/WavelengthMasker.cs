using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal static class WavelengthMasker
    {
        public static double MinWavelengthNm { get; set; } = 380.0;
        public static double MaxWavelengthNm { get; set; } = 840.0;

        public static Tuple<int, int> GetStartAndEndIndex(List<double> rawWaveLengthsNm)
        {
            double startWaveLength, endWaveLength;
            int startIndex, endIndex;
            try
            {
                startWaveLength = rawWaveLengthsNm.First(w => w > MinWavelengthNm);
                startIndex = rawWaveLengthsNm.IndexOf(startWaveLength) - 1;
            }
            catch (InvalidOperationException)
            {
                startIndex = 0;
            }
            if (startIndex < 0 || startIndex > rawWaveLengthsNm.Count - 1)
                throw new Exception($"startIndex ({startIndex}) out of bounds");
            try
            {
                endWaveLength = rawWaveLengthsNm.First(w => w > MaxWavelengthNm);
                endIndex = rawWaveLengthsNm.IndexOf(endWaveLength);
            }
            catch (InvalidOperationException)
            {
                endIndex = rawWaveLengthsNm.Count - 1;
            }
            if (endIndex < 0 || endIndex > rawWaveLengthsNm.Count - 1)
                throw new Exception($"endIndex ({endIndex}) out of bounds");
            return new Tuple<int, int>(startIndex, endIndex);
        }
    }
}
