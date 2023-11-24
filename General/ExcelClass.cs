using ExcelDataReader;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SharpCompress.Common;
using SwiftExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IO_list_automation_new
{
    internal class ExcelFiles
    {
        private string Name { get; }
        public string FileExtension { get; }
        private ProgressIndication Progress { get; }

        public ExcelFiles(string name, string fileExtension, ProgressIndication progress)
        {
            Name = name;
            FileExtension = fileExtension;
            Progress = progress;
        }

        /// <summary>
        /// Get excel cell data, uses general function and always return string
        /// </summary>
        /// <param name="row">current row</param>
        /// <param name="col">column to read</param>
        /// <param name="maxCol">maximum columns in row</param>
        /// <param name="excel">opened excel file</param>
        /// <returns>string value of cell value</returns>
        public static string ReadExcelCell(int row, int col, int maxCol, IExcelDataReader excel)
        {
            Debug debug = new Debug();
            if (col >= maxCol || col < 0)
            {
                debug.ToFile(Resources.DataReadFailBounds + " " + Resources.Column + " " + col + " max(" + maxCol + ")" + Resources.Row + " " + row, DebugLevels.Minimum, DebugMessageType.Warning);
                return string.Empty;
            }

            System.Type _type = excel.GetFieldType(col);
            if (_type == null)
                return string.Empty;

            if (_type.Name == "String")
                return excel.GetString(col);
            else if (_type.Name == "Double")
                return excel.GetDouble(col).ToString();

            debug.ToPopUp(Resources.DataReadFailFormat + ": " + _type.Name, DebugLevels.None, DebugMessageType.Critical);
            return string.Empty;
        }

        /// <summary>
        /// Create save file of current grid to excel with file selection
        /// </summary>
        public void SaveSelect(DataTable data)
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

            SaveToFile(_saveFile.FileName, data);
        }

        /// <summary>
        /// Create save file of current grid to excel
        /// </summary>
        /// <param name="fileName">file name</param>
        public void SaveToFile(string fileName, DataTable data)
        {
            if (!GeneralFunctions.ValidDataTable(data))
                return;

            string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.SaveDataToFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.SaveDataToFile + ": " + _fileName, data.Rows.Count);

            ExcelWriter _excel = new ExcelWriter(_fileName);
            int _rowOffset = 1;
            string _cellValue;

            //write data to file
            for (int _row = 0; _row < data.Rows.Count; _row++)
            {
                for (int _column = 0; _column < data.Columns.Count; _column++)
                {
                    if (data.Rows[_row][_column] is System.DBNull)
                        _cellValue = string.Empty;
                    else
                    {
                        _cellValue = GeneralFunctions.GetDataTableValue(data, _row, _column);
                        if (string.IsNullOrEmpty(_cellValue))
                            _cellValue = string.Empty;
                    }
                    _excel.Write(_cellValue, _column + 1, _row + _rowOffset);
                }

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
        public DataTable LoadSelect()
        {
            Debug debug = new Debug();

            OpenFileDialog _loadFile = new OpenFileDialog()
            {
                Filter = Name + "|*" + FileExtension,
            };

            if (_loadFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return null;
            }
            return LoadFromFile(_loadFile.FileName);
        }

        /// <summary>
        /// load data from excel to memory
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>list of list string array of data</returns>
        public DataTable LoadFromFile(string fileName)
        {
            string _fileName = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);
            DataTable _data = new DataTable();

            if (!File.Exists(_fileName))
            {
                debug.ToPopUp(Resources.NoFile + ": " + _fileName, DebugLevels.None, DebugMessageType.Alarm);
                return null;
            }

            FileStream stream = File.Open(_fileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader _excel = ExcelReaderFactory.CreateReader(stream);

            int _rowCount = _excel.RowCount;
            int _columnCount;
            Progress.RenameProgressBar(Resources.LoadDataFromFile + ": " + _fileName, _rowCount);

            //add columns to dataTable
            for (int _column = 0; _column < _excel.FieldCount; _column++)
                _data.Columns.Add(_column.ToString());

            //read all excel rows
            for (int _row = 1; _row <= _rowCount; _row++)
            {
                // if nothing to read exit
                if (!_excel.Read())
                    break;

                _columnCount = _excel.FieldCount;

                DataRow row = _data.NewRow();
                for (int _column = 0; _column < _columnCount; _column++)
                    row[_column] = ReadExcelCell(_row, _column, _columnCount, _excel);

                _data.Rows.Add(row);

                Progress.UpdateProgressBar(_row);
            }
            _excel.Close();

            Progress.HideProgressBar();
            debug.ToFile(Resources.LoadDataFromFile + ": " + _fileName + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (_data.Rows.Count == 0)
            {
                debug.ToPopUp(Resources.LoadDataFromFile + ": " + _fileName + " - " + Resources.NoData, DebugLevels.None, DebugMessageType.Info);
                return null;
            }

            return _data;
        }

        /// <summary>
        /// Creates DB file
        /// </summary>
        /// <param name="fileName">file to check and create</param>
        /// <param name="additionalFolder">additional folder in DB</param>
        /// <param name="data">data to add to file</param>
        /// <returns>DB files exists</returns>
        public bool CreateFileInDB(string fileName, string additionalFolder, DataTable data)
        {
            string _directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB";

            if (!string.IsNullOrEmpty(additionalFolder))
                _directory += "\\" + additionalFolder;

            string _fileName = _directory + "\\" + fileName + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.CreateNew + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            ExcelWriter _excel = new ExcelWriter(_fileName);

            //write data
            for (int row = 0; row < data.Rows.Count; row++)
            {
                for (int column = 0; column < data.Columns.Count; column++)
                    _excel.Write(GeneralFunctions.GetDataTableValue(data, row, column), 1 + column, 1 + row);
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

            string[] _files = System.IO.Directory.GetFiles(_directory, fileName + "." + FileExtension);
            return _files.Length > 0;
        }
    }
}
