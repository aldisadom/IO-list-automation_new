using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string UniqueModuleName { get { return Cabinet + "_" + ModuleName + "_"+ CPU; } }

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
            string _returnValue = string.Empty;
            switch (parameterName)
            {
                case KeywordColumn.ID:
                    _returnValue = ID;
                    break;
                case KeywordColumn.CPU:
                    _returnValue = CPU;
                    break;
                case KeywordColumn.ModuleName:
                    _returnValue = ModuleName;
                    break;
                case KeywordColumn.ModuleType:
                    _returnValue = ModuleType;
                    break;
                case KeywordColumn.Cabinet:
                    _returnValue = Cabinet;
                    break;
                default:
                    if (!suppressError)
                    {
                        Debug _debug = new Debug();
                        _debug.ToPopUp("ModuleSignal.GetValueString " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    }
                    break;
            }
            return _returnValue;
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
            List<GeneralColumn> _columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsModule.Default.ColumnID, true),
                new GeneralColumn(KeywordColumn.CPU, SettingsModule.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.ModuleName, SettingsModule.Default.ColumnModuleName, false),
                new GeneralColumn(KeywordColumn.ModuleType, SettingsModule.Default.ColumnModuleType, true),
                new GeneralColumn(KeywordColumn.Cabinet, SettingsModule.Default.ColumnCabinet, false),
            };

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            SettingsModule.Default.ColumnID = _columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsModule.Default.ColumnCPU = _columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsModule.Default.ColumnModuleName = _columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleName);
            SettingsModule.Default.ColumnModuleType = _columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleType);
            SettingsModule.Default.ColumnCabinet = _columns.GetColumnNumberFromKeyword(KeywordColumn.Cabinet);

            SettingsObject.Default.Save();
        }

        public ModuleClass(ProgressIndication progress, DataGridView grid) : base("Module", false, nameof(FileExtensions.modDB), progress, grid)
        {}

        public ModuleClass() : base("Module", false, nameof(FileExtensions.modDB), null, null)
        {}

        /// <summary>
        /// Get unique data signals
        /// </summary>
        /// <param name="data">data signals object</param>
        public void ExtractFromData(DataClass data)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ModuleGenerateUniqueData, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ModuleGenerateUniqueData, data.Signals.Count);

            UpdateColumnNumbers(data.BaseColumns.Columns);
            Signals.Clear();
            string _keyword = string.Empty;
            for (int _dataNumber = 0; _dataNumber < data.Signals.Count; _dataNumber++)
            {
                DataSignal _dataSignal = data.Signals[_dataNumber];

                if (_dataSignal.ModuleName != string.Empty)
                {
                    ModuleSignal _objectSignal = new ModuleSignal();

                    // go through all column in objects and send data to objects
                    foreach (GeneralColumn _column in Columns)
                    {
                        _keyword = _column.Keyword;
                        _objectSignal.SetValueFromString(_dataSignal.GetValueString(_keyword, true), _keyword);
                    }

                    if (_objectSignal.ValidateSignal())
                        Signals.Add(_objectSignal);
                }

                Progress.UpdateProgressBar(_dataNumber);
            }

            // go through all objects
            for (int _objectNumber = Signals.Count - 1; _objectNumber >= 0; _objectNumber--)
            {
                //find if it repeats, if yes then delete element
                for (int _findNumber = _objectNumber - 1; _findNumber >= 0; _findNumber--)
                {
                    if (Signals[_findNumber].UniqueModuleName == Signals[_objectNumber].UniqueModuleName)
                    {
                        Signals.RemoveAt(_objectNumber);
                        break;
                    }
                }
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ModuleGenerateUniqueData + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }
    }
}
