using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFT
{
    internal interface ISecondaryControlsHolder
    {
        List<Control> SecondaryControls { get; }
    }
}
