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
            ERROR,
            EXPOSURE_SETTINGS_CREATE,
            EXPOSURE_SETTINGS_CREATED,
            EXPOSURE_SETTINGS_UPDATED,
            EXPOSURE_SETTINGS_SET_DEFAULT,
            EXPOSURE_SETTINGS_DEFAULT_SET,
            SPECTROMETER_CONNECT,
            SPECTROMETER_UPDATED,
        }
    }
}
