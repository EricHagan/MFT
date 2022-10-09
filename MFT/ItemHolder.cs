using System.Windows.Forms;

namespace MFT
{
    public class ItemHolder
    {
        public ItemHolder(ItemTypes type, TabPage page, object _object)
        {
            Type = type;
            Page = page;
            Object = _object;
        }

        public enum ItemTypes { CAMERA, SPECTROMETER, EXPOSURE, EXPOSURE_SETTINGS }

        public ItemTypes Type { get; set; }
        public TabPage Page { get; set; }
        public object Object { get; set; }
    }
}
