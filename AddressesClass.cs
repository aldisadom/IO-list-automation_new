﻿using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
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

                    const string debugText = "AddressObject.GetValueString";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + parameterName + " is not created for this element");
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

        public void Update(string id, string inCPU, string objectGeneralType, string objectType, string objectName)
        {
            ID = id;
            CPU = inCPU;
            ObjectType = objectType;
            ObjectName = objectName;
            ObjectGeneralType = objectGeneralType;
        }

        public List<string> GetColumns()
        {
            List<string> columns = new List<string>();
            foreach (AddressSingle addressSingle in Addresses)
                columns.Add(addressSingle.ObjectVariableType);

            return columns;
        }

        public bool CheckOverlap(AddressSingle address)
        {
            if (!int.TryParse(address.AddressStart, out int addressStart))
                return false;

            if (!int.TryParse(address.AddressSize, out int addressSize))
                return false;

            foreach (AddressSingle addressSingle in Addresses)
            {
                if (addressSingle.CheckOverlap(address.Area, addressStart, addressSize))
                    return true;
            }
            return false;
        }
    }

    internal class AddressesClass : GeneralClass<AddressObject>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, 0, false),
                new GeneralColumn(KeywordColumn.CPU, 1, false),
                new GeneralColumn(KeywordColumn.ObjectGeneralType, 2,false),
                new GeneralColumn(KeywordColumn.ObjectType, 3,false),
                new GeneralColumn(KeywordColumn.ObjectName, 4, false),
            };
            return columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
        }

        public AddressesClass(ProgressIndication progress, DataGridView grid) : base("Address", nameof(FileExtensions.address), progress, grid, false)
        {
            Grid.UseKeywordAsName = true;
        }

        public AddressesClass() : base("Address", nameof(FileExtensions.address), null, null, false)
        {
            Grid.UseKeywordAsName = true;
        }

        private void AddColumnToList(List<string> inList, List<string> outList)
        {
            bool found;
            foreach (string inColumn in inList)
            {
                found = false;
                foreach (string outColumn in outList)
                {
                    if (inColumn == outColumn)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    outList.Add(inColumn);
            }
        }

        public List<string> GetColumns()
        {
            List<string> columns = new List<string>();

            foreach (AddressObject addressObject in Signals)
                AddColumnToList(addressObject.GetColumns(), columns);

            return columns;
        }

        public override DataTable SignalsToList()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ConvertDataToList + ": " + Name, Signals.Count);

            int columnNumber;

            //get list of columns that is used
            List<GeneralColumn> newColumnList = new List<GeneralColumn>();

            Columns.SetColumns(GeneralGenerateColumnsList(), false);
            foreach (GeneralColumn column in Columns)
            {
                columnNumber = column.Number;
                if (columnNumber >= 0)
                    newColumnList.Add(column);
            }

            int columnIndex = newColumnList.Count;

            List<string> addressColumns = GetColumns();
            foreach (string columnName in addressColumns)
            {
                newColumnList.Add(new GeneralColumn(ResourcesUI.Area + columnName, columnIndex, false));
                newColumnList.Add(new GeneralColumn(ResourcesUI.Start + columnName, columnIndex + 1, false));
                newColumnList.Add(new GeneralColumn(ResourcesUI.Size + columnName, columnIndex + 2, false));
                columnIndex += 3;
            }

            Columns.SetColumns(newColumnList, false);
            DataTable data = new DataTable();

            //add columns to dataTable
            foreach (GeneralColumn column in Columns.Columns)
                data.Columns.Add(column.Keyword);

            foreach (AddressObject signal in Signals)
            {
                DataRow row = data.NewRow();

                row[0] = signal.ID;
                row[1] = signal.CPU;
                row[2] = signal.ObjectGeneralType;
                row[3] = signal.ObjectType;
                row[4] = signal.ObjectName;
                columnIndex = BaseColumns.Columns.Count;

                foreach (string columnName in addressColumns)
                {
                    foreach (AddressSingle addressSingle in signal.Addresses)
                    {
                        if (addressSingle.ObjectVariableType != columnName)
                            continue;

                        row[columnIndex] = addressSingle.Area;
                        row[columnIndex + 1] = addressSingle.AddressStart;
                        row[columnIndex + 2] = addressSingle.AddressSize;
                        break;
                    }
                    columnIndex += 3;
                }

                data.Rows.Add(row);
                Progress.UpdateProgressBar(data.Rows.Count);
            }
            Progress.HideProgressBar();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);

            return data;
        }

        /// <summary>
        /// Convert list to signals
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>there is data in signals</returns>
        public override bool ListToSignals(DataTable inputData, List<GeneralColumn> newColumnList, bool suppressError)
        {
            if (inputData == null)
                return false;

            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertListToData + ": " + Name, DebugLevels.High, DebugMessageType.Info);

            Signals.Clear();

            int signalCount = 0;
            int columnNumber;
            string keyword;
            string cellValue;
            string columnName;
            int indexText;

            Progress.RenameProgressBar(Resources.ConvertListToData + ": " + Name, inputData.Rows.Count);
            for (int rowNumber = 0; rowNumber < inputData.Rows.Count; rowNumber++)
            {
                AddressObject signal = new AddressObject();

                foreach (GeneralColumn baseColumn in BaseColumns.Columns)
                {
                    columnNumber = baseColumn.Number;

                    if (columnNumber < 0)
                        continue;

                    //put value based on keyword to memory
                    keyword = baseColumn.Keyword;
                    cellValue = GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber);

                    signal.SetValueFromString(cellValue, keyword);
                }

                for (int columnIndex = BaseColumns.Columns.Count; columnIndex < newColumnList.Count; columnIndex += 3)
                {
                    columnNumber = newColumnList[columnIndex].Number;
                    if (string.IsNullOrEmpty(GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber)))
                        continue;

                    indexText = newColumnList[columnIndex].Keyword.IndexOf("(");
                    columnName = indexText == -1 ? string.Empty : newColumnList[columnIndex].Keyword.Substring(indexText);

                    AddressSingle addressSingle = new AddressSingle(GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber)
                                                                    , GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber + 1)
                                                                    , GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber + 2)
                                                                    , columnName);
                    signal.Addresses.Add(addressSingle);
                }

                Progress.UpdateProgressBar(rowNumber);

                if (signal.ValidateSignal())
                {
                    signalCount++;
                    Signals.Add(signal);
                }
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ConvertListToData + ": " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);

            if (signalCount == 0 && !suppressError)
                debug.ToPopUp(Resources.NoData + ": " + Name, DebugLevels.None, DebugMessageType.Warning);

            return signalCount > 0;
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

            foreach (AddressObject addressObject in Signals)
            {
                if (addressObject.ObjectName != objectName)
                    continue;

                addressObject.SetValueFromString(cpu, KeywordColumn.CPU);
                addressObject.SetValueFromString(objectName, KeywordColumn.ObjectName);
                addressObject.SetValueFromString(objectGeneralType, KeywordColumn.ObjectGeneralType);
                addressObject.SetValueFromString(objectType, KeywordColumn.ObjectType);

                int indexAddress = addressObject.FindAddressIndex(objectVariableType);
                // found, then update these
                if (indexAddress != -1)
                    addressObject.Addresses[indexAddress].Update(area, addressStart, addressSize, objectVariableType);
                //add new address type
                else
                    addressObject.Addresses.Add(new AddressSingle(area, addressStart, addressSize, objectVariableType));
                return;
            }

            AddressObject objects = new AddressObject();
            objects.Update(GeneralFunctions.AddZeroes(Signals.Count), cpu, objectGeneralType, objectType, objectName);
            objects.Addresses.Add(new AddressSingle(area, addressStart, addressSize, objectVariableType));

            Signals.Add(objects);
        }

        /// <summary>
        /// Check overlap of address
        /// </summary>
        /// <param name="checkIndex">index of address to check overlap</param>
        /// <param name="checkAddressIndex">index of address to check</param>
        /// <returns>overlapped</returns>
        private bool CheckOverlap(int checkIndex, int checkAddressIndex)
        {
            AddressObject signal = Signals[checkIndex];
            AddressSingle address = Signals[checkIndex].Addresses[checkAddressIndex];

            for (int i = checkAddressIndex + 1; i < Signals[checkIndex].Addresses.Count; i++)
            {
                if (Signals[checkIndex].Addresses[i].CheckOverlapString(address))
                    address.Overlap = true;
            }

            for (int signalIndex = checkIndex + 1; signalIndex < Signals.Count; signalIndex++)
            {
                if (signal.CPU != Signals[signalIndex].CPU)
                    continue;

                if (Signals[signalIndex].CheckOverlap(address))
                    address.Overlap = true;
            }

            return address.Overlap;
        }

        public void CheckOverlapAll()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.CheckOverlap + ": " + Name, DebugLevels.High, DebugMessageType.Info);
            Progress.RenameProgressBar(Resources.CheckOverlap + ": " + Name, Signals.Count);
            bool overlap = false;

            //clear overlap
            foreach (AddressObject addressObject in Signals)
            {
                foreach (AddressSingle addressSingle in addressObject.Addresses)
                    addressSingle.Overlap = false;
            }

            for (int signalIndex = 0; signalIndex < Signals.Count; signalIndex++)
            {
                AddressObject signal = Signals[signalIndex];

                for (int i = 0; i < signal.Addresses.Count; i++)
                {
                    if (CheckOverlap(signalIndex, i))
                        overlap = true;
                }
                Progress.UpdateProgressBar(signalIndex);
            }
            if (overlap)
                ColorOverlap();

            Progress.HideProgressBar();
            debug.ToFile(Resources.CheckOverlap + ": " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }

        private void ColorOverlap()
        {
            Debug debug = new Debug();
            debug.ToFile("Color overlapping addresses: " + Name, DebugLevels.High, DebugMessageType.Info);

            bool overlap = false;

            for (int signalIndex = 0; signalIndex < Signals.Count; signalIndex++)
            {
                for (int i = 0; i < Signals[signalIndex].Addresses.Count; i++)
                {
                    if (!Signals[signalIndex].Addresses[i].Overlap)
                        continue;

                    for (int column = BaseColumns.Columns.Count; column < Columns.Columns.Count; column += 3)
                    {
                        if (Columns.Columns[column].Keyword != (ResourcesUI.Area + Signals[signalIndex].Addresses[i].ObjectVariableType))
                            continue;

                        overlap = true;
                        Grid.ColorCell(signalIndex, column);
                        Grid.ColorCell(signalIndex, column + 1);
                        Grid.ColorCell(signalIndex, column + 2);
                        break;
                    }
                }
            }

            if (overlap)
                debug.ToPopUp(Resources.MemoryOverlap, DebugLevels.None, DebugMessageType.Warning);

            debug.ToFile("Color overlapping addresses: " + Name + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }
    }
}