using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

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

        private void InputDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point relativePoint = PointToClient(Cursor.Position);
            ColumnIndex = e.ColumnIndex;

            if (e.Button != MouseButtons.Right)
            {
                comboBoxColumn.Visible = false;
                return;
            }

            Debug _debug = new Debug();
            _debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

            //change location to mouse press location and clear previous dropdown
            comboBoxColumn.Location = relativePoint;
            comboBoxColumn.Items.Clear();
            List<string> _columnNames = GetAvailableColumns();

            //add remove item
            comboBoxColumn.Items.Add("---");

            //add current column if it is selected
            if (InputDataGridView.Columns[ColumnIndex].HeaderText != (ColumnIndex.ToString()))
                comboBoxColumn.Items.Add(InputDataGridView.Columns[ColumnIndex].HeaderText);

            //add new columns selection
            foreach (string _column in _columnNames)
                comboBoxColumn.Items.Add(_column);

            comboBoxColumn.Visible = true;
        }

        private void ComboBoxColumn_SelectedValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;

            if (comboBox.SelectedIndex < 0 || !comboBox.Visible)
                return;

            string _columnName = comboBox.SelectedItem.ToString();

            if (_columnName == "---")
                InputDataGridView.Columns[ColumnIndex].HeaderText = (ColumnIndex.ToString());
            else
                InputDataGridView.Columns[ColumnIndex].HeaderText = _columnName;

            GetColumns();
            //after hide comboBox
            comboBoxColumn.Visible = false;
            comboBoxColumn.SelectedIndex = -1;
        }

        private List<string> GetAvailableColumns()
        {
            List<string> _columnNames = new List<string>();
            InitExcelColumnsList();

            foreach (GeneralColumn _column in ExcelColumns)
            {
                if (_column.Number == -1)
                    _columnNames.Add(_column.GetColumnName(false));
            }

            return _columnNames;
        }

        public void InitExcelColumnsList()
        {
            List<GeneralColumn> _excelColumn = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsDesignInput.Default.ColumnID, true),
                new GeneralColumn(KeywordColumn.CPU, SettingsDesignInput.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.KKS, SettingsDesignInput.Default.ColumnKKS, true),
                new GeneralColumn(KeywordColumn.RangeMin, SettingsDesignInput.Default.ColumnRangeMin, true),
                new GeneralColumn(KeywordColumn.RangeMax, SettingsDesignInput.Default.ColumnRangeMax, true),
                new GeneralColumn(KeywordColumn.Units, SettingsDesignInput.Default.ColumnUnits, true),
                new GeneralColumn(KeywordColumn.FalseText, SettingsDesignInput.Default.ColumnFalseText, true),
                new GeneralColumn(KeywordColumn.TrueText, SettingsDesignInput.Default.ColumnTrueText, true),
                new GeneralColumn(KeywordColumn.Revision, SettingsDesignInput.Default.ColumnRevision, true),
                new GeneralColumn(KeywordColumn.Cable, SettingsDesignInput.Default.ColumnCable, true),
                new GeneralColumn(KeywordColumn.Cabinet, SettingsDesignInput.Default.ColumnCabinet, true),
                new GeneralColumn(KeywordColumn.ModuleName, SettingsDesignInput.Default.ColumnModuleName, false),
                new GeneralColumn(KeywordColumn.Pin, SettingsDesignInput.Default.ColumnPin, false),
                new GeneralColumn(KeywordColumn.Channel, SettingsDesignInput.Default.ColumnChannel, false),
                new GeneralColumn(KeywordColumn.IOText, SettingsDesignInput.Default.ColumnIOText, false),
                new GeneralColumn(KeywordColumn.Page, SettingsDesignInput.Default.ColumnPage, true),
                new GeneralColumn(KeywordColumn.Changed, SettingsDesignInput.Default.ColumnChanged, true),
                new GeneralColumn(KeywordColumn.Terminal, SettingsDesignInput.Default.ColumnTerminal, true),
            };

            ExcelColumns.SetColumns(_excelColumn, false);
        }

        public DesignInputData(DataTable data)
        {
            InitializeComponent();

            ExcelColumns = new ColumnList();
            InitExcelColumnsList();

            RowOffsetInput.Text = SettingsDesignInput.Default.RowOffset.ToString();
            ExcelColumns.SortColumnsList(true);
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
            string _emptyName;
            string _name;

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
                _emptyName = "Col " + i.ToString();
                _name = InputDataGridView.Columns[i].HeaderText;
                //if column has no name selected skip
                if (_name == _emptyName)
                    continue;

                if (_name == ResourcesColumns.ID)
                    SettingsDesignInput.Default.ColumnID = i;
                else if (_name == ResourcesColumns.CPU)
                    SettingsDesignInput.Default.ColumnCPU = i;
                else if (_name == ResourcesColumns.KKS)
                    SettingsDesignInput.Default.ColumnKKS = i;
                else if (_name == ResourcesColumns.RangeMin)
                    SettingsDesignInput.Default.ColumnRangeMin = i;
                else if (_name == ResourcesColumns.RangeMax)
                    SettingsDesignInput.Default.ColumnRangeMax = i;
                else if (_name == ResourcesColumns.Units)
                    SettingsDesignInput.Default.ColumnUnits = i;
                else if (_name == ResourcesColumns.FalseText)
                    SettingsDesignInput.Default.ColumnFalseText = i;
                else if (_name == ResourcesColumns.TrueText)
                    SettingsDesignInput.Default.ColumnTrueText = i;
                else if (_name == ResourcesColumns.Revision)
                    SettingsDesignInput.Default.ColumnRevision = i;
                else if (_name == ResourcesColumns.Cable)
                    SettingsDesignInput.Default.ColumnCable = i;
                else if (_name == ResourcesColumns.Cabinet)
                    SettingsDesignInput.Default.ColumnCabinet = i;
                else if (_name == ResourcesColumns.ModuleName)
                    SettingsDesignInput.Default.ColumnModuleName = i;
                else if (_name == ResourcesColumns.Pin)
                    SettingsDesignInput.Default.ColumnPin = i;
                else if (_name == ResourcesColumns.Channel)
                    SettingsDesignInput.Default.ColumnChannel = i;
                else if (_name == ResourcesColumns.IOText)
                    SettingsDesignInput.Default.ColumnIOText = i;
                else if (_name == ResourcesColumns.Page)
                    SettingsDesignInput.Default.ColumnPage = i;
                else if (_name == ResourcesColumns.Changed)
                    SettingsDesignInput.Default.ColumnChanged = i;
                else if (_name == ResourcesColumns.Terminal)
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
                foreach (GeneralColumn _column in ExcelColumns)
                {
                    if (_column.Number != i)
                        continue;

                    InputDataGridView.Columns[i].Name = _column.Keyword;
                    InputDataGridView.Columns[i].HeaderText = _column.GetColumnName(false);
                    break;
                }
            }
        }
    }
}