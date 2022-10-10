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
        public Dictionary<long, SpectrumProcessorChain> SpectrumProcessorChains { get; set; }
        public int CurrentSpectrumProcessorChain { get; set; }
        public Dictionary<long, TestCollection> TestCollections { get; set; }
        public Dictionary<long, ExposureSettings> ExposureSettings { get; set; }
        public ExposureSettings DefaultExposureSettings { get; set; }
        public int CurrentExposureSettings { get; set; }

        public void Clear()
        {
            Spectrometer = null;
            SpectrumProcessorChains = new Dictionary<long, SpectrumProcessorChain>();
            CurrentSpectrumProcessorChain = 0;
            TestCollections = new Dictionary<long, TestCollection>();
            ExposureSettings = new Dictionary<long, ExposureSettings>();
            DefaultExposureSettings = new ExposureSettings();
            CurrentExposureSettings = 0;
        }
    }
}
