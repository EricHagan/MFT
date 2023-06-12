using System;
using System.Windows.Forms;

namespace MFT
{
    internal class SpectrumProcessorGuiFactory
    {
        public static SpectrumProcessorControlBase GetSpectrumProcessorControl(SpectrumProcessorFactory.Types type)
        {
            switch (type)
            {
                case SpectrumProcessorFactory.Types.MOVING_AVERAGE:
                    return new MovingAverageControl();
                case SpectrumProcessorFactory.Types.RESAMPLE:
                    return new ResampleControl();
                case SpectrumProcessorFactory.Types.WINDOW:
                    return new SpectrumWindowControl();
                default:
                    throw new Exception($"Unknown type: '{type}'");
            }
        }
    }
}
