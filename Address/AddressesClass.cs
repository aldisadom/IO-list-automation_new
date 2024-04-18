using IO_list_automation_new.Address;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class AddressesClass : GeneralClass<AddressObject>
    {
        protected override void InitialCollumnList()
        {
            Columns = new ColumnList(Name);

            Columns.Columns.Add(KeywordColumn.ID, new ColumnParameters(0, false, false));
            Columns.Columns.Add(KeywordColumn.CPU, new ColumnParameters(1, false, false));
            Columns.Columns.Add(KeywordColumn.ObjectGeneralType, new ColumnParameters(2, false, false));
            Columns.Columns.Add(KeywordColumn.ObjectType, new ColumnParameters(3, false, false));
            Columns.Columns.Add(KeywordColumn.ObjectName, new ColumnParameters(4, false, false));

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

        public List<string> GetColumnsList()
        {
            List<string> columns = new List<string>();

            foreach (AddressObject addressObject in Signals)
                AddColumnToList(addressObject.GetColumns(), columns);

            return columns;
        }

        public override DataTable SignalsToDataTable()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ConvertDataToList + ": " + Name, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ConvertDataToList + ": " + Name, Signals.Count);

            int columnNumber;

            //get list of columns that is used
            ColumnList newColumnList = new ColumnList(Name);

            foreach (var column in Columns.Columns)
            {
                columnNumber = column.Value.NR;
                if (columnNumber >= 0)
                    newColumnList.Columns.Add(column.Key, column.Value);
            }

            int columnIndex = newColumnList.Columns.Count;

            List<string> addressColumns = GetColumnsList();
            foreach (string columnName in addressColumns)
            {
                newColumnList.Columns.Add(ResourcesUI.Area + columnName, new ColumnParameters(columnIndex, false, false));
                newColumnList.Columns.Add(ResourcesUI.Area + ResourcesUI.Start + columnName, new ColumnParameters(columnIndex + 1, false, false));
                newColumnList.Columns.Add(ResourcesUI.Area + ResourcesUI.Size + columnName, new ColumnParameters(columnIndex + 2, false, false));
                columnIndex += 3;
            }

            DataTable data = new DataTable();

            //add columns to dataTable
            foreach (var column in Columns.Columns)
                data.Columns.Add(column.Key);

            foreach (AddressObject signal in Signals)
            {
                DataRow row = data.NewRow();

                row[0] = signal.ID;
                row[1] = signal.CPU;
                row[2] = signal.ObjectGeneralType;
                row[3] = signal.ObjectType;
                row[4] = signal.ObjectName;
                columnIndex = Columns.Columns.Count;

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
        public override bool DataTableToSignals(DataTable inputData, bool suppressError)
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

                foreach (var column in Columns.Columns)
                {
                    columnNumber = column.Value.NR;

                    if (columnNumber < 0)
                        continue;

                    //put value based on keyword to memory
                    keyword = column.Key;
                    cellValue = GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber);

                    signal.SetValueFromString(cellValue, keyword);
                }
                foreach (var column in Columns.Columns)
                {
                    columnNumber = column.Value.NR;
                    if (string.IsNullOrEmpty(GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber)))
                        continue;

                    indexText = column.Key.IndexOf("(");
                    columnName = indexText == -1 ? string.Empty : column.Key.Substring(indexText);

                    AddressSingle addressSingle = new AddressSingle(GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber)
                                                                    , GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber + 1)
                                                                    , GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber + 2)
                                                                    , columnName);
                    signal.Addresses.Add(addressSingle);
                }

                foreach (var column in Columns.Columns)
                {
                    columnNumber = column.Value.NR;
                    if (string.IsNullOrEmpty(GeneralFunctions.GetDataTableValue(inputData, rowNumber, columnNumber)))
                        continue;

                    indexText = column.Key.IndexOf("(");
                    columnName = indexText == -1 ? string.Empty : column.Key.Substring(indexText);

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

                    foreach (var column in Columns.Columns)
                    {
                        if (column.Key != (ResourcesUI.Area + Signals[signalIndex].Addresses[i].ObjectVariableType))
                            continue;

                        overlap = true;
                        Grid.ColorCell(signalIndex, column.Value.NR);
                        Grid.ColorCell(signalIndex, column.Value.NR + 1);
                        Grid.ColorCell(signalIndex, column.Value.NR + 2);
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