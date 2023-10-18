using ExcelDataReader;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace IO_list_automation_new.Forms
{
    public partial class DesignInputData : Form
    {
        string PreviousOffset;
        int RowOffset = 0;
        int ColumnIndex = 0;

        private ColumnList ExcelColumns { get; set; }

        public void InitExcelColumnsList()
        {
            List<GeneralColumn> _excelColumn = new List<GeneralColumn>();
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameID, SettingsDesignInput.Default.ColumnID, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameCPU, SettingsDesignInput.Default.ColumnCPU, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameKKS, SettingsDesignInput.Default.ColumnKKS, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameRangeMin, SettingsDesignInput.Default.ColumnRangeMin, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameRangeMax, SettingsDesignInput.Default.ColumnRangeMax, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameUnits, SettingsDesignInput.Default.ColumnUnits, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameFalseText, SettingsDesignInput.Default.ColumnFalseText, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameTrueText, SettingsDesignInput.Default.ColumnTrueText, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameRevision, SettingsDesignInput.Default.ColumnRevision, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameCable, SettingsDesignInput.Default.ColumnCable, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameCabinet, SettingsDesignInput.Default.ColumnCabinet, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameModuleName, SettingsDesignInput.Default.ColumnModuleName, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNamePin, SettingsDesignInput.Default.ColumnPin, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameChannel, SettingsDesignInput.Default.ColumnChannel, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameIOText, SettingsDesignInput.Default.ColumnIOText, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNamePage, SettingsDesignInput.Default.ColumnPage, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameChanged, SettingsDesignInput.Default.ColumnChanged, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameTerminal, SettingsDesignInput.Default.ColumnTerminal, true));

            ExcelColumns.SetColumns(_excelColumn, false);
        }

        public DesignInputData(string[,] _data)
        {
            InitializeComponent();

            ExcelColumns = new ColumnList();
            InitExcelColumnsList();

            RowOffsetInput.Text = SettingsDesignInput.Default.RowOffset.ToString();
            PreviousOffset = SettingsDesignInput.Default.RowOffset.ToString();
            RowOffset = SettingsDesignInput.Default.RowOffset;

            PinHasNumber.Checked = SettingsDesignInput.Default.PinHasNumber;
            PinIsNumber.Checked = SettingsDesignInput.Default.PinIsNumber;
            ChannelHasNumber.Checked = SettingsDesignInput.Default.ChannelHasNumber;
            ChannelIsNumber.Checked = SettingsDesignInput.Default.ChannelIsNumber;

            //adding columns
            for (int i=0; i< _data.GetLength(1); i++)
            {
                DataGridViewColumn _columnGridView = new DataGridViewColumn();

                DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();
                _columnGridView.CellTemplate = _cell;                
                _columnGridView.SortMode = DataGridViewColumnSortMode.NotSortable;
                InputDataGridView.Columns.Insert(i, _columnGridView);
            }

            //adding rows
            for (int i = 0; i < _data.GetLength(0); i++)
                InputDataGridView.Rows.Add();

            RenameColumns();

            //for removing columns that are outside of excel range
            GetColumns();
            
            //add data to grid
            for (int i = 0; i < _data.GetLength(0); i++)
            {
                string[] _row = new string[_data.GetLength(1)];

                for (int j = 0; j < _data.GetLength(1); j++)
                    _row[j] = _data[i,j];

                InputDataGridView.Rows[i].SetValues(_row);
            }

            InputDataGridView.Visible = true;
            InputDataGridView.AutoResizeColumns();
        }

        //update settings from grid
        private void GetColumns()
        {
            string _emptyName = string.Empty;
            string _name = string.Empty;

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
                else
                {
                    if (_name == Resources.ColumnID)
                        SettingsDesignInput.Default.ColumnID = i;
                    else if (_name == Resources.ColumnCPU)
                        SettingsDesignInput.Default.ColumnCPU = i;
                    else if (_name == Resources.ColumnKKS)
                        SettingsDesignInput.Default.ColumnKKS = i;
                    else if (_name == Resources.ColumnRangeMin)
                        SettingsDesignInput.Default.ColumnRangeMin = i;
                    else if (_name == Resources.ColumnRangeMax)
                        SettingsDesignInput.Default.ColumnRangeMax = i;
                    else if (_name == Resources.ColumnUnits)
                        SettingsDesignInput.Default.ColumnUnits = i;
                    else if (_name == Resources.ColumnFalseText)
                        SettingsDesignInput.Default.ColumnFalseText = i;
                    else if (_name == Resources.ColumnTrueText)
                        SettingsDesignInput.Default.ColumnTrueText = i;
                    else if (_name == Resources.ColumnRevision)
                        SettingsDesignInput.Default.ColumnRevision = i;
                    else if (_name == Resources.ColumnCable)
                        SettingsDesignInput.Default.ColumnCable = i;
                    else if (_name == Resources.ColumnCabinet)
                        SettingsDesignInput.Default.ColumnCabinet = i;
                    else if (_name == Resources.ColumnModuleName)
                        SettingsDesignInput.Default.ColumnModuleName = i;
                    else if (_name == Resources.ColumnPin)
                        SettingsDesignInput.Default.ColumnPin = i;
                    else if (_name == Resources.ColumnChannel)
                        SettingsDesignInput.Default.ColumnChannel = i;
                    else if (_name == Resources.ColumnIOText)
                        SettingsDesignInput.Default.ColumnIOText = i;
                    else if (_name == Resources.ColumnPage)
                        SettingsDesignInput.Default.ColumnPage = i;
                    else if (_name == Resources.ColumnChanged)
                        SettingsDesignInput.Default.ColumnChanged = i;
                    else if (_name == Resources.ColumnTerminal)
                        SettingsDesignInput.Default.ColumnTerminal = i;
                }
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
                    if (_column.Number == i)
                    {
                        InputDataGridView.Columns[i].Name = _column.Keyword;
                        InputDataGridView.Columns[i].HeaderText = _column.GetColumnName();
                        break;
                    }
                }
            }
        }

        private void RowOffsetInput_TextChanged(object sender, EventArgs e)
        {
            int _value = 0;
            if (int.TryParse(RowOffsetInput.Text, out _value))
            {
                //if negative wrte data from settings
                if (_value < 0)
                    RowOffsetInput.Text = SettingsDesignInput.Default.RowOffset.ToString();
                else
                    RowOffset = _value;

                PreviousOffset = RowOffsetInput.Text;
            }
            else
                RowOffsetInput.Text = PreviousOffset;
        }

        private void DesignInputData_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingsDesignInput.Default.PinHasNumber = PinHasNumber.Checked;
            SettingsDesignInput.Default.PinIsNumber = PinIsNumber.Checked;
            SettingsDesignInput.Default.ChannelIsNumber = ChannelIsNumber.Checked;
            SettingsDesignInput.Default.ChannelHasNumber = ChannelHasNumber.Checked;
            SettingsDesignInput.Default.Save();
        }

        private List<string> GetAwailableColumns()
        {
            List<string> _columnNames = new List<string>();

            foreach (GeneralColumn _column in ExcelColumns)
            {
                if (_column.Number == -1)
                    _columnNames.Add(_column.GetColumnName());
            }

            return _columnNames;
        }

        private void InputDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point relativePoint = PointToClient(Cursor.Position);
            ColumnIndex = e.ColumnIndex;

            if (e.Button == MouseButtons.Right)
            {
                Debug _debug = new Debug();
                _debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

                //change location to mouse press location and clear previous dropdown
                comboboxColumn.Location = relativePoint;
                comboboxColumn.Items.Clear();
                string _keyword = string.Empty;
                string _boxText = string.Empty;

                List<string> _columnNames = GetAwailableColumns();

                //add remove item
                comboboxColumn.Items.Add("---");

                //add curent column if is is selected
                if (InputDataGridView.Columns[ColumnIndex].HeaderText != ("Col " + ColumnIndex.ToString()))
                    comboboxColumn.Items.Add(InputDataGridView.Columns[ColumnIndex].HeaderText);

                //add new columns selection
                foreach (string _column in _columnNames)
                    comboboxColumn.Items.Add(_column);

                comboboxColumn.Visible = true;
            }
            else if (comboboxColumn.Visible)
                comboboxColumn.Visible = false;
        }

        private void comboboxColumn_SelectedValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;

            if (comboBox.SelectedIndex >= 0 && comboBox.Visible)
            {
                string _columnName = comboBox.SelectedItem.ToString();

                if (_columnName == "---")
                    InputDataGridView.Columns[ColumnIndex].HeaderText = ("Col " + ColumnIndex.ToString());
                else
                    InputDataGridView.Columns[ColumnIndex].HeaderText = _columnName;

                GetColumns();
                //after hide combobox
                comboboxColumn.Visible = false;
                comboboxColumn.SelectedIndex = -1;
            }
        }
    }
}
