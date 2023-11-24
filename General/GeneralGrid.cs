using ExcelDataReader;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
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

        private DataGridView Grid { get; set; }

        public bool UseKeywordAsName = false;

        public GeneralGrid(string name, GridTypes gridType, DataGridView grid, ColumnList columns)
        {
            Name = name;
            Grid = grid;
            Columns = columns;
            GridType = gridType;
        }

        /// <summary>
        /// Change to new grid
        /// </summary>
        /// <param name="grid">new grid</param>
        public void ChangeGrid(DataGridView grid)
        {
            Grid = grid;
        }

        /// <summary>
        /// Checks if rows are empty, 1 line is also empty
        /// </summary>
        /// <returns>grid is empty</returns>
        public bool IsEmpty()
        {
            if (Grid == null)
                return true;

            return Grid.RowCount <= 1;
        }

        /// <summary>
        /// Clear all grid data
        /// </summary>
        public void GridClear()
        {
            Grid.DataSource = null;
            Grid.Rows.Clear();
            Grid.Columns.Clear();

            Grid.DataSource = null;
            Debug debug = new Debug();
            debug.ToFile("Clearing all grid: " + Name, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Get grid columns and add to columns list
        /// </summary>
        /// <returns>grid column list</returns>
        public List<GeneralColumn> GetColumns()
        {
            int _columnNumber;
            int _columnIndex;
            string _keyword;
            bool _canHide;

            Debug debug = new Debug();
            debug.ToFile("Getting columns from grid to memory(" + Grid.ColumnCount + ") to grid: " + Name, DebugLevels.Development, DebugMessageType.Info);

            List<GeneralColumn> _columnList = new List<GeneralColumn>();
            for (_columnNumber = 0; _columnNumber < Grid.ColumnCount; _columnNumber++)
            {
                _keyword = Grid.Columns[_columnNumber].Name;
                _columnIndex = Grid.Columns[_columnNumber].DisplayIndex;
                _canHide = true;
                _columnList.Add(new GeneralColumn(_keyword, _columnIndex, _canHide));
            }
            return _columnList;
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
            Grid.Visible = false;
            Grid.SuspendLayout();
            GridClear();

            bool _supressColumnError = false;

            switch (GridType)
            {
                case GridTypes.Data:
                case GridTypes.DBForceEdit:
                    Grid.AllowUserToAddRows = true;
                    Grid.AllowUserToDeleteRows = true;
                    Grid.ReadOnly = false;
                    break;

                case GridTypes.DB:
                case GridTypes.DBEditable:
                case GridTypes.DataNoEdit:
                    Grid.AllowUserToAddRows = false;
                    Grid.AllowUserToDeleteRows = false;
                    Grid.ReadOnly = true;
                    break;

                default:
                    const string _debugText = "GeneralGrid.PutData";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + nameof(GridType), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + nameof(GridType) + " is not created for this element");
            }

            if (GridType == GridTypes.DB || GridType == GridTypes.DBEditable || GridType == GridTypes.DBForceEdit || GridType == GridTypes.DataNoEdit)
                _supressColumnError = true;

            debug.ToFile(Resources.PutDataToGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            Grid.DataSource = data;
            for (int _columnNumber = 0; _columnNumber < data.Columns.Count; _columnNumber++)
            {
                if (Columns == null || Columns.Columns.Count == 0)
                {
                    data.Columns[_columnNumber].ColumnName = _columnNumber.ToString();
                    Grid.Columns[_columnNumber].Name = _columnNumber.ToString();
                }
                else
                {
                    data.Columns[_columnNumber].ColumnName = Columns.Columns[_columnNumber].GetColumnName(_supressColumnError);
                    Grid.Columns[_columnNumber].Name = Columns.Columns[_columnNumber].Keyword;
                }
            }
            Grid.ResumeLayout(false);
            Grid.Refresh();
            Grid.Visible = true;
            Grid.AutoResizeColumns();
        }

        /// <summary>
        /// Get all data from grid
        /// </summary>
        /// <param name="suppressError">suppress error</param>
        /// <returns>grid data as list of list string</returns>
        public DataTable GetData(bool suppressError)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.GetDataFromGrid + ": " + Name, DebugLevels.Development, DebugMessageType.Info);

            return Grid.DataSource as DataTable;
        }

        /// <summary>
        /// Remove columns from grid that are not in base column list
        /// </summary>
        /// <param name="baseColumns">base column list</param>
        public void RemoveNotBaseColumns(List<GeneralColumn> baseColumns)
        {
            bool _found;
            if (GridType == GridTypes.DataNoEdit)
                return;

            for (int _gridColumn = Grid.ColumnCount - 1; _gridColumn >= 0; _gridColumn--)
            {
                _found = false;
                foreach (GeneralColumn _generalColumn in baseColumns)
                {
                    if (Grid.Columns[_gridColumn].Name != _generalColumn.Keyword)
                        continue;

                    _found = true;
                    break;
                }

                if (_found)
                    continue;

                Grid.Columns.RemoveAt(_gridColumn);
            }
        }

        /// <summary>
        /// Paint cell background
        /// </summary>
        /// <param name="row">row of cell</param>
        /// <param name="column">column of cell</param>
        public void ColorCell(int row, int column)
        {
            Grid.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(0, 255, 255);
        }
    }
}