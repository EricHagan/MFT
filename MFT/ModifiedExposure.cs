using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal class ModifiedExposure : Exposure
    {
        public SpectrumProcessorChain SpectrumProcessor { get; set; }

        public Spectrum InputSpectrum { get; set; }


    }
}
