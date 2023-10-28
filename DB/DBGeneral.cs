using IO_list_automation_new.DB;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal enum DBTypeLevel
    {
        Base,
        CPU,
        SCADA,
    }

    internal class DBGeneralSignal
    {
        public List<string> Line { get; private set; }

        public string TagType { get; private set; }
        public string MemoryArea { get; private set; }
        public string Address { get; private set; }

        private int Index = 0;

        public DBGeneralSignal()
        {
            TagType = string.Empty;
            MemoryArea = string.Empty;
            Address = string.Empty;
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
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>IO tag</returns>
        private string DecodeIO(DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            Index++;
            if (simulation)
                return "KKS." + Line[Index];

            for (int i = 0; i < data.Signals.Count; i++)
            {
                if ((objectSignal.KKS == data.Signals[i].KKS) && (data.Signals[i].Function == Line[Index]))
                    return data.Signals[i].Tag;
            }
            return string.Empty;
        }

        /// <summary>
        /// Find requested column in data
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>column value</returns>
        private string DecodeData(DataClass data, ObjectSignal objectSignal, bool simulation)
        {
            Index++;
            if (simulation)
                return "Data." + Line[Index];

            for (int i = 0; i < data.Signals.Count; i++)
            {
                if (objectSignal.KKS == data.Signals[i].KKS)
                    return data.Signals[i].GetValueString(Line[Index], false);
            }
            return string.Empty;
        }

        /// <summary>
        /// find requested data in objects
        /// </summary>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>column value</returns>
        private string DecodeObjects(ObjectSignal objectSignal, bool simulation)
        {
            Index++;
            if (simulation)
                return "Object." + Line[Index];

            return objectSignal.GetValueString(Line[Index], false);
        }

        /// <summary>
        /// find requested data in objects
        /// </summary>
        /// <param name="moduleSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>column value</returns>
        private string DecodeModules(ModuleSignal moduleSignal, bool simulation)
        {
            Index++;
            if (simulation)
                return "Module." + Line[Index];

            return moduleSignal.GetValueString(Line[Index], false);
        }

        /// <summary>
        /// Set Tag of this line
        /// </summary>
        /// <param name="tagType">tag type</param>
        private void DecodeTagType(string tagType)
        {
            Index++;
            TagType = tagType;
        }

        /// <summary>
        /// Calculate value
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="indexLine">index of coded line</param>
        /// <param name="line">coded line</param>
        /// <returns>value calculated with offset and multiplier</returns>
        private string DecodeIndex(int index, int indexLine, List<string> line)
        {
            Index += 3;

            MemoryArea = line[indexLine];
            if (!int.TryParse(line[indexLine + 1], out int _value))
                return string.Empty;

            int _returnValue = index * _value;
            if (int.TryParse(line[indexLine + 2], out _value))
                _returnValue += _value;

            Address = _returnValue.ToString();
            return Address;
        }

        /// <summary>
        /// decoding if statement from 4 cells
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine">decoded list</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        /// <param name="simulation">simulate data</param>
        private void DecodeIf(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, bool simulation)
        {
            Index++;
            string _text = Line[Index];
            string _returnPart1;
            int _newIndex;
            Debug _debug = new Debug();
            string text;

            switch (_text)
            {
                case KeywordDBChoices.Data:
                    _returnPart1 = DecodeData(data, objectSignal, simulation);
                    break;

                case KeywordDBChoices.Object:
                    _returnPart1 = DecodeObjects(objectSignal, simulation);
                    break;

                case KeywordDBChoices.Modules:
                    _returnPart1 = DecodeModules(moduleSignal, simulation);
                    break;

                case KeywordDBChoices.IO:
                    _returnPart1 = DecodeIO(data, objectSignal, simulation);
                    break;

                default:
                    text = "DBGeneralSignal.DecodeIf";

                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + _text + " is not created for this element");
            }

            _text = Line[Index];
            bool _ifTrue;

            switch (_text)
            {
                case KeywordDBChoices.IsEmpty:
                    _ifTrue = string.IsNullOrEmpty(_returnPart1);
                    break;

                case KeywordDBChoices.IsNotEmpty:
                    _ifTrue = !string.IsNullOrEmpty(_returnPart1);
                    break;

                default:
                    text = "DBGeneralSignal.DecodeIf";

                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + _text + " is not created for this element");
            }

            //if = true
            if (_ifTrue)
            {
                Index++;
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, simulation);

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
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, simulation);
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

            switch (_text)
            {
                case KeywordDBChoices.If:
                    //peek for variable
                    _newIndex = DecodePeek(_newIndex);
                    //skip operation
                    _newIndex++;
                    //peek for true
                    _newIndex = DecodePeek(_newIndex);
                    //peek for false
                    _newIndex = DecodePeek(_newIndex);
                    return _newIndex;

                case KeywordDBChoices.Tab:
                    return _newIndex;

                case KeywordDBChoices.Data:
                case KeywordDBChoices.Object:
                case KeywordDBChoices.Modules:
                case KeywordDBChoices.IO:
                case KeywordDBChoices.Text:
                case KeywordDBChoices.TagType:
                    return _newIndex++;

                case KeywordDBChoices.Index:
                    return _newIndex += 3;

                default:
                    Debug _debug = new Debug();
                    const string text = "DBGeneralSignal.DecodePeek.IF";
                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + _text + " is not created for this element");
            }
        }

        /// <summary>
        /// 1 cell decoding
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine"></param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        /// <param name="simulation">simulate data</param>
        private void DecodeCell(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, bool simulation)
        {
            string _text = Line[Index];

            switch (_text)
            {
                case KeywordDBChoices.If:
                    DecodeIf(index, decodedLine, data, objectSignal, moduleSignal, simulation);
                    break;

                case KeywordDBChoices.Tab:
                    decodedLine.Add(string.Empty);
                    break;

                case KeywordDBChoices.Data:
                    decodedLine[decodedLine.Count - 1] += DecodeData(data, objectSignal, simulation);
                    break;

                case KeywordDBChoices.Index:
                    decodedLine[decodedLine.Count - 1] += DecodeIndex(index + 1, Index, Line);
                    break;

                case KeywordDBChoices.Object:
                    decodedLine[decodedLine.Count - 1] += DecodeObjects(objectSignal, simulation);
                    break;

                case KeywordDBChoices.Modules:
                    decodedLine[decodedLine.Count - 1] += DecodeModules(moduleSignal, simulation);
                    break;

                case KeywordDBChoices.TagType:
                    DecodeTagType(Line[Index]);
                    break;

                case KeywordDBChoices.Text:
                    Index++;
                    _text = Line[Index];
                    decodedLine[decodedLine.Count - 1] += _text;
                    break;

                case KeywordDBChoices.IO:
                    decodedLine[decodedLine.Count - 1] += DecodeIO(data, objectSignal, simulation);
                    break;

                case KeywordDBChoices.None:
                    break;

                default:
                    Debug _debug = new Debug();
                    const string text = "DBGeneralSignal.DecodeCell";
                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + _text + " is not created for this element");
            }
        }

        /// <summary>
        /// Decode line
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="simulation">simulate data</param>
        /// <returns>decoded text line</returns>
        public List<string> DecodeLine(int index, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, bool simulation)
        {
            Index = 0;
            List<string> decodedLine = new List<string>() { string.Empty };

            for (int i = 0; i < Line.Count; i++)
            {
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, simulation);
                i = Index;
                Index++;
            }

            return decodedLine;
        }
    }

    internal class DBGeneralType
    {
        public string Name { get; }

        public string DBType { get; }

        public List<DBGeneralSignal> Data;

        public ProgressIndication Progress { get; set; }

        public GeneralGrid Grid { get; }
        public GeneralGrid GridResult { get; }

        public DBGeneralType(string name, string dbType, string fileExtension, ProgressIndication progress, DataGridView grid)
        {
            Name = name;
            DBType = dbType;
            Data = new List<DBGeneralSignal>();
            Progress = progress;
            Grid = new GeneralGrid(Name, GridTypes.EditableDB, fileExtension, Progress, grid, new ColumnList());
            GridResult = new GeneralGrid(Name, GridTypes.DB, fileExtension, Progress, new DataGridView(), new ColumnList());
        }

        /// <summary>
        /// Decode one object of this type
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object to be decoded</param>
        /// <param name="moduleSignal">module to be decoded</param>
        /// <returns>decoded text</returns>
        public List<List<string>> Decode(ref int index, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, bool simulation)
        {
            //if type mismatch skip
            if (objectSignal.ObjectType == DBType)
            {
                List<List<string>> _decodedObject = new List<List<string>>();
                for (int i = 0; i < Data.Count; i++)
                    _decodedObject.Add(Data[i].DecodeLine(index, data, objectSignal, moduleSignal, simulation));

                index++;
                return _decodedObject;
            }
            return null;
        }

        /// <summary>
        /// Convert DBGeneralSignal to List of List of string
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
        public string FileExtension { get; }
        private ProgressIndication Progress { get; }

        private readonly string Directory;

        private readonly string BaseDirectory;

        private readonly string NameDB;

        private readonly DBTypeLevel Level;

        private bool ModuleBased { get; set; }

        public DBGeneral(ProgressIndication progress, string nameDB, string fileExtension, DBTypeLevel level, bool moduleBased)
        {
            Progress = progress;
            NameDB = nameDB;
            FileExtension = fileExtension;
            Devices = new List<DBGeneralType>();
            Level = level;
            ModuleBased = moduleBased;

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
                    const string text = "DBGeneral.DBGeneral";
                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + nameof(level), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + nameof(level) + " is not created for this element");
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
            return _files.Length > 0;
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
                    _filesList.Add(_files[i].Replace(Directory + "\\", string.Empty).Replace("." + FileExtension, string.Empty));
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

            Debug _debug = new Debug();
            _debug.ToFile(Resources.CreateNew + " " + Level.ToString() + " " + folderName + " " + NameDB, DebugLevels.Development, DebugMessageType.Info);

            System.IO.Directory.CreateDirectory(folderPath);
            return DBFolderExists(folderName);
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
            debug.ToFile("Searching " + NameDB + " configuration from file", DebugLevels.Development, DebugMessageType.Info);
            Devices.Clear();

            List<string> _files = GetDBFileList();

            string _fileName;
            string _type;

            for (int i = 0; i < _files.Count; i++)
            {
                _fileName = Directory + "\\" + _files[i] + "." + FileExtension;
                _type = _files[i];

                //assign dummy grid
                DataGridView _grid = new DataGridView();
                DBGeneralType _instance = new DBGeneralType(NameDB, _type, FileExtension, Progress, _grid);
                List<List<string>> _fileData = _instance.Grid.LoadFromFileToMemory(_fileName);
                _instance.SetData(_fileData);

                Devices.Add(_instance);
            }

            debug.ToFile("Searching " + NameDB + " configuration from file - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get all instances from grid to Devices
        /// </summary>
        private void GetDeviceTypesFromGrid(TabControl tabControl)
        {
            Debug debug = new Debug();
            debug.ToFile("Searching " + NameDB + " configuration from grid", DebugLevels.Development, DebugMessageType.Info);
            Devices.Clear();

            string _type;
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                _type = tabControl.TabPages[i].Name;

                DataGridView _grid = (DataGridView)tabControl.TabPages[i].Controls[0];
                DBGeneralType _instance = new DBGeneralType(NameDB, _type, FileExtension, Progress, _grid);
                List<List<string>> _fileData = _instance.Grid.GetData();
                _instance.SetData(_fileData);

                Devices.Add(_instance);
            }

            debug.ToFile("Searching " + NameDB + " configuration from grid - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
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
        /// <param name="modules">modules signals</param>
        public void DecodeAll(DataClass data, ObjectsClass objects, ModuleClass modules)
        {
            if (!CheckDBFiles())
            {
                DialogResult _result = MessageBox.Show(Resources.CreateNew + " " + NameDB + "?", Resources.CreateNew, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result != DialogResult.Yes)
                    return;

                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language);
                _newName.ShowDialog();
                if (!string.IsNullOrEmpty(_newName.Output))
                {
                    CreateDBFile(_newName.Output);
                    DecodeAll(data, objects, modules);
                }
                return;
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            List<List<string>> _decodedObject;
            List<string> _CPUList = new List<string>();

            Debug debug = new Debug();
            string _debugText = "Generating " + NameDB;
            debug.ToFile(_debugText, DebugLevels.Development, DebugMessageType.Info);
            Progress.RenameProgressBar(_debugText, Devices.Count);

            DBResultForm _DBResultForm = new DBResultForm(NameDB, false, ModuleBased);

            //add first signal element CPU to CPU list
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
                    _CPUList.Add(objects.Signals[_objectIndex].CPU);
            }

            int[] _indexes = new int[_CPUList.Count];
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

                    ModuleSignal _dummyModule = new ModuleSignal();
                    for (int _objectIndex = 0; _objectIndex < objects.Signals.Count; _objectIndex++)
                    {
                        if (_CPUList[_CPUIndex] != objects.Signals[_objectIndex].CPU)
                            continue;
                        _decodedObject = Devices[_deviceIndex].Decode(ref _typeCount, data, objects.Signals[_objectIndex], _dummyModule, false);
                        CopyDecodedList(_decodedObject, _decodedDevices);
                    }

                    //if no devices found clear last line if in multi cpu mode
                    if ((_indexes[_CPUIndex] == _typeCount) && (_CPUList.Count > 1))
                        _decodedDevices.RemoveAt(_decodedDevices.Count - 1);

                    //increase index
                    if (!Settings.Default.NewTypeResetIndex)
                        _indexes[_CPUIndex] += _typeCount;
                }
                DataGridView _grid = _DBResultForm.AddData(Devices[_deviceIndex].DBType);

                //add grid to form and update grid of device to match in form
                Devices[_deviceIndex].GridResult.ChangeGrid(_grid);
                Devices[_deviceIndex].GridResult.PutData(_decodedDevices);
                Progress.UpdateProgressBar(_deviceIndex);
            }

            Progress.HideProgressBar();

            debug.ToFile(_debugText + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
            //put updated data with used column
            data.PutDataToGrid();

            _DBResultForm.ShowDialog();
        }

        /// <summary>
        /// Edit instances data
        /// </summary>
        public void EditAll()
        {
            if (!CheckDBFiles())
            {
                DialogResult _result = MessageBox.Show(Resources.CreateNew + " " + NameDB + "?", Resources.CreateNew, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result != DialogResult.Yes)
                    return;

                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language);
                _newName.ShowDialog();
                if (!string.IsNullOrEmpty(_newName.Output))
                {
                    CreateDBFile(_newName.Output);
                    EditAll();
                }
                return;
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            List<List<string>> _deviceData;

            Debug debug = new Debug();
            string _debugText = "Editing " + NameDB;
            debug.ToFile(_debugText, DebugLevels.Development, DebugMessageType.Info);
            Progress.RenameProgressBar(_debugText, Devices.Count);

            DBResultForm _DBResultForm = new DBResultForm(NameDB + " Edit", true,ModuleBased);

            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                _deviceData = Devices[_deviceIndex].ConvertDataToList();
                DataGridView _grid = _DBResultForm.AddData(Devices[_deviceIndex].DBType);

                //add grid to form and update grid of device to match in form
                Devices[_deviceIndex].Grid.ChangeGrid(_grid);
                Devices[_deviceIndex].Grid.PutData(_deviceData);

                Progress.UpdateProgressBar(_deviceIndex);
            }
            Progress.HideProgressBar();

            debug.ToFile(_debugText + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            _DBResultForm.ShowDialog();

            GetDeviceTypesFromGrid(_DBResultForm.DBTabControl);

            string _fileName;
            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                _fileName = Directory + "\\" + Devices[_deviceIndex].DBType + "." + FileExtension;
                Devices[_deviceIndex].Grid.GetData();
                Devices[_deviceIndex].Grid.SaveToFile(_fileName);
            }
        }
    }
}