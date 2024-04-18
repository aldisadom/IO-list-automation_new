using IO_list_automation_new.Data;
using IO_list_automation_new.General;
using IO_list_automation_new.Modules;
using IO_list_automation_new.Properties;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class ModuleClass : GeneralClass<ModuleSignal>
    {
        protected override void InitialCollumnList()
        {
            Columns = new ColumnList(Name);

            Columns.Columns.Add(KeywordColumn.ID, new ColumnParameters(0, true, false));
            Columns.Columns.Add(KeywordColumn.CPU, new ColumnParameters(1, true, false));
            Columns.Columns.Add(KeywordColumn.ModuleName, new ColumnParameters(2, false, false));
            Columns.Columns.Add(KeywordColumn.ModuleType, new ColumnParameters(3, true, false));
            Columns.Columns.Add(KeywordColumn.Cabinet, new ColumnParameters(4, false, false));
        }

        public ModuleClass(ProgressIndication progress, DataGridView grid) : base("Module", nameof(FileExtensions.modDB), progress, grid, true)
        { }

        /// <summary>
        /// Get unique data signals
        /// </summary>
        /// <param name="data">data signals object</param>
        public void ExtractFromData(DataClass data)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ModuleGenerateUniqueData, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ModuleGenerateUniqueData, data.Signals.Count);

            Signals.Clear();
            string keyword;
            for (int dataNumber = 0; dataNumber < data.Signals.Count; dataNumber++)
            {
                DataSignal dataSignal = data.Signals[dataNumber];

                if (string.IsNullOrEmpty(dataSignal.ModuleName))
                    continue;

                ModuleSignal objectsignal = new ModuleSignal();

                // go through all column in objects and send data to objects
                foreach (var column in Columns.Columns)
                {
                    keyword = column.Key;
                    objectsignal.SetValueFromString(dataSignal.GetValueString(keyword, true), keyword);
                }

                if (objectsignal.ValidateSignal())
                    Signals.Add(objectsignal);

                Progress.UpdateProgressBar(dataNumber);
            }

            // go through all objects
            for (int objectNumber = Signals.Count - 1; objectNumber >= 0; objectNumber--)
            {
                //find if it repeats, if yes then delete element
                for (int findNumber = objectNumber - 1; findNumber >= 0; findNumber--)
                {
                    if (Signals[findNumber].UniqueModuleName == Signals[objectNumber].UniqueModuleName)
                    {
                        Signals.RemoveAt(objectNumber);
                        break;
                    }
                }
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ModuleGenerateUniqueData + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }
    }
}