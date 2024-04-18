using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

            DataGridView dataGrid = new DataGridView();
            DataClass data = new DataClass(null, dataGrid);
            DataColumns = data.Columns.Columns.Select(c => c.Key).ToList();

            DataGridView objectsGrid = new DataGridView();
            ObjectsClass objects = new ObjectsClass(null, objectsGrid);
            ObjectColumns = objects.Columns.Columns.Select(c => c.Key).ToList();

            DataGridView modulesGrid = new DataGridView();
            ModuleClass modules = new ModuleClass(null, modulesGrid);
            ModuleColumns = modules.Columns.Columns.Select(c => c.Key).ToList();
        }
    }
}