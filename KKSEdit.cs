using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new.Forms
{
    public partial class KKSEdit : Form
    {
        public int Configured = 0;

        public KKSEdit()
        {
            InitializeComponent();
            InitSettings();
            KKS1Label.Text = Resources.ColumnKKSPlant;
            KKS2Label.Text = Resources.ColumnKKSLocation;
            KKS3Label.Text = Resources.ColumnKKSDevice;
            KKS4Label.Text = Resources.ColumnKKSFunction;
        }

        private void InitSettings()
        {
            KKSPartCheck1.Checked = SettingsData.Default.KKSEditCheckPart1;
            KKSPartCheck2.Checked = SettingsData.Default.KKSEditCheckPart2;
            KKSPartCheck3.Checked = SettingsData.Default.KKSEditCheckPart3;
            KKSPartCheck4.Checked = SettingsData.Default.KKSEditCheckPart4;

            KKSBox01.Text = SettingsData.Default.KKSEditText01;
            KKSBox12.Text = SettingsData.Default.KKSEditText12;
            KKSBox23.Text = SettingsData.Default.KKSEditText23;
            KKSBox34.Text = SettingsData.Default.KKSEditText34;
            KKSBox45.Text = SettingsData.Default.KKSEditText45;
        }

        private void UpdateSettings()
        {
            SettingsData.Default.KKSEditCheckPart1 = KKSPartCheck1.Checked;
            SettingsData.Default.KKSEditCheckPart2 = KKSPartCheck2.Checked;
            SettingsData.Default.KKSEditCheckPart3 = KKSPartCheck3.Checked;
            SettingsData.Default.KKSEditCheckPart4 = KKSPartCheck4.Checked;

            SettingsData.Default.KKSEditText01 = KKSBox01.Text;
            SettingsData.Default.KKSEditText12 = KKSBox12.Text;
            SettingsData.Default.KKSEditText23 = KKSBox23.Text;
            SettingsData.Default.KKSEditText34 = KKSBox34.Text;
            SettingsData.Default.KKSEditText45 = KKSBox45.Text;

            SettingsData.Default.Save();
        }
        /// <summary>
        /// Update input data and combine based on form data
        /// </summary>
        /// <param name="_KKS">full kks before modification</param>
        /// <param name="_KKSPlant">kks part1</param>
        /// <param name="_KKSLocation">kks part 2</param>
        /// <param name="_KKSDevice">kks part 3</param>
        /// <param name="_KKSFunction">kks part 4</param>
        public void UpdateKKS(string _KKS, string _KKSPlant, string _KKSLocation, string _KKSDevice, string _KKSFunction)
        {
            KKS = _KKS;
            KKSPlant = _KKSPlant;
            KKSLocation = _KKSLocation;
            KKSDevice = _KKSDevice;
            KKSFunction = _KKSFunction;

            KKSIn.Text = KKS;
            KKSPart1.Text = KKSPlant;
            KKSPart2.Text = KKSLocation;
            KKSPart3.Text = KKSDevice;
            KKSPart4.Text = KKSFunction;

            Combine();
        }

        /// <summary>
        /// Kombines KKS all 4 parts (selectable) and add aditional text
        /// </summary>
        /// <returns></returns>
        private string Combine()
        {
            string _returnValue = string.Empty;

            if (!KKSPartCheck1.Checked)
                KKSPlant = string.Empty;
            if (!KKSPartCheck2.Checked)
                KKSLocation = string.Empty;
            if (!KKSPartCheck3.Checked)
                KKSDevice = string.Empty;
            if (!KKSPartCheck4.Checked)
                KKSFunction = string.Empty;


            _returnValue += KKSBox01.Text;
            _returnValue += KKSPlant;
            if (KKSPlant.Length != 0 && (KKSLocation.Length != 0 || KKSDevice.Length != 0 || KKSFunction.Length != 0))
                _returnValue += KKSBox12.Text;

            _returnValue += KKSLocation;
            if (KKSLocation.Length != 0 && ( KKSDevice.Length != 0 || KKSFunction.Length != 0))
                _returnValue += KKSBox23.Text;

            _returnValue += KKSDevice;
            if (KKSDevice.Length != 0 && (KKSFunction.Length != 0))
                _returnValue += KKSBox34.Text;

            _returnValue += KKSFunction;
            _returnValue += KKSBox45.Text;

            KKSOut.Text = _returnValue;

            return _returnValue;
        }

        /// <summary>
        /// get combined KKS from form
        /// </summary>
        /// <returns>combined KKS</returns>
        public string GetCombined()
        {
            return KKSOut.Text;
        }

        private void KKSBox12_TextChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSBox23_TextChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSBox34_TextChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSBox45_TextChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSBox01_TextChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSPartCheck1_CheckedChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSPartCheck2_CheckedChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSPartCheck3_CheckedChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSPartCheck4_CheckedChanged(object sender, EventArgs e)
        {
            Combine();
        }

        private void KKSOkNextButton_Click(object sender, EventArgs e)
        {
            Configured = 0;
            UpdateSettings();
            this.Close();
        }

        private void KKSCancelButton_Click(object sender, EventArgs e)
        {
            Configured = -1;
            this.Close();
        }

        private void KKSOKButton_Click(object sender, EventArgs e)
        {
            Configured = 1;
            UpdateSettings();
            this.Close();
        }
    }
}
