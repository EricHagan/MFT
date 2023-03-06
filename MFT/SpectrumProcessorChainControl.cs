using RgbDriverKit;
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
            chain = new SpectrumProcessorChain();
        }

        public bool Quiet { get; set; } = false;

        internal SpectrumProcessorChain Chain
        {
            get
            {
                UpdateFromForm();
                return chain;
            }
            set
            {
                chain = value;
                if (chain != null)
                    UpdateForm();
            }
        }
        SpectrumProcessorChain chain;

        void OnSettingsChanged()
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.SPECTRUM_PROCESSOR_CHAIN_UPDATED, Chain);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var selected = availableProcessorsListBox.SelectedItem;
            if (selected == null)
                return;
            var processorView = (SpectrumProcessorView)selected;
            var control = SpectrumProcessorGuiFactory.GetSpectrumProcessorControl(processorView.Type);
            control.BorderStyle = BorderStyle.FixedSingle;
            chainFlowLayoutPanel.Controls.Add(control);
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            chain.Name = nameTextBox.Text;
            OnSettingsChanged();
        }

        private void chainFlowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            UpdateFromForm();
            OnSettingsChanged();
        }

        void UpdateForm()
        {
            nameTextBox.Text = chain.Name;


        }

        void UpdateFromForm()
        {
            chain.Name = nameTextBox.Text;
            chain.Clear();
            foreach (var p in chainFlowLayoutPanel.Controls)
            {
                var processorGui = (ISpectrumProcessorControl)p;
                processorGui.UpdateFromForm();
                chain.Add(processorGui.GetProcessor());
            }
        }

        
    }
}
