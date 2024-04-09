using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class ModuleSignal : GeneralSignal
    {
        public string CPU { get; private set; }
        public string ID { get; private set; }
        public string ModuleName { get; private set; }
        public string ModuleType { get; private set; }
        public string Cabinet { get; private set; }

        public string UniqueModuleName
        { get { return Cabinet + "_" + ModuleName + "_" + CPU; } }

        public ModuleSignal() : base()
        {
            ID = string.Empty;
            CPU = string.Empty;
            ModuleName = string.Empty;
            ModuleType = string.Empty;
            Cabinet = string.Empty;
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

                case KeywordColumn.ModuleName:
                    ModuleName = value;
                    break;

                case KeywordColumn.ModuleType:
                    ModuleType = value;
                    break;

                case KeywordColumn.Cabinet:
                    Cabinet = value;
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

                case KeywordColumn.ModuleName:
                    return ModuleName;

                case KeywordColumn.ModuleType:
                    return ModuleType;

                case KeywordColumn.Cabinet:
                    return Cabinet;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string debugText = "ModuleSignal.GetValueString";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + parameterName + " is not created for this element");
            }
        }

        /// <summary>
        /// Checks if design signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            return true;
        }
    }

    internal class ModuleClass : GeneralClass<ModuleSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsModule.Default.ColumnID, true),
                new GeneralColumn(KeywordColumn.CPU, SettingsModule.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.ModuleName, SettingsModule.Default.ColumnModuleName, false),
                new GeneralColumn(KeywordColumn.ModuleType, SettingsModule.Default.ColumnModuleType, true),
                new GeneralColumn(KeywordColumn.Cabinet, SettingsModule.Default.ColumnCabinet, false),
            };

            return columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList columns = Columns;

            SettingsModule.Default.ColumnID = columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsModule.Default.ColumnCPU = columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsModule.Default.ColumnModuleName = columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleName);
            SettingsModule.Default.ColumnModuleType = columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleType);
            SettingsModule.Default.ColumnCabinet = columns.GetColumnNumberFromKeyword(KeywordColumn.Cabinet);

            SettingsObject.Default.Save();
        }

        public ModuleClass(ProgressIndication progress, DataGridView grid) : base("Module", nameof(FileExtensions.modDB), progress, grid, true)
        { }

        public ModuleClass() : base("Module", nameof(FileExtensions.modDB), null, null, true)
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

            UpdateColumnNumbers(data.BaseColumns.Columns);
            Signals.Clear();
            string keyword;
            for (int dataNumber = 0; dataNumber < data.Signals.Count; dataNumber++)
            {
                DataSignal dataSignal = data.Signals[dataNumber];

                if (string.IsNullOrEmpty(dataSignal.ModuleName))
                    continue;

                ModuleSignal objectsignal = new ModuleSignal();

                // go through all column in objects and send data to objects
                foreach (GeneralColumn column in Columns)
                {
                    keyword = column.Keyword;
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