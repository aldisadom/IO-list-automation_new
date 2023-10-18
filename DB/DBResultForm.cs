using IO_list_automation_new.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IO_list_automation_new.DB
{
    public partial class DBResultForm : Form
    {
        private bool Editable {  get; set; }

        public DataGridView AddData( string tabName)
        {

            DataGridView dataGridView1 = new DataGridView();
            TabPage tabPage1 = new TabPage();

//            ((ISupportInitialize)(dataGridView1)).BeginInit();
            
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(178, 153);
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = tabName;
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(178, 153);
            tabPage1.Text = tabName;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // ResultForm
            // 
            Controls.Add(tabControl1);
//            ((ISupportInitialize)(dataGridView1)).EndInit();

            tabControl1.Controls.Add(tabPage1);


            dataGridView1.AutoResizeColumns();

            return dataGridView1;
        }

        public DBResultForm(string name, bool editable)
        {
            InitializeComponent();
            this.Text = name;
            Editable = editable;

            if (!editable)
            {
                tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;

                tabControl2.Visible = false;
            }
        }

        private void UpdateResult()
        {
            if (Editable)
            {
                DBGeneralInstanceSignal _instance = new DBGeneralInstanceSignal();

                //clear result grid
                ResultDataGridView.Rows.Clear();
                ResultDataGridView.Columns.Clear();
                DataGridViewTextBoxCell _cell = new DataGridViewTextBoxCell();

                //get current grid
                int _index = tabControl1.SelectedIndex;
                TabPage _page = tabControl1.TabPages[_index];
                DataGridView _grid = (DataGridView)_page.Controls[0];

                //go through all rows
                for (int i = 0; i < _grid.RowCount; i++)
                {
                    //read grid data of configuration
                    List<string> _line = new List<string>();
                    for (int j = 0; j < _grid.ColumnCount; j++)
                        _line.Add(_grid.Rows[i].Cells[j].Value.ToString());

                    //decode current line
                    _instance.SetValue(_line);
                    List<string> _decodedline = _instance.DecodeLine(null, null, true);

                    string[] _row = new string[_decodedline.Count];
                    for (int j = 0; j < _decodedline.Count; j++)
                        _row[j] = _decodedline[j];

                    //add result columns if needed
                    if (ResultDataGridView.ColumnCount < _decodedline.Count)
                    {
                        for (int j = ResultDataGridView.ColumnCount; j < _decodedline.Count; j++)
                        {
                            DataGridViewColumn _columnGridView = new DataGridViewColumn();
                            _columnGridView.CellTemplate = _cell;
                            _columnGridView.SortMode = DataGridViewColumnSortMode.NotSortable;
                            _columnGridView.Name = j.ToString();
                            _columnGridView.HeaderText = j.ToString();

                            ResultDataGridView.Columns.Insert(j, _columnGridView);
                        }
                    }

                    if (i==11)
                    {
                        DBCellEdit _form = new DBCellEdit(_line);
                        _form.ShowDialog();
                    }
                    //put result to grid
                    ResultDataGridView.Rows.Add();
                    ResultDataGridView.Rows[i].SetValues(_row);
                }
                ResultDataGridView.AutoResizeColumns();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateResult();
        }

        private void ResultForm_Shown(object sender, EventArgs e)
        {
            UpdateResult();
        }

        private void RecalculateButton_Click(object sender, EventArgs e)
        {
            UpdateResult();
        }
    }
}
