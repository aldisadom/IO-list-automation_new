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

            for (int cpuIndex = DBTabControlCPU.TabCount - 1; cpuIndex >= 0; cpuIndex--)
            {
                bool found = false;

                TabControl control = (TabControl)DBTabControlCPU.TabPages[cpuIndex].Controls[0];
                for (int objectIndex = control.TabCount - 1; objectIndex >= 0; objectIndex--)
                {
                    TabPage page = control.TabPages[objectIndex];
                    DataGridView grid = (DataGridView)page.Controls[0];

                    if (grid.Rows.Count == 0)
                        control.TabPages.RemoveAt(objectIndex);
                    else
                        found = true;
                }

                if (!found)
                    DBTabControlCPU.TabPages.RemoveAt(cpuIndex);
            }
        }

        private void DeleteComboBox()
        {
            foreach (var item in this.Controls)
            {
                if (!item.GetType().Name.Contains("ComboBox"))
                    continue;

                if (((System.Windows.Forms.ComboBox)item).Name == "RowEditComboBox")
                {
                    DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)item)
                    {
                        IndexChangedEventRemove = RowEditComboBoxSelectedIndexChanged,
                    };
                    this.Controls.Remove((System.Windows.Forms.Control)item);
                }
                else if (((System.Windows.Forms.ComboBox)item).Name == "PageEditComboBox")
                {
                    DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)item)
                    {
                        IndexChangedEventRemove = PageEditComboBoxSelectedIndexChanged,
                    };
                    this.Controls.Remove((System.Windows.Forms.Control)item);
                }
            }
        }

        private void DBTabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteComboBox();
            UpdateResult();
        }

        private void DBTabControlMouseClick(object sender, MouseEventArgs e)
        {
            DeleteComboBox();

            if (!Editable)
                return;

            if (e.Button != MouseButtons.Right)
                return;

            DropDownClass dropDown = new DropDownClass("PageEditComboBox");

            dropDown.Editable(false);
            dropDown.Location = PointToClient(Cursor.Position);

            dropDown.AddItemCustom(string.Empty, string.Empty);
            dropDown.AddItemCustom(Resources.Add, string.Empty);
            dropDown.AddItemCustom(Resources.Copy, string.Empty);

            if (DBTabControlCPU.TabPages.Count > 1)
            {
                foreach (TabPage cpuPage in DBTabControlCPU.TabPages)
                    dropDown.AddItemCustom(Resources.Remove, cpuPage.Text);
            }

            this.Controls.Add(dropDown.Element);

            dropDown.IndexChangedEvent = PageEditComboBoxSelectedIndexChanged;
            dropDown.BringToFront();
        }

        private void PageEditComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownClass dropDown = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            string text = dropDown.SelectedName();
            string mod = dropDown.SelectedMod();
            string fileName;

            DeleteComboBox();
            if (mod == Resources.Add)
            {
                NewName newName = new NewName(Resources.CreateNew + " " + this.Text, string.Empty, true);

                newName.ShowDialog();
                fileName = newName.Output;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (TabPage cpuPage in DBTabControlCPU.TabPages)
                {
                    if (cpuPage.Text == fileName)
                    {
                        MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataGridView grid = AddData("", newName.Output, newName.Output);
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("0");
                dataTable.Columns.Add("1");
                dataTable.Rows.Add("", "");
                grid.DataSource = dataTable;

                DBTabControlCPU.SelectedIndex = DBTabControlCPU.TabCount - 1;
            }
            else if (mod == Resources.Remove)
            {
                for (int i = 0; i < DBTabControlCPU.TabPages.Count; i++)
                {
                    if (DBTabControlCPU.TabPages[i].Text != text)
                        continue;

                    DBTabControlCPU.TabPages.RemoveAt(i);
                    string filePath = Directory + "\\" + text + "." + FileExtension;
                    File.Delete(filePath);
                    break;
                }
            }
            else if (mod == Resources.Copy)
            {
                NewName newName = new NewName(Resources.CreateNew + " " + this.Text, DBTabControlCPU.SelectedTab.Name, true);

                newName.ShowDialog();
                fileName = newName.Output;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show(Resources.EnteredEmptyName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (TabPage cpuPage in DBTabControlCPU.TabPages)
                {
                    if (cpuPage.Text == fileName)
                    {
                        MessageBox.Show(Resources.EnteredExistingName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataGridView gridCopyFrom = (DataGridView)DBTabControlCPU.SelectedTab.Controls[0];
                DataGridView grid = AddData("", newName.Output, newName.Output);

                for (int i = 0; i < gridCopyFrom.ColumnCount; i++)
                    grid.Columns.Add(i.ToString(), i.ToString());

                for (int i = 0; i < gridCopyFrom.RowCount; i++)
                    grid.Rows.Add(gridCopyFrom.Rows[i].Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());

                DBTabControlCPU.SelectedIndex = DBTabControlCPU.TabCount - 1;
            }
        }

        private void DataGridViewRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DeleteComboBox();

            if (e.Button != MouseButtons.Right)
                return;

            DropDownClass comboBox = new DropDownClass("RowEditComboBox");

            comboBox.Editable(false);
            comboBox.Location = PointToClient(Cursor.Position);
            comboBox.ChangeDisplayMember(DropDownElementType.Mod);

            string index = e.RowIndex.ToString();
            comboBox.AddItemCustom(string.Empty, index);
            comboBox.AddItemCustom(Resources.Add, index);
            comboBox.AddItemCustom(Resources.AddBefore, index);
            comboBox.AddItemCustom(Resources.Copy, index);
            comboBox.AddItemCustom(Resources.CopyEnd, index);
            comboBox.AddItemCustom(Resources.Remove, index);

            this.Controls.Add(comboBox.Element);
            comboBox.IndexChangedEvent = RowEditComboBoxSelectedIndexChanged;
            comboBox.BringToFront();
        }

        private void DataGridViewCellClick(object sender, EventArgs e)
        {
            DeleteComboBox();

            int row = ((System.Windows.Forms.DataGridViewCellEventArgs)e).RowIndex;
            List<string> line = new List<string>();

            DataTable dataTable = ((DataGridView)sender).DataSource as DataTable;

            //put selected line to edit
            for (int column = 0; column < dataTable.Columns.Count; column++)
                line.Add(GeneralFunctions.GetDataTableValue(dataTable, row, column));

            DBCellEdit dbCellEdit = new DBCellEdit(line, Base);
            dbCellEdit.ShowDialog();
            line = dbCellEdit.OutputData;

            if (line.Count > dataTable.Columns.Count)
            {
                //add result columns if needed
                for (int i = dataTable.Columns.Count; i < line.Count; i++)
                    dataTable.Columns.Add(i.ToString());
            }

            for (int i = 0; i < line.Count; i++)
                dataTable.Rows[row][i] = line[i];

            //clear other cells
            for (int i = line.Count; i < dataTable.Columns.Count; i++)
                dataTable.Rows[row][i] = string.Empty;

            UpdateResult();
        }

        private void RowEditComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl objectControl = (TabControl)DBTabControlCPU.TabPages[0].Controls[0];
            DataGridView grid = (DataGridView)objectControl.TabPages[objectControl.SelectedIndex].Controls[0];

            DropDownClass dropDown = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            string mod = dropDown.SelectedMod();
            int selectedRow = int.Parse(dropDown.SelectedKeyword());

            DeleteComboBox();

            DataTable data = grid.DataSource as DataTable;
            //add after selected row
            if (mod == Resources.Add)
            {
                data.Rows.InsertAt(data.NewRow(), selectedRow + 1);
            }
            else if (mod == Resources.AddBefore)
            {
                data.Rows.InsertAt(data.NewRow(), selectedRow);
            }
            else if (mod == Resources.Copy)
            {
                data.Rows.InsertAt(data.NewRow(), selectedRow + 1);

                for (int i = 0; i < data.Columns.Count; i++)
                    data.Rows[selectedRow + 1][i] = data.Rows[selectedRow][i];
            }
            else if (mod == Resources.CopyEnd)
            {
                data.Rows.Add(data.NewRow(), selectedRow + 1);

                for (int i = 0; i < data.Columns.Count; i++)
                    data.Rows[data.Rows.Count - 1][i] = data.Rows[selectedRow][i];
            }
            else if (mod == Resources.Remove)
            {
                data.Rows.RemoveAt(selectedRow);
            }
            UpdateResult();
        }

        public DataGridView AddData(string nameCPU, string fullName, string tabName)
        {
            DataGridView dataGridViewObject = new DataGridView();
            TabPage tabPageObject = new TabPage();
            TabControl objectTabControl;

            int foundIndex = -1;
            for (int i = 0; i < DBTabControlCPU.TabCount; i++)
            {
                if (DBTabControlCPU.TabPages[i].Name == nameCPU)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex == -1)
            {
                //new CPU tab page
                TabPage tabPageCPU = new TabPage
                {
                    Name = nameCPU,
                    Text = nameCPU
                };

                // new object tab page control
                objectTabControl = new TabControl { Dock = System.Windows.Forms.DockStyle.Fill };

                if (Editable)
                    objectTabControl.SelectedIndexChanged += DBTabControlSelectedIndexChanged;

                DBTabControlCPU.TabPages.Add(tabPageCPU);
                DBTabControlCPU.TabPages[DBTabControlCPU.TabCount - 1].Controls.Add(objectTabControl);
            }
            else
            {
                objectTabControl = (TabControl)DBTabControlCPU.TabPages[foundIndex].Controls[0];
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
                dataGridViewObject.CellClick += DataGridViewCellClick;
                dataGridViewObject.RowHeaderMouseClick += DataGridViewRowHeaderMouseClick;
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

            objectTabControl.Controls.Add(tabPageObject);

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

            DBGeneralSignal instance = new DBGeneralSignal(true);

            //get current grid
            TabControl control = (TabControl)DBTabControlCPU.TabPages[0].Controls[0];
            TabPage _page = control.SelectedTab;
            DataGridView grid = (DataGridView)_page.Controls[0];

            GeneralGrid resultGrid = new GeneralGrid(Name, GridTypes.DB, ResultDataGridView, null);
            DataTable dataTable = new DataTable();

            //go through all rows
            foreach (DataGridViewRow row in grid.Rows)
            {
                //read grid data of configuration
                List<string> line = new List<string>();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null)
                        line.Add(string.Empty);
                    else
                        line.Add(cell.Value.ToString());
                }

                //decode current line
                instance.SetValue(line);
                instance.DecodeLine(dataTable, 0, null, null, null, null, Base);
            }

            resultGrid.PutData(dataTable);
        }
    }
}