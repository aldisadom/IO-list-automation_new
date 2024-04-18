using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal abstract class GeneralClass<T>
    where T : GeneralSignal, new()
    {
        public string Name { get; }
        public List<T> Signals;

        //columns in software
        public ColumnList Columns { get; set; }
        public ProgressIndication Progress { get; set; }
        public GeneralGrid Grid { get; set; }
        public ExcelFiles File { get; set; }

        public GeneralClass(string name, string fileExtension, ProgressIndication progress, DataGridView grid, bool writableGrid)
        {
            Name = name;
            Signals = new List<T>();
            InitialCollumnList();
            Progress = progress;

            GridTypes gridType = writableGrid ? GridTypes.Data : GridTypes.DataNoEdit;
            File = new ExcelFiles(name, fileExtension, progress);

            Columns.LoadColumnsParameters();
            Grid = new GeneralGrid(name, gridType, grid, Columns);

            Columns.SaveColumnsParameters();
        }

        protected abstract void InitialCollumnList();

        private void GetColumnsFromData(DataTable inputData)
        {
            if (inputData == null)
                return;

            Debug debug = new Debug();

            switch (Grid.GridType)
            {
                case GridTypes.Data:
                case GridTypes.DataNoEdit:
                    for (int columnNumber = inputData.Columns.Count - 1; columnNumber >= 0; columnNumber--)
                    {
                        string columnKey = GeneralFunctions.GetDataTableValue(inputData, 0, columnNumber);

                        inputData.Columns[columnNumber].ColumnName = columnKey;
                        if (Columns.Columns.TryGetValue(columnKey, out ColumnParameters columnParameters))
                        {
                            columnParameters.Hidden = false;
                            columnParameters.NR = columnNumber;
                        }
                        else
                            debug.ToFile($"{Resources.DeleteMe}: {Name} {columnKey} is incorrect", DebugLevels.Minimum, DebugMessageType.Warning);
                    }

                    bool add;
                    //search if all mandatory columns if they are shown
                    foreach (var column in Columns.Columns)
                    {
                        if (column.Value.CanHide)
                            continue;

                        add = true;
                        column.Value.Hidden = false;
                        for (int columnNumber = inputData.Columns.Count - 1; columnNumber >= 0; columnNumber--)
                        {
                            if (inputData.Columns[columnNumber].ColumnName == column.Key)
                            {
                                add = false;
                                break;
                            }
                        }
                        if (add)
                        {
                            column.Value.NR = inputData.Columns.Count;
                            inputData.Columns.Add(column.Key);
                        }
                    }
                    inputData.Rows[0].Delete();
                    break;
            }
        }

        /// <summary>
        /// Convert signals to list
        /// </summary>
        /// <returns>return list</returns>
        public virtual DataTable SignalsToDataTable()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ConvertDataToList + ": " + Name, Signals.Count);

            Dictionary<string, int> sortedList = new Dictionary<string, int>();
            foreach (var column in Columns.Columns)
            {
                if (!column.Value.Hidden)
                    sortedList.Add(column.Key, column.Value.NR);
            }

            sortedList = sortedList.OrderBy(c => c.Value).ToDictionary(c => c.Key, c => c.Value);

            DataTable data = new DataTable();
            for (int i = 0; i < sortedList.Count; i++)
            {
                data.Columns.Add(sortedList.ElementAt(i).Key);
                sortedList[sortedList.ElementAt(i).Key] = i;
            }

            int progress = 0;
            foreach (T signal in Signals)
            {
                DataRow row = data.NewRow();

                foreach (var column in sortedList)
                    row[column.Value] = signal.GetValueString(column.Key, false);

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
        public virtual bool DataTableToSignals(DataTable inputData, bool suppressError)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertListToData + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Signals.Clear();

            int signalCount = 0;
            string cellValue;
            for (int rowNumber = 0; rowNumber < inputData.Rows.Count; rowNumber++)
            {
                T signal = new T();
                foreach (var column in Columns.Columns)
                {
                    if (column.Value.Hidden)
                        continue;

                    cellValue = GeneralFunctions.GetDataTableValue(inputData, rowNumber, column.Value.NR);

                    signal.SetValueFromString(cellValue, column.Key);
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
            DataTable data = SignalsToDataTable();

            Grid.PutData(data);
        }

        /// <summary>
        /// Get all data from grid
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>there is data in grid</returns>
        public bool GetDataFromGrid(bool suppressError)
        {
            Grid.UpdateColumnsList();
            return DataTableToSignals(Grid.GetData(), suppressError);
        }

        /// <summary>
        /// Get cpu list
        /// </summary>
        /// <returns>list of CPU</returns>
        public List<string> GetCPUList()
        {
            return Signals.Select(s => s.GetValueString(KeywordColumn.CPU, false)).Distinct().ToList();
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

            if (data.Columns.Count == 0)
                return false;

            if (data.Rows.Count == 0)
                return false;

            Grid.PutData(data);
            Columns.SaveColumnsParameters();

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