using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public enum ItemTypes { CAMERA, SPECTROMETER, EXPOSURE }

        public ItemTypes Type { get; set; }
        public TabPage Page { get; set; }
        public object Object { get; set; }
    }
}
