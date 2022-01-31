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
            double startWaveLength = rawWaveLengthsNm.First(w => w > MinWavelengthNm);
            int startIndex = rawWaveLengthsNm.IndexOf(startWaveLength) - 1;
            if (startIndex < 0 || startIndex > rawWaveLengthsNm.Count - 1)
                throw new Exception($"startIndex ({startIndex}) out of bounds");
            double endWaveLength = rawWaveLengthsNm.First(w => w > MaxWavelengthNm);
            int endIndex = rawWaveLengthsNm.IndexOf(endWaveLength);
            if (endIndex < 0 || endIndex > rawWaveLengthsNm.Count - 1)
                throw new Exception($"endIndex ({endIndex}) out of bounds");
            return new Tuple<int, int>(startIndex, endIndex);
        }
    }
}
