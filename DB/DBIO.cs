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
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(ConstCol.ColumnNameID, SettingsObject.Default.ObjectsColumnID, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCPU, SettingsObject.Default.ObjectsColumnCPU, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKS, SettingsObject.Default.ObjectsColumnKKS, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameOperative, SettingsObject.Default.ObjectsColumnOperative, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectType, SettingsObject.Default.ObjectsColumnObjectType, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectName, SettingsObject.Default.ObjectsColumnObjectName, false));

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {

        }

        public DBIO(ProgressIndication progress, DataGridView grid) : base("DB Language", false, ".langDB", progress, grid)
        {

        }
    }
}
