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
        public List<SpectrumProcessorChain> SpectrumProcessorChains { get; set; }
        public SpectrumProcessorChain DefaultSpectrumProcessorChain { get; set; }
        public int NextSpectrumProcessorChainNameIndex { get; set; }
        public int CurrentSpectrumProcessorChainId { get; set; }
        public Dictionary<long, TestCollection> TestCollections { get; set; }
        public List<ExposureSettings> ExposureSettings { get; set; }
        public ExposureSettings DefaultExposureSettings { get; set; }

        public List<string> SpectrumProcessorChainNames
        {
            get
            {
                if (SpectrumProcessorChains == null)
                    return new List<string>();
                return SpectrumProcessorChains.Select(x => x.Name).ToList();
            }
        }

        public void Clear()
        {
            Cameras = new HashSet<ICamera>();
            Spectrometer = null;
            SpectrumProcessorChains = new List<SpectrumProcessorChain>();
            DefaultSpectrumProcessorChain = new SpectrumProcessorChain();
            NextSpectrumProcessorChainNameIndex = 1;
            CurrentSpectrumProcessorChainId = 0;
            TestCollections = new Dictionary<long, TestCollection>();
            ExposureSettings = new List<ExposureSettings>();
            DefaultExposureSettings = new ExposureSettings();
        }
    }
}
