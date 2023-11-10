using IO_list_automation_new.Properties;
using System;
using System.Collections;
using System.Collections.Generic;

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

            if (_returnName != null)
                return _returnName;

            const string _debugText = "GeneralColumn.GetColumnOrChoicesName";
            Debug _debug = new Debug();
            _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + keyword, DebugLevels.None, DebugMessageType.Critical);
            throw new InvalidProgramException(_debugText + "." + keyword + " is not created for this element");
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
            switch (keyword)
            {
                case KeywordColumn.ID:
                    return ResourcesColumns.ID;

                case KeywordColumn.CPU:
                    return ResourcesColumns.CPU;

                case KeywordColumn.KKS:
                    return ResourcesColumns.KKS;

                case KeywordColumn.RangeMin:
                    return ResourcesColumns.RangeMin;

                case KeywordColumn.RangeMax:
                    return ResourcesColumns.RangeMax;

                case KeywordColumn.Units:
                    return ResourcesColumns.Units;

                case KeywordColumn.FalseText:
                    return ResourcesColumns.FalseText;

                case KeywordColumn.TrueText:
                    return ResourcesColumns.TrueText;

                case KeywordColumn.Revision:
                    return ResourcesColumns.Revision;

                case KeywordColumn.Cable:
                    return ResourcesColumns.Cable;

                case KeywordColumn.Cabinet:
                    return ResourcesColumns.Cabinet;

                case KeywordColumn.ModuleName:
                    return ResourcesColumns.ModuleName;

                case KeywordColumn.Pin:
                    return ResourcesColumns.Pin;

                case KeywordColumn.Channel:
                    return ResourcesColumns.Channel;

                case KeywordColumn.IOText:
                    return ResourcesColumns.IOText;

                case KeywordColumn.Page:
                    return ResourcesColumns.Page;

                case KeywordColumn.Changed:
                    return ResourcesColumns.Changed;

                case KeywordColumn.Operative:
                    return ResourcesColumns.Operative;

                case KeywordColumn.KKSPlant:
                    return ResourcesColumns.KKSPlant;

                case KeywordColumn.KKSLocation:
                    return ResourcesColumns.KKSLocation;

                case KeywordColumn.KKSDevice:
                    return ResourcesColumns.KKSDevice;

                case KeywordColumn.KKSFunction:
                    return ResourcesColumns.KKSFunction;

                case KeywordColumn.Used:
                    return ResourcesColumns.Used;

                case KeywordColumn.ObjectType:
                    return ResourcesColumns.ObjectType;

                case KeywordColumn.ObjectName:
                    return ResourcesColumns.ObjectName;

                case KeywordColumn.ObjectSpecifics:
                    return ResourcesColumns.ObjectSpecifics;

                case KeywordColumn.FunctionText:
                    return ResourcesColumns.FunctionText;

                case KeywordColumn.Function:
                    return ResourcesColumns.Function;

                case KeywordColumn.Terminal:
                    return ResourcesColumns.Terminal;

                case KeywordColumn.DeviceTypeText:
                    return ResourcesColumns.DeviceTypeText;

                case KeywordColumn.FunctionText1:
                    return ResourcesColumns.FunctionText1;

                case KeywordColumn.Function1:
                    return ResourcesColumns.Function1;

                case KeywordColumn.FunctionText1o2:
                    return ResourcesColumns.FunctionText1o2;

                case KeywordColumn.FunctionText2o2:
                    return ResourcesColumns.FunctionText2o2;

                case KeywordColumn.Function2:
                    return ResourcesColumns.Function2;

                case KeywordColumn.Tag:
                    return ResourcesColumns.Tag;

                case KeywordColumn.ModuleType:
                    return ResourcesColumns.ModuleType;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string _debugText = "GeneralColumn.GetColumnName";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + keyword, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + keyword + " is not created for this element");
            }
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

                case KeywordDBChoices.Modules:
                    return ResourcesChoices.Modules;

                case KeywordDBChoices.BaseAddress:
                    return ResourcesChoices.BaseAddress;

                case KeywordDBChoices.Address:
                    return ResourcesChoices.Address;

                case KeywordDBChoices.AddressArea:
                    return ResourcesChoices.AddressArea;

                case KeywordDBChoices.GetBaseAddress:
                    return ResourcesChoices.GetBaseAddress;

                case KeywordDBChoices.Insert:
                    return ResourcesChoices.Insert;

                case KeywordDBChoices.MultiLine:
                    return ResourcesChoices.MultiLine;

                case KeywordDBChoices.IsEmpty:
                    return ResourcesChoices.IsEmpty;

                case KeywordDBChoices.IsNotEmpty:
                    return ResourcesChoices.IsNotEmpty;

                case KeywordDBChoices.Equal:
                    return ResourcesChoices.Equal;

                case KeywordDBChoices.nEqual:
                    return ResourcesChoices.nEqual;

                case KeywordDBChoices.GreaterEqual:
                    return ResourcesChoices.GreaterEqual;

                case KeywordDBChoices.Greater:
                    return ResourcesChoices.Greater;

                case KeywordDBChoices.LessEqual:
                    return ResourcesChoices.LessEqual;

                case KeywordDBChoices.Less:
                    return ResourcesChoices.Less;

                default:
                    if (suppressError)
                        return null;

                    const string _debugText = "GeneralColumn.GetChoicesName";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + keyword, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + keyword + " is not created for this element");
            }
        }
    }

    internal class GeneralColumn
    {
        //column keyword
        public string Keyword { get; }

        //column number for sorting
        public int Number { get; }

        public bool CanHide { get; }

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
        public string GetColumnName(bool useKeywordAsName)
        {
            return useKeywordAsName ? Keyword : ColumnName.GetColumnName(Keyword, false);
        }
    }

    internal class ColumnList : IEnumerable<GeneralColumn>
    {
        public List<GeneralColumn> Columns { get; }

        /// <summary>
        /// Sort all columns from lowest to highest
        /// </summary>
        /// <param name="columnsFromZero">for writing data to grid, to do not have empty columns</param>
        /// <returns>sorted list</returns>
        public void SortColumnsList(bool columnsFromZero)
        {
            string _keyword;
            int _columnNumber;
            bool _canHide;

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
            int _minValue;
            int _columnIndex = 0;
            int _count = _tmpList.Count;

            //sorting from lower to higher
            for (int _index = 0; _index < _count; _index++)
            {
                _minValue = int.MaxValue;
                for (int i = 0; i < _tmpList.Count; i++)
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

        public List<string> GetColumnsKeyword()
        {
            List<string> _columns = new List<string>();

            foreach (GeneralColumn _column in Columns)
                _columns.Add(_column.Keyword);

            return _columns;
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