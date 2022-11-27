using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MFT
{
    public partial class MainDialog : Form
    {
        public MainDialog()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Messenger.MessageAvailable += OnMessageReceived;
        }

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.ERROR:
                    OnError(msg.Object as string);
                    break;
            }
        }

        void OnError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new AboutDialog();
            d.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Messenger.SendMessage(this, Message.Types.EXITING, null);
        }
    }
}
