using IO_list_automation_new.General;
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
                DataGridView grid = Data_Grid;

                foreach (DataGridViewCell cell in grid.SelectedCells)
                    cell.Value = string.Empty;

                this.ResumeLayout();
                this.Update();
            }
            // Paste function
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.V))
            {
                this.SuspendLayout();
                DataGridView grid = Data_Grid;

                if (grid.SelectedCells.Count == 0)
                    return;

                GeneralFunctions.Paste(grid);

                this.ResumeLayout();
                this.Update();
            }
        }
    }
}