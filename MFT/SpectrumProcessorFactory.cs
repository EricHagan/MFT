using System;

namespace MFT
{
    public static class SpectrumProcessorFactory
    {
        public enum Types { MOVING_AVERAGE, RESAMPLE, WINDOW, CHAIN }
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
                case Types.CHAIN:
                    return "Chain";
                default:
                    throw new Exception($"Unknown type: '{type}'");
            }
        }
        public static ISpectrumProcessor GetCopyOf(ISpectrumProcessor x)
        {
            switch (x.Type)
            {
                case Types.MOVING_AVERAGE:
                    return new MovingAverage((MovingAverage)x);
                case Types.RESAMPLE:
                    return null; // todo
                case Types.WINDOW:
                    return null; // todo
                case Types.CHAIN:
                    return null; // todo
                default:
                    throw new Exception($"Unknown type: '{x.Type}'");
            }
        }
    }
}
