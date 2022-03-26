using System;

namespace MFT
{
    internal class SimulatedSpectrometer : Qmini, ISpectrometer
    {
        public override bool Connect(out string ErrMsg)
        {
            try
            {
                ErrMsg = string.Empty;
                spectrometer = new RgbDriverKit.SimulatedSpectrometer();
                spectrometer.Open();
                GetWavelengths();
                return true;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return false;
            }
        }

        public override SpectrometerTypes GetDeviceType()
        {
            return SpectrometerTypes.SIMULATED;
        }
    }
}
