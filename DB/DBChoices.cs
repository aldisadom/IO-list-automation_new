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
            ChoicesMainObjects = new List<string>();
            ChoicesMainObjectsNoEmpty = new List<string>();
            ChoicesIfObjects = new List<string>();

            ChoicesMainModules = new List<string>();
            ChoicesMainModulesNoEmpty = new List<string>();
            ChoicesIfModules = new List<string>();

            ChoicesIfStatement = new List<string>();
            DataColumns = new List<string>();
            ObjectColumns = new List<string>();
            ModuleColumns = new List<string>();

            //only for objects
            ChoicesMainObjects.Add(KeywordDBChoices.None);
            ChoicesMainObjects.Add(KeywordDBChoices.If);
            ChoicesMainObjects.Add(KeywordDBChoices.Tab);
            ChoicesMainObjects.Add(KeywordDBChoices.Text);
            ChoicesMainObjects.Add(KeywordDBChoices.Index);
            ChoicesMainObjects.Add(KeywordDBChoices.Object);
            ChoicesMainObjects.Add(KeywordDBChoices.Data);
            ChoicesMainObjects.Add(KeywordDBChoices.IO);
            ChoicesMainObjects.Add(KeywordDBChoices.TagType);

            ChoicesMainObjectsNoEmpty = GeneralFunctions.ListCopy(ChoicesMainObjects);
            ChoicesMainObjectsNoEmpty.RemoveAt(0);

            ChoicesIfObjects.Add(KeywordDBChoices.Object);
            ChoicesIfObjects.Add(KeywordDBChoices.Data);
            ChoicesIfObjects.Add(KeywordDBChoices.IO);

            //only for modules
            ChoicesMainModules.Add(KeywordDBChoices.None);
            ChoicesMainModules.Add(KeywordDBChoices.If);
            ChoicesMainModules.Add(KeywordDBChoices.Tab);
            ChoicesMainModules.Add(KeywordDBChoices.Text);
            ChoicesMainModules.Add(KeywordDBChoices.Index);
            ChoicesMainModules.Add(KeywordDBChoices.Modules);
            ChoicesMainModules.Add(KeywordDBChoices.Data);
            ChoicesMainModules.Add(KeywordDBChoices.IO);

            ChoicesMainModulesNoEmpty = GeneralFunctions.ListCopy(ChoicesMainModules);
            ChoicesMainModulesNoEmpty.RemoveAt(0);

            ChoicesIfModules.Add(KeywordDBChoices.Data);
            ChoicesIfModules.Add(KeywordDBChoices.Modules);
            ChoicesIfModules.Add(KeywordDBChoices.IO);

            //for all
            ChoicesIfStatement.Add(KeywordDBChoices.IsEmpty);
            ChoicesIfStatement.Add(KeywordDBChoices.IsNotEmpty);

            DataClass Data = new DataClass();
            ObjectsClass Objects = new ObjectsClass();
            ModuleClass Modules = new ModuleClass();

            foreach (GeneralColumn _column in Data.BaseColumns.Columns)
                DataColumns.Add(_column.Keyword);

            foreach (GeneralColumn _column in Objects.BaseColumns.Columns)
                ObjectColumns.Add(_column.Keyword);

            foreach (GeneralColumn _column in Modules.BaseColumns.Columns)
                ModuleColumns.Add(_column.Keyword);
        }
    }
}