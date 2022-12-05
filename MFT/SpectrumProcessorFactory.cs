using System;

namespace MFT
{
    internal static class SpectrumProcessorFactory
    {
        public enum Types { MOVING_AVERAGE, RESAMPLE, WINDOW }
        public static ISpectrumProcessor GetSpectrumProcessor(Types type)
        {
            switch (type)
            {
                case Types.MOVING_AVERAGE:
                    return new MovingAverage();
                case Types.RESAMPLE:
                    return new Resample();
                case Types.WINDOW:
                    return new SpectrumWindow();
                default:
                    throw new Exception($"Unknown type: '{type}'");
            }
        }
        public static string GetName(Types type)
        {
            switch (type)
            {
                case Types.MOVING_AVERAGE:
                    return "Moving Average";
                case Types.RESAMPLE:
                    return "Resample";
                case Types.WINDOW:
                    return "Window";
                default:
                    throw new Exception($"Unknown type: '{type}'");
            }
        }

    }
}
