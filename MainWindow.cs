using IO_list_automation_new.DB;
using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SharpCompress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static IO_list_automation_new.GeneralColumnName;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IO_list_automation_new
{
    public partial class MainWindow : Form
    {
        ProgressIndication Progress;
        public MainWindow()
        {
            InitializeComponent();
            Progress = new ProgressIndication(ProgressBars,ProgressLabel);

            if (Settings.Default.AutoLoad && Settings.Default.AutoLoadFile.Length>0)
            {
                Debug debug = new Debug();
                debug.ToFile(Resources.FileAutoLoad, DebugLevels.Minimum, DebugMessageType.Info);

                DesignClass design = new DesignClass(Progress, DesignGridView);
                DataClass data = new DataClass(Progress, DataGridView);
                ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                ModuleClass modules = new ModuleClass(Progress, ModulesGridView);

                design.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                data.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                objects.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                modules.Grid.LoadFromFile(Settings.Default.AutoLoadFile);

                debug.ToFile(Resources.FileAutoLoad + " " +Resources.Finished, DebugLevels.Minimum, DebugMessageType.Info);
            }
            UpdateUIElement();
        }

        private void UpdateUIElement()
        {
            // **********************
            FileDropDownButton.Text = ResourcesUI.File;
            FileHelpMenuItem.Text = ResourcesUI.Help;
            FileAboutMenuItem.Text = ResourcesUI.About;
            FileExitMenuItem.Text = ResourcesUI.Exit;
            FileLanguageToolMenuItem.Text = Resources.Language;
            FileLanguageENMenuItem.Text = ResourcesUI.EN ;
            FileLanguageLTMenuItem.Text = ResourcesUI.LT ;
            FileSaveMenuItem.Text = ResourcesUI.Save;
            FileSaveAllMenuItem.Text = ResourcesUI.SaveAll;
            FileLoadMenuItem.Text = ResourcesUI.Load;
            FileLoadAllMenuItem.Text = ResourcesUI.LoadAll ;

            // **********************
            ProjectDropDownButton.Text = ResourcesUI.Project ;
            ProjectGetDataFromDesignMenuItem.Text = ResourcesUI.GetDataFromDesign ;
            ProjectCompareDesignMenuItem.Text = ResourcesUI.CompareWithNewDesign ;
            ProjectTransferDataMenuItem.Text = ResourcesUI.TransferProjectToData;

            ProjectCPUMenuItem.Text = Resources.CPU;
            ProjectCPUAddMenuItem.Text = Resources.Add ;
            ProjectSCADAMenuItem.Text = Resources.SCADA ;
            ProjectSCADAAddMenuItem.Text = Resources.Add;
            ProjectLanguageMenuItem.Text = ResourcesUI.IO + " " + Resources.Language ;
            ProjectLanguageAddMenuItem.Text = Resources.Add;

            // **********************
            DataDropDownButton.Text = ResourcesUI.Data;
            DataFindFunctionMenuItem.Text = ResourcesUI.FindFunction;
            DataKKSCombineMenuItem.Text = ResourcesUI.KKSCombine;

            // **********************
            ObjectsDropDownButton.Text = ResourcesUI.Objects;
            ObjectsFindMenuItem.Text = ResourcesUI.ObjectsFindUnique ;
            ObjectsFindTypeMenuItem.Text = ResourcesUI.ObjectFindType;
            ObjectTransferToDataMenuItem.Text = ResourcesUI.ObjectsTransferData;
            ObjectEditTypesMenuItem.Text = ResourcesUI.ObjectsEdit;

            // **********************
            IODropDownButton.Text = ResourcesUI.IO;
            IOFindModulesMenuItem.Text = ResourcesUI.FindUniqueModules;
            IOEditMenuItem.Text = ResourcesUI.IODBEdit;

            // **********************
            DeclareDropDownButton.Text = ResourcesUI.Declare;
            DeclareEditMenuItem.Text = ResourcesUI.DeclareDBEdit;
            DeclareGenerateMenuItem.Text = ResourcesUI.DeclareGenerate;

            // **********************
            InstanceDropDownButton.Text = ResourcesUI.Instance;
            InstancesEditMenuItem.Text = ResourcesUI.InstanceDBEdit;
            InstancesGenerateMenuItem.Text = ResourcesUI.InstanceGenerate;

            // **********************
            SCADADropDownButton.Text = Resources.SCADA;
            SCADAEditMenuItem.Text =  ResourcesUI.ScadaEdit;

            // **********************
            DataTab.Text = ResourcesUI.Data;
            DesignTab.Text = ResourcesUI.Project;
            ModuleTab.Text = ResourcesUI.Modules;
            ObjectTab.Text = ResourcesUI.Objects;

            // **********************
            FindButton.Text =  ResourcesUI.Find;
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
 /*           if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                DialogResult _result = ShowConfirmWindow(Resources.ConfirmExit);
                if (_result == DialogResult.OK || _result == DialogResult.Yes)
                    Application.Exit();
                else
                    e.Cancel = true;
            }
*/
        }

        private void ComboBoxColumn_SelectedValueChanged(object sender, EventArgs e)
        {
            DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            if (comboBox.ValidCheck())
            {
                string _keyword = comboBox.SelectedKeyword();

                TabIndex _index = (TabIndex)tabControl1.SelectedIndex;
                DataGridView _grid = (DataGridView)tabControl1.SelectedTab.Controls[0];

                //if add column
                if (comboBox.SelectedMod().Contains(Resources.Add))
                {
                    DataGridViewColumn _columnGridView = new DataGridViewColumn();

                    DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();
                    _columnGridView.CellTemplate = _cell;
                    _columnGridView.Name = _keyword;
                    _columnGridView.HeaderText = comboBox.SelectedName();
                    _columnGridView.SortMode = DataGridViewColumnSortMode.Automatic;
                    _grid.Columns.Insert(_grid.ColumnCount, _columnGridView);
                }
                //remove column
                else
                {
                    //get column name and number of column to remove
                    for (int _columnNumber = 0; _columnNumber < _grid.ColumnCount; _columnNumber++)
                    {
                        if (_grid.Columns[_columnNumber].Name == _keyword)
                        {
                            _grid.Columns.Remove(_grid.Columns[_columnNumber]);
                            break;
                        }
                    }
                }

                //after add/remove hide combo box
                comboBox.Visible = false;
                comboBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Function to add column list to ComboBox which can be added or removed from grid
        /// </summary>
        /// <param name="e">event arguments</param>
        /// <param name="columnList">column list visible</param>
        /// <param name="baseColumnList">base column list</param>
        private void GridView_Click(EventArgs e,  ColumnList columnList, ColumnList baseColumnList)
        {
            DropDownClass comboBox = new DropDownClass(comboBoxColumn);
            MouseEventArgs _mouse = (MouseEventArgs)e;

            if (_mouse.Button == MouseButtons.Right)
            {
                Debug _debug = new Debug();
                _debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

                //change location to mouse press location and clear previous dropdown
                comboBox.Location = PointToClient(Cursor.Position);
                comboBox.ClearItems();

                //current columns that are shown can be removed
                foreach (GeneralColumn _column in columnList)
                {
                    if (!_column.CanHide)
                        continue;

                    comboBox.AddItemColumn(Resources.Remove + ": ", _column.Keyword);
                }

                string _keyword = string.Empty;
                bool _found = false;
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
                        comboBox.AddItemColumn(Resources.Add + ": ", _keyword);
                }
                comboBox.ChangeDisplayMember(DropDownElementType.FullName);
                comboBox.Visible = true;
            }
            else if (comboBox.Visible)
                comboBox.Visible = false;
        }

        private void DesignGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DesignClass design = new DesignClass(Progress, DesignGridView);
            GridView_Click(e, design.Columns, design.BaseColumns);
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataClass data = new DataClass(Progress, DataGridView);
            GridView_Click(e, data.Columns, data.BaseColumns);
        }

        private void ObjectsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            GridView_Click(e, objects.Columns, objects.BaseColumns);
        }

        private void ModulesGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
            GridView_Click(e, modules.Columns, modules.BaseColumns);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
//            DataGridView _grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];
//            _grid.AutoResizeColumns();
            comboBoxColumn.Visible = false;
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
                _debug.ToPopUp(Resources.NoPasteData, DebugLevels.None, DebugMessageType.Alarm);
            else
            {
                int _selRowMin = _grid.SelectedCells[0].RowIndex;
                int _selColMin = _grid.SelectedCells[0].ColumnIndex;

                int _selRow = 0;
                int _selCol = 0;

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
                    _debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Row, DebugLevels.None, DebugMessageType.Alarm);
                else if (_colsInBoard < 1)
                    _debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Column, DebugLevels.None, DebugMessageType.Alarm);
                else
                {
                    _debug.ToFile(Resources.PasteData + ": " + _grid.Name +
                                    " " + Resources.Row + "(" + _rowsInBoard + ")" +
                                    " " + Resources.Column + "(" + _colsInBoard + ")" +
                                    " " + Resources.PasteAt + "(" + _selRowMin + ":" + _selColMin + ")"
                                    , DebugLevels.Medium, DebugMessageType.Info);

                    if ((_enableRowCount < _rowsInBoard) || (_enableColumnCount < _colsInBoard))
                        _debug.ToPopUp(Resources.ToMuchDataPaste, DebugLevels.None, DebugMessageType.Alarm);
                    else
                    {
                        int _row = 0;

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
                    }
                }
            }
        }

        /// <summary>
        /// Key Down event for all forms and application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown_Event(object sender, KeyEventArgs e)
        {
            // Delete function
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                DataGridView _grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                if (_grid.SelectedCells.Count > 1)
                {
                    for (int i = 0; i < (_grid.SelectedCells.Count); i++)
                        _grid.SelectedCells[i].Value = "";
                }
                this.Update();
            }
            // Paste function
            else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.V))
            {
                DataGridView _grid = (DataGridView)((TabControl)sender).SelectedTab.Controls[0];

                if (_grid.SelectedCells.Count > 0)
                    Global_paste(_grid);

                this.Update();
            }
        }

        //-------------------------File drop down------------------------------------------
        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            TabIndex _index = (TabIndex)tabControl1.SelectedIndex;
            switch (_index)
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
                default:
                    DisplayNoFunction(sender);
                    break;
            }

            ButtonFunctionFinished(sender);
        }

        private void FileSaveAllMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            Debug debug = new Debug();

            SaveFileDialog _saveFile = new SaveFileDialog();

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);

            _saveFile.Filter = "All save files|*" + design.Grid.FileExtension + ";*" + data.Grid.FileExtension
                                                    + ";*" + objects.Grid.FileExtension + ";*" + modules.Grid.FileExtension;
            if (_saveFile.ShowDialog() == DialogResult.OK)
            {
                design.Grid.SaveToFile(_saveFile.FileName);
                data.Grid.SaveToFile(_saveFile.FileName);
                objects.Grid.SaveToFile(_saveFile.FileName);
                modules.Grid.SaveToFile(_saveFile.FileName);

                Settings.Default.AutoLoadFile = _saveFile.FileName;
                Settings.Default.Save();
            }
            else
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);

            ButtonFunctionFinished(sender);
        }

        private void FileLoadMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            TabIndex _index = (TabIndex)tabControl1.SelectedIndex;
            switch (_index)
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
                default:
                    DisplayNoFunction(sender);
                    break;
            }
            ButtonFunctionFinished(sender);
        }

        private void FileLoadAllMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            Debug debug = new Debug();

            OpenFileDialog _loadFile = new OpenFileDialog();

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            ModuleClass modules = new ModuleClass(Progress, ModulesGridView);

            _loadFile.Filter = "All save files|*" + design.Grid.FileExtension + ";*" + data.Grid.FileExtension
                                        + ";*" + objects.Grid.FileExtension + ";*" + modules.Grid.FileExtension;

            if (_loadFile.ShowDialog() == DialogResult.OK)
            {
                design.Grid.LoadFromFile(_loadFile.FileName);
                data.Grid.LoadFromFile(_loadFile.FileName);
                objects.Grid.LoadFromFile(_loadFile.FileName);
                modules.Grid.LoadFromFile(_loadFile.FileName);
            }
            else
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);

            ButtonFunctionFinished(sender);
        }

        private void FileLanguage_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _item = (ToolStripMenuItem)sender;

            if (Settings.Default.ApplicationLanguage != _item.Text.ToLower())
            {
                Settings.Default.ApplicationLanguage = _item.Text.ToLower();
                Settings.Default.Save();
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Default.ApplicationLanguage);

                UpdateUIElement();
                this.Update();
            }
        }

        private void FileHelpMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void FileAboutMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show(Resources.ConfirmExit, "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (_result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        //-------------------------Project dropdown------------------------------------------
        private void ProjectGetDataFromDesignMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            if (design.GetDataFromImportFile())
                design.Grid.PutData();

            ButtonFunctionFinished(sender);
        }

        private void ProjectCompareDesignMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void ProjectTransferDataMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            if (design.Grid.GetData())
            {
                DataClass data = new DataClass(Progress, DataGridView);
                data.ExtractFromDesign(design);
                data.Grid.PutData();
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
            for (int i = menuItem.DropDownItems.Count-1; i > 0; i--)
                menuItem.DropDownItems.Remove(menuItem.DropDownItems[i]);

            //add items
            for (int i = 0; i < list.Count; i++)
                menuItem.DropDownItems.Add(list[i]);
        }

        private void ProjectCPUMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DBGeneral _DB = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB), DBTypeLevel.CPU);
            List<string> _list = _DB.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown((ToolStripMenuItem)sender, _list);
        }

        private void ProjectCPUMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null) { }
            else if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral _DB = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decDB.ToString), DBTypeLevel.CPU);
                NewName _newName = new NewName(Resources.CreateNew + ": " + Resources.CPU);

                _newName.ShowDialog();
                if (!string.IsNullOrEmpty(_newName.Output))
                    _DB.DBFolderCreate(_newName.Output);
            }
            else
            {
                Settings.Default.SelectedCPU = e.ClickedItem.Text;
                DBGeneral _DBScada = new DBGeneral(Progress, Resources.CPU, nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA);
                List<string> _list = _DBScada.GetDBFolderList();

                //set SCADA for first element
                if (_list.Count > 0)
                    Settings.Default.SelectedSCADA = _list[0];
                else
                    Settings.Default.SelectedSCADA = "-";
                Settings.Default.Save();
            }
        }

        private void ProjectSCADAMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DBGeneral _DB = new DBGeneral(Progress, Resources.SCADA,nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA);
            List<string> _list = _DB.GetDBFolderList();
            //add items to dropdown
            AddMenuItemDropDown((ToolStripMenuItem)sender, _list);
        }

        private void ProjectSCADAMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null) { }
            else if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral _DB = new DBGeneral(Progress, Resources.SCADA,nameof(FileExtensions.decScadaDB), DBTypeLevel.SCADA);
                NewName _newName = new NewName(Resources.CreateNew + ": " + Resources.SCADA);

                _newName.ShowDialog();
                if (!string.IsNullOrEmpty(_newName.Output))
                    _DB.DBFolderCreate(_newName.Output);
            }
            else
            {
                Settings.Default.SelectedSCADA = e.ClickedItem.Text;
                Settings.Default.Save();
            }
        }

        private void ProjectLanguageMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.IO + " " + Resources.Language,nameof(FileExtensions.langFuncDB), DBTypeLevel.Base);
            List<string> _list = _DB.GetDBFileList();
            //add items to dropdown
            AddMenuItemDropDown((ToolStripMenuItem)sender, _list);
        }

        private void ProjectLanguageMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == null) { }
            else if (e.ClickedItem.Text == Resources.Add)
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.IO + " " + Resources.Language, nameof(FileExtensions.langFuncDB), DBTypeLevel.Base);
                NewName _newName = new NewName(Resources.CreateNew + ": " + ResourcesUI.IO +" " + Resources.Language);

                _newName.ShowDialog();
                if (!string.IsNullOrEmpty(_newName.Output))
                {
                    DBGeneral _DBType = new DBGeneral(Progress, ResourcesUI.IO + " " + Resources.Language, nameof(FileExtensions.langTypeDB), DBTypeLevel.Base);
                    _DB.CreateDBFile(_newName.Output);
                    _DBType.CreateDBFile(_newName.Output);
                }
            }
            else
            {
                Settings.Default.IOLanguage = e.ClickedItem.Text;
                Settings.Default.Save();
            }
        }

        private void ProjectDropDownButton_DropDownOpened(object sender, EventArgs e)
        {
            ProjectCPUMenuItem.Text = Resources.CPU + ": " + Settings.Default.SelectedCPU;
            ProjectSCADAMenuItem.Text = Resources.SCADA + ": " + Settings.Default.SelectedSCADA;
            ProjectLanguageMenuItem.Text = ResourcesUI.IO + " "+ Resources.Language + ": " + Settings.Default.IOLanguage;
        }

        //-------------------------Data dropdown------------------------------------------
        private void DataKKSCombineMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            if (data.Grid.GetData())
            {
                data.MakeKKS();
                data.Grid.PutData();
            }

            ButtonFunctionFinished(sender);
        }

        private void DataFindFunctionMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);

            if (data.Grid.GetData())
            {
                DataGridView _grid = new DataGridView();
                DBLanguage _DBLanguage = new DBLanguage(Progress, _grid);
                if (_DBLanguage.FunctionType.Grid.LoadFromFileToMemory(DeleteMe.LTpath))
                    _DBLanguage.FunctionType.FindAllFunctionType(data);
            }

            ButtonFunctionFinished(sender);
        }

        //-------------------------Objects dropdown------------------------------------------
        private void ObjectsFindMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (data.Grid.GetData())
            {
                objects.ExtractFromData(data);
                objects.Grid.PutData();
            }

            ButtonFunctionFinished(sender);
        }

        private void ObjectsFindTypeMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (objects.Grid.GetData())
            {
                DataGridView _grid = new DataGridView();
                DBLanguage _DBLanguage = new DBLanguage(Progress, _grid);
                if (_DBLanguage.Type.Grid.LoadFromFileToMemory(DeleteMe.LTpath))
                    _DBLanguage.Type.FindAllType(objects);
            }

            ButtonFunctionFinished(sender);
        }

        private void ObjectTransferToDataMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (data.Grid.GetData())
            {
                if (objects.Grid.GetData())
                {
                    objects.SendToData(data);
                    data.Grid.PutData();
                }
            }
            ButtonFunctionFinished(sender);
        }

        private void ObjectEditTypesMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        //-------------------------IO dropdown------------------------------------------
        private void IOFindModulesMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);

            if (data.Grid.GetData())
            {
                ModuleClass modules = new ModuleClass(Progress, ModulesGridView);
                modules.ExtractFromData(data);
                modules.Grid.PutData();
            }

            ButtonFunctionFinished(sender);
        }
        private void IOEditMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        //-------------------------Declare dropdown------------------------------------------
        private void DeclareGenerateMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (design.Grid.GetData() && data.Grid.GetData() && objects.Grid.GetData())
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU);
                _DB.DecodeAll(data, objects);
            }
            ButtonFunctionFinished(sender);
        }

        private void DeclareEditMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Declare, nameof(FileExtensions.decDB), DBTypeLevel.CPU);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------Instances dropdown------------------------------------------

        private void InstancesGenerateMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (design.Grid.GetData() && data.Grid.GetData() && objects.Grid.GetData())
            {
                DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU);
                _DB.DecodeAll(data, objects);
            }
            ButtonFunctionFinished(sender);
        }

        private void InstancesEditMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DBGeneral _DB = new DBGeneral(Progress, ResourcesUI.Instance, nameof(FileExtensions.instDB), DBTypeLevel.CPU);
            _DB.EditAll();

            ButtonFunctionFinished(sender);
        }

        //-------------------------SCADA dropdown------------------------------------------
        private void SCADAEditMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }
    }
}
