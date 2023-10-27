using ExcelDataReader;
using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
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
        public string UniqueKKS { get { return KKS + "_" + CPU; } }
        public string UniqueModuleName { get { return Cabinet + "_" + ModuleName + "_" + CPU; } }

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
                case KeywordColumn.ID:
                    ID = value;
                    break;
                case KeywordColumn.CPU:
                    CPU = value;
                    break;
                case KeywordColumn.KKS:
                    KKS = value;
                    break;
                case KeywordColumn.RangeMin:
                    RangeMin = value;
                    break;
                case KeywordColumn.RangeMax:
                    RangeMax = value;
                    break;
                case KeywordColumn.Units:
                    Units = value;
                    break;
                case KeywordColumn.FalseText:
                    FalseText = value;
                    break;
                case KeywordColumn.TrueText:
                    TrueText = value;
                    break;
                case KeywordColumn.Revision:
                    Revision = value;
                    break;
                case KeywordColumn.Cable:
                    Cable = value;
                    break;
                case KeywordColumn.Cabinet:
                    Cabinet = value;
                    break;
                case KeywordColumn.ModuleName:
                    ModuleName = value;
                    break;
                case KeywordColumn.Pin:
                    Pin = value;
                    break;
                case KeywordColumn.Channel:
                    Channel = value;
                    break;
                case KeywordColumn.IOText:
                    IOText = value;
                    break;
                case KeywordColumn.Page:
                    Page = value;
                    break;
                case KeywordColumn.Changed:
                    Changed = value;
                    break;
                case KeywordColumn.Terminal:
                    Terminal = value;
                    break;
                case KeywordColumn.Tag:
                    Tag = value;
                    break;
            }
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="suppressError">suppress alarm message, used only for transferring from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public override string GetValueString(string parameterName, bool suppressError)
        {
            string _returnValue = string.Empty;
            switch (parameterName)
            {
                case KeywordColumn.ID:
                    _returnValue = ID;
                    break;
                case KeywordColumn.CPU:
                    _returnValue = CPU;
                    break;
                case KeywordColumn.KKS:
                    _returnValue = KKS;
                    break;
                case KeywordColumn.RangeMin:
                    _returnValue = RangeMin;
                    break;
                case KeywordColumn.RangeMax:
                    _returnValue = RangeMax;
                    break;
                case KeywordColumn.Units:
                    _returnValue = Units;
                    break;
                case KeywordColumn.FalseText:
                    _returnValue = FalseText;
                    break;
                case KeywordColumn.TrueText:
                    _returnValue = TrueText;
                    break;
                case KeywordColumn.Revision:
                    _returnValue = Revision;
                    break;
                case KeywordColumn.Cable:
                    _returnValue = Cable;
                    break;
                case KeywordColumn.Cabinet:
                    _returnValue = Cabinet;
                    break;
                case KeywordColumn.ModuleName:
                    _returnValue = ModuleName;
                    break;
                case KeywordColumn.Pin:
                    _returnValue = Pin;
                    break;
                case KeywordColumn.Channel:
                    _returnValue = Channel;
                    break;
                case KeywordColumn.IOText:
                    _returnValue = IOText;
                    break;
                case KeywordColumn.Page:
                    _returnValue = Page;
                    break;
                case KeywordColumn.Changed:
                    _returnValue = Changed;
                    break;
                case KeywordColumn.Terminal:
                    _returnValue = Terminal;
                    break;
                case KeywordColumn.Tag:
                    _returnValue = Tag;
                    break;
                default:
                    if (!suppressError)
                    {
                        Debug _debug = new Debug();
                        _debug.ToPopUp("DesignSignal.GetValueString " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    }
                    break;
            }
            return _returnValue;
        }

        /// <summary>
        /// Checks if design signal is valid:
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            if (string.IsNullOrEmpty(ModuleName))
                return false;
            else if (string.IsNullOrEmpty(IOText))
                return false;
            else if (string.IsNullOrEmpty(Pin) && !string.IsNullOrEmpty(Channel))
                return false;

            return true;
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
        /// <returns>has useful data</returns>
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
            List<GeneralColumn> _columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsDesign.Default.ColumnID,false),
                new GeneralColumn(KeywordColumn.CPU, SettingsDesign.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.KKS, SettingsDesign.Default.ColumnKKS, true),
                new GeneralColumn(KeywordColumn.RangeMin, SettingsDesign.Default.ColumnRangeMin, true),
                new GeneralColumn(KeywordColumn.RangeMax, SettingsDesign.Default.ColumnRangeMax, true),
                new GeneralColumn(KeywordColumn.Units, SettingsDesign.Default.ColumnUnits, true),
                new GeneralColumn(KeywordColumn.FalseText, SettingsDesign.Default.ColumnFalseText, true),
                new GeneralColumn(KeywordColumn.TrueText, SettingsDesign.Default.ColumnTrueText, true),
                new GeneralColumn(KeywordColumn.Revision, SettingsDesign.Default.ColumnRevision, true),
                new GeneralColumn(KeywordColumn.Cable, SettingsDesign.Default.ColumnCable, true),
                new GeneralColumn(KeywordColumn.Cabinet, SettingsDesign.Default.ColumnCabinet, false),
                new GeneralColumn(KeywordColumn.ModuleName, SettingsDesign.Default.ColumnModuleName, false),
                new GeneralColumn(KeywordColumn.Pin, SettingsDesign.Default.ColumnPin, false),
                new GeneralColumn(KeywordColumn.Channel, SettingsDesign.Default.ColumnChannel, false),
                new GeneralColumn(KeywordColumn.IOText, SettingsDesign.Default.ColumnIOText, false),
                new GeneralColumn(KeywordColumn.Page, SettingsDesign.Default.ColumnPage, true),
                new GeneralColumn(KeywordColumn.Changed, SettingsDesign.Default.ColumnChanged, true),
                new GeneralColumn(KeywordColumn.Terminal, SettingsDesign.Default.ColumnTerminal, true),
                new GeneralColumn(KeywordColumn.Tag, SettingsDesign.Default.ColumnTag, true),
            };

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            SettingsDesign.Default.ColumnID = _columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsDesign.Default.ColumnCPU = _columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsDesign.Default.ColumnKKS = _columns.GetColumnNumberFromKeyword(KeywordColumn.KKS);
            SettingsDesign.Default.ColumnRangeMin = _columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMin);
            SettingsDesign.Default.ColumnRangeMax = _columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMax);
            SettingsDesign.Default.ColumnUnits = _columns.GetColumnNumberFromKeyword(KeywordColumn.Units);
            SettingsDesign.Default.ColumnFalseText = _columns.GetColumnNumberFromKeyword(KeywordColumn.FalseText);
            SettingsDesign.Default.ColumnTrueText = _columns.GetColumnNumberFromKeyword(KeywordColumn.TrueText);
            SettingsDesign.Default.ColumnRevision = _columns.GetColumnNumberFromKeyword(KeywordColumn.Revision);
            SettingsDesign.Default.ColumnCable = _columns.GetColumnNumberFromKeyword(KeywordColumn.Cable);
            SettingsDesign.Default.ColumnCabinet = _columns.GetColumnNumberFromKeyword(KeywordColumn.Cabinet);
            SettingsDesign.Default.ColumnModuleName = _columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleName);
            SettingsDesign.Default.ColumnPin = _columns.GetColumnNumberFromKeyword(KeywordColumn.Pin);
            SettingsDesign.Default.ColumnChannel = _columns.GetColumnNumberFromKeyword(KeywordColumn.Channel);
            SettingsDesign.Default.ColumnIOText = _columns.GetColumnNumberFromKeyword(KeywordColumn.IOText);
            SettingsDesign.Default.ColumnPage = _columns.GetColumnNumberFromKeyword(KeywordColumn.Page);
            SettingsDesign.Default.ColumnChanged = _columns.GetColumnNumberFromKeyword(KeywordColumn.Changed);
            SettingsDesign.Default.ColumnTerminal = _columns.GetColumnNumberFromKeyword(KeywordColumn.Terminal);
            SettingsDesign.Default.ColumnTag = _columns.GetColumnNumberFromKeyword(KeywordColumn.Tag);

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
            List<GeneralColumn> _excelColumn = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsDesignInput.Default.ColumnID, true),
                new GeneralColumn(KeywordColumn.CPU, SettingsDesignInput.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.KKS, SettingsDesignInput.Default.ColumnKKS, true),
                new GeneralColumn(KeywordColumn.RangeMin, SettingsDesignInput.Default.ColumnRangeMin, true),
                new GeneralColumn(KeywordColumn.RangeMax, SettingsDesignInput.Default.ColumnRangeMax, true),
                new GeneralColumn(KeywordColumn.Units, SettingsDesignInput.Default.ColumnUnits, true),
                new GeneralColumn(KeywordColumn.FalseText, SettingsDesignInput.Default.ColumnFalseText, true),
                new GeneralColumn(KeywordColumn.TrueText, SettingsDesignInput.Default.ColumnTrueText, true),
                new GeneralColumn(KeywordColumn.Revision, SettingsDesignInput.Default.ColumnRevision, true),
                new GeneralColumn(KeywordColumn.Cable, SettingsDesignInput.Default.ColumnCable, true),
                new GeneralColumn(KeywordColumn.Cabinet, SettingsDesignInput.Default.ColumnCabinet, true),
                new GeneralColumn(KeywordColumn.ModuleName, SettingsDesignInput.Default.ColumnModuleName, false),
                new GeneralColumn(KeywordColumn.Pin, SettingsDesignInput.Default.ColumnPin, false),
                new GeneralColumn(KeywordColumn.Channel, SettingsDesignInput.Default.ColumnChannel, false),
                new GeneralColumn(KeywordColumn.IOText, SettingsDesignInput.Default.ColumnIOText, false),
                new GeneralColumn(KeywordColumn.Page, SettingsDesignInput.Default.ColumnPage, true),
                new GeneralColumn(KeywordColumn.Changed, SettingsDesignInput.Default.ColumnChanged, true),
                new GeneralColumn(KeywordColumn.Terminal, SettingsDesignInput.Default.ColumnTerminal, true),
                new GeneralColumn(KeywordColumn.Tag, SettingsDesignInput.Default.ColumnTag, true),
            };

            SetExcelColumnList(_excelColumn, false);
        }

        public DesignClass(ProgressIndication progress, DataGridView grid) : base("Design", false, nameof(FileExtensions.design), progress, grid)
        {
            ExcelColumns = new ColumnList();
        }

        /// <summary>
        /// Get data from excel file and get relevant signals
        /// </summary>
        /// <returns>successful data read</returns>
        public bool GetDataFromImportFile()
        {
            Debug debug = new Debug();

            //crate open file dialog to open excel files only
            OpenFileDialog _importFile = new OpenFileDialog();
            _importFile.Filter = "Excel Worksheets|*.xls;*.xlsx";
            if (_importFile.ShowDialog() == DialogResult.OK)
            {
                debug.ToFile("Excel file for design is: " + _importFile.FileName, DebugLevels.Development, DebugMessageType.Info);
                //open excel file
                FileStream stream = File.Open(_importFile.FileName, FileMode.Open, FileAccess.Read);
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
                    _inputData[_row - 1, 0] = GeneralFunctions.AddZeroes(_row);
                    for (int _column = 0; _column < _columnCount; _column++)
                        _inputData[_row-1,_column+1] = GeneralFunctions.ReadExcelCell(_row, _column, _columnCount, _excel);

                    Progress.UpdateProgressBar(_row);
                }
                debug.ToFile("Processing input file - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
                Progress.HideProgressBar();
                _excel.Close();

                DesignInputData _designInputData = new DesignInputData(_inputData);
                _designInputData.ShowDialog();

                InitExcelColumnsList();

                int _columnNumber = 0;
                int _maxColumns = _inputData.GetLength(1);
                string _cellValue = string.Empty;
                string _ColumnName = string.Empty;

                UpdateColumnNumbers(ExcelColumns.Columns);

                _rowCount = _inputData.GetLength(0);
                Progress.RenameProgressBar(Resources.DesignImport, _rowCount);
                debug.ToFile("Extracting data from input file", DebugLevels.Development, DebugMessageType.Info);

                for (int _row = SettingsDesignInput.Default.RowOffset; _row < _rowCount; _row++)
                {
                    //create signal and add corresponding Columns to each signal element
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

                debug.ToFile("Extracting data from input file - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
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
