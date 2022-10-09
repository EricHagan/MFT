﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace MFT
{
    public class ExposureStream
    {
        public ExposureStream(ISpectrometer s)
        {
            Spectrometer = s;
        }

        public ISpectrometer Spectrometer { get; set; }
        public float IntegrationTimeS { get; set; } = 0.05f;
        public int Averaging { get; set; } = 10;
        public int DwellTimeMs { get; set; } = 150;
        public bool Normalized { get; set; } = false;
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
                    var exposure = Exposure.GetExposure(Spectrometer, IntegrationTimeS, Averaging, Normalized, out errMsg);
                    if (exposure == null)
                        continue;
                    if (ExposureAvailable != null)
                        ExposureAvailable(this, new ExposureEventArgs(exposure));
                    Thread.Sleep(DwellTimeMs);
                }
            }    );
        }

        public void ControlsAdjustedEventHandler(object sender, SpectrometerControlsChangedEventArgs e)
        {
            Averaging = e.Averaging;
            DwellTimeMs = e.DwellTimeMs;
            IntegrationTimeS = e.IntegrationTimeS;
            Normalized = e.Normalize;
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


