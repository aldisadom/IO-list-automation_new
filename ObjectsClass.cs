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
        public string UniqueKKS { get {return KKS + "_" + CPU; } }

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
                case KeywordColumn.ID:
                    ID = value;
                    break;
                case KeywordColumn.CPU:
                    CPU = value;
                    break;
                case KeywordColumn.KKS:
                    KKS = value;
                    break;
                case KeywordColumn.Operative:
                    Operative = value;
                    break;
                case KeywordColumn.ObjectType:
                    ObjectType = value;
                    break;
                case KeywordColumn.ObjectName:
                    ObjectName = value;
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
            string _returnValue = string.Empty;
            switch (parameterName)
            {
                case KeywordColumn.ID:
                    _returnValue = ID;
                    break;
                case KeywordColumn.CPU:
                    _returnValue = CPU;
                    break;
                case KeywordColumn.KKS:
                    _returnValue = KKS;
                    break;
                case KeywordColumn.Operative:
                    _returnValue = Operative;
                    break;
                case KeywordColumn.ObjectType:
                    _returnValue = ObjectType;
                    break;
                case KeywordColumn.ObjectName:
                    _returnValue = ObjectName;
                    break;
                default:
                    if (!suppressError)
                    {
                        Debug _debug = new Debug();
                        _debug.ToPopUp("ObjectsSignal.GetValueString " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    }
                    break;
            }
            return _returnValue;
        }

        /// <summary>
        /// Checks if design signal is valid:
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            if (string.IsNullOrEmpty(KKS))
                return false;
            else if (string.IsNullOrEmpty(ObjectName))
                return false;

            return true;
        }
    }

    internal class ObjectsClass : GeneralClass<ObjectSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsObject.Default.ColumnID,true),
                new GeneralColumn(KeywordColumn.CPU, SettingsObject.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.KKS, SettingsObject.Default.ColumnKKS, false),
                new GeneralColumn(KeywordColumn.Operative, SettingsObject.Default.ColumnOperative, true),
                new GeneralColumn(KeywordColumn.ObjectType, SettingsObject.Default.ColumnObjectType, false),
                new GeneralColumn(KeywordColumn.ObjectName, SettingsObject.Default.ColumnObjectName, false),
            };

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            SettingsObject.Default.ColumnID = _columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsObject.Default.ColumnCPU = _columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsObject.Default.ColumnKKS = _columns.GetColumnNumberFromKeyword(KeywordColumn.KKS);
            SettingsObject.Default.ColumnOperative = _columns.GetColumnNumberFromKeyword(KeywordColumn.Operative);
            SettingsObject.Default.ColumnObjectType = _columns.GetColumnNumberFromKeyword(KeywordColumn.ObjectType);
            SettingsObject.Default.ColumnObjectName = _columns.GetColumnNumberFromKeyword(KeywordColumn.ObjectName);

            SettingsObject.Default.Save();
        }

        public ObjectsClass(ProgressIndication progress, DataGridView grid) : base("Object",false, nameof(FileExtensions.objects), progress, grid)
        {}

        public ObjectsClass() : base("Object", false, nameof(FileExtensions.objects), null, null)
        {}

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
                DataSignal _dataSignal = data.Signals[_dataNumber];

                if (_dataSignal.HasKKS())
                    continue;

                ObjectSignal _objectSignal = new ObjectSignal();

                // go through all column in objects and send data to objects
                foreach (GeneralColumn _column in Columns)
                {
                    _keyword = _column.Keyword;
                    _objectSignal.SetValueFromString(_dataSignal.GetValueString(_keyword, true), _keyword);
                }

                if (_objectSignal.ValidateSignal())
                    Signals.Add(_objectSignal);

                Progress.UpdateProgressBar(_dataNumber);
            }

            // go through all objects
            for (int _objectNumber = Signals.Count - 1; _objectNumber >= 0; _objectNumber--)
            {
                //find if it repeats, if yes then delete element
                for (int _findNumber = _objectNumber - 1; _findNumber >= 0; _findNumber--)
                {
                    if (Signals[_findNumber].UniqueKKS == Signals[_objectNumber].UniqueKKS)
                    {
                        Signals.RemoveAt(_objectNumber);
                        break;
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

            string _UniqueName = string.Empty;
            string _keyword = string.Empty;
            for (int _dataNumber = 0; _dataNumber < data.Signals.Count; _dataNumber++)
            {
                DataSignal _dataSignal = data.Signals[_dataNumber];

                _UniqueName = _dataSignal.UniqueKKS;
                // if data signal has KKS
                if (string.IsNullOrEmpty(_UniqueName))
                    continue;

                for (int _objectNumber = Signals.Count - 1; _objectNumber >= 0; _objectNumber--)
                {
                    ObjectSignal _objectSignal = Signals[_objectNumber];

                    // and finds this KKS in objects in same cpu, then transfer all data from objects to data signals
                    if ((_objectSignal.UniqueKKS) != _UniqueName)
                        continue;

                    // go through all column in objects and send data to objects
                    foreach (GeneralColumn _column in Columns)
                    {
                        _keyword = _column.Keyword;
                        //do not transfer ID
                        if (_keyword == "ID")
                            continue;

                        _dataSignal.SetValueFromString(_objectSignal.GetValueString(_keyword, false), _keyword);
                    }
                }

                Progress.UpdateProgressBar(_dataNumber);
            }
            debug.ToFile(Resources.ObjectTransferToData + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }
    }
}
