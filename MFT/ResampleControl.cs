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
    public partial class ResampleControl : ProcessorControlBase
    {
        public ResampleControl()
        {
            InitializeComponent();
            Messenger.MessageAvailable += OnMessageReceived;
            Quiet = true;
            resample = new Resample();
            UpdateForm();
            Quiet = false;
        }

        internal Resample Resample
        {
            get
            {
                UpdateFromForm();
                return resample;
            }
            set
            {
                resample = value;
                if (resample != null)
                    UpdateForm();
            }
        }
        Resample resample;

        public override void UpdateForm()
        {
            MinWavelengthNumericUpDown.Value = resample.MinWavelength_nm;
            MaxWavelengthNumericUpDown.Value = resample.MaxWavelength_nm;
            IncrementNumericUpDown.Value = resample.Increment_nm;
        }

        public override void UpdateFromForm()
        {
            resample.MinWavelength_nm = (int)MinWavelengthNumericUpDown.Value;
            resample.MaxWavelength_nm = (int)MaxWavelengthNumericUpDown.Value;
            resample.Increment_nm = (int)IncrementNumericUpDown.Value;
        }

        public override IProcessor GetProcessor()
        {
            return resample;
        }

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.RESAMPLE_UPDATED:
                    if (sender == this)
                        return;
                    ResampleUpdated(msg.Object as Resample);
                    break;
            }
        }

        void ResampleUpdated(Resample x)
        {
            if (x == Resample)
                UpdateForm();
        }

        private void MinWavelengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.RESAMPLE_UPDATED, Resample);
        }

        private void MaxWavelengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.RESAMPLE_UPDATED, Resample);
        }

        private void IncrementNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.RESAMPLE_UPDATED, Resample);
        }
    }
}
