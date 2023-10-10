using ExcelDataReader;
using IO_list_automation_new.Properties;
using SharpCompress.Common;
using SharpCompress.Readers.Zip;
using SwiftExcel;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private string CPU { get; set; }
        private string KKS { get; set; }
        private string RangeMin { get; set; }
        private string RangeMax { get; set; }
        private string Units { get; set; }
        private string FalseText { get; set; }
        private string TrueText { get; set; }
        private string Revision { get; set; }
        private string Cable { get; set; }
        private string Cabinet { get; set; }
        private string ModuleName { get; set; }
        private string Pin { get; set; }
        private string Channel { get; set; }
        private string IOText { get; set; }
        private string Page { get; set; }
        private string Changed { get; set; }
        private string Terminal { get; set; }

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
                case Const.ColumnNameID:
                    SetID(value);
                    break;
                case Const.ColumnNameCPU:
                    CPU = value;
                    break;
                case Const.ColumnNameKKS:
                    KKS = value;
                    break;
                case Const.ColumnNameRangeMin:
                    RangeMin = value;
                    break;
                case Const.ColumnNameRangeMax:
                    RangeMax = value;
                    break;
                case Const.ColumnNameUnits:
                    Units = value;
                    break;
                case Const.ColumnNameFalseText:
                    FalseText = value;
                    break;
                case Const.ColumnNameTrueText:
                    TrueText = value;
                    break;
                case Const.ColumnNameRevision:
                    Revision = value;
                    break;
                case Const.ColumnNameCable:
                    Cable = value;
                    break;
                case Const.ColumnNameCabinet:
                    Cabinet = value;
                    break;
                case Const.ColumnNameModuleName:
                    ModuleName = value;
                    break;
                case Const.ColumnNamePin:
                    Pin = value;
                    break;
                case Const.ColumnNameChannel:
                    Channel = value;
                    break;
                case Const.ColumnNameIOText:
                    IOText = value;
                    break;
                case Const.ColumnNamePage:
                    Page = value;
                    break;
                case Const.ColumnNameChanged:
                    Changed = value;
                    break;
                case Const.ColumnNameTerminal:
                    Terminal = value;
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
                case Const.ColumnNameID:
                    _returnValue = GetID();
                    break;
                case Const.ColumnNameCPU:
                    _returnValue = CPU;
                    break;
                case Const.ColumnNameKKS:
                    _returnValue = KKS;
                    break;
                case Const.ColumnNameRangeMin:
                    _returnValue = RangeMin;
                    break;
                case Const.ColumnNameRangeMax:
                    _returnValue = RangeMax;
                    break;
                case Const.ColumnNameUnits:
                    _returnValue = Units;
                    break;
                case Const.ColumnNameFalseText:
                    _returnValue = FalseText;
                    break;
                case Const.ColumnNameTrueText:
                    _returnValue = TrueText;
                    break;
                case Const.ColumnNameRevision:
                    _returnValue = Revision;
                    break;
                case Const.ColumnNameCable:
                    _returnValue = Cable;
                    break;
                case Const.ColumnNameCabinet:
                    _returnValue = Cabinet;
                    break;
                case Const.ColumnNameModuleName:
                    _returnValue = ModuleName;
                    break;
                case Const.ColumnNamePin:
                    _returnValue = Pin;
                    break;
                case Const.ColumnNameChannel:
                    _returnValue = Channel;
                    break;
                case Const.ColumnNameIOText:
                    _returnValue = IOText;
                    break;
                case Const.ColumnNamePage:
                    _returnValue = Page;
                    break;
                case Const.ColumnNameChanged:
                    _returnValue = Changed;
                    break;
                case Const.ColumnNameTerminal:
                    _returnValue = Terminal;
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
                if (Settings.Default.InputDataPinIsNumber)
                {
                    if (!int.TryParse(Pin, out int _value))
                        _returnValue = false;
                }
                else if (Settings.Default.InputDataPinHasNumber)
                {
                    if (!HasNumber(Pin))
                        _returnValue = false;
                }
            }

            if (_checkChannel && _returnValue)
            {
                if (Settings.Default.InputDataChannelIsNumber)
                {
                    if (!int.TryParse(Channel, out int _value))
                        _returnValue = false;
                }
                else if (Settings.Default.InputDataChannelHasNumber)
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

        public override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(Const.ColumnNameID, Settings.Default.DesignColumnID));
            _columns.Add(new GeneralColumn(Const.ColumnNameCPU, Settings.Default.DesignColumnCPU));
            _columns.Add(new GeneralColumn(Const.ColumnNameKKS, Settings.Default.DesignColumnKKS));
            _columns.Add(new GeneralColumn(Const.ColumnNameRangeMin, Settings.Default.DesignColumnRangeMin));
            _columns.Add(new GeneralColumn(Const.ColumnNameRangeMax, Settings.Default.DesignColumnRangeMax));
            _columns.Add(new GeneralColumn(Const.ColumnNameUnits, Settings.Default.DesignColumnUnits));
            _columns.Add(new GeneralColumn(Const.ColumnNameFalseText, Settings.Default.DesignColumnFalseText));
            _columns.Add(new GeneralColumn(Const.ColumnNameTrueText, Settings.Default.DesignColumnTrueText));
            _columns.Add(new GeneralColumn(Const.ColumnNameRevision, Settings.Default.DesignColumnRevision));
            _columns.Add(new GeneralColumn(Const.ColumnNameCable, Settings.Default.DesignColumnCable));
            _columns.Add(new GeneralColumn(Const.ColumnNameCabinet, Settings.Default.DesignColumnCabinet));
            _columns.Add(new GeneralColumn(Const.ColumnNameModuleName, Settings.Default.DesignColumnModuleName));
            _columns.Add(new GeneralColumn(Const.ColumnNamePin, Settings.Default.DesignColumnPin));
            _columns.Add(new GeneralColumn(Const.ColumnNameChannel, Settings.Default.DesignColumnChannel));
            _columns.Add(new GeneralColumn(Const.ColumnNameIOText, Settings.Default.DesignColumnIOText));
            _columns.Add(new GeneralColumn(Const.ColumnNamePage, Settings.Default.DesignColumnPage));
            _columns.Add(new GeneralColumn(Const.ColumnNameChanged, Settings.Default.DesignColumnChanged));
            _columns.Add(new GeneralColumn(Const.ColumnNameTerminal, Settings.Default.DesignColumnTerminal));

            return _columns;
        }

        public override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            Settings.Default.DesignColumnID = _columns.GetColumnNumberFromKeyword(Const.ColumnNameID);
            Settings.Default.DesignColumnCPU = _columns.GetColumnNumberFromKeyword(Const.ColumnNameCPU);
            Settings.Default.DesignColumnKKS = _columns.GetColumnNumberFromKeyword(Const.ColumnNameKKS);
            Settings.Default.DesignColumnRangeMin = _columns.GetColumnNumberFromKeyword(Const.ColumnNameRangeMin);
            Settings.Default.DesignColumnRangeMax = _columns.GetColumnNumberFromKeyword(Const.ColumnNameRangeMax);
            Settings.Default.DesignColumnUnits = _columns.GetColumnNumberFromKeyword(Const.ColumnNameUnits);
            Settings.Default.DesignColumnFalseText = _columns.GetColumnNumberFromKeyword(Const.ColumnNameFalseText);
            Settings.Default.DesignColumnTrueText = _columns.GetColumnNumberFromKeyword(Const.ColumnNameTrueText);
            Settings.Default.DesignColumnRevision = _columns.GetColumnNumberFromKeyword(Const.ColumnNameRevision);
            Settings.Default.DesignColumnCable = _columns.GetColumnNumberFromKeyword(Const.ColumnNameCable);
            Settings.Default.DesignColumnCabinet = _columns.GetColumnNumberFromKeyword(Const.ColumnNameCabinet);
            Settings.Default.DesignColumnModuleName = _columns.GetColumnNumberFromKeyword(Const.ColumnNameModuleName);
            Settings.Default.DesignColumnPin = _columns.GetColumnNumberFromKeyword(Const.ColumnNamePin);
            Settings.Default.DesignColumnChannel = _columns.GetColumnNumberFromKeyword(Const.ColumnNameChannel);
            Settings.Default.DesignColumnIOText = _columns.GetColumnNumberFromKeyword(Const.ColumnNameIOText);
            Settings.Default.DesignColumnPage = _columns.GetColumnNumberFromKeyword(Const.ColumnNamePage);
            Settings.Default.DesignColumnChanged = _columns.GetColumnNumberFromKeyword(Const.ColumnNameChanged);
            Settings.Default.DesignColumnTerminal = _columns.GetColumnNumberFromKeyword(Const.ColumnNameTerminal);

            Settings.Default.Save();
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

            ExcelColumns.CopyColumnList(list, columnsFromZero);
        }
        public void InitExcelColumnsList()
        {
            List<GeneralColumn> _excelColumn = new List<GeneralColumn>();
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameID, Settings.Default.ExcelColumnID));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameCPU, Settings.Default.ExcelColumnCPU));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameKKS, Settings.Default.ExcelColumnKKS));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameRangeMin, Settings.Default.ExcelColumnRangeMin));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameRangeMax, Settings.Default.ExcelColumnRangeMax));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameUnits, Settings.Default.ExcelColumnUnits));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameFalseText, Settings.Default.ExcelColumnFalseText));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameTrueText, Settings.Default.ExcelColumnTrueText));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameRevision, Settings.Default.ExcelColumnRevision));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameCable, Settings.Default.ExcelColumnCable));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameCabinet, Settings.Default.ExcelColumnCabinet));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameModuleName, Settings.Default.ExcelColumnModuleName));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNamePin, Settings.Default.ExcelColumnPin));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameChannel, Settings.Default.ExcelColumnChannel));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameIOText, Settings.Default.ExcelColumnIOText));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNamePage, Settings.Default.ExcelColumnPage));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameChanged, Settings.Default.ExcelColumnChanged));
            _excelColumn.Add(new GeneralColumn(Const.ColumnNameTerminal, Settings.Default.ExcelColumnTerminal));

            SetExcelColumnList(_excelColumn, false);
        }

        public DesignClass(ProgressIndication progress, DataGridView grid) : base(Const.DesignName, progress, grid)
        {
            ExcelColumns = new ColumnList();
            InitExcelColumnsList();
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
                Progress.RenameProgressBar(Resources.ProgressBarDesignImport, _rowCount);

                debug.ToFile("Processing input file", DebugLevels.Development, DebugMessageType.Info);
                int _columnNumber = 0;
                int _columnCount = 0;
                string _cellValue = "";
                string _ColumnName = "";
                bool _checkPin = Settings.Default.ExcelColumnPin >= 0;
                bool _checkChannel = Settings.Default.ExcelColumnChannel >= 0;
                //read all excel rows
                for (int _row = 1; _row <= _rowCount; _row++)
                {
                    // if nothing to read exit
                    if (!_excel.Read())
                        break;

                    _columnCount = _excel.FieldCount;
                    if (_row >= Settings.Default.ExcelRowOffset)
                    {
                        //create signal and add coresponding Columns to each signal element
                        DesignSignal _signalNew = new DesignSignal();
                        foreach (GeneralColumn Column in ExcelColumns)
                        {
                            _columnNumber = Column.GetColumnNumber();
                            if (_columnNumber != -1 && _columnNumber < _excel.FieldCount)
                            {
                                _cellValue = ReadExcelCell(_row, _columnNumber, _columnCount, _excel);
                                _ColumnName = Column.GetColumnKeyword();

                                //convert null to ""
                                if (_cellValue == null)
                                    _cellValue = string.Empty;

                                _signalNew.SetValueFromString(_cellValue, _ColumnName);
                            }
                        }

                        // if signal is valid add to list
                        if (_signalNew.ValidateSignal())
                        {
                            // if signal has useful data add to list
                            if (_signalNew.ExtractUseful(_checkPin, _checkChannel))
                            {
                                // if id is not extracted from file take row of line
                                if (Settings.Default.ExcelColumnID == -1)
                                    _signalNew.SetValueFromString(_row.ToString(), Const.ColumnNameID);
                                Signals.Add(_signalNew);
                            }
                        }
                    }
                    Progress.UpdateProgressBar(_row);
                }
                debug.ToFile("Processing input file - finished", DebugLevels.Development, DebugMessageType.Info);
                Progress.HideProgressBar();
                _excel.Close();
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
