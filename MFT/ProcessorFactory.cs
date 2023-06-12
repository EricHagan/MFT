using System;

namespace MFT
{
    public static class ProcessorFactory
    {
        public enum Types { MOVING_AVERAGE, RESAMPLE, WINDOW, CHAIN }
        public static IProcessor GetSpectrumProcessor(Types type)
        {
            switch (type)
            {
                case Types.MOVING_AVERAGE:
                    return new MovingAverage();
                case Types.RESAMPLE:
                    return new Resample();
                case Types.WINDOW:
                    return new Window();
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
        public static IProcessor GetCopyOf(IProcessor x)
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
