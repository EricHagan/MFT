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

        public HashSet<ICamera> Cameras { get; set; }
        public ISpectrometer Spectrometer { get; set; }
        public Dictionary<long, SpectrumProcessorChain> SpectrumProcessorChains { get; set; }
        public int CurrentSpectrumProcessorChain { get; set; }
        public Dictionary<long, TestCollection> TestCollections { get; set; }
        public List<ExposureSettings> ExposureSettings { get; set; }
        public ExposureSettings DefaultExposureSettings { get; set; }

        public void Clear()
        {
            Cameras = new HashSet<ICamera>();
            Spectrometer = null;
            SpectrumProcessorChains = new Dictionary<long, SpectrumProcessorChain>();
            CurrentSpectrumProcessorChain = 0;
            TestCollections = new Dictionary<long, TestCollection>();
            ExposureSettings = new List<ExposureSettings>();
            DefaultExposureSettings = new ExposureSettings();
        }
    }
}
