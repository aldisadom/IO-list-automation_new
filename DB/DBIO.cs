using IO_list_automation_new.Properties;
using IO_list_automation_new;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal class DBIOSignal : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string Operative { get; private set; }
        public string KKS { get; private set; }
        public string ObjectType { get; private set; }
        public string ObjectName { get; private set; }

        public DBIOSignal() : base()
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
                        _debug.ToPopUp("DBIOSignal.GetValueString " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
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

    internal class DBIO : GeneralClass<DBIOSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsObject.Default.ColumnID, true),
                new GeneralColumn(KeywordColumn.CPU, SettingsObject.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.KKS, SettingsObject.Default.ColumnKKS, false),
                new GeneralColumn(KeywordColumn.Operative, SettingsObject.Default.ColumnOperative, true),
                new GeneralColumn(KeywordColumn.ObjectType, SettingsObject.Default.ColumnObjectType, false),
                new GeneralColumn(KeywordColumn.ObjectName, SettingsObject.Default.ColumnObjectName, false),
            };

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {}

        public DBIO(ProgressIndication progress, DataGridView grid) : base("DB Language", false, ".langDB", progress, grid)
        {}
    }
}
