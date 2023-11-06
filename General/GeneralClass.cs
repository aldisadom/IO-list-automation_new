using IO_list_automation_new.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal abstract class GeneralSignal
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

        protected abstract List<GeneralColumn> GeneralGenerateColumnsList();

        private List<GeneralColumn> GenerateColumnsList(bool getFromGrid)
        {
            if (!getFromGrid)
                return GeneralGenerateColumnsList();

            if (Grid.IsEmpty())
                return GeneralGenerateColumnsList();
            else
                return Grid.GetColumns();
        }

        /// <summary>
        /// Update current lists column numbers from new list
        /// </summary>
        /// <param name="newList">new list with new column numbers</param>
        public void UpdateColumnNumbers(List<GeneralColumn> newList)
        {
            string _keyword;
            int _columnNumber;
            bool _canHide;

            List<GeneralColumn> _tmpList = new List<GeneralColumn>();
            for (int _index = 0; _index < BaseColumns.Columns.Count; _index++)
            {
                _keyword = BaseColumns.ElementAt(_index).Keyword;
                _columnNumber = BaseColumns.ElementAt(_index).Number;
                _canHide = BaseColumns.ElementAt(_index).CanHide;

                foreach (GeneralColumn _newColumn in newList)
                {
                    if (_newColumn.Keyword != _keyword)
                        continue;

                    //if in new list column is used and in current list column is not used, add column to end
                    if (_columnNumber == -1 && _newColumn.Number != -1)
                        _columnNumber = 100;
                    break;
                }
                GeneralColumn _column = new GeneralColumn(_keyword, _columnNumber, _canHide);
                _tmpList.Add(_column);
            }

            BaseColumns.Columns.Clear();
            Columns.Columns.Clear();

            for (int _index = 0; _index < _tmpList.Count; _index++)
            {
                GeneralColumn _column = _tmpList[_index];

                BaseColumns.Columns.Add(_column);
                Columns.Columns.Add(_column);
            }

            BaseColumns.SortColumnsList(false);
            Columns.SortColumnsList(true);

            UpdateSettingsColumnsList();
        }

        protected abstract void UpdateSettingsColumnsList();

        public GeneralClass(string name, string fileExtension, ProgressIndication progress, DataGridView grid)
        {
            Name = name;
            Signals = new List<T>();
            Columns = new ColumnList();
            BaseColumns = new ColumnList();
            Progress = progress;

            //            Grid = new GeneralGrid<T>(name, notSortable, fileExtension, Signals, progress, grid, Columns, BaseColumns);
            Grid = new GeneralGrid(name, GridTypes.Data, fileExtension, progress, grid, Columns);

            BaseColumns.SetColumns(GenerateColumnsList(false), false);

            Columns.SetColumns(GenerateColumnsList(true), true);
            Columns.SortColumnsList(true);

            UpdateSettingsColumnsList();
        }

        /// <summary>
        /// Convert signals to list
        /// </summary>
        /// <returns>return list</returns>
        public virtual List<List<string>> SignalsToList()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ConvertDataToList + ": " + Name, Signals.Count);

            int _columnNumber;
            string _keyword;
            string _cellValue;

            //get list of columns that is used
            List<GeneralColumn> _newColumnList = new List<GeneralColumn>();
            foreach (GeneralColumn _column in Columns)
            {
                _columnNumber = _column.Number;
                if (_columnNumber >= 0)
                    _newColumnList.Add(_column);
            }

            List<List<string>> _data = new List<List<string>>();
            for (int _signalNumber = 0; _signalNumber < Signals.Count; _signalNumber++)
            {
                _data.Add(new List<string>(new string[_newColumnList.Count]));
                for (_columnNumber = 0; _columnNumber < _newColumnList.Count; _columnNumber++)
                {
                    //get value based on keyword
                    _keyword = _newColumnList[_columnNumber].Keyword;
                    _cellValue = Signals[_signalNumber].GetValueString(_keyword, false);

                    _data[_signalNumber][_columnNumber] = _cellValue;
                }
                Progress.UpdateProgressBar(_signalNumber);
            }
            Progress.HideProgressBar();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            return _data;
        }

        /// <summary>
        /// Convert list to signals
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>there is data in signals</returns>
        public virtual bool ListToSignals(List<List<string>> inputData, List<GeneralColumn> newColumnList,bool suppressError)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertListToData + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Signals.Clear();

            int _signalCount = 0;
            int _columnNumber;
            string _keyword;
            string _cellValue;
            for (int _rowNumber = 0; _rowNumber < inputData.Count; _rowNumber++)
            {
                T _signal = new T();
                for (int _columnIndex = 0; _columnIndex < newColumnList.Count; _columnIndex++)
                {
                    _columnNumber = newColumnList[_columnIndex].Number;

                    if (_columnNumber < 0)
                        continue;

                    //put value based on keyword to memory
                    _keyword = newColumnList[_columnIndex].Keyword;
                    _cellValue = inputData[_rowNumber][_columnNumber];

                    _signal.SetValueFromString(_cellValue, _keyword);
                }
                Progress.UpdateProgressBar(_rowNumber);

                if (_signal.ValidateSignal())
                {
                    _signalCount++;
                    Signals.Add(_signal);
                }
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ConvertListToData + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            if (_signalCount == 0 && !suppressError)
                debug.ToPopUp(Resources.NoData + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return _signalCount > 0;
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
            debug.ToFile(Resources.PutDataToGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            List<List<string>> _data = SignalsToList();

            Grid.PutData(_data);
            debug.ToFile(Resources.PutDataToGrid + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get all data from grid
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>there is data in grid</returns>
        public bool GetDataFromGrid(bool suppressError)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.GetDataFromGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            bool _validData = ListToSignals(Grid.GetData(suppressError), Grid.GetColumns(), suppressError);

            debug.ToFile(Resources.GetDataFromGrid + ": " + Name + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            return _validData;
        }

        public List<string> GetCPUList()
        {
            bool _found;
            List<string> _CPUList = new List<string>();

            if (Signals.Count == 0)
                return _CPUList;

            //add first signal element CPU to CPU list
            _CPUList.Add(Signals[0].GetValueString(KeywordColumn.CPU, false));

            //find CPU list
            for (int _signalIndex = 0; _signalIndex < Signals.Count; _signalIndex++)
            {
                _found = false;
                for (int _CPUIndex = 0; _CPUIndex < _CPUList.Count; _CPUIndex++)
                {
                    if (_CPUList[_CPUIndex] == Signals[_signalIndex].GetValueString(KeywordColumn.CPU, false))
                    {
                        _found = true;
                        break;
                    }
                }
                if (!_found)
                    _CPUList.Add(Signals[_signalIndex].GetValueString(KeywordColumn.CPU, false));
            }
            return _CPUList;
        }
    }
}