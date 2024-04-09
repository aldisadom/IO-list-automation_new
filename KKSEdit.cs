using IO_list_automation_new.Properties;
using System;
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
            label.Text = ResourcesColumns.KKS;
            KKS1Label.Text = ResourcesColumns.KKSPlant;
            KKS2Label.Text = ResourcesColumns.KKSLocation;
            KKS3Label.Text = ResourcesColumns.KKSDevice;
            KKS4Label.Text = ResourcesColumns.KKSFunction;
        }

        /// <summary>
        /// put settings to form elements
        /// </summary>
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

        /// <summary>
        /// update settings from form elements
        /// </summary>
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
        /// <param name="kks">full kks before modification</param>
        /// <param name="kksPlant">kks part1</param>
        /// <param name="kksLocation">kks part 2</param>
        /// <param name="kksDevice">kks part 3</param>
        /// <param name="kksFunction">kks part 4</param>
        public void UpdateKKS(string kks, string kksPlant, string kksLocation, string kksDevice, string kksFunction)
        {
            KKSIn.Text = kks;
            KKSPart1.Text = kksPlant;
            KKSPart2.Text = kksLocation;
            KKSPart3.Text = kksDevice;
            KKSPart4.Text = kksFunction;

            Combine();
        }

        /// <summary>
        /// Elements data changed, event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combine_Event(object sender, EventArgs e)
        {
            Combine();
        }

        /// <summary>
        /// Combines KKS all 4 parts (selectable) and add additional text
        /// </summary>
        private string Combine()
        {
            string returnValue = string.Empty;
            string kksPlant = KKSPlant;
            string kksLocation = KKSLocation;
            string kksDevice = KKSDevice;
            string kksFunction = KKSFunction;

            if (!KKSPartCheck1.Checked)
                kksPlant = string.Empty;
            if (!KKSPartCheck2.Checked)
                kksLocation = string.Empty;
            if (!KKSPartCheck3.Checked)
                kksDevice = string.Empty;
            if (!KKSPartCheck4.Checked)
                kksFunction = string.Empty;

            returnValue += KKSBox01.Text;
            returnValue += kksPlant;
            if (kksPlant.Length != 0 && (kksLocation.Length != 0 || kksDevice.Length != 0 || kksFunction.Length != 0))
                returnValue += KKSBox12.Text;

            returnValue += kksLocation;
            if (kksLocation.Length != 0 && (kksDevice.Length != 0 || kksFunction.Length != 0))
                returnValue += KKSBox23.Text;

            returnValue += kksDevice;
            if (kksDevice.Length != 0 && (kksFunction.Length != 0))
                returnValue += KKSBox34.Text;

            returnValue += kksFunction;
            returnValue += KKSBox45.Text;

            KKSOut.Text = returnValue;

            return returnValue;
        }

        /// <summary>
        /// get combined KKS from form
        /// </summary>
        /// <returns>combined KKS</returns>
        public string GetCombined()
        {
            return KKSOut.Text;
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