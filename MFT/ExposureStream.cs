using System;
using System.Threading;
using System.Threading.Tasks;

namespace MFT
{
    public class ExposureStream
    {
        public ExposureStream(ISpectrometer s)
        {
            Spectrometer = s;
            Settings = new ExposureSettings();
        }

        public ISpectrometer Spectrometer { get; set; }
        public ExposureSettings Settings { get; set; }
        public event EventHandler<ExposureEventArgs> ExposureAvailable;

        bool PleaseStop;
        Task ExposureGetter;
        public void Start()
        {
            if (ExposureGetter != null && !ExposureGetter.IsCompleted)
            {
                Stop();
            }
            PleaseStop = false;
            ExposureGetter = Task.Run(() =>
            {
                while (!PleaseStop)
                {
                    string errMsg;
                    var exposure = Exposure.GetExposure(Spectrometer, Settings, out errMsg);
                    if (exposure == null)
                        continue;
                    if (ExposureAvailable != null)
                        ExposureAvailable(this, new ExposureEventArgs(exposure));
                    Thread.Sleep(Settings.DwellTimeMs);
                }
            }    );
        }

        public void ControlsAdjustedEventHandler(object sender, SpectrometerControlsChangedEventArgs e)
        {
            Settings = e.Settings;
        }

        public void Stop()
        {
            PleaseStop = true;
            ExposureGetter.Wait();
        }
    }

    public class ExposureEventArgs : EventArgs
    {
        public ExposureEventArgs(Exposure x)
        {
            Exposure = x;
        }
        public Exposure Exposure { get; set; }
    }
}


