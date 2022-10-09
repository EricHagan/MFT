namespace MFT
{
    public class ExposureSettings : WorkspaceItem
    {
        public ExposureSettings(int averaging, int integrationTimeMs,
            int dwellTimeMs, bool normalized, string name = "", long handle = 0)
            : base(name, handle)
        {
            Averaging = averaging;
            IntegrationTimeMs = integrationTimeMs;
            DwellTimeMs = dwellTimeMs;
            Normalized = normalized;
        }

        public ExposureSettings()
            : this(10, 50, 100, false, "") { }

        public int Averaging { get; private set; }
        public int IntegrationTimeMs { get; private set; }
        public int DwellTimeMs { get; private set; }
        public bool Normalized { get; private set; }

        public ExposureSettings Unnormalized()
        {
            return new ExposureSettings(
                Averaging, IntegrationTimeMs, DwellTimeMs, false, Name, Handle);
        }
    }
}
