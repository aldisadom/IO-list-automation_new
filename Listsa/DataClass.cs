using IO_list_automation_new.Data;
using IO_list_automation_new.Design;
using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class DataClass : GeneralClass<DataSignal>
    {
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
            Columns.Columns.Add(KeywordColumn.Operative, new ColumnParameters(17, true, false));
            Columns.Columns.Add(KeywordColumn.KKSPlant, new ColumnParameters(18, true, false));
            Columns.Columns.Add(KeywordColumn.KKSLocation, new ColumnParameters(19, true, false));
            Columns.Columns.Add(KeywordColumn.KKSDevice, new ColumnParameters(20, true, false));
            Columns.Columns.Add(KeywordColumn.KKSFunction, new ColumnParameters(21, true, false));
            Columns.Columns.Add(KeywordColumn.Used, new ColumnParameters(22, false, false));
            Columns.Columns.Add(KeywordColumn.ObjectType, new ColumnParameters(23, true, false));
            Columns.Columns.Add(KeywordColumn.Function, new ColumnParameters(24, false, false));
            Columns.Columns.Add(KeywordColumn.Terminal, new ColumnParameters(25, true, false));
            Columns.Columns.Add(KeywordColumn.Tag, new ColumnParameters(26, false, false));
        }

        public DataClass(ProgressIndication progress, DataGridView grid) : base("Data", nameof(FileExtensions.data), progress, grid, true)
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