using IO_list_automation_new.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_list_automation_new.DB
{
    internal class DBChoices
    {
        public List<string> ChoicesMain { get; }
        public List<string> ChoicesMainNoEmpty { get; }
        public List<string> ChoicesIf { get; }
        public List<string> ChoicesIfStatement { get; }
        public List<string> DataColumns { get; }
        public List<string> ObjectColumns { get; }

        public DBChoices()
        {
            ChoicesMain = new List<string>();
            ChoicesMainNoEmpty = new List<string>();
            ChoicesIf = new List<string>();
            ChoicesIfStatement = new List<string>();
            DataColumns = new List<string>();
            ObjectColumns = new List<string>();

            ChoicesMain.Add(KeywordDBChoices.None);
            ChoicesMain.Add(KeywordDBChoices.If);
            ChoicesMain.Add(KeywordDBChoices.Tab);
            ChoicesMain.Add(KeywordDBChoices.Text);
            ChoicesMain.Add(KeywordDBChoices.Index);
            ChoicesMain.Add(KeywordDBChoices.Object);
            ChoicesMain.Add(KeywordDBChoices.Data);
            ChoicesMain.Add(KeywordDBChoices.IO);
            ChoicesMain.Add(KeywordDBChoices.TagType);

            ChoicesMainNoEmpty = GeneralFunctions.ListCopy(ChoicesMain);
            ChoicesMainNoEmpty.RemoveAt(0);

            ChoicesIf.Add(KeywordDBChoices.Object);
            ChoicesIf.Add(KeywordDBChoices.Data);
            ChoicesIf.Add(KeywordDBChoices.IO);

            ChoicesIfStatement.Add(KeywordDBChoices.IsEmpty);
            ChoicesIfStatement.Add(KeywordDBChoices.IsNotEmpty);

            DataClass Data = new DataClass();
            ObjectsClass Objects = new ObjectsClass();


            foreach (GeneralColumn _column in Data.BaseColumns.Columns)
                DataColumns.Add(_column.Keyword);

            foreach (GeneralColumn _column in Objects.BaseColumns.Columns)
                ObjectColumns.Add(_column.Keyword);
        }

    }
}
