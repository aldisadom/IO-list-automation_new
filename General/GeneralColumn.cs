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
using SharpCompress;

namespace IO_list_automation_new
{
    internal class GeneralColumnName
    {
        public GeneralColumnName()
        {
        }

        /// <summary>
        /// Get name from column keyword and from DB choices
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <returns>name of column</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public string GetColumnOrChoicesName(string keyword)
        {
            string _returnName = GetChoicesName(keyword, true) ?? GetColumnName(keyword, true);

            if (_returnName == null)
            {
                const string text = "GetName";
                Debug _debug = new Debug();
                _debug.ToFile("Report to programmer that " + text + "." + keyword + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                throw new InvalidProgramException(text + "." + keyword + " is not created for this element");
            }
            return _returnName;
        }

        /// <summary>
        /// Get name from column keyword
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <param name="suppressError">suppress error if not found</param>
        /// <returns>name of column</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public string GetColumnName(string keyword, bool suppressError)
        {
            string _returnName = string.Empty;

            switch (keyword)
            {
                case KeywordColumn.ID:
                    _returnName = ResourcesColumns.ID;
                    break;
                case KeywordColumn.CPU:
                    _returnName = ResourcesColumns.CPU;
                    break;
                case KeywordColumn.KKS:
                    _returnName = ResourcesColumns.KKS;
                    break;
                case KeywordColumn.RangeMin:
                    _returnName = ResourcesColumns.RangeMin;
                    break;
                case KeywordColumn.RangeMax:
                    _returnName = ResourcesColumns.RangeMax;
                    break;
                case KeywordColumn.Units:
                    _returnName = ResourcesColumns.Units;
                    break;
                case KeywordColumn.FalseText:
                    _returnName = ResourcesColumns.FalseText;
                    break;
                case KeywordColumn.TrueText:
                    _returnName = ResourcesColumns.TrueText;
                    break;
                case KeywordColumn.Revision:
                    _returnName = ResourcesColumns.Revision;
                    break;
                case KeywordColumn.Cable:
                    _returnName = ResourcesColumns.Cable;
                    break;
                case KeywordColumn.Cabinet:
                    _returnName = ResourcesColumns.Cabinet;
                    break;
                case KeywordColumn.ModuleName:
                    _returnName = ResourcesColumns.ModuleName;
                    break;
                case KeywordColumn.Pin:
                    _returnName = ResourcesColumns.Pin;
                    break;
                case KeywordColumn.Channel:
                    _returnName = ResourcesColumns.Channel;
                    break;
                case KeywordColumn.IOText:
                    _returnName = ResourcesColumns.IOText;
                    break;
                case KeywordColumn.Page:
                    _returnName = ResourcesColumns.Page;
                    break;
                case KeywordColumn.Changed:
                    _returnName = ResourcesColumns.Changed;
                    break;
                case KeywordColumn.Operative:
                    _returnName = ResourcesColumns.Operative;
                    break;
                case KeywordColumn.KKSPlant:
                    _returnName = ResourcesColumns.KKSPlant;
                    break;
                case KeywordColumn.KKSLocation:
                    _returnName = ResourcesColumns.KKSLocation;
                    break;
                case KeywordColumn.KKSDevice:
                    _returnName = ResourcesColumns.KKSDevice;
                    break;
                case KeywordColumn.KKSFunction:
                    _returnName = ResourcesColumns.KKSFunction;
                    break;
                case KeywordColumn.Used:
                    _returnName = ResourcesColumns.Used;
                    break;
                case KeywordColumn.ObjectType:
                    _returnName = ResourcesColumns.ObjectType;
                    break;
                case KeywordColumn.ObjectName:
                    _returnName = ResourcesColumns.ObjectName;
                    break;
                case KeywordColumn.ObjectSpecifics:
                    _returnName = ResourcesColumns.ObjectSpecifics;
                    break;
                case KeywordColumn.FunctionText:
                    _returnName = ResourcesColumns.FunctionText;
                    break;
                case KeywordColumn.Function:
                    _returnName = ResourcesColumns.Function;
                    break;
                case KeywordColumn.Terminal:
                    _returnName = ResourcesColumns.Terminal;
                    break;
                case KeywordColumn.DeviceTypeText:
                    _returnName = ResourcesColumns.DeviceTypeText;
                    break;
                case KeywordColumn.FunctionText1:
                    _returnName = ResourcesColumns.FunctionText1;
                    break;
                case KeywordColumn.Function1:
                    _returnName = ResourcesColumns.Function1;
                    break;
                case KeywordColumn.FunctionText1o2:
                    _returnName = ResourcesColumns.FunctionText1o2;
                    break;
                case KeywordColumn.FunctionText2o2:
                    _returnName = ResourcesColumns.FunctionText2o2;
                    break;
                case KeywordColumn.Function2:
                    _returnName = ResourcesColumns.Function2;
                    break;
                case KeywordColumn.Tag:
                    _returnName = ResourcesColumns.Tag;
                    break;
                case KeywordColumn.ModuleType:
                    _returnName = ResourcesColumns.ModuleType;
                    break;
                default:
                    _returnName = string.Empty;
                    if (!suppressError)
                    {
                        const string text = "GetColumnName";
                        Debug _debug = new Debug();
                        _debug.ToFile("Report to programmer that " + text + "." + keyword + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                        throw new InvalidProgramException(text + "." + keyword + " is not created for this element");
                    }
                    break;
            }
            return _returnName;
        }

        /// <summary>
        /// Get name from choices keyword
        /// </summary>
        /// <param name="keyword">choices keyword</param>
        /// <param name="suppressError">suppress error if not found</param>
        /// <returns>name of choice</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public string GetChoicesName(string keyword, bool suppressError)
        {
            switch (keyword)
            {
                case KeywordDBChoices.None:
                    return string.Empty;
                case KeywordDBChoices.Tab:
                    return ResourcesChoices.Tab;
                case KeywordDBChoices.If:
                    return ResourcesChoices.If;
                case KeywordDBChoices.Text:
                    return ResourcesChoices.Text;
                case KeywordDBChoices.Data:
                    return ResourcesChoices.Data;
                case KeywordDBChoices.Object:
                    return ResourcesChoices.Object;
                case KeywordDBChoices.IO:
                    return ResourcesChoices.IO;
                case KeywordDBChoices.TagType:
                    return ResourcesChoices.TagType;
                case KeywordDBChoices.IsEmpty:
                    return ResourcesChoices.IsEmpty;
                case KeywordDBChoices.Index:
                    return ResourcesChoices.Index;
                case KeywordDBChoices.IsNotEmpty:
                    return ResourcesChoices.IsNotEmpty;
                default:
                    if (!suppressError)
                    {
                        const string text = "GetChoicesName";
                        Debug _debug = new Debug();
                        _debug.ToFile("Report to programmer that " + text + "." + keyword + " is not created for this element", DebugLevels.None, DebugMessageType.Critical);
                        throw new InvalidProgramException(text + "." + keyword + " is not created for this element");
                    }
                    else
                    {
                        return null;
                    }
            }
        }
    }

    internal class GeneralColumn
    {
        //column keyword
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
        /// <returns>column name of column based on language</returns>
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
        /// <param name="columnsFromZero">for writing data to grid, to do not have empty columns</param>
        /// <returns>sorted list</returns>
        public void SortColumnsList(bool columnsFromZero)
        {
            string _keyword = string.Empty;
            int _columnNumber = 0;
            bool _canHide = false;

            List<GeneralColumn> _tmpList = new List<GeneralColumn>();
            for (int _index = 0; _index < Columns.Count; _index++)
            {
                GeneralColumn _column = Columns[_index];

                _keyword = _column.Keyword;
                _columnNumber = _column.Number;
                _canHide = _column.CanHide;
                // if columnsFromZero and column number is -1 then do not add to array
                if (!columnsFromZero || _columnNumber >= 0)
                    _tmpList.Add(new GeneralColumn(_keyword, _columnNumber, _canHide));
            }
            Columns.Clear();

            //set 0 index in list to minimum value
            int _minIndex = 0;
            int _minValue = int.MaxValue;
            int _columnIndex = 0;
            int _count = _tmpList.Count;

            //sorting from lower to higher
            for (int _index = 0; _index < _count; _index++)
            {
                _minValue = int.MaxValue;
                for (int i = 0; i < _tmpList.Count(); i++)
                {
                    if (_tmpList[i].Number < _minValue)
                    {
                        _minIndex = i;
                        _minValue = _tmpList[i].Number;
                    }
                }

                _columnNumber = _tmpList[_minIndex].Number;

                if (columnsFromZero)
                {
                    if (_columnNumber < 0)
                        continue;

                    _columnNumber = _columnIndex;
                    _columnIndex++;
                }

                _keyword = _tmpList[_minIndex].Keyword;
                _canHide = _tmpList[_minIndex].CanHide;
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
        /// Find column with keyword and return its column number
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
