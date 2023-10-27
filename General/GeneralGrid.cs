using ExcelDataReader;
using IO_list_automation_new.General;
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
        private string Name { get; }
        public string FileExtension { get; }
        private bool NotSortable { get; set; }

        private List<T> Signals;
        //columns in software
        private ColumnList Columns { get; set; }
        private ColumnList BaseColumns { get; set; }

        private ProgressIndication Progress { get; set; }

        private DataGridView Grid { get; set; }

        public GeneralGrid(string name,bool notSortable, string fileExtension, List<T> signals, ProgressIndication progress, DataGridView grid, ColumnList columns, ColumnList baseColumns)
        {
            Name = name;
            Signals = signals;
            Columns = columns;
            BaseColumns = baseColumns;
            Progress = progress;
            Grid = grid;
            FileExtension = fileExtension;
            NotSortable = notSortable;
        }

        /// <summary>
        /// Cheacks if rows are empty, 1 line is also empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (Grid == null)
                return true;

            return Grid.RowCount <= 1;
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
        private void AddColumns(List<GeneralColumn> columns)
        {
            int _columnNumber = 0;

            Debug debug = new Debug();
            debug.ToFile("Putting new GeneralColumns(" + columns.Count() + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            foreach (GeneralColumn _column in columns)
            {
                _columnNumber = _column.Number;
                if (_columnNumber >= 0)
                {
                    DataGridViewColumn _columnGridView = new DataGridViewColumn();

                    DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();
                    _columnGridView.CellTemplate = _cell;
                    _columnGridView.Name = _column.Keyword;
                    _columnGridView.HeaderText = _column.GetColumnName();
                    if (NotSortable)
                        _columnGridView.SortMode = DataGridViewColumnSortMode.NotSortable;
                    else
                        _columnGridView.SortMode = DataGridViewColumnSortMode.Automatic;
                    Grid.Columns.Insert(_columnNumber, _columnGridView);
                }
            }
        }

        /// <summary>
        /// Get grid columns and add to columns list
        /// </summary>
        /// <returns>grid column list</returns>
        public List<GeneralColumn> GetColumns()
        {
            int _columnNumber = 0;
            int _columnIndex = 0;
            string _keyword = string.Empty;
            bool _canHide = false;

            Debug debug = new Debug();
            debug.ToFile("Getting columns from grid to memory(" + Grid.ColumnCount + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            List<GeneralColumn> _columnList = new List<GeneralColumn>();
            for (_columnNumber = 0; _columnNumber < Grid.ColumnCount; _columnNumber++)
            {
                _keyword = Grid.Columns[_columnNumber].Name;
                _columnIndex = Grid.Columns[_columnNumber].DisplayIndex;

                foreach (GeneralColumn _column in BaseColumns)
                {
                    if (_column.Keyword == _keyword)
                    {
                        _canHide = _column.CanHide;
                        break;
                    }
                }
                _columnList.Add(new GeneralColumn(_keyword, _columnIndex, _canHide));
            }
            return _columnList;
        }

        /// <summary>
        /// Add rows to grid
        /// </summary>
        /// <param name="count">number of signals</param>
        private void AddRows(int count)
        {
            Debug debug = new Debug();
            debug.ToFile("Putting new rows(" + count + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);
            for (int _rowNumber = 0; _rowNumber < count; _rowNumber++)
                Grid.Rows.Add();
        }

        /// <summary>
        /// Put all design data to grid
        /// </summary>
        public void PutData()
        {
            Debug debug = new Debug();
            if (Signals.Count() > 0)
            {
                //hide grid to speed up
                Grid.Visible = false;
                Grid.SuspendLayout();

                debug.ToFile(Resources.PutDataToGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);
                GridClear();
                AddColumns(Columns.Columns);
                AddRows(Signals.Count());

                Progress.RenameProgressBar(Resources.PutDataToGrid + ": " + Name, Signals.Count());

                int _columnNumber = 0;
                string _keyword = string.Empty;
                string _cellValue = string.Empty;
                for (int _signalNumber = 0; _signalNumber < Signals.Count(); _signalNumber++)
                {
                    string[] _row = new string[Columns.Columns.Count()];
                    foreach (GeneralColumn _column in Columns)
                    {
                        _columnNumber = _column.Number;
                        if (_columnNumber >= 0)
                        {
                            //get value based on keyword
                            _keyword = _column.Keyword;
                            _cellValue = Signals.ElementAt(_signalNumber).GetValueString(_keyword, false);

                            _row[_columnNumber] = _cellValue;
                        }
                    }
                    Progress.UpdateProgressBar(_signalNumber);
                    Grid.Rows[_signalNumber].SetValues(_row);
                }
                Progress.HideProgressBar();

                Grid.ResumeLayout();
                Grid.Visible = true;
                Grid.AutoResizeColumns();
                debug.ToFile(Resources.PutDataToGrid + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
            }
            else
                debug.ToPopUp(Resources.PutDataToGrid + ": " + Name + " - " + Resources.NoData, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Put all design data to grid
        /// </summary>
        /// <returns>there is data in grid</returns>
        public bool GetData()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.GetDataFromGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.GetDataFromGrid + ": " + Name, Grid.Rows.Count);

            GetColumns();
            int _signalCount = 0;
            Signals.Clear();
            string _keyword = string.Empty;
            string _cellValue = string.Empty;
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

            debug.ToFile(Resources.GetDataFromGrid + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (_signalCount == 0)
                debug.ToPopUp(Resources.NoDataGrid + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return _signalCount > 0;
        }

        /// <summary>
        /// Create save file of current grid to excel with file selection
        /// </summary>
        public void SaveSelect()
        {
            Debug debug = new Debug();

            SaveFileDialog _saveFile = new SaveFileDialog();
            _saveFile.Filter = Name + "|*" + FileExtension;
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
            if (GetData())
            {
                string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

                Debug debug = new Debug();
                debug.ToFile(Resources.SaveDataToFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

                Progress.RenameProgressBar(Resources.SaveDataToFile + ": " + _fileName, Signals.Count());

                ExcelWriter _excel = new ExcelWriter(_fileName);

                string _keyword = string.Empty;
                string _cellValue = string.Empty;
                int _ColumnNumber = 0;
                int _rowOffset = 1;

                //first line in excel is column keywords
                foreach (GeneralColumn _column in Columns)
                {
                    _ColumnNumber = _column.Number;
                    if (_ColumnNumber >= 0)
                    {
                        _keyword = _column.Keyword;

                        _excel.Write(_keyword, _ColumnNumber + 1, _rowOffset);
                    }
                }
                _rowOffset++;

                for (int _signalNumber = 0; _signalNumber < Signals.Count(); _signalNumber++)
                {
                    string[] _row = new string[Columns.Columns.Count()];
                    foreach (GeneralColumn _column in Columns)
                    {
                        _ColumnNumber = _column.Number;
                        if (_ColumnNumber >= 0)
                        {
                            _keyword = _column.Keyword;
                            _cellValue = Signals.ElementAt(_signalNumber).GetValueString(_keyword, false);

                            _excel.Write(_cellValue, _ColumnNumber + 1, _signalNumber + _rowOffset);
                        }
                    }
                    Progress.UpdateProgressBar(_signalNumber);
                }
                _excel.Save();
                _excel.Dispose();

                Progress.HideProgressBar();

                debug.ToFile(Resources.SaveDataToFile + ": " + _fileName + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
            }
        }

        /// <summary>
        /// load data from excel to grid with file selection
        /// </summary>
        /// <returns>there is valid data</returns>
        public bool LoadSelect()
        {
            Debug debug = new Debug();

            OpenFileDialog _loadFile = new OpenFileDialog();
            _loadFile.Filter = Name + "|*" + FileExtension;
            if (_loadFile.ShowDialog() == DialogResult.OK)
                return LoadFromFile(_loadFile.FileName);
            else
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);

            return false;
        }

        /// <summary>
        /// load data from excel to memory
        /// </summary>
        /// <param name="fileName">save file name</param>
        /// <returns>there is valid data</returns>
        public bool LoadFromFileToMemory(string fileName)
        {
            string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            if (File.Exists(_fileName))
            {
                FileStream stream = File.Open(_fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader _excel = ExcelReaderFactory.CreateReader(stream);

                int _rowCount = _excel.RowCount;
                int _columnCount = 0;
                int _columnNumber = 0;
                string _cellValue = string.Empty;
                string _ColumnName = string.Empty;
                Progress.RenameProgressBar(Resources.LoadDataFromFile + ": " + _fileName, _rowCount);

                List<GeneralColumn> _Column = new List<GeneralColumn>();

                for (int i = 0; i < _excel.ResultsCount; i++)
                {
                    _excel.Read();
                    //read column keywords in line 1
                    for (_columnNumber = 0; _columnNumber < _excel.FieldCount; _columnNumber++)
                    {
                        _cellValue = _excel.GetString(_columnNumber);
                        if (_cellValue == null || _cellValue == string.Empty)
                            break;
                        else
                            _Column.Add(new GeneralColumn(_cellValue, _columnNumber, false));
                    }
                    Columns.SetColumns(_Column, false);

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

                        foreach (GeneralColumn _column in Columns.Columns)
                        {
                            _columnNumber = _column.Number;
                            if (_columnNumber != -1)
                            {
                                _cellValue = GeneralFunctions.ReadExcelCell(_row, _columnNumber, _columnCount, _excel);
                                _ColumnName = _column.Keyword;

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
                    if (!_excel.NextResult())
                       break;
                }
                _excel.Close();

                Progress.HideProgressBar();
            }
            else
            {
                debug.ToPopUp(Resources.NoFile + ": " + _fileName, DebugLevels.None, DebugMessageType.Alarm);
                return false;
            }

            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (Signals.Count > 0)
                return true;
            else
            {
                debug.ToPopUp(Resources.LoadDataFromFile + ": " + _fileName + " - " + Resources.NoData, DebugLevels.None, DebugMessageType.Info);
                return false;
            }
        }
        /// <summary>
        /// load data from excel to grid
        /// </summary>
        /// <param name="fileName">save file name</param>
        public bool LoadFromFile(string fileName)
        {
            if (LoadFromFileToMemory(fileName))
            {
                PutData();
                return true;
            }
            else
                return false;
        }
    }
}
