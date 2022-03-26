namespace MFT
{
    internal class Measurement
    {
        public Measurement(double _timeMinutes, Color _color)
        {
            TimeMinutes = _timeMinutes;
            Color = _color;
        }

        public double TimeMinutes { get; private set; }
        public Color Color { get; private set; }     
    }
}
