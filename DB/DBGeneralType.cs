using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
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

            Grid = new GeneralGrid(Name, GridTypes.DBEditable, fileExtension, Progress, grid, new ColumnList());
            GridResult = new GeneralGrid(Name, GridTypes.DB, fileExtension, Progress, new DataGridView(), new ColumnList());
        }

        /// <summary>
        /// Decode one object of this type
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object to be decoded</param>
        /// <param name="moduleSignal">module to be decoded</param>
        /// <returns>decoded text</returns>
        public List<List<string>> Decode(ref int index, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase)
        {
            //if type mismatch skip
            switch (inputBase)
            {
                case BaseTypes.ModuleCPU:
                    if (moduleSignal == null || moduleSignal.ModuleType != DBType)
                        return null;
                    break;

                case BaseTypes.ModuleSCADA:
                    if (addressObject == null || addressObject.ObjectType != DBType || addressObject.ObjectGeneralType != ResourcesUI.Modules)
                        return null;
                    break;

                case BaseTypes.ObjectsCPU:
                    if (objectSignal == null || objectSignal.ObjectType != DBType)
                        return null;
                    break;

                case BaseTypes.ObjectSCADA:
                    if (addressObject == null || addressObject.ObjectType != DBType || addressObject.ObjectGeneralType != ResourcesUI.Objects)
                        return null;
                    break;

                default:
                    const string _debugText = "DBGeneralType.Decode";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(inputBase), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + nameof(inputBase) + " is not created for this element");
            }

            List<List<string>> _decodedObject = new List<List<string>>();
            for (int i = 0; i < Data.Count; i++)
            {
                _decodedObject.Add(Data[i].DecodeLine(index, data, objectSignal, moduleSignal, addressObject, inputBase));
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
            return _decodedObject;
        }

        /// <summary>
        /// Convert DBGeneralSignal to List of List of string
        /// </summary>
        /// <returns>converted data</returns>
        public List<List<string>> ConvertDataToList()
        {
            List<List<string>> _allData = new List<List<string>>();

            //go through all data from all lines of one type
            for (int j = 0; j < Data.Count; j++)
                _allData.Add(Data[j].Line);

            return _allData;
        }

        /// <summary>
        /// Set data from list of list string
        /// </summary>
        /// <param name="inputData">list string</param>
        public void SetData(List<List<string>> inputData)
        {
            Data.Clear();

            if (inputData == null)
                return;

            for (int j = 0; j < inputData.Count; j++)
            {
                DBGeneralSignal _signal = new DBGeneralSignal();

                _signal.SetValue(inputData[j]);
                Data.Add(_signal);
            }
        }
    }
}