using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
                    Debug debug = new Debug();
                    const string debugText = "DBGeneral.DBGeneral";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(level), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(level) + " is not created for this element");
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
            string fullPath = Directory + "\\" + fileName + "." + FileExtension;

            Debug debug = new Debug();
            debug.ToFile(Resources.CreateNew + " " + Level.ToString() + ": " + fullPath, DebugLevels.High, DebugMessageType.Info);

            ExcelWriter excel = new ExcelWriter(fullPath);

            //write data
            for (int i = 0; i < data.Count; i++)
                excel.Write(data[i], 1, 1 + i);

            excel.Save();
            excel.Dispose();

            debug.ToFile(Resources.CreateNew + " " + Level.ToString() + ": " + fullPath + Resources.Finished, DebugLevels.High, DebugMessageType.Info);

            return DBFileExists(fullPath);
        }

        /// <summary>
        /// Check if database files exists and ask if needed to create
        /// </summary>
        /// <param name="fileName">file to check and create</param>
        /// <returns>DB files exists</returns>
        public bool DBFileExists(string fileName)
        {
            //get file in folder            
            //           string[] files = System.IO.Directory.GetFiles(Directory, fileName + "." + FileExtension);
            return File.Exists(fileName + "." + FileExtension);
        }

        /// <summary>
        /// Check if database files exists
        /// </summary>
        /// <returns>DB files exists</returns>
        private bool DBFilesExists()
        {
            //get files in folder
            string[] files = System.IO.Directory.GetFiles(Directory, "*" + FileExtension);
            return files.Length > 0;
        }

        /// <summary>
        /// Get DB file list
        /// </summary>
        /// <returns>list of files in database with needed extension </returns>
        public List<string> GetDBFileList()
        {
            List<string> filesList = new List<string>();

            string[] files = System.IO.Directory.GetFiles(Directory, "*" + FileExtension);
            if (files.Length == 0)
                return filesList;

            foreach (string file in files)
                filesList.Add(file.Replace(Directory + "\\", string.Empty).Replace("." + FileExtension, string.Empty));

            return filesList;
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

            Debug debug = new Debug();
            debug.ToFile(Resources.CreateNew + " " + Level.ToString() + " " + folderName + " " + NameDB, DebugLevels.High, DebugMessageType.Info);

            System.IO.Directory.CreateDirectory(folderPath);
            return DBFolderExists(folderName);
        }

        /// <summary>
        /// Get DB folder list
        /// </summary>
        /// <returns>list of folders in database </returns>
        public List<string> GetDBFolderList()
        {
            List<string> folderList = new List<string>();

            string[] folders = System.IO.Directory.GetDirectories(BaseDirectory);
            if (folders.Length == 0)
                return folderList;

            foreach (string folder in folders)
                folderList.Add(folder.Replace(BaseDirectory, string.Empty).Replace("\\", string.Empty));

            return folderList;
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

            List<string> files = GetDBFileList();

            string fileName;
            string type;
            string fullType;

            foreach (string file in files)
            {
                fileName = Directory + "\\" + file + "." + FileExtension;
                type = file.Split('(')[0];
                fullType = file;

                //assign dummy grid
                DataGridView grid = new DataGridView();
                DBGeneralType instance = new DBGeneralType(NameDB, fullType, type, FileExtension, Progress, grid);
                DataTable fileData = instance.File.LoadFromFile(fileName);
                instance.SetData(fileData);

                Devices.Add(instance);
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

            string type;
            string fullType;
            //go through all CPU tab control pages
            foreach (TabPage mainTabPage in mainTabControl.TabPages)
            {
                TabControl cpuTab = (TabControl)mainTabPage.Controls[0];

                //go through all tab control pages
                foreach (TabPage cpuPage in cpuTab.TabPages)
                {
                    type = cpuPage.Name;
                    fullType = cpuPage.Text;

                    DataGridView grid = (DataGridView)cpuPage.Controls[0];
                    DBGeneralType instance = new DBGeneralType(NameDB, fullType, type, FileExtension, Progress, grid);
                    DataTable fileData = instance.Grid.GetData();
                    instance.SetData(fileData);

                    Devices.Add(instance);
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
                    const string debugText = "DBGeneral.GetCPUList";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
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
                DialogResult result = MessageBox.Show(Resources.CreateNew + " " + NameDB + "?", Resources.CreateNew, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                NewName newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language, string.Empty, true);

                newName.ShowDialog();
                string fileName = newName.Output;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (DBFileExists(fileName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<string> _data = new List<string>() { "" };
                CreateDBFile(newName.Output, _data);
                DecodeAll(data, objects, modules, addresses);
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            Debug debug = new Debug();
            string debugText = "Generating " + NameDB;
            debug.ToFile(debugText, DebugLevels.High, DebugMessageType.Info);
            Progress.RenameProgressBar(debugText, Devices.Count);

            DBResultForm dbResultForm = new DBResultForm(NameDB, false, Base, Directory, FileExtension);
            List<string> cpuList = GetCPUList(objects, modules, addresses);

            int typeCount = 0;
            int progress = 0;
            //go through all device types
            foreach (DBGeneralType device in Devices)
            {
                foreach (string cpu in cpuList)
                {
                    DataTable decodedDevices = new DataTable();
                    typeCount = 0;
                    switch (Base)
                    {
                        case BaseTypes.ModuleCPU:
                            foreach (ModuleSignal moduleSignal in modules.Signals)
                            {
                                if (cpu != moduleSignal.CPU)
                                    continue;

                                device.Decode(decodedDevices, ref typeCount, data, null, moduleSignal, null, Base);

                                addresses.PutDataToElement(moduleSignal.CPU, ResourcesUI.Modules, moduleSignal.ModuleType, moduleSignal.ModuleName,
                                        device.ObjectVariableType, device.MemoryArea, device.Address, device.AddressSize);
                            }
                            break;

                        case BaseTypes.ModuleSCADA:
                            foreach (AddressObject addressObject in addresses.Signals)
                            {
                                if (cpu != addressObject.CPU)
                                    continue;

                                device.Decode(decodedDevices, ref typeCount, data, null, null, addressObject, Base);

                                addresses.PutDataToElement(addressObject.CPU, ResourcesUI.Modules, addressObject.ObjectType, addressObject.ObjectName,
                                        device.ObjectVariableType, device.MemoryArea, device.Address, device.AddressSize);
                            }
                            break;

                        case BaseTypes.ObjectsCPU:
                            foreach (ObjectSignal objectSignal in objects.Signals)
                            {
                                if (cpu != objectSignal.CPU)
                                    continue;

                                device.Decode(decodedDevices, ref typeCount, data, objectSignal, null, null, Base);

                                addresses.PutDataToElement(objectSignal.CPU, ResourcesUI.Objects, objectSignal.ObjectType, objectSignal.KKS,
                                        device.ObjectVariableType, device.MemoryArea, device.Address, device.AddressSize);
                            }
                            break;

                        case BaseTypes.ObjectSCADA:
                            foreach (AddressObject addressObject in addresses.Signals)
                            {
                                if (cpu != addressObject.CPU)
                                    continue;

                                device.Decode(decodedDevices, ref typeCount, data, null, null, addressObject, Base);

                                addresses.PutDataToElement(addressObject.CPU, ResourcesUI.Objects, addressObject.ObjectType, addressObject.ObjectName,
                                        device.ObjectVariableType, device.MemoryArea, device.Address, device.AddressSize);
                            }
                            break;

                        default:
                            debugText = "DBGeneral.DecodeAll";
                            debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
                    }
                    DataGridView grid = dbResultForm.AddData(cpu, device.FullDBType, device.DBType);

                    //add grid to form and update grid of device to match in form
                    device.GridResult.ChangeGrid(grid);
                    device.GridResult.PutData(decodedDevices);
                }
                progress++;
                Progress.UpdateProgressBar(progress);
            }
            Progress.HideProgressBar();

            debug.ToFile(debugText + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
            //put updated data with used column
            data.PutDataToGrid(false);
            addresses.PutDataToGrid(true);

            dbResultForm.ShowDialog();
        }

        /// <summary>
        /// Edit database data
        /// Get all DB files and load them to grid
        /// Edit,copy delete lines and DB files
        /// After edit save those files
        /// </summary>
        public void EditAll()
        {
            string fileName;

            if (!CheckDBFiles())
            {
                DialogResult result = MessageBox.Show(Resources.CreateNew + " " + NameDB + "?", Resources.CreateNew, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                NewName newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language, string.Empty, true);

                newName.ShowDialog();
                fileName = newName.Output;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (DBFileExists(fileName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<string> data = new List<string>() { "" };
                CreateDBFile(newName.Output, data);
                EditAll();
            }
            GetDeviceTypesFromFile();

            if (Devices.Count < 1)
                return;

            DataTable deviceData;

            Debug debug = new Debug();
            string debugText = "Editing " + NameDB;
            debug.ToFile(debugText, DebugLevels.Development, DebugMessageType.Info);
            Progress.RenameProgressBar(debugText, Devices.Count);

            DBResultForm dbResultForm = new DBResultForm(NameDB + " Edit", true, Base, Directory, FileExtension);

            int progress = 0;
            //go through all device types
            foreach (DBGeneralType device in Devices)
            {
                deviceData = device.ConvertDataToList();
                DataGridView grid = dbResultForm.AddData("", device.FullDBType, device.DBType);

                //add grid to form and update grid of device to match in form
                device.Grid.ChangeGrid(grid);
                device.Grid.PutData(deviceData);

                progress++;
                Progress.UpdateProgressBar(progress);
            }
            Progress.HideProgressBar();

            debug.ToFile(debugText + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);

            dbResultForm.ShowDialog();

            GetDeviceTypesFromGrid(dbResultForm.DBTabControlCPU);

            //go through all device types
            foreach (DBGeneralType device in Devices)
            {
                fileName = Directory + "\\" + device.FullDBType + "." + FileExtension;
                DataTable data = device.Grid.GetData();

                //remove empty columns
                for (int column = data.Columns.Count - 1; column >= 0; column--)
                {
                    bool isEmpty = true;
                    for (int row = 0; row < data.Rows.Count; row++)
                    {
                        isEmpty = string.IsNullOrEmpty(GeneralFunctions.GetDataTableValue(data, row, column));
                        if (!isEmpty)
                            break;
                    }

                    if (isEmpty)
                        data.Columns.RemoveAt(column);
                }
                device.File.SaveToFile(fileName, data);
            }
        }
    }
}