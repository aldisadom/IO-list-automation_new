using ExcelDataReader;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class GeneralGrid<T>
    where T : GeneralSignal, new()
    {
        public string Name { get; }

        private List<T> Signals;
        //columns in software
        private ColumnList Columns { get; set; }
        private ColumnList BaseColumns { get; set; }

        private ProgressIndication Progress { get; set; }

        private DataGridView Grid { get; set; }

        public GeneralGrid(string name, List<T> signals, ProgressIndication progress, DataGridView grid, ColumnList columns, ColumnList baseColumns)
        {
            Name = name;
            Signals = signals;
            Columns = columns;
            BaseColumns = baseColumns;
            Progress = progress;
            Grid = grid;
        }

        public ColumnList GetColumnList()
        {
            return Columns;
        }

        public ColumnList GetBaseColumnList()
        {
            return BaseColumns;
        }

        public bool IsEmpty()
        {
            return Grid.RowCount ==0;
        }
        /// <summary>
        /// Clear all grid data
        /// </summary>
        private void GridClear()
        {
            Grid.Rows.Clear();
            Grid.Columns.Clear();

            Debug debug = new Debug();
            debug.ToFile("Clearing all grid: " + Name, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Add Columns to grid
        /// </summary>
        /// <param name="columns">list of columns</param>
        private void GridAddColumns(List<GeneralColumn> columns)
        {
            int _columnNumber = 0;

            Debug debug = new Debug();
            debug.ToFile("Putting new GeneralColumns(" + columns.Count() + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            foreach (GeneralColumn column in columns)
            {
                _columnNumber = column.GetColumnNumber();
                if (_columnNumber >= 0)
                {
                    DataGridViewColumn _columnGridView = new DataGridViewColumn();

                    DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();
                    _columnGridView.CellTemplate = _cell;
                    _columnGridView.Name = column.GetColumnKeyword();
                    _columnGridView.HeaderText = column.GetColumnName(_columnGridView.Name);
                    _columnGridView.SortMode = DataGridViewColumnSortMode.Automatic;
                    Grid.Columns.Insert(_columnNumber, _columnGridView);
                }
            }
        }

        /// <summary>
        /// copy new list to grid column list
        /// </summary>
        /// <param name="list">new GeneralColumn list</param>
        /// <param name="columnsFromZero">true, Columns start from zero</param>
        public void SetColumnList(List<GeneralColumn> list, bool columnsFromZero)
        {
            Debug debug = new Debug();
            debug.ToFile("Updating : " + Name + " Grid Columns", DebugLevels.Development, DebugMessageType.Info);

            Columns.CopyColumnList(list, columnsFromZero);
        }

        /// <summary>
        /// Get grid columns and add to columns list
        /// </summary>
        /// <returns>grid column list</returns>
        public List<GeneralColumn> GridGetColumns()
        {
            int _columnNumber = 0;
            Debug debug = new Debug();
            debug.ToFile("Getting columns from grid to memory(" + Grid.ColumnCount + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            List<GeneralColumn> _Column = new List<GeneralColumn>();
            for (_columnNumber = 0; _columnNumber < Grid.ColumnCount; _columnNumber++)
                _Column.Add(new GeneralColumn(Grid.Columns[_columnNumber].Name, Grid.Columns[_columnNumber].DisplayIndex));

            return _Column;
        }

        /// <summary>
        /// Add rows to grid
        /// </summary>
        /// <param name="count">number of signals</param>
        private void GridAddRows(int count)
        {
            Debug debug = new Debug();
            debug.ToFile("Putting new rows(" + count + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);
            for (int _rowNumber = 0; _rowNumber < count; _rowNumber++)
                Grid.Rows.Add();
        }

        /// <summary>
        /// Put all design data to grid
        /// </summary>
        public void GridPutData()
        {
            //hide grid to speed up
            Grid.Visible = false;
            Debug debug = new Debug();
            debug.ToFile(Resources.PutDataToGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);
            GridClear();
            GridAddColumns(Columns.Columns);
            GridAddRows(Signals.Count());

            Progress.RenameProgressBar(Resources.PutDataToGrid + ": " + Name, Signals.Count());

            int _columnNumber = 0;
            string _keyword = "";
            string _cellValue = "";
            for (int _signalNumber = 0; _signalNumber < Signals.Count(); _signalNumber++)
            {
                string[] _row = new string[Columns.Columns.Count()];
                foreach (GeneralColumn column in Columns)
                {
                    _columnNumber = column.GetColumnNumber();
                    if (_columnNumber >= 0)
                    {
                        //get value based on keyword
                        _keyword = column.GetColumnKeyword();
                        _cellValue = Signals.ElementAt(_signalNumber).GetValueString(_keyword, false);

                        _row[_columnNumber] = _cellValue;
                    }
                }
                Progress.UpdateProgressBar(_signalNumber);
                Grid.Rows[_signalNumber].SetValues(_row);
            }
            Progress.HideProgressBar();

            Grid.Visible = true;
            Grid.AutoResizeColumns();
            debug.ToFile(Resources.PutDataToGrid + ": " + Name + " - finished", DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Put all design data to grid
        /// </summary>
        /// <returns>there is data in grid</returns>
        public bool GridGetData()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.GetDataFromGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.GetDataFromGrid + ": " + Name, Grid.Rows.Count);

            GridGetColumns();
            int _signalCount = 0;
            Signals.Clear();
            string _keyword = "";
            string _cellValue = "";
            for (int _rowNumber = 0; _rowNumber < Grid.Rows.Count; _rowNumber++)
            {
                T _signal = new T();
                for (int _columnNumber = 0; _columnNumber < Grid.Columns.Count; _columnNumber++)
                {
                    if (Grid.Rows[_rowNumber].Cells[_columnNumber].Value != null)
                    {
                        //put value based on keyword to memory
                        _keyword = Grid.Columns[_columnNumber].Name;
                        _cellValue = Grid.Rows[_rowNumber].Cells[_columnNumber].Value.ToString();

                        _signal.SetValueFromString(_cellValue, _keyword);
                    }
                }
                Progress.UpdateProgressBar(_rowNumber);

                if (_signal.ValidateSignal())
                {
                    _signalCount++;
                    Signals.Add(_signal);
                }

            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.GetDataFromGrid + ": " + Name + " - finished", DebugLevels.Development, DebugMessageType.Info);

            if (_signalCount == 0)
                debug.ToPopUp(Resources.NoDataGrid + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return (_signalCount > 0);
        }

        /// <summary>
        /// Get excel cell data, uses general function and always return string
        /// </summary>
        /// <param name="row">current row</param>
        /// <param name="col">column to read</param>
        /// <param name="maxCol">maximum columns in row</param>
        /// <param name="excel">opened excel file</param>
        /// <returns>string value of cell value</returns>
        public string ReadExcelCell(int row, int col, int maxCol, IExcelDataReader excel)
        {
            string _retunValue = string.Empty;
            if (col >= maxCol || col < 0)
            {
                Debug debug = new Debug();
                debug.ToFile(Resources.DataReadFailBounds + " " + Resources.Column + " " + col + " max(" + maxCol + ")" +
                                Resources.Row + " " + row, DebugLevels.Minimum, DebugMessageType.Warning);
            }
            else
            {
                System.Type _type = excel.GetFieldType(col);
                if (_type == null)
                    _retunValue = string.Empty;
                else
                {
                    if (_type.Name == "String")
                        _retunValue = excel.GetString(col);
                    else if (_type.Name == "Double")
                        _retunValue = excel.GetDouble(col).ToString();
                    else
                    {
                        Debug debug = new Debug();
                        debug.ToPopUp(Resources.DataReadFailFormat + ": " + _type.Name, DebugLevels.None, DebugMessageType.Critical);
                    }
                }
            }
            return _retunValue;
        }

        /// <summary>
        /// Create save file of current grid to excel with file sellection
        /// </summary>
        public void SaveSellect()
        {
            Debug debug = new Debug();

            SaveFileDialog _saveFile = new SaveFileDialog();
            _saveFile.Filter = Name + "Worksheets|*." + Name;
            _saveFile.AddExtension = false;
            if (_saveFile.ShowDialog() == DialogResult.OK)
                SaveToFile(_saveFile.FileName);
            else
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
        }

        /// <summary>
        /// Create save file of current grid to excel
        /// </summary>
        /// <param name="fileName">file name to save</param>
        public void SaveToFile(string fileName)
        {
            if (GridGetData())
            {
                string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + Name;

                Debug debug = new Debug();
                debug.ToFile(Resources.SaveDataToFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

                Progress.RenameProgressBar(Resources.SaveDataToFile + ": " + _fileName, Signals.Count());

                Sheet sheet = new Sheet()
                {
                    Name = "Data",
                    ColumnsWidth = new List<double> { 10, 12, 8, 8, 35 }
                };
                ExcelWriter _excel = new ExcelWriter(_fileName);

                string _keyword = "";
                string _cellValue = "";
                int _ColumnNumber = 0;
                int _rowOffset = 1;

                //first line in excel is column keywords
                foreach (GeneralColumn column in Columns)
                {
                    _ColumnNumber = column.GetColumnNumber();
                    if (_ColumnNumber >= 0)
                    {
                        _keyword = column.GetColumnKeyword();

                        _excel.Write(_keyword, _ColumnNumber + 1, _rowOffset);
                    }
                }
                _rowOffset++;

                for (int _signalNumber = 0; _signalNumber < Signals.Count(); _signalNumber++)
                {
                    string[] _row = new string[Columns.Columns.Count()];
                    foreach (GeneralColumn column in Columns)
                    {
                        _ColumnNumber = column.GetColumnNumber();
                        if (_ColumnNumber >= 0)
                        {
                            _keyword = column.GetColumnKeyword();
                            _cellValue = Signals.ElementAt(_signalNumber).GetValueString(_keyword, false);

                            _excel.Write(_cellValue, _ColumnNumber + 1, _signalNumber + _rowOffset);
                        }
                    }
                    Progress.UpdateProgressBar(_signalNumber);
                }
                _excel.Save();
                _excel.Dispose();

                Progress.HideProgressBar();

                debug.ToFile(Resources.SaveDataToFile + ": " + _fileName + " - finished", DebugLevels.Development, DebugMessageType.Info);
            }
        }

        /// <summary>
        /// load data from excel to grid with file sellection
        /// </summary>
        public void LoadSellect()
        {
            Debug debug = new Debug();

            OpenFileDialog _loadFile = new OpenFileDialog();
            _loadFile.Filter = Name + "Worksheets|*." + Name;
            if (_loadFile.ShowDialog() == DialogResult.OK)
                LoadFromFile(_loadFile.FileName);
            else
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
        }

        /// <summary>
        /// load data from excel to grid
        /// </summary>
        /// <param name="fileName">save file name</param>
        public void LoadFromFile(string fileName)
        {
            string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + Name;

            Debug debug = new Debug();
            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            if (File.Exists(_fileName))
            {
                FileStream stream = File.Open(_fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader _excel = ExcelReaderFactory.CreateReader(stream);

                int _rowCount = _excel.RowCount;
                int _columnCount = 0;
                int _columnNumber = 0;
                string _cellValue = "";
                string _ColumnName = "";
                Progress.RenameProgressBar(Resources.LoadDataFromFile + ": " + _fileName, _rowCount);

                List<GeneralColumn> _Column = new List<GeneralColumn>();

                _excel.Read();
                //read column keywords in line 1
                for (_columnNumber = 0; _columnNumber < _excel.FieldCount; _columnNumber++)
                {
                    _cellValue = _excel.GetString(_columnNumber);
                    if (_cellValue == null || _cellValue == string.Empty)
                        break;
                    else
                        _Column.Add(new GeneralColumn(_cellValue, _columnNumber));
                }
                SetColumnList(_Column, false);

                Signals.Clear();

                //read all excel rows from line 2
                for (int _row = 2; _row <= _rowCount; _row++)
                {
                    // if nothing to read exit
                    if (!_excel.Read())
                        break;

                    _columnCount = _excel.FieldCount;
                    //create signal and add coresponding Columns to each signal element
                    T _signal = new T();

                    foreach (GeneralColumn _column in GetColumnList())
                    {
                        _columnNumber = _column.GetColumnNumber();
                        if (_columnNumber != -1)
                        {
                            _cellValue = ReadExcelCell(_row, _columnNumber, _columnCount, _excel);
                            _ColumnName = _column.GetColumnKeyword();

                            //convert null to ""
                            if (_cellValue == null)
                                _cellValue = string.Empty;

                            _signal.SetValueFromString(_cellValue, _ColumnName);
                        }
                    }
                    Progress.UpdateProgressBar(_row);

                    if (_signal.ValidateSignal())
                    {
                        Signals.Add(_signal);
                    }
                }
                _excel.Close();

                Progress.HideProgressBar();

                GridPutData();
            }
            else
                debug.ToPopUp(Resources.NoFile + ": " + _fileName, DebugLevels.None, DebugMessageType.Alarm);

            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName + " - finished", DebugLevels.Development, DebugMessageType.Info);
        }
    }
}
