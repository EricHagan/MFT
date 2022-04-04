using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal class Workspace
    {
        public ISpectrometer Spectrometer { get; set; }
        public Dictionary<int, SpectrumProcessorChain> SpectrumProcessorChains { get; set; } = new Dictionary<int, SpectrumProcessorChain>();
        public int CurrentSpectrumProcessorChain { get; set; } = -1;
        public Dictionary<int, TestCollection> TestCollections { get; set; } = new Dictionary<int, TestCollection>();
        public Dictionary<int, ExposureSettings> ExposureSettings { get; set; } = new Dictionary<int, ExposureSettings>();
        public ExposureSettings DefaultExposureSettings { get; set; } = null;
        public int CurrentExposureSettings { get; set; } = -1;
    }
}
