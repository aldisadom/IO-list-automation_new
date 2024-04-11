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
                InputDataGridView.Columns[ColumnIndex].HeaderText = columnName;

            GetColumns();
            //after hide comboBox
            comboBoxColumn.Visible = false;
            comboBoxColumn.SelectedIndex = -1;
        }

        private List<string> GetAvailableColumns()
        {
            List<string> columnNames = new List<string>();
            InitExcelColumnsList();

            foreach (var column in ExcelColumns.Columns)
            {
                if (column.Value.NR == -1)
                    columnNames.Add(TextHelper.GetColumnName(column.Key, false));
            }

            return columnNames;
        }

        public void InitExcelColumnsList()
        {
            ColumnList excelColumns = new ColumnList();

            excelColumns.Columns.Add(KeywordColumn.ID, new GeneralColumnParameters(0, false));
            excelColumns.Columns.Add(KeywordColumn.CPU, new GeneralColumnParameters(1, true));
            excelColumns.Columns.Add(KeywordColumn.KKS, new GeneralColumnParameters(2, false));
            excelColumns.Columns.Add(KeywordColumn.RangeMin, new GeneralColumnParameters(3, true));
            excelColumns.Columns.Add(KeywordColumn.RangeMax, new GeneralColumnParameters(4, true));
            excelColumns.Columns.Add(KeywordColumn.Units, new GeneralColumnParameters(5, true));
            excelColumns.Columns.Add(KeywordColumn.FalseText, new GeneralColumnParameters(6, true));
            excelColumns.Columns.Add(KeywordColumn.TrueText, new GeneralColumnParameters(7, true));
            excelColumns.Columns.Add(KeywordColumn.Revision, new GeneralColumnParameters(8, true));
            excelColumns.Columns.Add(KeywordColumn.Cable, new GeneralColumnParameters(9, true));
            excelColumns.Columns.Add(KeywordColumn.Cabinet, new GeneralColumnParameters(10, false));
            excelColumns.Columns.Add(KeywordColumn.ModuleName, new GeneralColumnParameters(11, false));
            excelColumns.Columns.Add(KeywordColumn.Pin, new GeneralColumnParameters(12, true));
            excelColumns.Columns.Add(KeywordColumn.Channel, new GeneralColumnParameters(13, false));
            excelColumns.Columns.Add(KeywordColumn.IOText, new GeneralColumnParameters(14, false));
            excelColumns.Columns.Add(KeywordColumn.Page, new GeneralColumnParameters(15, true));
            excelColumns.Columns.Add(KeywordColumn.Changed, new GeneralColumnParameters(16, true));
            excelColumns.Columns.Add(KeywordColumn.Terminal, new GeneralColumnParameters(17, true));

            ExcelColumns.SetColumns(excelColumns, false);
        }

        public DesignInputData(DataTable data)
        {
            InitializeComponent();

            ExcelColumns = new ColumnList();
            InitExcelColumnsList();

            RowOffsetInput.Text = SettingsDesignInput.Default.RowOffset.ToString();
            ExcelColumns.SortColumns(true);
            Grid = new GeneralGrid(Name, GridTypes.DataNoEdit, InputDataGridView, ExcelColumns);
            Grid.PutData(data);

            ElementHasChannelAndIsNumber.Text = ResourcesUI.ElementHasChannelAndIsNumber;
            ElementHasIOText.Text = ResourcesUI.ElementHasIOText;
            ElementHasModuleName.Text = ResourcesUI.ElementHasModuleName;
        }

        /// <summary>
        /// update settings from grid
        /// </summary>
        private void GetColumns()
        {
            string emptyName;
            string name;

            //init all settings
            SettingsDesignInput.Default.ColumnID = -1;
            SettingsDesignInput.Default.ColumnCPU = -1;
            SettingsDesignInput.Default.ColumnKKS = -1;
            SettingsDesignInput.Default.ColumnRangeMin = -1;
            SettingsDesignInput.Default.ColumnRangeMax = -1;
            SettingsDesignInput.Default.ColumnUnits = -1;
            SettingsDesignInput.Default.ColumnFalseText = -1;
            SettingsDesignInput.Default.ColumnTrueText = -1;
            SettingsDesignInput.Default.ColumnRevision = -1;
            SettingsDesignInput.Default.ColumnCable = -1;
            SettingsDesignInput.Default.ColumnCabinet = -1;
            SettingsDesignInput.Default.ColumnModuleName = -1;
            SettingsDesignInput.Default.ColumnPin = -1;
            SettingsDesignInput.Default.ColumnChannel = -1;
            SettingsDesignInput.Default.ColumnIOText = -1;
            SettingsDesignInput.Default.ColumnPage = -1;
            SettingsDesignInput.Default.ColumnChanged = -1;
            SettingsDesignInput.Default.ColumnTerminal = -1;

            //try to get all settings from columns
            for (int i = 0; i < InputDataGridView.Columns.Count; i++)
            {
                emptyName = "Col " + i.ToString();
                name = InputDataGridView.Columns[i].HeaderText;
                //if column has no name selected skip
                if (name == emptyName)
                    continue;

                if (name == ResourcesColumns.ID)
                    SettingsDesignInput.Default.ColumnID = i;
                else if (name == ResourcesColumns.CPU)
                    SettingsDesignInput.Default.ColumnCPU = i;
                else if (name == ResourcesColumns.KKS)
                    SettingsDesignInput.Default.ColumnKKS = i;
                else if (name == ResourcesColumns.RangeMin)
                    SettingsDesignInput.Default.ColumnRangeMin = i;
                else if (name == ResourcesColumns.RangeMax)
                    SettingsDesignInput.Default.ColumnRangeMax = i;
                else if (name == ResourcesColumns.Units)
                    SettingsDesignInput.Default.ColumnUnits = i;
                else if (name == ResourcesColumns.FalseText)
                    SettingsDesignInput.Default.ColumnFalseText = i;
                else if (name == ResourcesColumns.TrueText)
                    SettingsDesignInput.Default.ColumnTrueText = i;
                else if (name == ResourcesColumns.Revision)
                    SettingsDesignInput.Default.ColumnRevision = i;
                else if (name == ResourcesColumns.Cable)
                    SettingsDesignInput.Default.ColumnCable = i;
                else if (name == ResourcesColumns.Cabinet)
                    SettingsDesignInput.Default.ColumnCabinet = i;
                else if (name == ResourcesColumns.ModuleName)
                    SettingsDesignInput.Default.ColumnModuleName = i;
                else if (name == ResourcesColumns.Pin)
                    SettingsDesignInput.Default.ColumnPin = i;
                else if (name == ResourcesColumns.Channel)
                    SettingsDesignInput.Default.ColumnChannel = i;
                else if (name == ResourcesColumns.IOText)
                    SettingsDesignInput.Default.ColumnIOText = i;
                else if (name == ResourcesColumns.Page)
                    SettingsDesignInput.Default.ColumnPage = i;
                else if (name == ResourcesColumns.Changed)
                    SettingsDesignInput.Default.ColumnChanged = i;
                else if (name == ResourcesColumns.Terminal)
                    SettingsDesignInput.Default.ColumnTerminal = i;
            }
            SettingsDesignInput.Default.Save();
            InitExcelColumnsList();
        }

        /// <summary>
        /// rename grid columns from settings
        /// </summary>
        private void RenameColumns()
        {
            for (int i = 0; i < InputDataGridView.Columns.Count; i++)
            {
                InputDataGridView.Columns[i].HeaderText = "Col " + i.ToString();
                foreach (var column in ExcelColumns.Columns)
                {
                    if (column.Value.NR != i)
                        continue;

                    InputDataGridView.Columns[i].Name = column.Key;
                    InputDataGridView.Columns[i].HeaderText = TextHelper.GetColumnName(column.Key, false);
                    break;
                }
            }
        }
    }
}