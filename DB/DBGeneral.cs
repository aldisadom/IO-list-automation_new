using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
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
        ModuleCPU,
        ModuleSCADA,
        ObjectsCPU,
        ObjectSCADA,
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
        /// <param name="data">data to add to file</param>
        /// <returns>DB files exists</returns>
        public bool CreateDBFile(string fileName, List<string> data)
        {
            string _fileName = Directory + "\\" + fileName + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.CreateNew + " " + Level.ToString() + ": " + _fileName, DebugLevels.High, DebugMessageType.Info);

            ExcelWriter _excel = new ExcelWriter(_fileName);

            //write data
            for (int i = 0; i < data.Count; i++)
                _excel.Write(string.Empty, 1, 1+i);

            _excel.Save();
            _excel.Dispose();

            debug.ToFile(Resources.CreateNew + " " + Level.ToString() + ": " + _fileName + Resources.Finished, DebugLevels.High, DebugMessageType.Info);

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
            _debug.ToFile(Resources.CreateNew + " " + Level.ToString() + " " + folderName + " " + NameDB, DebugLevels.High, DebugMessageType.Info);

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
                DataTable _fileData = _instance.File.LoadFromFile(_fileName);
                _instance.SetData(_fileData);

                Devices.Add(_instance);
            }

            debug.ToFile("Searching " + NameDB + " configuration from file - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get all instances from grid to Devices object
        /// </summary>
        /// <param name="mainTabControl">tab control where is pages and grids</param>
        private void GetDeviceTypesFromGrid(TabControl mainTabControl)
        {
            Debug debug = new Debug();
            debug.ToFile("Searching " + NameDB + " configuration from grid", DebugLevels.Development, DebugMessageType.Info);
            Devices.Clear();

            string _type;
            string _fullType;
            //go through all CPU tab control pages
            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
            {
                TabControl _CPUTab = (TabControl)mainTabControl.TabPages[i].Controls[0];

                //go through all tab control pages
                for (int j = 0; j < _CPUTab.TabPages.Count; j++)
                {
                    _type = _CPUTab.TabPages[j].Name;
                    _fullType = _CPUTab.TabPages[j].Text;

                    DataGridView _grid = (DataGridView)_CPUTab.TabPages[j].Controls[0];
                    DBGeneralType _instance = new DBGeneralType(NameDB, _fullType, _type, FileExtension, Progress, _grid);
                    DataTable _fileData = _instance.Grid.GetData(false);
                    _instance.SetData(_fileData);

                    Devices.Add(_instance);
                }
            }
            debug.ToFile("Searching " + NameDB + " configuration from grid - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
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
                case BaseTypes.ModuleCPU:
                    return modules.GetCPUList();

                case BaseTypes.ModuleSCADA:
                case BaseTypes.ObjectSCADA:
                    return addresses.GetCPUList();

                case BaseTypes.ObjectsCPU:
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

                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language,string.Empty, true);

                _newName.ShowDialog();
                string _fileName = _newName.Output;
                if (string.IsNullOrEmpty(_fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (DBFileExists(_fileName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<string> _data = new List<string>() { "" };
                CreateDBFile(_newName.Output, _data);
                DecodeAll(data, objects, modules, addresses);
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            Debug _debug = new Debug();
            string _debugText = "Generating " + NameDB;
            _debug.ToFile(_debugText, DebugLevels.High, DebugMessageType.Info);
            Progress.RenameProgressBar(_debugText, Devices.Count);

            DBResultForm _DBResultForm = new DBResultForm(NameDB, false, Base, Directory, FileExtension);
            List<string> _CPUList = GetCPUList(objects, modules, addresses);

            int _typeCount = 0;
            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                for (int _CPUIndex = 0; _CPUIndex < _CPUList.Count; _CPUIndex++)
                {
                    DataTable _decodedDevices = new DataTable();
                    _typeCount = 0;
                    switch (Base)
                    {
                        case BaseTypes.ModuleCPU:
                            for (int _moduleIndex = 0; _moduleIndex < modules.Signals.Count; _moduleIndex++)
                            {
                                ModuleSignal _module = modules.Signals[_moduleIndex];

                                if (_CPUList[_CPUIndex] != _module.CPU)
                                    continue;

                                Devices[_deviceIndex].Decode(_decodedDevices, ref _typeCount, data, null, _module, null, Base);

                                addresses.PutDataToElement(_module.CPU, ResourcesUI.Modules, _module.ModuleType, _module.ModuleName,
                                        Devices[_deviceIndex].ObjectVariableType, Devices[_deviceIndex].MemoryArea, Devices[_deviceIndex].Address, Devices[_deviceIndex].AddressSize);
                            }
                            break;

                        case BaseTypes.ModuleSCADA:
                            for (int _addressIndex = 0; _addressIndex < addresses.Signals.Count; _addressIndex++)
                            {
                                AddressObject _address = addresses.Signals[_addressIndex];
                                if (_CPUList[_CPUIndex] != _address.CPU)
                                    continue;

                                Devices[_deviceIndex].Decode(_decodedDevices, ref _typeCount, data, null, null, _address, Base);

                                addresses.PutDataToElement(_address.CPU, ResourcesUI.Modules, _address.ObjectType, _address.ObjectName,
                                        Devices[_deviceIndex].ObjectVariableType, Devices[_deviceIndex].MemoryArea, Devices[_deviceIndex].Address, Devices[_deviceIndex].AddressSize);
                            }
                            break;

                        case BaseTypes.ObjectsCPU:
                            for (int _objectIndex = 0; _objectIndex < objects.Signals.Count; _objectIndex++)
                            {
                                ObjectSignal _object = objects.Signals[_objectIndex];
                                if (_CPUList[_CPUIndex] != _object.CPU)
                                    continue;

                                Devices[_deviceIndex].Decode(_decodedDevices, ref _typeCount, data, _object, null, null, Base);

                                addresses.PutDataToElement(_object.CPU, ResourcesUI.Objects, _object.ObjectType, _object.KKS,
                                        Devices[_deviceIndex].ObjectVariableType, Devices[_deviceIndex].MemoryArea, Devices[_deviceIndex].Address, Devices[_deviceIndex].AddressSize);
                            }
                            break;

                        case BaseTypes.ObjectSCADA:
                            for (int _addressIndex = 0; _addressIndex < addresses.Signals.Count; _addressIndex++)
                            {
                                AddressObject _address = addresses.Signals[_addressIndex];
                                if (_CPUList[_CPUIndex] != _address.CPU)
                                    continue;

                                Devices[_deviceIndex].Decode(_decodedDevices, ref _typeCount, data, null, null, _address, Base);

                                addresses.PutDataToElement(_address.CPU, ResourcesUI.Objects, _address.ObjectType, _address.ObjectName,
                                        Devices[_deviceIndex].ObjectVariableType, Devices[_deviceIndex].MemoryArea, Devices[_deviceIndex].Address, Devices[_deviceIndex].AddressSize);
                            }
                            break;

                        default:
                            _debugText = "DBGeneral.DecodeAll";
                            _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(_debugText + "." + nameof(Base) + " is not created for this element");
                    }
                    DataGridView _grid = _DBResultForm.AddData(_CPUList[_CPUIndex],Devices[_deviceIndex].FullDBType, Devices[_deviceIndex].DBType);

                    //add grid to form and update grid of device to match in form
                    Devices[_deviceIndex].GridResult.ChangeGrid(_grid);
                    Devices[_deviceIndex].GridResult.PutData(_decodedDevices);
                }
                Progress.UpdateProgressBar(_deviceIndex);
            }
            Progress.HideProgressBar();

            _debug.ToFile(_debugText + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
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
            string _fileName;

            if (!CheckDBFiles())
            {
                DialogResult _result = MessageBox.Show(Resources.CreateNew + " " + NameDB + "?", Resources.CreateNew, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result != DialogResult.Yes)
                    return;

                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language, string.Empty, true);

                _newName.ShowDialog();
                _fileName = _newName.Output;
                if (string.IsNullOrEmpty(_fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (DBFileExists(_fileName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<string> _data = new List<string>() { "" };
                CreateDBFile(_newName.Output,_data);
                EditAll();
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            DataTable _deviceData;

            Debug debug = new Debug();
            string _debugText = "Editing " + NameDB;
            debug.ToFile(_debugText, DebugLevels.Development, DebugMessageType.Info);
            Progress.RenameProgressBar(_debugText, Devices.Count);

            DBResultForm _DBResultForm = new DBResultForm(NameDB + " Edit", true, Base, Directory, FileExtension);

            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                _deviceData = Devices[_deviceIndex].ConvertDataToList();
                DataGridView _grid = _DBResultForm.AddData("",Devices[_deviceIndex].FullDBType, Devices[_deviceIndex].DBType);

                //add grid to form and update grid of device to match in form
                Devices[_deviceIndex].Grid.ChangeGrid(_grid);
                Devices[_deviceIndex].Grid.PutData(_deviceData);

                Progress.UpdateProgressBar(_deviceIndex);
            }
            Progress.HideProgressBar();

            debug.ToFile(_debugText + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            _DBResultForm.ShowDialog();

            GetDeviceTypesFromGrid(_DBResultForm.DBTabControlCPU);

            //go through all device types
            for (int _deviceIndex = 0; _deviceIndex < Devices.Count; _deviceIndex++)
            {
                _fileName = Directory + "\\" + Devices[_deviceIndex].FullDBType + "." + FileExtension;
                DataTable _data = Devices[_deviceIndex].Grid.GetData(false);

                //remove empty columns
                for (int column = _data.Columns.Count-1; column >=0 ; column--)
                {
                    bool _isEmpty = true;
                    for (int row = 0; row < _data.Rows.Count; row++)
                    {
                        _isEmpty = string.IsNullOrEmpty(GeneralFunctions.GetDataTableValue(_data, row, column));
                        if (!_isEmpty)
                            break;
                    }

                    if (_isEmpty)
                        _data.Columns.RemoveAt(column);
                }
                Devices[_deviceIndex].File.SaveToFile(_fileName, _data);
            }
        }
    }
}