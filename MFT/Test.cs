using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal class Test
    {
        ISpectrometer Spectrometer { get; set; }
        double IlluminanceMlx { get; set; } 
        double IrradianceWm2 { get; set; }
        double ViewAngleDeg { get; set; }
        string SampleName { get; set; }
        string User { get; set; }
        double SpotDiameter_mm { get; set; }
        ProcessorChain SpectrumProcessorChain { get; set; }
        List<Measurement> Measurements { get; set; }
    }
}
