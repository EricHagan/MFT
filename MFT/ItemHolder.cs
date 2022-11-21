using System.Windows.Forms;

namespace MFT
{
    public class ItemHolder
    {
        public ItemHolder(ItemTypes type, object _object)
        {
            Type = type;
            Object = _object;
        }

        public enum ItemTypes { CAMERA, SPECTROMETER, EXPOSURE, EXPOSURE_SETTINGS }

        public ItemTypes Type { get; set; }
        public object Object { get; set; }
    }
}
