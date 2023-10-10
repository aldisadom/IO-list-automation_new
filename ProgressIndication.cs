using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class ProgressIndication
    {
        private ProgressBar Bar { get; set; }

        private Label BarLabel { get; set; }

        private int Max = 100;

        private float multiplier = 0;

        public ProgressIndication(ProgressBar bar, Label barLabel)
        {
            Bar = bar;
            BarLabel = barLabel;
            Bar.Maximum = 100;
            Bar.Minimum = 0;
        }

        private void ShowProgressBar(int max)
        {
            BarLabel.Visible = true;
            Bar.Visible = true;
            Bar.Value = 0;

            if (max > 0)
            {
                Max = max;
                multiplier = 100.0f / (float)max;
            }
            else
            {
                Max = 100;
                multiplier = 1.0f;
            }
        }

        /// <summary>
        /// Hide progress bar
        /// </summary>
        public void HideProgressBar()
        {
            BarLabel.Visible = false;
            Bar.Visible = false;
            Bar.Value = 0;
        }

        /// <summary>
        /// Update progress bar
        /// </summary>
        /// <param name="value">new progress bar value in percentage 0-100/param>
        public void UpdateProgressBar(int value)
        {
            if ((value >= 0) && (value <= Max))
            {
                int _scaledValue = (int)((float)value * multiplier);
                Bar.Value = _scaledValue;
            }
            else
            {
                Debug _debug = new Debug();
                _debug.ToFile(Resources.ProgressBarOutRange + value.ToString() + " of " + Bar.Maximum, DebugLevels.None, DebugMessageType.Info);
            }
        }

        /// <summary>
        /// Rename progress bar label
        /// </summary>
        /// <param name="name">new progres bar label</param>
        public void RenameProgressBar(string name, int max)
        {
            Debug _debug = new Debug();
            _debug.ToFile("Progress bar renamed to " + name, DebugLevels.Development, DebugMessageType.Info);
            BarLabel.Text = name;
            ShowProgressBar(max);
        }
    }
}
