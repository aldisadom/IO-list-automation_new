﻿using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace IO_list_automation_new.DB
{
    internal class DBGeneralType
    {
        public string Name { get; }

        public string FullDBType { get; }

        public string ObjectVariableType { get; }

        public string DBType { get; }

        public string MemoryArea { get; private set; }
        public string Address { get; private set; }
        public string AddressSize { get; private set; }

        public List<DBGeneralSignal> Data;

        public ProgressIndication Progress { get; set; }
        public GeneralGrid Grid { get; }
        public ExcelFiles File { get; set; }
        public GeneralGrid GridResult { get; }

        public DBGeneralType(string name, string fullDBType, string dbType, string fileExtension, ProgressIndication progress, DataGridView grid)
        {
            Name = name;
            FullDBType = fullDBType;
            DBType = dbType;
            MemoryArea = string.Empty;
            Address = string.Empty;
            AddressSize = string.Empty;

            ObjectVariableType = FullDBType.Replace(dbType, "");

            Data = new List<DBGeneralSignal>();
            Progress = progress;

            Grid = new GeneralGrid(Name, GridTypes.DBEditable, grid, null);
            File = new ExcelFiles(Name, fileExtension, Progress);
            GridResult = new GeneralGrid(Name, GridTypes.DB, new DataGridView(), null);
        }

        /// <summary>
        /// Decode one object of this type
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object to be decoded</param>
        /// <param name="moduleSignal">module to be decoded</param>
        /// <returns>decoded text</returns>
        public void Decode(DataTable dataTable, ref int index, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase)
        {
            //if type mismatch skip
            switch (inputBase)
            {
                case BaseTypes.ModuleCPU:
                    if (moduleSignal == null || moduleSignal.ModuleType != DBType)
                        return;
                    break;

                case BaseTypes.ModuleSCADA:
                    if (addressObject == null || addressObject.ObjectType != DBType || addressObject.ObjectGeneralType != ResourcesUI.Modules)
                        return;
                    break;

                case BaseTypes.ObjectsCPU:
                    if (objectSignal == null || objectSignal.ObjectType != DBType)
                        return;
                    break;

                case BaseTypes.ObjectSCADA:
                    if (addressObject == null || addressObject.ObjectType != DBType || addressObject.ObjectGeneralType != ResourcesUI.Objects)
                        return;
                    break;

                default:
                    const string debugText = "DBGeneralType.Decode";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(inputBase), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(inputBase) + " is not created for this element");
            }

            for (int i = 0; i < Data.Count; i++)
            {
                Data[i].DecodeLine(dataTable, index, data, objectSignal, moduleSignal, addressObject, inputBase);

                if (Data[i].BaseAddressSet)
                {
                    Address = Data[i].BaseAddress.ToString();
                    MemoryArea = Data[i].MemoryArea;
                    AddressSize = Data[i].AddressSize.ToString();

                    // update other rows base addresses
                    for (int j = i + 1; j < Data.Count; j++)
                        Data[j].UpdateBaseAddress(Data[i].MemoryArea, Data[i].BaseAddress, Data[i].AddressSize);
                }
            }
            index++;
        }

        /// <summary>
        /// Convert DBGeneralSignal to List of List of string
        /// </summary>
        /// <returns>converted data</returns>
        public DataTable ConvertDataToList()
        {
            DataTable allData = new DataTable();

            //go through all data from all lines of one type
            foreach (DBGeneralSignal dataRow in Data)
            {
                //add columns to dataTable
                for (int column = allData.Columns.Count; column < dataRow.Cell.Count; column++)
                    allData.Columns.Add(column.ToString());

                DataRow row = allData.NewRow();

                for (int i = 0; i < dataRow.Cell.Count; i++)
                    row[i] = dataRow.Cell[i];

                allData.Rows.Add(row);
            }
            return allData;
        }

        /// <summary>
        /// Set data from list of list string
        /// </summary>
        /// <param name="inputData">list string</param>
        public void SetData(DataTable inputData)
        {
            Data.Clear();

            if (inputData == null)
                return;

            for (int row = 0; row < inputData.Rows.Count; row++)
            {
                DBGeneralSignal signal = new DBGeneralSignal();

                List<string> list = new List<string>();
                for (int column = 0; column < inputData.Columns.Count; column++)
                    list.Add(GeneralFunctions.GetDataTableValue(inputData, row, column));

                signal.SetValue(list);
                Data.Add(signal);
            }
        }
    }
}