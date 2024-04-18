using IO_list_automation_new.General;
using IO_list_automation_new.Helper_functions;
using IO_list_automation_new.Properties;
using System;
using System.Data;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal enum GridTypes
    {
        Data,
        DataNoEdit,
        DBEditable,
        DBForceEdit,
        DB,
    }

    internal class GeneralGrid
    {
        private string Name { get; }
        public GridTypes GridType { get; }

        //columns in software
        private ColumnList Columns { get; set; }

        private DataGridView GridView { get; set; }

        public bool UseKeywordAsName = false;

        public GeneralGrid(string name, GridTypes gridType, DataGridView gridView, ColumnList columns)
        {
            Name = name;
            GridView = gridView;
            Columns = columns ?? new ColumnList(name);
            GridType = gridType;

            UpdateColumnsList();
        }

        /// <summary>
        /// Change to new grid
        /// </summary>
        /// <param name="grid">new grid</param>
        public void ChangeGrid(DataGridView grid)
        {
            GridView = grid;
        }

        /// <summary>
        /// Checks if rows are empty, 1 line is also empty
        /// </summary>
        /// <returns>grid is empty</returns>
        public bool IsEmpty()
        {
            if (GridView == null)
                return true;

            return GridView.RowCount <= 1;
        }

        /// <summary>
        /// Clear all grid data
        /// </summary>
        public void GridClear()
        {
            GridView.DataSource = null;
            GridView.Rows.Clear();
            GridView.Columns.Clear();

            GridView.DataSource = null;
            Debug debug = new Debug();
            debug.ToFile("Clearing all grid: " + Name, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get grid columns and add to columns list
        /// </summary>
        /// <returns>grid column list</returns>
        public void UpdateColumnsList()
        {
            if (GridView.Columns.Count <= 0)
                return;

            Debug debug = new Debug();
            debug.ToFile("Getting columns from grid to memory(" + GridView.ColumnCount + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            foreach (var column in Columns.Columns)
                column.Value.Hidden = column.Value.CanHide;

            foreach (DataGridViewColumn column in GridView.Columns)
            {
                string columnKey = column.Name;
                if (Columns.Columns.TryGetValue(columnKey, out ColumnParameters columnParameters))
                {
                    columnParameters.Hidden = false;
                    columnParameters.NR = column.DisplayIndex;
                }
                else
                    debug.ToFile($"{Resources.DeleteMe}: {Name} {columnKey} is incorrect", DebugLevels.Minimum, DebugMessageType.Warning);
            }
        }

        /// <summary>
        /// Put data to grid
        /// </summary>
        /// <param name="data">data to put to grid</param>
        public void PutData(DataTable data)
        {
            Debug debug = new Debug();
            if (data == null)
            {
                debug.ToPopUp(Resources.PutDataToGrid + ": " + Name + " - " + Resources.NoData, DebugLevels.Development, DebugMessageType.Info);
                return;
            }

            //hide grid to speed up
            GridView.Visible = false;
            GridView.SuspendLayout();
            GridClear();

            bool suppressColumnError = false;

            switch (GridType)
            {
                case GridTypes.Data:
                case GridTypes.DBForceEdit:
                    GridView.AllowUserToAddRows = true;
                    GridView.AllowUserToDeleteRows = true;
                    GridView.ReadOnly = false;
                    break;

                case GridTypes.DB:
                case GridTypes.DBEditable:
                case GridTypes.DataNoEdit:
                    GridView.AllowUserToAddRows = false;
                    GridView.AllowUserToDeleteRows = false;
                    GridView.ReadOnly = true;
                    break;

                default:
                    const string debugText = "GeneralGrid.PutData";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(GridType), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(GridType) + " is not created for this element");
            }

            if (GridType == GridTypes.DB || GridType == GridTypes.DBEditable || GridType == GridTypes.DBForceEdit || GridType == GridTypes.DataNoEdit)
                suppressColumnError = true;

            debug.ToFile(Resources.PutDataToGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);
            // kazkas negerai su vardu priskyrimu stulpeliams, visus hide o 
            GridView.DataSource = data;

            for (int columnNumber = 0; columnNumber < data.Columns.Count; columnNumber++)
            {
                GridView.Columns[columnNumber].HeaderText = TextHelper.GetColumnName(data.Columns[columnNumber].ColumnName, suppressColumnError);
                GridView.Columns[columnNumber].Name = data.Columns[columnNumber].ColumnName;
            }
            GridView.ResumeLayout(false);
            GridView.Refresh();
            GridView.Visible = true;
            GridView.AutoResizeColumns();
        }

        /// <summary>
        /// Get all data from grid
        /// </summary>
        /// <returns>grid data as list of list string</returns>
        public DataTable GetData()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.GetDataFromGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            return GridView.DataSource as DataTable;
        }

        /// <summary>
        /// Paint cell background
        /// </summary>
        /// <param name="row">row of cell</param>
        /// <param name="column">column of cell</param>
        public void ColorCell(int row, int column)
        {
            GridView.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(0, 255, 255);
        }
    }
}