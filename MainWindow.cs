using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

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

                DesignClass design = new DesignClass(Progress, Design_GridView);
                DataClass data = new DataClass(Progress, Data_GridView);
                ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
                ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
                AddressesClass address = new AddressesClass(Progress, Addresses_GridView);

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
            Project_CPUAdd.Text = Resources.Add;
            Project_SCADA.Text = ResourcesUI.SCADA;
            Project_SCADAAdd.Text = Resources.Add;
            Project_Language.Text = ResourcesUI.IO + " " + Resources.Language;
            Project_LanguageAdd.Text = Resources.Add;

            // **********************
            Data_DropDown.Text = ResourcesUI.Data;
            Data_GetDataFromProject.Text = ResourcesUI.GetDataFromProject;
            Data_FindFunctionMenuItem.Text = ResourcesUI.FindFunction;
            Data_KKSCombine.Text = ResourcesUI.KKSCombine;

            // **********************
            Objects_DropDown.Text = ResourcesUI.Objects;
            Objects_Find.Text = ResourcesUI.ObjectsFindUnique;
            Objects_FindTypeMenuItem.Text = ResourcesUI.ObjectFindType;
            Objects_TransferToData.Text = ResourcesUI.ObjectsTransferData;

            Objects_DeclareGenerate.Text = ResourcesUI.ObjectDeclareGenerate;
            Objects_InstancesGenerate.Text = ResourcesUI.ObjectInstanceGenerate;
            Objects_ClearAddresses.Text = ResourcesUI.ClearAddresses;

            Objects_EditTypes.Text = ResourcesUI.ObjectsEdit;
            Objects_DeclareEdit.Text = ResourcesUI.ObjectDeclareDBEdit;
            Objects_InstancesEdit.Text = ResourcesUI.ObjectInstanceEditDB;

            // **********************
            IO_dropDown.Text = ResourcesUI.IO;
            IO_FndModules.Text = ResourcesUI.FindUniqueModules;
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
            Data_Tab.Text = ResourcesUI.Data;
            Design_Tab.Text = ResourcesUI.Project;
            Module_Tab.Text = ResourcesUI.Modules;
            Object_Tab.Text = ResourcesUI.Objects;

            // **********************
            Address_Tab.Text = ResourcesUI.Address;

            // **********************
            FindButton.Text = ResourcesUI.Find;
        }

        /// <summary>
        /// Informs what button was pressed
        /// </summary>
        /// <param name="button">button pressed</param>
        private void ButtonPressed(object button)
        {
            Debug debug = new Debug();
            ToolStripMenuItem item = (ToolStripMenuItem)button;
            string buttonName = item.Text;
            debug.ToFile($"{Resources.ButtonPressed}: {buttonName}", DebugLevels.Minimum, DebugMessageType.Info);
        }

        /// <summary>
        /// Informs what button was pressed and function is finished
        /// </summary>
        /// <param name="button">button pressed</param>
        private void ButtonFunctionFinished(object button)
        {
            Debug debug = new Debug();
            ToolStripMenuItem item = (ToolStripMenuItem)button;
            string buttonName = item.Text;
            debug.ToFile($"{Resources.ButtonPressed}: {buttonName} - {Resources.FunctionFinished}", DebugLevels.Minimum, DebugMessageType.Info);
        }

        /// <summary>
        /// Informs that function for this button does not exists
        /// </summary>
        /// <param name="button">button pressed</param>
        private void DisplayNoFunction(object button)
        {
            Debug debug = new Debug();
            ToolStripMenuItem item = (ToolStripMenuItem)button;
            string buttonName = item.Text;
            debug.ToPopUp($"{Resources.NoFunction}: {buttonName}", DebugLevels.None, DebugMessageType.Critical);
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

            DialogResult result = MessageBox.Show(Resources.ConfirmExit, "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveAll();
                return false;
            }
            else if (result == DialogResult.No)
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
            foreach (var item in this.Controls)
            {
                if (!item.GetType().Name.Contains("ComboBox"))
                    continue;

                if (((System.Windows.Forms.ComboBox)item).Name != "ColumnSelectComboBox")
                    continue;

                DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)item)
                {
                    IndexChangedEventRemove = ColumnSelectComboBoxSelectedValueChanged,
                };
                this.Controls.Remove((System.Windows.Forms.Control)item);
                break;
            }
        }

        private void ColumnSelectComboBoxSelectedValueChanged(object sender, EventArgs e)
        {
            DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            if (!comboBox.ValidCheck())
                return;

            ColumnList columnList;
            switch ((TabIndex)MainTabControl.SelectedIndex)
            {
                case IO_list_automation_new.TabIndex.Design:
                    DesignClass design = new DesignClass(Progress, Design_GridView);
                    columnList = design.Columns;
                    break;

                case IO_list_automation_new.TabIndex.Data:
                    DataClass data = new DataClass(Progress, Data_GridView);
                    columnList = data.Columns;
                    break;

                case IO_list_automation_new.TabIndex.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
                    columnList = objects.Columns;
                    break;

                case IO_list_automation_new.TabIndex.Modules:
                    ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
                    columnList = modules.Columns;
                    break;

                case IO_list_automation_new.TabIndex.Address:
                    AddressesClass address = new AddressesClass(Progress, Addresses_GridView);
                    columnList = address.Columns;
                    break;

                default:
                    throw new NotImplementedException();
            }

            string keyword = comboBox.SelectedKeyword();
            DataGridView grid = (DataGridView)MainTabControl.SelectedTab.Controls[0];

            columnList.Columns.TryGetValue(keyword, out ColumnParameters column);
            //if add column
            if (comboBox.SelectedMod().Contains(Resources.Add))
            {
                DataGridViewColumn columnGridView = new DataGridViewColumn()
                {
                    CellTemplate = new DataGridViewTextBoxCell(),
                    Name = keyword,
                    HeaderText = comboBox.SelectedName(),
                    SortMode = DataGridViewColumnSortMode.Automatic,
                };
                column.Hidden = false;
                column.NR = grid.ColumnCount;

                grid.Columns.Insert(grid.ColumnCount, columnGridView);
            }
            //remove column
            else
            {
                //get column name and number of column to remove
                foreach (DataGridViewColumn gridColumn in grid.Columns)
                {
                    if (gridColumn.Name != keyword)
                        continue;

                    column.Hidden = true;

                    grid.Columns.Remove(gridColumn);
                    break;
                }
            }
            columnList.SaveColumnsParameters();
            DeleteColumnComboBox();
        }

        /// <summary>
        /// Function to add column list to ComboBox which can be added or removed from grid
        /// </summary>
        /// <param name="e">event arguments</param>
        /// <param name="columnList">column list visible</param>
        private void GridViewColumn_Click(EventArgs e, ColumnList columnList)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;

            //check and if exists delete columnComboBox just to be save to not create infinite amount of it
            DeleteColumnComboBox();
            if (mouse.Button != MouseButtons.Right)
                return;

            Debug debug = new Debug();
            debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

            DropDownClass comboBox = new DropDownClass("ColumnSelectComboBox");
            comboBox.Editable(false);
            comboBox.Location = PointToClient(Cursor.Position);


            foreach (var column in columnList.Columns)
            {
                //columns that are shown can be removed
                if (column.Value.CanHide && !column.Value.Hidden)
                    comboBox.AddItemColumn(Resources.Remove, column.Key);
                //columns that are hidden can be added
                else if (column.Value.Hidden)
                    comboBox.AddItemColumn(Resources.Add, column.Key);
            }

            comboBox.IndexChangedEvent = ColumnSelectComboBoxSelectedValueChanged;

            this.Controls.Add(comboBox.Element);
            comboBox.BringToFront();
        }

        private void Design_GridViewColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DesignClass design = new DesignClass(Progress, Design_GridView);
            GridViewColumn_Click(e, design.Columns);
        }

        private void DataGridViewColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataClass data = new DataClass(Progress, Data_GridView);
            GridViewColumn_Click(e, data.Columns);
        }

        private void ObjectsGridViewColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            GridViewColumn_Click(e, objects.Columns);
        }

        private void Modules_GridViewColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
            GridViewColumn_Click(e, modules.Columns);
        }

        /// <summary>
        /// Key Down event for all forms and application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown_Event(object sender, KeyEventArgs e)
        {
            DeleteColumnComboBox();

            if ((TabIndex)MainTabControl.SelectedIndex == IO_list_automation_new.TabIndex.Address)
                return;

            // Delete function
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                this.SuspendLayout();
                DataGridView grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                foreach (DataGridViewCell cell in grid.SelectedCells)
                    cell.Value = "";

                this.ResumeLayout();
                this.Update();
            }
            // Paste function
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.V))
            {
                this.SuspendLayout();
                DataGridView grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                if (grid.SelectedCells.Count == 0)
                    return;

                GeneralFunctions.Paste(grid);

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
                    DesignClass design = new DesignClass(Progress, Design_GridView);
                    design.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Data:
                    DataClass data = new DataClass(Progress, Data_GridView);
                    data.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
                    objects.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Modules:
                    ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
                    modules.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Address:
                    AddressesClass address = new AddressesClass(Progress, Addresses_GridView);
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

            SaveFileDialog saveFile = new SaveFileDialog();

            DesignClass design = new DesignClass(Progress, Design_GridView);
            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
            AddressesClass address = new AddressesClass(Progress, Addresses_GridView);

            saveFile.Filter = "All save files|*" + design.File.FileExtension + ";*" + data.File.FileExtension
                                                    + ";*" + objects.File.FileExtension + ";*" + modules.File.FileExtension
                                                    + ";*" + address.File.FileExtension;

            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return;
            }
            design.SaveToFile(saveFile.FileName);
            data.SaveToFile(saveFile.FileName);
            objects.SaveToFile(saveFile.FileName);
            modules.SaveToFile(saveFile.FileName);
            address.SaveToFile(saveFile.FileName);

            Settings.Default.AutoLoadFile = saveFile.FileName;
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
                    DesignClass design = new DesignClass(Progress, Design_GridView);
                    design.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Data:
                    DataClass data = new DataClass(Progress, Data_GridView);
                    data.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
                    objects.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Modules:
                    ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
                    modules.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Address:
                    AddressesClass address = new AddressesClass(Progress, Addresses_GridView);
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

            OpenFileDialog loadFile = new OpenFileDialog();

            DesignClass design = new DesignClass(Progress, Design_GridView);
            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
            AddressesClass address = new AddressesClass(Progress, Addresses_GridView);

            loadFile.Filter = "All save files|*" + design.File.FileExtension + ";*" + data.File.FileExtension
                                        + ";*" + objects.File.FileExtension + ";*" + modules.File.FileExtension
                                        + ";*" + address.File.FileExtension;

            if (loadFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return;
            }
            design.LoadFromFile(loadFile.FileName);
            data.LoadFromFile(loadFile.FileName);
            objects.LoadFromFile(loadFile.FileName);
            modules.LoadFromFile(loadFile.FileName);
            address.LoadFromFile(loadFile.FileName);
            address.GetDataFromGrid(true);
            address.CheckOverlapAll();

            Settings.Default.AutoLoadFile = loadFile.FileName;
            Settings.Default.Save();

            AutoSizeAllTabs();

            ButtonFunctionFinished(sender);
        }

        private void File_Language_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (Settings.Default.ApplicationLanguage == item.Text.ToLower())
                return;

            Settings.Default.ApplicationLanguage = item.Text.ToLower();
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

        private void File_DebugLevelMinimum_Click(object sender, EventArgs e)
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

        //-------------------------Project_ dropdown------------------------------------------
        private void Project_GetDataFromDesign_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, Design_GridView);
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
            foreach (string item in list)
                menuItem.DropDownItems.Add(item);
        }

        private void Project_CPUDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral db = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB.ToString), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                NewName newName = new NewName(Resources.CreateNew + ": " + Resources.CPU, string.Empty, false);

                newName.ShowDialog();
                string folderName = newName.Output;
                if (string.IsNullOrEmpty(folderName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (db.DBFolderExists(folderName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                db.DBFolderCreate(newName.Output);
            }
            else
            {
                Settings.Default.SelectedCPU = e.ClickedItem.Text;
                DBGeneral dbScada = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
                List<string> list = dbScada.GetDBFolderList();

                //set SCADA for first element
                if (list.Count > 0)
                    Settings.Default.SelectedSCADA = list[0];
                else
                    Settings.Default.SelectedSCADA = "-";
                Settings.Default.Save();
            }
        }

        private void Project_SCADADropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral db = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
                NewName newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.SCADA, string.Empty, false);

                newName.ShowDialog();
                string folderName = newName.Output;
                if (string.IsNullOrEmpty(folderName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (db.DBFolderExists(folderName))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                db.DBFolderCreate(newName.Output);
            }
            else
            {
                Settings.Default.SelectedSCADA = e.ClickedItem.Text;
                Settings.Default.Save();
            }
        }

        private void Project_LanguageDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DataGridView grid = new DataGridView();
                DBLanguage dbLanguage = new DBLanguage(Progress, grid);

                NewName newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language, string.Empty, false);

                newName.ShowDialog();
                string fileName = newName.Output;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (dbLanguage.FunctionType.File.FileExistsInDB(fileName, null))
                {
                    MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable data = new DataTable();
                DataRow row = data.NewRow();
                data.Columns.Add("0");
                data.Columns.Add("");
                row[0] = "Text to replace";
                row[1] = "OPD";
                data.Rows.Add(row);
                dbLanguage.FunctionType.File.CreateFileInDB(fileName, null, data);

                data.Rows[0][1] = "VLV";
                dbLanguage.Type.File.CreateFileInDB(fileName, null, data);
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

            DBGeneral dbCPU = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            List<string> listCPU = dbCPU.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown(Project_CPU, listCPU);

            DBGeneral dbSCADA = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
            List<string> listSCADA = dbSCADA.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown(Project_SCADA, listSCADA);

            DBGeneral dbIO = new DBGeneral(Progress, ResourcesUI.IO + " " + Resources.Language, nameof(FileExtensions.langTypeDB), DBTypeLevel.Base, BaseTypes.ObjectsCPU);
            List<string> listIO = dbIO.GetDBFileList();
            //add items to dropdown
            AddMenuItemDropDown(Project_Language, listIO);
        }

        //-------------------------Data dropdown------------------------------------------

        private void Data_GetDataFromProjectClick(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, Design_GridView);
            if (design.GetDataFromGrid(false))
            {
                DataClass data = new DataClass(Progress, Data_GridView);
                data.ExtractFromDesign(design);
                data.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Data);
            ButtonFunctionFinished(sender);
        }

        private void Data_KKSCombine_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);
            if (data.GetDataFromGrid(false))
            {
                data.MakeKKS();
                data.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Data);
            ButtonFunctionFinished(sender);
        }

        private void Data_FindFunctionMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);

            if (data.GetDataFromGrid(false))
            {
                DataGridView grid = new DataGridView();
                DBLanguage dbLanguage = new DBLanguage(Progress, grid);
                dbLanguage.FunctionType.FindAllFunctionType(data);
            }

            SelectTab(IO_list_automation_new.TabIndex.Data);
            ButtonFunctionFinished(sender);
        }

        private void Data_EditFunctions_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBForceEditForm dBForceEditForm = new DBForceEditForm();
            DBLanguage dbLanguage = new DBLanguage(Progress, dBForceEditForm.Data_Grid);

            dbLanguage.FunctionType.LoadEdit();
            dBForceEditForm.ShowDialog();
            dbLanguage.FunctionType.SaveEdit();

            ButtonFunctionFinished(sender);
        }

        //-------------------------Objects dropdown------------------------------------------
        private void Objects_Find_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);

            if (data.GetDataFromGrid(false))
            {
                objects.ExtractFromData(data);
                objects.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Object);
            ButtonFunctionFinished(sender);
        }

        private void Objects_FindTypeMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);

            if (objects.GetDataFromGrid(false))
            {
                DataGridView grid = new DataGridView();
                DBLanguage dbLanguage = new DBLanguage(Progress, grid);
                dbLanguage.Type.FindAllType(objects);
            }

            SelectTab(IO_list_automation_new.TabIndex.Object);
            ButtonFunctionFinished(sender);
        }

        private void Objects_TransferToData_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);

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

            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            AddressesClass addresses = new AddressesClass(Progress, Addresses_GridView);
            addresses.GetDataFromGrid(true);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral db = new DBGeneral(Progress, Resources.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                db.DecodeAll(data, objects, null, addresses);

                addresses.CheckOverlapAll();
            }

            SelectTab(IO_list_automation_new.TabIndex.Address);
            ButtonFunctionFinished(sender);
        }

        private void Objects_InstancesGenerate_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            AddressesClass addresses = new AddressesClass(Progress, Addresses_GridView);
            addresses.GetDataFromGrid(true);

            //clear all used columns
            for (int objectIndex = 0; objectIndex < objects.Signals.Count; objectIndex++)
                objects.Signals[objectIndex].SetValueFromString(string.Empty, KeywordColumn.Used);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral db = new DBGeneral(Progress, Resources.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                db.DecodeAll(data, objects, null, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void ClearAddresses_Click(object sender, EventArgs e)
        {
            AddressesClass addresses = new AddressesClass(Progress, Addresses_GridView);
            addresses.Grid.GridClear();
        }

        private void Objects_EditTypes_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBForceEditForm dBForceEditForm = new DBForceEditForm();
            DBLanguage dbLanguage = new DBLanguage(Progress, dBForceEditForm.Data_Grid);

            dbLanguage.Type.LoadEdit();
            dBForceEditForm.ShowDialog();
            dbLanguage.Type.SaveEdit();

            ButtonFunctionFinished(sender);
        }

        private void Objects_DeclareEdit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral db = new DBGeneral(Progress, Resources.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            db.EditAll();

            ButtonFunctionFinished(sender);
        }

        private void Objects_InstancesEdit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral db = new DBGeneral(Progress, Resources.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            db.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------IO dropdown------------------------------------------
        private void IO_FndModules_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);

            if (data.GetDataFromGrid(false))
            {
                ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
                modules.ExtractFromData(data);
                modules.PutDataToGrid(false);
            }

            SelectTab(IO_list_automation_new.TabIndex.Modules);
            ButtonFunctionFinished(sender);
        }

        private void IO_Generate_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
            AddressesClass addresses = new AddressesClass(Progress, Addresses_GridView);
            addresses.GetDataFromGrid(true);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false) && modules.GetDataFromGrid(false))
            {
                DBGeneral db = new DBGeneral(Progress, ResourcesUI.Modules, nameof(FileExtensions.modDB), DBTypeLevel.CPU, BaseTypes.ModuleCPU);
                db.DecodeAll(data, objects, modules, addresses);

                addresses.CheckOverlapAll();
            }

            SelectTab(IO_list_automation_new.TabIndex.Address);
            ButtonFunctionFinished(sender);
        }

        private void IO_Edit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral db = new DBGeneral(Progress, ResourcesUI.Modules, nameof(FileExtensions.modDB), DBTypeLevel.CPU, BaseTypes.ModuleCPU);
            db.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------SCADA dropdown------------------------------------------

        private void SCADA_GenerateObjects_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
            AddressesClass addresses = new AddressesClass(Progress, Addresses_GridView);
            addresses.GetDataFromGrid(true);

            if (modules.GetDataFromGrid(false) && data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral db = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectSCADA);
                db.DecodeAll(data, objects, modules, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void SCADA_GenerateModules_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, Data_GridView);
            ObjectsClass objects = new ObjectsClass(Progress, Objects_GridView);
            ModuleClass modules = new ModuleClass(Progress, Modules_GridView);
            AddressesClass addresses = new AddressesClass(Progress, Addresses_GridView);
            addresses.GetDataFromGrid(true);

            if (modules.GetDataFromGrid(false) && data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral db = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.modScadaDB), DBTypeLevel.SCADA, BaseTypes.ModuleSCADA);
                db.DecodeAll(data, objects, modules, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void SCADA_EditObjects_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral db = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectSCADA);
            db.EditAll();

            ButtonFunctionFinished(sender);
        }

        private void SCADA_EditModules_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral db = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.modScadaDB), DBTypeLevel.SCADA, BaseTypes.ModuleSCADA);
            db.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------------------------------------------------
        private void MainWindowMouse_Click(object sender, MouseEventArgs e)
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

        private void Design_GridViewCell_Click(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void DataGridViewCell_Click(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void ObjectsGridViewCell_Click(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void Modules_GridViewCell_Click(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void Addresses_GridViewCell_Click(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
        }

        private void AutoSizeAllTabs()
        {
            foreach (TabPage tab in MainTabControl.TabPages)
            {
                MainTabControl.SelectedTab = tab;
                ((DataGridView)tab.Controls[0]).AutoResizeColumns();
            }
            MainTabControl.SelectedIndex = (int)IO_list_automation_new.TabIndex.Data;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            AutoSizeAllTabs();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            int startRow = 0;
            int startColumn = 0;

            DataGridView grid = (DataGridView)MainTabControl.SelectedTab.Controls[0];

            if (grid.SelectedCells.Count >= 1)
            {
                startRow = grid.SelectedCells[0].RowIndex;
                startColumn = grid.SelectedCells[0].ColumnIndex + 1;
            }

            if (grid.RowCount < 1)
                return;

            string cellText;
            int row;

            for (int rowIndex = 0; rowIndex < grid.RowCount - 1; rowIndex++)
            {
                // to be able to go back from beginning
                row = rowIndex + startRow;
                if (row >= grid.RowCount - 1)
                    row -= grid.RowCount - 1;

                // fill all cells with data
                for (int column = startColumn; column <= grid.ColumnCount - 1; column++)
                {
                    cellText = grid.Rows[row].Cells[column].Value.ToString();

                    if (!cellText.Contains(FindTextBox.Text))
                        continue;

                    grid.ClearSelection();

                    //go to that cell and select it
                    grid.CurrentCell = grid.Rows[row].Cells[column];
                    grid.Rows[row].Cells[column].Selected = true;

                    //breaking first loop
                    rowIndex = grid.RowCount;
                    break;
                }
                startColumn = 0;
            }
        }
    }
}