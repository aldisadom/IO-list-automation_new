using ExcelDataReader;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System.Data;
using System.IO;
using System.Windows.Forms;

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
                debug.ToFile(Resources.Data_ReadFailBounds + " " + Resources.Column + " " + col + " max(" + maxCol + ")" + Resources.Row + " " + row, DebugLevels.Minimum, DebugMessageType.Warning);
                return string.Empty;
            }

            System.Type type = excel.GetFieldType(col);
            if (type == null)
                return string.Empty;

            if (type.Name == "String")
                return excel.GetString(col);
            else if (type.Name == "Double")
                return excel.GetDouble(col).ToString();

            debug.ToPopUp(Resources.Data_ReadFailFormat + ": " + type.Name, DebugLevels.None, DebugMessageType.Critical);
            return string.Empty;
        }

        /// <summary>
        /// Create save file of current grid to excel with file selection
        /// </summary>
        public void SaveSelect(DataTable data)
        {
            Debug debug = new Debug();
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = Name + "|*" + FileExtension,
                AddExtension = false,
            };

            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return;
            }

            SaveToFile(saveFile.FileName, data);
        }

        /// <summary>
        /// Create save file of current grid to excel
        /// </summary>
        /// <param name="fileName">file name</param>
        public void SaveToFile(string fileName, DataTable data)
        {
            if (!GeneralFunctions.ValidDataTable(data))
                return;

            string fullPath = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.SaveDataToFile + ": " + fullPath, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.SaveDataToFile + ": " + fullPath, data.Rows.Count);

            ExcelWriter excel = new ExcelWriter(fullPath);
            int rowOffset = 2;
            string cellValue;

            for (int column = 0; column < data.Columns.Count; column++)
                excel.Write(data.Columns[column].ColumnName, column + 1, 1);

            //write data to file
            for (int row = 0; row < data.Rows.Count; row++)
            {
                for (int column = 0; column < data.Columns.Count; column++)
                {
                    if (data.Rows[row][column] is System.DBNull)
                        cellValue = string.Empty;
                    else
                    {
                        cellValue = GeneralFunctions.GetDataTableValue(data, row, column);
                        if (string.IsNullOrEmpty(cellValue))
                            cellValue = string.Empty;
                    }
                    excel.Write(cellValue, column + 1, row + rowOffset);
                }

                Progress.UpdateProgressBar(row);
            }
            excel.Save();
            excel.Dispose();
            Progress.HideProgressBar();

            debug.ToFile(Resources.SaveDataToFile + ": " + fullPath + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// load data from excel to grid with file selection
        /// </summary>
        /// <returns>there is valid data</returns>
        public DataTable LoadSelect()
        {
            Debug debug = new Debug();

            OpenFileDialog loadFile = new OpenFileDialog()
            {
                Filter = Name + "|*" + FileExtension,
            };

            if (loadFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return null;
            }
            return LoadFromFile(loadFile.FileName);
        }

        /// <summary>
        /// load data from excel to memory
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>list of list string array of data</returns>
        public DataTable LoadFromFile(string fileName)
        {
            string fullPath = System.IO.Path.ChangeExtension(fileName, null) + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.LoadDataFromFile + ": " + fullPath, DebugLevels.Development, DebugMessageType.Info);
            DataTable data = new DataTable();

            if (!File.Exists(fullPath))
            {
                debug.ToPopUp(Resources.NoFile + ": " + fullPath, DebugLevels.None, DebugMessageType.Alarm);
                return null;
            }

            FileStream stream = File.Open(fullPath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excel = ExcelReaderFactory.CreateReader(stream);

            int rowCount = excel.RowCount;
            int columnCount = excel.FieldCount;
            Progress.RenameProgressBar(Resources.LoadDataFromFile + ": " + fullPath, rowCount);

            //add columns to dataTable
            for (int column = 0; column < columnCount; column++)
                data.Columns.Add(column.ToString());

            //read all excel rows
            for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++)
            {
                // if nothing to read exit
                if (!excel.Read())
                    break;

                DataRow row = data.NewRow();
                for (int column = 0; column < columnCount; column++)
                    row[column] = ReadExcelCell(rowIndex, column, columnCount, excel);

                data.Rows.Add(row);

                Progress.UpdateProgressBar(rowIndex);
            }
            excel.Close();

            Progress.HideProgressBar();
            debug.ToFile(Resources.LoadDataFromFile + ": " + fullPath + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (data.Rows.Count == 0)
            {
                if (data.Columns.Count == 0)
                    data.Columns.Add();

                if (data.Rows.Count == 0)
                    data.Rows.Add();
                debug.ToPopUp(Resources.LoadDataFromFile + ": " + fullPath + " - " + Resources.NoData, DebugLevels.None, DebugMessageType.Info);
            }

            return data;
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
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB";

            if (!string.IsNullOrEmpty(additionalFolder))
                directory += "\\" + additionalFolder;

            string fullPath = directory + "\\" + fileName + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.CreateNew + ": " + fullPath, DebugLevels.Development, DebugMessageType.Info);

            ExcelWriter excel = new ExcelWriter(fullPath);

            //write data
            for (int row = 0; row < data.Rows.Count; row++)
            {
                for (int column = 0; column < data.Columns.Count; column++)
                    excel.Write(GeneralFunctions.GetDataTableValue(data, row, column), 1 + column, 1 + row);
            }
            excel.Save();
            excel.Dispose();

            debug.ToFile(Resources.CreateNew + ": " + fullPath + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            return FileExistsInDB(fullPath, additionalFolder);
        }

        /// <summary>
        /// Check if database files exists and ask if needed to create
        /// </summary>
        /// <param name="fileName">file to check and create</param>
        /// <returns>DB files exists</returns>
        public bool FileExistsInDB(string fileName, string additionalFolder)
        {
            //get files in folder
            string directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB";

            if (!string.IsNullOrEmpty(additionalFolder))
                directory += "\\" + additionalFolder;

            string[] files = System.IO.Directory.GetFiles(directory, fileName + "." + FileExtension);
            return files.Length > 0;
        }
    }
}