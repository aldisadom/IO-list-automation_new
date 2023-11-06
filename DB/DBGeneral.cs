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

    public enum BaseTypes
    {
        Module,
        SCADA,
        Objects,
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

        private BaseTypes Base { get; }

        public DBGeneral(ProgressIndication progress, string nameDB, string fileExtension, DBTypeLevel level, BaseTypes inputBase)
        {
            Progress = progress;
            NameDB = nameDB;
            FileExtension = fileExtension;
            Devices = new List<DBGeneralType>();
            Level = level;
            Base = inputBase;

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
                    const string _debugText = "DBGeneral.DBGeneral";
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(level), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + nameof(level) + " is not created for this element");
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
            if (_files.Length == 0)
                return _filesList;

            for (int i = 0; i < _files.Length; i++)
                _filesList.Add(_files[i].Replace(Directory + "\\", string.Empty).Replace("." + FileExtension, string.Empty));

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
            if (_folders.Length == 0)
                return _folderList;

            for (int i = 0; i < _folders.Length; i++)
                _folderList.Add(_folders[i].Replace(BaseDirectory, string.Empty).Replace("\\", string.Empty));

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
            string _fullType;

            for (int i = 0; i < _files.Count; i++)
            {
                _fileName = Directory + "\\" + _files[i] + "." + FileExtension;
                _type = _files[i].Split('(')[0];
                _fullType = _files[i];

                //assign dummy grid
                DataGridView _grid = new DataGridView();
                DBGeneralType _instance = new DBGeneralType(NameDB, _fullType, _type, FileExtension, Progress, _grid);
                List<List<string>> _fileData = _instance.Grid.LoadFromFileToMemory(_fileName);
                _instance.SetData(_fileData);

                Devices.Add(_instance);
            }

            debug.ToFile("Searching " + NameDB + " configuration from file - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get all instances from grid to Devices object
        /// </summary>
        /// <param name="tabControl">tab control where is pages and grids</param>
        private void GetDeviceTypesFromGrid(TabControl tabControl)
        {
            Debug debug = new Debug();
            debug.ToFile("Searching " + NameDB + " configuration from grid", DebugLevels.Development, DebugMessageType.Info);
            Devices.Clear();

            string _type;
            string _fullType;
            //go through all tab control pages
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                _type = tabControl.TabPages[i].Name;
                _fullType = tabControl.TabPages[i].Text;

                DataGridView _grid = (DataGridView)tabControl.TabPages[i].Controls[0];
                DBGeneralType _instance = new DBGeneralType(NameDB, _fullType, _type, FileExtension, Progress, _grid);
                List<List<string>> _fileData = _instance.Grid.GetData(false);
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
            if (_input == null)
                return;

            for (int i = 0; i < _input.Count; i++)
                _output.Add(_input[i]);
        }

        /// <summary>
        /// Get CPU list of Base
        /// </summary>
        /// <param name="objects">object signals</param>
        /// <param name="modules">modules signals</param>
        /// <param name="addresses">addresses signals</param>
        /// <returns>Unique CPU list</returns>
        private List<string> GetCPUList(ObjectsClass objects, ModuleClass modules, AddressesClass addresses)
        {
            switch (Base)
            {
                case BaseTypes.Module:
                    return modules.GetCPUList();
                case BaseTypes.SCADA:
                    return addresses.GetCPUList();
                case BaseTypes.Objects:
                    return objects.GetCPUList();
                default:
                    const string _debugText = "DBGeneral.GetCPUList";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + nameof(Base) + " is not created for this element");
            }
        }

        /// <summary>
        /// Decode all objects for this DB type and show in grid
        /// Update addresses if necessary
        /// </summary>
        /// <param name="data">data signals</param>
        /// <param name="objects">object signals</param>
        /// <param name="modules">modules signals</param>
        /// <param name="addresses">addresses signals</param>
        public void DecodeAll(DataClass data, ObjectsClass objects, ModuleClass modules, AddressesClass addresses)
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
                    DecodeAll(data, objects, modules, addresses);
                }
                return;
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            List<List<string>> _decodedObject;

            Debug _debug = new Debug();
            string _debugText = "Generating " + NameDB;
            _debug.ToFile(_debugText, DebugLevels.Development, DebugMessageType.Info);
            Progress.RenameProgressBar(_debugText, Devices.Count);

            DBResultForm _DBResultForm = new DBResultForm(NameDB, false, Base, Directory, FileExtension);
            List<string> _CPUList = GetCPUList(objects,modules,addresses);

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

                    _typeCount = 0;
                    switch (Base)
                    {
                        case BaseTypes.Module:
                            for (int _moduleIndex = 0; _moduleIndex < modules.Signals.Count; _moduleIndex++)
                            {
                                ModuleSignal _module = modules.Signals[_moduleIndex];

                                if (_CPUList[_CPUIndex] != _module.CPU)
                                    continue;
                                _decodedObject = Devices[_deviceIndex].Decode(ref _typeCount, data, null, _module);
                                CopyDecodedList(_decodedObject, _decodedDevices);

                                if (_decodedObject == null)
                                    continue;

                                //update addresses for this element
                                addresses.PutDataToElement(_module.CPU, ResourcesUI.Modules, _module.ModuleType, _module.ModuleName, Devices[_deviceIndex].ObjectVariableType
                                    , Devices[_deviceIndex].MemoryArea, Devices[_deviceIndex].Address, Devices[_deviceIndex].AddressSize);
                            }
                            break;
                        case BaseTypes.SCADA:
                            break;
                        case BaseTypes.Objects:
                            for (int _objectIndex = 0; _objectIndex < objects.Signals.Count; _objectIndex++)
                            {
                                ObjectSignal _object = objects.Signals[_objectIndex];
                                if (_CPUList[_CPUIndex] != _object.CPU)
                                    continue;
                                _decodedObject = Devices[_deviceIndex].Decode(ref _typeCount, data, _object, null);
                                CopyDecodedList(_decodedObject, _decodedDevices);

                                if (_decodedObject == null)
                                    continue;

                                //update addresses for this element
                                addresses.PutDataToElement(_object.CPU, ResourcesUI.Objects, _object.ObjectType, _object.KKS, Devices[_deviceIndex].ObjectVariableType
                                    , Devices[_deviceIndex].MemoryArea, Devices[_deviceIndex].Address, Devices[_deviceIndex].AddressSize);
                            }
                            break;
                        default:
                            _debugText = "DBGeneral.DecodeAll";
                            _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(_debugText + "." + nameof(Base) + " is not created for this element");
                    }

                    //if no devices found clear last line if in multi cpu mode
                    if (_CPUList.Count > 1)
                    {
                        if (_typeCount == 0)
                            _decodedDevices.RemoveAt(_decodedDevices.Count - 1);
                        else
                            _decodedDevices.Add(new List<string> { string.Empty });
                    }
                }
                DataGridView _grid = _DBResultForm.AddData(Devices[_deviceIndex].FullDBType, Devices[_deviceIndex].DBType);

                //add grid to form and update grid of device to match in form
                Devices[_deviceIndex].GridResult.ChangeGrid(_grid);
                Devices[_deviceIndex].GridResult.PutData(_decodedDevices);
                Progress.UpdateProgressBar(_deviceIndex);
            }
            Progress.HideProgressBar();

            _debug.ToFile(_debugText + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
            //put updated data with used column
            data.PutDataToGrid(false);
            addresses.PutDataToGrid(true);

            _DBResultForm.ShowDialog();
        }

        /// <summary>
        /// Edit database data
        /// Get all DB files and load them to grid
        /// Edit,copy delete lines and DB files
        /// After edit save those files
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

            DBResultForm _DBResultForm = new DBResultForm(NameDB + " Edit", true, Base, Directory, FileExtension);

            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                _deviceData = Devices[_deviceIndex].ConvertDataToList();
                DataGridView _grid = _DBResultForm.AddData(Devices[_deviceIndex].FullDBType, Devices[_deviceIndex].DBType);

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
                _fileName = Directory + "\\" + Devices[_deviceIndex].FullDBType + "." + FileExtension;
                Devices[_deviceIndex].Grid.GetData(false);
                Devices[_deviceIndex].Grid.SaveToFile(_fileName);
            }
        }
    }
}