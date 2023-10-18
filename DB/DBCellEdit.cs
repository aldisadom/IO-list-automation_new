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
        private List<string> InputData {get; }
        private List<string> OutputData { get; }

        private DBChoices Choices = new DBChoices();

        private int StartX = 20;
        private int StartY = 30;
        private string PreviosValue = string.Empty;

        /// <summary>
        /// remove all elements from this.controll
        /// </summary>
        private void DeleteAllItem()
        {
            string _type = string.Empty;
            int i = 0;

            while (this.Controls.Count >0)
            {
                System.Windows.Forms.Control _item = this.Controls[0];
                _type = _item.GetType().Name;
                if (_item.GetType().Name.Contains("ComboBox"))
                {
                    System.Windows.Forms.ComboBox _box = ((System.Windows.Forms.ComboBox)_item);
                    _box.SelectedIndexChanged -= ComboboxValueChanged;
                    _box.DropDown -= ComboboxOpen;

                    this.Controls.Remove(_box);
                }
                else 
                    this.Controls.Remove(_item);
            }
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
                default:
                    //remove 1 element
                    list.RemoveAt(startIndex);
                    break;
            }
        }

        /// <summary>
        /// Combobox value changed event
        /// </summary>
        /// <param name="sender">combobox</param>
        /// <param name="e">event arguments</param>
        /// <exception cref="InvalidProgramException"></exception>
        private void ComboboxValueChanged(object sender, EventArgs e)
        {
            DropDownClass _box = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            string _currentValue = _box.SelectedKeyword();

            if (PreviosValue != _currentValue)
            {
                List<string> _inOut = ListCopy(OutputData);
                List<string> _list = SortElements();
                OutputData.Clear();

                //before element
                int _index = 0;
                for (int i = 0; i < _list.Count; i++)
                {
                    if (_list[i] == _box.GetName())
                    {
                        _index = i;
                        break;
                    }

                    DropDownClass _boxFound = new DropDownClass((System.Windows.Forms.ComboBox)this.Controls.Find(_list[i],false)[0]);
                    OutputData.Add(_boxFound.SelectedKeyword());
                }

                //delete elements
                DeleteOldElements(_index,_list,PreviosValue);

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
                    default:
                        OutputData.Add(_currentValue);
                        break;
                }

                //add after
                for (int i= _index; i < _list.Count; i++)
                {
                    DropDownClass _boxFound = new DropDownClass((System.Windows.Forms.ComboBox)this.Controls.Find(_list[i], false)[0]);
                    OutputData.Add(_boxFound.SelectedKeyword());
                }

                DeleteAllItem();
                List <string> _tmpOut = OutputData;
                
                DecodeAll(OutputData);
            }
            
        }

        /// <summary>
        /// Combobox open event to get previous value
        /// </summary>
        /// <param name="sender">combobox</param>
        /// <param name="e">event arguments</param>
        private void ComboboxOpen(object sender, EventArgs e)
        {
            DropDownClass _box = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            PreviosValue = _box.SelectedKeyword();
        }

        /// <summary>
        /// add aditional zeros before number for better sorting
        /// </summary>
        /// <param name="_input">value</param>
        /// <returns>formated string</returns>
        private string AddZeroes(int _input)
        {
            if (_input < 10)
                return ("000" + _input.ToString());
            else if (_input < 100)
                return ("00" + _input.ToString());
            else if (_input < 1000)
                return ("0" + _input.ToString());
            else
                return _input.ToString();
        }

        /// <summary>
        /// Add elements to form
        /// </summary>
        /// <param name="x">x location</param>
        /// <param name="y">y location </param>
        /// <param name="selectedText">selected text of combobox</param>
        /// <param name="labelText">Text of label</param>
        /// <param name="selectList">Combobox selection list</param>
        private void AddElement(int x, int y, string selectedText, string labelText, List<string> selectList)
        {
            int _offsetX = 150;
            int _offsetY = 50;

            // 
            // label1
            //
            if (labelText != null)
            {
                System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
                label1.AutoSize = true;
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
                label1.Location = new System.Drawing.Point(x * _offsetX + StartX, y * _offsetY + StartY);
                label1.Name = "Row" + AddZeroes(y) + "Col" + AddZeroes(x) + ":Label";
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
            comboBox1.Name = "Row"+ AddZeroes(y) + "Col" + AddZeroes(x) + ":Dropdown";
            comboBox1.Size = new System.Drawing.Size(120, 21);

            DropDownClass DropDowns = new DropDownClass(comboBox1);

            comboBox1.DisplayMember = "Name";
            //for showing only name of dropdown element
            comboBox1.Format += (s, e) =>
            {
                if (e.ListItem is DropDownClass.DropDownElement item)
                    e.Value = item.GetName();
            };
            
            if (selectList == null)
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;

                DropDowns.AddItemText(selectedText);
                comboBox1.SelectedIndex = comboBox1.Items.Count-1;
            }
            else
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                for (int i = 0; i < selectList.Count; i++)
                {
                    DropDowns.AddItemFull("", selectList[i]);
                    if (selectedText == selectList[i])
                        comboBox1.SelectedIndex = i;
                }

                //change index event
                comboBox1.SelectedIndexChanged += new System.EventHandler(ComboboxValueChanged);
                comboBox1.DropDown += new System.EventHandler(ComboboxOpen);
            }

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
        public int Decode(List<string> inputData, int index, string labelText,ref int column,ref int row)
        {
            int _index = index;
            switch (inputData[_index])
            {
                case ConstDBChoices.ChoiceTab:
                    AddElement(column, row, inputData[_index], labelText, Choices.ChoicesMain);
                    row++;
                    break;
                case ConstDBChoices.ChoiceText:
                    AddElement(column, row, inputData[_index], labelText, Choices.ChoicesMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index],null,null);
                    row++;
                    break;
                case ConstDBChoices.ChoiceObject:
                    AddElement(column, row, inputData[_index], labelText, Choices.ChoicesMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index],null, Choices.ObjectColumns);
                    row++;
                    break;
                case ConstDBChoices.ChoiceData:
                    AddElement(column, row, inputData[_index], labelText, Choices.ChoicesMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index], null, Choices.DataColumns);
                    row++;
                    break;
                case ConstDBChoices.ChoiceIO:
                    AddElement(column, row, inputData[_index], labelText, Choices.ChoicesMain);
                    _index++;
                    AddElement(column+1, row, inputData[_index], null, null);
                    row++;
                    break;
                case ConstDBChoices.ChoiceIf:
                    AddElement(column, row, inputData[_index], labelText, Choices.ChoicesMain);
                    _index++;
                    column++;
                    // variable
                    switch (inputData[_index])
                    {
                        case ConstDBChoices.ChoiceObject:
                            AddElement(column, row, inputData[_index],null, Choices.ChoicesIf);
                            _index++;
                            AddElement(column+1, row, inputData[_index],null, Choices.ObjectColumns);
                            break;
                        case ConstDBChoices.ChoiceData:
                            AddElement(column, row, inputData[_index], null, Choices.ChoicesIf);
                            _index++;
                            AddElement(column+1, row, inputData[_index], null, Choices.DataColumns);
                            break;
                        case ConstDBChoices.ChoiceIO:
                            AddElement(column, row, inputData[_index], null, Choices.ChoicesIf);
                            _index++;
                            AddElement(column+1, row, inputData[_index],null,null);
                            break;
                    }
                    column++;
                    row++;
                    _index++;
                    //if statement
                    AddElement(column, row, inputData[_index], "Statement", Choices.ChoicesIfStatement);

                    column++;
                    _index++;
                    //true statement
                    _index = Decode(inputData, _index, "true",ref column, ref row);

                    _index++;
                    //false statement
                    _index = Decode(inputData, _index,"false", ref column, ref row);

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
                i = Decode(inputData, i,null,ref x,ref y);

            AddElement(x, y,ConstDBChoices.ChoiceNone,null, Choices.ChoicesMain);

            List<string> sortas = SortElements();
            this.ResumeLayout();
            this.Refresh();
        }

        /// <summary>
        /// Copy one list to another
        /// </summary>
        /// <param name="input">input list</param>
        /// <returns>new list</returns>
        public List<string> ListCopy(List<string> input)
        {
            List<string> _list = new List<string>();

            for (int i = 0; i < input.Count; i++)
                _list.Add(input[i]);

            return _list;
        }

        public DBCellEdit(List<string> inputData)
        {
            InputData = inputData;
            OutputData = ListCopy(inputData);

            InitializeComponent();

            DecodeAll (inputData);
        }
    }
}
