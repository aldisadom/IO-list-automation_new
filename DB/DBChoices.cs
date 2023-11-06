using IO_list_automation_new.General;
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

        public List<string> DataColumns { get; }
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
                KeywordDBChoices.BaseIndex,
                KeywordDBChoices.Address,
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
            };

            ChoicesObjectsIfStatement = new List<string>()
            {
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.BaseIndex,
                KeywordDBChoices.Address,
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
                KeywordDBChoices.MultiLine,
            };

            ChoicesObjectsIfCondition = new List<string>()
            {
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
            };

            //only for modules--------------------------------------
            ChoicesModulesMain = new List<string>()
            {
                KeywordDBChoices.None,
                KeywordDBChoices.Insert,
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.BaseIndex,
                KeywordDBChoices.Address,
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
            };

            ChoicesModulesIfStatement = new List<string>()
            {
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.BaseIndex,
                KeywordDBChoices.Address,
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
                KeywordDBChoices.MultiLine,
            };

            ChoicesModulesIfCondition = new List<string>()
            {
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
            };

            //only for address--------------------------------------
            ChoicesSCADAMain = new List<string>()
            {
                KeywordDBChoices.None,
                KeywordDBChoices.Insert,
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
            };

            ChoicesSCADAIfStatement = new List<string>()
            {
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
                KeywordDBChoices.MultiLine,
            };

            ChoicesSCADAIfCondition = new List<string>()
            {
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.IOPin,
                KeywordDBChoices.IOText,
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

            DataClass Data = new DataClass();
            DataColumns = new List<string>();
            foreach (GeneralColumn _column in Data.BaseColumns.Columns)
                DataColumns.Add(_column.Keyword);

            ObjectsClass Objects = new ObjectsClass();
            ObjectColumns = new List<string>();
            foreach (GeneralColumn _column in Objects.BaseColumns.Columns)
                ObjectColumns.Add(_column.Keyword);

            ModuleClass Modules = new ModuleClass();
            ModuleColumns = new List<string>();
            foreach (GeneralColumn _column in Modules.BaseColumns.Columns)
                ModuleColumns.Add(_column.Keyword);
        }
    }
}