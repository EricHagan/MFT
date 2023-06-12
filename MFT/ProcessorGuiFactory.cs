using System;
using System.Windows.Forms;

namespace MFT
{
    internal class ProcessorGuiFactory
    {
        public static ProcessorControlBase GetSpectrumProcessorControl(ProcessorFactory.Types type)
        {
            switch (type)
            {
                case ProcessorFactory.Types.MOVING_AVERAGE:
                    return new MovingAverageControl();
                case ProcessorFactory.Types.RESAMPLE:
                    return new ResampleControl();
                case ProcessorFactory.Types.WINDOW:
                    return new WindowControl();
                default:
                    throw new Exception($"Unknown type: '{type}'");
            }
        }
    }
}
