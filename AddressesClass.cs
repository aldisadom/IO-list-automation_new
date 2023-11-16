using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class AddressSingle
    {
        public string Area { get; private set; }
        public string AddressStart { get; private set; }
        public string AddressSize { get; private set; }
        public string ObjectVariableType { get; private set; }

        public bool Overlap;

        public AddressSingle(string area, string addressStart, string addressSize, string objectVariableType)
        {
            Area = area;
            AddressStart = addressStart;
            AddressSize = addressSize;
            ObjectVariableType = objectVariableType;
            Overlap = false;
        }

        public void Update(string area, string addressStart, string addressSize, string objectVariableType)
        {
            Area = area;
            AddressStart = addressStart;
            AddressSize = addressSize;
            ObjectVariableType = objectVariableType;
        }

        public bool CheckOverlapString(AddressSingle address)
        {
            if (address.Area != Area)
                return false;

            if (!int.TryParse(AddressStart, out int myAddress))
                return false;

            if (!int.TryParse(AddressSize, out int myAddressSize))
                return false;

            if (!int.TryParse(address.AddressStart, out int addressStart))
                return false;

            if (!int.TryParse(address.AddressSize, out int addressSize))
                return false;

            if (myAddress < addressStart + addressSize && addressStart < myAddress + myAddressSize)
            {
                Overlap = true;
                return true;
            }
            return false;
        }

        public bool CheckOverlap(string area, int addressStart, int addressSize)
        {
            if (area != Area)
                return false;

            if (!int.TryParse(AddressStart, out int myAddress))
                return false;

            if (!int.TryParse(AddressSize, out int myAddressSize))
                return false;

            if (myAddress < addressStart + addressSize && addressStart < myAddress + myAddressSize)
            {
                Overlap = true;
                return true;
            }
            return false;
        }
    }

    internal class AddressObject : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string ObjectType { get; private set; }
        public string ObjectName { get; private set; }
        public string ObjectGeneralType { get; private set; }

        public List<AddressSingle> Addresses { get; }

        public string UniqueModuleName
        { get { return CPU + "_" + ObjectType + "_" + ObjectName; } }

        public AddressObject() : base()
        {
            Addresses = new List<AddressSingle>();
            ID = string.Empty;
            CPU = string.Empty;
            ObjectType = string.Empty;
            ObjectGeneralType = string.Empty;
            ObjectName = string.Empty;
        }

        /// <summary>
        /// Parsing data from excel or grid, according to Column to signal element
        /// </summary>
        /// <param name="value">value to be passed</param>
        /// <param name="parameterName">parameter to be set</param>
        public override void SetValueFromString(string value, string parameterName)
        {
            switch (parameterName)
            {
                case KeywordColumn.ID:
                    ID = value;
                    break;

                case KeywordColumn.CPU:
                    CPU = value;
                    break;

                case KeywordColumn.ObjectType:
                    ObjectType = value;
                    break;

                case KeywordColumn.ObjectName:
                    ObjectName = value;
                    break;

                case KeywordColumn.ObjectGeneralType:
                    ObjectGeneralType = value;
                    break;
            }
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="suppressError">suppress alarm message, used only for transferring from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public override string GetValueString(string parameterName, bool suppressError)
        {
            switch (parameterName)
            {
                case KeywordColumn.ID:
                    return ID;

                case KeywordColumn.CPU:
                    return CPU;

                case KeywordColumn.ObjectType:
                    return ObjectType;

                case KeywordColumn.ObjectName:
                    return ObjectName;

                case KeywordColumn.ObjectGeneralType:
                    return ObjectGeneralType;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string _debugText = "AddressObject.GetValueString";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + parameterName + " is not created for this element");
            }
        }

        /// <summary>
        /// Checks if design signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            if (string.IsNullOrEmpty(ObjectType))
                return false;
            else if (string.IsNullOrEmpty(ObjectName))
                return false;
            else if (string.IsNullOrEmpty(ObjectGeneralType))
                return false;

            return true;
        }

        public int FindAddressIndex(string objectVariableType)
        {
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].ObjectVariableType == objectVariableType)
                    return i;
            }
            return -1;
        }

        public void Update(string id, string _CPU, string objectGeneralType, string objectType, string objectName)
        {
            ID = id;
            CPU = _CPU;
            ObjectType = objectType;
            ObjectName = objectName;
            ObjectGeneralType = objectGeneralType;
        }

        public List<string> GetColumns()
        {
            List<string> _columns = new List<string>();
            for (int _addressIndex = 0; _addressIndex < Addresses.Count; _addressIndex++)
                _columns.Add(Addresses[_addressIndex].ObjectVariableType);

            return _columns;
        }

        public bool CheckOverlap(AddressSingle address)
        {
            if (!int.TryParse(address.AddressStart, out int addressStart))
                return false;

            if (!int.TryParse(address.AddressSize, out int addressSize))
                return false;

            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].CheckOverlap(address.Area, addressStart, addressSize))
                    return true;
            }
            return false;
        }
    }

    internal class AddressesClass : GeneralClass<AddressObject>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, 0, false),
                new GeneralColumn(KeywordColumn.CPU, 1, false),
                new GeneralColumn(KeywordColumn.ObjectGeneralType, 2,false),
                new GeneralColumn(KeywordColumn.ObjectType, 3,false),
                new GeneralColumn(KeywordColumn.ObjectName, 4, false),
            };
            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
        }

        public AddressesClass(ProgressIndication progress, DataGridView grid) : base("Address", nameof(FileExtensions.address), progress, grid,false)
        {
            Grid.UseKeywordAsName = true;
        }

        public AddressesClass() : base("Address", nameof(FileExtensions.address), null, null,false)
        {
            Grid.UseKeywordAsName = true;
        }

        private void AddColumnToList(List<string> inList, List<string> outList)
        {
            bool _found;
            for (int i = 0; i < inList.Count; i++)
            {
                _found = false;
                for (int j = 0; j < outList.Count; j++)
                {
                    if (inList[i] == outList[j])
                        _found = true;
                }

                if (!_found)
                    outList.Add(inList[i]);
            }
        }

        public List<string> GetColumns()
        {
            List<string> _columns = new List<string>();

            for (int _sigIndex = 0; _sigIndex < Signals.Count; _sigIndex++)
                AddColumnToList(Signals[_sigIndex].GetColumns(), _columns);

            return _columns;
        }

        public override List<List<string>> SignalsToList()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ConvertDataToList + ": " + Name, Signals.Count);

            int _columnNumber;

            //get list of columns that is used
            List<GeneralColumn> _newColumnList = new List<GeneralColumn>();

            Columns.SetColumns(GeneralGenerateColumnsList(), false);
            foreach (GeneralColumn _column in Columns)
            {
                _columnNumber = _column.Number;
                if (_columnNumber >= 0)
                    _newColumnList.Add(_column);
            }

            int _columnIndex = _newColumnList.Count;

            List<string> addressColumns = GetColumns();
            foreach (string _columnName in addressColumns)
            {
                _newColumnList.Add(new GeneralColumn(ResourcesUI.Area + _columnName, _columnIndex, false));
                _newColumnList.Add(new GeneralColumn(ResourcesUI.Start + _columnName, _columnIndex + 1, false));
                _newColumnList.Add(new GeneralColumn(ResourcesUI.Size + _columnName, _columnIndex + 2, false));
                _columnIndex += 3;
            }

            Columns.SetColumns(_newColumnList, false);
            List<List<string>> _data = new List<List<string>>();
            for (int _signalNumber = 0; _signalNumber < Signals.Count; _signalNumber++)
            {
                AddressObject _signal = Signals[_signalNumber];
                _data.Add(new List<string>(new string[_newColumnList.Count]));

                _data[_signalNumber][0] = _signal.ID;
                _data[_signalNumber][1] = _signal.CPU;
                _data[_signalNumber][2] = _signal.ObjectGeneralType;
                _data[_signalNumber][3] = _signal.ObjectType;
                _data[_signalNumber][4] = _signal.ObjectName;
                _columnIndex = BaseColumns.Columns.Count;
                foreach (string _columnName in addressColumns)
                {
                    for (int i = 0; i < _signal.Addresses.Count; i++)
                    {
                        if (_signal.Addresses[i].ObjectVariableType != _columnName)
                            continue;

                        _data[_signalNumber][_columnIndex] = _signal.Addresses[i].Area;
                        _data[_signalNumber][_columnIndex + 1] = _signal.Addresses[i].AddressStart;
                        _data[_signalNumber][_columnIndex + 2] = _signal.Addresses[i].AddressSize;
                        break;
                    }
                    _columnIndex += 3;
                }
                Progress.UpdateProgressBar(_signalNumber);
            }
            Progress.HideProgressBar();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);

            return _data;
        }

        /// <summary>
        /// Convert list to signals
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>there is data in signals</returns>
        public override bool ListToSignals(List<List<string>> inputData, List<GeneralColumn> newColumnList, bool suppressError)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertListToData + ": " + Name, DebugLevels.High, DebugMessageType.Info);

            Signals.Clear();

            int _signalCount = 0;
            int _columnNumber;
            string _keyword;
            string _cellValue;
            string _columnName;
            int _indexText;

            Progress.RenameProgressBar(Resources.ConvertListToData + ": " + Name, inputData.Count);
            for (int _rowNumber = 0; _rowNumber < inputData.Count; _rowNumber++)
            {
                AddressObject _signal = new AddressObject();
                for (int _columnIndex = 0; _columnIndex < BaseColumns.Columns.Count; _columnIndex++)
                {
                    _columnNumber = BaseColumns.Columns[_columnIndex].Number;

                    if (_columnNumber < 0)
                        continue;

                    //put value based on keyword to memory
                    _keyword = BaseColumns.Columns[_columnIndex].Keyword;
                    _cellValue = inputData[_rowNumber][_columnNumber];

                    _signal.SetValueFromString(_cellValue, _keyword);
                }

                for (int _columnIndex = BaseColumns.Columns.Count; _columnIndex < newColumnList.Count; _columnIndex += 3)
                {
                    _columnNumber = newColumnList[_columnIndex].Number;
                    if (string.IsNullOrEmpty(inputData[_rowNumber][_columnNumber]))
                        continue;

                    _indexText = newColumnList[_columnIndex].Keyword.IndexOf("(");
                    _columnName = _indexText == -1 ? string.Empty : newColumnList[_columnIndex].Keyword.Substring(_indexText);

                    AddressSingle _addressSingle = new AddressSingle(inputData[_rowNumber][_columnIndex], inputData[_rowNumber][_columnIndex + 1],
                                                                    inputData[_rowNumber][_columnIndex + 2], _columnName);
                    _signal.Addresses.Add(_addressSingle);
                }

                Progress.UpdateProgressBar(_rowNumber);

                if (_signal.ValidateSignal())
                {
                    _signalCount++;
                    Signals.Add(_signal);
                }
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ConvertListToData + ": " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);

            if (_signalCount == 0 && !suppressError)
                debug.ToPopUp(Resources.NoData + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return _signalCount > 0;
        }

        /// <summary>
        /// Put address data to needed element or create new
        /// </summary>
        /// <param name="cpu">cpu of element</param>
        /// <param name="objectGeneralType">general type of element</param>
        /// <param name="objectType">object type of element</param>
        /// <param name="objectName">object name of element</param>
        /// <param name="objectVariableType">variable name of element</param>
        /// <param name="area">area of element</param>
        /// <param name="addressStart">start address of element</param>
        /// <param name="addressSize">address size of element</param>
        public void PutDataToElement(string cpu, string objectGeneralType, string objectType, string objectName, string objectVariableType, string area, string addressStart, string addressSize)
        {
            if (string.IsNullOrEmpty(addressStart))
                return;

            for (int _signalIndex = 0; _signalIndex < Signals.Count; _signalIndex++)
            {
                if (Signals[_signalIndex].ObjectName != objectName)
                    continue;

                Signals[_signalIndex].SetValueFromString(cpu, KeywordColumn.CPU);
                Signals[_signalIndex].SetValueFromString(objectName, KeywordColumn.ObjectName);
                Signals[_signalIndex].SetValueFromString(objectGeneralType, KeywordColumn.ObjectGeneralType);
                Signals[_signalIndex].SetValueFromString(objectType, KeywordColumn.ObjectType);

                int _indexAddress = Signals[_signalIndex].FindAddressIndex(objectVariableType);
                // found, then update these
                if (_indexAddress != -1)
                    Signals[_signalIndex].Addresses[_indexAddress].Update(area, addressStart, addressSize, objectVariableType);
                //add new address type
                else
                    Signals[_signalIndex].Addresses.Add(new AddressSingle(area, addressStart, addressSize, objectVariableType));
                return;
            }

            AddressObject _objects = new AddressObject();
            _objects.Update(GeneralFunctions.AddZeroes(Signals.Count), cpu, objectGeneralType, objectType, objectName);
            _objects.Addresses.Add(new AddressSingle(area, addressStart, addressSize, objectVariableType));

            Signals.Add(_objects);
        }

        /// <summary>
        /// Check overlap of address
        /// </summary>
        /// <param name="checkIndex">index of address to check overlap</param>
        /// <param name="checkAddressIndex">index of address to check</param>
        /// <returns>overlapped</returns>
        private bool CheckOverlap(int checkIndex, int checkAddressIndex)
        {
            AddressObject _signal = Signals[checkIndex];
            AddressSingle _address = Signals[checkIndex].Addresses[checkAddressIndex];

            for (int i = checkAddressIndex + 1; i < Signals[checkIndex].Addresses.Count; i++)
            {
                if (Signals[checkIndex].Addresses[i].CheckOverlapString(_address))
                    _address.Overlap = true;
            }

            for (int _signalIndex = checkIndex+1; _signalIndex < Signals.Count; _signalIndex++)
            {
                if (_signal.CPU != Signals[_signalIndex].CPU)
                    continue;

                if (Signals[_signalIndex].CheckOverlap(_address))
                    _address.Overlap = true;
            }

            return _address.Overlap;
        }

        public void CheckOverlapAll()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.CheckOverlap + ": " + Name, DebugLevels.High, DebugMessageType.Info);
            Progress.RenameProgressBar(Resources.CheckOverlap + ": " + Name, Signals.Count);
            bool _overlap = false;

            //clear overlap
            for (int _signalIndex = 0; _signalIndex < Signals.Count; _signalIndex++)
            {
                for (int i = 0; i < Signals[_signalIndex].Addresses.Count; i++)
                    Signals[_signalIndex].Addresses[i].Overlap = false;
            }

            for (int _signalIndex = 0; _signalIndex < Signals.Count; _signalIndex++)
            {
                AddressObject _signal = Signals[_signalIndex];

                for (int i = 0; i < _signal.Addresses.Count; i++)
                {
                    if (CheckOverlap(_signalIndex, i))
                        _overlap = true;
                }
                Progress.UpdateProgressBar(_signalIndex);
            }
            if (_overlap)
                ColorOverlap();

            Progress.HideProgressBar();
            debug.ToFile(Resources.CheckOverlap + ": " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }

        private void ColorOverlap()
        {
            Debug debug = new Debug();
            debug.ToFile("Color overlapping addresses: " + Name, DebugLevels.High, DebugMessageType.Info);

            bool _overlap = false;

            for (int _signalIndex = 0; _signalIndex < Signals.Count; _signalIndex++)
            {
                for (int i = 0; i < Signals[_signalIndex].Addresses.Count; i++)
                {
                    if (!Signals[_signalIndex].Addresses[i].Overlap)
                        continue;

                    for (int _column = BaseColumns.Columns.Count; _column < Columns.Columns.Count; _column+=3)
                    {
                        if (Columns.Columns[_column].Keyword != (ResourcesUI.Area +Signals[_signalIndex].Addresses[i].ObjectVariableType))
                            continue;

                        _overlap = true;
                        Grid.ColorCell(_signalIndex, _column);
                        Grid.ColorCell(_signalIndex, _column + 1);
                        Grid.ColorCell(_signalIndex, _column + 2);
                        break;
                    }
                }
            }

            if (_overlap)
                debug.ToPopUp(Resources.MemoryOverlap, DebugLevels.None, DebugMessageType.Warning);

            debug.ToFile("Color overlapping addresses: " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }
    }
}