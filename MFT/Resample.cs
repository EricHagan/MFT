using System;
using System.Collections.Generic;
using System.Linq;
using Colorimetry;

namespace MFT
{
    internal class Resample : ISpectrumProcessor
    {
        public int MinWavelength_nm { get; set; } = 400;
        public int MaxWavelength_nm { get; set; } = 750;
        public int Increment_nm { get; set; } = 5;

        public Spectrum Process(Spectrum data)
        {
            (int[] newWavelengths, double[] newSpectrum) = 
                Functions.ResampleSpectrum(data.Values.ToArray(),
                data.WavelengthsNm.ToArray(), MinWavelength_nm, MaxWavelength_nm, Increment_nm);
            return new Spectrum(new List<double>(newWavelengths.Select(x => (double)x)), new List<double>(newSpectrum));
        }

        public string GetDescription() => "Resample";

        public SpectrumProcessorFactory.Types Type => SpectrumProcessorFactory.Types.RESAMPLE;

    }
}
