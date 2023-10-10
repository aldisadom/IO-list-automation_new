using SharpCompress.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Security.AccessControl;
using IO_list_automation_new.Properties;
using System.Data.Common;

namespace IO_list_automation_new
{
    internal class GeneralColumn
    {
        //culumn keyword
        protected string ColumnKeyword { get; set; }
        //column number for sorting
        protected int ColumnNumber { get; set; }

        public GeneralColumn(string columnNameProgram, int columnNumber)
        {
            ColumnKeyword = columnNameProgram;
            ColumnNumber = columnNumber;
        }
        public string GetColumnKeyword()
        {
            return ColumnKeyword;
        }

        /// <summary>
        /// Get column name based on software language
        /// </summary>
        /// <param name="keyword">keyword of column</param>
        /// <returns>collumn name of column based on language</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public string GetColumnName(string keyword)
        {
            string _returnName = string.Empty;

            switch (keyword)
            {
                case Const.ColumnNameID:
                    _returnName = Resources.ColumnID;
                    break;
                case Const.ColumnNameCPU:
                    _returnName = Resources.ColumnCPU;
                    break;
                case Const.ColumnNameKKS:
                    _returnName = Resources.ColumnKKS;
                    break;
                case Const.ColumnNameRangeMin:
                    _returnName = Resources.ColumnRangeMin;
                    break;
                case Const.ColumnNameRangeMax:
                    _returnName = Resources.ColumnRangeMax;
                    break;
                case Const.ColumnNameUnits:
                    _returnName = Resources.ColumnUnits;
                    break;
                case Const.ColumnNameFalseText:
                    _returnName = Resources.ColumnFalseText;
                    break;
                case Const.ColumnNameTrueText:
                    _returnName = Resources.ColumnTrueText;
                    break;
                case Const.ColumnNameRevision:
                    _returnName = Resources.ColumnRevision;
                    break;
                case Const.ColumnNameCable:
                    _returnName = Resources.ColumnCable;
                    break;
                case Const.ColumnNameCabinet:
                    _returnName = Resources.ColumnCabinet;
                    break;
                case Const.ColumnNameModuleName:
                    _returnName = Resources.ColumnModuleName;
                    break;
                case Const.ColumnNamePin:
                    _returnName = Resources.ColumnPin;
                    break;
                case Const.ColumnNameChannel:
                    _returnName = Resources.ColumnChannel;
                    break;
                case Const.ColumnNameIOText:
                    _returnName = Resources.ColumnIOText;
                    break;
                case Const.ColumnNamePage:
                    _returnName = Resources.ColumnPage;
                    break;
                case Const.ColumnNameChanged:
                    _returnName = Resources.ColumnChanged;
                    break;
                case Const.ColumnNameOperative:
                    _returnName = Resources.ColumnOperative;
                    break;
                case Const.ColumnNameKKSPlant:
                    _returnName = Resources.ColumnKKSPlant;
                    break;
                case Const.ColumnNameKKSLocation:
                    _returnName = Resources.ColumnKKSLocation;
                    break;
                case Const.ColumnNameKKSDevice:
                    _returnName = Resources.ColumnKKSDevice;
                    break;
                case Const.ColumnNameKKSFunction:
                    _returnName = Resources.ColumnKKSFunction;
                    break;
                case Const.ColumnNameUsed:
                    _returnName = Resources.ColumnUsed;
                    break;
                case Const.ColumnNameObjectType:
                    _returnName = Resources.ColumnObjectType;
                    break;
                case Const.ColumnNameObjectName:
                    _returnName = Resources.ColumnObjectName;
                    break;
                case Const.ColumnNameObjectDetalisation:
                    _returnName = Resources.ColumnObjectDetalisation;
                    break;
                case Const.ColumnNameFunctionText:
                    _returnName = Resources.ColumnFunctionText;
                    break;
                case Const.ColumnNameFunction:
                    _returnName = Resources.ColumnFunction;
                    break;
                case Const.ColumnNameTerminal:
                    _returnName = Resources.ColumnTerminal;
                    break;
                default:
                    _returnName = keyword;
                    string text = "GetColumnName";
                    Debug _debug = new Debug();
                    _debug.ToFile("Report to programer that " + text + "." + keyword + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + keyword + " is not created for this element");
            }
            return _returnName;
        }

