using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

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

    internal class ComboBoxTag
    {
        public ComboBoxType Type { get; }
        public string PreviousValue { get; set; }

        public ComboBoxTag(ComboBoxType type, string previousValue)
        {
            Type = type;
            PreviousValue = previousValue;
        }
    }

    internal class DropDownClass
    {
        public ComboBoxTag Tag
        {
            get
            {
                return (ComboBoxTag)Element.Tag;
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

        public EventHandler OpenEvent {set { Element.DropDown += value; } }
        public KeyPressEventHandler KeyPressEvent { set { Element.KeyPress += value; } }
        public EventHandler IndexChangedEvent { set { Element.SelectedIndexChanged += value; } }
        public EventHandler OpenEventRemove { set { Element.DropDown -= value; } }
        public KeyPressEventHandler KeyPressEventRemove { set { Element.KeyPress -= value; } }
        public EventHandler IndexChangedEventRemove { set { Element.SelectedIndexChanged -= value; } }

        private GeneralColumnName ColumnNames = new GeneralColumnName();

        public System.Windows.Forms.ComboBox Element {get;private set; }

        public DropDownClass(System.Windows.Forms.ComboBox element)
        {
            Element = element;
        }

        public DropDownClass(string name)
        {
            Element = new System.Windows.Forms.ComboBox();
            Element.FormattingEnabled = true;
            Element.Size = new System.Drawing.Size(120, 21);
            Element.Location = new System.Drawing.Point(20, 20);
            Element.Name = name;
        }

        /// <summary>
        /// Clear all items of comboBox
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

        public void SetTag(ComboBoxType type, string previousValue)
        {
            Element.Tag = new ComboBoxTag(type,previousValue);
        }

        /// <summary>
        /// Change display member of comboBox
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
            DropDownElement _element = new DropDownElement(string.Empty, text,text);
            Element.Items.Add(_element);
        }

        /// <summary>
        /// Add dropdown element from all available keywords
        /// </summary>
        /// <param name="mod">Name modificator</param>
        /// <param name="keyword">keyword of item</param>
        public void AddItemFull(string mod, string keyword)
        {
            DropDownElement _element = new DropDownElement(mod, keyword, ColumnNames.GetColumnOrChoicesName(keyword));

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
