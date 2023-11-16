using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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

            if (e.Button != MouseButtons.Right)
                return;

            DropDownClass _dropDown = new DropDownClass("PageEditComboBox");

            _dropDown.Editable(false);
            _dropDown.Location = PointToClient(Cursor.Position);

            _dropDown.AddItemCustom(string.Empty, string.Empty);
            _dropDown.AddItemCustom(Resources.Add, string.Empty);
            _dropDown.AddItemCustom(Resources.Copy, string.Empty);

            if (DBTabControl.TabPages.Count > 1)
            {
                for (int i = 0; i < DBTabControl.TabPages.Count; i++)
                    _dropDown.AddItemCustom(Resources.Remove, DBTabControl.TabPages[i].Text);
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
                for (int i = 0; i < DBTabControl.TabPages.Count; i++)
                {
                    if (DBTabControl.TabPages[i].Text == _fileName)
                    {
                        MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataGridView _grid = AddData(_newName.Output, _newName.Output);

                _grid.Columns.Add("0", "0");
                _grid.Rows.Add();

                DBTabControl.SelectedIndex = DBTabControl.TabCount - 1;
            }
            else if (_mod == Resources.Remove)
            {
                for (int i = 0; i < DBTabControl.TabPages.Count; i++)
                {
                    if (DBTabControl.TabPages[i].Text != _text)
                        continue;

                    DBTabControl.TabPages.RemoveAt(i);
                    string _filePath = Directory + "\\" + _text + "." + FileExtension;
                    File.Delete(_filePath);
                }
            }
            else if (_mod == Resources.Copy)
            {
                NewName _newName = new NewName(Resources.CreateNew + " " + this.Text, DBTabControl.SelectedTab.Name, true);

                _newName.ShowDialog();
                _fileName = _newName.Output;
                if (string.IsNullOrEmpty(_fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < DBTabControl.TabPages.Count; i++)
                {
                    if (DBTabControl.TabPages[i].Text == _fileName)
                    {
                        MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataGridView _gridCopyFrom = (DataGridView)DBTabControl.SelectedTab.Controls[0];
                DataGridView _grid = AddData(_newName.Output, _newName.Output);

                for (int i = 0; i < _gridCopyFrom.ColumnCount; i++)
                    _grid.Columns.Add(i.ToString(), i.ToString());

                for (int i = 0; i < _gridCopyFrom.RowCount; i++)
                    _grid.Rows.Add(_gridCopyFrom.Rows[i].Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());

                DBTabControl.SelectedIndex = DBTabControl.TabCount - 1;
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
            DataGridView _grid = (DataGridView)sender;
            List<string> _line = new List<string>();

            for (int i = 0; i < _grid.ColumnCount; i++)
            {
                if (_grid.Rows[_row].Cells[i].Value == null)
                    _line.Add(string.Empty);
                else
                    _line.Add(_grid.Rows[_row].Cells[i].Value.ToString());
            }

            DBCellEdit _DBCellEdit = new DBCellEdit(_line, Base);
            _DBCellEdit.ShowDialog();
            _line = _DBCellEdit.OutputData;

            if (_line.Count > _grid.ColumnCount)
            {
                DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();

                //add result columns if needed
                if (_grid.ColumnCount < _line.Count)
                {
                    for (int i = _grid.ColumnCount; i < _line.Count; i++)
                    {
                        DataGridViewColumn _columnGridView = new DataGridViewColumn()
                        {
                            CellTemplate = _cell,
                            SortMode = DataGridViewColumnSortMode.NotSortable,
                            Name = i.ToString(),
                            HeaderText = i.ToString(),
                        };
                        _grid.Columns.Insert(i, _columnGridView);
                    }
                }
            }

            for (int i = 0; i < _line.Count; i++)
                _grid.Rows[_row].Cells[i].Value = _line[i];

            //clear other cells
            for (int i = _line.Count; i < _grid.ColumnCount; i++)
                _grid.Rows[_row].Cells[i].Value = string.Empty;

            UpdateResult();
        }

        private void RowEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView _grid = (DataGridView)DBTabControl.SelectedTab.Controls[0];

            DropDownClass _dropDown = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            string _mod = _dropDown.SelectedMod();
            int _selectedRow = int.Parse(_dropDown.SelectedKeyword());

            DeleteComboBox();

            //add after selected row
            if (_mod == Resources.Add)
            {
                _grid.Rows.Insert(_selectedRow + 1, 1);
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[_selectedRow + 1].Cells[i].Value = string.Empty;
            }
            else if (_mod == Resources.AddBefore)
            {
                _grid.Rows.Insert(_selectedRow, 1);
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[_selectedRow].Cells[i].Value = string.Empty;
            }
            else if (_mod == Resources.Copy)
            {
                _grid.Rows.Insert(_selectedRow + 1, 1);
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[_selectedRow + 1].Cells[i].Value = _grid.Rows[_selectedRow].Cells[i].Value;
            }
            else if (_mod == Resources.CopyEnd)
            {
                _grid.Rows.Add();
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[_grid.RowCount - 1].Cells[i].Value = _grid.Rows[_selectedRow].Cells[i].Value;
            }
            else if (_mod == Resources.Remove)
            {
                _grid.Rows.RemoveAt(_selectedRow);
            }
            UpdateResult();
        }

        public DataGridView AddData(string fullName, string tabName)
        {
            DataGridView dataGridView1 = new DataGridView();
            TabPage tabPage1 = new TabPage();

            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView - " + tabName;
            dataGridView1.Size = new Size(178, 153);
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ReadOnly = true;

            if (Editable)
            {
                dataGridView1.CellClick += DataGridView_CellClick;
                dataGridView1.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            }
            //
            // tabPage1
            //
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = tabName;
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(178, 153);
            tabPage1.Text = fullName;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            //
            // ResultForm
            //
            //            Controls.Add(tabControl1);
            //            ((ISupportInitialize)(dataGridView1)).EndInit();

            DBTabControl.Controls.Add(tabPage1);

            dataGridView1.AutoResizeColumns();

            return dataGridView1;
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

            //clear result grid
            ResultDataGridView.Rows.Clear();
            ResultDataGridView.Columns.Clear();
            DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();

            //get current grid
            int _index = DBTabControl.SelectedIndex;
            TabPage _page = DBTabControl.TabPages[_index];
            DataGridView _grid = (DataGridView)_page.Controls[0];

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
                List<string> _decodedLine = _instance.DecodeLine(0, null, null, null, null, Base);

                string[] _row = new string[_decodedLine.Count];
                for (int j = 0; j < _decodedLine.Count; j++)
                    _row[j] = _decodedLine[j];

                //add result columns if needed
                if (ResultDataGridView.ColumnCount < _decodedLine.Count)
                {
                    for (int j = ResultDataGridView.ColumnCount; j < _decodedLine.Count; j++)
                    {
                        DataGridViewColumn _columnGridView = new DataGridViewColumn()
                        {
                            CellTemplate = _cell,
                            SortMode = DataGridViewColumnSortMode.NotSortable,
                            Name = j.ToString(),
                            HeaderText = j.ToString(),
                        };
                        ResultDataGridView.Columns.Insert(j, _columnGridView);
                    }
                }

                //put result to grid
                ResultDataGridView.Rows.Add();
                ResultDataGridView.Rows[i].SetValues(_row);
            }
            ResultDataGridView.AutoResizeColumns();
        }
    }
}