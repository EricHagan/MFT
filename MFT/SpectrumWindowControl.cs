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
    public partial class SpectrumWindowControl : SpectrumProcessorControlBase
    {
        public SpectrumWindowControl()
        {
            InitializeComponent();
            Messenger.MessageAvailable += OnMessageReceived;
            Quiet = true;
            window = new SpectrumWindow();
            UpdateForm();
            Quiet = false;
        }

        internal SpectrumWindow Window
        {
            get
            {
                UpdateFromForm();
                return window;
            }
            set
            {
                window = value;
                if (window != null)
                    UpdateForm();
            }
        }
        SpectrumWindow window;

        public override void UpdateForm()
        {
            MinWavelengthNumericUpDown.Value = (decimal)window.MinWavelength_nm;
            MaxWavelengthNumericUpDown.Value = (decimal)window.MaxWavelength_nm;
        }

        public override void UpdateFromForm()
        {
            window.MinWavelength_nm = (double)MinWavelengthNumericUpDown.Value;
            window.MaxWavelength_nm = (double)MaxWavelengthNumericUpDown.Value;
        }

        public override ISpectrumProcessor GetProcessor()
        {
            return window;
        }

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.WINDOW_UPDATED:
                    if (sender == this)
                        return;
                    WindowUpdated(msg.Object as SpectrumWindow);
                    break;
            }
        }

        void WindowUpdated(SpectrumWindow x)
        {
            if (x == Window)
                UpdateForm();
        }

        private void MinWavelengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.WINDOW_UPDATED, Window);
        }

        private void MaxWavelengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.WINDOW_UPDATED, Window);
        }
    }
}
