using IO_list_automation_new.General;

namespace IO_list_automation_new.Modules
{
    internal class ModuleSignal : GeneralSignal
    {
        public string CPU { get; private set; }
        public string ID { get; private set; }
        public string ModuleName { get; private set; }
        public string ModuleType { get; private set; }
        public string Cabinet { get; private set; }

        public string UniqueModuleName
        { get { return Cabinet + "_" + ModuleName + "_" + CPU; } }

        public ModuleSignal() : base()
        {
            ID = string.Empty;
            CPU = string.Empty;
            ModuleName = string.Empty;
            ModuleType = string.Empty;
            Cabinet = string.Empty;
        }

        /// <summary>
        /// Checks if design signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            return true;
        }
    }
}