        /// <summary>
        /// Column number return
        /// </summary>
        /// <returns>column number</returns>
        public int GetColumnNumber()
        {
            return ColumnNumber;
        }

    }

    internal class ColumnList : IEnumerable<GeneralColumn>
    {
        public List<GeneralColumn> Columns;

        /// <summary>
        /// Sort all columns from lowest to highest
        /// </summary>
        /// <param name="columnList">list to be sorted</param>
        /// <param name="columnsFromZero">for writing data to grid, to dont have empty columns</param>
        /// <returns>sorted list</returns>
        public void SortColumnsList(bool columnsFromZero)
        {
            string _keyword = string.Empty;
            int _columnNumber = 0;

            List<GeneralColumn> _tmpList = new List<GeneralColumn>();
            for (int _index = 0; _index < Columns.Count; _index++)
            {
                _keyword = Columns.ElementAt(_index).GetColumnKeyword();
                _columnNumber = Columns.ElementAt(_index).GetColumnNumber();
                // if columnsFromZero and collumn number is -1 then do not add to array
                if (!columnsFromZero || _columnNumber >= 0)
                {
                    GeneralColumn _column = new GeneralColumn(_keyword, _columnNumber);
                    _tmpList.Add(_column);
                }
            }
            Columns.Clear();

            //set 0 index in list to minimum value
            int _minIndex = 0;
            int _minValue = int.MaxValue;
            
            int _columnIndex = 0;

            int _count = _tmpList.Count;
            //sorting from lowet to higher
            for (int _index = 0; _index < _count; _index++)
            {
                _minValue = int.MaxValue;
                for (int i = 0; i < _tmpList.Count(); i++)
                {
                    if (_tmpList.ElementAt(i).GetColumnNumber() < _minValue)
                    {
                        _minIndex = i;
                        _minValue = _tmpList.ElementAt(i).GetColumnNumber();
                    }
                }

                _columnNumber = _tmpList.ElementAt(_minIndex).GetColumnNumber();

                if (columnsFromZero)
                {
                    if (_columnNumber >= 0)
                    {
                        _columnNumber = _columnIndex;
                        _columnIndex++;
                    }
                    else
                        continue;
                }

                _keyword = _tmpList.ElementAt(_minIndex).GetColumnKeyword();
                GeneralColumn _column = new GeneralColumn(_keyword, _columnNumber);

                Columns.Add(_column);
                _tmpList.RemoveAt(_minIndex);
            }
        }

        public ColumnList()
        {
            Columns = new List<GeneralColumn>();
        }

        /// <summary>
        /// Create new GeneralColumn list
        /// </summary>
        /// <param name="listFrom">list to be copied</param>
        /// <param name="columnsFromZero">true, Columns start from zero</param>
        public void CopyColumnList(List<GeneralColumn> listFrom, bool columnsFromZero)
        {
            string _keyword = string.Empty;
            int _columnNumber = 0;

            Columns.Clear();

            foreach (GeneralColumn _column in listFrom)
            {
                _keyword = _column.GetColumnKeyword();
                _columnNumber = _column.GetColumnNumber();
                GeneralColumn _newColumn = new GeneralColumn(_keyword, _columnNumber);

                foreach (GeneralColumn _columnNew in listFrom)
                {
                    if (_keyword == _columnNew.GetColumnKeyword())
                    {
                        _newColumn = new GeneralColumn(_keyword, _columnNew.GetColumnNumber());
                        break;
                    }
                }
                Columns.Add(_newColumn);
            }
            SortColumnsList(columnsFromZero);
        }

        public int GetColumnNumberFromKeyword(string _keyword)
        {
            foreach (GeneralColumn _column in Columns)
            {
                if (_keyword == _column.GetColumnKeyword())
                    return _column.GetColumnNumber();
            }
            return -1;
        }

        IEnumerator<GeneralColumn> IEnumerable<GeneralColumn>.GetEnumerator()
        {
            return Columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
