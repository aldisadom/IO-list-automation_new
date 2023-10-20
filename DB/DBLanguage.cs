using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
                case ConstCol.ColumnNameObjectType:
                    ObjectType = value;
                    break;
                case ConstCol.ColumnNameDeviceTypeText:
                    DeviceTypeText = value;
                    break;
            }
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="supressError">supress alarm message, used only for transfering from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public override string GetValueString(string parameterName, bool supressError)
        {
            string _returnValue = string.Empty;
            switch (parameterName)
            {
                case ConstCol.ColumnNameObjectType:
                    _returnValue = ObjectType;
                    break;
                case ConstCol.ColumnNameDeviceTypeText:
                    _returnValue = DeviceTypeText;
                    break;
                default:
                    if (!supressError)
                    {
                        Debug _debug = new Debug();
                        _debug.ToPopUp(Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    }
                    break;
            }
            return _returnValue;
        }

        /// <summary>
        /// Checks if signal is valid:
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            bool _returnValue = true;

            if (ObjectType == "" || ObjectType == null)
                _returnValue = false;
            else if (DeviceTypeText == "" || DeviceTypeText == null)
                _returnValue = false;

            return _returnValue;
        }
    }

    internal class DBLanguageType : GeneralClass<DBLanguageTypeSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(ConstCol.ColumnNameDeviceTypeText, 0, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectType, 1, true));

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {

        }

        public DBLanguageType(ProgressIndication progress, DataGridView grid) : base("DBLanguageType",true, FileExtensions.langTypeDB.ToString(), progress, grid)
        {

        }

        /// <summary>
        /// Find in text its object type
        /// </summary>
        /// <param name="inputText">text to be searched</param>
        /// <returns>object type</returns>
        private string FindType(string inputText)
        {
            string _search = string.Empty;
            string _returnText = string.Empty;
            string _text = inputText.ToLower();

            //go through all type texts
            for (int i = 0; i < Signals.Count; i++)
            {
                _search = Signals[i].DeviceTypeText.ToLower();
                if (_search != string.Empty)
                {
                    //if input text contains function return its function type 
                    if (_text.Contains(_search))
                    {
                        _returnText = Signals[i].ObjectType;
                        break;
                    }
                }
            }

            return _returnText;
        }

        /// <summary>
        /// find object type in all objects
        /// </summary>
        /// <param name="objects">input objects</param>
        public void FindAllType(ObjectsClass objects)
        {
            string _functionType = string.Empty;

            Debug debug = new Debug();
            debug.ToFile("Finding object type in data", DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.FindObjectType, objects.Signals.Count);

            for (int i = 0; i < objects.Signals.Count; i++)
            {
                _functionType = FindType(objects.Signals[i].ObjectName);
                objects.Signals[i].SetValueFromString(_functionType, ConstCol.ColumnNameObjectType);

                Progress.UpdateProgressBar(i);
            }
            Progress.HideProgressBar();

            objects.Grid.PutData();
            debug.ToFile("Finding object type in data - finished", DebugLevels.Development, DebugMessageType.Info);
        }
    }

    internal class DBLanguageFunctionSignal : GeneralSignal
    {
        public string FunctionText1 { get; private set; }
        public string Funcion1 { get; private set; }
        public string FunctionText1o2 { get; private set; }
        public string FunctionText2o2 { get; private set; }
        public string Funcion2 { get; private set; }

        public DBLanguageFunctionSignal() : base()
        {
            FunctionText1 = string.Empty;
            Funcion1 = string.Empty;
            FunctionText1o2 = string.Empty;
            FunctionText2o2 = string.Empty;
            Funcion2 = string.Empty;
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
                case ConstCol.ColumnNameFunctionText1:
                    FunctionText1 = value;
                    break;
                case ConstCol.ColumnNameFunction1:
                    Funcion1 = value;
                    break;
                case ConstCol.ColumnNameFunctionText1o2:
                    FunctionText1o2 = value;
                    break;
                case ConstCol.ColumnNameFunctionText2o2:
                    FunctionText2o2 = value;
                    break;
                case ConstCol.ColumnNameFunction2:
                    Funcion2 = value;
                    break;
            }
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="supressError">supress alarm message, used only for transfering from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public override string GetValueString(string parameterName, bool supressError)
        {
            string _returnValue = string.Empty;
            switch (parameterName)
            {
                case ConstCol.ColumnNameFunctionText1:
                    _returnValue = FunctionText1;
                    break;
                case ConstCol.ColumnNameFunction1:
                    _returnValue = Funcion1;
                    break;
                case ConstCol.ColumnNameFunctionText1o2:
                    _returnValue = FunctionText1o2;
                    break;
                case ConstCol.ColumnNameFunctionText2o2:
                    _returnValue = FunctionText2o2;
                    break;
                case ConstCol.ColumnNameFunction2:
                    _returnValue = Funcion2;
                    break;
                default:
                    if (!supressError)
                    {
                        Debug _debug = new Debug();
                        _debug.ToPopUp(Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    }
                    break;
            }
            return _returnValue;
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
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFunctionText1, 0, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFunction1, 1, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFunctionText1o2, 2, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFunctionText2o2, 3, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFunction2, 4, true));

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {

        }

        public DBLanguageFunctionType(ProgressIndication progress, DataGridView grid) : base("DBFunctionLanguage",true, FileExtensions.langfuncDB.ToString(), progress, grid)
        {

        }

        /// <summary>
        /// Find in text its function type
        /// </summary>
        /// <param name="inputText">text to be searched</param>
        /// <returns>function type</returns>
        private string FindFunctionType(string inputText)
        {
            string _search = string.Empty;
            string _returnText = string.Empty;
            string _text = inputText.ToLower();

            //first go through 2part function texts
            //go through all 1o2 function texts
            for (int i = 0; i < Signals.Count; i++)
            {
                _search = Signals[i].FunctionText1o2.ToLower();
                if (_search != string.Empty)
                {
                    //if found 1o2 part then search for 2o2 part
                    if (_text.Contains(_search))
                    {
                        //go through all 2o2 function texts
                        for (int j = 0; j < Signals.Count; j++)
                        {
                            _search = Signals[j].FunctionText2o2.ToLower();
                            if (_search != string.Empty)
                            {
                                //if input text contains function return its function type 
                                if (_text.Contains(_search))
                                {
                                    _returnText = Signals[j].Funcion2;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //if not found in 2 parts then go throug 1 part function text
            if (_returnText == string.Empty)
            {
                //go through all 1 function texts
                for (int i = 0; i < Signals.Count; i++)
                {
                    _search = Signals[i].FunctionText1.ToLower();
                    if (_search != string.Empty)
                    {
                        //if input text contains function return its function type 
                        if (_text.Contains(_search))
                        {
                            _returnText = Signals[i].Funcion1;
                            break;
                        }
                    }
                }
            }
            return _returnText;
        }

        /// <summary>
        /// Find function type in all data
        /// </summary>
        /// <param name="data">input data</param>
        public void FindAllFunctionType(DataClass data)
        {
            string _functionType = string.Empty;

            Debug debug = new Debug();
            debug.ToFile("Finding function type in data", DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.FindFunction, data.Signals.Count);

            for (int i = 0; i < data.Signals.Count; i++)
            {
                _functionType = FindFunctionType(data.Signals[i].IOText);
                data.Signals[i].SetValueFromString(_functionType, ConstCol.ColumnNameFunction);

                Progress.UpdateProgressBar(i);
            }

            Progress.HideProgressBar();

            data.Grid.PutData();
            debug.ToFile("Finding function type in data - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }
    }

    internal class DBLanguage
    {
        public DBLanguageFunctionType FunctionType { get; set; }
        public DBLanguageType Type { get; set; }

        public DBLanguage(ProgressIndication progress, DataGridView grid)
        {
            FunctionType = new DBLanguageFunctionType(progress,grid);
            Type = new DBLanguageType(progress, grid);
        }
    }
}
