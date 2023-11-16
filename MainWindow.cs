using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IO_list_automation_new
{
    public partial class MainWindow : Form
    {
        private ProgressIndication Progress;

        public MainWindow()
        {
            InitializeComponent();
            Progress = new ProgressIndication(ProgressBars, ProgressLabel);

            if (Settings.Default.AutoLoad && Settings.Default.AutoLoadFile.Length > 0)
            {
                Debug debug = new Debug();
                debug.ToFile(Resources.FileAutoLoad, DebugLevels.Minimum, DebugMessageType.Info);

                DesignClass design = new DesignClass(Progress, DesignGridView);
                DataClass data = new DataClass(Progress, DataGridView);
                ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                AddressesClass address = new AddressesClass(Progress, AddressesGridView);

                design.LoadFromFile(Settings.Default.AutoLoadFile);
                data.LoadFromFile(Settings.Default.AutoLoadFile);
                objects.LoadFromFile(Settings.Default.AutoLoadFile);
                modules.LoadFromFile(Settings.Default.AutoLoadFile);
                address.LoadFromFile(Settings.Default.AutoLoadFile);
                address.GetDataFromGrid(true);
                address.CheckOverlapAll();

                debug.ToFile(Resources.FileAutoLoad + " " + Resources.Finished, DebugLevels.Minimum, DebugMessageType.Info);
            }

            UpdateUIElement();
        }

        /// <summary>
        /// update UI element text
        /// </summary>
        private void UpdateUIElement()
        {
            // **********************
            File_DropDown.Text = ResourcesUI.File;
            File_Help.Text = ResourcesUI.Help;
            File_About.Text = ResourcesUI.About;
            File_Exit.Text = ResourcesUI.Exit;
            File_Language.Text = Resources.Language;
            File_Language_EN.Text = ResourcesUI.EN;
            File_Language_LT.Text = ResourcesUI.LT;
            File_Save.Text = ResourcesUI.Save;
            File_SaveAll.Text = ResourcesUI.SaveAll;
            File_Load.Text = ResourcesUI.Load;
            File_LoadAll.Text = ResourcesUI.LoadAll;

            // **********************
            Project_DropDown.Text = ResourcesUI.Project;
            Project_GetDataFromDesign.Text = ResourcesUI.GetDataFromDesign;
            Project_CompareDesign.Text = ResourcesUI.CompareWithNewDesign;

            Project_CPU.Text = Resources.CPU;
            Project_CPU_Add.Text = Resources.Add;
            Project_SCADA.Text = ResourcesUI.SCADA;
            Project_SCADA_Add.Text = Resources.Add;
            Project_Language.Text = ResourcesUI.IO + " " + Resources.Language;
            Project_Language_Add.Text = Resources.Add;

            // **********************
            Data_DropDown.Text = ResourcesUI.Data;
            Data_GetDataFromProject.Text = ResourcesUI.GetDataFromProject;
            DataFindFunctionMenuItem.Text = ResourcesUI.FindFunction;
            Data_KKSCombine.Text = ResourcesUI.KKSCombine;

            // **********************
            Objects_DropDown.Text = ResourcesUI.Objects;
            Objects_Find.Text = ResourcesUI.ObjectsFindUnique;
            ObjectsFindTypeMenuItem.Text = ResourcesUI.ObjectFindType;
            Object_TransferToData.Text = ResourcesUI.ObjectsTransferData;

            Objects_DeclareGenerate.Text = ResourcesUI.ObjectDeclareGenerate;
            Objects_InstancesGenerate.Text = ResourcesUI.ObjectInstanceGenerate;
            Objects_ClearAddresses.Text = ResourcesUI.ClearAddresses;

            Object_EditTypes.Text = ResourcesUI.ObjectsEdit;
            Objects_DeclareEdit.Text = ResourcesUI.ObjectDeclareDBEdit;
            Objects_InstancesEdit.Text = ResourcesUI.ObjectInstanceEditDB;

            // **********************
            IO_DropDown.Text = ResourcesUI.IO;
            IO_FindModules.Text = ResourcesUI.FindUniqueModules;
            IO_Generate.Text = ResourcesUI.IOGenerate;
            IO_ClearAddresses.Text = ResourcesUI.ClearAddresses;
            IO_Edit.Text = ResourcesUI.IODBEdit;

            // **********************
            SCADA_DropDown.Text = ResourcesUI.SCADA;
            SCADA_GenerateObjects.Text = ResourcesUI.SCADAObjectGenerate;
            SCADA_GenerateModules.Text = ResourcesUI.SCADAModuleGenerate;
            SCADA_EditObjects.Text = ResourcesUI.SCADAObjectEdit;
            SCADA_EditModules.Text = ResourcesUI.SCADAModuleEdit;

            // **********************
            DataTab.Text = ResourcesUI.Data;
            DesignTab.Text = ResourcesUI.Project;
            ModuleTab.Text = ResourcesUI.Modules;
            ObjectTab.Text = ResourcesUI.Objects;

            // **********************
            AddressTab.Text = ResourcesUI.Address;

            // **********************
            FindButton.Text = ResourcesUI.Find;
        }

        /// <summary>
        /// Informs what button was pressed
        /// </summary>
        /// <param name="button">button pressed</param>
        private void ButtonPressed(object button)
        {
            Debug _debug = new Debug();
            ToolStripMenuItem _button = (ToolStripMenuItem)button;
            string _buttonName = _button.Text;
            _debug.ToFile($"{Resources.ButtonPressed}: {_buttonName}", DebugLevels.Minimum, DebugMessageType.Info);
        }

        /// <summary>
        /// Informs what button was pressed and function is finished
        /// </summary>
        /// <param name="button">button pressed</param>
        private void ButtonFunctionFinished(object button)
        {
            Debug _debug = new Debug();
            ToolStripMenuItem _button = (ToolStripMenuItem)button;
            string _buttonName = _button.Text;
            _debug.ToFile($"{Resources.ButtonPressed}: {_buttonName} - {Resources.FunctionFinished}", DebugLevels.Minimum, DebugMessageType.Info);
        }

        /// <summary>
        /// Informs that function for this button does not exists
        /// </summary>
        /// <param name="button">button pressed</param>
        private void DisplayNoFunction(object button)
        {
            Debug _debug = new Debug();
            ToolStripMenuItem _button = (ToolStripMenuItem)button;
            string _buttonName = _button.Text;
            _debug.ToPopUp($"{Resources.NoFunction}: {_buttonName}", DebugLevels.None, DebugMessageType.Critical);
        }

        /// <summary>
        /// Ask before terminating program
        /// Yes - save and exit
        /// No - do not save and exit
        /// Cancel -  abort exit
        /// </summary>
        /// <returns>abort exit</returns>
        private bool AskBeforeExit()
        {
            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                return false;

            DialogResult _result = MessageBox.Show(Resources.ConfirmExit, "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (_result == DialogResult.Yes)
            {
                SaveAll();
                return false;
            }
            else if (_result == DialogResult.No)
            {
                Application.Exit();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
                e.Cancel = AskBeforeExit();
        }

        private void DeleteColumnComboBox()
        {
            foreach (var _item in this.Controls)
            {
                if (!_item.GetType().Name.Contains("ComboBox"))
                    continue;

                if (((System.Windows.Forms.ComboBox)_item).Name != "ColumnSelectComboBox")
                    continue;

                DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)_item)
                {
                    IndexChangedEventRemove = ColumnSelectComboBox_SelectedValueChanged,
                };
                this.Controls.Remove((System.Windows.Forms.Control)_item);
                break;
            }
        }

        private void ColumnSelectComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            if (!comboBox.ValidCheck())
                return;

            string _keyword = comboBox.SelectedKeyword();
            DataGridView _grid = (DataGridView)MainTabControl.SelectedTab.Controls[0];

            //if add column
            if (comboBox.SelectedMod().Contains(Resources.Add))
            {
                DataGridViewColumn _columnGridView = new DataGridViewColumn()
                {
                    CellTemplate = new DataGridViewTextBoxCell(),
                    Name = _keyword,
                    HeaderText = comboBox.SelectedName(),
                    SortMode = DataGridViewColumnSortMode.Automatic,
                };

                _grid.Columns.Insert(_grid.ColumnCount, _columnGridView);
            }
            //remove column
            else
            {
                //get column name and number of column to remove
                for (int _columnNumber = 0; _columnNumber < _grid.ColumnCount; _columnNumber++)
                {
                    if (_grid.Columns[_columnNumber].Name != _keyword)
                        continue;

                    _grid.Columns.Remove(_grid.Columns[_columnNumber]);
                    break;
                }
            }
            DeleteColumnComboBox();
        }

        /// <summary>
        /// Function to add column list to ComboBox which can be added or removed from grid
        /// </summary>
        /// <param name="e">event arguments</param>
        /// <param name="columnList">column list visible</param>
        /// <param name="baseColumnList">base column list</param>
        private void GridViewColumn_Click(EventArgs e, ColumnList columnList, ColumnList baseColumnList)
        {
            MouseEventArgs _mouse = (MouseEventArgs)e;

            //check and if exists delete columnComboBox just to be save to not create infinite amount of it
            DeleteColumnComboBox();
            if (_mouse.Button != MouseButtons.Right)
                return;

            Debug _debug = new Debug();
            _debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

            DropDownClass comboBox = new DropDownClass("ColumnSelectComboBox");
            comboBox.Editable(false);
            comboBox.Location = PointToClient(Cursor.Position);

            //current columns that are shown can be removed
            foreach (GeneralColumn _column in columnList)
            {
                if (!_column.CanHide)
                    continue;

                comboBox.AddItemColumn(Resources.Remove, _column.Keyword);
            }

            string _keyword;
            bool _found;
            //current columns that are not visible can be added
            foreach (GeneralColumn baseColumn in baseColumnList)
            {
                _found = false;
                _keyword = baseColumn.Keyword;
                foreach (GeneralColumn _column in columnList)
                {
                    if (_keyword == _column.Keyword)
                    {
                        _found = true;
                        break;
                    }
                }
                // when column list does not have keyword that is in base than column can be added
                if (!_found)
                    comboBox.AddItemColumn(Resources.Add, _keyword);
            }
            comboBox.IndexChangedEvent = ColumnSelectComboBox_SelectedValueChanged;

            this.Controls.Add(comboBox.Element);
            comboBox.BringToFront();
        }

        private void DesignGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DesignClass design = new DesignClass(Progress, DesignGridView);
            GridViewColumn_Click(e, design.Columns, design.BaseColumns);
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataClass data = new DataClass(Progress, DataGridView);
            GridViewColumn_Click(e, data.Columns, data.BaseColumns);
        }

        private void ObjectsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            GridViewColumn_Click(e, objects.Columns, objects.BaseColumns);
        }

        private void ModulesGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
            GridViewColumn_Click(e, modules.Columns, modules.BaseColumns);
        }

        /// <summary>
        /// Key Down event for all forms and application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown_Event(object sender, KeyEventArgs e)
        {
            DeleteColumnComboBox();
            // Delete function
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                this.SuspendLayout();
                DataGridView _grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                for (int i = 0; i < (_grid.SelectedCells.Count); i++)
                    _grid.SelectedCells[i].Value = "";

                this.ResumeLayout();
                this.Update();
            }
            // Paste function
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.V))
            {
                this.SuspendLayout();
                DataGridView _grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                if (_grid.SelectedCells.Count == 0)
                    return;

                GeneralFunctions.Paste(_grid);

                this.ResumeLayout();
                this.Update();
            }
        }

        private void SelectTab(TabIndex tabIndex)
        {
            MainTabControl.SelectedIndex = (int)tabIndex;
            ((DataGridView)MainTabControl.SelectedTab.Controls[0]).AutoResizeColumns();
        }

        //-------------------------File drop down------------------------------------------
        private void File_Save_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            switch ((TabIndex)MainTabControl.SelectedIndex)
            {
                case IO_list_automation_new.TabIndex.Design:
                    DesignClass design = new DesignClass(Progress, DesignGridView);
                    design.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Modules:
                    ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                    modules.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Address:
                    AddressesClass address = new AddressesClass(Progress, AddressesGridView);
                    address.SaveSelect();
                    break;

                default:
                    DisplayNoFunction(sender);
                    break;
            }

            ButtonFunctionFinished(sender);
        }

        private void SaveAll()
        {
            Debug debug = new Debug();

            SaveFileDialog _saveFile = new SaveFileDialog();

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
            AddressesClass address = new AddressesClass(Progress, AddressesGridView);

            _saveFile.Filter = "All save files|*" + design.Grid.FileExtension + ";*" + data.Grid.FileExtension
                                                    + ";*" + objects.Grid.FileExtension + ";*" + modules.Grid.FileExtension
                                                    + ";*" + address.Grid.FileExtension;

            if (_saveFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return;
            }
            design.SaveToFile(_saveFile.FileName);
            data.SaveToFile(_saveFile.FileName);
            objects.SaveToFile(_saveFile.FileName);
            modules.SaveToFile(_saveFile.FileName);
            address.SaveToFile(_saveFile.FileName);

            Settings.Default.AutoLoadFile = _saveFile.FileName;
            Settings.Default.Save();
        }

        private void File_SaveAll_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);
            SaveAll();
            ButtonFunctionFinished(sender);
        }

        private void File_Load_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            switch ((TabIndex)MainTabControl.SelectedIndex)
            {
                case IO_list_automation_new.TabIndex.Design:
                    DesignClass design = new DesignClass(Progress, DesignGridView);
                    design.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Modules:
                    ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                    modules.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Address:
                    AddressesClass address = new AddressesClass(Progress, AddressesGridView);
                    address.LoadSelect();
                    address.GetDataFromGrid(true);
                    address.CheckOverlapAll();
                    break;

                default:
                    DisplayNoFunction(sender);
                    break;
            }
            ButtonFunctionFinished(sender);
        }

        private void File_LoadAll_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            Debug debug = new Debug();

            OpenFileDialog _loadFile = new OpenFileDialog();

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
            AddressesClass address = new AddressesClass(Progress, AddressesGridView);

            _loadFile.Filter = "All save files|*" + design.Grid.FileExtension + ";*" + data.Grid.FileExtension
                                        + ";*" + objects.Grid.FileExtension + ";*" + modules.Grid.FileExtension
                                        + ";*" + address.Grid.FileExtension;

            if (_loadFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return;
            }
            design.LoadFromFile(_loadFile.FileName);
            data.LoadFromFile(_loadFile.FileName);
            objects.LoadFromFile(_loadFile.FileName);
            modules.LoadFromFile(_loadFile.FileName);
            address.LoadFromFile(_loadFile.FileName);
            address.GetDataFromGrid(true);
            address.CheckOverlapAll();

            Settings.Default.AutoLoadFile = _loadFile.FileName;
            Settings.Default.Save();

            AutoSizeAllTabs();

            ButtonFunctionFinished(sender);
        }

        private void FileLanguage_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _item = (ToolStripMenuItem)sender;

            if (Settings.Default.ApplicationLanguage == _item.Text.ToLower())
                return;

            Settings.Default.ApplicationLanguage = _item.Text.ToLower();
            Settings.Default.Save();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Default.ApplicationLanguage);

            UpdateUIElement();
            this.Update();
        }

        private void File_DebugLevel_None_Click(object sender, EventArgs e)
        {
            Debug debug = new Debug();
            debug.SetDebugLevel(DebugLevels.None);
        }

        private void File_DebugLevel_Minimum_Click(object sender, EventArgs e)
        {
            Debug debug = new Debug();
            debug.SetDebugLevel(DebugLevels.Minimum);
        }

        private void File_DebugLevel_High_Click(object sender, EventArgs e)
        {
            Debug debug = new Debug();
            debug.SetDebugLevel(DebugLevels.High);
        }

        private void File_DebugLevel_Development_Click(object sender, EventArgs e)
        {
            Debug debug = new Debug();
            debug.SetDebugLevel(DebugLevels.Development);
        }

        private void File_Help_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void File_About_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void File_Exit_Click(object sender, EventArgs e)
        {
            if (!AskBeforeExit())
                System.Windows.Forms.Application.Exit();
        }

        //-------------------------Project dropdown------------------------------------------
        private void Project_GetDataFromDesign_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            if (design.GetDataFromImportFile())
                design.PutDataToGrid(false);

            SelectTab(IO_list_automation_new.TabIndex.Design);
            ButtonFunctionFinished(sender);
        }

        private void Project_CompareDesign_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        /// <summary>
        /// Add elements to drop downs from DB
        /// </summary>
        /// <param name="menuItem">menu item to add dropdown</param>
        /// <param name="list">list to add to dropdown</param>
        private void AddMenuItemDropDown(ToolStripMenuItem menuItem, List<string> list)
        {
            //clear all items, but leave first one
            for (int i = menuItem.DropDownItems.Count - 1; i > 0; i--)
                menuItem.DropDownItems.Remove(menuItem.DropDownItems[i]);

            //add items
            for (int i = 0; i < list.Count; i++)
                menuItem.DropDownItems.Add(list[i]);
        }

        private void Project_CPU_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral _DB = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB.ToString), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                NewName _newName = new NewName(Resources.CreateNew + ": " + Resources.CPU, string.Empty, false);

                _newName.ShowDialog();
                string _folderName = _newName.Output;
                if (string.IsNullOrEmpty(_folderName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (_DB.DBFolderExists(_folderName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _DB.DBFolderCreate(_newName.Output);
            }
            else
            {
                Settings.Default.SelectedCPU = e.ClickedItem.Text;
                DBGeneral _DBScada = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
                List<string> _list = _DBScada.GetDBFolderList();

                //set SCADA for first element
                if (_list.Count > 0)
                    Settings.Default.SelectedSCADA = _list[0];
                else
                    Settings.Default.SelectedSCADA = "-";
                Settings.Default.Save();
            }
        }

        private void Project_SCADA_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.SCADA, string.Empty, false);

                _newName.ShowDialog();
                string _folderName = _newName.Output;
                if (string.IsNullOrEmpty(_folderName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (_DB.DBFolderExists(_folderName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _DB.DBFolderCreate(_newName.Output);
            }
            else
            {
                Settings.Default.SelectedSCADA = e.ClickedItem.Text;
                Settings.Default.Save();
            }
        }

        private void Project_Language_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DataGridView _grid = new DataGridView();
                DBLanguage _DBLanguage = new DBLanguage(Progress,_grid);

                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language, string.Empty, false);

                _newName.ShowDialog();
                string _fileName = _newName.Output;
                if (string.IsNullOrEmpty(_fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (_DBLanguage.FunctionType.Grid.FileExistsInDB(_fileName, null))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List <List<string>> _data= new List<List<string>>()
                {
                    _DBLanguage.FunctionType.Columns.GetColumnsKeyword(),
                    new List<string>(){"Text to replace", "OPD"},
                };
                _DBLanguage.FunctionType.Grid.CreateFileInDB(_fileName, null, _data);

                _data = new List<List<string>>()
                {
                    _DBLanguage.Type.Columns.GetColumnsKeyword(),
                    new List<string>(){"Text to replace", "VLV"},
                };
                _DBLanguage.Type.Grid.CreateFileInDB(_fileName, null, _data);
            }
            else
            {
                Settings.Default.IOLanguage = e.ClickedItem.Text;
                Settings.Default.Save();
            }
        }

        private void Project_DropDown_DropDownOpened(object sender, EventArgs e)
        {
            Project_CPU.Text = Resources.CPU + ": " + Settings.Default.SelectedCPU;
            Project_SCADA.Text = ResourcesUI.SCADA + ": " + Settings.Default.SelectedSCADA;
            Project_Language.Text = ResourcesUI.IO + " " + Resources.Language + ": " + Settings.Default.IOLanguage;

            DBGeneral _DBCPU = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            List<string> _listCPU = _DBCPU.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown(Project_CPU, _listCPU);

            DBGeneral _DBSCADA = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
            List<string> _listSCADA = _DBSCADA.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown(Project_SCADA, _listSCADA);

            DBGeneral _DBIO = new DBGeneral(Progress, ResourcesUI.IO + " " + Resources.Language, nameof(FileExtensions.langTypeDB), DBTypeLevel.Base, BaseTypes.ObjectsCPU);
            List<string> _listIO = _DBIO.GetDBFileList();
            //add items to dropdown
            AddMenuItemDropDown(Project_Language, _listIO);
        }

        //-------------------------Data dropdown------------------------------------------

        private void Data_GetDataFromProject_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            if (design.GetDataFromGrid(false))
            {
                DataClass data = new DataClass(Progress, DataGridView);
                data.ExtractFromDesign(design);
                data.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Data);
            ButtonFunctionFinished(sender);
        }

        private void Data_KKSCombine_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            if (data.GetDataFromGrid(false))
            {
                data.MakeKKS();
                data.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Data);
            ButtonFunctionFinished(sender);
        }

        private void DataFindFunctionMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);

            if (data.GetDataFromGrid(false))
            {
                DataGridView _grid = new DataGridView();
                DBLanguage _DBLanguage = new DBLanguage(Progress, _grid);
                _DBLanguage.FunctionType.FindAllFunctionType(data);
            }

            SelectTab(IO_list_automation_new.TabIndex.Data);
            ButtonFunctionFinished(sender);
        }

        private void Data_EditFunctions_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBForceEditForm dBForceEditForm = new DBForceEditForm();
            DBLanguage _DBLanguage = new DBLanguage(Progress, dBForceEditForm.DataGrid);

            _DBLanguage.FunctionType.LoadEdit();
            dBForceEditForm.ShowDialog();
            _DBLanguage.FunctionType.SaveEdit();

            ButtonFunctionFinished(sender);
        }

        //-------------------------Objects dropdown------------------------------------------
        private void Objects_Find_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (data.GetDataFromGrid(false))
            {
                objects.ExtractFromData(data);
                objects.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Object);
            ButtonFunctionFinished(sender);
        }

        private void ObjectsFindTypeMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (objects.GetDataFromGrid(false))
            {
                DataGridView _grid = new DataGridView();
                DBLanguage _DBLanguage = new DBLanguage(Progress, _grid);
                _DBLanguage.Type.FindAllType(objects);
            }

            SelectTab(IO_list_automation_new.TabIndex.Object);
            ButtonFunctionFinished(sender);
        }

        private void Object_TransferToData_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                objects.SendToData(data);
                data.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Object);
            ButtonFunctionFinished(sender);
        }

        private void Objects_DeclareGenerate_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.GetDataFromGrid(true);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral _DB = new DBGeneral(Progress, Resources.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                _DB.DecodeAll(data, objects, null, addresses);

                addresses.CheckOverlapAll();
            }

            SelectTab(IO_list_automation_new.TabIndex.Address);
            ButtonFunctionFinished(sender);
        }

        private void Objects_InstancesGenerate_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.GetDataFromGrid(true);

            //clear all used columns
            for (int _objectIndex = 0; _objectIndex < objects.Signals.Count; _objectIndex++)
                objects.Signals[_objectIndex].SetValueFromString(string.Empty, KeywordColumn.Used);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral _DB = new DBGeneral(Progress, Resources.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                _DB.DecodeAll(data, objects, null, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void ClearAddresses_Click(object sender, EventArgs e)
        {
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.Grid.GridClear();
        }

        private void Object_EditTypes_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBForceEditForm dBForceEditForm = new DBForceEditForm();
            DBLanguage _DBLanguage = new DBLanguage(Progress, dBForceEditForm.DataGrid);

            _DBLanguage.Type.LoadEdit();
            dBForceEditForm.ShowDialog();
            _DBLanguage.Type.SaveEdit();

            ButtonFunctionFinished(sender);
        }

        private void Objects_DeclareEdit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, Resources.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        private void Objects_InstancesEdit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, Resources.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------IO dropdown------------------------------------------
        private void IO_FindModules_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);

            if (data.GetDataFromGrid(false))
            {
                ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                modules.ExtractFromData(data);
                modules.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Modules);
            ButtonFunctionFinished(sender);
        }

        private void IO_Generate_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.GetDataFromGrid(true);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false) && modules.GetDataFromGrid(false))
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Modules, nameof(FileExtensions.modDB), DBTypeLevel.CPU, BaseTypes.ModuleCPU);
                _DB.DecodeAll(data, objects, modules, addresses);

                addresses.CheckOverlapAll();
            }

            SelectTab(IO_list_automation_new.TabIndex.Address);
            ButtonFunctionFinished(sender);
        }

        private void IO_Edit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Modules, nameof(FileExtensions.modDB), DBTypeLevel.CPU, BaseTypes.ModuleCPU);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------SCADA dropdown------------------------------------------

        private void SCADA_GenerateObjects_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.GetDataFromGrid(true);

            if (modules.GetDataFromGrid(false) && data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectSCADA);
                _DB.DecodeAll(data, objects, modules, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void SCADA_GenerateModules_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.GetDataFromGrid(true);

            if (modules.GetDataFromGrid(false) && data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.modScadaDB), DBTypeLevel.SCADA, BaseTypes.ModuleSCADA);
                _DB.DecodeAll(data, objects, modules, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void SCADA_EditObjects_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectSCADA);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        private void SCADA_EditModules_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.modScadaDB), DBTypeLevel.SCADA, BaseTypes.ModuleSCADA);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------------------------------------------------
        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void MainToolStrip_Click(object sender, EventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void MainTabControl_Click(object sender, EventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void DesignGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void ObjectsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void ModulesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void AddressesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void AutoSizeAllTabs()
        {
            foreach (TabPage _tab in MainTabControl.TabPages)
            {
                MainTabControl.SelectedTab = _tab;
                ((DataGridView)_tab.Controls[0]).AutoResizeColumns();
            }
            MainTabControl.SelectedIndex = (int)IO_list_automation_new.TabIndex.Data;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            AutoSizeAllTabs();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            int _startRow = 0;
            int _startColumn = 0;

            DataGridView  _grid = (DataGridView)MainTabControl.SelectedTab.Controls[0];

            if (_grid.SelectedCells.Count >= 1)
            {
                _startRow = _grid.SelectedCells[0].RowIndex;
                _startColumn = _grid.SelectedCells[0].ColumnIndex + 1;
            }

            if (_grid.RowCount < 1)
                return;

            string cell_text;
            int _row;

            for (int _rowIndex = 0; _rowIndex < _grid.RowCount - 1; _rowIndex++)
            {
                // to be able to go back from beginning
                _row = _rowIndex + _startRow;
                if (_row >= _grid.RowCount - 1)
                    _row -= _grid.RowCount - 1;

                // fill all cells with data
                for (int _column = _startColumn; _column <= _grid.ColumnCount - 1; _column++)
                {
                    cell_text = _grid.Rows[_row].Cells[_column].Value.ToString();

                    if (!cell_text.Contains(FindTextBox.Text))
                        continue;

                    _grid.ClearSelection();

                    //go to that cell and select it
                    _grid.CurrentCell = _grid.Rows[_row].Cells[_column];
                    _grid.Rows[_row].Cells[_column].Selected = true;

                    //breaking first loop
                    _rowIndex = _grid.RowCount;
                    break;
                }
                _startColumn = 0;
            }
        }
    }
}