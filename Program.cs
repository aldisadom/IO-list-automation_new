using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace IO_list_automation_new
{
    enum FileExtensions
    {
        data,
        design,
        objects,
        langfuncDB,
        langTypeDB,
        instDB,
    }
    enum DebugLevels
    {
        None = 0,
        Minimum = 1,
        Medium = 2,
        High = 3,
        Development = 10,
    }
    enum DebugMessageType
    {
        Info = 0,
        Warning = 1,
        Alarm = 2,
        Critical = 3,
    }
    enum TabIndexs
    {
        Design = 0,
        Data = 1,
        Object = 2,
    }
    enum ComboboxType
    {
        Main,
        MainNoEmpty,
        If,
        IfStatement,
        Data,
        Object,
        Text,
    }

    public static class ConstCol
    {
        public const string ColumnNameID = "ID";
        public const string ColumnNameCPU = "CPU";
        public const string ColumnNameKKS = "KKS";
        public const string ColumnNameRangeMin = "RangeMin";
        public const string ColumnNameRangeMax = "RangeMax";
        public const string ColumnNameUnits = "Units";
        public const string ColumnNameFalseText = "FalseText";
        public const string ColumnNameTrueText = "TrueText";
        public const string ColumnNameRevision = "Revision";
        public const string ColumnNameCable = "Cable";
        public const string ColumnNameCabinet = "Cabinet";
        public const string ColumnNameModuleName = "ModuleName";
        public const string ColumnNamePin = "Pin";
        public const string ColumnNameChannel = "Channel";
        public const string ColumnNameIOText = "IOText";
        public const string ColumnNamePage = "Page";
        public const string ColumnNameChanged = "Changed";
        public const string ColumnNameOperative = "Operative";
        public const string ColumnNameKKSPlant = "KKSPlant";
        public const string ColumnNameKKSLocation = "KKSLocation";
        public const string ColumnNameKKSDevice = "KKSDevice";
        public const string ColumnNameKKSFunction = "KKSFunction";
        public const string ColumnNameUsed = "Used";
        public const string ColumnNameObjectType = "ObjectType";
        public const string ColumnNameObjectName = "ObjectName";
        public const string ColumnNameObjectDetalisation = "ObjectDetalisation";
        public const string ColumnNameFunctionText = "FunctionText";
        public const string ColumnNameFunction = "Function";
        public const string ColumnNameTerminal = "Terminal";
        public const string ColumnNameTag = "Tag";

        public const string ColumnNameDeviceTypeText = "DeviceTypeText";
        public const string ColumnNameFunctionText1 = "FunctionText1";
        public const string ColumnNameFunctionText1o2 = "FunctionText1o2";
        public const string ColumnNameFunctionText2o2 = "FunctionText2o2";
        public const string ColumnNameFunction1 = "Function1";
        public const string ColumnNameFunction2 = "Function2";
    }

    public static class ConstDBChoices
    {
        public const string ChoiceNone = "";
        public const string ChoiceIf = "IF";
        public const string ChoiceTab = "TAB";
        public const string ChoiceData = "Data";
        public const string ChoiceObject = "Object";
        public const string ChoiceText = "Text";
        public const string ChoiceIO = "IO";

        public const string ChoiceIsEmpty = "is empty";
        public const string ChoiceIsNotEmpty = "is not empty";
    }


    public static class DeleteMe
    {
        public const string LTpath = "C:\\Users\\Aldis\\Desktop\\IO-list-automation_new\\IO-list-automation_new\\bin\\Debug\\DB\\LT";
        public const string xa800pathVariables = "C:\\Users\\Aldis\\Desktop\\IO-list-automation_new\\IO-list-automation_new\\bin\\Debug\\DB\\800xA\\Declarations";
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static public void Main()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("lt");

            string _versionNumber = "0.0.1";

            Debug _debug = new Debug();
            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                _debug.ClearDebug();


            Settings.Default.SelectedCPU = "800xA";
            Settings.Default.Save();

            _debug.ToFile("--------------------------------------------------------------", DebugLevels.None, DebugMessageType.Info);
            _debug.ToFile(Resources.SoftwareStart + ": " + _versionNumber , DebugLevels.None, DebugMessageType.Info);
            _debug.CurrentDebugLevel();

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
