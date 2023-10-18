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
using System.Security.Cryptography;

namespace IO_list_automation_new
{
    internal class GeneralColumnName
    {
        public GeneralColumnName()
        {
        }

        public string GetName(string keyword)
        {
            string _returnName = GetChoicesName(keyword, true);
            if (_returnName == null)
                _returnName = GetColumnName(keyword, true);

            if (_returnName == null)
            {
                string text = "GetName";
                Debug _debug = new Debug();
                _debug.ToFile("Report to programer that " + text + "." + keyword + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                throw new InvalidProgramException(text + "." + keyword + " is not created for this element");
            }

            return _returnName;
        }

        public string GetColumnName(string keyword, bool supressError)
        {
            string _returnName = string.Empty;

            switch (keyword)
            {
                case ConstCol.ColumnNameID:
                    _returnName = Resources.ColumnID;
                    break;
                case ConstCol.ColumnNameCPU:
                    _returnName = Resources.ColumnCPU;
                    break;
                case ConstCol.ColumnNameKKS:
                    _returnName = Resources.ColumnKKS;
                    break;
                case ConstCol.ColumnNameRangeMin:
                    _returnName = Resources.ColumnRangeMin;
                    break;
                case ConstCol.ColumnNameRangeMax:
                    _returnName = Resources.ColumnRangeMax;
                    break;
                case ConstCol.ColumnNameUnits:
                    _returnName = Resources.ColumnUnits;
                    break;
                case ConstCol.ColumnNameFalseText:
                    _returnName = Resources.ColumnFalseText;
                    break;
                case ConstCol.ColumnNameTrueText:
                    _returnName = Resources.ColumnTrueText;
                    break;
                case ConstCol.ColumnNameRevision:
                    _returnName = Resources.ColumnRevision;
                    break;
                case ConstCol.ColumnNameCable:
                    _returnName = Resources.ColumnCable;
                    break;
                case ConstCol.ColumnNameCabinet:
                    _returnName = Resources.ColumnCabinet;
                    break;
                case ConstCol.ColumnNameModuleName:
                    _returnName = Resources.ColumnModuleName;
                    break;
                case ConstCol.ColumnNamePin:
                    _returnName = Resources.ColumnPin;
                    break;
                case ConstCol.ColumnNameChannel:
                    _returnName = Resources.ColumnChannel;
                    break;
                case ConstCol.ColumnNameIOText:
                    _returnName = Resources.ColumnIOText;
                    break;
                case ConstCol.ColumnNamePage:
                    _returnName = Resources.ColumnPage;
                    break;
                case ConstCol.ColumnNameChanged:
                    _returnName = Resources.ColumnChanged;
                    break;
                case ConstCol.ColumnNameOperative:
                    _returnName = Resources.ColumnOperative;
                    break;
                case ConstCol.ColumnNameKKSPlant:
                    _returnName = Resources.ColumnKKSPlant;
                    break;
                case ConstCol.ColumnNameKKSLocation:
                    _returnName = Resources.ColumnKKSLocation;
                    break;
                case ConstCol.ColumnNameKKSDevice:
                    _returnName = Resources.ColumnKKSDevice;
                    break;
                case ConstCol.ColumnNameKKSFunction:
                    _returnName = Resources.ColumnKKSFunction;
                    break;
                case ConstCol.ColumnNameUsed:
                    _returnName = Resources.ColumnUsed;
                    break;
                case ConstCol.ColumnNameObjectType:
                    _returnName = Resources.ColumnObjectType;
                    break;
                case ConstCol.ColumnNameObjectName:
                    _returnName = Resources.ColumnObjectName;
                    break;
                case ConstCol.ColumnNameObjectDetalisation:
                    _returnName = Resources.ColumnObjectDetalisation;
                    break;
                case ConstCol.ColumnNameFunctionText:
                    _returnName = Resources.ColumnFunctionText;
                    break;
                case ConstCol.ColumnNameFunction:
                    _returnName = Resources.ColumnFunction;
                    break;
                case ConstCol.ColumnNameTerminal:
                    _returnName = Resources.ColumnTerminal;
                    break;
                case ConstCol.ColumnNameDeviceTypeText:
                    _returnName = Resources.ColumnDeviceTypeText;
                    break;
                case ConstCol.ColumnNameFunctionText1:
                    _returnName = Resources.ColumnFunctionText1;
                    break;
                case ConstCol.ColumnNameFunction1:
                    _returnName = Resources.ColumnFunction1;
                    break;
                case ConstCol.ColumnNameFunctionText1o2:
                    _returnName = Resources.ColumnFunctionText1o2;
                    break;
                case ConstCol.ColumnNameFunctionText2o2:
                    _returnName = Resources.ColumnFunctionText2o2;
                    break;
                case ConstCol.ColumnNameFunction2:
                    _returnName = Resources.ColumnFunction2;
                    break;
                case ConstCol.ColumnNameTag:
                    _returnName = Resources.ColumnTag;
                    break;
                default:
                    _returnName = string.Empty;
                    if (!supressError)
                    {
                        string text = "GetColumnName";
                        Debug _debug = new Debug();
                        _debug.ToFile("Report to programer that " + text + "." + keyword + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                        throw new InvalidProgramException(text + "." + keyword + " is not created for this element");
                    }
                    break;
            }
            return _returnName;
        }

        public string GetChoicesName(string keyword, bool supressError)
        {
            string _returnName = string.Empty;

            switch (keyword)
            {
                case ConstDBChoices.ChoiceNone:
                    _returnName = "";
                    break;
                case ConstDBChoices.ChoiceTab:
                    _returnName = Resources.ChoiceTab;
                    break;
                case ConstDBChoices.ChoiceIf:
                    _returnName = Resources.ChoiceIf;
                    break;
                case ConstDBChoices.ChoiceText:
                    _returnName = Resources.ChoiceText;
                    break;
                case ConstDBChoices.ChoiceData:
                    _returnName = Resources.ChoiceData;
                    break;
                case ConstDBChoices.ChoiceObject:
                    _returnName = Resources.ChoiceObject;
                    break;
                case ConstDBChoices.ChoiceIO:
                    _returnName = Resources.ChoiceIO;
                    break;
                case ConstDBChoices.ChoiceIsEmpty:
                    _returnName = Resources.ChoiceIsEmpty;
                    break;
                case ConstDBChoices.ChoiceIsNotEmpty:
                    _returnName = Resources.ChoiceIsNotEmpty;
                    break;
                default:
                    _returnName = null;
                    if (!supressError)
                    {
                        string text = "GetChoicesName";
                        Debug _debug = new Debug();
                        _debug.ToFile("Report to programer that " + text + "." + keyword + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                        throw new InvalidProgramException(text + "." + keyword + " is not created for this element");
                    }
                    break;
            }
            return _returnName;
        }

    }

    internal class GeneralColumn
    {
        //culumn keyword
        public string Keyword { get; private set; }
        //column number for sorting
        public int Number { get; private set; }

        public bool CanHide { get; private set; }

        private GeneralColumnName ColumnName = new GeneralColumnName();

        public GeneralColumn(string columnKeyword, int columnNumber, bool canHide)
        {
            Keyword = columnKeyword;
            Number = columnNumber;
            CanHide = canHide;
        }
        /// <summary>
        /// Get column name based on software language
        /// </summary>
        /// <returns>collumn name of column based on language</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public string GetColumnName()
        {
            return ColumnName.GetColumnName(Keyword,false);
        }
    }

    internal class ColumnList : IEnumerable<GeneralColumn>
    {
        public List<GeneralColumn> Columns { get; private set; }

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
            bool _canHide = false;

            List<GeneralColumn> _tmpList = new List<GeneralColumn>();
            for (int _index = 0; _index < Columns.Count; _index++)
            {
                _keyword = Columns.ElementAt(_index).Keyword;
                _columnNumber = Columns.ElementAt(_index).Number;
                _canHide = Columns.ElementAt(_index).CanHide;
                // if columnsFromZero and collumn number is -1 then do not add to array
                if (!columnsFromZero || _columnNumber >= 0)
                {
                    GeneralColumn _column = new GeneralColumn(_keyword, _columnNumber, _canHide);
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
                    if (_tmpList.ElementAt(i).Number < _minValue)
                    {
                        _minIndex = i;
                        _minValue = _tmpList.ElementAt(i).Number;
                    }
                }

                _columnNumber = _tmpList.ElementAt(_minIndex).Number;

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

                _keyword = _tmpList.ElementAt(_minIndex).Keyword;
                _canHide = _tmpList.ElementAt(_minIndex).CanHide;
                GeneralColumn _column = new GeneralColumn(_keyword, _columnNumber, _canHide);

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
        public void SetColumns(List<GeneralColumn> listFrom, bool columnsFromZero)
        {
            Columns.Clear();

            foreach (GeneralColumn _column in listFrom)
            {
                GeneralColumn _newColumn = new GeneralColumn(_column.Keyword, _column.Number, _column.CanHide);

                Columns.Add(_newColumn);
            }
            SortColumnsList(columnsFromZero);
        }


        /// <summary>
        /// Find collumn with keyword and return its column number
        /// </summary>
        /// <param name="_keyword">column keyword</param>
        /// <returns>column number</returns>
        public int GetColumnNumberFromKeyword(string _keyword)
        {
            foreach (GeneralColumn _column in Columns)
            {
                if (_keyword == _column.Keyword)
                    return _column.Number;
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
