using ExcelDataReader;
using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using SharpCompress.Common;
using SharpCompress.Readers.Zip;
using SwiftExcel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;


namespace IO_list_automation_new
{
    internal class ObjectSignal : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string Operative { get; private set; }
        public string KKS { get; private set; }
        public string ObjectType { get; private set; }
        public string ObjectName { get; private set; }

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
                case ConstCol.ColumnNameID:
                    ID = value;
                    break;
                case ConstCol.ColumnNameCPU:
                    CPU = value;
                    break;
                case ConstCol.ColumnNameKKS:
                    KKS = value;
                    break;
                case ConstCol.ColumnNameOperative:
                    Operative = value;
                    break;
                case ConstCol.ColumnNameObjectType:
                    ObjectType = value;
                    break;
                case ConstCol.ColumnNameObjectName:
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
                case ConstCol.ColumnNameID:
                    _returnValue = ID;
                    break;
                case ConstCol.ColumnNameCPU:
                    _returnValue = CPU;
                    break;
                case ConstCol.ColumnNameKKS:
                    _returnValue = KKS;
                    break;
                case ConstCol.ColumnNameOperative:
                    _returnValue = Operative;
                    break;
                case ConstCol.ColumnNameObjectType:
                    _returnValue = ObjectType;
                    break;
                case ConstCol.ColumnNameObjectName:
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
        /// *Has KKS
        /// *Has object name
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            bool _returnValue = true;

            if (KKS == "" || KKS == null)
                _returnValue = false;
            else if (ObjectName == "" || ObjectName == null)
                _returnValue = false;

            return _returnValue;
        }
    }

    internal class ObjectsClass : GeneralClass<ObjectSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(ConstCol.ColumnNameID, SettingsObject.Default.ObjectsColumnID,true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCPU, SettingsObject.Default.ObjectsColumnCPU, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKS, SettingsObject.Default.ObjectsColumnKKS, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameOperative, SettingsObject.Default.ObjectsColumnOperative, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectType, SettingsObject.Default.ObjectsColumnObjectType, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectName, SettingsObject.Default.ObjectsColumnObjectName, false));

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            SettingsObject.Default.ObjectsColumnID = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameID);
            SettingsObject.Default.ObjectsColumnCPU = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameCPU);
            SettingsObject.Default.ObjectsColumnKKS = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameKKS);
            SettingsObject.Default.ObjectsColumnOperative = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameOperative);
            SettingsObject.Default.ObjectsColumnObjectType = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameObjectType);
            SettingsObject.Default.ObjectsColumnObjectName = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameObjectName);

            SettingsObject.Default.Save();
        }

        public ObjectsClass(ProgressIndication progress, DataGridView grid) : base("Object",false, FileExtensions.objects.ToString(), progress, grid)
        {
            
        }

        public ObjectsClass() : base("Object", false, FileExtensions.objects.ToString(), null, null)
        {

        }

        /// <summary>
        /// Get unique data signals
        /// </summary>
        /// <param name="data">data signals object</param>
        public void ExtractFromData(DataClass data)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ObjectGenerateUniqueData, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ObjectGenerateUniqueData, data.Signals.Count);

            UpdateColumnNumbers(data.BaseColumns.Columns);
            Signals.Clear();
            string _keyword = string.Empty;
            for (int _dataNumber = 0; _dataNumber < data.Signals.Count; _dataNumber++)
            {
                DataSignal _dataSignal = data.Signals.ElementAt(_dataNumber);

                if (_dataSignal.KKS != string.Empty)
                {
                    ObjectSignal _objectSignal = new ObjectSignal();

                    // go through all collumn in objects and send data to objects
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

            string _KKS = string.Empty;
            bool _found = false;

            // go through all objects
            for (int _objectNumber = Signals.Count-1; _objectNumber >= 0; _objectNumber--)
            {
                _found = false;
                _KKS = Signals.ElementAt(_objectNumber).KKS;

                //find if it repeats, if yes then delete element
                for (int _findNumber = Signals.Count-1; _findNumber >= 0; _findNumber--)
                {
                    if (Signals.ElementAt(_findNumber).KKS == _KKS)
                    {
                        if (_found)
                        {
                            Signals.RemoveAt(_findNumber);
                            break;
                        }
                        else
                            _found = true;
                    }
                }
            }

            Progress.HideProgressBar();

            debug.ToFile(Resources.ObjectGenerateUniqueData + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Set object data to data signals
        /// </summary>
        /// <param name="data">data signals</param>
        public void SendToData(DataClass data)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ObjectTransferToData, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ObjectTransferToData, data.Signals.Count);

            string _KKS = string.Empty;
            string _keyword = string.Empty;
            for (int _dataNumber = 0; _dataNumber < data.Signals.Count; _dataNumber++)
            {
                DataSignal _dataSignal = data.Signals.ElementAt(_dataNumber);

                _KKS = _dataSignal.KKS;
                // if data signal has KKS
                if (_KKS != string.Empty)
                {
                    for (int _objectNumber = Signals.Count - 1; _objectNumber >= 0; _objectNumber--)
                    {
                        ObjectSignal _objectSignal = Signals.ElementAt(_objectNumber);

                        // and finds this KKS in objects, then transfer all data from objects to data signals
                        if (_objectSignal.KKS == _KKS)
                        {
                            // go through all collumn in objects and send data to objects
                            foreach (GeneralColumn _column in Columns)
                            {
                                _keyword = _column.Keyword;
                                //do not transfer ID
                                if (_keyword != "ID")
                                    _dataSignal.SetValueFromString(_objectSignal.GetValueString(_keyword, false), _keyword);
                            }
                        }
                    }
                }
                Progress.UpdateProgressBar(_dataNumber);
            }
            debug.ToFile(Resources.ObjectTransferToData + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }
    }
}
