using IO_list_automation_new.Helper_functions;
using IO_list_automation_new.Properties;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IO_list_automation_new
{
    internal class GeneralColumnParameters
    {
        //column number for sorting
        public int NR { get; set; } = 0;
        public bool CanHide { get; set; } = false;

        public GeneralColumnParameters(int columnNumber, bool canHide)
        {
            NR = columnNumber;
            CanHide = canHide;
        }
    }

    internal class ColumnList
    {
        public Dictionary<string, GeneralColumnParameters> Columns { get; set; } = new Dictionary<string, GeneralColumnParameters>();

        /// <summary>
        /// Sort all columns from lowest to highest
        /// </summary>
        /// <param name="columnsFromZero">for writing data to grid, to do not have empty columns</param>
        /// <returns>sorted list</returns>
        public void SortColumns(bool columnsFromZero)
        {
            Dictionary<string, GeneralColumnParameters> tmpList = new Dictionary<string, GeneralColumnParameters>();

            //copy list
            foreach (var item in Columns)
            {
                if (columnsFromZero && item.Value.NR < 0)
                    continue;

                tmpList.Add(item.Key, item.Value);

            }
            Columns.Clear();

            //set 0 index in list to minimum value
            string minKey = string.Empty;
            int minValue;
            int count = tmpList.Count;

            //sorting from lower to higher
            for (int index = 0; index < count; index++)
            {
                minValue = int.MaxValue;
                foreach (var column in tmpList)
                {
                    if (column.Value.NR < minValue)
                    {
                        minKey = column.Key;
                        minValue = column.Value.NR;
                    }
                }

                Columns.Add(minKey, tmpList[minKey]);
                tmpList.Remove(minKey);
            }
        }

        /// <summary>
        /// Create new GeneralColumn list
        /// </summary>
        /// <param name="listFrom">list to be copied</param>
        /// <param name="columnsFromZero">true, Columns start from zero</param>
        public void SetColumns(ColumnList listFrom, bool columnsFromZero)
        {
            Columns.Clear();

            foreach (var column in listFrom.Columns)
                Columns.Add(column.Key, column.Value);

            SortColumns(columnsFromZero);
        }


        /// <summary>
        /// Find column with keyword and return its column number
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <returns>column number</returns>
        public int GetColumnNumberFromKeyword(string keyword)
        {
            if (Columns.TryGetValue(keyword, out var result))
                return result.NR;
            else
                return -1;
        }

        /// <summary>
        /// Find column with keyword and return its column number
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <returns>column number</returns>
        public string GetColumnNameFromKeyword(string keyword)
        {
            return TextHelper.GetColumnName(keyword, false);            
        }

        public override string ToString()
        {
            List<string> texts = new List<string>();
            foreach (var column in Columns)
                texts.Add(column.Key);
            return texts.ToString();
        }
    }
}