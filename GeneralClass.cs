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
    internal class GeneralSignal
    {
        private string ID { get; set; }

        public void SetID(string newID)
        {
            ID = newID;
        }
        public string GetID()
        {
            return ID;
        }

        public GeneralSignal()
        {
            ID = string.Empty;
        }

        /// <summary>
        /// Parsing data from excel or grid, according to Column to signal element
        /// </summary>
        /// <param name="value">value to be passed</param>
        /// <param name="parameterName">parameter to be set</param>
        public virtual void SetValueFromString(string value, string parameterName)
        {
            string text = "SetValueFromString";
            Debug _debug1 = new Debug();
            _debug1.ToFile("Report to programer that " + text + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
            throw new InvalidProgramException(text + " is not created for this element");
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns>value of parameter</returns>
        public virtual string GetValueString(string parameterName, bool supressError)
        {
            string text = "GetValueString";
            Debug _debug1 = new Debug();
            _debug1.ToFile("Report to programer that " + text + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
            throw new InvalidProgramException(text + " is not created for this element");
        }

        /// <summary>
        /// Checks if design signal is valid:
        /// *Has Channel or PIN
        /// *Has Module
        /// *Has IO funcion text
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public virtual bool ValidateSignal()
        {
            string text = "ValidateSignal";
            Debug _debug1 = new Debug();
            _debug1.ToFile("Report to programer that " + text + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
            throw new InvalidProgramException(text + " is not created for this element");
        }
    }

    internal class GeneralClass <T>
    where T : GeneralSignal, new()
    {
        public string Name { get; }

        public List<T> Signals;
        //columns in software
        public ColumnList Columns { get; set; }
        public ColumnList BaseColumns { get; set; }

        public ProgressIndication Progress { get; set; }

        public GeneralGrid<T> Gridasikas { get; set; }

        public virtual List<GeneralColumn> GeneralGenerateColumnsList ()
        {
            string text = "GeneralGenerateColumnsList";
            Debug _debug1 = new Debug();
            _debug1.ToFile("Report to programer that " + Name + "." + text + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
            throw new InvalidProgramException(Name + "." + text + " is not created for this element");
        }

        public virtual List<GeneralColumn> GenerateColumnsList(bool getFromGrid)
        {
            if (getFromGrid)
            {
                if (Gridasikas.IsEmpty())
                    return GeneralGenerateColumnsList();
                else
                    return Gridasikas.GridGetColumns();
            }
            else
                return GeneralGenerateColumnsList();
        }

        public virtual void UpdateSettingsColumnsList()
        {
            string text = "UpdateSettingsColumnsList";
            Debug _debug1 = new Debug();
            _debug1.ToFile("Report to programer that " + Name + "." + text + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
            throw new InvalidProgramException(Name + "." + text + " is not created for this element");
        }

        public GeneralClass(string name, ProgressIndication progress, DataGridView grid)
        {
            Name = name;
            Signals = new List<T>();
            Columns = new ColumnList();
            BaseColumns = new ColumnList();
            Progress = progress;
            Gridasikas = new GeneralGrid<T>(name, Signals, progress,grid, Columns, BaseColumns);

            Columns.Columns = GenerateColumnsList(true);
            BaseColumns.Columns = GenerateColumnsList(false);

            Columns.SortColumnsList(true);
        }

        /// <summary>
        /// copy new list to grid column list
        /// </summary>
        /// <param name="list">new GeneralColumn list</param>
        /// <param name="columnsFromZero">true, Columns start from zero</param>
        public void SetColumnList(List<GeneralColumn> list, bool columnsFromZero)
        {
            Debug debug = new Debug();
            debug.ToFile("Updating : " + Name + " Grid Columns", DebugLevels.Development, DebugMessageType.Info);

            Columns.CopyColumnList(list, columnsFromZero);
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
