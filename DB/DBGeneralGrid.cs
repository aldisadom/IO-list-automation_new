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
using System.Xml.Linq;

namespace IO_list_automation_new
{
    internal class DBGeneralGrid
    {
        private string Name { get; }
        private string SheetName { get; }
        public string FileExtension { get; }
        public string FilePaths { get; }
        public bool Editable { get; set; }

        private ProgressIndication Progress { get; set; }

        private DataGridView Grid { get; set; }

        public DBGeneralGrid(string name,string filePath, string sheetName, string fileExtension, ProgressIndication progress, DataGridView grid, bool editable)
        {
            Name = name;
            SheetName = sheetName;
            Progress = progress;
            Grid = grid;
            FileExtension = fileExtension;
            FilePaths = filePath;
            Editable = editable;
        }

        public void ChangeGrid(DataGridView grid)
        {
            Grid = grid;
        }

        /// <summary>
        /// Cheacks if rows are empty, 1 line is also empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
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
        /// Get all data from grid
        /// </summary>
        /// <returns>grid data as list list string</returns>
        public List<List<string>> GetData()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.GetDataFromGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.GetDataFromGrid + ": " + Name, Grid.Rows.Count);

            int _signalCount = 0;

            List<List<string>> Data = new List<List<string>>();

            for (int _rowNumber = 0; _rowNumber < Grid.Rows.Count; _rowNumber++)
            {
                DBGeneralInstanceSignal _data = new DBGeneralInstanceSignal();
                List<string> _line = new List<string>();

                for (int _column = 0; _column < Grid.ColumnCount; _column++)
                    _line.Add(Grid.Rows[_rowNumber].Cells[_column].Value.ToString());

                Progress.UpdateProgressBar(_rowNumber);

                Data.Add(_line);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.GetDataFromGrid + ": " + Name + " - finished", DebugLevels.Development, DebugMessageType.Info);

            if (_signalCount == 0)
                debug.ToPopUp(Resources.NoDataGrid + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return Data;
        }

        /// <summary>
        /// Put data to grid
        /// </summary>
        /// <param name="data">data to put to grid</param>
        public void PutData(List<List<string>> data)
        {
            Grid.AllowUserToAddRows = Editable;
            Grid.AllowUserToDeleteRows = Editable;
            Grid.ReadOnly = !Editable;

            Debug debug = new Debug();
            if (data != null)
            {
                //hide grid to speed up
                Grid.Visible = false;
                Grid.SuspendLayout();

                debug.ToFile(Resources.PutDataToGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);
                GridClear();

                Progress.RenameProgressBar(Resources.PutDataToGrid + ": " + Name, data.Count());
                DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();

                //put data to grid
                for (int i = 0; i < data.Count; i++)
                {
                    if (Grid.ColumnCount < data[i].Count)
                    {
                        for (int j = Grid.ColumnCount; j < data[i].Count; j++)
                        {
                            DataGridViewColumn _columnGridView = new DataGridViewColumn();
                            _columnGridView.CellTemplate = _cell;
                            _columnGridView.SortMode = DataGridViewColumnSortMode.NotSortable;
                            _columnGridView.Name = j.ToString();
                            _columnGridView.HeaderText = j.ToString();

                            Grid.Columns.Insert(j, _columnGridView);
                        }
                    }
                    Grid.Rows.Add();

                    string[] _row = new string[data[i].Count];
                    for (int j = 0; j < data[i].Count; j++)
                        _row[j] = data[i][j];

                    Grid.Rows[i].SetValues(_row);
                }

                Grid.ResumeLayout();
                Grid.AutoResizeColumns();
                Grid.Visible = true;

                debug.ToFile(Resources.PutDataToGrid + ": " + Name + " - finished", DebugLevels.Development, DebugMessageType.Info);
            }
            else
                debug.ToPopUp(Resources.PutDataToGrid + ": " + Name + " - " + Resources.NoData, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get excel cell data, uses general function and always return string
        /// </summary>
        /// <param name="row">current row</param>
        /// <param name="col">column to read</param>
        /// <param name="maxCol">maximum columns in row</param>
        /// <param name="excel">opened excel file</param>
        /// <returns>string value of cell value</returns>
        private string ReadExcelCell(int row, int col, int maxCol, IExcelDataReader excel)
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
        /// Create save file of current grid to excel
        /// </summary>
        public void SaveToFile()
        {
            List<List<string>> _data = GetData();
            if (_data != null)
            {
                string _fileName = FilePaths + FileExtension;

                Debug debug = new Debug();
                debug.ToFile(Resources.SaveDataToFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

                Progress.RenameProgressBar(Resources.SaveDataToFile + ": " + _fileName, _data.Count());

                Sheet _sheet = new Sheet();
                _sheet.Name = SheetName;
                ExcelWriter _excel = new ExcelWriter(_fileName, _sheet);

                for (int _row = 0; _row < _data.Count(); _row++)
                {
                    for (int _column = 0; _column < _data[_row].Count(); _column++)
                        _excel.Write(_data[_row][_column], _column + 1, _row + 1);

                    Progress.UpdateProgressBar(_row);
                }

                _excel.Save();
                _excel.Dispose();

                Progress.HideProgressBar();

                debug.ToFile(Resources.SaveDataToFile + ": " + _fileName + " - finished", DebugLevels.Development, DebugMessageType.Info);
            }
        }

        /// <summary>
        /// load data from excel to memory
        /// </summary>
        /// <returns>list list string array of data</returns>
        public List<List<string>> LoadFromFileToMemory()
        {
            string _fileName = System.IO.Path.ChangeExtension(FilePaths, null) + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            List<List<string>> _data = new List<List<string>>();

            if (File.Exists(_fileName))
            {
                FileStream stream = File.Open(_fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader _excel = ExcelReaderFactory.CreateReader(stream);

                int _rowCount = _excel.RowCount;
                int _columnCount = 0;
                Progress.RenameProgressBar(Resources.LoadDataFromFile + ": " + _fileName, _rowCount);

                for (int i = 0; i < _excel.ResultsCount; i++)
                {
                    if (_excel.Name == SheetName)
                    {
                        //read all excel rows
                        for (int _row = 1; _row <= _rowCount; _row++)
                        {
                            // if nothing to read exit
                            if (!_excel.Read())
                                break;

                            _columnCount = _excel.FieldCount;

                            List<string> _line = new List<string>();

                            for (int _column = 0; _column < _columnCount; _column++)
                                _line.Add(ReadExcelCell(_row, _column, _columnCount, _excel));

                            Progress.UpdateProgressBar(_row);

                            _data.Add(_line);
                        }
                        break;
                    }
                    else
                    {
                        if (!_excel.NextResult())
                            break;
                    }
                }
                _excel.Close();

                Progress.HideProgressBar();
            }
            else
            {
                debug.ToPopUp(Resources.NoFile + ": " + _fileName, DebugLevels.None, DebugMessageType.Alarm);
                return null;
            }

            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName + " - finished", DebugLevels.Development, DebugMessageType.Info);

            if (_data.Count > 0)
                return _data;
            else
            {
                debug.ToPopUp(Resources.LoadDataFromFile + ": " + _fileName + " - " + Resources.NoData, DebugLevels.None, DebugMessageType.Info);
                return null;
            }
        }

        /// <summary>
        /// load data from excel to grid
        /// </summary>
        public bool LoadFromFile()
        {
            List<List<string>> _data = LoadFromFileToMemory();
            if (_data != null)
            {
                PutData(_data);
                return true;
            }
            else
                return false;
        }
    }
}
