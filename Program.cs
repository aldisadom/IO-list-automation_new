using IO_list_automation_new.Properties;
using System;
using System.Globalization;
using System.Reflection;
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
        modScadaDB,
        modDB,
        address,
    }

    internal enum TabIndex
    {
        Design = 0,
        Data_ = 1,
        Object = 2,
        Modules = 3,
        Address = 4,
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
        public const string ObjectGeneralType = "ObjectGeneralType";

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
        public const string BaseAddress = "BaseAddress";
        public const string Address = "Address";
        public const string MultiLine = "MultiLine";
        public const string MultiLineEnd = "MultiLineEnd";
        public const string AddressArea = "AddressArea";
        public const string GetBaseAddress = "GetBaseAddress";
        public const string CPU = "CPU";
        public const string ObjectName = "ObjectName";

        public const string IsEmpty = "IsEmpty";
        public const string IsNotEmpty = "IsNotEmpty";
        public const string Equal = "=";
        public const string nEqual = "!=";
        public const string GreaterEqual = ">=";
        public const string Greater = ">";
        public const string LessEqual = "<=";
        public const string Less = "<";
        public const string Insert = "Insert";
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            if (Settings.Default.SettingsUpgrade)
            {
                Settings.Default.Upgrade();
                SettingsData.Default.Upgrade();
                SettingsDesign.Default.Upgrade();
                SettingsDesignInput.Default.Upgrade();
                SettingsModule.Default.Upgrade();
                SettingsObject.Default.Upgrade();
                Settings.Default.SettingsUpgrade = false;

                Settings.Default.Save();
            }

            Assembly thisAssembly = typeof(Program).Assembly;
            AssemblyName thisAssemblyName = thisAssembly.GetName();

            Version ver = thisAssemblyName.Version;

            string version = ver.Major.ToString() + "." +
                                    ver.Minor.ToString() + "." +
                                    ver.Build.ToString() + "." +
                                    ver.Revision.ToString();

            Debug debug = new Debug();
            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                debug.ClearDebug();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Settings.Default.ApplicationLanguage);

            debug.ToFile("--------------------------------------------------------------", DebugLevels.None, DebugMessageType.Info);
            debug.ToFile(Resources.SoftwareStart + ": " + version, DebugLevels.None, DebugMessageType.Info);
            debug.CurrentDebugLevel();

            debug.ToFile("UI language: " + Thread.CurrentThread.CurrentUICulture, DebugLevels.None, DebugMessageType.Info);

            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                debug.ToFile(Resources.EarlyReleaseWarning, DebugLevels.None, DebugMessageType.Warning);
            else
                debug.ToPopUp(Resources.EarlyReleaseWarning, DebugLevels.None, DebugMessageType.Warning);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow mainWindow = new MainWindow();
            Application.Run(mainWindow);
        }
    }
}