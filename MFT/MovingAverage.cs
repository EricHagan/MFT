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


        public List<double> Process(List<double> data)
        {
            return new List<double>(Functions.SmoothSpectrum(data.ToArray(), WindowPoints, Iterations));
        }

        public List<double> Wavelengths { get; set; }
    }
}
