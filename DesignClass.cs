using ExcelDataReader;
using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

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

        public string UniqueKKS
        { get { return KKS + "_" + CPU; } }

        public string UniqueModuleName
        { get { return Cabinet + "_" + ModuleName + "_" + CPU; } }

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
            switch (parameterName)
            {
                case KeywordColumn.ID:
                    return ID;

                case KeywordColumn.CPU:
                    return CPU;

                case KeywordColumn.KKS:
                    return KKS;

                case KeywordColumn.RangeMin:
                    return RangeMin;

                case KeywordColumn.RangeMax:
                    return RangeMax;

                case KeywordColumn.Units:
                    return Units;

                case KeywordColumn.FalseText:
                    return FalseText;

                case KeywordColumn.TrueText:
                    return TrueText;

                case KeywordColumn.Revision:
                    return Revision;

                case KeywordColumn.Cable:
                    return Cable;

                case KeywordColumn.Cabinet:
                    return Cabinet;

                case KeywordColumn.ModuleName:
                    return ModuleName;

                case KeywordColumn.Pin:
                    return Pin;

                case KeywordColumn.Channel:
                    return Channel;

                case KeywordColumn.IOText:
                    return IOText;

                case KeywordColumn.Page:
                    return Page;

                case KeywordColumn.Changed:
                    return Changed;

                case KeywordColumn.Terminal:
                    return Terminal;

                case KeywordColumn.Tag:
                    return Tag;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string _debugText = "DesignSignal.GetValueString";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + parameterName + " is not created for this element");
            }
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
            else if (string.IsNullOrEmpty(Channel))
                return false;
            else if (!HasNumber(Channel))
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
    }

    internal class DesignClass : GeneralClass<DesignSignal>
    {
        //columns in excel
        private ColumnList ExcelColumns { get; }

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

        public DesignClass(ProgressIndication progress, DataGridView grid) : base("Design", nameof(FileExtensions.design), progress, grid, true)
        {
            ExcelColumns = new ColumnList();
        }

        /// <summary>
        /// Get data from excel file and get relevant signals
        /// </summary>
        /// <returns>data is not empty</returns>
        public bool GetDataFromImportFile()
        {
            Debug debug = new Debug();

            //crate open file dialog to open excel files only
            OpenFileDialog _importFile = new OpenFileDialog()
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx",
            };

            if (_importFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return false;
            }

            debug.ToFile("Excel file for design is: " + _importFile.FileName, DebugLevels.High, DebugMessageType.Info);
            //open excel file
            ExcelFiles _excelFile = new ExcelFiles(_importFile.FileName, Path.GetExtension(_importFile.FileName).Substring(1), Progress);
            DataTable _data = _excelFile.LoadFromFile(_importFile.FileName);

            DesignInputData _designInputData = new DesignInputData(_data);
            _designInputData.ShowDialog();

            InitExcelColumnsList();

            int _columnNumber;
            string _cellValue;
            string _ColumnName;

            UpdateColumnNumbers(ExcelColumns.Columns);

            Progress.RenameProgressBar(Resources.DesignImport, _data.Rows.Count);
            debug.ToFile("Extracting data from input file", DebugLevels.High, DebugMessageType.Info);

            for (int _row = SettingsDesignInput.Default.RowOffset; _row < _data.Rows.Count; _row++)
            {
                //create signal and add corresponding Columns to each signal element
                DesignSignal _signalNew = new DesignSignal();
                foreach (GeneralColumn _column in ExcelColumns)
                {
                    _columnNumber = _column.Number;
                    if (_columnNumber != -1 && _columnNumber < _data.Columns.Count)
                    {
                        _cellValue = GeneralFunctions.GetDataTableValue(_data, _row, _columnNumber);
                        _ColumnName = _column.Keyword;

                        _signalNew.SetValueFromString(_cellValue, _ColumnName);
                    }
                }

                // if signal is valid add to list
                if (_signalNew.ValidateSignal())
                    Signals.Add(_signalNew);

                Progress.UpdateProgressBar(_row);
            }

            debug.ToFile("Extracting data from input file - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
            Progress.HideProgressBar();

            return Signals.Count > 0;
        }
    }
}