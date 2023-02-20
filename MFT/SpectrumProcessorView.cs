namespace MFT
{
    internal class SpectrumProcessorView
    {
        public SpectrumProcessorFactory.Types Type { get; set; }

        public string Name => SpectrumProcessorFactory.GetName(Type);

        public override string ToString() => Name;
    }
}
