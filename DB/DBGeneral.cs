using ExcelDataReader;
using IO_list_automation_new.DB;
using IO_list_automation_new.Properties;
using SharpCompress.Compressors.Xz;
using SharpCompress.Crypto;
using SwiftExcel;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class DBGeneralSignal
    {
        public List<string> Line { get; private set; }

        private int Index = 0;
        public DBGeneralSignal()
        {
            Line = new List<string>();
        }

        /// <summary>
        /// Update signal data
        /// </summary>
        /// <param name="newList">new data of signal</param>
        public void SetValue(List<string> newList)
        {
            Line = newList;
        }

        /// <summary>
        /// Checks if design signal is valid:
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public bool ValidateSignal()
        {
            bool _returnValue = true;
            return _returnValue;
        }

        /// <summary>
        /// Peek to next cell and return next cell to read next
        /// </summary>
        /// <param name="inIndex">index to search from</param>
        /// <returns>last index to of data</returns>
        private int DecodePeek(int inIndex)
        {
            int _newIndex = inIndex + 1;
            string _text = Line[_newIndex];

            if (_text == ConstDBChoices.ChoiceIf)
            {
                //peek for variable
                _newIndex = DecodePeek(_newIndex);

                //skip operation
                _newIndex++;

                //peek for true
                _newIndex = DecodePeek(_newIndex);

                //peek for false
                _newIndex = DecodePeek(_newIndex);

                return _newIndex;
            }
            else if (_text == ConstDBChoices.ChoiceTab)
                return _newIndex;
            else
            {
                if (_text == ConstDBChoices.ChoiceData)
                    _newIndex++;
                else if (_text == ConstDBChoices.ChoiceObject)
                    _newIndex++;
                else if (_text == ConstDBChoices.ChoiceIO)
                    _newIndex++;
                else if (_text == ConstDBChoices.ChoiceText)
                    _newIndex++;

                return _newIndex;
            }
        }

        /// <summary>
        /// Find IO in data based on function of object KKS
        /// </summary>
        /// <param name="functionType">function type to find</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>IO tag</returns>
        private string DecodeIO(string functionType, DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            if (!simulation)
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if ((objectSignal.KKS == data.Signals[i].KKS) && (data.Signals[i].Function == functionType))
                        return data.Signals[i].Tag;
                }
                return string.Empty;
            }
            else
                return "KKS." + functionType;
        }

        /// <summary>
        /// Find requested column in data
        /// </summary>
        /// <param name="columnName">column name in data</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>column value</returns>
        private string DecodeData(string columnName, DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            if (!simulation)
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if (objectSignal.KKS == data.Signals[i].KKS)
                        return data.Signals[i].GetValueString(columnName, false);
                }
                return string.Empty;
            }
            else
                return "Data." + columnName;
        }

        /// <summary>
        /// find requested data in objects
        /// </summary>
        /// <param name="columnName">column name in objects</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>column value</returns>
        private string DecodeObjects(string columnName, ObjectSignal objectSignal, bool simulation)
        {
            if (!simulation)
                return objectSignal.GetValueString(columnName, false);
            else
                return "Object." + columnName;
        }

        /// <summary>
        /// decoding if statement from 4 cells
        /// </summary>
        /// <param name="decodedLine">decoded list</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        private void DecodeIf(List<string> decodedLine, DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            Index++;
            string _text = Line[Index];
            string _returnPart1 = string.Empty;
            int _newIndex = 0;

            if (_text == ConstDBChoices.ChoiceData)
            {
                Index++;
                _returnPart1 = DecodeData(Line[Index], data, objectSignal, simulation);
            }
            else if (_text == ConstDBChoices.ChoiceObject)
            {
                Index++;
                _returnPart1 = DecodeObjects(Line[Index], objectSignal, simulation);
            }
            else if (_text == ConstDBChoices.ChoiceIO)
            {
                Index++;
                _returnPart1 = DecodeIO(Line[Index], data, objectSignal, simulation);
            }

            Index++;
            _text = Line[Index];
            bool _returnPart2 = false;
            if (_text == ConstDBChoices.ChoiceIsEmpty)
                _returnPart2 = _returnPart1 == string.Empty;
            else
                _returnPart2 = _returnPart1 != string.Empty;

            //if = true
            if (_returnPart2)
            {
                Index++;
                DecodeCell(decodedLine, data, objectSignal, simulation);

                //peek false statement and update index to skip it
                _newIndex = DecodePeek(Index);
                Index = _newIndex;
            }
            //if = false
            else
            {
                //peek true statement and update index to skip it
                _newIndex = DecodePeek(Index) + 1;
                Index = _newIndex;
                DecodeCell(decodedLine, data, objectSignal, simulation);
            }
        }

        /// <summary>
        /// 1 cell decoding
        /// </summary>
        /// <param name="decodedLine"></param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        private void DecodeCell(List<string> decodedLine, DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            string _text = Line[Index];

            if (_text == ConstDBChoices.ChoiceIf)
                DecodeIf(decodedLine, data, objectSignal, simulation);
            else if (_text == ConstDBChoices.ChoiceTab)
                decodedLine.Add(string.Empty);
            else
            {
                if (_text == ConstDBChoices.ChoiceData)
                {
                    Index++;
                    decodedLine[decodedLine.Count - 1] += DecodeData(Line[Index], data, objectSignal, simulation);
                }
                else if (_text == ConstDBChoices.ChoiceObject)
                {
                    Index++;
                    decodedLine[decodedLine.Count - 1] += DecodeObjects(Line[Index], objectSignal, simulation);
                }
                else if (_text == ConstDBChoices.ChoiceText)
                {
                    Index++;
                    _text = Line[Index];
                    decodedLine[decodedLine.Count - 1] += _text;
                }
                else if (_text == ConstDBChoices.ChoiceIO)
                {
                    Index++;
                    decodedLine[decodedLine.Count - 1] += DecodeIO(Line[Index], data, objectSignal, simulation);
                }
            }
        }

        /// <summary>
        /// Decode instance line
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>decoded text line</returns>
        public List<string> DecodeLine(DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            Index = 0;
            List<string> decodedLine = new List<string>();
            decodedLine.Add(string.Empty);

            for (int i=0; i< Line.Count; i++)
            {
                DecodeCell(decodedLine, data, objectSignal, simulation);
                i = Index;
                Index++;
            }

            return decodedLine;
        }
    }

    internal class DBGeneralType
    {
        public string Name { get; }

        public string InstanceType { get; }

        public List<DBGeneralSignal> Data;

        public ProgressIndication Progress { get; set; }

        public DBGeneralGrid Grid { get; private set; }

        public DBGeneralType(string name, string filePath, string instanceType, string fileExtension, ProgressIndication progress, DataGridView grid, bool editableGrid)
        {
            Name = name;
            InstanceType = instanceType;
            Data = new List<DBGeneralSignal>();
            Progress = progress;
            Grid = new DBGeneralGrid(Name, filePath, fileExtension, Progress, grid, editableGrid);
        }

        /// <summary>
        /// Decode one object of this type
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object to be decoded</param>
        /// <returns>decoded text</returns>
        public List<List<string>> Decode(DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            //if type mismatch skip
            if (objectSignal.ObjectType == InstanceType)
            {
                List<List<string>> _decodedObject = new List<List<string>>();
                for (int i = 0; i < Data.Count; i++)
                    _decodedObject.Add(Data[i].DecodeLine(data, objectSignal, simulation));

                return _decodedObject;
            }
            return null;
        }

        /// <summary>
        /// Convert DBGeneralInstanceSignal to List of List of string
        /// </summary>
        /// <returns>converted data</returns>
        public List<List<string>> ConvertDataToList()
        {
            List<List<string>> _allData = new List<List<string>>();
            //det all data from all lines of one type
            for (int j = 0; j < Data.Count; j++)
                _allData.Add(Data[j].Line);

            return _allData;
        }

        /// <summary>
        /// Set data from list list string
        /// </summary>
        /// <param name="inputData">list string</param>
        public void SetData(List<List<string>> inputData)
        {
            Data.Clear();

            if (inputData != null)
            {
                for (int j = 0; j < inputData.Count; j++)
                {
                    DBGeneralSignal _signal = new DBGeneralSignal();

                    _signal.SetValue(inputData[j]);
                    Data.Add(_signal);
                }
            }

        }
    }

    internal class DBGeneral
    {
        public List<DBGeneralType> Devices { get; set; }
        public string FileExtension { get; private set; }
        private ProgressIndication Progress { get; set; }

        private string Directory;
        public DBGeneral(ProgressIndication progress, string fileExtension, string aditionalFolder)
        {
            FileExtension = fileExtension;
            Progress = progress;

            Devices = new List<DBGeneralType>();
            Directory = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\DB\\" + aditionalFolder + Settings.Default.SelectedCPU;
            GetDeviceTypesFromFile();
        }

        /// <summary>
        /// Get all instances from DB configuration files to Devices
        /// </summary>
        private void GetDeviceTypesFromFile()
        {
            Debug debug = new Debug();
            debug.ToFile("Serching instances configuration from file", DebugLevels.Development, DebugMessageType.Info);
            Devices.Clear();

            try
            {
                string[] _files = System.IO.Directory.GetFiles(Directory, "*" + FileExtension);

                if (_files.Length > 0)
                {
                    List<string> _instancesTypes = new List<string>();

                    string _fileName = string.Empty;
                    string _type = string.Empty;

                    for (int i = 0; i < _files.Length; i++)
                    {
                        _fileName = _files[i];
                        _type = _fileName.Replace(Directory + "\\", "").Replace("." + FileExtension, "");

                        //assign dummy grid
                        DataGridView _gridas = new DataGridView();
                        DBGeneralType _instance = new DBGeneralType("Instances", _fileName, _type, FileExtension, Progress, _gridas, false);
                        List<List<string>> _fileData = _instance.Grid.LoadFromFileToMemory();
                        _instance.SetData(_fileData);

                        Devices.Add(_instance);
                    }
                }
                else
                    debug.ToPopUp(Resources.DBNotFound + ": " + Settings.Default.SelectedCPU + " - " + Resources.Instances, DebugLevels.Development, DebugMessageType.Info);
            }
            catch (Exception ex)
            {
                debug.ToPopUp(Resources.DebugCritical + ": " + ex.Message, DebugLevels.None, DebugMessageType.Critical);
            }

            debug.ToFile("Serching instances configuration from file - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get all instances from grid to Devices
        /// </summary>
        private void GetDeviceTypesFromGrid(TabControl tabControl)
        {
            Debug debug = new Debug();
            debug.ToFile("Serching instances configuration from grid", DebugLevels.Development, DebugMessageType.Info);
            Devices.Clear();

            if (tabControl.TabPages.Count > 0)
            {
                List<string> _instancesTypes = new List<string>();

                string _fileName = string.Empty;
                string _type = string.Empty;

                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    _type = tabControl.TabPages[i].Name;
                    _fileName = Directory + "\\" + _type + "." + FileExtension;

                    DataGridView _gridas = (DataGridView)tabControl.TabPages[i].Controls[0];
                    DBGeneralType _instance = new DBGeneralType("Instances", _fileName, _type, FileExtension, Progress, _gridas, false);
                    List<List<string>> _fileData = _instance.Grid.GetData();
                    _instance.SetData(_fileData);

                    Devices.Add(_instance);
                }
            }
            else
                debug.ToPopUp(Resources.DBNotFound + ": " + Settings.Default.SelectedCPU + " - " + Resources.Instances, DebugLevels.Development, DebugMessageType.Info);

            debug.ToFile("Serching instances configuration from grid - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// copy device list to all list
        /// </summary>
        /// <param name="_input">new data of device</param>
        /// <param name="_output">all device data</param>
        private void CopyDecodedList(List<List<string>> _input, List<List<string>> _output)
        {
            if (_input != null)
            {
                for (int i = 0; i < _input.Count; i++)
                    _output.Add(_input[i]);
            }
        }

        /// <summary>
        /// Decode all objects for instance
        /// </summary>
        /// <param name="data">data signals</param>
        /// <param name="objects">object signals</param>
        public void DecodeAll(DataClass data, ObjectsClass objects)
        {
            if (Devices.Count > 0)
            {
                List<List<string>> _decodedObject = new List<List<string>>();

                Debug debug = new Debug();
                debug.ToFile("Generating instances", DebugLevels.Development, DebugMessageType.Info);
                Progress.RenameProgressBar(Resources.InstancesGenerate, Devices.Count);

                DBResultForm _form = new DBResultForm("Instances", false);
                //clear all used columns
                for (int i = 0; i < data.Signals.Count; i++)
                    data.Signals[i].SetValueFromString(string.Empty, ConstCol.ColumnNameUsed);

                //go through all device types
                for (int i = 0; i < Devices.Count; i++)
                {
                    List<List<string>> _decodedDevices = new List<List<string>>();
                    for (int j = 0; j < objects.Signals.Count; j++)
                    {
                        _decodedObject = Devices[i].Decode(data, objects.Signals[j],false);
                        CopyDecodedList(_decodedObject, _decodedDevices);
                    }
                    DataGridView _grid = _form.AddData(Devices[i].InstanceType);

                    //add grid to form and update grid of device to mach in form
                    Devices[i].Grid.ChangeGrid(_grid);
                    Devices[i].Grid.PutData(_decodedDevices);
                    Progress.UpdateProgressBar(i);
                }
                Progress.HideProgressBar();

                debug.ToFile("Generating instances - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
                //put updated data with used collumn
                data.Grid.PutData();

                
                _form.ShowDialog();
            }
        }

        /// <summary>
        /// Edit instances data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="objects"></param>
        public void EditAll()
        {
            if (Devices.Count > 0)
            {
                List<List<string>> _deviceData = new List<List<string>>();

                Debug debug = new Debug();
                debug.ToFile("Editing instances", DebugLevels.Development, DebugMessageType.Info);
                Progress.RenameProgressBar(Resources.InstancesEditGenerate, Devices.Count);

                DBResultForm _form = new DBResultForm("Instances Edit",true);

                //go through all device types
                for (int i = 0; i < Devices.Count; i++)
                {
                    _deviceData = Devices[i].ConvertDataToList();
                    DataGridView _grid = _form.AddData(Devices[i].InstanceType);

                    //add grid to form and update grid of device to mach in form
                    Devices[i].Grid.ChangeGrid(_grid);
                    Devices[i].Grid.PutData(_deviceData);

                    Progress.UpdateProgressBar(i);
                }
                Progress.HideProgressBar();

                debug.ToFile("Editing instances - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

                _form.ShowDialog();

                GetDeviceTypesFromGrid(_form.DBTabControl);
                //go through all device types
                for (int i = 0; i < Devices.Count; i++)
                {
                    Devices[i].Grid.GetData();
                    Devices[i].Grid.SaveToFile();
                }
            }
        }
    }
}