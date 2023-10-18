using ExcelDataReader;
using IO_list_automation_new.Properties;
using SwiftExcel;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal abstract class GeneralSignal
    {
        public GeneralSignal()
        {

        }

        /// <summary>
        /// Parsing data from excel or grid, according to Column to signal element
        /// </summary>
        /// <param name="value">value to be passed</param>
        /// <param name="parameterName">parameter to be set</param>
        public abstract void SetValueFromString(string value, string parameterName);

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns>value of parameter</returns>
        public abstract string GetValueString(string parameterName, bool supressError);

        /// <summary>
        /// Checks if signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public abstract bool ValidateSignal();
    }

    internal abstract class GeneralClass <T>
    where T : GeneralSignal, new()
    {
        public string Name { get; }

        public List<T> Signals;
        //columns in software
        public ColumnList Columns { get; set; }
        public ColumnList BaseColumns { get; set; }

        public ProgressIndication Progress { get; set; }

        public GeneralGrid<T> Grid { get; set; }

        protected abstract List<GeneralColumn> GeneralGenerateColumnsList();

        private List<GeneralColumn> GenerateColumnsList(bool getFromGrid)
        {
            if (getFromGrid)
            {
                if (Grid.IsEmpty())
                    return GeneralGenerateColumnsList();
                else
                    return Grid.GetColumns();
            }
            else
                return GeneralGenerateColumnsList();
        }

        /// <summary>
        /// Update current lists column numbers from new list
        /// </summary>
        /// <param name="newList">new list with new column numbers</param>
        public void UpdateColumnNumbers (List<GeneralColumn> newList)
        {
            string _keyword = string.Empty;
            int _columnNumber = 0;
            bool _canHide = false;

            List<GeneralColumn> _tmpList = new List<GeneralColumn>();
            for (int _index = 0; _index < BaseColumns.Columns.Count; _index++)
            {
                _keyword = BaseColumns.ElementAt(_index).Keyword;
                _columnNumber = BaseColumns.ElementAt(_index).Number;
                _canHide = BaseColumns.ElementAt(_index).CanHide;

                foreach (GeneralColumn _newColumn in newList)
                {
                    if (_newColumn.Keyword == _keyword)
                    {
                        //if in new list column is used and in current list column is not used, add column to end
                        if (_columnNumber == -1 && _newColumn.Number != -1)
                            _columnNumber = 100;
                        break;
                    }
                }
                GeneralColumn _column = new GeneralColumn(_keyword, _columnNumber, _canHide);
                _tmpList.Add(_column);
            }

            BaseColumns.Columns.Clear();
            Columns.Columns.Clear();

            for (int _index = 0; _index < _tmpList.Count; _index++)
            {
                GeneralColumn _column = _tmpList.ElementAt(_index);

                BaseColumns.Columns.Add(_column);
                Columns.Columns.Add(_column);
            }

            BaseColumns.SortColumnsList(false);
            Columns.SortColumnsList(true);

            UpdateSettingsColumnsList();
        }
        protected abstract void UpdateSettingsColumnsList();

        public GeneralClass(string name, string sheetName,bool notSortable, string fileExtension,  ProgressIndication progress, DataGridView grid)
        {
            Name = name;
            Signals = new List<T>();
            Columns = new ColumnList();
            BaseColumns = new ColumnList();
            Progress = progress;

            Grid = new GeneralGrid<T>(name, sheetName, notSortable, fileExtension, Signals, progress,grid, Columns, BaseColumns);

            BaseColumns.SetColumns(GenerateColumnsList(false), false);
            Columns.SetColumns(GenerateColumnsList(true),true);

            Columns.SortColumnsList(true);

            UpdateSettingsColumnsList();
        }

        /// <summary>
        /// Get excel cell data, uses general function and always return string
        /// </summary>
        /// <param name="row">current row</param>
        /// <param name="col">column to read</param>
        /// <param name="maxCol">maximum columns in row</param>
        /// <param name="excel">opened excel file</param>
        /// <returns>string value of cell value</returns>
        public string ReadExcelCell(int row, int col,int maxCol, IExcelDataReader excel)
        {
            string _retunValue = string.Empty;
            if (col >= maxCol || col < 0)
            {
                Debug debug = new Debug();
                debug.ToFile(Resources.DataReadFailBounds + " " + Resources.Column +" "+ col + " max(" + maxCol + ")"+
                                Resources.Row + " " + row, DebugLevels.Minimum, DebugMessageType.Warning);
            }
            else
            {
                System.Type _type = excel.GetFieldType(col);
                if (_type == null)
                    _retunValue = string.Empty;
                else
                {
                    if (_type.Name == "String")
                        _retunValue = excel.GetString(col);
                    else if (_type.Name == "Double")
                        _retunValue = excel.GetDouble(col).ToString();
                    else
                    {
                        Debug debug = new Debug();
                        debug.ToPopUp(Resources.DataReadFailFormat + ": " + _type.Name, DebugLevels.None, DebugMessageType.Critical);
                    }
                }
            }
            return _retunValue;
        }
    }  
}
