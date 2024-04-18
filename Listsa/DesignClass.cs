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
        private ColumnList ExcelColumns { get; set; }

        protected override void InitialCollumnList()
        {
            Columns = new ColumnList(Name);

            Columns.Columns.Add(KeywordColumn.ID, new ColumnParameters(0, false, false));
            Columns.Columns.Add(KeywordColumn.CPU, new ColumnParameters(1, true, false));
            Columns.Columns.Add(KeywordColumn.KKS, new ColumnParameters(2, false, false));
            Columns.Columns.Add(KeywordColumn.RangeMin, new ColumnParameters(3, true, false));
            Columns.Columns.Add(KeywordColumn.RangeMax, new ColumnParameters(4, true, false));
            Columns.Columns.Add(KeywordColumn.Units, new ColumnParameters(5, true, false));
            Columns.Columns.Add(KeywordColumn.FalseText, new ColumnParameters(6, true, false));
            Columns.Columns.Add(KeywordColumn.TrueText, new ColumnParameters(7, true, false));
            Columns.Columns.Add(KeywordColumn.Revision, new ColumnParameters(8, true, false));
            Columns.Columns.Add(KeywordColumn.Cable, new ColumnParameters(9, true, false));
            Columns.Columns.Add(KeywordColumn.Cabinet, new ColumnParameters(10, false, false));
            Columns.Columns.Add(KeywordColumn.ModuleName, new ColumnParameters(11, false, false));
            Columns.Columns.Add(KeywordColumn.Pin, new ColumnParameters(12, true, false));
            Columns.Columns.Add(KeywordColumn.Channel, new ColumnParameters(13, false, false));
            Columns.Columns.Add(KeywordColumn.IOText, new ColumnParameters(14, false, false));
            Columns.Columns.Add(KeywordColumn.Page, new ColumnParameters(15, true, false));
            Columns.Columns.Add(KeywordColumn.Changed, new ColumnParameters(16, true, false));
            Columns.Columns.Add(KeywordColumn.Terminal, new ColumnParameters(17, true, false));
            Columns.Columns.Add(KeywordColumn.Tag, new ColumnParameters(18, false, false));
        }

        public void InitExcelColumnsList()
        {
            ExcelColumns = new ColumnList(Name);

            ExcelColumns.Columns.Add(KeywordColumn.ID, new ColumnParameters(0, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.CPU, new ColumnParameters(1, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.KKS, new ColumnParameters(2, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.RangeMin, new ColumnParameters(3, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.RangeMax, new ColumnParameters(4, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Units, new ColumnParameters(5, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.FalseText, new ColumnParameters(6, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.TrueText, new ColumnParameters(7, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Revision, new ColumnParameters(8, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Cable, new ColumnParameters(9, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Cabinet, new ColumnParameters(10, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.ModuleName, new ColumnParameters(11, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Pin, new ColumnParameters(12, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Channel, new ColumnParameters(13, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.IOText, new ColumnParameters(14, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Page, new ColumnParameters(15, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Changed, new ColumnParameters(16, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Terminal, new ColumnParameters(17, false, false));
            ExcelColumns.Columns.Add(KeywordColumn.Tag, new ColumnParameters(18, false, false));
        }

        public DesignClass(ProgressIndication progress, DataGridView grid) : base("Design", nameof(FileExtensions.design), progress, grid, true)
        {
            InitExcelColumnsList();
            ExcelColumns.LoadColumnsParameters();
            ExcelColumns.SaveColumnsParameters();
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

            for (int i = 0; i < data.Columns.Count; i++)
                data.Columns[i].ColumnName = i.ToString();

            DesignInputData designInputData = new DesignInputData(data, ExcelColumns);
            designInputData.ShowDialog();

            int columnNumber;
            string cellValue;
            string columnName;

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