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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace IO_list_automation_new
{
    internal class DBGeneralSignal
    {
        public List<string> Line { get; private set; }

        public string TagType { get; private set; }
        public string MemoryArea { get; private set; }
        public string Adress { get; private set; }

        private int Index = 0;
        public DBGeneralSignal()
        {
            TagType = string.Empty;
            MemoryArea = string.Empty;
            Adress = string.Empty;
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
        /// Set Tag of this line
        /// </summary>
        /// <param name="tagType">tag type</param>
        private void DecodeTagType(string tagType)
        {
            TagType = tagType;
        }

        /// <summary>
        /// Calculate value
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine">line of DB</param>
        /// <returns>value calculated with offset and multiplyer</returns>
        private string DecodeIndex(int index,int indexLine, List<string> line)
        {
            int _value = 0;
            int _returnValue = 0;

            MemoryArea = line[indexLine];
            if (int.TryParse(line[indexLine+1], out _value))
            {
                _returnValue = index * _value;
                if (int.TryParse(line[indexLine + 2], out _value))
                    _returnValue = _returnValue + _value;

                Adress = _returnValue.ToString();
                return Adress;
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// decoding if statement from 4 cells
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine">decoded list</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        private void DecodeIf(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            Index++;
            string _text = Line[Index];
            string _returnPart1 = string.Empty;
            int _newIndex = 0;
            Debug _debug = new Debug();
            string text = string.Empty;

            Index++;
            if (_text == KeywordDBChoices.Data)
                _returnPart1 = DecodeData(Line[Index], data, objectSignal, simulation);
            else if (_text == KeywordDBChoices.Object)
                _returnPart1 = DecodeObjects(Line[Index], objectSignal, simulation);
            else if (_text == KeywordDBChoices.IO)
                _returnPart1 = DecodeIO(Line[Index], data, objectSignal, simulation);
            else
            {
                text = "DBGeneralSignal.DecodeIf";
                _debug.ToFile("Report to programmer that there is error in " + text + " " + _text, DebugLevels.None, DebugMessageType.Critical);
                throw new InvalidProgramException("Error in - " + text + "." + _text);
            }

            Index++;
            _text = Line[Index];
            bool _ifTrue = false;

            if (_text == KeywordDBChoices.IsEmpty)
                _ifTrue = _returnPart1 == string.Empty;
            else if (_text == KeywordDBChoices.IsNotEmpty)
                _ifTrue = _returnPart1 != string.Empty;
            else
            {
                text = "DBGeneralSignal.DecodeIf";
                _debug.ToFile("Report to programmer that there is error in " + text + " " + _text, DebugLevels.None, DebugMessageType.Critical);
                throw new InvalidProgramException("Error in - " + text + "." + _text);
            }

            //if = true
            if (_ifTrue)
            {
                Index++;
                DecodeCell(index, decodedLine, data, objectSignal, simulation);

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
                DecodeCell(index, decodedLine, data, objectSignal, simulation);
            }
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

            if (_text == KeywordDBChoices.If)
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
            else if (_text == KeywordDBChoices.Tab)
                return _newIndex;
            else
            {
                if (_text == KeywordDBChoices.Data)
                    _newIndex++;
                else if (_text == KeywordDBChoices.Object)
                    _newIndex++;
                else if (_text == KeywordDBChoices.IO)
                    _newIndex++;
                else if (_text == KeywordDBChoices.Text)
                    _newIndex++;
                else if (_text == KeywordDBChoices.TagType)
                    _newIndex++;
                else if (_text == KeywordDBChoices.Index)
                    _newIndex += 3;
                else
                {
                    Debug _debug = new Debug();
                    string text = "DBGeneralSignal.DecodePeek.IF";
                    _debug.ToFile("Report to programmer that there is error in " + text + " " + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException("Error in - " + text + "." + _text);
                }

                return _newIndex;
            }
        }

        /// <summary>
        /// 1 cell decoding
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine"></param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        private void DecodeCell(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            string _text = Line[Index];

            if (_text == KeywordDBChoices.If)
                DecodeIf(index, decodedLine, data, objectSignal, simulation);
            else if (_text == KeywordDBChoices.Tab)
                decodedLine.Add(string.Empty);
            else
            {
                Index++;
                switch (_text)
                {
                    case KeywordDBChoices.Data:
                        decodedLine[decodedLine.Count - 1] += DecodeData(Line[Index], data, objectSignal, simulation);
                        break;
                    case KeywordDBChoices.Index:
                        decodedLine[decodedLine.Count - 1] += DecodeIndex(index, Index, Line);
                        Index += 2;
                        break;
                    case KeywordDBChoices.Object:
                        decodedLine[decodedLine.Count - 1] += DecodeObjects(Line[Index], objectSignal, simulation);
                        break;
                    case KeywordDBChoices.TagType:
                        DecodeTagType(Line[Index]);
                        break;
                    case KeywordDBChoices.Text:
                        _text = Line[Index];
                        decodedLine[decodedLine.Count - 1] += _text;
                        break;
                    case KeywordDBChoices.IO:
                        decodedLine[decodedLine.Count - 1] += DecodeIO(Line[Index], data, objectSignal, simulation);
                        break;
                    case KeywordDBChoices.None:
                        break;
                    default:
                        Debug _debug = new Debug();
                        string text = "DBGeneralSignal.DecodeCell";
                        _debug.ToFile("Report to programmer that there is error in " + text + " " + _text, DebugLevels.None, DebugMessageType.Critical);
                        throw new InvalidProgramException("Error in - " + text + "." + _text);
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
        public List<string> DecodeLine(int index,DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            Index = 0;
            List<string> decodedLine = new List<string>();
            decodedLine.Add(string.Empty);

            for (int i=0; i< Line.Count; i++)
            {
                DecodeCell(index,decodedLine, data, objectSignal, simulation);
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
        public List<List<string>> Decode(ref int index,DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            //if type mismatch skip
            if (objectSignal.ObjectType == InstanceType)
            {
                List<List<string>> _decodedObject = new List<List<string>>();
                for (int i = 0; i < Data.Count; i++)
                    _decodedObject.Add(Data[i].DecodeLine(index,data, objectSignal, simulation));

                index++;
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
        /// Set data from list of list string
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

        private string BaseDirectory;

        private string NameDB;

        private DBTypeLevel Level;

        public DBGeneral(ProgressIndication progress, string nameDB, string fileExtension, DBTypeLevel level)
        {
            Progress = progress;
            NameDB = nameDB;
            FileExtension = fileExtension;
            Devices = new List<DBGeneralType>();
            Level = level;

            switch (level)
            {
                case DBTypeLevel.Base:
                    BaseDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB";
                    Directory = BaseDirectory;
                    break;
                case DBTypeLevel.CPU:
                    BaseDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB";
                    Directory = BaseDirectory + "\\" + Settings.Default.SelectedCPU;
                    break;
                case DBTypeLevel.SCADA:
                    BaseDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB\\" + Settings.Default.SelectedCPU;
                    Directory = BaseDirectory + "\\" + Settings.Default.SelectedSCADA;
                    break;
                default:
                    Debug _debug = new Debug();
                    string text = "DBGeneral.DBInit";
                    _debug.ToFile("Report to programmer that there is error in " + text + " " + level.ToString(), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException("Error in - " + text + "." + level.ToString());
            }
        }

        /// <summary>
        /// Creates DB file
        /// </summary>
        /// <param name="fileName">file to check and create</param>
        /// <returns>DB files exists</returns>
        public bool CreateDBFile(string fileName)
        {
            string _fileName = Directory + "\\" + fileName + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.CreateNew + " " + Level.ToString() + ": " + _fileName, DebugLevels.Development, DebugMessageType.Info);

            ExcelWriter _excel = new ExcelWriter(_fileName);

            //write 1 empty cell
            _excel.Write(string.Empty, 1, 1);
            _excel.Save();
            _excel.Dispose();

            debug.ToFile(Resources.CreateNew + " " + Level.ToString() + ": " + _fileName + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            return DBFileExists(fileName);
        }

        /// <summary>
        /// Check if database files exists and ask if needed to create
        /// </summary>
        /// <param name="fileName">file to check and create</param>
        /// <returns>DB files exists</returns>
        public bool DBFileExists(string fileName)
        {
            //get files in folder
            string[] _files = System.IO.Directory.GetFiles(Directory, fileName + "." + FileExtension);
            return _files.Length>0;
        }

        /// <summary>
        /// Check if database files exists
        /// </summary>
        /// <returns>DB files exists</returns>
        private bool DBFilesExists()
        {
            //get files in folder
            string[] _files = System.IO.Directory.GetFiles(Directory, "*" + FileExtension);
            return _files.Length > 0;
        }

        /// <summary>
        /// Get DB file list
        /// </summary>
        /// <returns>list of files in database with needed extension </returns>
        public List<string> GetDBFileList()
        {
            List<string> _filesList = new List<string>();

            string[] _files = System.IO.Directory.GetFiles(Directory, "*" + FileExtension);
            if (_files.Length > 0)
            {
                for (int i = 0; i < _files.Length; i++)
                    _filesList.Add(_files[i].Replace(Directory + "\\", string.Empty).Replace("."+FileExtension, string.Empty));
            }

            return _filesList;
        }

        /// <summary>
        /// Check if database folder exists and ask if needed to create
        /// </summary>
        /// <param name="folderName">folder to check and create</param>
        /// <returns>DB folder exists</returns>
        public bool DBFolderExists(string folderName)
        {
            return System.IO.Directory.Exists(BaseDirectory + "\\" + folderName);
        }

        /// <summary>
        /// Check if database folder exists and ask if needed to create
        /// </summary>
        /// <param name="folderName">folder to check and create</param>
        /// <returns>DB folder exists</returns>
        public bool DBFolderCreate(string folderName)
        {
            string folderPath = BaseDirectory + "\\" + folderName;

            //check if folder exists
            if (DBFolderExists(folderPath))
                return true;
            else
            {
                Debug _debug = new Debug();
                _debug.ToFile(Resources.CreateNew + " " + Level.ToString() + " " + folderName + " " + NameDB, DebugLevels.Development, DebugMessageType.Info);

                System.IO.Directory.CreateDirectory(folderPath);
                return DBFolderExists(folderName);
            }
        }

        /// <summary>
        /// Get DB folder list
        /// </summary>
        /// <returns>list of folders in database </returns>
        public List<string> GetDBFolderList()
        {
            List<string> _folderList = new List<string>();

            string[] _folders = System.IO.Directory.GetDirectories(BaseDirectory);
            if (_folders.Length > 0)
            {
                for (int i = 0; i < _folders.Length; i++)
                    _folderList.Add(_folders[i].Replace(BaseDirectory, string.Empty).Replace("\\", string.Empty));
            }

            return _folderList;
        }

        /// <summary>
        /// Check DB files and folders
        /// </summary>
        /// <returns>DB files exists</returns>
        private bool CheckDBFiles()
        {
            return DBFolderExists(Directory.Replace(BaseDirectory, string.Empty).Replace("\\", string.Empty)) && DBFilesExists();
        }

        /// <summary>
        /// Get all instances from DB configuration files to Devices
        /// </summary>
        private void GetDeviceTypesFromFile()
        {
            Debug debug = new Debug();
            debug.ToFile("Searching instances configuration from file", DebugLevels.Development, DebugMessageType.Info);
            Devices.Clear();

            List<string> _files = GetDBFileList();

            string _fileName = string.Empty;
            string _type = string.Empty;

            for (int i = 0; i < _files.Count; i++)
            {
                _fileName = Directory + "\\" + _files[i] + "." + FileExtension;
                _type = _files[i];

                //assign dummy grid
                DataGridView _gridas = new DataGridView();
                DBGeneralType _instance = new DBGeneralType("Instances", _fileName, _type, FileExtension, Progress, _gridas, false);
                List<List<string>> _fileData = _instance.Grid.LoadFromFileToMemory();
                _instance.SetData(_fileData);

                Devices.Add(_instance);
            }

            debug.ToFile("Serching instances configuration from file - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get all instances from grid to Devices
        /// </summary>
        private void GetDeviceTypesFromGrid(TabControl tabControl)
        {
            Debug debug = new Debug();
            debug.ToFile("Searching instances configuration from grid", DebugLevels.Development, DebugMessageType.Info);
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
                debug.ToPopUp(Resources.DBNotFound + ": " + Settings.Default.SelectedCPU + " - " + Resources.DeleteMe, DebugLevels.Development, DebugMessageType.Info);

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
            if (!CheckDBFiles())
            {
                DialogResult _result = MessageBox.Show(Resources.CreateNew + " " + Level.ToString() + "?", Resources.CreateNew, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result != DialogResult.Yes)
                    return;

                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language);
                _newName.ShowDialog();
                if (_newName.Output != null && _newName.Output != string.Empty)
                {
                    CreateDBFile(_newName.Output);
                    DecodeAll(data, objects);
                }
                return;
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            List<List<string>> _decodedObject = new List<List<string>>();
            List<string> _CPUList = new List<string>();

            Debug debug = new Debug();
            debug.ToFile("Generating " + NameDB, DebugLevels.Development, DebugMessageType.Info);
            Progress.RenameProgressBar(Resources.InstancesGenerate, Devices.Count);

            DBResultForm _DBResultForm = new DBResultForm(NameDB, false);

            //add first signal elemet CPU to CPU list
            _CPUList.Add(objects.Signals[0].CPU);
            bool _found;

            //clear all used columns and find CPU list
            for (int _objectIndex = 0; _objectIndex < objects.Signals.Count; _objectIndex++)
            {
                _found = false;
                objects.Signals[_objectIndex].SetValueFromString(string.Empty, KeywordColumn.Used);
                for (int _CPUIndex = 0; _CPUIndex < _CPUList.Count; _CPUIndex++)
                {
                    if (_CPUList[_CPUIndex] == objects.Signals[_objectIndex].CPU)
                    {
                        _found = true;
                        break;
                    }
                }
                if (!_found)
                    _CPUList.Add(data.Signals[_objectIndex].CPU);
            }

            int [] _indexes = new int [_CPUList.Count];
            int _typeCount = 0;
            //go through all device types                    
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                List<List<string>> _decodedDevices = new List<List<string>>();
                for (int _CPUIndex = 0; _CPUIndex < _CPUList.Count; _CPUIndex++)
                {
                    // if more than 1 cpu create visual division
                    if (_CPUList.Count > 1)
                        _decodedDevices.Add(new List<string> { "----------------------------" + _CPUList[_CPUIndex] + "----------------------------" });

                    //restore index
                    if (!Settings.Default.NewTypeResetIndex)
                        _typeCount = _indexes[_CPUIndex];
                    else
                        _typeCount = 0;

                    for (int _objectIndex = 0; _objectIndex < objects.Signals.Count; _objectIndex++)
                    {
                        if (_CPUList[_CPUIndex] != objects.Signals[_objectIndex].CPU)
                            continue;
                        _decodedObject = Devices[_deviceIndex].Decode(ref _typeCount, data, objects.Signals[_objectIndex], false);
                        CopyDecodedList(_decodedObject, _decodedDevices);
                    }

                    //if no devices found clear last line if in multi cpu mode
                    if ((_indexes[_CPUIndex] == _typeCount) && (_CPUList.Count > 1))
                        _decodedDevices.RemoveAt(_decodedDevices.Count-1);

                    //increase index
                    if (!Settings.Default.NewTypeResetIndex)
                        _indexes[_CPUIndex] += _typeCount;
                }
                DataGridView _grid = _DBResultForm.AddData(Devices[_deviceIndex].InstanceType);

                //add grid to form and update grid of device to mach in form
                Devices[_deviceIndex].Grid.ChangeGrid(_grid);
                Devices[_deviceIndex].Grid.PutData(_decodedDevices);
                Progress.UpdateProgressBar(_deviceIndex);
            }

            Progress.HideProgressBar();

            debug.ToFile("Generating " + NameDB + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
            //put updated data with used column
            data.Grid.PutData();

            _DBResultForm.ShowDialog();
        }

        /// <summary>
        /// Edit instances data
        /// </summary>
        public void EditAll()
        {
            if (!CheckDBFiles())
            {
                DialogResult _result = MessageBox.Show(Resources.CreateNew + " " + Level.ToString() + "?", Resources.CreateNew, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language);
                    _newName.ShowDialog();
                    if (_newName.Output != null && _newName.Output != string.Empty)
                    {
                        CreateDBFile(_newName.Output);
                        EditAll();
                    }
                }
                return;
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            List<List<string>> _deviceData = new List<List<string>>();

            Debug debug = new Debug();
            debug.ToFile("Editing " + NameDB, DebugLevels.Development, DebugMessageType.Info);
            Progress.RenameProgressBar(Resources.InstancesEditGenerate, Devices.Count);

            DBResultForm _DBResultForm = new DBResultForm(NameDB + " Edit", true);

            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                _deviceData = Devices[_deviceIndex].ConvertDataToList();
                DataGridView _grid = _DBResultForm.AddData(Devices[_deviceIndex].InstanceType);

                //add grid to form and update grid of device to mach in form
                Devices[_deviceIndex].Grid.ChangeGrid(_grid);
                Devices[_deviceIndex].Grid.PutData(_deviceData);

                Progress.UpdateProgressBar(_deviceIndex);
            }
            Progress.HideProgressBar();

            debug.ToFile("Editing " + NameDB + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            _DBResultForm.ShowDialog();

            GetDeviceTypesFromGrid(_DBResultForm.DBTabControl);

            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                Devices[_deviceIndex].Grid.GetData();
                Devices[_deviceIndex].Grid.SaveToFile();
            }
        }
    }
}