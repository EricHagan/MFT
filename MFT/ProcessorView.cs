namespace MFT
{
    internal class ProcessorView
    {
        public ProcessorFactory.Types Type { get; set; }

        public string Name => ProcessorFactory.GetName(Type);

        public override string ToString() => Name;
    }
}
