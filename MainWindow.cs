using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
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

                DesignClass design = new DesignClass(Progress, DesignGridView);
                DataClass data = new DataClass(Progress, DataGridView);
                ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                AddressesClass address = new AddressesClass(Progress, AddressesGridView);

                design.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                data.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                objects.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                modules.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                address.Grid.LoadFromFile(Settings.Default.AutoLoadFile);

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
            Project_TransferData.Text = ResourcesUI.TransferProjectToData;

            Project_CPU.Text = Resources.CPU;
            Project_CPU_Add.Text = Resources.Add;
            Project_SCADA.Text = ResourcesUI.SCADA;
            Project_SCADA_Add.Text = Resources.Add;
            Project_Language.Text = ResourcesUI.IO + " " + Resources.Language;
            Project_Language_Add.Text = Resources.Add;

            // **********************
            Data_DropDown.Text = ResourcesUI.Data;
            DataFindFunctionMenuItem.Text = ResourcesUI.FindFunction;
            Data_KKSCombine.Text = ResourcesUI.KKSCombine;

            // **********************
            Objects_DropDown.Text = ResourcesUI.Objects;
            Objects_Find.Text = ResourcesUI.ObjectsFindUnique;
            ObjectsFindTypeMenuItem.Text = ResourcesUI.ObjectFindType;
            Object_TransferToData.Text = ResourcesUI.ObjectsTransferData;
            Object_EditTypes.Text = ResourcesUI.ObjectsEdit;

            // **********************
            IO_DropDown.Text = ResourcesUI.IO;
            IO_FindModules.Text = ResourcesUI.FindUniqueModules;
            IO_Generate.Text = ResourcesUI.IOGenerate;
            IO_Edit.Text = ResourcesUI.IODBEdit;

            // **********************
            Declare_DropDown.Text = ResourcesUI.Declare;
            Declare_Edit.Text = ResourcesUI.DeclareDBEdit;
            Declare_Generate.Text = ResourcesUI.DeclareGenerate;
            Declare_ClearAddresses.Text = ResourcesUI.SCADAObjectGenerate;

            // **********************
            Instance_DropDown.Text = ResourcesUI.Instance;
            Instances_Edit.Text = ResourcesUI.InstanceEditDB;
            Instances_Generate.Text = ResourcesUI.InstanceGenerate;

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
        /// Paste data to grid
        /// </summary>
        /// <param name="_grid"></param>
        private void Global_paste(DataGridView _grid)
        {
            Debug _debug = new Debug();

            IDataObject _dataInClipboard = Clipboard.GetDataObject();

            string _stringInClipboard = _dataInClipboard.GetData(DataFormats.UnicodeText).ToString();

            // no row data
            if (_stringInClipboard == null)
            {
                _debug.ToPopUp(Resources.NoPasteData, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            int _selRowMin = _grid.SelectedCells[0].RowIndex;
            int _selColMin = _grid.SelectedCells[0].ColumnIndex;

            int _selRow;
            int _selCol;

            //get selected min and max cells
            for (int i = 0; i < _grid.SelectedCells.Count; i++)
            {
                _selRow = _grid.SelectedCells[i].RowIndex;
                _selCol = _grid.SelectedCells[i].ColumnIndex;

                if (_selRow < _selRowMin)
                    _selRowMin = _selRow;

                if (_selCol < _selColMin)
                    _selColMin = _selCol;
            }

            int _enableRowCount = _grid.RowCount - _selRowMin;
            int _enableColumnCount = _grid.ColumnCount - _selColMin;

            _stringInClipboard = _stringInClipboard.Replace("\r", "");
            string[] _clipboardRows = _stringInClipboard.Split('\n');

            int _rowsInBoard = _clipboardRows.Length;

            string[] _clipboardCells = _clipboardRows[0].Split('\t');
            int _colsInBoard = _clipboardCells.Length;

            if (_rowsInBoard < 1)
            {
                _debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Row, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }
            else if (_colsInBoard < 1)
            {
                _debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Column, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            _debug.ToFile(Resources.PasteData + ": " + _grid.Name +
                            " " + Resources.Row + "(" + _rowsInBoard + ")" +
                            " " + Resources.Column + "(" + _colsInBoard + ")" +
                            " " + Resources.PasteAt + "(" + _selRowMin + ":" + _selColMin + ")"
                            , DebugLevels.Medium, DebugMessageType.Info);

            if ((_enableRowCount < _rowsInBoard) || (_enableColumnCount < _colsInBoard))
            {
                _debug.ToPopUp(Resources.ToMuchDataPaste, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            int _row;
            //when only 1 row is copied, paste it in all selected rows
            if (_rowsInBoard == 1)
            {
                Progress.RenameProgressBar(Resources.PasteData + ": " + _grid.Name, _grid.SelectedCells.Count);
                for (int i = 0; i < _grid.SelectedCells.Count; i++)
                {
                    _row = _grid.SelectedCells[i].RowIndex;
                    for (int _col = 0; _col < _clipboardCells.Length; _col++)
                        _grid.Rows[_row].Cells[_selColMin + _col].Value = _clipboardCells[_col];

                    Progress.UpdateProgressBar(i);
                }
            }
            //else paste to required amount
            else
            {
                Progress.RenameProgressBar(Resources.PasteData + ": " + _grid.Name, _rowsInBoard);
                for (_row = 0; _row < _rowsInBoard; _row++)
                {
                    _clipboardCells = _clipboardRows[_row].Split('\t');
                    for (int _col = 0; _col < _clipboardCells.Length; _col++)
                        _grid.Rows[_selRowMin + _row].Cells[_selColMin + _col].Value = _clipboardCells[_col];

                    Progress.UpdateProgressBar(_row);
                }
            }
            Progress.HideProgressBar();

            this.Update();
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
                DataGridView _grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                for (int i = 0; i < (_grid.SelectedCells.Count); i++)
                    _grid.SelectedCells[i].Value = "";
                this.Update();
            }
            // Paste function
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.V))
            {
                DataGridView _grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                if (_grid.SelectedCells.Count == 0)
                    return;

                Global_paste(_grid);
            }
        }

        //-------------------------File drop down------------------------------------------
        private void File_Save_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            switch ((TabIndex)MainTabControl.SelectedIndex)
            {
                case IO_list_automation_new.TabIndex.Design:
                    DesignClass design = new DesignClass(Progress, DesignGridView);
                    design.Grid.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.Grid.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.Grid.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Modules:
                    ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                    modules.Grid.SaveSelect();
                    break;

                case IO_list_automation_new.TabIndex.Address:
                    AddressesClass address = new AddressesClass(Progress, AddressesGridView);
                    address.Grid.SaveSelect();
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
            design.Grid.SaveToFile(_saveFile.FileName);
            data.Grid.SaveToFile(_saveFile.FileName);
            objects.Grid.SaveToFile(_saveFile.FileName);
            modules.Grid.SaveToFile(_saveFile.FileName);
            address.Grid.SaveToFile(_saveFile.FileName);

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
                    design.Grid.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.Grid.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.Grid.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Modules:
                    ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                    modules.Grid.LoadSelect();
                    break;

                case IO_list_automation_new.TabIndex.Address:
                    AddressesClass address = new AddressesClass(Progress, AddressesGridView);
                    address.Grid.LoadSelect();
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
            design.Grid.LoadFromFile(_loadFile.FileName);
            data.Grid.LoadFromFile(_loadFile.FileName);
            objects.Grid.LoadFromFile(_loadFile.FileName);
            modules.Grid.LoadFromFile(_loadFile.FileName);
            address.Grid.LoadFromFile(_loadFile.FileName);

            Settings.Default.AutoLoadFile = _loadFile.FileName;
            Settings.Default.Save();

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

            ButtonFunctionFinished(sender);
        }

        private void Project_CompareDesign_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void Project_TransferData_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            if (design.GetDataFromGrid(false))
            {
                DataClass data = new DataClass(Progress, DataGridView);
                data.ExtractFromDesign(design);
                data.PutDataToGrid(false);
            }

            ButtonFunctionFinished(sender);
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

        private void Project_CPU_MouseEnter(object sender, EventArgs e)
        {
            DBGeneral _DB = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            List<string> _list = _DB.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown((ToolStripMenuItem)sender, _list);
        }

        private void Project_CPU_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral _DB = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB.ToString), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                NewName _newName = new NewName(Resources.CreateNew + ": " + Resources.CPU,false);

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

        private void Project_SCADA_MouseEnter(object sender, EventArgs e)
        {
            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
            List<string> _list = _DB.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown((ToolStripMenuItem)sender, _list);
        }

        private void Project_SCADA_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.SCADA, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA, BaseTypes.ObjectsCPU);
                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.SCADA,false);

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

        private void Project_Language_MouseEnter(object sender, EventArgs e)
        {
            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.IO + " " + Resources.Language, nameof(FileExtensions.langFuncDB), DBTypeLevel.Base, BaseTypes.ObjectsCPU);
            List<string> _list = _DB.GetDBFileList();
            //add items to dropdown
            AddMenuItemDropDown((ToolStripMenuItem)sender, _list);
        }

        private void Project_Language_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null)
                return;

            if (e.ClickedItem.Text == Resources.Add)
            {
                DataGridView _grid = new DataGridView();
                DBLanguage _DBLanguage = new DBLanguage(Progress,_grid);

                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO + " " + Resources.Language,false);

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
        }

        //-------------------------Data dropdown------------------------------------------
        private void Data_KKSCombine_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            if (data.GetDataFromGrid(false))
            {
                data.MakeKKS();
                data.PutDataToGrid(false);
            }

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

            ButtonFunctionFinished(sender);
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
            }
            ButtonFunctionFinished(sender);
        }

        private void IO_Edit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Modules, nameof(FileExtensions.modDB), DBTypeLevel.CPU, BaseTypes.ModuleCPU);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------Declare dropdown------------------------------------------
        private void Declare_Generate_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.GetDataFromGrid(true);

            if (data.GetDataFromGrid(false) && objects.GetDataFromGrid(false))
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                _DB.DecodeAll(data, objects, null, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void Declare_ClearAddresses_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedIndex = (int)IO_list_automation_new.TabIndex.Address;
            AddressesClass addresses = new AddressesClass(Progress, AddressesGridView);
            addresses.Grid.GridClear();
        }

        private void Declare_Edit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------Instances dropdown------------------------------------------

        private void Instances_Generate_Click(object sender, EventArgs e)
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
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
                _DB.DecodeAll(data, objects, null, addresses);
            }
            ButtonFunctionFinished(sender);
        }

        private void Instances_Edit_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU, BaseTypes.ObjectsCPU);
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
    }
}