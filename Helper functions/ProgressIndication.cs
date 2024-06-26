﻿using IO_list_automation_new.Properties;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class ProgressIndication
    {
        private ProgressBar Bar { get; }

        private Label BarLabel { get; }

        private int PreviousValue;

        private int Deadband;

        private int SuppressLevel
        { get { return (int)Bar.Tag; } set { Bar.Tag = value; } }

        public ProgressIndication(ProgressBar bar, Label barLabel)
        {
            Bar = bar;
            BarLabel = barLabel;
            if (Bar.Tag == null)
                SuppressLevel = 0;
        }

        private void ShowProgressBar(int max)
        {
            SuppressLevel++;
            if (SuppressLevel > 1)
                return;

            BarLabel.Visible = true;
            Bar.Visible = true;
            Bar.Value = 0;
            PreviousValue = 0;

            // progress bar update every 5%
            Deadband = max / 20;

            Bar.Maximum = max;
        }

        /// <summary>
        /// Hide progress bar
        /// </summary>
        public void HideProgressBar()
        {
            SuppressLevel--;
            BarLabel.Visible = false;
            Bar.Visible = false;
            Bar.Value = 0;
        }

        /// <summary>
        /// Progress bar update
        /// </summary>
        /// <param name="value">new value of progress bar</param>
        public void UpdateProgressBar(int value)
        {
            if (SuppressLevel > 1)
                return;

            //value in range of bar element
            if ((value < 0) || (value > Bar.Maximum))
            {
                Debug debug = new Debug();
                debug.ToFile(Resources.ProgressBarOutRange + " " + value.ToString() + " Max: " + Bar.Maximum, DebugLevels.None, DebugMessageType.Warning);
                return;
            }

            //value change is greater than deadband
            if (value - PreviousValue < Deadband)
                return;

            PreviousValue = value;
            Bar.Value = value;
            Bar.Update();
        }

        /// <summary>
        /// Rename progress bar label
        /// </summary>
        /// <param name="name">new progress bar label</param>
        public void RenameProgressBar(string name, int max)
        {
            Debug debug = new Debug();
            debug.ToFile("Progress bar renamed to " + name, DebugLevels.Development, DebugMessageType.Info);
            BarLabel.Text = name;
            ShowProgressBar(max);
            Bar.Update();
            BarLabel.Update();
        }
    }
}