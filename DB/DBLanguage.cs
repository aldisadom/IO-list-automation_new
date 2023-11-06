using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class DBLanguageTypeSignal : GeneralSignal
    {
        public string DeviceTypeText { get; private set; }
        public string ObjectType { get; private set; }

        public DBLanguageTypeSignal() : base()
        {
            DeviceTypeText = string.Empty;
            ObjectType = string.Empty;
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
                case KeywordColumn.ObjectType:
                    ObjectType = value;
                    break;

                case KeywordColumn.DeviceTypeText:
                    DeviceTypeText = value;
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
                case KeywordColumn.ObjectType:
                    return ObjectType;

                case KeywordColumn.DeviceTypeText:
                    return DeviceTypeText;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string _debugText = "DBLanguageTypeSignal.GetValueString";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + parameterName + " is not created for this element");
            }
        }

        /// <summary>
        /// Checks if signal is valid:
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            if (string.IsNullOrEmpty(ObjectType))
                return false;
            else if (string.IsNullOrEmpty(DeviceTypeText))
                return false;

            return true;
        }
    }

    internal class DBLanguageType : GeneralClass<DBLanguageTypeSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.DeviceTypeText, 0, true),
                new GeneralColumn(KeywordColumn.ObjectType, 1, true),
            };

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
        }

        public DBLanguageType(ProgressIndication progress, DataGridView grid) : base("DBLanguageType", nameof(FileExtensions.langTypeDB), progress, grid)
        {
        }

        /// <summary>
        /// Find in text its object type
        /// </summary>
        /// <param name="inputText">text to be searched</param>
        /// <returns>object type</returns>
        private string FindType(string inputText)
        {
            string _search;
            string _text = inputText.ToLower();

            //go through all type texts
            for (int i = 0; i < Signals.Count; i++)
            {
                _search = Signals[i].DeviceTypeText.ToLower();
                //if input text contains function return its function type
                if (!string.IsNullOrEmpty(_search) && _text.Contains(_search))
                    return Signals[i].ObjectType;
            }
            return string.Empty;
        }

        /// <summary>
        /// find object type in all objects
        /// </summary>
        /// <param name="objects">input objects</param>
        public void FindAllType(ObjectsClass objects)
        {
            string _functionType;

            Debug debug = new Debug();
            debug.ToFile("Finding object type in objects", DebugLevels.Development, DebugMessageType.Info);

            string _fileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB\\" + Settings.Default.IOLanguage;

            //convert data from file to signals
            if (!ListToSignals(Grid.LoadFromFileToMemory(_fileName), BaseColumns.Columns, false))
                return;

            Progress.RenameProgressBar(Resources.FindObjectType, objects.Signals.Count);

            for (int i = 0; i < objects.Signals.Count; i++)
            {
                _functionType = FindType(objects.Signals[i].ObjectName);
                objects.Signals[i].SetValueFromString(_functionType, KeywordColumn.ObjectType);

                Progress.UpdateProgressBar(i);
            }
            Progress.HideProgressBar();

            objects.PutDataToGrid(false);
            debug.ToFile("Finding object type in objects - finished", DebugLevels.Development, DebugMessageType.Info);
        }
    }

    internal class DBLanguageFunctionSignal : GeneralSignal
    {
        public string FunctionText1 { get; private set; }
        public string Function1 { get; private set; }
        public string FunctionText1o2 { get; private set; }
        public string FunctionText2o2 { get; private set; }
        public string Function2 { get; private set; }

        public DBLanguageFunctionSignal() : base()
        {
            FunctionText1 = string.Empty;
            Function1 = string.Empty;
            FunctionText1o2 = string.Empty;
            FunctionText2o2 = string.Empty;
            Function2 = string.Empty;
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
                case KeywordColumn.FunctionText1:
                    FunctionText1 = value;
                    break;

                case KeywordColumn.Function1:
                    Function1 = value;
                    break;

                case KeywordColumn.FunctionText1o2:
                    FunctionText1o2 = value;
                    break;

                case KeywordColumn.FunctionText2o2:
                    FunctionText2o2 = value;
                    break;

                case KeywordColumn.Function2:
                    Function2 = value;
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
                case KeywordColumn.FunctionText1:
                    return FunctionText1;

                case KeywordColumn.Function1:
                    return Function1;

                case KeywordColumn.FunctionText1o2:
                    return FunctionText1o2;

                case KeywordColumn.FunctionText2o2:
                    return FunctionText2o2;

                case KeywordColumn.Function2:
                    return Function2;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string _debugText = "DBLanguageFunctionSignal.GetValueString";
                    Debug _debug = new Debug();
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + parameterName + " is not created for this element");
            }
        }

        /// <summary>
        /// Checks if signal is valid:
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            return true;
        }
    }

    internal class DBLanguageFunctionType : GeneralClass<DBLanguageFunctionSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.FunctionText1, 0, false),
                new GeneralColumn(KeywordColumn.Function1, 1, false),
                new GeneralColumn(KeywordColumn.FunctionText1o2, 2, false),
                new GeneralColumn(KeywordColumn.FunctionText2o2, 3, false),
                new GeneralColumn(KeywordColumn.Function2, 4, false),
            };

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
        }

        public DBLanguageFunctionType(ProgressIndication progress, DataGridView grid) : base("DBFunctionLanguage", nameof(FileExtensions.langFuncDB), progress, grid)
        {
        }

        /// <summary>
        /// Find in text its function type
        /// </summary>
        /// <param name="inputText">text to be searched</param>
        /// <returns>function type</returns>
        private string FindFunctionType(string inputText)
        {
            string _textToSearch;
            string _text = inputText.ToLower();

            //first go through 2part function texts
            //go through all 1o2 function texts
            for (int _signalIndex = 0; _signalIndex < Signals.Count; _signalIndex++)
            {
                _textToSearch = Signals[_signalIndex].FunctionText1o2.ToLower();
                if (string.IsNullOrEmpty(_textToSearch))
                    continue;

                //if found 1o2 part then search for 2o2 part
                if (!_text.Contains(_textToSearch))
                    continue;

                //go through all 2o2 function texts
                for (int _signalIndex2 = 0; _signalIndex2 < Signals.Count; _signalIndex2++)
                {
                    _textToSearch = Signals[_signalIndex2].FunctionText2o2.ToLower();
                    if (string.IsNullOrEmpty(_textToSearch))
                        continue;

                    //if input text contains function return its function type
                    if (_text.Contains(_textToSearch))
                        return Signals[_signalIndex2].Function2;
                }
            }

            //if not found in 2 parts then go through 1 part function text
            for (int _signalIndex = 0; _signalIndex < Signals.Count; _signalIndex++)
            {
                _textToSearch = Signals[_signalIndex].FunctionText1.ToLower();
                if (string.IsNullOrEmpty(_textToSearch))
                    continue;

                //if input text contains function return its function type
                if (_text.Contains(_textToSearch))
                    return Signals[_signalIndex].Function1;
            }

            return string.Empty;
        }

        /// <summary>
        /// Find function type in all data
        /// </summary>
        /// <param name="data">input data</param>
        public void FindAllFunctionType(DataClass data)
        {
            string _functionType;

            Debug debug = new Debug();
            debug.ToFile("Finding function type in data", DebugLevels.Development, DebugMessageType.Info);

            string _fileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB\\" + Settings.Default.IOLanguage;

            //convert data from file to signals
            if (!ListToSignals(Grid.LoadFromFileToMemory(_fileName), BaseColumns.Columns, false))
                return;

            Progress.RenameProgressBar(Resources.FindFunction, data.Signals.Count);

            for (int i = 0; i < data.Signals.Count; i++)
            {
                _functionType = FindFunctionType(data.Signals[i].IOText);
                data.Signals[i].SetValueFromString(_functionType, KeywordColumn.Function);

                Progress.UpdateProgressBar(i);
            }

            Progress.HideProgressBar();

            data.PutDataToGrid(false);
            debug.ToFile("Finding function type in data - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }
    }

    internal class DBLanguage
    {
        public DBLanguageFunctionType FunctionType { get; set; }
        public DBLanguageType Type { get; set; }

        public DBLanguage(ProgressIndication progress, DataGridView grid)
        {
            FunctionType = new DBLanguageFunctionType(progress, grid);
            Type = new DBLanguageType(progress, grid);
        }
    }
}