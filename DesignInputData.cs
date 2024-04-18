using IO_list_automation_new.General;
using IO_list_automation_new.Helper_functions;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace IO_list_automation_new.Forms
{
    public partial class DesignInputData : Form
    {
        private int ColumnIndex = 0;

        private GeneralGrid Grid { get; set; }

        private ColumnList ExcelColumns { get; set; }

        public DesignInputData(DataTable data, ColumnList excelColumns)
        {
            InitializeComponent();

            RowOffsetInput.Text = SettingsDesignInput.Default.RowOffset.ToString();

            ExcelColumns = excelColumns;
            ExcelColumns.LoadColumnsParameters();
            Grid = new GeneralGrid(Name, GridTypes.DataNoEdit, InputDataGridView, ExcelColumns);
            Grid.PutData(data);
            ExcelColumns.SaveColumnsParameters();

            ElementHasChannelAndIsNumber.Text = ResourcesUI.ElementHasChannelAndIsNumber;
            ElementHasIOText.Text = ResourcesUI.ElementHasIOText;
            ElementHasModuleName.Text = ResourcesUI.ElementHasModuleName;
        }

        /// <summary>
        /// ComboBox accept only numbers
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">event arguments</param>
        private void RowOffsetInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key is not a digit and not a control key (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }

        /// <summary>
        /// update settings from form data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignInputData_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingsDesignInput.Default.RowOffset = int.Parse(RowOffsetInput.Text);
            SettingsDesignInput.Default.Save();
        }

        private void InputDataGridViewColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point relativePoint = PointToClient(Cursor.Position);
            ColumnIndex = e.ColumnIndex;

            if (e.Button != MouseButtons.Right)
            {
                comboBoxColumn.Visible = false;
                return;
            }

            Debug debug = new Debug();
            debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

            //change location to mouse press location and clear previous dropdown
            comboBoxColumn.Location = relativePoint;
            comboBoxColumn.Items.Clear();
            List<string> columnNames = GetAvailableColumns();

            //add remove item
            comboBoxColumn.Items.Add("---");

            //add current column if it is selected
            if (InputDataGridView.Columns[ColumnIndex].HeaderText != (ColumnIndex.ToString()))
                comboBoxColumn.Items.Add(InputDataGridView.Columns[ColumnIndex].HeaderText);

            //add new columns selection
            foreach (string column in columnNames)
                comboBoxColumn.Items.Add(column);

            comboBoxColumn.Visible = true;
        }

        private void ComboBoxColumnSelectedValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;

            if (comboBox.SelectedIndex < 0 || !comboBox.Visible)
                return;

            string columnName = comboBox.SelectedItem.ToString();

            if (columnName == "---")
                InputDataGridView.Columns[ColumnIndex].HeaderText = (ColumnIndex.ToString());
            else
            {
                InputDataGridView.Columns[ColumnIndex].Name = columnName;
                InputDataGridView.Columns[ColumnIndex].HeaderText = TextHelper.GetColumnName(columnName, false);
            }

            UpdateColumnFromGrid();
            //after hide comboBox
            comboBoxColumn.Visible = false;
            comboBoxColumn.SelectedIndex = -1;
        }

        private List<string> GetAvailableColumns()
        {
            List<string> columnNames = new List<string>();
            bool found;
            //jei yra toks vistiek prideda, ir jei pasirenku tam tikrus stulpelius tai juos ir reiktu i design butinai perduot
            foreach (var column in ExcelColumns.Columns)
            {
                found = false;
                for (int i = 0; i < InputDataGridView.Columns.Count; i++)
                {
                    if (InputDataGridView.Columns[i].Name == column.Key)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    column.Value.Hidden = false;
                    columnNames.Add(column.Key);
                }
            }

            return columnNames;
        }

        /// <summary>
        /// update settings from grid
        /// </summary>
        private void UpdateColumnFromGrid()
        {
            string columnName;

            //try to get all settings from columns
            for (int i = 0; i < InputDataGridView.Columns.Count; i++)
            {
                columnName = InputDataGridView.Columns[i].Name;

                if (ExcelColumns.Columns.TryGetValue(columnName, out ColumnParameters columnParameters))
                {
                    columnParameters.Hidden = false;
                    columnParameters.NR = i;
                }
            }
            ExcelColumns.SaveColumnsParameters();
        }
    }
}