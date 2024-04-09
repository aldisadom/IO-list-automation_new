using IO_list_automation_new.Properties;
using System;
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

        public void Update(string ioText)
        {
            IOTextIn.Text = ioText;
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
            string ioText = IOTextIn.Text;

            int objectspecificsIndex = ioText.IndexOf(ObjectSpecificsSeparator.Text);
            int functionTextIndex = ioText.IndexOf(FunctionTextSeparator.Text, objectspecificsIndex);

            // found object Specifics, found function Text
            if ((objectspecificsIndex != -1) && (functionTextIndex != -1))
            {
                ObjectName.Text = ioText.Substring(0, objectspecificsIndex).TrimEnd(' ');
                ObjectSpecifics.Text = ioText.Substring(objectspecificsIndex, functionTextIndex).TrimEnd(' ').TrimStart(' ');
                FunctionText.Text = ioText.Substring(functionTextIndex).TrimStart(' ');
            }
            // no object Specifics, found function Text
            else if (functionTextIndex != -1)
            {
                ObjectName.Text = ioText.Substring(0, objectspecificsIndex).TrimEnd(' ');
                ObjectSpecifics.Text = ioText.Substring(objectspecificsIndex).TrimEnd(' ').TrimStart(' ');
                FunctionText.Text = "";
            }
            // found object Specifics, no function Text
            else if (objectspecificsIndex != -1)
            {
                ObjectName.Text = ioText.Substring(0, functionTextIndex).TrimEnd(' ');
                ObjectSpecifics.Text = "";
                FunctionText.Text = ioText.Substring(functionTextIndex).TrimStart(' ');
            }
            // no object Specifics, no function Text
            else
            {
                ObjectName.Text = ioText;
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