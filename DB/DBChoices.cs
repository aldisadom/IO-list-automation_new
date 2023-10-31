using IO_list_automation_new.General;
using System.Collections.Generic;

namespace IO_list_automation_new.DB
{
    internal class DBChoices
    {
        //only for objects
        public List<string> ChoicesMainObjects { get; }

        public List<string> ChoicesMainObjectsNoEmpty { get; }
        public List<string> ChoicesIfObjects { get; }

        //only for modules
        public List<string> ChoicesMainModules { get; }

        public List<string> ChoicesMainModulesNoEmpty { get; }
        public List<string> ChoicesIfModules { get; }

        //for all
        public List<string> ChoicesIfStatement { get; }

        public List<string> DataColumns { get; }
        public List<string> ObjectColumns { get; }
        public List<string> ModuleColumns { get; }

        public DBChoices()
        {
            //only for objects
            ChoicesMainObjects = new List<string>()
            {
                KeywordDBChoices.None,
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.Index,
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.VariableType,
            };
            ChoicesMainObjectsNoEmpty = GeneralFunctions.ListCopy(ChoicesMainObjects);
            ChoicesMainObjectsNoEmpty.RemoveAt(0);

            ChoicesIfObjects = new List<string>()
            {
                KeywordDBChoices.Object,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
            };

            //only for modules
            ChoicesMainModules = new List<string>()
            {
                KeywordDBChoices.None,
                KeywordDBChoices.If,
                KeywordDBChoices.Tab,
                KeywordDBChoices.Text,
                KeywordDBChoices.Index,
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
                KeywordDBChoices.VariableType,
            };
            ChoicesMainModulesNoEmpty = GeneralFunctions.ListCopy(ChoicesMainModules);
            ChoicesMainModulesNoEmpty.RemoveAt(0);

            ChoicesIfModules = new List<string>()
            {
                KeywordDBChoices.Modules,
                KeywordDBChoices.Data,
                KeywordDBChoices.IOTag,
                KeywordDBChoices.IOChannel,
            };

            //for all
            ChoicesIfStatement = new List<string>()
            {
                KeywordDBChoices.IsEmpty,
                KeywordDBChoices.IsNotEmpty,
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