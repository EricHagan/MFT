namespace Colorimetry
{
    public struct LabPoint
    {
        public LabPoint(double _L, double _a, double _b)
        {
            L = _L;
            a = _a;
            b = _b;
        }

        public double L { get; private set; }
        public double a { get; private set; }
        public double b { get; private set; }

        public double[] ToArray()
        {
            return new double[3] { L, a, b };
        }
    }
}
