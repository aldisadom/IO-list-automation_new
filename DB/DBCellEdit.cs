using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IO_list_automation_new.Forms
{
    public partial class DBCellEdit : Form
    {
        public List<string> OutputData { get; }

        private DBChoices Choices = new DBChoices();

        private const int StartX = 20;
        private const int StartY = 30;

        private bool ModuleBased { get; set; }

        /// <summary>
        /// ComboBox value changed event
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">event arguments</param>
        private void ComboBox_ValueChangedEvent(object sender, EventArgs e)
        {
            DropDownClass _box = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            string _currentValue = _box.SelectedKeyword();

            //ComboBox type is of type that need to change layout
            if (_box.ChangeLayout)
            {
                //dropdown value changed
                if (_box.Tag.PreviousValue != _currentValue)
                {
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
                        case KeywordDBChoices.If:
                            //if
                            OutputData.Add(KeywordDBChoices.If);
                            //object
                            OutputData.Add(KeywordDBChoices.Data);
                            OutputData.Add(Choices.DataColumns[0]);
                            //statement
                            OutputData.Add(Choices.ChoicesIfStatement[0]);
                            //true object
                            OutputData.Add(KeywordDBChoices.Text);
                            OutputData.Add("edit");
                            //false object
                            OutputData.Add(KeywordDBChoices.Text);
                            OutputData.Add("edit");

                            break;

                        case KeywordDBChoices.Index:
                            OutputData.Add(KeywordDBChoices.Index);
                            //index memory area
                            OutputData.Add(string.Empty);
                            //index multiplier
                            OutputData.Add("1");
                            //index offset
                            OutputData.Add("0");
                            break;

                        case KeywordDBChoices.Tab:
                            OutputData.Add(KeywordDBChoices.Tab);
                            break;

                        case KeywordDBChoices.None:

                            break;

                        case KeywordDBChoices.Text:
                            OutputData.Add(KeywordDBChoices.Text);
                            OutputData.Add("edit");
                            break;

                        case KeywordDBChoices.Data:
                            OutputData.Add(KeywordDBChoices.Data);
                            OutputData.Add(Choices.DataColumns[0]);
                            break;

                        case KeywordDBChoices.Object:
                            OutputData.Add(KeywordDBChoices.Object);
                            OutputData.Add(Choices.ObjectColumns[0]);
                            break;

                        case KeywordDBChoices.Modules:
                            OutputData.Add(KeywordDBChoices.Modules);
                            OutputData.Add(Choices.ModuleColumns[0]);
                            break;

                        case KeywordDBChoices.IO:
                            OutputData.Add(KeywordDBChoices.IO);
                            OutputData.Add("ON");
                            break;

                        case KeywordDBChoices.TagType:
                            OutputData.Add(KeywordDBChoices.TagType);
                            OutputData.Add("Status");
                            break;

                        default:
                            Debug _debug = new Debug();
                            const string text = "DBCellEdit.ComboBoxValueChangedEvent";
                            _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _currentValue, DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(text + "." + _currentValue + " is not created for this element");
                    }

                    //add after
                    for (int i = _index; i < _list.Count; i++)
                        OutputData.Add(GetDropDownSelectedKeyword(_list[i]));

                    DeleteAllItem();
                    DecodeElementsAll(OutputData);
                }
            }
        }

        /// <summary>
        /// ComboBox accept only numbers
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">event arguments</param>
        private void ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key is not a digit and not a control key (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Block the input
        }

        /// <summary>
        /// ComboBox opened event, to get previous value
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">event arguments</param>
        private void ComboBox_OpenEvent(object sender, EventArgs e)
        {
            DropDownClass _box = new DropDownClass((System.Windows.Forms.ComboBox)sender);
            if (_box.Tag == null)
                _box.SetTag(ComboBoxType.Text, _box.SelectedKeyword());
            else
                _box.Tag.PreviousValue = _box.SelectedKeyword();
        }

        /// <summary>
        /// Read all comboBoxes and update Output Data
        /// </summary>
        private void UpdateOutputData()
        {
            List<string> _list = SortElements();
            OutputData.Clear();

            for (int i = 0; i < _list.Count; i++)
                OutputData.Add(GetDropDownSelectedKeyword(_list[i]));
        }

        /// <summary>
        /// remove all elements from this.control
        /// </summary>
        private void DeleteAllItem()
        {
            this.SuspendLayout();
            while (this.Controls.Count > 0)
            {
                System.Windows.Forms.Control _item = this.Controls[0];

                if (_item.GetType().Name.Contains("ComboBox"))
                {
                    DropDownClass DropDowns = new DropDownClass((System.Windows.Forms.ComboBox)_item)
                    {
                        IndexChangedEventRemove = ComboBox_ValueChangedEvent,
                        KeyPressEventRemove = ComboBox_KeyPress,
                        OpenEventRemove = ComboBox_OpenEvent,
                    };
                }
                this.Controls.Remove(_item);
            }
            this.ResumeLayout();
        }

        /// <summary>
        /// Find object by name and return selected keyword of comboBox
        /// </summary>
        /// <param name="name">object name</param>
        /// <returns>comboBox selected keyword</returns>
        private string GetDropDownSelectedKeyword(string name)
        {
            DropDownClass _boxFound = new DropDownClass((System.Windows.Forms.ComboBox)this.Controls.Find(name, false)[0]);
            return _boxFound.SelectedKeyword();
        }

        /// <summary>
        /// Create and sort all comboBoxes names
        /// </summary>
        /// <returns>comboBoxes names</returns>
        private List<string> SortElements()
        {
            List<string> _list = new List<string>();
            string _type;

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
        /// Deleting all items based on type comboBox current value
        /// </summary>
        /// <param name="startIndex">comboBox index from list to be deleted</param>
        /// <param name="list">comboBox list</param>
        /// <param name="cellValue">current cell value</param>
        private void DeleteOldElements(int startIndex, List<string> list, string cellValue)
        {
            switch (cellValue)
            {
                case KeywordDBChoices.If:
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

                //remove 4 element
                case KeywordDBChoices.Index:
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                //remove 2 element
                case KeywordDBChoices.TagType:
                case KeywordDBChoices.Text:
                case KeywordDBChoices.Data:
                case KeywordDBChoices.Object:
                case KeywordDBChoices.IO:
                case KeywordDBChoices.Modules:
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                //remove 1 element
                case KeywordDBChoices.Tab:
                case KeywordDBChoices.None:
                    list.RemoveAt(startIndex);
                    break;

                default:
                    Debug _debug = new Debug();
                    const string text = "DBCellEdit.DeleteOldElements";
                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + cellValue, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + cellValue + " is not created for this element");
            }
        }

        /// <summary>
        /// Add elements to form
        /// </summary>
        /// <param name="x">x location</param>
        /// <param name="y">y location </param>
        /// <param name="selectedText">selected text of comboBox</param>
        /// <param name="labelText">Text of label</param>
        /// <param name="selectList">ComboBox selection list</param>
        private void AddElement(int x, int y, string selectedText, string labelText, List<string> selectList, ComboBoxType elementType)
        {
            const int _offsetX = 150;
            const int _offsetY = 50;

            //
            // label1
            //
            if (labelText != null)
            {
                System.Windows.Forms.Label label1 = new System.Windows.Forms.Label()
                {
                    AutoSize = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10F),
                    Location = new System.Drawing.Point((x * _offsetX) + StartX, (y * _offsetY) + StartY),
                    Name = "Row" + GeneralFunctions.AddZeroes(y) + "Col" + GeneralFunctions.AddZeroes(x) + ":Label",
                    Size = new System.Drawing.Size(46, 17),
                    Text = labelText,
                };
                this.Controls.Add(label1);
            }
            //
            // comboBox1
            //

            DropDownClass DropDowns = new DropDownClass("Row" + GeneralFunctions.AddZeroes(y) + "Col" + GeneralFunctions.AddZeroes(x) + ":Dropdown");
            DropDowns.SetTag(elementType, selectedText);
            DropDowns.ChangeDisplayMember(DropDownElementType.Name);
            DropDowns.Location = new System.Drawing.Point((x * _offsetX) + StartX, (y * _offsetY) + StartY + 20);

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
                    DropDowns.AddItemFull(string.Empty, selectList[i]);
                    if (selectedText == selectList[i])
                        DropDowns.SelectedIndex = i;
                }
            }

            //comboBox open event to get previous value
            if (elementType != ComboBoxType.Text || elementType != ComboBoxType.Data || elementType != ComboBoxType.Object || elementType != ComboBoxType.Module)
                DropDowns.OpenEvent = ComboBox_OpenEvent;

            //to check if entered text is number
            if (elementType == ComboBoxType.Number)
                DropDowns.KeyPressEvent = ComboBox_KeyPress;

            //change index event
            DropDowns.IndexChangedEvent = ComboBox_ValueChangedEvent;

            this.Controls.Add(DropDowns.Element);
        }

        /// <summary>
        /// Decode one element
        /// </summary>
        /// <param name="inputData">all element list</param>
        /// <param name="index">index of current element</param>
        /// <param name="labelText">label text of element</param>
        /// <param name="column">column index of element</param>
        /// <param name="row">row index of element</param>
        /// <returns>new index</returns>
        public int DecodeElement(List<string> inputData, int index, string labelText, ref int column, ref int row, bool emptySelectionAvailable)
        {
            int _index = index;
            List<string> _choicesMain;
            List<string> _choicesIf;
            ComboBoxType _typeMain;
            string text;
            Debug _debug = new Debug();

            if (emptySelectionAvailable)
            {
                if (ModuleBased)
                    _choicesMain = Choices.ChoicesMainModules;
                else
                    _choicesMain = Choices.ChoicesMainObjects;

                _typeMain = ComboBoxType.Main;
            }
            else
            {
                if (ModuleBased)
                    _choicesMain = Choices.ChoicesMainModulesNoEmpty;
                else
                    _choicesMain = Choices.ChoicesMainObjectsNoEmpty;

                _typeMain = ComboBoxType.MainNoEmpty;
            }

            if (ModuleBased)
                _choicesIf = Choices.ChoicesIfModules;
            else
                _choicesIf = Choices.ChoicesIfObjects;

            switch (inputData[_index])
            {
                case KeywordDBChoices.Tab:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    row++;
                    break;

                case KeywordDBChoices.Text:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column + 1, row, inputData[_index], null, null, ComboBoxType.Text);
                    row++;
                    break;

                case KeywordDBChoices.Object:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column + 1, row, inputData[_index], null, Choices.ObjectColumns, ComboBoxType.Object);
                    row++;
                    break;

                case KeywordDBChoices.Data:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column + 1, row, inputData[_index], null, Choices.DataColumns, ComboBoxType.Data);
                    row++;
                    break;

                case KeywordDBChoices.Modules:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column + 1, row, inputData[_index], null, Choices.ModuleColumns, ComboBoxType.Module);
                    row++;
                    break;

                case KeywordDBChoices.IO:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column + 1, row, inputData[_index], null, null, ComboBoxType.Text);
                    row++;
                    break;

                case KeywordDBChoices.TagType:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    AddElement(column + 1, row, inputData[_index], null, null, ComboBoxType.Text);
                    row++;
                    break;

                case KeywordDBChoices.Index:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    //memory area
                    AddElement(column + 1, row, inputData[_index], Resources.MemoryArea, null, ComboBoxType.Text);
                    _index++;
                    //multiplier
                    AddElement(column + 2, row, inputData[_index], Resources.Multiplier, null, ComboBoxType.Number);
                    _index++;
                    //offset
                    AddElement(column + 3, row, inputData[_index], Resources.Offset, null, ComboBoxType.Number);
                    row++;
                    break;

                case KeywordDBChoices.If:
                    AddElement(column, row, inputData[_index], labelText, _choicesMain, _typeMain);
                    _index++;
                    column++;
                    // variable
                    switch (inputData[_index])
                    {
                        case KeywordDBChoices.Object:
                            AddElement(column, row, inputData[_index], null, _choicesIf, ComboBoxType.If);
                            _index++;
                            AddElement(column + 1, row, inputData[_index], null, Choices.ObjectColumns, ComboBoxType.Object);
                            break;

                        case KeywordDBChoices.Data:
                            AddElement(column, row, inputData[_index], null, _choicesIf, ComboBoxType.If);
                            _index++;
                            AddElement(column + 1, row, inputData[_index], null, Choices.DataColumns, ComboBoxType.Data);
                            break;

                        case KeywordDBChoices.Modules:
                            AddElement(column, row, inputData[_index], null, _choicesIf, ComboBoxType.If);
                            _index++;
                            AddElement(column + 1, row, inputData[_index], null, Choices.ModuleColumns, ComboBoxType.Module);
                            break;

                        case KeywordDBChoices.IO:
                            AddElement(column, row, inputData[_index], null, _choicesIf, ComboBoxType.If);
                            _index++;
                            AddElement(column + 1, row, inputData[_index], null, null, ComboBoxType.Text);
                            break;

                        default:
                            text = "DBCellEdit.Decode.IF";
                            _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + inputData[_index], DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(text + "." + inputData[_index] + " is not created for this element");
                    }
                    column++;
                    row++;
                    _index++;
                    //if statement
                    AddElement(column, row, inputData[_index], Resources.Statement, Choices.ChoicesIfStatement, ComboBoxType.IfStatement);

                    column++;
                    _index++;
                    //true statement
                    _index = DecodeElement(inputData, _index, Resources.True, ref column, ref row, false);

                    _index++;
                    //false statement
                    _index = DecodeElement(inputData, _index, Resources.False, ref column, ref row, false);

                    column -= 3;
                    break;

                case KeywordDBChoices.None:
                    break;

                default:
                    text = "DBCellEdit.Decode";
                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + inputData[_index], DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + inputData[_index] + " is not created for this element");
            }
            return _index;
        }

        /// <summary>
        /// Decode all input data
        /// </summary>
        /// <param name="inputData">DB line</param>
        public void DecodeElementsAll(List<string> inputData)
        {
            this.SuspendLayout();
            int x = 0;
            int y = 0;
            for (int i = 0; i < inputData.Count; i++)
                i = DecodeElement(inputData, i, null, ref x, ref y, true);

            AddElement(x, y, KeywordDBChoices.None, null, Choices.ChoicesMainObjects, ComboBoxType.Main);

            this.ResumeLayout();
            this.Refresh();
        }

        public DBCellEdit(List<string> inputData, bool moduleBased)
        {
            OutputData = GeneralFunctions.ListCopy(inputData);

            InitializeComponent();
            ModuleBased = moduleBased;
            DecodeElementsAll(inputData);
        }

        private void DBCellEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateOutputData();
        }
    }
}