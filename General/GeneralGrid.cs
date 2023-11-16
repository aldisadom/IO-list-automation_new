using ExcelDataReader;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal enum GridTypes
    {
        Data,
        DataNoEdit,
        DBEditable,
        DBForceEdit,
        DB,
    }

    internal class GeneralGrid
    {
        private string Name { get; }
        public string FileExtension { get; }
        private GridTypes GridType { get; }

        //columns in software
        private ColumnList Columns { get; set; }

        private ProgressIndication Progress { get; }

        private DataGridView Grid { get; set; }

        public bool UseKeywordAsName = false;

        public GeneralGrid(string name, GridTypes gridType, string fileExtension, ProgressIndication progress, DataGridView grid, ColumnList columns)
        {
            Name = name;
            Progress = progress;
            Grid = grid;
            FileExtension = fileExtension;
            Columns = columns;
            GridType = gridType;
        }

        /// <summary>
        /// Change to new grid
        /// </summary>
        /// <param name="grid">new grid</param>
        public void ChangeGrid(DataGridView grid)
        {
            Grid = grid;
        }

        /// <summary>
        /// Checks if rows are empty, 1 line is also empty
        /// </summary>
        /// <returns>grid is empty</returns>
        public bool IsEmpty()
        {
            if (Grid == null)
                return true;

            return Grid.RowCount <= 1;
        }

        /// <summary>
        /// Clear all grid data
        /// </summary>
        public void GridClear()
        {
            Grid.Rows.Clear();
            Grid.Columns.Clear();

            Grid.DataSource = null;
            Debug debug = new Debug();
            debug.ToFile("Clearing all grid: " + Name, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Add Columns to grid
        /// </summary>
        /// <param name="columnList">list of columns</param>
        private void AddColumns(ColumnList columnList)
        {
            Debug debug = new Debug();
            debug.ToFile("Putting new GeneralColumns(" + columnList.Columns.Count + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            foreach (GeneralColumn _column in columnList.Columns)
            {
                DataGridViewColumn _columnGridView = new DataGridViewColumn()
                {
                    CellTemplate = new DataGridViewTextBoxCell(),
                    Name = _column.Keyword,
                    SortMode = DataGridViewColumnSortMode.Automatic,
                };

                switch (GridType)
                {
                    case GridTypes.Data:
                        _columnGridView.HeaderText = _column.GetColumnName(UseKeywordAsName);
                        break;

                    case GridTypes.DataNoEdit:
                        _columnGridView.HeaderText = _column.GetColumnName(UseKeywordAsName);
                        _columnGridView.SortMode = DataGridViewColumnSortMode.NotSortable;
                        break;

                    case GridTypes.DBForceEdit:
                    case GridTypes.DB:
                    case GridTypes.DBEditable:
                        _columnGridView.HeaderText = _column.Keyword;
                        _columnGridView.SortMode = DataGridViewColumnSortMode.NotSortable;
                        break;

                    default:
                        const string _debugText = "GeneralGrid.AddColumns";
                        Debug _debug = new Debug();
                        _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(GridType), DebugLevels.None, DebugMessageType.Critical);
                        throw new InvalidProgramException(_debugText + "." + nameof(GridType) + " is not created for this element");
                }
                Grid.Columns.Insert(_column.Number, _columnGridView);
            }
        }

        /// <summary>
        /// Get grid columns and add to columns list
        /// </summary>
        /// <returns>grid column list</returns>
        public List<GeneralColumn> GetColumns()
        {
            int _columnNumber;
            int _columnIndex;
            string _keyword;
            bool _canHide;

            Debug debug = new Debug();
            debug.ToFile("Getting columns from grid to memory(" + Grid.ColumnCount + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            List<GeneralColumn> _columnList = new List<GeneralColumn>();
            for (_columnNumber = 0; _columnNumber < Grid.ColumnCount; _columnNumber++)
            {
                _keyword = Grid.Columns[_columnNumber].Name;
                _columnIndex = Grid.Columns[_columnNumber].DisplayIndex;
                _canHide = true;
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
        /// Put data to grid
        /// </summary>
        /// <param name="data">data to put to grid</param>
        public void PutData(List<List<string>> data)
        {
            switch (GridType)
            {
                case GridTypes.Data:
                case GridTypes.DBForceEdit:
                    Grid.AllowUserToAddRows = true;
                    Grid.AllowUserToDeleteRows = true;
                    Grid.ReadOnly = false;
                    break;

                case GridTypes.DB:
                case GridTypes.DBEditable:
                case GridTypes.DataNoEdit:
                    Grid.AllowUserToAddRows = false;
                    Grid.AllowUserToDeleteRows = false;
                    Grid.ReadOnly = true;
                    break;

                default:
                    const string _debugText = "GeneralGrid.PutData";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(GridType), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + nameof(GridType) + " is not created for this element");
            }

            Debug debug = new Debug();
            if (data == null)
            {
                debug.ToPopUp(Resources.PutDataToGrid + ": " + Name + " - " + Resources.NoData, DebugLevels.Development, DebugMessageType.Info);
                return;
            }

            //hide grid to speed up
            Grid.Visible = false;
            Grid.SuspendLayout();

            debug.ToFile(Resources.PutDataToGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);
            GridClear();

            //generate columns list
            if (Columns.Columns.Count == 0)
            {
                List<GeneralColumn> _newColumns = new List<GeneralColumn>();
                //get maximum columns number
                int _maxColumns = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Count > _maxColumns)
                        _maxColumns = data[i].Count;
                }
                for (int _columnNumber = 0; _columnNumber < _maxColumns; _columnNumber++)
                    _newColumns.Add(new GeneralColumn(_columnNumber.ToString(), _columnNumber, false));

                Columns.SetColumns(_newColumns, false);
            }
            AddColumns(Columns);
            AddRows(data.Count);

            Progress.RenameProgressBar(Resources.PutDataToGrid + ": " + Name, data.Count);

            //put data to grid
            for (int _dataIndex = 0; _dataIndex < data.Count; _dataIndex++)
                Grid.Rows[_dataIndex].SetValues(data[_dataIndex].ToArray());

            Grid.ResumeLayout(false);
            Grid.Refresh();
            Grid.Visible = true;
            Grid.AutoResizeColumns();

            debug.ToFile(Resources.PutDataToGrid + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get all data from grid
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>grid data as list of list string</returns>
        public List<List<string>> GetData(bool suppressError)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.GetDataFromGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.GetDataFromGrid + ": " + Name, Grid.Rows.Count);

            int _signalCount = 0;

            List<List<string>> Data = new List<List<string>>();

            for (int _rowNumber = 0; _rowNumber < Grid.Rows.Count; _rowNumber++)
            {
                List<string> _line = new List<string>();

                for (int _column = 0; _column < Grid.ColumnCount; _column++)
                {
                    if (Grid.Rows[_rowNumber].Cells[_column].Value == null)
                        _line.Add(string.Empty);
                    else
                        _line.Add(Grid.Rows[_rowNumber].Cells[_column].Value.ToString());
                }

                Progress.UpdateProgressBar(_rowNumber);
                _signalCount++;
                Data.Add(_line);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.GetDataFromGrid + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (_signalCount == 0 && !suppressError)
                debug.ToPopUp(Resources.NoDataGrid + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return Data;
        }

        /// <summary>
        /// Create save file of current grid to excel with file selection
        /// </summary>
        public void SaveSelect()
        {
            Debug debug = new Debug();

            SaveFileDialog _saveFile = new SaveFileDialog()
            {
                Filter = Name + "|*" + FileExtension,
                AddExtension = false,
            };

            if (_saveFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return;
            }

            SaveToFile(_saveFile.FileName);
        }

        /// <summary>
        /// Create save file of current grid to excel
        /// </summary>
        /// <param name="fileName">file name</param>
        public void SaveToFile(string fileName)
        {
            List<List<string>> _data = GetData(false);
            if (_data == null)
                return;

            string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.SaveDataToFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.SaveDataToFile + ": " + _fileName, _data.Count);

            ExcelWriter _excel = new ExcelWriter(_fileName);
            int _rowOffset = 1;
            int _ColumnNumber;

            switch (GridType)
            {
                //first line in excel is column keywords
                case GridTypes.Data:
                case GridTypes.DataNoEdit:
                    foreach (GeneralColumn _column in Columns)
                    {
                        _ColumnNumber = _column.Number;
                        if (_ColumnNumber >= 0)
                            _excel.Write(_column.Keyword, _ColumnNumber + 1, _rowOffset);
                    }
                    _rowOffset++;
                    break;
                //if DB, do not add columns
                case GridTypes.DB:
                case GridTypes.DBEditable:
                case GridTypes.DBForceEdit:
                    break;

                default:
                    const string _debugText = "GeneralGrid.LoadFromFileToMemory";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(GridType), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + nameof(GridType) + " is not created for this element");
            }

            //write data to file
            for (int _row = 0; _row < _data.Count; _row++)
            {
                for (int _column = 0; _column < _data[_row].Count; _column++)
                    _excel.Write(_data[_row][_column], _column + 1, _row + _rowOffset);

                Progress.UpdateProgressBar(_row);
            }

            _excel.Save();
            _excel.Dispose();

            Progress.HideProgressBar();

            debug.ToFile(Resources.SaveDataToFile + ": " + _fileName + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// load data from excel to grid with file selection
        /// </summary>
        /// <returns>there is valid data</returns>
        public bool LoadSelect()
        {
            Debug debug = new Debug();

            OpenFileDialog _loadFile = new OpenFileDialog()
            {
                Filter = Name + "|*" + FileExtension,
            };

            if (_loadFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return false;
            }
            return LoadFromFile(_loadFile.FileName);
        }

        /// <summary>
        /// load data from excel to memory
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>list of list string array of data</returns>
        public List<List<string>> LoadFromFileToMemory(string fileName)
        {
            string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            List<List<string>> _data = new List<List<string>>();

            if (!File.Exists(_fileName))
            {
                debug.ToPopUp(Resources.NoFile + ": " + _fileName, DebugLevels.None, DebugMessageType.Alarm);
                return null;
            }

            FileStream stream = File.Open(_fileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader _excel = ExcelReaderFactory.CreateReader(stream);

            int _rowCount = _excel.RowCount;
            int _columnCount;
            string _cellValue;
            int _rowOffset = 1;
            List<GeneralColumn> _newColumns = new List<GeneralColumn>();
            Progress.RenameProgressBar(Resources.LoadDataFromFile + ": " + _fileName, _rowCount);

            //get columns names
            switch (GridType)
            {
                //read column keywords in line 1
                case GridTypes.Data:
                case GridTypes.DataNoEdit:
                    _excel.Read();
                    for (int _columnNumber = 0; _columnNumber < _excel.FieldCount; _columnNumber++)
                    {
                        _cellValue = GeneralFunctions.ReadExcelCell(1, _columnNumber, _excel.FieldCount, _excel);
                        if (string.IsNullOrEmpty(_cellValue))
                            break;

                        _newColumns.Add(new GeneralColumn(_cellValue, _columnNumber, false));
                    }
                    Columns.SetColumns(_newColumns, false);
                    _rowOffset++;
                    break;
                //if DB, then columns not in excel then write 0 1 2 3 ...
                case GridTypes.DB:
                case GridTypes.DBEditable:
                case GridTypes.DBForceEdit:
                    for (int _columnNumber = 0; _columnNumber < _excel.FieldCount; _columnNumber++)
                        _newColumns.Add(new GeneralColumn(_columnNumber.ToString(), _columnNumber, false));

                    Columns.SetColumns(_newColumns, false);
                    break;

                default:
                    const string _debugText = "GeneralGrid.LoadFromFileToMemory";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(GridType), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + nameof(GridType) + " is not created for this element");
            }

            //read all excel rows
            for (int _row = _rowOffset; _row <= _rowCount; _row++)
            {
                // if nothing to read exit
                if (!_excel.Read())
                    break;

                _columnCount = _excel.FieldCount;

                List<string> _line = new List<string>();

                for (int _column = 0; _column < _columnCount; _column++)
                    _line.Add(GeneralFunctions.ReadExcelCell(_row, _column, _columnCount, _excel));

                Progress.UpdateProgressBar(_row);

                switch (GridType)
                {
                    //only if Data type then check if all line element are empty
                    case GridTypes.Data:
                    case GridTypes.DataNoEdit:
                        if (!_line.TrueForAll(e => e.Equals(string.Empty)))
                            _data.Add(_line);
                        break;

                    case GridTypes.DB:
                    case GridTypes.DBEditable:
                    case GridTypes.DBForceEdit:
                        _data.Add(_line);
                        break;

                    default:
                        const string _debugText = "GeneralGrid.LoadFromFileToMemory";
                        Debug _debug = new Debug();
                        _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(GridType), DebugLevels.None, DebugMessageType.Critical);
                        throw new InvalidProgramException(_debugText + "." + nameof(GridType) + " is not created for this element");
                }
            }
            _excel.Close();

            Progress.HideProgressBar();
            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (_data.Count == 0)
            {
                debug.ToPopUp(Resources.LoadDataFromFile + ": " + _fileName + " - " + Resources.NoData, DebugLevels.None, DebugMessageType.Info);
                return null;
            }

            return _data;
        }

        /// <summary>
        /// load data from excel to grid
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>data is present</returns>
        public bool LoadFromFile(string fileName)
        {
            List<List<string>> _data = LoadFromFileToMemory(fileName);

            if (_data == null)
                return false;

            PutData(_data);
            return true;
        }

        /// <summary>
        /// Remove columns from grid that are not in base column list
        /// </summary>
        /// <param name="baseColumns">base column list</param>
        public void RemoveNotBaseColumns(List<GeneralColumn> baseColumns)
        {
            bool _found;
            if (GridType == GridTypes.DataNoEdit)
                return;

            for (int _gridColumn = Grid.ColumnCount-1; _gridColumn >=0; _gridColumn--)
            {
                _found = false;
                foreach (GeneralColumn _generalColumn in baseColumns)
                {
                    if (Grid.Columns[_gridColumn].Name != _generalColumn.Keyword)
                        continue;

                    _found = true;
                    break;
                }

                if (_found)
                    continue;

                Grid.Columns.RemoveAt(_gridColumn);
            }
        }

        /// <summary>
        /// Creates DB file
        /// </summary>
        /// <param name="fileName">file to check and create</param>
        /// <param name="additionalFolder">additional folder in DB</param>
        /// <param name="data">data to add to file</param>
        /// <returns>DB files exists</returns>
        public bool CreateFileInDB(string fileName,string additionalFolder, List<List<string>> data)
        {
            string _directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB";

            if (!string.IsNullOrEmpty(additionalFolder))
                _directory += "\\" + additionalFolder;

            string _fileName = _directory + "\\" + fileName + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.CreateNew + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            ExcelWriter _excel = new ExcelWriter(_fileName);

            //write data
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                    _excel.Write(data[i][j], 1 + j, 1+i);
            }
            _excel.Save();
            _excel.Dispose();

            debug.ToFile(Resources.CreateNew + ": " + _fileName + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            return FileExistsInDB(fileName, additionalFolder);
        }

        /// <summary>
        /// Check if database files exists and ask if needed to create
        /// </summary>
        /// <param name="fileName">file to check and create</param>
        /// <returns>DB files exists</returns>
        public bool FileExistsInDB(string fileName, string additionalFolder)
        {
            //get files in folder
            string _directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB";

            if (!string.IsNullOrEmpty(additionalFolder))
                _directory += "\\" + additionalFolder;

 //           string _fileName = _directory + "\\" + fileName + "." + FileExtension;

            string[] _files = System.IO.Directory.GetFiles(_directory, fileName + "." + FileExtension);
            return _files.Length > 0;
        }

        public void ColorCell(int row, int column)
        {
            Grid.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(0, 255, 255);
        }
    }
}