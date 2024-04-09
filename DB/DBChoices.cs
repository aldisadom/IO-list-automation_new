using System.Collections.Generic;

namespace IO_list_automation_new.DB
{
    internal class DBChoices
    {
        //only for objects--------------------------------------
        public List<string> ChoicesObjectsMain { get; }

        public List<string> ChoicesObjectsIfStatement { get; }
        public List<string> ChoicesObjectsIfCondition { get; }

        //only for modules--------------------------------------
        public List<string> ChoicesModulesMain { get; }

        public List<string> ChoicesModulesIfStatement { get; }
        public List<string> ChoicesModulesIfCondition { get; }

        //only for address--------------------------------------
        public List<string> ChoicesSCADAMain { get; }

        public List<string> ChoicesSCADAIfStatement { get; }
        public List<string> ChoicesSCADAIfCondition { get; }

        //for all--------------------------------------
        public List<string> ChoicesIfConditions { get; }

        public List<string> Data_Columns { get; }
        public List<string> ObjectColumns { get; }
        public List<string> ModuleColumns { get; }

        public DBChoices()
        {
            //only for objects--------------------------------------
            ChoicesObjectsMain = new List<string>()
            {
                KeywordDBChoices.None,
                KeywordDBChoices.Insert,
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.BaseAddress,
                KeywordDBChoices.Address,
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
            };

            ChoicesObjectsIfStatement = new List<string>()
            {
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.BaseAddress,
                KeywordDBChoices.Address,
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
                KeywordDBChoices.MultiLine,
            };

            ChoicesObjectsIfCondition = new List<string>()
            {
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
            };

            //only for modules--------------------------------------
            ChoicesModulesMain = new List<string>()
            {
                KeywordDBChoices.None,
                KeywordDBChoices.Insert,
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.BaseAddress,
                KeywordDBChoices.Address,
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
            };

            ChoicesModulesIfStatement = new List<string>()
            {
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.BaseAddress,
                KeywordDBChoices.Address,
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.MultiLine,
            };

            ChoicesModulesIfCondition = new List<string>()
            {
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
            };

            //only for address--------------------------------------
            ChoicesSCADAMain = new List<string>()
            {
                KeywordDBChoices.None,
                KeywordDBChoices.Insert,
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.Data,
                KeywordDBChoices.GetBaseAddress,
                KeywordDBChoices.Address,
                KeywordDBChoices.AddressArea,
                KeywordDBChoices.CPU,
                KeywordDBChoices.ObjectName,
            };

            ChoicesSCADAIfStatement = new List<string>()
            {
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.Data,
                KeywordDBChoices.Address,
                KeywordDBChoices.AddressArea,
                KeywordDBChoices.CPU,
                KeywordDBChoices.ObjectName,
                KeywordDBChoices.MultiLine,
            };

            ChoicesSCADAIfCondition = new List<string>()
            {
                KeywordDBChoices.Data,
                KeywordDBChoices.BaseAddress,
                KeywordDBChoices.Address,
                KeywordDBChoices.AddressArea,
                KeywordDBChoices.GetBaseAddress,
                KeywordDBChoices.CPU,
                KeywordDBChoices.ObjectName,
            };

            //for all--------------------------------------
            ChoicesIfConditions = new List<string>()
            {
                KeywordDBChoices.IsNotEmpty,
                KeywordDBChoices.IsEmpty,
                KeywordDBChoices.Equal,
                KeywordDBChoices.nEqual,
                KeywordDBChoices.GreaterEqual,
                KeywordDBChoices.Greater,
                KeywordDBChoices.Less,
                KeywordDBChoices.LessEqual,
            };

            DataClass Data_ = new DataClass();
            Data_Columns = new List<string>();
            foreach (GeneralColumn column in Data_.BaseColumns.Columns)
                Data_Columns.Add(column.Keyword);

            ObjectsClass Objects = new ObjectsClass();
            ObjectColumns = new List<string>();
            foreach (GeneralColumn column in Objects.BaseColumns.Columns)
                ObjectColumns.Add(column.Keyword);

            ModuleClass Modules = new ModuleClass();
            ModuleColumns = new List<string>();
            foreach (GeneralColumn column in Modules.BaseColumns.Columns)
                ModuleColumns.Add(column.Keyword);
        }
    }
}