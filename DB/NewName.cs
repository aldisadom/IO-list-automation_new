using IO_list_automation_new.Properties;
using System;
using System.Windows.Forms;

namespace IO_list_automation_new.DB
{
    public partial class NewName : Form
    {
        public string Output
        {
            get
            {
                if (string.IsNullOrEmpty(InputDBObjectSignalName.Text))
                    return InputDBObjectName.Text;
                else
                    return InputDBObjectName.Text + "(" + InputDBObjectSignalName.Text + ")";
            }
        }

        public NewName(string name, string predefinedName, bool withSignalName)
        {
            InitializeComponent();
            this.Name = name;

            DBObjectSignalName.Text = ResourcesUI.DBObjectSignalName;
            DBObjectName.Text = ResourcesUI.DBObjectName;

            DBObjectSignalName.Visible = withSignalName;
            InputDBObjectSignalName.Visible = withSignalName;

            InputDBObjectName.Text = predefinedName;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}