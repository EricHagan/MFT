using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    public class Workspace
    {
        ISpectrometer spectrometer { get; set; }
        Dictionary<int, SpectrumProcessorChain> SpectrumProcessorChains { get; set; }
        // todo: Dictionary<int, Test>
    }
}
