using IO_list_automation_new.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new.DB
{
    public partial class DBForceEditForm : Form
    {
        public DBForceEditForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Key Down event for all forms and application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown_Event(object sender, KeyEventArgs e)
        {
            // Delete function
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                this.SuspendLayout();
                DataGridView _grid = DataGrid;

                for (int i = 0; i < (_grid.SelectedCells.Count); i++)
                    _grid.SelectedCells[i].Value = "";

                this.ResumeLayout();
                this.Update();
            }
            // Paste function
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.V))
            {
                this.SuspendLayout();
                DataGridView _grid = DataGrid;

                if (_grid.SelectedCells.Count == 0)
                    return;

                GeneralFunctions.Paste(_grid);

                this.ResumeLayout();
                this.Update();
            }
        }
    }
}
