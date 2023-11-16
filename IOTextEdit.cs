using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    public partial class IOTextEdit : Form
    {
        public int Configured = 0;

        public IOTextEdit()
        {
            InitializeComponent();
            InitSettings();
            label.Text = ResourcesColumns.IOText;
            SeparatorLabel.Text = Resources.Separator;
        }

        /// <summary>
        /// put settings to form elements
        /// </summary>
        private void InitSettings()
        {
            ObjectSpecificsSeparator.Text = SettingsData.Default.ObjectSpecificsSeparator;
            FunctionTextSeparator.Text = SettingsData.Default.FunctionTextSeparator;
        }

        /// <summary>
        /// update settings from form elements
        /// </summary>
        private void UpdateSettings()
        {
            SettingsData.Default.ObjectSpecificsSeparator = ObjectSpecificsSeparator.Text;
            SettingsData.Default.FunctionTextSeparator = FunctionTextSeparator.Text;

            SettingsData.Default.Save();
        }

        public void Update(string _IOText)
        {
            IOTextIn.Text = _IOText;
            Parse();
        }

        /// <summary>
        /// Elements data changed, event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parse_Event(object sender, EventArgs e)
        {
            Parse();
        }

        /// <summary>
        /// Parse Object name, object specifics and signal function
        /// </summary>
        private void Parse()
        {
            string _IOText = IOTextIn.Text;

            int _objectSpecificsIndex = _IOText.IndexOf(ObjectSpecificsSeparator.Text);
            int _functionTextIndex= _IOText.IndexOf(FunctionTextSeparator.Text,_objectSpecificsIndex);

            // found object Specifics, found function Text
            if ((_objectSpecificsIndex != -1) && (_functionTextIndex != -1))
            {
                ObjectName.Text = _IOText.Substring(0, _objectSpecificsIndex).TrimEnd(' ');
                ObjectSpecifics.Text = _IOText.Substring(_objectSpecificsIndex, _functionTextIndex).TrimEnd(' ').TrimStart(' ');
                FunctionText.Text = _IOText.Substring(_functionTextIndex).TrimStart(' ');
            }
            // no object Specifics, found function Text
            else if (_functionTextIndex != -1)
            {
                ObjectName.Text = _IOText.Substring(0, _objectSpecificsIndex).TrimEnd(' ');
                ObjectSpecifics.Text = _IOText.Substring(_objectSpecificsIndex).TrimEnd(' ').TrimStart(' ');
                FunctionText.Text = "";
            }
            // found object Specifics, no function Text
            else if (_objectSpecificsIndex != -1)
            {
                ObjectName.Text = _IOText.Substring(0, _functionTextIndex).TrimEnd(' ');
                ObjectSpecifics.Text = "";
                FunctionText.Text = _IOText.Substring(_functionTextIndex).TrimStart(' ');
            }
            // no object Specifics, no function Text
            else
            {
                ObjectName.Text = _IOText;
                ObjectSpecifics.Text = "";
                FunctionText.Text = "";
            }
        }

        private void IOEditOkNextButton_Click(object sender, EventArgs e)
        {
            Configured = 0;
            UpdateSettings();
            this.Close();
        }

        private void IOEditOKButton_Click(object sender, EventArgs e)
        {
            Configured = 1;
            UpdateSettings();
            this.Close();
        }

        private void IOEditCancelButton_Click(object sender, EventArgs e)
        {
            Configured = -1;
            this.Close();
        }
    }
}
