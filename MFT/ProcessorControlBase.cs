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
    public partial class ProcessorControlBase : UserControl, IProcessorControl
    {
        public ProcessorControlBase()
        {
            InitializeComponent();
        }

        public bool Quiet { get; set; } = false;
        public virtual void UpdateForm() { }
        public virtual void UpdateFromForm() { }
        public virtual IProcessor GetProcessor() { return null; }
    }
}
