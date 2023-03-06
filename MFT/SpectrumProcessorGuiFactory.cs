using System;
using System.Windows.Forms;

namespace MFT
{
    internal class SpectrumProcessorGuiFactory
    {
        public static UserControl GetSpectrumProcessorControl(SpectrumProcessorFactory.Types type)
        {
            switch (type)
            {
                case SpectrumProcessorFactory.Types.MOVING_AVERAGE:
                    return new MovingAverageControl();
                case SpectrumProcessorFactory.Types.RESAMPLE:
                    return null;// new Resample();
                case SpectrumProcessorFactory.Types.WINDOW:
                    return null;// new SpectrumWindow();
                default:
                    throw new Exception($"Unknown type: '{type}'");
            }
        }
    }
}
