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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IO_list_automation_new.GeneralColumnName;
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
                
                design.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                data.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
                objects.Grid.LoadFromFile(Settings.Default.AutoLoadFile);
            }

            this.comboboxColumn.DisplayMember = "Name";
            this.comboboxColumn.Format += (s, e) =>
            {
                if (e.ListItem is DropDownClass.DropDownElement item)
                    e.Value = item.GetName();
            };
        }

        private DataGridView GetCurrentGrid()
        {
            TabIndexs _index = (TabIndexs)tabControl1.SelectedIndex;
            DataGridView _grid = new DataGridView();
            switch (_index)
            {
                case TabIndexs.Design:
                    _grid = DesignGridView;
                    break;
                case TabIndexs.Data:
                    _grid = DataGridView;
                    break;
                case TabIndexs.Object:
                    _grid = ObjectsGridView;
                    break;
                default:
                    string text = "GetCurrentGrid";
                    Debug _debug1 = new Debug();
                    _debug1.ToFile("Report to programer that " + text + ": " + _grid.Name + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + " is not created for this element");
            }
            return _grid;
        }

        /// <summary>
        /// Informs what button was pressed
        /// </summary>
        /// <param name="buttonName">Button name which was pressed</param>
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
        /// <param name="buttonName">Button name which was pressed</param>
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
        /// <param name="buttonName">Button name which was pressed</param>
        private void DisplayNoFunction(object button)
        {
            Debug _debug = new Debug();
            ToolStripMenuItem _button = (ToolStripMenuItem)button;
            string _buttonName = _button.Text;
            _debug.ToPopUp($"{Resources.NoFunction}: {_buttonName}", DebugLevels.None, DebugMessageType.Critical);
        }

        /// <summary>
        /// General confirmation of action
        /// </summary>
        /// <param name="message">Action to be confirmed</param>
        /// <returns></returns>
        public DialogResult ShowConfirmWindow(string message)
        {
            MessageBoxIcon _icon = MessageBoxIcon.Question;
            string _title = "Confirmation";
            MessageBoxButtons _buttons = MessageBoxButtons.YesNoCancel;

            return MessageBox.Show(message, _title, _buttons, _icon);
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEvent(sender, e);
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEvent(sender, e);
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

        private void comboboxColumn_SelectedValueChanged(object sender, EventArgs e)
        {
            DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            if (comboBox.ValidCheck())
            {
                string _keyword = comboBox.SelectedKeyword();
                bool _columnAdd = comboBox.SelectedMod().Contains(Resources.Add);
                string _columnName = comboBox.GetName();

                TabIndexs _index = (TabIndexs)tabControl1.SelectedIndex;
                DataGridView _grid = GetCurrentGrid();

                if (_columnAdd)
                {
                    DataGridViewColumn _columnGridView = new DataGridViewColumn();

                    DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();
                    _columnGridView.CellTemplate = _cell;
                    _columnGridView.Name = _keyword;
                    _columnGridView.HeaderText = _columnName;
                    _columnGridView.SortMode = DataGridViewColumnSortMode.Automatic;
                    _grid.Columns.Insert(_grid.ColumnCount, _columnGridView);
                }
                else
                {
                    //get collumn name and number of column to remove
                    int _collumnNumber = 0;
                    for (_collumnNumber = 0; _collumnNumber < _grid.ColumnCount; _collumnNumber++)
                    {
                        if (_grid.Columns[_collumnNumber].Name == _keyword)
                        {
                            _columnName = _grid.Columns[_collumnNumber].HeaderText;
                            break;
                        }
                    }

                    _grid.Columns.Remove(_grid.Columns[_collumnNumber]);
                }

                switch (_index)
                {
                    case TabIndexs.Design:
                        DesignClass design = new DesignClass(Progress, DesignGridView);
                        break;
                    case TabIndexs.Data:
                        DataClass data = new DataClass(Progress, DataGridView);
                        break;
                    case TabIndexs.Object:
                        ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                        break;
                    default:
                        DisplayNoFunction(sender);
                        break;
                }
                //after add/remove hide combobox
                comboboxColumn.Visible = false;
                comboboxColumn.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Function to add column list to dropbox which can be added or removed from grid
        /// </summary>
        /// <param name="e">event arguments</param>
        /// <param name="columnList">column list visible</param>
        /// <param name="baseColumnList">base column list</param>
        private void GridView_Click(EventArgs e,  ColumnList columnList, ColumnList baseColumnList)
        {
            Point relativePoint = PointToClient(Cursor.Position);

            MouseEventArgs _mouse = (MouseEventArgs)e;
            if (_mouse.Button == MouseButtons.Right)
            {
                Debug _debug = new Debug();
                _debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

                //change location to mouse press location and clear previous dropdown
                comboboxColumn.Location = relativePoint;

                List<DropDownClass.DropDownElement> _items = new List<DropDownClass.DropDownElement>();
                DropDownClass DropDowns = new DropDownClass(comboboxColumn);

                //current columns that are shown can be removed
                foreach (GeneralColumn _column in columnList)
                {
                    if (!_column.CanHide)
                        continue;

                    DropDowns.AddItemColumn(Resources.Remove + ": ", _column.Keyword);
                }

                string _keyword = string.Empty;
                bool _found = false;
                //current columns that are not visible can be added
                foreach (GeneralColumn basecolumn in baseColumnList)
                {
                    _found = false;
                    _keyword = basecolumn.Keyword;
                    foreach (GeneralColumn _column in columnList)
                    {
                        if (_keyword == _column.Keyword)
                        {
                            _found = true;
                            break;
                        }
                    }
                    // when column list doesn not have keyword that is in base than column can be added
                    if (!_found)
                        DropDowns.AddItemColumn(Resources.Remove + ": ", _keyword);

                }
                comboboxColumn.DataSource = _items;
                comboboxColumn.Visible = true;
            }
            else if (comboboxColumn.Visible)
                comboboxColumn.Visible = false;
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxColumn.Visible = false;
        }

        /// <summary>
        /// Paste data to grid
        /// </summary>
        /// <param name="_grid"></param>
        private void Global_paste(DataGridView _grid)
        {
            Debug _debug = new Debug();

            IDataObject _dataInclipboard = Clipboard.GetDataObject();

            string _stringInClipboard = _dataInclipboard.GetData(DataFormats.UnicodeText).ToString();

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
                        int _col = 0;

                        //when only 1 row is copyed, paste it in all sellected rows
                        if (_rowsInBoard == 1)
                        {
                            Progress.RenameProgressBar(Resources.PasteData + ": " + _grid.Name, _grid.SelectedCells.Count);
                            for (int i = 0; i < _grid.SelectedCells.Count; i++)
                            {
                                _row = _grid.SelectedCells[i].RowIndex;
                                for (_col = 0; _col < _clipboardCells.Length; _col++)
                                    _grid.Rows[_row].Cells[_selColMin + _col].Value = _clipboardCells[_col];

                                Progress.UpdateProgressBar(i);
                            }
                        }
                        else
                        {
                            Progress.RenameProgressBar(Resources.PasteData + ": " + _grid.Name, _rowsInBoard);
                            for (_row = 0; _row < _rowsInBoard; _row++)
                            {
                                _clipboardCells = _clipboardRows[_row].Split('\t');
                                for (_col = 0; _col < _clipboardCells.Length - 1; _col++)
                                {
                                    _grid.Rows[_selRowMin + _row].Cells[_selColMin + _col].Value = _clipboardCells[_col];
                                }
                                _clipboardCells = _clipboardCells[_col].Split('\r');
                                _grid.Rows[_selRowMin + _row].Cells[_selColMin + _col].Value = _clipboardCells[0];
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
        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            // Delete function
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                DataGridView _grid = GetCurrentGrid();

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
                DataGridView _grid = GetCurrentGrid();

                if (_grid.SelectedCells.Count > 0)
                    Global_paste(_grid);

                this.Update();
            }
        }

        //File drop down
        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            TabIndexs _index = (TabIndexs)tabControl1.SelectedIndex;
            switch (_index)
            {
                case TabIndexs.Design:
                    DesignClass design = new DesignClass(Progress, DesignGridView);
                    design.Grid.SaveSellect();
                    break;
                case TabIndexs.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.Grid.SaveSellect();
                    break;
                case TabIndexs.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.Grid.SaveSellect();
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

            _saveFile.Filter = "All save files|*" + design.Grid.FileExtension + ";*" + data.Grid.FileExtension + ";*" + objects.Grid.FileExtension;
            if (_saveFile.ShowDialog() == DialogResult.OK)
            {
                design.Grid.SaveToFile(_saveFile.FileName);
                data.Grid.SaveToFile(_saveFile.FileName);
                objects.Grid.SaveToFile(_saveFile.FileName);

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

            TabIndexs _index = (TabIndexs)tabControl1.SelectedIndex;
            switch (_index)
            {
                case TabIndexs.Design:
                    DesignClass design = new DesignClass(Progress, DesignGridView);
                    design.Grid.LoadSellect();
                    break;
                case TabIndexs.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.Grid.LoadSellect();
                    break;
                case TabIndexs.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.Grid.LoadSellect();
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

            _loadFile.Filter = "All save files|*" + design.Grid.FileExtension + ";*" + data.Grid.FileExtension + ";*" + objects.Grid.FileExtension;
            if (_loadFile.ShowDialog() == DialogResult.OK)
            {
                design.Grid.LoadFromFile(_loadFile.FileName);
                data.Grid.LoadFromFile(_loadFile.FileName);
                objects.Grid.LoadFromFile(_loadFile.FileName);
            }
            else
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);

            ButtonFunctionFinished(sender);
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
            DialogResult _result = ShowConfirmWindow(Resources.ConfirmExit);
            if (_result == DialogResult.OK || _result == DialogResult.Yes)
                Application.Exit();
        }

//Project drop down
        private void ProjectDesignMenuItem_Click(object sender, EventArgs e)
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

        private void ProjectCPUAddMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void ProjectSCADAAddMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

        private void ProjectLanguageAddMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

//data dropdown
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

//objects dropdown
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

//IO dropdown
        private void IOEditMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

//declare dropdown
        private void DeclareEditMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }

//Instances dropdown

        private void InstancesGenerateMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (design.Grid.GetData() && data.Grid.GetData() && objects.Grid.GetData())
            {
                DBGeneralInstances Instances = new DBGeneralInstances(Progress);
                Instances.DecodeAll(data, objects);
            }
            ButtonFunctionFinished(sender);
        }

        private void InstancesEditMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DesignClass design = new DesignClass(Progress, DesignGridView);
            DataClass data = new DataClass(Progress, DataGridView);
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);

            if (design.Grid.GetData() && data.Grid.GetData() && objects.Grid.GetData())
            {
                DBGeneralInstances Instances = new DBGeneralInstances(Progress);
                Instances.EditAll(data, objects);
            }
            ButtonFunctionFinished(sender);
        }

//SCADA dropdown
        private void SCADAEditMenuItem_Click(object sender, EventArgs e)
        {
            DisplayNoFunction(sender);
        }


    }
}
