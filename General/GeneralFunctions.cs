using ExcelDataReader;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_list_automation_new.General
{
    internal class GeneralFunctions
    {
        /// <summary>
        /// Copy one list to another
        /// </summary>
        /// <param name="input">input list</param>
        /// <returns>new list</returns>
        public List<string> ListCopy(List<string> input)
        {
            List<string> _list = new List<string>();

            for (int i = 0; i < input.Count; i++)
                _list.Add(input[i]);

            return _list;
        }

        /// <summary>
        /// add aditional zeros before number for better sorting
        /// </summary>
        /// <param name="_input">value</param>
        /// <returns>formated string</returns>
        public string AddZeroes(int _input)
        {
            if (_input < 10)
                return ("000" + _input.ToString());
            else if (_input < 100)
                return ("00" + _input.ToString());
            else if (_input < 1000)
                return ("0" + _input.ToString());
            else
                return _input.ToString();
        }

        /// <summary>
        /// Get excel cell data, uses general function and always return string
        /// </summary>
        /// <param name="row">current row</param>
        /// <param name="col">column to read</param>
        /// <param name="maxCol">maximum columns in row</param>
        /// <param name="excel">opened excel file</param>
        /// <returns>string value of cell value</returns>
        public string ReadExcelCell(int row, int col, int maxCol, IExcelDataReader excel)
        {
            string _retunValue = string.Empty;
            if (col >= maxCol || col < 0)
            {
                Debug debug = new Debug();
                debug.ToFile(Resources.DataReadFailBounds + " " + Resources.Column + " " + col + " max(" + maxCol + ")" +
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
