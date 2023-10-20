using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new.DB
{
    public partial class NewName : Form
    {
        public string Output { get { return InputData.Text; } }
        public NewName(string type)
        {
            InitializeComponent();
            this.Name = type;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
