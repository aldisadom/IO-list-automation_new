using IO_list_automation_new.Helper_functions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IO_list_automation_new.General
{
    public enum ComboBoxType
    {
        Main,
        IfCondition,
        Data_,
        Object,
        Module,
        Text,
        Number,
    }

    public enum DropDownElementType
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

        public DropDownElement(string mod, string separator, string keyword, string name)
        {
            Mod = mod;
            Keyword = keyword;
            Name = name;
            if (!string.IsNullOrEmpty(Mod) && !string.IsNullOrEmpty(Name))
                FullName = Mod + separator + Name;
            else
                FullName = Mod + Name;
        }
    }

    internal class ComboBoxTag
    {
        public ComboBoxType Type { get; }
        public string PreviousValue { get; }

        public string Name { get; }

        public int X { get; }

        public int Y { get; }

        public ComboBoxTag(ComboBoxType type, string name, string previousValue, int x, int y)
        {
            Type = type;
            Name = name;
            PreviousValue = previousValue;
            X = x;
            Y = y;
        }
    }

    internal class DropDownClass
    {
        private string Separator = ": ";

        public ComboBoxTag Tag
        {
            get { return (ComboBoxTag)Element.Tag; }
            set { Element.Tag = value; }
        }

        public string Name
        {
            get { return Element.Name; }
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

        public KeyPressEventHandler KeyPressEvent
        { set { Element.KeyPress += value; } }

        public KeyPressEventHandler KeyPressEventRemove
        { set { Element.KeyPress -= value; } }

        public EventHandler IndexChangedEvent
        { set { Element.SelectedIndexChanged += value; } }

        public EventHandler IndexChangedEventRemove
        { set { Element.SelectedIndexChanged -= value; } }

        public System.Windows.Forms.ComboBox Element { get; }

        public DropDownClass(System.Windows.Forms.ComboBox element)
        {
            Element = element;
        }

        public DropDownClass(string name)
        {
            Element = new System.Windows.Forms.ComboBox()
            {
                FormattingEnabled = true,
                Size = new System.Drawing.Size(120, 21),
                Location = new System.Drawing.Point(20, 20),
                Name = name,
            };
            ChangeDisplayMember(DropDownElementType.FullName);
        }

        /// <summary>
        /// Show element in front
        /// </summary>
        public void BringToFront()
        {
            Element.BringToFront();
        }

        public void Editable(bool editable)
        {
            Element.DropDownStyle = editable ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
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
        public string SelectedName()
        {
            var item = Element.SelectedItem;
            if (item != null)
                return ((DropDownElement)item).Name;
            else
                return Element.Text;
        }

        /// <summary>
        /// Get selected item keyword
        /// </summary>
        /// <returns>keyword of selected element</returns>
        public string SelectedKeyword()
        {
            var item = Element.SelectedItem;

            if (item != null)
                return ((DropDownElement)item).Keyword;
            else
                return Element.Text;
        }

        /// <summary>
        /// Get selected item modification
        /// </summary>
        /// <returns>modification of selected element</returns>
        public string SelectedMod()
        {
            var item = Element.SelectedItem;

            if (item != null)
                return ((DropDownElement)item).Mod;
            else
                return Element.Text;
        }

        /// <summary>
        /// Add dropdown element from all text
        /// </summary>
        /// <param name="text">name of item</param>
        public void AddItemText(string text)
        {
            DropDownElement element = new DropDownElement(string.Empty, Separator, text, text);
            Element.Items.Add(element);
        }

        /// <summary>
        /// Add dropdown element from all available keywords
        /// </summary>
        /// <param name="mod">Name modification</param>
        /// <param name="keyword">keyword of item</param>
        public void AddItemFull(string mod, string keyword)
        {
            DropDownElement element = new DropDownElement(mod, Separator, keyword, TextHelper.GetColumnOrChoicesName(keyword));

            Element.Items.Add(element);
        }

        /// <summary>
        /// Add dropdown elements from column
        /// </summary>
        /// <param name="mod">Name modification</param>
        /// <param name="keyword">keyword of item</param>
        public void AddItemColumn(string mod, string keyword)
        {
            DropDownElement element = new DropDownElement(mod, Separator, keyword, TextHelper.GetColumnName(keyword, false));

            Element.Items.Add(element);
        }

        /// <summary>
        /// Add dropdown elements custom
        /// </summary>
        /// <param name="mod">Name modification</param>
        /// <param name="keyword">keyword of item</param>
        public void AddItemCustom(string mod, string keyword)
        {
            DropDownElement element = new DropDownElement(mod, Separator, keyword, keyword);

            Element.Items.Add(element);
        }

        /// <summary>
        /// Check if element is visible and selectedIndex is correct
        /// </summary>
        /// <returns>Visible and value selected</returns>
        public bool ValidCheck()
        {
            return Element.Visible && Element.SelectedIndex >= 0;
        }
    }
}