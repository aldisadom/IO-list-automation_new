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

namespace IO_list_automation_new
{
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


    public static class Const
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

        public const string DesignName = "Design";
        public const string DataName = "Data";
        public const string ObjectName = "Object";
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static public void Main()
        {
            string _versionNumber = "0.0.1";

            Settings.Default.ExcelColumnChannel = 2;
            Settings.Default.ExcelColumnModuleName = 1;
            Settings.Default.ExcelColumnIOText = 3;
            Settings.Default.ExcelColumnPin = -1;
            Settings.Default.InputDataChannelHasNumber = true;
            Settings.Default.ExcelColumnCabinet = -1;
            Settings.Default.ExcelRowOffset = 3;

            Debug _debug = new Debug();
            if (Settings.Default.DebugLevel == (uint)DebugLevels.Development)
                _debug.ClearDebug();

            _debug.ToFile("--------------------------------------------------------------", DebugLevels.None, DebugMessageType.Info);
            _debug.ToFile(Resources.SoftwareStart + ": " + _versionNumber , DebugLevels.None, DebugMessageType.Info);
            _debug.CurrentDebugLevel();
            _debug.ToPopUp(Resources.EarlyReleaseWarning, DebugLevels.None, DebugMessageType.Warning);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow mainWindow = new MainWindow();
            Application.Run(mainWindow);
        }
    }

    

}
