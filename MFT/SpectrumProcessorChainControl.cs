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
    public partial class SpectrumProcessorChainControl : UserControl
    {
        public SpectrumProcessorChainControl()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            availableProcessorsListBox.Items.Clear();
            foreach (int i in Enum.GetValues(typeof(SpectrumProcessorFactory.Types)))
            {
                var view = new SpectrumProcessorView();
                view.Type = (SpectrumProcessorFactory.Types)i;
                availableProcessorsListBox.Items.Add(view);
            }
        }

        internal SpectrumProcessorChain Chain { get; set; }

    }
}
