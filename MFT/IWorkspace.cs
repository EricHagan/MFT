using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal interface IWorkspace
    {
        ISpectrometer spectrometer { get; set; }
        Dictionary<int, SpectrumProcessorChain> SpectrumProcessorChains { get; set; }
        Dictionary<int, TestCollection> TestCollections { get; set; }
        ExposureSettings CurrentExposureSettings { get; set; }

    }
}
