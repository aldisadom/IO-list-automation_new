using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IO_list_automation_new.DB
{
    public partial class DBResultForm : Form
    {
        private bool Editable { get; set; }
        private bool ModuleBased { get; set; }

        private void ResultForm_Shown(object sender, EventArgs e)
        {
            UpdateResult();
        }

        private void DBTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageEditComboBox.Visible = false;
            CellEditComboBox.Visible = false;
            UpdateResult();
        }

        private void DBTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            PageEditComboBox.Visible = false;
            CellEditComboBox.Visible = false;

            if (e.Button != MouseButtons.Right)
                return;

            PageEditComboBox.Items.Clear();
            PageEditComboBox.Items.Add(string.Empty);
            PageEditComboBox.Items.Add(Resources.Add);
            PageEditComboBox.Items.Add(Resources.Copy);

            for (int i = 0; i < DBTabControl.TabPages.Count; i++)
                PageEditComboBox.Items.Add(Resources.Remove + ": " + DBTabControl.TabPages[i].Name);

            PageEditComboBox.Visible = true;
            PageEditComboBox.Location = PointToClient(Cursor.Position);
            PageEditComboBox.BringToFront();
        }

        private void PageEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _text = PageEditComboBox.SelectedItem.ToString();
            PageEditComboBox.Visible = false;

            if (_text == Resources.Add)
            {
                NewName _newName = new NewName(Resources.CreateNew + " " + this.Text);
                _newName.ShowDialog();
                if (string.IsNullOrEmpty(_newName.Output))
                    return;

                DataGridView _grid = AddData(_newName.Output, _newName.Output);

                _grid.Columns.Add("0", "0");
                _grid.Rows.Add();
            }
            else if (_text == Resources.Remove)
            {
                DBTabControl.TabPages.RemoveByKey(_text.Replace(Resources.Remove + ": ", string.Empty));
            }
            else if (_text == Resources.Copy)
            {
                NewName _newName = new NewName(Resources.CreateNew + " " + this.Text);
                _newName.ShowDialog();
                if (string.IsNullOrEmpty(_newName.Output))
                    return;

                DataGridView _gridCopyFrom = (DataGridView)DBTabControl.SelectedTab.Controls[0];
                DataGridView _grid = AddData(_newName.Output, _newName.Output);

                for (int i = 0; i < _gridCopyFrom.ColumnCount; i++)
                    _grid.Columns.Add(i.ToString(), i.ToString());

                for (int i = 0; i < _gridCopyFrom.RowCount; i++)
                    _grid.Rows.Add(_gridCopyFrom.Rows[i].Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());
            }
        }

        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PageEditComboBox.Visible = false;
            CellEditComboBox.Visible = false;

            if (e.Button != MouseButtons.Right)
                return;

            CellEditComboBox.Items.Clear();
            CellEditComboBox.Items.Add(string.Empty);
            CellEditComboBox.Items.Add(Resources.Add);
            CellEditComboBox.Items.Add(Resources.AddBefore);
            CellEditComboBox.Items.Add(Resources.Copy);
            CellEditComboBox.Items.Add(Resources.CopyEnd);
            CellEditComboBox.Items.Add(Resources.Remove);

            CellEditComboBox.Tag = e.RowIndex;

            CellEditComboBox.Visible = true;
            CellEditComboBox.Location = PointToClient(Cursor.Position);
            CellEditComboBox.BringToFront();
        }

        private void DataGridView_CellClick(object sender, EventArgs e)
        {
            PageEditComboBox.Visible = false;
            CellEditComboBox.Visible = false;

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

            DBCellEdit _DBCellEdit = new DBCellEdit(_line, ModuleBased);
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

        private void CellEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView _grid = (DataGridView)DBTabControl.SelectedTab.Controls[0];

            //add after selected row
            if (CellEditComboBox.SelectedItem.ToString() == Resources.Add)
            {
                _grid.Rows.Insert((int)CellEditComboBox.Tag + 1, 1);
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[(int)CellEditComboBox.Tag + 1].Cells[i].Value = string.Empty;
            }
            else if (CellEditComboBox.SelectedItem.ToString() == Resources.AddBefore)
            {
                _grid.Rows.Insert((int)CellEditComboBox.Tag, 1);
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[(int)CellEditComboBox.Tag].Cells[i].Value = string.Empty;
            }
            else if (CellEditComboBox.SelectedItem.ToString() == Resources.Copy)
            {
                _grid.Rows.Insert((int)CellEditComboBox.Tag + 1, 1);
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[(int)CellEditComboBox.Tag + 1].Cells[i].Value = _grid.Rows[(int)CellEditComboBox.Tag].Cells[i].Value;
            }
            else if (CellEditComboBox.SelectedItem.ToString() == Resources.CopyEnd)
            {
                _grid.Rows.Add();
                for (int i = 0; i < _grid.ColumnCount; i++)
                    _grid.Rows[_grid.RowCount-1].Cells[i].Value = _grid.Rows[(int)CellEditComboBox.Tag].Cells[i].Value;
            }
            else if (CellEditComboBox.SelectedItem.ToString() == Resources.Remove)
            {
                _grid.Rows.RemoveAt((int)CellEditComboBox.Tag);
            }

            DBTabControl.SelectedTab.Controls.Remove(this.CellEditComboBox);
            CellEditComboBox.Visible = false;
            UpdateResult();
        }

        public DataGridView AddData(string fullName,string tabName)
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

        public DBResultForm(string name, bool editable, bool moduleBased)
        {
            InitializeComponent();
            this.Text = name;
            Editable = editable;
            ModuleBased = moduleBased;

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
                List<string> _decodedLine = _instance.DecodeLine(0, null, null, null);

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