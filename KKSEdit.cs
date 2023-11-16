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
        /// <param name="_KKS">full kks before modification</param>
        /// <param name="_KKSPlant">kks part1</param>
        /// <param name="_KKSLocation">kks part 2</param>
        /// <param name="_KKSDevice">kks part 3</param>
        /// <param name="_KKSFunction">kks part 4</param>
        public void UpdateKKS(string _KKS, string _KKSPlant, string _KKSLocation, string _KKSDevice, string _KKSFunction)
        {
            KKSIn.Text = _KKS;
            KKSPart1.Text = _KKSPlant;
            KKSPart2.Text = _KKSLocation;
            KKSPart3.Text = _KKSDevice;
            KKSPart4.Text = _KKSFunction;

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
            string _returnValue = string.Empty;
            string _KKSPlant = KKSPlant;
            string _KKSLocation = KKSLocation;
            string _KKSDevice = KKSDevice;
            string _KKSFunction = KKSFunction;

            if (!KKSPartCheck1.Checked)
                _KKSPlant = string.Empty;
            if (!KKSPartCheck2.Checked)
                _KKSLocation = string.Empty;
            if (!KKSPartCheck3.Checked)
                _KKSDevice = string.Empty;
            if (!KKSPartCheck4.Checked)
                _KKSFunction = string.Empty;

            _returnValue += KKSBox01.Text;
            _returnValue += _KKSPlant;
            if (_KKSPlant.Length != 0 && (_KKSLocation.Length != 0 || _KKSDevice.Length != 0 || _KKSFunction.Length != 0))
                _returnValue += KKSBox12.Text;

            _returnValue += _KKSLocation;
            if (_KKSLocation.Length != 0 && (_KKSDevice.Length != 0 || _KKSFunction.Length != 0))
                _returnValue += KKSBox23.Text;

            _returnValue += _KKSDevice;
            if (_KKSDevice.Length != 0 && (_KKSFunction.Length != 0))
                _returnValue += KKSBox34.Text;

            _returnValue += _KKSFunction;
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