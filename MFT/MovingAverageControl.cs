﻿using System;
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
    public partial class MovingAverageControl : UserControl
    {
        public MovingAverageControl()
        {
            InitializeComponent();
            Messenger.MessageAvailable += OnMessageReceived;
        }

        bool Quiet { get; set; } = false;

        internal MovingAverage MovingAverage 
        {
            get
            {
                UpdateFromForm();
                return movingAverage;
            }
            set
            {
                UpdateForm();
                movingAverage = value;
            }
        }
        MovingAverage movingAverage;

        void UpdateForm()
        {
            PointsNumericUpDown.Value = movingAverage.WindowPoints;
            iterationsNumericUpDown.Value = movingAverage.Iterations;
        }

        void UpdateFromForm()
        {
            movingAverage.WindowPoints = (int)PointsNumericUpDown.Value;
            movingAverage.Iterations = (int)iterationsNumericUpDown.Value;

        }

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.MOVING_AVERAGE_UPDATED:
                    if (sender == this)
                        return;
                    MovingAverageUpdated(msg.Object as MovingAverage);
                    break;
            }
        }

        void MovingAverageUpdated(MovingAverage m)
        {
            if (m == MovingAverage)
                UpdateForm();
        }

        private void PointsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!Quiet)
                Messenger.SendMessage(this, Message.Types.MOVING_AVERAGE_UPDATED, MovingAverage);
        }
    }
}
