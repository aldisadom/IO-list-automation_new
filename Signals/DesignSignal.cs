using IO_list_automation_new.General;
using System;

namespace IO_list_automation_new.Design
{
    internal class DesignSignal : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string KKS { get; private set; }
        public string RangeMin { get; private set; }
        public string RangeMax { get; private set; }
        public string Units { get; private set; }
        public string FalseText { get; private set; }
        public string TrueText { get; private set; }
        public string Revision { get; private set; }
        public string Cable { get; private set; }
        public string Cabinet { get; private set; }
        public string ModuleName { get; private set; }
        public string Pin { get; private set; }
        public string Channel { get; private set; }
        public string IOText { get; private set; }
        public string Page { get; private set; }
        public string Changed { get; private set; }
        public string Terminal { get; private set; }
        public string Tag { get; private set; }

        public string UniqueKKS
        { get { return KKS + "_" + CPU; } }

        public string UniqueModuleName
        { get { return Cabinet + "_" + ModuleName + "_" + CPU; } }

        public DesignSignal() : base()
        {
            ID = string.Empty;
            CPU = string.Empty;
            KKS = string.Empty;
            RangeMin = string.Empty;
            RangeMax = string.Empty;
            Units = string.Empty;
            FalseText = string.Empty;
            TrueText = string.Empty;
            Revision = string.Empty;
            Cable = string.Empty;
            Cabinet = string.Empty;
            ModuleName = string.Empty;
            Pin = string.Empty;
            Channel = string.Empty;
            IOText = string.Empty;
            Page = string.Empty;
            Changed = string.Empty;
            Terminal = string.Empty;
            Tag = string.Empty;
        }

        /// <summary>
        /// Checks if design signal is valid:
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            if (string.IsNullOrEmpty(ModuleName))
                return false;
            else if (string.IsNullOrEmpty(IOText))
                return false;
            else if (string.IsNullOrEmpty(Channel))
                return false;
            else if (!HasNumber(Channel))
                return false;

            return true;
        }

        /// <summary>
        /// Check if text has number
        /// </summary>
        /// <param name="text">text to check</param>
        /// <returns>return if has number in string</returns>
        private bool HasNumber(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                    return true;
            }
            return false;
        }
    }
}
