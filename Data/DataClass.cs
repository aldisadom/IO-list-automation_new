using IO_list_automation_new.Data;
using IO_list_automation_new.Design;
using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class DataClass : GeneralClass<DataSignal>
    {
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
            columns.Columns.Add(KeywordColumn.Operative, new GeneralColumnParameters(17, true));
            columns.Columns.Add(KeywordColumn.KKSPlant, new GeneralColumnParameters(18, true));
            columns.Columns.Add(KeywordColumn.KKSLocation, new GeneralColumnParameters(19, true));
            columns.Columns.Add(KeywordColumn.KKSDevice, new GeneralColumnParameters(20, true));
            columns.Columns.Add(KeywordColumn.KKSFunction, new GeneralColumnParameters(21, true));
            columns.Columns.Add(KeywordColumn.Used, new GeneralColumnParameters(22, false));
            columns.Columns.Add(KeywordColumn.ObjectType, new GeneralColumnParameters(23, true));
            columns.Columns.Add(KeywordColumn.Function, new GeneralColumnParameters(24, false));
            columns.Columns.Add(KeywordColumn.Terminal, new GeneralColumnParameters(25, true));
            columns.Columns.Add(KeywordColumn.Tag, new GeneralColumnParameters(26, false));

            return columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList columns = Columns;

            SettingsData.Default.ColumnID = columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsData.Default.ColumnCPU = columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsData.Default.ColumnKKS = columns.GetColumnNumberFromKeyword(KeywordColumn.KKS);
            SettingsData.Default.ColumnRangeMin = columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMin);
            SettingsData.Default.ColumnRangeMax = columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMax);
            SettingsData.Default.ColumnUnits = columns.GetColumnNumberFromKeyword(KeywordColumn.Units);
            SettingsData.Default.ColumnFalseText = columns.GetColumnNumberFromKeyword(KeywordColumn.FalseText);
            SettingsData.Default.ColumnTrueText = columns.GetColumnNumberFromKeyword(KeywordColumn.TrueText);
            SettingsData.Default.ColumnRevision = columns.GetColumnNumberFromKeyword(KeywordColumn.Revision);
            SettingsData.Default.ColumnCable = columns.GetColumnNumberFromKeyword(KeywordColumn.Cable);
            SettingsData.Default.ColumnCabinet = columns.GetColumnNumberFromKeyword(KeywordColumn.Cabinet);
            SettingsData.Default.ColumnModuleName = columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleName);
            SettingsData.Default.ColumnPin = columns.GetColumnNumberFromKeyword(KeywordColumn.Pin);
            SettingsData.Default.ColumnChannel = columns.GetColumnNumberFromKeyword(KeywordColumn.Channel);
            SettingsData.Default.ColumnIOText = columns.GetColumnNumberFromKeyword(KeywordColumn.IOText);
            SettingsData.Default.ColumnPage = columns.GetColumnNumberFromKeyword(KeywordColumn.Page);
            SettingsData.Default.ColumnChanged = columns.GetColumnNumberFromKeyword(KeywordColumn.Changed);
            SettingsData.Default.ColumnKKSPlant = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSPlant);
            SettingsData.Default.ColumnKKSLocation = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSLocation);
            SettingsData.Default.ColumnKKSDevice = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSDevice);
            SettingsData.Default.ColumnKKSFunction = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSFunction);
            SettingsData.Default.ColumnUsed = columns.GetColumnNumberFromKeyword(KeywordColumn.Used);
            SettingsData.Default.ColumnObjectType = columns.GetColumnNumberFromKeyword(KeywordColumn.ObjectType);
            SettingsData.Default.ColumnFunction = columns.GetColumnNumberFromKeyword(KeywordColumn.Function);
            SettingsData.Default.ColumnTerminal = columns.GetColumnNumberFromKeyword(KeywordColumn.Terminal);
            SettingsData.Default.ColumnTag = columns.GetColumnNumberFromKeyword(KeywordColumn.Tag);

            SettingsData.Default.Save();
        }

        public DataClass(ProgressIndication progress, DataGridView grid) : base("Data", nameof(FileExtensions.data), progress, grid, true)
        {
        }

        public DataClass() : base("Data", nameof(FileExtensions.data), null, null, true)
        {
        }

        /// <summary>
        /// Extract data from design data
        /// </summary>
        /// <param name="design">design data</param>
        public void ExtractFromDesign(DesignClass design)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ExtractDataFromDesign, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ExtractDataFromDesign, design.Signals.Count);

            UpdateColumnNumbers(design.BaseColumns);

            Signals.Clear();
            design.Grid.GetData();
            string keyword;

            foreach (DesignSignal designSignal in design.Signals)
            {
                DataSignal dataSignal = new DataSignal();

                // go through all column in design and send it to signals
                foreach (var column in design.Columns.Columns)
                {
                    keyword = column.Key;
                    dataSignal.SetValueFromString(designSignal.GetValueString(keyword, true), keyword);
                }
                dataSignal.FindKKSInSignal(false);
                dataSignal.KKSDecode();
                dataSignal.ExtractNumberFromChannel();

                Signals.Add(dataSignal);
                Progress.UpdateProgressBar(Signals.Count);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ExtractDataFromDesign + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }

        /// <summary>
        /// Create KKS from 4 KKS parts
        /// </summary>
        public void MakeKKS()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.KKSCombine, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.KKSCombine, Signals.Count);

            KKSEdit kksEdit = new KKSEdit();

            int progress = 0;
            foreach (DataSignal dataSignal in Signals)
            {
                if (!dataSignal.HasKKS())
                    continue;

                //transfer KKS to KKS edit form
                kksEdit.UpdateKKS(dataSignal.KKS, dataSignal.KKSPlant, dataSignal.KKSLocation, dataSignal.KKSDevice, dataSignal.KKSFunction);

                //not configured
                if (kksEdit.Configured == 0)
                {
                    kksEdit.ShowDialog();
                    //canceled edit
                    if (kksEdit.Configured == -1)
                    {
                        debug.ToFile(Resources.KKSCombine + " - " + Resources.Canceled, DebugLevels.Minimum, DebugMessageType.Info);
                        break;
                    }
                }
                dataSignal.SetValueFromString(kksEdit.GetCombined(), KeywordColumn.KKS);
                progress++;
                Progress.UpdateProgressBar(progress);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.KKSCombine + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }
    }
}