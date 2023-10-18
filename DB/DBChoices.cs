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
        public List<string> ChoicesIf { get; }
        public List<string> ChoicesIfStatement { get; }
        public List<string> DataColumns { get; }
        public List<string> ObjectColumns { get; }

        public DBChoices()
        {
            ChoicesMain = new List<string>();
            ChoicesIf = new List<string>();
            ChoicesIfStatement = new List<string>();
            DataColumns = new List<string>();
            ObjectColumns = new List<string>();

            ChoicesMain.Add(ConstDBChoices.ChoiceNone);
            ChoicesMain.Add(ConstDBChoices.ChoiceIf);
            ChoicesMain.Add(ConstDBChoices.ChoiceTab);
            ChoicesMain.Add(ConstDBChoices.ChoiceText);
            ChoicesMain.Add(ConstDBChoices.ChoiceObject);
            ChoicesMain.Add(ConstDBChoices.ChoiceData);
            ChoicesMain.Add(ConstDBChoices.ChoiceIO);

            ChoicesIf.Add(ConstDBChoices.ChoiceObject);
            ChoicesIf.Add(ConstDBChoices.ChoiceData);
            ChoicesIf.Add(ConstDBChoices.ChoiceIO);

            ChoicesIfStatement.Add(ConstDBChoices.ChoiceIsEmpty);
            ChoicesIfStatement.Add(ConstDBChoices.ChoiceIsNotEmpty);

            DataClass Data = new DataClass();
            ObjectsClass Objects = new ObjectsClass();


            foreach (GeneralColumn _column in Data.BaseColumns.Columns)
                DataColumns.Add(_column.Keyword);

            foreach (GeneralColumn _column in Objects.BaseColumns.Columns)
                ObjectColumns.Add(_column.Keyword);
        }

    }
}
