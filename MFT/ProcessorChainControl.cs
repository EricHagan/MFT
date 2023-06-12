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
    public partial class ProcessorChainControl : UserControl
    {
        public ProcessorChainControl()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            availableProcessorsListBox.Items.Clear();
            foreach (int i in Enum.GetValues(typeof(ProcessorFactory.Types)))
            {
                var view = new ProcessorView();
                view.Type = (ProcessorFactory.Types)i;
                availableProcessorsListBox.Items.Add(view);
            }
            Chain = new ProcessorChain();
        }

        public bool Quiet { get; set; } = false;

        internal ProcessorChain Chain
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
        ProcessorChain chain;

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
            var processorView = (ProcessorView)selected;
            var control = ProcessorGuiFactory.GetSpectrumProcessorControl(processorView.Type);
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
            chainFlowLayoutPanel.Controls.Clear();
            foreach (var p in chain)
            {
                var control = ProcessorGuiFactory.GetSpectrumProcessorControl(p.Type);
                control.BorderStyle = BorderStyle.FixedSingle;
                chainFlowLayoutPanel.Controls.Add(control);
            }
        }

        void UpdateFromForm()
        {
            chain.Name = nameTextBox.Text;
            chain.Clear();
            foreach (var p in chainFlowLayoutPanel.Controls)
            {
                var processorGui = (IProcessorControl)p;
                processorGui.UpdateFromForm();
                chain.Add(processorGui.GetProcessor());
            }
        }

        
    }
}
