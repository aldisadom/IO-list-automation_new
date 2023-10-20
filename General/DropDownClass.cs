using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IO_list_automation_new.General
{
    enum DropDownElementType
    {
        Mod,
        Keyword,
        Name,
        FullName,
    }

    internal class DropDownElement
    {
        public string Mod { get; }
        public string Name { get; }
        public string Keyword { get; }
        public string FullName { get; }

        public DropDownElement(string mod, string keyword, string name)
        {
            Mod = mod;
            Keyword = keyword;
            Name = name;
            FullName = Mod + Name;
        }

        

    }
    internal class ComboboxTag
    {
        public ComboboxType Type { get; }
        public string PreviousValue { get; set; }

        public ComboboxTag(ComboboxType type, string previousValue)
        {
            Type = type;
            PreviousValue = previousValue;
        }
    }

    internal class DropDownClass
    {
        public ComboboxTag Tag
        {
            get
            {
                return (ComboboxTag)Element.Tag;
            }
        }

        public string Name
        {
            get { return Element.Name; }
        }

        public bool Visible
        {
            set { Element.Visible = value; }
            get {return Element.Visible;}
        }

        public int SelectedIndex
        {
            set { Element.SelectedIndex = value; }
            get { return Element.SelectedIndex; }
        }

        public Point Location
        {
            set { Element.Location = value; }
            get { return Element.Location; }
        }        

        private GeneralColumnName ColumnNames = new GeneralColumnName();

        private System.Windows.Forms.ComboBox Element {get;set; }

        public DropDownClass(System.Windows.Forms.ComboBox element)
        {
            Element = element;
        }

        /// <summary>
        /// Clear all items of combobox
        /// </summary>
        public void ClearItems()
        {
            Element.Items.Clear();
        }

        public void Editable(bool editable)
        {
            if(editable)
            {
                Element.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                Element.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        public void SetTag(ComboboxType type, string previousValue)
        {
            Element.Tag = new ComboboxTag(type,previousValue);
        }

        /// <summary>
        /// Change display member of combobox
        /// </summary>
        /// <param name="displayMember">display member enum</param>
        public void ChangeDisplayMember(DropDownElementType displayMember)
        {
            Element.DisplayMember = displayMember.ToString();
        }

        /// <summary>
        /// Get selected item Name
        /// </summary>
        /// <returns>Name of selected element</returns>
        public string SelectedName ()
        {
            var _item = Element.SelectedItem;
            if (_item != null)
                return ((DropDownElement)_item).Name;
            else
                return Element.Text;
        }

        /// <summary>
        /// Get selected item keyword
        /// </summary>
        /// <returns>keyword of selected element</returns>
        public string SelectedKeyword()
        {
            var _item = Element.SelectedItem;

            if (_item != null)
                return ((DropDownElement)_item).Keyword;
            else
                return Element.Text;
        }

        /// <summary>
        /// Get selected item modification
        /// </summary>
        /// <returns>modification of selected element</returns>
        public string SelectedMod()
        {
            var _item = Element.SelectedItem;

            if (_item != null)
                return ((DropDownElement)_item).Mod;
            else
                return Element.Text;
        }

        /// <summary>
        /// Add dropdown element from all text
        /// </summary>
        /// <param name="text">name of item</param>
        public void AddItemText(string text)
        {
            DropDownElement _element = new DropDownElement("",text,text);
            Element.Items.Add(_element);
        }

        /// <summary>
        /// Add dropdown element from all available keywords
        /// </summary>
        /// <param name="mod">Name modificator</param>
        /// <param name="keyword">keyword of item</param>
        public void AddItemFull(string mod, string keyword)
        {
            DropDownElement _element = new DropDownElement(mod, keyword, ColumnNames.GetName(keyword));

            Element.Items.Add(_element);
        }

        /// <summary>
        /// Add dropdown elements from column
        /// </summary>
        /// <param name="mod">Name modificator</param>
        /// <param name="keyword">keyword of item</param>
        public void AddItemColumn(string mod, string keyword)
        {
            DropDownElement _element = new DropDownElement(mod, keyword, ColumnNames.GetColumnName(keyword, false));

            Element.Items.Add(_element);
        }

        /// <summary>
        /// Check if element is visible and slectedIndex is correct
        /// </summary>
        /// <returns>Visible and value sellected</returns>
        public bool ValidCheck()
        {
            return Element.Visible && Element.SelectedIndex >= 0;
        }
    }
}
