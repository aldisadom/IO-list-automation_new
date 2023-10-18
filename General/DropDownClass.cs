using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_list_automation_new.General
{
    internal class DropDownClass
    {

        private GeneralColumnName ColumnNames = new GeneralColumnName();

        private System.Windows.Forms.ComboBox Element {get;set; }

        public struct DropDownElement
        {
            private string Mod;
            private string Name;
            private string Keyword;

            public void Set(string mod, string name, string keyword)
            {
                Mod = mod;
                Name = name;
                Keyword = keyword;
            }
            public string GetName()
            {
                return (Mod+Name);
            }
            public string GetKeyword()
            {
                return Keyword;
            }
            public string GetMod()
            {
                return Mod;
            }
        }

        public DropDownClass(System.Windows.Forms.ComboBox element)
        {
            Element = element;
            Element.DisplayMember = "Name";
        }

        public string GetName()
        {
            return Element.Name;
        }

        public string SelectedName ()
        {
            string _returnText=string.Empty;
            var _item = Element.SelectedItem;
            if (_item != null)
            {
                if (_item.GetType().Name == "String")
                    return _item.ToString();
                else if (_item.GetType().Name == "DropDownElement")
                    return ((DropDownClass.DropDownElement)_item).GetName();
            }

            return "";
        }

        public string SelectedKeyword()
        {
            string _returnText = string.Empty;
            var _item = Element.SelectedItem;
            if (_item != null)
            {
                if (_item.GetType().Name == "String")
                    return _item.ToString();
                else if (_item.GetType().Name == "DropDownElement")
                    return ((DropDownClass.DropDownElement)_item).GetKeyword();
            }

            return "";
        }

        public string SelectedMod()
        {
            string _returnText = string.Empty;
            var _item = Element.SelectedItem;
            if (_item != null)
            {
                if (_item.GetType().Name == "String")
                    return _item.ToString();
                else if (_item.GetType().Name == "DropDownElement")
                    return ((DropDownClass.DropDownElement)_item).GetMod();
            }

            return "";
        }

        public void AddItemText(string text)
        {
            DropDownElement _element = new DropDownElement();
            _element.Set("", text, text);

            Element.Items.Add(_element);
        }

        public void AddItemFull(string mod, string keyword)
        {
            DropDownElement _element = new DropDownElement();
            _element.Set(mod, ColumnNames.GetName(keyword), keyword);

            Element.Items.Add(_element);
        }

        public void AddItemColumn(string mod, string keyword)
        {
            DropDownElement _element = new DropDownElement();
            _element.Set(mod, ColumnNames.GetColumnName(keyword, false), keyword);

            Element.Items.Add(_element);
        }

        public bool ValidCheck()
        {
            return Element.Visible && Element.SelectedIndex >= 0;
        }
    }
}
