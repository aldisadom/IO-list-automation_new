using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Properties;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IO_list_automation_new.Forms
{
    internal enum RestrainLevel
    {
        None,
        IfCondition,
        IfStatement,
        NoEdit,
    }

    public partial class DBCellEdit : Form
    {
        public List<string> OutputData { get; }

        private readonly DBChoices Choices = new DBChoices();

        private const int StartX = 10;
        private const int StartY = 10;
        private const int _offsetX = 135;
        private const int _offsetY = 45;

        private int Index;
        private int PositionColumn;
        private int PositionRow;
        private int ComboBoxIndex = 1;

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
            if (!_box.ChangeLayout)
                return;

            //dropdown value changed
            if (_box.Tag.PreviousValue == _currentValue)
                return;

            List<string> _list = SortElements();
            List<string> _prvList = OutputData.ToList();
            OutputData.Clear();

            List<string> _listElementKeywords = new List<string>();
            for (int i = 0; i < _list.Count; i++)
                //extracting keyword to list
                _listElementKeywords.Add(GetDropDownSelectedKeyword(_list[i]));

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
            if (_currentValue != KeywordDBChoices.Insert)
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
                    OutputData.Add(Choices.ChoicesIfConditions[0]);
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

                case KeywordDBChoices.Insert:
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

                case KeywordDBChoices.IOTag:
                case KeywordDBChoices.IOChannel:
                case KeywordDBChoices.IOPin:
                case KeywordDBChoices.IOText:
                    OutputData.Add(_currentValue);
                    OutputData.Add(ModuleBased ? "0" : "ON");
                    break;

                case KeywordDBChoices.Equal:
                case KeywordDBChoices.nEqual:
                case KeywordDBChoices.GreaterEqual:
                case KeywordDBChoices.Greater:
                case KeywordDBChoices.Less:
                case KeywordDBChoices.LessEqual:
                    OutputData.Add(_currentValue);
                    OutputData.Add(KeywordDBChoices.Text);
                    OutputData.Add("edit");
                    break;

                case KeywordDBChoices.IsEmpty:
                case KeywordDBChoices.IsNotEmpty:
                    OutputData.Add(_currentValue);
                    break;
                case KeywordDBChoices.MultiLine:
                    OutputData.Add(KeywordDBChoices.MultiLine);
                    OutputData.Add(KeywordDBChoices.Text);
                    OutputData.Add("edit");
                    OutputData.Add(KeywordDBChoices.MultiLineEnd);
                    break;

                default:
                    Debug _debug = new Debug();
                    const string _debugText = "DBCellEdit.ComboBoxValueChangedEvent";
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + _currentValue, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + _currentValue + " is not created for this element");
            }

            //add after
            if (_currentValue == KeywordDBChoices.Insert)
            {
                OutputData.Add(_box.Tag.PreviousValue);
                _index++;
            }

            for (int i = _index; i < _list.Count; i++)
                OutputData.Add(GetDropDownSelectedKeyword(_list[i]));

            DeleteAllItem();
            DecodeElementsAll(OutputData);
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
                    switch (GetDropDownSelectedKeyword(list[startIndex]))
                    {
                        case KeywordDBChoices.IsEmpty:
                        case KeywordDBChoices.IsNotEmpty:
                            list.RemoveAt(startIndex);
                            break;
                        default:
                            list.RemoveAt(startIndex);
                            DeleteOldElements(startIndex, list, GetDropDownSelectedKeyword(list[startIndex]));
                            break;
                    }
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
                case KeywordDBChoices.Text:
                case KeywordDBChoices.Data:
                case KeywordDBChoices.Object:
                case KeywordDBChoices.IOTag:
                case KeywordDBChoices.IOText:
                case KeywordDBChoices.IOPin:
                case KeywordDBChoices.IOChannel:
                case KeywordDBChoices.Modules:
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                case KeywordDBChoices.Equal:
                case KeywordDBChoices.nEqual:
                case KeywordDBChoices.GreaterEqual:
                case KeywordDBChoices.Greater:
                case KeywordDBChoices.Less:
                case KeywordDBChoices.LessEqual:
                    list.RemoveAt(startIndex);
                    DeleteOldElements(startIndex, list, GetDropDownSelectedKeyword(list[startIndex]));
                    break;

                //remove 1 element
                case KeywordDBChoices.Tab:
                case KeywordDBChoices.None:
                case KeywordDBChoices.IsEmpty:
                case KeywordDBChoices.IsNotEmpty:
                    list.RemoveAt(startIndex);
                    break;

                case KeywordDBChoices.MultiLine:
                    int _layer = 1;
                    list.RemoveAt(startIndex);
                    while (_layer > 0)
                    {
                        if (GetDropDownSelectedKeyword(list[startIndex]) == KeywordDBChoices.MultiLine)
                            _layer++;
                        else if (GetDropDownSelectedKeyword(list[startIndex]) == KeywordDBChoices.MultiLineEnd)
                            _layer--;
                        list.RemoveAt(startIndex);
                    }
                    break;

                default:
                    Debug _debug = new Debug();
                    const string _debugText = "DBCellEdit.DeleteOldElements";
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + cellValue, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + cellValue + " is not created for this element");
            }
        }

        /// <summary>
        /// Add elements to form
        /// </summary>
        /// <param name="row">y location </param>
        /// <param name="column">x location</param>
        /// <param name="comboBoxKeyword">ComboBox name keyword</param>
        /// <param name="labelText">Text of label</param>
        /// <param name="restrainLevel">restrains level</param>
        private void AddElement(int row, int column, string comboBoxKeyword, string labelText, RestrainLevel restrainLevel, ComboBoxType elementType)
        {
            List<string> _selectList = new List<string>();

            Debug _debug = new Debug();
            string _debugText;

            switch (elementType)
            {
                case ComboBoxType.Main:
                    switch (restrainLevel)
                    {
                        case RestrainLevel.None:
                            _selectList = ModuleBased ? Choices.ChoicesModulesMain : Choices.ChoicesObjectsMain;
                            break;
                        case RestrainLevel.IfCondition:
                            _selectList = ModuleBased ? Choices.ChoicesModulesIfCondition : Choices.ChoicesObjectsIfCondition;
                            break;
                        case RestrainLevel.IfStatement:
                            _selectList = ModuleBased ? Choices.ChoicesModulesIfStatement : Choices.ChoicesObjectsIfStatement;
                            break;
                        case RestrainLevel.NoEdit:
                            _selectList = null;
                            break;
                        default:
                            _debugText = "DBCellEdit.AddElement.Main";
                            _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + restrainLevel, DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(_debugText + "." + restrainLevel + " is not created for this element");
                    }
                    break;
                case ComboBoxType.IfCondition:
                    _selectList = Choices.ChoicesIfConditions;
                    break;
                case ComboBoxType.Data:
                    _selectList = Choices.DataColumns;
                    break;
                case ComboBoxType.Object:
                    _selectList = Choices.ObjectColumns;
                    break;
                case ComboBoxType.Module:
                    _selectList = Choices.ModuleColumns;
                    break;
                case ComboBoxType.Text:
                case ComboBoxType.Number:
                    _selectList = null;
                    break;
                default:
                    _debugText = "DBCellEdit.AddElement.Main";
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + restrainLevel, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + restrainLevel + " is not created for this element");
            }

            //
            // label1
            //
            if (labelText != null)
            {
                System.Windows.Forms.Label label1 = new System.Windows.Forms.Label()
                {
                    AutoSize = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10F),
                    Location = new System.Drawing.Point((column * _offsetX) + StartX, (row * _offsetY) + StartY),
                    Name = "Element:" + GeneralFunctions.AddZeroes(ComboBoxIndex)+":Label",
                    Size = new System.Drawing.Size(46, 17),
                    Text = labelText,
                };
                this.Controls.Add(label1);
            }
            //
            // comboBox1
            //

            DropDownClass DropDowns = new DropDownClass("Element:" + GeneralFunctions.AddZeroes(ComboBoxIndex) + ":Dropdown");
            DropDowns.SetTag(elementType, comboBoxKeyword);
            DropDowns.ChangeDisplayMember(DropDownElementType.Name);
            DropDowns.Location = new System.Drawing.Point((column * _offsetX) + StartX, (row * _offsetY) + StartY + 20);

            if (_selectList == null)
            {
                if (restrainLevel == RestrainLevel.NoEdit)
                    DropDowns.Editable(false);
                else
                    DropDowns.Editable(true);

                DropDowns.AddItemText(comboBoxKeyword);
                DropDowns.SelectedIndex = 0;
            }
            else
            {
                DropDowns.Editable(false);
                for (int i = 0; i < _selectList.Count; i++)
                {
                    DropDowns.AddItemFull(string.Empty, _selectList[i]);
                    if (comboBoxKeyword == _selectList[i])
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

            ComboBoxIndex++;
            Index++;
            this.Controls.Add(DropDowns.Element);
        }

        /// <summary>
        /// Decode IO Pin element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeIO(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow ,PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn+1, inputData[Index], null, restrainLevel, ModuleBased ? ComboBoxType.Number : ComboBoxType.Text);
            PositionRow++;
        }

        /// <summary>
        /// Decode data element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeData(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], null, restrainLevel, ComboBoxType.Data);
            PositionRow++;
        }

        /// <summary>
        /// Decode object element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeObject(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], null, restrainLevel, ComboBoxType.Object);
            PositionRow++;
        }

        /// <summary>
        /// Decode tab element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeTab(List<string> inputData,string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            PositionRow++;
        }

        /// <summary>
        /// Decode module element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeModule(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], null, restrainLevel, ComboBoxType.Module);
            PositionRow++;
        }

        /// <summary>
        /// Decode text element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeText(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], null, restrainLevel, ComboBoxType.Text);
            PositionRow++;
        }

        /// <summary>
        /// Decode None element and put to form
        /// </summary>
        private void DecodeNone()
        {
            Index++;
        }

        /// <summary>
        /// Decode index element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeIndex(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], Resources.MemoryArea, RestrainLevel.None, ComboBoxType.Text);
            AddElement(PositionRow, PositionColumn + 2, inputData[Index], Resources.Multiplier, RestrainLevel.None, ComboBoxType.Number);
            AddElement(PositionRow, PositionColumn + 3, inputData[Index], Resources.Offset, RestrainLevel.None, ComboBoxType.Number);
            PositionRow++;
        }

        /// <summary>
        /// Decode multiline element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeMultiline(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);

            PositionColumn++;
            PositionRow++;
            while (inputData[Index] != KeywordDBChoices.MultiLineEnd)
                DecodeElement(inputData, "", RestrainLevel.None);

            PositionColumn--;
            AddElement(PositionRow, PositionColumn, inputData[Index], null, RestrainLevel.NoEdit, ComboBoxType.Text);
            PositionRow++;
        }

        private void DecodeIf(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            Debug _debug = new Debug();
            string _debugText;

            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            PositionColumn++;
            //condition
            switch (inputData[Index])
            {
              case KeywordDBChoices.Object:
                    DecodeObject(inputData, null, RestrainLevel.IfCondition);
                    break;

                case KeywordDBChoices.Data:
                    DecodeData(inputData, null, RestrainLevel.IfCondition);
                    break;

                case KeywordDBChoices.Modules:
                    DecodeModule(inputData, null, RestrainLevel.IfCondition);
                    break;

                case KeywordDBChoices.IOText:
                case KeywordDBChoices.IOTag:
                case KeywordDBChoices.IOPin:
                case KeywordDBChoices.IOChannel:
                    DecodeIO(inputData, null, RestrainLevel.IfCondition);
                    break;

                default:
                    _debugText = "DCCellEdit.DecodeIf.Condition";

                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + inputData[Index], DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + inputData[Index] + " is not created for this element");
            }
            //go back 1 row to have if in one line
            PositionRow--;
            PositionColumn+=2;
            int _removeColumnCount;

            switch (inputData[Index])
            {
                case KeywordDBChoices.IsEmpty:
                case KeywordDBChoices.IsNotEmpty:
                    AddElement(PositionRow, PositionColumn, inputData[Index], Resources.Statement, RestrainLevel.IfStatement, ComboBoxType.IfCondition);
                    PositionRow++;
                    _removeColumnCount = 3;
                    break;

                case KeywordDBChoices.Equal:
                case KeywordDBChoices.nEqual:
                case KeywordDBChoices.GreaterEqual:
                case KeywordDBChoices.Greater:
                case KeywordDBChoices.LessEqual:
                case KeywordDBChoices.Less:
                    AddElement(PositionRow, PositionColumn, inputData[Index], Resources.Statement, RestrainLevel.IfStatement, ComboBoxType.IfCondition);
                    PositionColumn++;
                    // then it is variable
                    DecodeElement(inputData, null, RestrainLevel.IfStatement);
                    _removeColumnCount = 4;
                    break;

                default:
                    _debugText = "DCCellEdit.DecodeIf.Statement";

                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + inputData[Index], DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + inputData[Index] + " is not created for this element");
            }
            //if = true
            DecodeElement(inputData, Resources.True, RestrainLevel.IfStatement);
            //if = false
            DecodeElement(inputData, Resources.False, RestrainLevel.IfStatement);
            PositionColumn -= _removeColumnCount;
        }

        /// <summary>
        /// Decode one element
        /// </summary>
        /// <param name="inputData">all element list</param>
        /// <param name="labelText">label text of element</param>
        /// <param name="restrainLevel">choices are main not if statement</param>
        private void DecodeElement(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            string _debugText;
            Debug _debug = new Debug();

            switch (inputData[Index])
            {
                case KeywordDBChoices.Tab:
                    DecodeTab(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.Text:
                    DecodeText(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.Object:
                    DecodeObject(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.Data:
                    DecodeData(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.Modules:
                    DecodeModule(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.IOTag:
                case KeywordDBChoices.IOPin:
                case KeywordDBChoices.IOChannel:
                case KeywordDBChoices.IOText:
                    DecodeIO(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.Index:
                    DecodeIndex(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.If:
                    DecodeIf(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.None:
                    DecodeNone();
                    break;

                case KeywordDBChoices.MultiLine:
                    DecodeMultiline(inputData, labelText, restrainLevel);
                    break;

                default:
                    _debugText = "DBCellEdit.Decode";
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + inputData[Index], DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + inputData[Index] + " is not created for this element");
            }
        }

        /// <summary>
        /// Decode all input data
        /// </summary>
        /// <param name="inputData">DB line</param>
        public void DecodeElementsAll(List<string> inputData)
        {
            this.SuspendLayout();
            PositionColumn = 0;
            PositionRow = 0;
            Index = 0;

            int _count = 0;
            while (Index < inputData.Count)
            {
                _count++;
                DecodeElement(inputData, null, RestrainLevel.None);
                if (_count > 1000)
                {
                    Debug _debug = new Debug();
                    const string _debugText = "DecodeElementsAll";
                    _debug.ToFile(_debugText + ": infinite loop", DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + ": infinite loop");
                }
            }

            Index = 0;
            if (ModuleBased)
                AddElement(PositionRow, PositionColumn, "" , null, RestrainLevel.None, ComboBoxType.Main);
            else
                AddElement(PositionRow, PositionColumn, "", null, RestrainLevel.None, ComboBoxType.Main);

            int x = 800;
            int y = Math.Max (PositionRow * _offsetY + 100,400);
            this.Size = new System.Drawing.Size(x, y);

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