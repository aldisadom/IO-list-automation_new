using IO_list_automation_new.Properties;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal enum FileExtensions
    {
        data,
        design,
        objects,
        langFuncDB,
        langTypeDB,
        instDB,
        decDB,
        decScadaDB,
        instScadaDB,
        modDB,
    }

    internal enum TabIndex
    {
        Design = 0,
        Data = 1,
        Object = 2,
        Modules = 3,
    }

    public static class KeywordColumn
    {
        public const string ID = "ID";
        public const string CPU = "CPU";
        public const string KKS = "KKS";
        public const string RangeMin = "RangeMin";
        public const string RangeMax = "RangeMax";
        public const string Units = "Units";
        public const string FalseText = "FalseText";
        public const string TrueText = "TrueText";
        public const string Revision = "Revision";
        public const string Cable = "Cable";
        public const string Cabinet = "Cabinet";
        public const string ModuleName = "ModuleName";
        public const string Pin = "Pin";
        public const string Channel = "Channel";
        public const string IOText = "IOText";
        public const string Page = "Page";
        public const string Changed = "Changed";
        public const string Operative = "Operative";
        public const string KKSPlant = "KKSPlant";
        public const string KKSLocation = "KKSLocation";
        public const string KKSDevice = "KKSDevice";
        public const string KKSFunction = "KKSFunction";
        public const string Used = "Used";
        public const string ObjectType = "ObjectType";
        public const string ObjectName = "ObjectName";
        public const string ObjectSpecifics = "ObjectSpecifics";
        public const string FunctionText = "FunctionText";
        public const string Function = "Function";
        public const string Terminal = "Terminal";
        public const string Tag = "Tag";
        public const string ModuleType = "ModuleType";

        public const string DeviceTypeText = "DeviceTypeText";
        public const string FunctionText1 = "FunctionText1";
        public const string FunctionText1o2 = "FunctionText1o2";
        public const string FunctionText2o2 = "FunctionText2o2";
        public const string Function1 = "Function1";
        public const string Function2 = "Function2";
    }

    public static class KeywordDBChoices
    {
        public const string None = "";
        public const string If = "IF";
        public const string Tab = "TAB";
        public const string Data = "Data";
        public const string Object = "Object";
        public const string Modules = "Modules";
        public const string Text = "Text";
        public const string IOTag = "IOTag";
        public const string IOChannel = "IOChannel";
        public const string Index = "Index";
        public const string VariableType = "VariableType";

        public const string IsEmpty = "IsEmpty";
        public const string IsNotEmpty = "IsNotEmpty";
        public const string Equal = "=";
        public const string nEqual = "!=";
        public const string GreaterEqual = ">=";
        public const string Greater = ">";
        public const string LessEqual = "<=";
        public const string Less = "<";
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            const string _versionNumber = "0.0.1";

            Debug _debug = new Debug();
            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                _debug.ClearDebug();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Default.ApplicationLanguage);

            _debug.ToFile("--------------------------------------------------------------", DebugLevels.None, DebugMessageType.Info);
            _debug.ToFile(Resources.SoftwareStart + ": " + _versionNumber, DebugLevels.None, DebugMessageType.Info);
            _debug.CurrentDebugLevel();

            _debug.ToFile("UI language: " + Thread.CurrentThread.CurrentUICulture, DebugLevels.None, DebugMessageType.Info);

            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                _debug.ToFile(Resources.EarlyReleaseWarning, DebugLevels.None, DebugMessageType.Warning);
            else
                _debug.ToPopUp(Resources.EarlyReleaseWarning, DebugLevels.None, DebugMessageType.Warning);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow mainWindow = new MainWindow();
            Application.Run(mainWindow);
        }
    }
}