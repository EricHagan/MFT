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


        public List<double> Process(List<double> data)
        {
            (int[] newWavelengths, double[] newSpectrum) = 
                Functions.ResampleSpectrum(data.ToArray(),
                Wavelengths.ToArray(), MinWavelength_nm, MaxWavelength_nm, Increment_nm);
            Wavelengths = new List<double>(newWavelengths.Select(x => (double)x));
            return new List<double>(newSpectrum);
        }

        public List<double> Wavelengths { get; set; }
    }
}
