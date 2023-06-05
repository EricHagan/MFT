using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFT
{
    public partial class SpectrumProcessorControlBase : UserControl, ISpectrumProcessorControl
    {
        public SpectrumProcessorControlBase()
        {
            InitializeComponent();
        }

        public bool Quiet { get; set; } = false;
        public virtual void UpdateForm() { }
        public virtual void UpdateFromForm() { }
        public virtual ISpectrumProcessor GetProcessor() { return null; }
    }
}
