using System;
using System.Windows.Forms;

namespace IO_list_automation_new.DB
{
    public partial class NewName : Form
    {
        public string Output
        { get { return InputData.Text; } }

        public NewName(string name)
        {
            InitializeComponent();
            this.Name = name;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            InputData.Text = string.Empty;
            this.Close();
        }
    }
}