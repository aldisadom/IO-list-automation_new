using ExcelDataReader;
using IO_list_automation_new.Properties;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IO_list_automation_new.General
{
    internal static class GeneralFunctions
    {
        /// <summary>
        /// Copy one list to another
        /// </summary>
        /// <param name="input">input list</param>
        /// <returns>new list</returns>
        public static List<string> ListCopy(List<string> input)
        {
            List<string> _list = new List<string>();

            for (int i = 0; i < input.Count; i++)
                _list.Add(input[i]);

            return _list;
        }

        /// <summary>
        /// add additional zeros before number for better sorting
        /// </summary>
        /// <param name="_input">value</param>
        /// <returns>formatted string</returns>
        public static string AddZeroes(int _input)
        {
            if (_input < 10)
                return "000" + _input.ToString();
            else if (_input < 100)
                return "00" + _input.ToString();
            else if (_input < 1000)
                return "0" + _input.ToString();
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
        public static string ReadExcelCell(int row, int col, int maxCol, IExcelDataReader excel)
        {
            Debug debug = new Debug();
            if (col >= maxCol || col < 0)
            {
                debug.ToFile(Resources.DataReadFailBounds + " " + Resources.Column + " " + col + " max(" + maxCol + ")" + Resources.Row + " " + row, DebugLevels.Minimum, DebugMessageType.Warning);
                return string.Empty;
            }

            System.Type _type = excel.GetFieldType(col);
            if (_type == null)
                return string.Empty;

            if (_type.Name == "String")
                return excel.GetString(col);
            else if (_type.Name == "Double")
                return excel.GetDouble(col).ToString();

            debug.ToPopUp(Resources.DataReadFailFormat + ": " + _type.Name, DebugLevels.None, DebugMessageType.Critical);
            return string.Empty;
        }
    }
}