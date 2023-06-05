using System;

namespace MFT
{
    public class Message : EventArgs
    {
        public Message(Types type, Object _object)
        {
            Type = type;
            Object = _object;
        }

        public Types Type { get; private set; }
        public Object Object { get; private set; }

        public enum Types
        {
            CAMERA_CONNECT,
            CAMERA_UPDATED,
            CAMERA_ACTIVATED,
            ERROR,
            EXPOSURE_ACTIVATED,
            EXPOSURE_SETTINGS_CREATE,
            EXPOSURE_SETTINGS_CREATED,
            EXPOSURE_SETTINGS_UPDATED,
            EXPOSURE_SETTINGS_APPLY,
            EXPOSURE_SETTINGS_SET_DEFAULT,
            EXPOSURE_SETTINGS_DEFAULT_SET,
            EXPOSURE_SETTINGS_ACTIVATED,
            EXPOSURE_SETTINGS_DELETE,
            EXPOSURE_SETTINGS_DELETED,
            MOVING_AVERAGE_UPDATED,
            RESAMPLE_UPDATED,
            SPECTROMETER_CONNECT,
            SPECTROMETER_CONNECTED,
            SPECTROMETER_UPDATED,
            SPECTROMETER_ACTIVATED,
            SPECTRUM_PROCESSOR_CHAIN_CREATE,
            SPECTRUM_PROCESSOR_CHAIN_CREATED,
            SPECTRUM_PROCESSOR_CHAIN_UPDATED,
            SPECTRUM_PROCESSOR_CHAIN_ACTIVATED,
            EXITING,
        }
    }
}
