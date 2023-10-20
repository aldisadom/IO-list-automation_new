using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static IO_list_automation_new.GeneralColumnName;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IO_list_automation_new.Forms
{
    public partial class DBCellEdit : Form
    {
        public List<string> OutputData { get; }

        private DBChoices Choices = new DBChoices();

        private int StartX = 20;
        private int StartY = 30;

        /// <summary>
        /// Combobox value changed event
        /// </summary>
        /// <param name="sender">combobox</param>
        /// <param name="e">event arguments</param>
        private void ComboboxValueChangedEvent(object sender, EventArgs e)
        {
            DropDownClass _box = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            string _currentValue = _box.SelectedKeyword();

            //combobox type is of type that need to change layout
            if (_box.Tag.Type != ComboboxType.Text || _box.Tag.Type != ComboboxType.Data || _box.Tag.Type != ComboboxType.Object)
            {
                //dropdown value changed
                if (_box.Tag.PreviousValue != _currentValue)
                {
                    GeneralFunctions _generalFunctions = new GeneralFunctions();
                    List<string> _list = SortElements();
                    OutputData.Clear();

                    //before element
                    int _index = 0;
                    for (int i = 0; i < _list.Count; i++)
                    {
                        if (_list[i] == _box.Name)
                        {
                            _index = i;
                            break;
                        }
                        //extracting keyword to list
                        OutputData.Add(GetDropDownSelectedKeyword(_list[i]));
                    }

                    //delete elements
                    DeleteOldElements(_index, _list, _box.Tag.PreviousValue);

                    //add new
                    switch (_currentValue)
                    {
                        case ConstDBChoices.ChoiceIf:
                            //if
                            OutputData.Add(ConstDBChoices.ChoiceIf);
                            //object
                            OutputData.Add(ConstDBChoices.ChoiceObject);
                            OutputData.Add(Choices.ObjectColumns[0]);
                            //statement
                            OutputData.Add(Choices.ChoicesIfStatement[0]);
                            //true object
                            OutputData.Add(ConstDBChoices.ChoiceText);
                            OutputData.Add("edit");
                            //false object
                            OutputData.Add(ConstDBChoices.ChoiceText);
                            OutputData.Add("edit");

                            break;
                        case ConstDBChoices.ChoiceTab:
                            OutputData.Add(ConstDBChoices.ChoiceTab);
                            break;
                        case ConstDBChoices.ChoiceNone:

                            break;
                        case ConstDBChoices.ChoiceText:
                            OutputData.Add(ConstDBChoices.ChoiceText);
                            OutputData.Add("edit");
                            break;
                        case ConstDBChoices.ChoiceData:
                            OutputData.Add(ConstDBChoices.ChoiceData);
                            OutputData.Add(Choices.DataColumns[0]);
                            break;
                        case ConstDBChoices.ChoiceObject:
                            OutputData.Add(ConstDBChoices.ChoiceObject);
                            OutputData.Add(Choices.ObjectColumns[0]);
                            break;
                        case ConstDBChoices.ChoiceIO:
                            OutputData.Add(ConstDBChoices.ChoiceIO);
                            OutputData.Add("ON");
                            break;
                    }

                    //add after
                    for (int i = _index; i < _list.Count; i++)
                        OutputData.Add(GetDropDownSelectedKeyword(_list[i]));

                    DeleteAllItem();
                    DecodeAll(OutputData);
                }
            }
        }

        /// <summary>
        /// Read all comboboxes and update Output Data
        /// </summary>
        private void UpdateOutputData()
        {
            List<string> _list = SortElements();
            OutputData.Clear();

            for (int i = 0; i < _list.Count; i++)
                OutputData.Add(GetDropDownSelectedKeyword(_list[i]));
        }

        /// <summary>
        /// Combobox opened event, to get previous value
        /// </summary>
        /// <param name="sender">combobox</param>
        /// <param name="e">event arguments</param>
        private void ComboboxOpenEvent(object sender, EventArgs e)
        {
            DropDownClass _box = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            if (_box.Tag == null)
            {
                _box.SetTag(ComboboxType.Text, _box.SelectedKeyword());
            }
            else
                _box.Tag.PreviousValue = _box.SelectedKeyword();
        }

        /// <summary>
        /// remove all elements from this.controll
        /// </summary>
        private void DeleteAllItem()
        {
            this.SuspendLayout();
            while (this.Controls.Count >0)
            {
                System.Windows.Forms.Control _item = this.Controls[0];

                if (_item.GetType().Name.Contains("ComboBox"))
                {
                    System.Windows.Forms.ComboBox _box = ((System.Windows.Forms.ComboBox)_item);
                    _box.SelectedIndexChanged -= ComboboxValueChangedEvent;
                    _box.DropDown -= ComboboxOpenEvent;

                    this.Controls.Remove(_box);
                }
                else 
                    this.Controls.Remove(_item);
            }
            this.ResumeLayout();
        }

        /// <summary>
        /// Find object by name and return selected keyword of combobox
        /// </summary>
        /// <param name="name">object name</param>
        /// <returns>combobox selected keyword</returns>
        private string GetDropDownSelectedKeyword(string name)
        {
            DropDownClass _boxFound = new DropDownClass((System.Windows.Forms.ComboBox)this.Controls.Find(name, false)[0]);
            return _boxFound.SelectedKeyword();
        }

        /// <summary>
        /// Create and sort all comboboxes names
        /// </summary>
        /// <returns>comboboxes names</returns>
        private List<string> SortElements()
        {
            List<string> _list = new List<string>();
            string _type = string.Empty;

            foreach (var item in this.Controls)
            {
                _type = item.GetType().Name;
                if (item.GetType().Name.Contains("ComboBox"))
                    _list.Add(((System.Windows.Forms.ComboBox)item).Name);
            }
            _list.Sort();

            return _list;
        }

        /// <summary>
        /// Deleting all items based on type combobox current value
        /// </summary>
        /// <param name="startIndex">combobox index from list to be deleted</param>
        /// <param name="list">combobox list</param>
        /// <param name="cellValue">current cell value</param>
        private void DeleteOldElements(int startIndex, List<string> list, string cellValue)
        {
            switch (cellValue)
            {
                case ConstDBChoices.ChoiceIf:
                    //remove If element
                    list.RemoveAt(startIndex);
                    //remove variable
                    DeleteOldElements(startIndex, list, GetDropDownSelectedKeyword(list[startIndex]));
                    //remove statement
                    list.RemoveAt(startIndex);
                    //remove true variable
                    DeleteOldElements(startIndex, list, GetDropDownSelectedKeyword(list[startIndex]));
                    //remove false variable
                    DeleteOldElements(startIndex, list, GetDropDownSelectedKeyword(list[startIndex]));
                    break;

                case ConstDBChoices.ChoiceTab:
                    //remove 1 element
                    list.RemoveAt(startIndex);
                    break;

                case ConstDBChoices.ChoiceNone:
                    //remove 1 element
                    list.RemoveAt(startIndex);
                    break;

                case ConstDBChoices.ChoiceText:
                    //remove 2 elements
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                case ConstDBChoices.ChoiceData:
                    //remove 2 elements
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                case ConstDBChoices.ChoiceObject:
                    //remove 2 elements
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                case ConstDBChoices.ChoiceIO:
                    //remove 2 elements
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;
            }
        }

        /// <summary>
        /// Add elements to form
        /// </summary>
        /// <param name="x">x location</param>
        /// <param name="y">y location </param>
        /// <param name="selectedText">selected text of combobox</param>
        /// <param name="labelText">Text of label</param>
        /// <param name="selectList">Combobox selection list</param>
        private void AddElement(int x, int y, string selectedText, string labelText, List<string> selectList, ComboboxType elementType)
        {
            int _offsetX = 150;
            int _offsetY = 50;
            GeneralFunctions _generalFunctions = new GeneralFunctions();

            // 
            // label1
            //
            if (labelText != null)
            {
                System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
                label1.AutoSize = true;
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
                label1.Location = new System.Drawing.Point(x * _offsetX + StartX, y * _offsetY + StartY);
                label1.Name = "Row" + _generalFunctions.AddZeroes(y) + "Col" + _generalFunctions.AddZeroes(x) + ":Label";
                label1.Size = new System.Drawing.Size(46, 17);
                label1.Text = labelText;
                this.Controls.Add(label1);
            }
            // 
            // comboBox1
            //
            System.Windows.Forms.ComboBox comboBox1 = new System.Windows.Forms.ComboBox();
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(x * _offsetX + StartX, y * _offsetY + StartY + 20);
            comboBox1.Name = "Row"+ _generalFunctions.AddZeroes(y) + "Col" + _generalFunctions.AddZeroes(x) + ":Dropdown";
            comboBox1.Size = new System.Drawing.Size(120, 21);

            
            //combobox open event to get previous value
 //           if (elementType != ComboboxType.Text || elementType != ComboboxType.Data || elementType != ComboboxType.Object)
 //               comboBox1.DropDown += new System.EventHandler(ComboboxOpenEvent);

            DropDownClass DropDowns = new DropDownClass(comboBox1);
            DropDowns.SetTag(elementType, selectedText);
            DropDowns.ChangeDisplayMember(DropDownElementType.Name);

            if (selectList == null)
            {
                DropDowns.Editable(true);

                DropDowns.AddItemText(selectedText);
                DropDowns.SelectedIndex = 0;
            }
            else
            {
                DropDowns.Editable(false);
                for (int i = 0; i < selectList.Count; i++)
                {
                    DropDowns.AddItemFull("", selectList[i]);
                    if (selectedText == selectList[i])
                        DropDowns.SelectedIndex = i;
                }
            }

            //combobox open event to get previous value
            if (elementType != ComboboxType.Text || elementType != ComboboxType.Data || elementType != ComboboxType.Object)
                comboBox1.DropDown += new System.EventHandler(ComboboxOpenEvent);

            //change index event
            comboBox1.SelectedIndexChanged += new System.EventHandler(ComboboxValueChangedEvent);

            this.Controls.Add(comboBox1);
        }

        /// <summary>
        /// Decode one element
        /// </summary>
        /// <param name="inputData">all element list</param>
        /// <param name="index">index of curent element</param>
        /// <param name="labelText">label text of element</param>
        /// <param name="column">column index of element</param>
        /// <param name="row">row index of element</param>
        /// <returns></returns>
        public int Decode(List<string> inputData, int index, string labelText,ref int column,ref int row, bool emptySelectionAvailable)
        {
            int _index = index;
            List<string> _choicesMain;
            ComboboxType _typeMain;

            if (emptySelectionAvailable)
            {
                _choicesMain = Choices.ChoicesMain;
                _typeMain = ComboboxType.Main;

            }
            else
            {
                _choicesMain = Choices.ChoicesMainNoEmpty;
                _typeMain = ComboboxType.MainNoEmpty;
            }
            
            switch (inputData[_index])
            {
                case ConstDBChoices.ChoiceTab:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    row++;
                    break;
                case ConstDBChoices.ChoiceText:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index],null,null, ComboboxType.Text);
                    row++;
                    break;
                case ConstDBChoices.ChoiceObject:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index],null, Choices.ObjectColumns, ComboboxType.Object);
                    row++;
                    break;
                case ConstDBChoices.ChoiceData:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index], null, Choices.DataColumns, ComboboxType.Data);
                    row++;
                    break;
                case ConstDBChoices.ChoiceIO:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index], null, null, ComboboxType.Text);
                    row++;
                    break;
                case ConstDBChoices.ChoiceIf:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    column++;
                    // variable
                    switch (inputData[_index])
                    {
                        case ConstDBChoices.ChoiceObject:
                            AddElement(column, row, inputData[_index],null, Choices.ChoicesIf, ComboboxType.If);
                            _index++;
                            AddElement(column+1, row, inputData[_index],null, Choices.ObjectColumns, ComboboxType.Object);
                            break;
                        case ConstDBChoices.ChoiceData:
                            AddElement(column, row, inputData[_index], null, Choices.ChoicesIf, ComboboxType.If);
                            _index++;
                            AddElement(column+1, row, inputData[_index], null, Choices.DataColumns, ComboboxType.Data);
                            break;
                        case ConstDBChoices.ChoiceIO:
                            AddElement(column, row, inputData[_index], null, Choices.ChoicesIf, ComboboxType.If);
                            _index++;
                            AddElement(column+1, row, inputData[_index],null,null, ComboboxType.Text);
                            break;
                    }
                    column++;
                    row++;
                    _index++;
                    //if statement
                    AddElement(column, row, inputData[_index], "Statement", Choices.ChoicesIfStatement, ComboboxType.IfStatement);

                    column++;
                    _index++;
                    //true statement
                    _index = Decode(inputData, _index, "true",ref column, ref row,false);

                    _index++;
                    //false statement
                    _index = Decode(inputData, _index,"false", ref column, ref row,false);

                    column -= 3;
                    break;

            }
            return _index;
        }

        /// <summary>
        /// Decode all input data
        /// </summary>
        /// <param name="inputData">DB line</param>
        public void DecodeAll(List<string> inputData)
        {
            this.SuspendLayout();
            int x = 0;
            int y = 0;
            for (int i = 0; i < inputData.Count; i++)
                i = Decode(inputData, i, null, ref x, ref y, true);

            AddElement(x, y,ConstDBChoices.ChoiceNone,null, Choices.ChoicesMain, ComboboxType.Main);

            List<string> sortas = SortElements();
            this.ResumeLayout();
            this.Refresh();
        }

        public DBCellEdit(List<string> inputData)
        {
            GeneralFunctions _generalFunctions = new GeneralFunctions();
            OutputData = _generalFunctions.ListCopy(inputData);

            InitializeComponent();
            DecodeAll (inputData);
        }

        private void DBCellEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateOutputData();
        }
    }
}
