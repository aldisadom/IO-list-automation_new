using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    public abstract class GeneralSignal
    {
        /// <summary>
        /// Parsing data from excel or grid, according to Column to signal element
        /// </summary>
        /// <param name="value">value to be passed</param>
        /// <param name="parameterName">parameter to be set</param>
        public abstract void SetValueFromString(string value, string parameterName);

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns>value of parameter</returns>
        public abstract string GetValueString(string parameterName, bool suppressError);

        /// <summary>
        /// Checks if signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public abstract bool ValidateSignal();
    }

    internal abstract class GeneralClass<T>
    where T : GeneralSignal, new()
    {
        public string Name { get; }

        public List<T> Signals;

        //columns in software
        public ColumnList Columns { get; set; }

        public ColumnList BaseColumns { get; set; }

        public ProgressIndication Progress { get; set; }

        public GeneralGrid Grid { get; set; }

        public ExcelFiles File { get; set; }

        protected abstract ColumnList GeneralGenerateColumnsList();

        private ColumnList GenerateColumnsList(bool getFromGrid)
        {
            if (!getFromGrid)
                return GeneralGenerateColumnsList();

            if (Grid.IsEmpty())
                return GeneralGenerateColumnsList();
            else
                return Grid.GetColumns();
        }

        private void GetColumnsFromData(DataTable inputData)
        {
            if (inputData == null)
                return;

            switch (Grid.GridType)
            {
                case GridTypes.Data:
                case GridTypes.DataNoEdit:
                    //first line is columns keyword
                    ColumnList columns = new ColumnList();
                    bool canHide = false;
                    bool found = false;

                    for (int column = inputData.Columns.Count - 1; column >= 0; column--)
                    {
                        foreach (var baseColumn in BaseColumns.Columns)
                        {
                            if (GeneralFunctions.GetDataTableValue(inputData, 0, column) == baseColumn.Key)
                            {
                                found = true;
                                canHide = baseColumn.Value.CanHide;
                                break;
                            }
                        }
                        if (found)
                            columns.Columns.Add(GeneralFunctions.GetDataTableValue(inputData, 0, column),new GeneralColumnParameters(column, canHide));
                        else
                            inputData.Columns.RemoveAt(column);
                    }
                    inputData.Rows[0].Delete();
                    Columns.SetColumns(columns, true);
                    break;
            }
        }

        /// <summary>
        /// Update current lists column numbers from new list
        /// </summary>
        /// <param name="newList">new list with new column numbers</param>
        public void UpdateColumnNumbers(ColumnList newList)
        {
            string keyword;
            int columnNumber;
            bool canHide;

            ColumnList tmpList = new ColumnList();
            foreach (var baseColumn in BaseColumns.Columns)
            {
                keyword = baseColumn.Key;
                columnNumber = baseColumn.Value.NR;
                canHide = baseColumn.Value.CanHide;

                foreach (var newColumn in newList.Columns)
                {
                    if (newColumn.Key != keyword)
                        continue;

                    //if in new list column is used and in current list column is not used, add column to end
                    if (columnNumber == -1 && newColumn.Value.NR != -1)
                        columnNumber = 100;
                    break;
                }
                tmpList.Columns.Add(keyword, new GeneralColumnParameters(columnNumber, canHide));
            }

            BaseColumns.Columns.Clear();
            Columns.Columns.Clear();

            foreach (var column in tmpList.Columns)
            {
                BaseColumns.Columns.Add(column.Key, column.Value);
                Columns.Columns.Add(column.Key, column.Value);
            }

            BaseColumns.SortColumns(false);
            Columns.SortColumns(true);

            UpdateSettingsColumnsList();
        }

        protected abstract void UpdateSettingsColumnsList();

        public GeneralClass(string name, string fileExtension, ProgressIndication progress, DataGridView grid, bool writableGrid)
        {
            Name = name;
            Signals = new List<T>();
            Columns = new ColumnList();
            BaseColumns = new ColumnList();
            Progress = progress;

            GridTypes gridType = writableGrid ? GridTypes.Data : GridTypes.DataNoEdit;
            File = new ExcelFiles(name, fileExtension, progress);
            Grid = new GeneralGrid(name, gridType, grid, Columns);
            BaseColumns.SetColumns(GenerateColumnsList(false), false);

            Columns.SetColumns(GenerateColumnsList(true), true);
            Columns.SortColumns(true);

            UpdateSettingsColumnsList();
        }

        /// <summary>
        /// Convert signals to list
        /// </summary>
        /// <returns>return list</returns>
        public virtual DataTable SignalsToList()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ConvertDataToList + ": " + Name, Signals.Count);

            //get list of columns that is used
            ColumnList newColumnList = new ColumnList();
            foreach (var column in Columns.Columns)
            {
                if (column.Value.NR >= 0)
                    newColumnList.Columns.Add(column.Key, column.Value);
            }

            DataTable data = new DataTable();
            //add columns to dataTable
            foreach (var column in Columns.Columns)
                data.Columns.Add(column.Key);

            int progress = 0;
            foreach (T signal in Signals)
            {
                DataRow row = data.NewRow();

                foreach (var column in newColumnList.Columns)
                    row[column.Value.NR] = signal.GetValueString(column.Key, false);

                data.Rows.Add(row);
                progress++;
                Progress.UpdateProgressBar(progress);
            }
            Progress.HideProgressBar();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            return data;
        }

        /// <summary>
        /// Convert list to signals
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>there is data in signals</returns>
        public virtual bool ListToSignals(DataTable inputData, ColumnList newColumnList, bool suppressError)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertListToData + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Signals.Clear();

            int signalCount = 0;
            int columnNumber;
            string keyword;
            string cellValue;
            for (int rowNumber = 0; rowNumber < inputData.Rows.Count; rowNumber++)
            {
                T signal = new T();
                foreach(var newColumn in newColumnList.Columns)
                {
                    columnNumber = newColumn.Value.NR;

                    if (columnNumber < 0)
                        continue;

                    //put value based on keyword to memory
                    keyword = newColumn.Key;
                    cellValue = GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber);

                    signal.SetValueFromString(cellValue, keyword);
                }

                Progress.UpdateProgressBar(rowNumber);

                if (signal.ValidateSignal())
                {
                    signalCount++;
                    Signals.Add(signal);
                }
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ConvertListToData + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (signalCount == 0 && !suppressError)
                debug.ToPopUp(Resources.NoData + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return signalCount > 0;
        }

        /// <summary>
        /// Put all data to grid
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        public void PutDataToGrid(bool suppressError)
        {
            Debug debug = new Debug();
            if (Signals.Count == 0)
            {
                if (!suppressError)
                    debug.ToPopUp(Resources.PutDataToGrid + ": " + Name + " - " + Resources.NoData, DebugLevels.Development, DebugMessageType.Info);

                return;
            }
            DataTable data = SignalsToList();

            Grid.PutData(data);
        }

        /// <summary>
        /// Get all data from grid
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>there is data in grid</returns>
        public bool GetDataFromGrid(bool suppressError)
        {
            return ListToSignals(Grid.GetData(), Grid.GetColumns(), suppressError);
        }

        /// <summary>
        /// Get cpu list
        /// </summary>
        /// <returns>list of CPU</returns>
        public List<string> GetCPUList()
        {
            bool found;
            List<string> cpuList = new List<string>();

            if (Signals.Count == 0)
                return cpuList;

            //add first signal element CPU to CPU list
            cpuList.Add(Signals[0].GetValueString(KeywordColumn.CPU, false));

            //find CPU list
            foreach (T signal in Signals)
            {
                found = false;
                foreach (string cpu in cpuList)
                {
                    if (cpu == signal.GetValueString(KeywordColumn.CPU, false))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    cpuList.Add(signal.GetValueString(KeywordColumn.CPU, false));
            }
            return cpuList;
        }

        /// <summary>
        /// Load saved file and remove columns that are not in base columns, to correctly load files
        /// </summary>
        /// <param name="fileName">file name to load</param>
        /// <returns>there is data to load</returns>
        public bool LoadFromFile(string fileName)
        {
            DataTable data = File.LoadFromFile(fileName);
            GetColumnsFromData(data);
            return LoadToGrid(data);
        }

        /// <summary>
        /// Load saved selected file and remove columns that are not in base columns, to correctly load files
        /// </summary>
        /// <returns>there is data to load</returns>
        public bool LoadSelect()
        {
            DataTable data = File.LoadSelect();
            GetColumnsFromData(data);
            return LoadToGrid(data);
        }

        /// <summary>
        /// Put loaded data to grid
        /// </summary>
        /// <param name="data"></param>
        /// <returns>there is data in grid</returns>
        private bool LoadToGrid(DataTable data)
        {
            if (data == null)
                return false;

            if (data.Rows.Count == 0)
                return false;

            Grid.PutData(data);
            Grid.RemoveNotBaseColumns(BaseColumns);
            UpdateSettingsColumnsList();

            return true;
        }

        /// <summary>
        /// Create save file of current grid to excel
        /// </summary>
        /// <param name="fileName">file name</param>
        public void SaveToFile(string fileName)
        {
            File.SaveToFile(fileName, Grid.GetData());
        }

        /// <summary>
        /// Create save file of current grid to excel with file selection
        /// </summary>
        public void SaveSelect()
        {
            File.SaveSelect(Grid.GetData());
        }
    }
}