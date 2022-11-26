namespace MFT
{
    public class ExposureSettings
    {
        public ExposureSettings(int averaging, int integrationTimeMs,
            int dwellTimeMs, bool normalized, string name = "")
        {
            Averaging = averaging;
            IntegrationTimeMs = integrationTimeMs;
            DwellTimeMs = dwellTimeMs;
            Normalized = normalized;
        }

        public ExposureSettings()
            : this(10, 50, 100, false, "") { }

        public ExposureSettings(ExposureSettings source)
        {
            Averaging = source.Averaging;
            IntegrationTimeMs = source.IntegrationTimeMs;
            DwellTimeMs = source.DwellTimeMs;
            Normalized = source.Normalized;
            Name = source.Name;
        }

        public int Averaging { get; set; }
        public int IntegrationTimeMs { get; set; }
        public int DwellTimeMs { get; set; }
        public bool Normalized { get; set; }
        public string Name { get; set; }

        public ExposureSettings Unnormalized()
        {
            return new ExposureSettings(
                Averaging, IntegrationTimeMs, DwellTimeMs, false, Name);
        }

        public override string ToString()
        {
            string output = "";
            string description = $"Avg: {Averaging}, Int: {IntegrationTimeMs} ms, Dwell: {DwellTimeMs} ms";
            if (Normalized)
                description = description + " (Normalized)";
            if (!string.IsNullOrWhiteSpace(Name))
                output = $"{Name}: {description}";
            else
                output = description;
            return output;
        }
    }
}
