﻿using IO_list_automation_new.Data;
using IO_list_automation_new.Objects;
using IO_list_automation_new.Properties;
using System.Windows.Forms;

namespace IO_list_automation_new
{

    internal class ObjectsClass : GeneralClass<ObjectSignal>
    {
        protected override ColumnList GeneralGenerateColumnsList()
        {
            ColumnList columns = new ColumnList();

            columns.Columns.Add(KeywordColumn.ID, new GeneralColumnParameters(0, true));
            columns.Columns.Add(KeywordColumn.CPU, new GeneralColumnParameters(1, true));
            columns.Columns.Add(KeywordColumn.KKS, new GeneralColumnParameters(2, false));
            columns.Columns.Add(KeywordColumn.Operative, new GeneralColumnParameters(3, true));
            columns.Columns.Add(KeywordColumn.ObjectType, new GeneralColumnParameters(4, false));
            columns.Columns.Add(KeywordColumn.ObjectName, new GeneralColumnParameters(5, false));

            return columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList columns = Columns;

            SettingsObject.Default.ColumnID = columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsObject.Default.ColumnCPU = columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsObject.Default.ColumnKKS = columns.GetColumnNumberFromKeyword(KeywordColumn.KKS);
            SettingsObject.Default.ColumnOperative = columns.GetColumnNumberFromKeyword(KeywordColumn.Operative);
            SettingsObject.Default.ColumnObjectType = columns.GetColumnNumberFromKeyword(KeywordColumn.ObjectType);
            SettingsObject.Default.ColumnObjectName = columns.GetColumnNumberFromKeyword(KeywordColumn.ObjectName);

            SettingsObject.Default.Save();
        }

        public ObjectsClass(ProgressIndication progress, DataGridView grid) : base("Object", nameof(FileExtensions.objects), progress, grid, true)
        { }

        public ObjectsClass() : base("Object", nameof(FileExtensions.objects), null, null, true)
        { }

        /// <summary>
        /// Get unique data signals
        /// </summary>
        /// <param name="data">data signals object</param>
        public void ExtractFromData(DataClass data)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ObjectGenerateUniqueData, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ObjectGenerateUniqueData, data.Signals.Count);

            UpdateColumnNumbers(data.BaseColumns);
            Signals.Clear();
            string keyword;
            string getKeyword;
            for (int dataNumber = 0; dataNumber < data.Signals.Count; dataNumber++)
            {
                DataSignal dataSignal = data.Signals[dataNumber];

                if (!dataSignal.HasKKS())
                    continue;

                ObjectSignal objectSignal = new ObjectSignal();

                // go through all column in objects and send data to objects
                foreach (var column in Columns.Columns)
                {
                    keyword = column.Key;

                    // IOText is transferred to object Name column
                    getKeyword = keyword == KeywordColumn.ObjectName ? KeywordColumn.IOText : keyword;
                    objectSignal.SetValueFromString(dataSignal.GetValueString(getKeyword, true), keyword);
                }

                if (objectSignal.ValidateSignal())
                    Signals.Add(objectSignal);

                Progress.UpdateProgressBar(dataNumber);
            }

            // go through all objects
            for (int objectNumber = Signals.Count - 1; objectNumber >= 0; objectNumber--)
            {
                //find if it repeats, if yes then delete element
                for (int findNumber = objectNumber - 1; findNumber >= 0; findNumber--)
                {
                    if (Signals[findNumber].UniqueKKS == Signals[objectNumber].UniqueKKS)
                    {
                        Signals.RemoveAt(objectNumber);
                        break;
                    }
                }
            }

            Progress.HideProgressBar();

            debug.ToFile(Resources.ObjectGenerateUniqueData + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }

        /// <summary>
        /// Set object data to data signals
        /// </summary>
        /// <param name="data">data signals</param>
        public void SendToData(DataClass data)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ObjectTransferToData, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ObjectTransferToData, data.Signals.Count);

            string uniqueName;
            string keyword;
            for (int dataNumber = 0; dataNumber < data.Signals.Count; dataNumber++)
            {
                DataSignal dataSignal = data.Signals[dataNumber];

                uniqueName = dataSignal.UniqueKKS;
                // if data signal has KKS
                if (string.IsNullOrEmpty(uniqueName))
                    continue;

                for (int objectNumber = Signals.Count - 1; objectNumber >= 0; objectNumber--)
                {
                    ObjectSignal objectsignal = Signals[objectNumber];

                    // and finds this KKS in objects in same cpu, then transfer all data from objects to data signals
                    if ((objectsignal.UniqueKKS) != uniqueName)
                        continue;

                    // go through all column in objects and send data to objects
                    foreach (var column in Columns.Columns)
                    {
                        keyword = column.Key;
                        //do not transfer ID
                        if (keyword == "ID")
                            continue;

                        dataSignal.SetValueFromString(objectsignal.GetValueString(keyword, false), keyword);
                    }
                }

                Progress.UpdateProgressBar(dataNumber);
            }
            debug.ToFile(Resources.ObjectTransferToData + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }
    }
}