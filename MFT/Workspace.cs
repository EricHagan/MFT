using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal class Workspace
    {
        public Workspace()
        {
            Clear();
        }

        public ISpectrometer Spectrometer { get; set; }
        public Dictionary<int, SpectrumProcessorChain> SpectrumProcessorChains { get; set; }
        public int CurrentSpectrumProcessorChain { get; set; }
        public Dictionary<int, TestCollection> TestCollections { get; set; }
        public Dictionary<int, ExposureSettings> ExposureSettings { get; set; }
        public ExposureSettings DefaultExposureSettings { get; set; }
        public int CurrentExposureSettings { get; set; }

        public void Clear()
        {
            Spectrometer = null;
            SpectrumProcessorChains = new Dictionary<int, SpectrumProcessorChain>();
            CurrentSpectrumProcessorChain = -1;
            TestCollections = new Dictionary<int, TestCollection>();
            ExposureSettings = new Dictionary<int, ExposureSettings>();
            DefaultExposureSettings = null;
            CurrentExposureSettings = -1;
        }
    }
}
