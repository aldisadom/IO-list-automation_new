using IO_list_automation_new.Properties;
using System;

namespace IO_list_automation_new.Objects
{
    internal class ObjectSignal : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string Operative { get; private set; }
        public string KKS { get; private set; }
        public string ObjectType { get; private set; }
        public string ObjectName { get; private set; }

        public string UniqueKKS
        { get { return KKS + "_" + CPU; } }

        public ObjectSignal() : base()
        {
            ID = string.Empty;
            CPU = string.Empty;
            Operative = string.Empty;
            KKS = string.Empty;
            ObjectType = string.Empty;
            ObjectName = string.Empty;
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
}
