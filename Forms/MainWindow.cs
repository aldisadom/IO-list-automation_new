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
        }

        private DataGridView GetCurrentGrid(object sender)
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
                    DisplayNoFunction(sender);
                    break;
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
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                DialogResult _result = ShowConfirmWindow(Resources.ConfirmExit);
                if (_result == DialogResult.OK || _result == DialogResult.Yes)
                    Application.Exit();
                else
                    e.Cancel = true;
            }

        }

        private void comboboxColumn_SelectedValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;

            if (comboBox.SelectedIndex >= 0)
            {
                string _keyword = comboBox.Text.Substring(comboBox.Text.IndexOf("#@") + 2);
                bool _columnAdd = comboBox.Text.Contains(Resources.Add + ":");
                int _startIndex = comboBox.Text.IndexOf(":")+2;
                int _endIndex = comboBox.Text.IndexOf("                             #@");
                string _columnName = comboBox.Text.Substring(_startIndex,_endIndex- _startIndex);

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
                }

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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="grid"></param>
        /// <param name="columnList"></param>
        private void GridView_Click(object sender, EventArgs e, DataGridView grid, ColumnList columnList, ColumnList baseColumnList)
        {
            MouseEventArgs _mouse = (MouseEventArgs)e;
            if (_mouse.Button == MouseButtons.Right)
            {
                Debug _debug = new Debug();
                _debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Created, DebugLevels.Minimum, DebugMessageType.Info);

                //change location to mouse press location and clear previous dropdown
                comboboxColumn.Location = new System.Drawing.Point(_mouse.X + tabControl1.Location.X, _mouse.Y + tabControl1.Location.Y);
                comboboxColumn.Items.Clear();
                
                string _keyword = string.Empty;
                string _boxText = string.Empty;
                int _maxTextLength = -1;

                //current columns that are shown can be removed
                foreach (GeneralColumn column in columnList)
                {
                    _keyword = column.GetColumnKeyword();
                    _boxText = Resources.Remove+": " + column.GetColumnName(_keyword);
                    if (_maxTextLength < _boxText.Length)
                        _maxTextLength = _boxText.Length;

                    comboboxColumn.Items.Add(_boxText + "                             #@" + _keyword);
                }

                bool _found = false;
                //current columns that are not visible can be added
                foreach (GeneralColumn basecolumn in baseColumnList)
                {
                    _found = false;
                    _keyword = basecolumn.GetColumnKeyword();
                    foreach (GeneralColumn column in columnList)
                    {
                        if (_keyword == column.GetColumnKeyword())
                        {
                            _found = true;
                            break;
                        }
                    }
                    // when column list doesn not have keyword that is in base than column can be added
                    if (!_found)
                    {
                        _boxText = Resources.Add + ": " + basecolumn.GetColumnName(_keyword);
                        if (_maxTextLength < _boxText.Length)
                            _maxTextLength = _boxText.Length;

                        comboboxColumn.Items.Add(_boxText + "                             #@" + _keyword);
                    }
                }
                comboboxColumn.Width = _maxTextLength *6;
                comboboxColumn.Visible = true;
            }
            else if (comboboxColumn.Visible)
            {
                Debug _debug = new Debug();
                _debug.ToFile(Resources.ColumnAddDropDown + ": " + Resources.Hidden, DebugLevels.Minimum, DebugMessageType.Info);

                comboboxColumn.Visible = false;
            }
        }

        private void DesignGridView_Click(object sender, EventArgs e)
        {
            DesignClass design = new DesignClass(Progress, DesignGridView);
            GridView_Click(sender, e, DesignGridView, design.Columns, design.BaseColumns);
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            DataClass data = new DataClass(Progress, DataGridView);
            GridView_Click(sender, e, DataGridView, data.Columns, data.BaseColumns);
        }

        private void ObjectsGridView_Click(object sender, EventArgs e)
        {
            ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
            GridView_Click(sender, e, ObjectsGridView, objects.Columns, objects.BaseColumns);
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
                DataGridView _grid = GetCurrentGrid(sender);

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
                DataGridView _grid = GetCurrentGrid(sender);

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
                    design.Gridasikas.SaveSellect();
                    break;
                case TabIndexs.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.Gridasikas.SaveSellect();
                    break;
                case TabIndexs.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.Gridasikas.SaveSellect();
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
            _saveFile.Filter = "All save files|*." + Const.DesignName + ";*." + Const.DataName + ";*." + Const.ObjectName;
            if (_saveFile.ShowDialog() == DialogResult.OK)
            {
                DesignClass design = new DesignClass(Progress, DesignGridView);
                design.Gridasikas.SaveToFile(_saveFile.FileName);

                DataClass data = new DataClass(Progress, DataGridView);
                data.Gridasikas.SaveToFile(_saveFile.FileName);

                ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                objects.Gridasikas.SaveToFile(_saveFile.FileName);
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
                    design.Gridasikas.LoadSellect();
                    break;
                case TabIndexs.Data:
                    DataClass data = new DataClass(Progress, DataGridView);
                    data.Gridasikas.LoadSellect();
                    break;
                case TabIndexs.Object:
                    ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                    objects.Gridasikas.LoadSellect();
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
            _loadFile.Filter = "All save files|*." + Const.DesignName + ";*." + Const.DataName + ";*." + Const.ObjectName;
            if (_loadFile.ShowDialog() == DialogResult.OK)
            {
                DesignClass design = new DesignClass(Progress, DesignGridView);
                design.Gridasikas.LoadFromFile(_loadFile.FileName);

                DataClass data = new DataClass(Progress, DataGridView);
                data.Gridasikas.LoadFromFile(_loadFile.FileName);

                ObjectsClass objects = new ObjectsClass(Progress, ObjectsGridView);
                objects.Gridasikas.LoadFromFile(_loadFile.FileName);
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
                design.Gridasikas.GridPutData();

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
            if (design.Gridasikas.GridGetData())
            {
                DataClass data = new DataClass(Progress, DataGridView);
                data.ExtractFromDesign(design);
                data.Gridasikas.GridPutData();
            }

            ButtonFunctionFinished(sender);
        }

        private void KKSCombineMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPressed(sender);

            DataClass data = new DataClass(Progress, DataGridView);
            if (data.Gridasikas.GridGetData())
            {
                data.MakeKKS();
                data.Gridasikas.GridPutData();
            }

            ButtonFunctionFinished(sender);
        }
    }
}
