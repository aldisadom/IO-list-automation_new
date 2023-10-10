using ExcelDataReader;
using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using SharpCompress.Common;
using SharpCompress.Readers.Zip;
using SwiftExcel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace IO_list_automation_new
{
    internal class ObjectSignal : GeneralSignal
    {
        private string CPU { get; set; }
        private string Operative { get; set; }
        private string KKS { get; set; }
        private string ObjectType { get; set; }
        private string ObjectName { get; set; }

        public ObjectSignal() : base()
        {
            CPU = string.Empty;
            Operative = string.Empty;
            KKS = string.Empty;
            ObjectType = string.Empty;
            ObjectName = string.Empty;
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
                case Const.ColumnNameID:
                    SetID(value);
                    break;
                case Const.ColumnNameCPU:
                    CPU = value;
                    break;
                case Const.ColumnNameKKS:
                    KKS = value;
                    break;
                case Const.ColumnNameOperative:
                    Operative = value;
                    break;
                case Const.ColumnNameObjectType:
                    ObjectType = value;
                    break;
                case Const.ColumnNameObjectName:
                    ObjectName = value;
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
                case Const.ColumnNameID:
                    _returnValue = GetID();
                    break;
                case Const.ColumnNameCPU:
                    _returnValue = CPU;
                    break;
                case Const.ColumnNameKKS:
                    _returnValue = KKS;
                    break;
                case Const.ColumnNameOperative:
                    _returnValue = Operative;
                    break;
                case Const.ColumnNameObjectType:
                    _returnValue = ObjectType;
                    break;
                case Const.ColumnNameObjectName:
                    _returnValue = ObjectName;
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
        /// Checks if design signal is valid:
        /// *Has Channel or PIN
        /// *Has Module
        /// *Has IO funcion text
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            bool _returnValue = true;

            if (KKS == "" || KKS == null)
                _returnValue = false;
            else if (ObjectType == "" || ObjectType == null)
                _returnValue = false;

            return _returnValue;
        }
    }

    internal class ObjectsClass : GeneralClass<ObjectSignal>
    {
        public override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(Const.ColumnNameID, Settings.Default.ObjectsColumnID));
            _columns.Add(new GeneralColumn(Const.ColumnNameCPU, Settings.Default.ObjectsColumnCPU));
            _columns.Add(new GeneralColumn(Const.ColumnNameKKS, Settings.Default.ObjectsColumnKKS));
            _columns.Add(new GeneralColumn(Const.ColumnNameOperative, Settings.Default.ObjectsColumnOperative));
            _columns.Add(new GeneralColumn(Const.ColumnNameObjectType, Settings.Default.ObjectsColumnObjectType));
            _columns.Add(new GeneralColumn(Const.ColumnNameObjectName, Settings.Default.ObjectsColumnObjectName));

            return _columns;
        }

        public override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            Settings.Default.ObjectsColumnID = _columns.GetColumnNumberFromKeyword(Const.ColumnNameID);
            Settings.Default.ObjectsColumnCPU = _columns.GetColumnNumberFromKeyword(Const.ColumnNameCPU);
            Settings.Default.ObjectsColumnKKS = _columns.GetColumnNumberFromKeyword(Const.ColumnNameKKS);
            Settings.Default.ObjectsColumnOperative = _columns.GetColumnNumberFromKeyword(Const.ColumnNameOperative);
            Settings.Default.ObjectsColumnObjectType = _columns.GetColumnNumberFromKeyword(Const.ColumnNameObjectType);
            Settings.Default.ObjectsColumnObjectName = _columns.GetColumnNumberFromKeyword(Const.ColumnNameObjectName);

            Settings.Default.Save();
        }

        public ObjectsClass(ProgressIndication progress, DataGridView grid) : base(Const.ObjectName, progress, grid)
        {

        }

        public void ExtractFromData(DataClass data)
        {
/*
            Debug debug = new Debug();
            debug.ToFile(Resources.ExtractDataFromDesign, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ExtractDataFromDesign, data.Signals.Count);

            string _keyword = string.Empty;
            for (int _signalNumber = 0; _signalNumber < data.Signals.Count; _signalNumber++)
            {
                ObjectSignal _signal = new ObjectSignal();
                DataSignal _dSignal = data.Signals.ElementAt(_signalNumber);

                // go through all collumn in design and send it to signals
                foreach (Column column in data.GetColumnList())
                {
                    _keyword = column.GetColumnKeyword();
                    _signal.SetValueFromString(_dSignal.GetValueString(_keyword, true), _keyword);
                }
                _signal.FindKKSInSignal(false);
                _signal.KKSDecode();

                Signals.Add(_signal);
                Progress.UpdateProgressBar(_signalNumber);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ExtractDataFromDesign + " - finished", DebugLevels.Development, DebugMessageType.Info);
*/
        }
    }
}
