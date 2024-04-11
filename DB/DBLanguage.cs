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

                    const string debugText = "DBLanguageTypeSignal.GetValueString";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + parameterName + " is not created for this element");
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
        private string FileName = "";

        protected override ColumnList GeneralGenerateColumnsList()
        {
            ColumnList columns = new ColumnList();
            columns.Columns.Add(KeywordColumn.DeviceTypeText, new GeneralColumnParameters(0, true));
            columns.Columns.Add(KeywordColumn.ObjectType, new GeneralColumnParameters(1, true));

            return columns;
        }

        public void LoadEdit()
        {
            LoadFromFile(FileName);
        }

        public void SaveEdit()
        {
            SaveToFile(FileName);
        }

        protected override void UpdateSettingsColumnsList()
        {
        }

        public DBLanguageType(ProgressIndication progress, DataGridView grid) : base("DBLanguageType", nameof(FileExtensions.langTypeDB), progress, grid, true)
        {
            FileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB\\" + Settings.Default.IOLanguage;
            Grid = new GeneralGrid(Name, GridTypes.DBForceEdit, grid, null);
        }

        /// <summary>
        /// Find in text its object type
        /// </summary>
        /// <param name="inputText">text to be searched</param>
        /// <returns>object type</returns>
        private string FindType(string inputText)
        {
            string search;
            string text = inputText.ToLower();

            //go through all type texts
            foreach (DBLanguageTypeSignal objectTypeSignal in Signals)
            {
                search = objectTypeSignal.DeviceTypeText.ToLower();
                //if input text contains function return its function type
                if (!string.IsNullOrEmpty(search) && text.Contains(search))
                    return objectTypeSignal.ObjectType;
            }
            return string.Empty;
        }

        /// <summary>
        /// find object type in all objects
        /// </summary>
        /// <param name="objects">input objects</param>
        public void FindAllType(ObjectsClass objects)
        {
            string functionType;

            Debug debug = new Debug();
            debug.ToFile("Finding object type in objects", DebugLevels.High, DebugMessageType.Info);

            string fileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB\\" + Settings.Default.IOLanguage;

            //convert data from file to signals
            if (!ListToSignals(File.LoadFromFile(fileName), BaseColumns, false))
                return;

            Progress.RenameProgressBar(Resources.FindObjectType, objects.Signals.Count);

            int progress = 0;

            foreach (ObjectSignal objectSignal in objects.Signals)
            {
                functionType = FindType(objectSignal.ObjectName);
                objectSignal.SetValueFromString(functionType, KeywordColumn.ObjectType);

                progress++;
                Progress.UpdateProgressBar(progress);
            }
            Progress.HideProgressBar();

            objects.PutDataToGrid(false);
            debug.ToFile("Finding object type in objects - finished", DebugLevels.High, DebugMessageType.Info);
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

                    const string debugText = "DBLanguageFunctionSignal.GetValueString";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + parameterName + " is not created for this element");
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
        private string FileName = "";

        protected override ColumnList GeneralGenerateColumnsList()
        {
            ColumnList columns = new ColumnList();
            columns.Columns.Add(KeywordColumn.FunctionText1, new GeneralColumnParameters(0, false));
            columns.Columns.Add(KeywordColumn.Function1, new GeneralColumnParameters(1, false));
            columns.Columns.Add(KeywordColumn.FunctionText1o2, new GeneralColumnParameters(2, false));
            columns.Columns.Add(KeywordColumn.FunctionText2o2, new GeneralColumnParameters(3, false));
            columns.Columns.Add(KeywordColumn.Function2, new GeneralColumnParameters(4, false));
            
            return columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
        }

        public DBLanguageFunctionType(ProgressIndication progress, DataGridView grid) : base("DBFunctionLanguage", nameof(FileExtensions.langFuncDB), progress, grid, true)
        {
            FileName = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DB\\" + Settings.Default.IOLanguage;
            Grid = new GeneralGrid(Name, GridTypes.DBForceEdit, grid, null);
        }

        public void LoadEdit()
        {
            LoadFromFile(FileName);
        }

        public void SaveEdit()
        {
            SaveToFile(FileName);
        }

        /// <summary>
        /// Find in text its function type
        /// </summary>
        /// <param name="inputText">text to be searched</param>
        /// <returns>function type</returns>
        private string FindFunctionType(string inputText)
        {
            string textToSearch;
            string text = inputText.ToLower();

            //first go through 2part function texts
            //go through all 1o2 function texts
            foreach (DBLanguageFunctionSignal functionSignal1 in Signals)
            {
                textToSearch = functionSignal1.FunctionText1o2.ToLower();
                if (string.IsNullOrEmpty(textToSearch))
                    continue;

                //if found 1o2 part then search for 2o2 part
                if (!text.Contains(textToSearch))
                    continue;

                //go through all 2o2 function texts
                foreach (DBLanguageFunctionSignal functionSignal2 in Signals)
                {
                    textToSearch = functionSignal2.FunctionText2o2.ToLower();
                    if (string.IsNullOrEmpty(textToSearch))
                        continue;

                    //if input text contains function return its function type
                    if (text.Contains(textToSearch))
                        return functionSignal2.Function2;
                }
            }

            //if not found in 2 parts then go through 1 part function text
            foreach (DBLanguageFunctionSignal functionSignal in Signals)
            {
                textToSearch = functionSignal.FunctionText1.ToLower();
                if (string.IsNullOrEmpty(textToSearch))
                    continue;

                //if input text contains function return its function type
                if (text.Contains(textToSearch))
                    return functionSignal.Function1;
            }

            return string.Empty;
        }

        /// <summary>
        /// Find function type in all data
        /// </summary>
        /// <param name="data">input data</param>
        public void FindAllFunctionType(DataClass data)
        {
            string functionType;

            Debug debug = new Debug();
            debug.ToFile("Finding function type in data", DebugLevels.High, DebugMessageType.Info);

            //convert data from file to signals
            if (!ListToSignals(File.LoadFromFile(FileName), BaseColumns, false))
                return;

            Progress.RenameProgressBar(Resources.FindFunction, data.Signals.Count);

            int progress = 0;
            foreach (DataSignal dataSignal in data.Signals)
            {
                functionType = FindFunctionType(dataSignal.IOText);
                dataSignal.SetValueFromString(functionType, KeywordColumn.Function);

                progress++;
                Progress.UpdateProgressBar(progress);
            }

            Progress.HideProgressBar();

            data.PutDataToGrid(false);
            debug.ToFile("Finding function type in data - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
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