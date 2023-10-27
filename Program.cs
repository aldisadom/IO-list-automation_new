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
        langFuncDB,
        langTypeDB,
        instDB,
        decDB,
        decScadaDB,
        instScadaDB,
        modDB,
    }
    enum DBTypeLevel
    {
        Base,
        CPU,
        SCADA,
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
        Info,
        Warning,
        Alarm,
        Critical,
    }
    enum TabIndex
    {
        Design = 0,
        Data = 1,
        Object = 2,
        Modules =3,
    }
    enum ComboBoxType
    {
        Main,
        MainNoEmpty,
        If,
        IfStatement,
        Data,
        Object,
        Text,
        TagType,
        Number,
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
        public const string Text = "Text";
        public const string IO = "IO";
        public const string Index = "Index";
        public const string TagType = "TagType";

        public const string IsEmpty = "is empty";
        public const string IsNotEmpty = "is not empty";
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
            const string _versionNumber = "0.0.1";

            Debug _debug = new Debug();
            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                _debug.ClearDebug();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Default.ApplicationLanguage);

            _debug.ToFile("--------------------------------------------------------------", DebugLevels.None, DebugMessageType.Info);
            _debug.ToFile(Resources.SoftwareStart + ": " + _versionNumber , DebugLevels.None, DebugMessageType.Info);
            _debug.CurrentDebugLevel();

            _debug.ToFile("UI language: " + Thread.CurrentThread.CurrentUICulture, DebugLevels.None, DebugMessageType.Info);

            _debug.ToFile(ResourcesColumns.Units, DebugLevels.None, DebugMessageType.Info);

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
