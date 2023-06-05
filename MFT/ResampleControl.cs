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
    public partial class ResampleControl : SpectrumProcessorControlBase
    {
        public ResampleControl()
        {
            InitializeComponent();
            Messenger.MessageAvailable += OnMessageReceived;
            resample = new Resample();
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
            MinWavelengthTextBox.Text = Resample.MinWavelength_nm.ToString();
            MaxWavelengthTextBox.Text = Resample.MaxWavelength_nm.ToString();
            IncrementNumericUpDown.Value = Resample.Increment_nm;
        }

        public override void UpdateFromForm()
        {
            if (!int.TryParse(MinWavelengthTextBox.Text, out int minWavelengthNm))
                throw new Exception($"Could not convert '{MinWavelengthTextBox.Text}' to an integer.");
            if (!int.TryParse(MaxWavelengthTextBox.Text, out int maxWavelengthNm))
                throw new Exception($"Could not convert '{MaxWavelengthTextBox.Text}' to an integer.");

            Resample.MinWavelength_nm = minWavelengthNm;
            Resample.MaxWavelength_nm = maxWavelengthNm;
            Resample.Increment_nm = (int)IncrementNumericUpDown.Value;
        }

        public override ISpectrumProcessor GetProcessor()
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

        private void MinWavelengthTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }






}
