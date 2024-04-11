using IO_list_automation_new.Design;
using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace IO_list_automation_new
{

    internal class DesignClass : GeneralClass<DesignSignal>
    {
        //columns in excel
        private ColumnList ExcelColumns { get; }

        protected override ColumnList GeneralGenerateColumnsList()
        {
            ColumnList columns = new ColumnList();

            columns.Columns.Add(KeywordColumn.ID, new GeneralColumnParameters(0, false));
            columns.Columns.Add(KeywordColumn.CPU, new GeneralColumnParameters(1, true));
            columns.Columns.Add(KeywordColumn.KKS, new GeneralColumnParameters(2, false));
            columns.Columns.Add(KeywordColumn.RangeMin, new GeneralColumnParameters(3, true));
            columns.Columns.Add(KeywordColumn.RangeMax, new GeneralColumnParameters(4, true));
            columns.Columns.Add(KeywordColumn.Units, new GeneralColumnParameters(5, true));
            columns.Columns.Add(KeywordColumn.FalseText, new GeneralColumnParameters(6, true));
            columns.Columns.Add(KeywordColumn.TrueText, new GeneralColumnParameters(7, true));
            columns.Columns.Add(KeywordColumn.Revision, new GeneralColumnParameters(8, true));
            columns.Columns.Add(KeywordColumn.Cable, new GeneralColumnParameters(9, true));
            columns.Columns.Add(KeywordColumn.Cabinet, new GeneralColumnParameters(10, false));
            columns.Columns.Add(KeywordColumn.ModuleName, new GeneralColumnParameters(11, false));
            columns.Columns.Add(KeywordColumn.Pin, new GeneralColumnParameters(12, true));
            columns.Columns.Add(KeywordColumn.Channel, new GeneralColumnParameters(13, false));
            columns.Columns.Add(KeywordColumn.IOText, new GeneralColumnParameters(14, false));
            columns.Columns.Add(KeywordColumn.Page, new GeneralColumnParameters(15, true));
            columns.Columns.Add(KeywordColumn.Changed, new GeneralColumnParameters(16, true));
            columns.Columns.Add(KeywordColumn.Terminal, new GeneralColumnParameters(17, true));
            columns.Columns.Add(KeywordColumn.Tag, new GeneralColumnParameters(18, false));

            return columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList columns = Columns;

            SettingsDesign.Default.ColumnID = columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsDesign.Default.ColumnCPU = columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsDesign.Default.ColumnKKS = columns.GetColumnNumberFromKeyword(KeywordColumn.KKS);
            SettingsDesign.Default.ColumnRangeMin = columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMin);
            SettingsDesign.Default.ColumnRangeMax = columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMax);
            SettingsDesign.Default.ColumnUnits = columns.GetColumnNumberFromKeyword(KeywordColumn.Units);
            SettingsDesign.Default.ColumnFalseText = columns.GetColumnNumberFromKeyword(KeywordColumn.FalseText);
            SettingsDesign.Default.ColumnTrueText = columns.GetColumnNumberFromKeyword(KeywordColumn.TrueText);
            SettingsDesign.Default.ColumnRevision = columns.GetColumnNumberFromKeyword(KeywordColumn.Revision);
            SettingsDesign.Default.ColumnCable = columns.GetColumnNumberFromKeyword(KeywordColumn.Cable);
            SettingsDesign.Default.ColumnCabinet = columns.GetColumnNumberFromKeyword(KeywordColumn.Cabinet);
            SettingsDesign.Default.ColumnModuleName = columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleName);
            SettingsDesign.Default.ColumnPin = columns.GetColumnNumberFromKeyword(KeywordColumn.Pin);
            SettingsDesign.Default.ColumnChannel = columns.GetColumnNumberFromKeyword(KeywordColumn.Channel);
            SettingsDesign.Default.ColumnIOText = columns.GetColumnNumberFromKeyword(KeywordColumn.IOText);
            SettingsDesign.Default.ColumnPage = columns.GetColumnNumberFromKeyword(KeywordColumn.Page);
            SettingsDesign.Default.ColumnChanged = columns.GetColumnNumberFromKeyword(KeywordColumn.Changed);
            SettingsDesign.Default.ColumnTerminal = columns.GetColumnNumberFromKeyword(KeywordColumn.Terminal);
            SettingsDesign.Default.ColumnTag = columns.GetColumnNumberFromKeyword(KeywordColumn.Tag);

            SettingsDesign.Default.Save();
        }

        /// <summary>
        /// copy new list to excel column list
        /// </summary>
        /// <param name="list">new GeneralColumn list</param>
        /// <param name="columnsFromZero">true, Columns start from zero</param>
        public void SetExcelColumnList(ColumnList list, bool columnsFromZero)
        {
            Debug debug = new Debug();
            debug.ToFile("Updating : " + Name + " Grid Columns", DebugLevels.Development, DebugMessageType.Info);

            ExcelColumns.SetColumns(list, columnsFromZero);
        }

        public void InitExcelColumnsList()
        {
            ColumnList excelColumns = new ColumnList();

            excelColumns.Columns.Add(KeywordColumn.ID, new GeneralColumnParameters(0, false));
            excelColumns.Columns.Add(KeywordColumn.CPU, new GeneralColumnParameters(1, true));
            excelColumns.Columns.Add(KeywordColumn.KKS, new GeneralColumnParameters(2, false));
            excelColumns.Columns.Add(KeywordColumn.RangeMin, new GeneralColumnParameters(3, true));
            excelColumns.Columns.Add(KeywordColumn.RangeMax, new GeneralColumnParameters(4, true));
            excelColumns.Columns.Add(KeywordColumn.Units, new GeneralColumnParameters(5, true));
            excelColumns.Columns.Add(KeywordColumn.FalseText, new GeneralColumnParameters(6, true));
            excelColumns.Columns.Add(KeywordColumn.TrueText, new GeneralColumnParameters(7, true));
            excelColumns.Columns.Add(KeywordColumn.Revision, new GeneralColumnParameters(8, true));
            excelColumns.Columns.Add(KeywordColumn.Cable, new GeneralColumnParameters(9, true));
            excelColumns.Columns.Add(KeywordColumn.Cabinet, new GeneralColumnParameters(10, false));
            excelColumns.Columns.Add(KeywordColumn.ModuleName, new GeneralColumnParameters(11, false));
            excelColumns.Columns.Add(KeywordColumn.Pin, new GeneralColumnParameters(12, true));
            excelColumns.Columns.Add(KeywordColumn.Channel, new GeneralColumnParameters(13, false));
            excelColumns.Columns.Add(KeywordColumn.IOText, new GeneralColumnParameters(14, false));
            excelColumns.Columns.Add(KeywordColumn.Page, new GeneralColumnParameters(15, true));
            excelColumns.Columns.Add(KeywordColumn.Changed, new GeneralColumnParameters(16, true));
            excelColumns.Columns.Add(KeywordColumn.Terminal, new GeneralColumnParameters(17, true));
            excelColumns.Columns.Add(KeywordColumn.Tag, new GeneralColumnParameters(18, false));

            SetExcelColumnList(excelColumns, false);
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
            OpenFileDialog importFile = new OpenFileDialog()
            {
                Filter = "Excel Worksheets|*.xls;*.xlsx",
            };

            if (importFile.ShowDialog() != DialogResult.OK)
            {
                debug.ToFile(Resources.FileSellectCanceled, DebugLevels.Minimum, DebugMessageType.Info);
                return false;
            }

            debug.ToFile("Excel file for design is: " + importFile.FileName, DebugLevels.High, DebugMessageType.Info);
            //open excel file
            ExcelFiles excelFile = new ExcelFiles(importFile.FileName, Path.GetExtension(importFile.FileName).Substring(1), Progress);
            DataTable data = excelFile.LoadFromFile(importFile.FileName);

            DesignInputData designInputData = new DesignInputData(data);
            designInputData.ShowDialog();

            InitExcelColumnsList();

            int columnNumber;
            string cellValue;
            string columnName;

            UpdateColumnNumbers(ExcelColumns);

            Progress.RenameProgressBar(Resources.DesignImport, data.Rows.Count);
            debug.ToFile("Extracting data from input file", DebugLevels.High, DebugMessageType.Info);

            for (int row = SettingsDesignInput.Default.RowOffset; row < data.Rows.Count; row++)
            {
                //create signal and add corresponding Columns to each signal element
                DesignSignal signalNew = new DesignSignal();
                foreach (var column in ExcelColumns.Columns)
                {
                    columnNumber = column.Value.NR;
                    if (columnNumber != -1 && columnNumber < data.Columns.Count)
                    {
                        cellValue = GeneralFunctions.GetDataTableValue(data, row, columnNumber);
                        columnName = column.Key;

                        signalNew.SetValueFromString(cellValue, columnName);
                    }
                }

                // if signal is valid add to list
                if (signalNew.ValidateSignal())
                    Signals.Add(signalNew);

                Progress.UpdateProgressBar(row);
            }

            debug.ToFile("Extracting data from input file - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
            Progress.HideProgressBar();

            return Signals.Count > 0;
        }
    }
}