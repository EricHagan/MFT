using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal interface ISpectrumProcessor
    {
        List<double> Process(List<double> data);
        List<double> Wavelengths { get; set; } 
    }
}
