using ExcelDataReader;
using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using SharpCompress.Common;
using SharpCompress.Readers.Zip;
using SwiftExcel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace IO_list_automation_new
{
    internal class DesignSignal : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string KKS { get; private set; }
        public string RangeMin { get; private set; }
        public string RangeMax { get; private set; }
        public string Units { get; private set; }
        public string FalseText { get; private set; }
        public string TrueText { get; private set; }
        public string Revision { get; private set; }
        public string Cable { get; private set; }
        public string Cabinet { get; private set; }
        public string ModuleName { get; private set; }
        public string Pin { get; private set; }
        public string Channel { get; private set; }
        public string IOText { get; private set; }
        public string Page { get; private set; }
        public string Changed { get; private set; }
        public string Terminal { get; private set; }

        public string Tag { get; private set; }

        public DesignSignal() : base()
        {
            CPU = string.Empty;
            KKS = string.Empty;
            RangeMin = string.Empty;
            RangeMax = string.Empty;
            Units = string.Empty;
            FalseText = string.Empty;
            TrueText = string.Empty;
            Revision = string.Empty;
            Cable = string.Empty;
            Cabinet = string.Empty;
            ModuleName = string.Empty;
            Pin = string.Empty;
            Channel = string.Empty;
            IOText = string.Empty;
            Page = string.Empty;
            Changed = string.Empty;
            Terminal = string.Empty;
            Tag = string.Empty;
        }

        /// <summary>
        /// Parsing data from excel or grid, according to Column to signal element
        /// </summary>
        /// <param name="value">value to be passed</param>
        /// <param name="parameterName">parameter to be set</param>
        public override void SetValueFromString(string value, string parameterName)
        {
            switch (parameterName)
            {
                case ConstCol.ColumnNameID:
                    ID = value;
                    break;
                case ConstCol.ColumnNameCPU:
                    CPU = value;
                    break;
                case ConstCol.ColumnNameKKS:
                    KKS = value;
                    break;
                case ConstCol.ColumnNameRangeMin:
                    RangeMin = value;
                    break;
                case ConstCol.ColumnNameRangeMax:
                    RangeMax = value;
                    break;
                case ConstCol.ColumnNameUnits:
                    Units = value;
                    break;
                case ConstCol.ColumnNameFalseText:
                    FalseText = value;
                    break;
                case ConstCol.ColumnNameTrueText:
                    TrueText = value;
                    break;
                case ConstCol.ColumnNameRevision:
                    Revision = value;
                    break;
                case ConstCol.ColumnNameCable:
                    Cable = value;
                    break;
                case ConstCol.ColumnNameCabinet:
                    Cabinet = value;
                    break;
                case ConstCol.ColumnNameModuleName:
                    ModuleName = value;
                    break;
                case ConstCol.ColumnNamePin:
                    Pin = value;
                    break;
                case ConstCol.ColumnNameChannel:
                    Channel = value;
                    break;
                case ConstCol.ColumnNameIOText:
                    IOText = value;
                    break;
                case ConstCol.ColumnNamePage:
                    Page = value;
                    break;
                case ConstCol.ColumnNameChanged:
                    Changed = value;
                    break;
                case ConstCol.ColumnNameTerminal:
                    Terminal = value;
                    break;
                case ConstCol.ColumnNameTag:
                    Tag = value;
                    break;
            }
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="supressError">supress alarm message, used only for transfering from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public override string GetValueString(string parameterName, bool supressError)
        {
            string _returnValue = string.Empty;
            switch (parameterName)
            {
                case ConstCol.ColumnNameID:
                    _returnValue = ID;
                    break;
                case ConstCol.ColumnNameCPU:
                    _returnValue = CPU;
                    break;
                case ConstCol.ColumnNameKKS:
                    _returnValue = KKS;
                    break;
                case ConstCol.ColumnNameRangeMin:
                    _returnValue = RangeMin;
                    break;
                case ConstCol.ColumnNameRangeMax:
                    _returnValue = RangeMax;
                    break;
                case ConstCol.ColumnNameUnits:
                    _returnValue = Units;
                    break;
                case ConstCol.ColumnNameFalseText:
                    _returnValue = FalseText;
                    break;
                case ConstCol.ColumnNameTrueText:
                    _returnValue = TrueText;
                    break;
                case ConstCol.ColumnNameRevision:
                    _returnValue = Revision;
                    break;
                case ConstCol.ColumnNameCable:
                    _returnValue = Cable;
                    break;
                case ConstCol.ColumnNameCabinet:
                    _returnValue = Cabinet;
                    break;
                case ConstCol.ColumnNameModuleName:
                    _returnValue = ModuleName;
                    break;
                case ConstCol.ColumnNamePin:
                    _returnValue = Pin;
                    break;
                case ConstCol.ColumnNameChannel:
                    _returnValue = Channel;
                    break;
                case ConstCol.ColumnNameIOText:
                    _returnValue = IOText;
                    break;
                case ConstCol.ColumnNamePage:
                    _returnValue = Page;
                    break;
                case ConstCol.ColumnNameChanged:
                    _returnValue = Changed;
                    break;
                case ConstCol.ColumnNameTerminal:
                    _returnValue = Terminal;
                    break;
                case ConstCol.ColumnNameTag:
                    _returnValue = Tag;
                    break;
                default:
                    if (!supressError)
                    {
                        Debug _debug = new Debug();
                        _debug.ToPopUp(Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    }
                    break;
            }
            return _returnValue;
        }

        /// <summary>
        /// Checks if design signal is valid:
        /// *Has Channel or PIN
        /// *Has Module
        /// *Has IO funcion text
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            bool _returnValue = true;

            if (ModuleName == "" || ModuleName == null)
                _returnValue = false;
            else if (IOText == "" || IOText == null)
                _returnValue = false;
            else if ((Pin == "" || Pin == null) && (Channel == "" || Channel == null))
                _returnValue = false;

            return _returnValue;
        }

        /// <summary>
        /// Check if text has number
        /// </summary>
        /// <param name="text">text to check</param>
        /// <returns>return if has number in string</returns>
        private bool HasNumber(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                    return true;
            }
            return false;

        }

        /// <summary>
        /// Check if signal has useful information, if not discard
        /// </summary>
        /// <param name="_checkPin">pin check for number is needed</param>
        /// <param name="_checkChannel">channel check for number is needed</param>
        /// <returns></returns>
        public bool ExtractUseful(bool _checkPin, bool _checkChannel)
        {
            bool _returnValue = true;

            if (_checkPin)
            {
                if (SettingsDesignInput.Default.PinIsNumber)
                {
                    if (!int.TryParse(Pin, out int _value))
                        _returnValue = false;
                }
                else if (SettingsDesignInput.Default.PinHasNumber)
                {
                    if (!HasNumber(Pin))
                        _returnValue = false;
                }
            }

            if (_checkChannel && _returnValue)
            {
                if (SettingsDesignInput.Default.ChannelIsNumber)
                {
                    if (!int.TryParse(Channel, out int _value))
                        _returnValue = false;
                }
                else if (SettingsDesignInput.Default.ChannelHasNumber)
                {
                    if (!HasNumber(Channel))
                        _returnValue = false;
                }
            }
            return _returnValue;
        }
    }

    internal class DesignClass : GeneralClass<DesignSignal>
    {
        //columns in excel
        private ColumnList ExcelColumns { get; set; }

        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(ConstCol.ColumnNameID, SettingsDesign.Default.ColumnID,false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCPU, SettingsDesign.Default.ColumnCPU, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKS, SettingsDesign.Default.ColumnKKS, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameRangeMin, SettingsDesign.Default.ColumnRangeMin, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameRangeMax, SettingsDesign.Default.ColumnRangeMax, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameUnits, SettingsDesign.Default.ColumnUnits, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFalseText, SettingsDesign.Default.ColumnFalseText, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameTrueText, SettingsDesign.Default.ColumnTrueText, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameRevision, SettingsDesign.Default.ColumnRevision, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCable, SettingsDesign.Default.ColumnCable, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCabinet, SettingsDesign.Default.ColumnCabinet, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameModuleName, SettingsDesign.Default.ColumnModuleName, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNamePin, SettingsDesign.Default.ColumnPin, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameChannel, SettingsDesign.Default.ColumnChannel, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameIOText, SettingsDesign.Default.ColumnIOText, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNamePage, SettingsDesign.Default.ColumnPage, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameChanged, SettingsDesign.Default.ColumnChanged, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameTerminal, SettingsDesign.Default.ColumnTerminal, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameTag, SettingsDesign.Default.ColumnTag, true));

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            SettingsDesign.Default.ColumnID = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameID);
            SettingsDesign.Default.ColumnCPU = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameCPU);
            SettingsDesign.Default.ColumnKKS = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameKKS);
            SettingsDesign.Default.ColumnRangeMin = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameRangeMin);
            SettingsDesign.Default.ColumnRangeMax = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameRangeMax);
            SettingsDesign.Default.ColumnUnits = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameUnits);
            SettingsDesign.Default.ColumnFalseText = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameFalseText);
            SettingsDesign.Default.ColumnTrueText = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameTrueText);
            SettingsDesign.Default.ColumnRevision = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameRevision);
            SettingsDesign.Default.ColumnCable = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameCable);
            SettingsDesign.Default.ColumnCabinet = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameCabinet);
            SettingsDesign.Default.ColumnModuleName = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameModuleName);
            SettingsDesign.Default.ColumnPin = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNamePin);
            SettingsDesign.Default.ColumnChannel = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameChannel);
            SettingsDesign.Default.ColumnIOText = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameIOText);
            SettingsDesign.Default.ColumnPage = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNamePage);
            SettingsDesign.Default.ColumnChanged = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameChanged);
            SettingsDesign.Default.ColumnTerminal = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameTerminal);
            SettingsDesign.Default.ColumnTag = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameTag);

            SettingsDesign.Default.Save();
        }

        /// <summary>
        /// copy new list to excel column list
        /// </summary>
        /// <param name="list">new GeneralColumn list</param>
        /// <param name="columnsFromZero">true, Columns start from zero</param>
        public void SetExcelColumnList(List<GeneralColumn> list, bool columnsFromZero)
        {
            Debug debug = new Debug();
            debug.ToFile("Updating : " + Name + " Grid Columns", DebugLevels.Development, DebugMessageType.Info);

            ExcelColumns.SetColumns(list, columnsFromZero);
        }
        public void InitExcelColumnsList()
        {
            List<GeneralColumn> _excelColumn = new List<GeneralColumn>();
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameID, SettingsDesignInput.Default.ColumnID, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameCPU, SettingsDesignInput.Default.ColumnCPU, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameKKS, SettingsDesignInput.Default.ColumnKKS, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameRangeMin, SettingsDesignInput.Default.ColumnRangeMin, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameRangeMax, SettingsDesignInput.Default.ColumnRangeMax, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameUnits, SettingsDesignInput.Default.ColumnUnits, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameFalseText, SettingsDesignInput.Default.ColumnFalseText, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameTrueText, SettingsDesignInput.Default.ColumnTrueText, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameRevision, SettingsDesignInput.Default.ColumnRevision, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameCable, SettingsDesignInput.Default.ColumnCable, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameCabinet, SettingsDesignInput.Default.ColumnCabinet, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameModuleName, SettingsDesignInput.Default.ColumnModuleName, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNamePin, SettingsDesignInput.Default.ColumnPin, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameChannel, SettingsDesignInput.Default.ColumnChannel, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameIOText, SettingsDesignInput.Default.ColumnIOText, false));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNamePage, SettingsDesignInput.Default.ColumnPage, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameChanged, SettingsDesignInput.Default.ColumnChanged, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameTerminal, SettingsDesignInput.Default.ColumnTerminal, true));
            _excelColumn.Add(new GeneralColumn(ConstCol.ColumnNameTag, SettingsDesignInput.Default.ColumnTag, true));

            SetExcelColumnList(_excelColumn, false);
        }

        /// <summary>
        /// add aditional zeros before number for better sorting
        /// </summary>
        /// <param name="_input">value</param>
        /// <returns>formated string</returns>
        private string AddZeroes(int _input)
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

        public DesignClass(ProgressIndication progress, DataGridView grid) : base("Design", "sheet",false, ".design", progress, grid)
        {
            ExcelColumns = new ColumnList();
        }

        /// <summary>
        /// Get data from excel file and get relevant signals
        /// </summary>
        /// <returns>succesful data read</returns>
        public bool GetDataFromImportFile()
        {
            Debug debug = new Debug();

            //crate open file dialog to open excel files only
            OpenFileDialog _importfile = new OpenFileDialog();
            _importfile.Filter = "Excel Worksheets|*.xls;*.xlsx" + "|All Files|*.*";
            if (_importfile.ShowDialog() == DialogResult.OK)
            {
                debug.ToFile("Excel file for design is: " + _importfile.FileName, DebugLevels.Development, DebugMessageType.Info);
                //open excel file
                FileStream stream = File.Open(_importfile.FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader _excel = ExcelReaderFactory.CreateReader(stream);

                int _rowCount = _excel.RowCount;
                Progress.RenameProgressBar(Resources.DesignImport, _rowCount);

                debug.ToFile("Processing input file", DebugLevels.Development, DebugMessageType.Info);
                int _columnCount = 0;
                bool _checkPin = SettingsDesignInput.Default.ColumnPin >= 0;
                bool _checkChannel = SettingsDesignInput.Default.ColumnChannel >= 0;

                _columnCount = _excel.FieldCount;
                string [,] _inputData = new string[_rowCount, _columnCount+1];
                //read all excel rows
                for (int _row = 1; _row <= _rowCount; _row++)
                {
                    // if nothing to read exit
                    if (!_excel.Read())
                        break;

                    //first column is row number
                    _inputData[_row - 1, 0] = AddZeroes(_row);
                    for (int _column = 0; _column < _columnCount; _column++)
                        _inputData[_row-1,_column+1] = ReadExcelCell(_row, _column, _columnCount, _excel);

                    Progress.UpdateProgressBar(_row);
                }
                debug.ToFile("Processing input file - finished", DebugLevels.Development, DebugMessageType.Info);
                Progress.HideProgressBar();
                _excel.Close();

                DesignInputData _designInputData = new DesignInputData(_inputData);
                _designInputData.ShowDialog();

                InitExcelColumnsList();

                int _columnNumber = 0;
                int _maxColumns = _inputData.GetLength(1);
                string _cellValue = "";
                string _ColumnName = "";

                UpdateColumnNumbers(ExcelColumns.Columns);

                _rowCount = _inputData.GetLength(0);
                Progress.RenameProgressBar(Resources.DesignImport, _rowCount);
                debug.ToFile("Extracting data from input file", DebugLevels.Development, DebugMessageType.Info);

                for (int _row = SettingsDesignInput.Default.RowOffset; _row < _rowCount; _row++)
                {
                    //create signal and add coresponding Columns to each signal element
                    DesignSignal _signalNew = new DesignSignal();
                    foreach (GeneralColumn _column in ExcelColumns)
                    {
                        _columnNumber = _column.Number;
                        if (_columnNumber != -1 && _columnNumber < _maxColumns)
                        {
                            _cellValue = _inputData[_row,_columnNumber];
                            _ColumnName = _column.Keyword;

                            _signalNew.SetValueFromString(_cellValue, _ColumnName);
                        }
                    }

                    // if signal is valid add to list
                    if (_signalNew.ValidateSignal())
                    {
                        // if signal has useful data add to list
                        if (_signalNew.ExtractUseful(_checkPin, _checkChannel))
                            Signals.Add(_signalNew);
                    }
                    Progress.UpdateProgressBar(_row);
                }

                debug.ToFile("Extracting data from input file - finished", DebugLevels.Development, DebugMessageType.Info);
                Progress.HideProgressBar();
            }
            else
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return false;
            }
            return true;
        }
    }
}
