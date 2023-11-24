using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace IO_list_automation_new.DB
{
    public partial class DBResultForm : Form
    {
        private string Directory { get; set; }
        private string FileExtension { get; set; }
        private bool Editable { get; set; }

        private BaseTypes Base { get; }

        private void ResultForm_Shown(object sender, EventArgs e)
        {
            UpdateResult();
            if (Editable)
                return;

            //hide empty grids

            for (int _cpuIndex = DBTabControlCPU.TabCount-1; _cpuIndex >= 0; _cpuIndex--)
            {
                bool _found = false;

                TabControl _control = (TabControl)DBTabControlCPU.TabPages[_cpuIndex].Controls[0];
                for (int _objectIndex = _control.TabCount-1; _objectIndex >= 0; _objectIndex--)
                {
                    TabPage _page = _control.TabPages[_objectIndex];
                    DataGridView _grid = (DataGridView)_page.Controls[0];

                    if (_grid.Rows.Count == 0)
                        _control.TabPages.RemoveAt(_objectIndex);
                    else
                        _found = true;
                }

                if (!_found)
                    DBTabControlCPU.TabPages.RemoveAt(_cpuIndex);
            }
        }

        private void DeleteComboBox()
        {
            foreach (var _item in this.Controls)
            {
                if (!_item.GetType().Name.Contains("ComboBox"))
                    continue;

                if (((System.Windows.Forms.ComboBox)_item).Name == "RowEditComboBox")
                {
                    DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)_item)
                    {
                        IndexChangedEventRemove = RowEditComboBox_SelectedIndexChanged,
                    };
                    this.Controls.Remove((System.Windows.Forms.Control)_item);
                }
                else if (((System.Windows.Forms.ComboBox)_item).Name == "PageEditComboBox")
                {
                    DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)_item)
                    {
                        IndexChangedEventRemove = PageEditComboBox_SelectedIndexChanged,
                    };
                    this.Controls.Remove((System.Windows.Forms.Control)_item);
                }
            }
        }

        private void DBTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteComboBox();
            UpdateResult();
        }

        private void DBTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            DeleteComboBox();

            if (!Editable)
                return;

            if (e.Button != MouseButtons.Right)
                return;

            DropDownClass _dropDown = new DropDownClass("PageEditComboBox");

            _dropDown.Editable(false);
            _dropDown.Location = PointToClient(Cursor.Position);

            _dropDown.AddItemCustom(string.Empty, string.Empty);
            _dropDown.AddItemCustom(Resources.Add, string.Empty);
            _dropDown.AddItemCustom(Resources.Copy, string.Empty);

            if (DBTabControlCPU.TabPages.Count > 1)
            {
                for (int i = 0; i < DBTabControlCPU.TabPages.Count; i++)
                    _dropDown.AddItemCustom(Resources.Remove, DBTabControlCPU.TabPages[i].Text);
            }

            this.Controls.Add(_dropDown.Element);

            _dropDown.IndexChangedEvent = PageEditComboBox_SelectedIndexChanged;
            _dropDown.BringToFront();
        }

        private void PageEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownClass dropDown = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            string _text = dropDown.SelectedName();
            string _mod = dropDown.SelectedMod();
            string _fileName;

            DeleteComboBox();
            if (_mod == Resources.Add)
            {
                NewName _newName = new NewName(Resources.CreateNew + " " + this.Text, string.Empty, true);

                _newName.ShowDialog();
                _fileName = _newName.Output;
                if (string.IsNullOrEmpty(_fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < DBTabControlCPU.TabPages.Count; i++)
                {
                    if (DBTabControlCPU.TabPages[i].Text == _fileName)
                    {
                        MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataGridView _grid = AddData("", _newName.Output, _newName.Output);
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("0");
                dataTable.Columns.Add("1");
                dataTable.Rows.Add("","");
                _grid.DataSource = dataTable;

                DBTabControlCPU.SelectedIndex = DBTabControlCPU.TabCount - 1;
            }
            else if (_mod == Resources.Remove)
            {
                for (int i = 0; i < DBTabControlCPU.TabPages.Count; i++)
                {
                    if (DBTabControlCPU.TabPages[i].Text != _text)
                        continue;

                    DBTabControlCPU.TabPages.RemoveAt(i);
                    string _filePath = Directory + "\\" + _text + "." + FileExtension;
                    File.Delete(_filePath);
                }
            }
            else if (_mod == Resources.Copy)
            {
                NewName _newName = new NewName(Resources.CreateNew + " " + this.Text, DBTabControlCPU.SelectedTab.Name, true);

                _newName.ShowDialog();
                _fileName = _newName.Output;
                if (string.IsNullOrEmpty(_fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < DBTabControlCPU.TabPages.Count; i++)
                {
                    if (DBTabControlCPU.TabPages[i].Text == _fileName)
                    {
                        MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataGridView _gridCopyFrom = (DataGridView)DBTabControlCPU.SelectedTab.Controls[0];
                DataGridView _grid = AddData("", _newName.Output, _newName.Output);

                for (int i = 0; i < _gridCopyFrom.ColumnCount; i++)
                    _grid.Columns.Add(i.ToString(), i.ToString());

                for (int i = 0; i < _gridCopyFrom.RowCount; i++)
                    _grid.Rows.Add(_gridCopyFrom.Rows[i].Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());

                DBTabControlCPU.SelectedIndex = DBTabControlCPU.TabCount - 1;
            }
        }

        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DeleteComboBox();

            if (e.Button != MouseButtons.Right)
                return;

            DropDownClass comboBox = new DropDownClass("RowEditComboBox");

            comboBox.Editable(false);
            comboBox.Location = PointToClient(Cursor.Position);
            comboBox.ChangeDisplayMember(DropDownElementType.Mod);

            string _index = e.RowIndex.ToString();
            comboBox.AddItemCustom(string.Empty, _index);
            comboBox.AddItemCustom(Resources.Add, _index);
            comboBox.AddItemCustom(Resources.AddBefore, _index);
            comboBox.AddItemCustom(Resources.Copy, _index);
            comboBox.AddItemCustom(Resources.CopyEnd, _index);
            comboBox.AddItemCustom(Resources.Remove, _index);

            this.Controls.Add(comboBox.Element);
            comboBox.IndexChangedEvent = RowEditComboBox_SelectedIndexChanged;
            comboBox.BringToFront();
        }

        private void DataGridView_CellClick(object sender, EventArgs e)
        {
            DeleteComboBox();

            int _row = ((System.Windows.Forms.DataGridViewCellEventArgs)e).RowIndex;
            List<string> _line = new List<string>();

            DataTable _dataTable = ((DataGridView)sender).DataSource as DataTable;

            //put selected line to edit
            for (int column = 0; column < _dataTable.Columns.Count; column++)
                _line.Add(GeneralFunctions.GetDataTableValue(_dataTable, _row, column));

            DBCellEdit _DBCellEdit = new DBCellEdit(_line, Base);
            _DBCellEdit.ShowDialog();
            _line = _DBCellEdit.OutputData;

            if (_line.Count > _dataTable.Columns.Count)
            {
                DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();

                //add result columns if needed
                for (int i = _dataTable.Columns.Count; i < _line.Count; i++)
                {
                    DataGridViewColumn _columnGridView = new DataGridViewColumn()
                    {
                        CellTemplate = _cell,
                        SortMode = DataGridViewColumnSortMode.NotSortable,
                        Name = i.ToString(),
                        HeaderText = i.ToString(),
                    };
                    _dataTable.Columns.Add(i.ToString());
                }
            }

            for (int i = 0; i < _line.Count; i++)
                _dataTable.Rows[_row][i] = _line[i];

            //clear other cells
            for (int i = _line.Count; i < _dataTable.Columns.Count; i++)
                _dataTable.Rows[_row][i] = string.Empty;

            UpdateResult();
        }

        private void RowEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl _objectControl = (TabControl)DBTabControlCPU.TabPages[0].Controls[0];
            DataGridView _grid = (DataGridView)_objectControl.TabPages[_objectControl.SelectedIndex].Controls[0];

            DropDownClass _dropDown = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            string _mod = _dropDown.SelectedMod();
            int _selectedRow = int.Parse(_dropDown.SelectedKeyword());

            DeleteComboBox();

            DataTable _data = _grid.DataSource as DataTable;
            //add after selected row
            if (_mod == Resources.Add)
            {
                _data.Rows.InsertAt(_data.NewRow(),_selectedRow+1);
            }
            else if (_mod == Resources.AddBefore)
            {
                _data.Rows.InsertAt(_data.NewRow(), _selectedRow);
            }
            else if (_mod == Resources.Copy)
            {
                _data.Rows.InsertAt(_data.NewRow(), _selectedRow + 1);

                for (int i = 0; i < _data.Columns.Count; i++)
                    _data.Rows[_selectedRow + 1][i] = _data.Rows[_selectedRow][i];
            }
            else if (_mod == Resources.CopyEnd)
            {
                _data.Rows.Add(_data.NewRow(), _selectedRow + 1);

                for (int i = 0; i < _data.Columns.Count; i++)
                    _data.Rows[_data.Rows.Count - 1][i] = _data.Rows[_selectedRow][i];
            }
            else if (_mod == Resources.Remove)
            {
                _data.Rows.RemoveAt(_selectedRow);
            }
            UpdateResult();
        }

        public DataGridView AddData(string nameCPU, string fullName, string tabName)
        {
            DataGridView dataGridViewObject = new DataGridView();
            TabPage tabPageObject = new TabPage();
            TabControl _objectTabControl;

            int _foundIndex = -1;
            for (int i = 0; i < DBTabControlCPU.TabCount; i++)
            {
                if (DBTabControlCPU.TabPages[i].Name == nameCPU)
                {
                    _foundIndex = i;
                    break;
                }
            }

            if (_foundIndex == -1)
            {
                //new CPU tab page
                TabPage tabPageCPU = new TabPage();
                tabPageCPU.Name = nameCPU;
                tabPageCPU.Text = nameCPU;

                // new object tab page control
                _objectTabControl = new TabControl();
                _objectTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
                if (Editable)
                    _objectTabControl.SelectedIndexChanged += DBTabControl_SelectedIndexChanged;

                DBTabControlCPU.TabPages.Add(tabPageCPU);
                DBTabControlCPU.TabPages[DBTabControlCPU.TabCount-1].Controls.Add(_objectTabControl);
            }
            else
            {
                _objectTabControl = (TabControl)DBTabControlCPU.TabPages[_foundIndex].Controls[0];
            }

            dataGridViewObject.ColumnHeadersVisible = false;
            dataGridViewObject.Location = new Point(0, 0);
            dataGridViewObject.Name = "dataGridView - " + tabName;
            dataGridViewObject.Size = new Size(178, 153);
            dataGridViewObject.ScrollBars = ScrollBars.Both;
            dataGridViewObject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewObject.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewObject.AllowUserToAddRows = false;
            dataGridViewObject.AllowUserToDeleteRows = false;
            dataGridViewObject.AllowUserToResizeColumns = false;
            dataGridViewObject.AllowUserToResizeRows = false;
            dataGridViewObject.ReadOnly = true;

            if (Editable)
            {
                dataGridViewObject.CellClick += DataGridView_CellClick;
                dataGridViewObject.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            }
            //
            // tabPage1
            //
            tabPageObject.Controls.Add(dataGridViewObject);
            tabPageObject.Location = new Point(4, 22);
            tabPageObject.Name = tabName;
            tabPageObject.Padding = new Padding(3);
            tabPageObject.Size = new Size(178, 153);
            tabPageObject.Text = fullName;
            tabPageObject.UseVisualStyleBackColor = true;
            tabPageObject.Dock = System.Windows.Forms.DockStyle.Fill;
            //
            // ResultForm
            //
            //            Controls.Add(tabControl1);
            //            ((ISupportInitialize)(dataGridView1)).EndInit();

            _objectTabControl.Controls.Add(tabPageObject);

            dataGridViewObject.AutoResizeColumns();

            return dataGridViewObject;
        }

        public DBResultForm(string name, bool editable, BaseTypes inputBase, string directory, string fileExtension)
        {
            InitializeComponent();
            this.Text = name;
            Editable = editable;
            Base = inputBase;
            Directory = directory;
            FileExtension = fileExtension;

            if (!editable)
                tableLayoutPanel1.ColumnStyles[1].Width = 0;
        }

        private void UpdateResult()
        {
            if (!Editable)
                return;

            DBGeneralSignal _instance = new DBGeneralSignal(true);

            //get current grid
            TabControl _control = (TabControl)DBTabControlCPU.TabPages[0].Controls[0];
            TabPage _page = _control.SelectedTab;
            DataGridView _grid = (DataGridView)_page.Controls[0];

            GeneralGrid resultGrid = new GeneralGrid(Name, GridTypes.DB, ResultDataGridView, null);
            DataTable dataTable = new DataTable();

            //go through all rows
            for (int i = 0; i < _grid.RowCount; i++)
            {
                //read grid data of configuration
                List<string> _line = new List<string>();
                for (int j = 0; j < _grid.ColumnCount; j++)
                {
                    if (_grid.Rows[i].Cells[j].Value == null)
                        _line.Add(string.Empty);
                    else
                        _line.Add(_grid.Rows[i].Cells[j].Value.ToString());
                }

                //decode current line
                _instance.SetValue(_line);
                _instance.DecodeLine(dataTable,0, null, null, null, null, Base);
            }
            resultGrid.PutData(dataTable);
        }
    }
}