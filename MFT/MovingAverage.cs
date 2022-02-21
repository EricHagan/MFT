using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorimetry;

namespace MFT
{
    internal class MovingAverage : ISpectrumProcessor
    {
        public int WindowPoints { get; set; } = 1;
        public int Iterations { get; set; } = 1;

        public Spectrum Process(Spectrum data)
        {
            var values = new List<double>(Functions.SmoothSpectrum(data.Values.ToArray(), WindowPoints, Iterations));
            return new Spectrum(data.WavelengthsNm, values);
        }
    }
}
