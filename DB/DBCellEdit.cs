using IO_list_automation_new.DB;
using IO_list_automation_new.General;
using IO_list_automation_new.Helper_functions;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
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

        private int Index;
        private int PositionColumn;
        private int PositionRow;

        private BaseTypes Base { get; }

        /// <summary>
        /// Get default text depending on base
        /// </summary>
        /// <returns>base text</returns>
        private string GetIODefaultText()
        {
            switch (Base)
            {
                case BaseTypes.ModuleCPU:
                case BaseTypes.ModuleSCADA:
                    return "0";

                case BaseTypes.ObjectSCADA:
                case BaseTypes.ObjectsCPU:
                    return "edit";

                default:
                    const string debugText = "DBCellEdit.GetIODefaultText";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
            }
        }

        /// <summary>
        /// Get default text depending on base
        /// </summary>
        /// <returns>ComboBox type</returns>
        private ComboBoxType GetIOComboBoxType()
        {
            switch (Base)
            {
                case BaseTypes.ModuleCPU:
                case BaseTypes.ModuleSCADA:
                    return ComboBoxType.Number;

                case BaseTypes.ObjectSCADA:
                case BaseTypes.ObjectsCPU:
                    return ComboBoxType.Text;

                default:
                    const string debugText = "DBCellEdit.GetIOComboBoxType";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
            }
        }

        /// <summary>
        /// Get label text depending on base
        /// </summary>
        /// <returns>label text</returns>
        private string GetIOLabel()
        {
            switch (Base)
            {
                case BaseTypes.ModuleCPU:
                case BaseTypes.ModuleSCADA:
                    return ResourcesColumns.Channel;

                case BaseTypes.ObjectSCADA:
                case BaseTypes.ObjectsCPU:
                    return ResourcesColumns.Function;

                default:
                    const string debugText = "DBCellEdit.GetIOLabel";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
            }
        }

        /// <summary>
        /// Get choices depending on base and restrains
        /// </summary>
        /// <param name="restrainLevel">restrain level of choices</param>
        /// <param name="elementType">element type </param>
        /// <returns>choices list</returns>
        private List<string> GetSelectList(RestrainLevel restrainLevel, ComboBoxType elementType)
        {
            string debugText;
            Debug debug = new Debug();

            switch (elementType)
            {
                case ComboBoxType.Main:
                    switch (restrainLevel)
                    {
                        case RestrainLevel.None:
                            switch (Base)
                            {
                                case BaseTypes.ModuleCPU:
                                    return Choices.ChoicesModulesMain;

                                case BaseTypes.ModuleSCADA:
                                case BaseTypes.ObjectSCADA:
                                    return Choices.ChoicesSCADAMain;

                                case BaseTypes.ObjectsCPU:
                                    return Choices.ChoicesObjectsMain;

                                default:
                                    debugText = "GetSelectList.Main.None";
                                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                                    throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
                            }
                        case RestrainLevel.IfCondition:
                            switch (Base)
                            {
                                case BaseTypes.ModuleCPU:
                                    return Choices.ChoicesModulesIfCondition;

                                case BaseTypes.ModuleSCADA:
                                case BaseTypes.ObjectSCADA:
                                    return Choices.ChoicesSCADAIfCondition;

                                case BaseTypes.ObjectsCPU:
                                    return Choices.ChoicesObjectsIfCondition;

                                default:
                                    debugText = "DBCellEdit.Main.IfCondition";
                                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                                    throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
                            }
                        case RestrainLevel.IfStatement:
                            switch (Base)
                            {
                                case BaseTypes.ModuleCPU:
                                    return Choices.ChoicesModulesIfStatement;

                                case BaseTypes.ModuleSCADA:
                                case BaseTypes.ObjectSCADA:
                                    return Choices.ChoicesSCADAIfStatement;

                                case BaseTypes.ObjectsCPU:
                                    return Choices.ChoicesObjectsIfStatement;

                                default:
                                    debugText = "DBCellEdit.Main.IfStatement";
                                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(Base), DebugLevels.None, DebugMessageType.Critical);
                                    throw new InvalidProgramException(debugText + "." + nameof(Base) + " is not created for this element");
                            }

                        case RestrainLevel.NoEdit:
                            return null;

                        default:
                            debugText = "DBCellEdit.Main.restrainLevel";
                            debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(restrainLevel), DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(debugText + "." + nameof(restrainLevel) + " is not created for this element");
                    }

                case ComboBoxType.IfCondition:
                    return Choices.ChoicesIfConditions;

                case ComboBoxType.Data_:
                    return Choices.DataColumns;

                case ComboBoxType.Object:
                    return Choices.ObjectColumns;

                case ComboBoxType.Module:
                    return Choices.ModuleColumns;

                case ComboBoxType.Text:
                case ComboBoxType.Number:
                    return null;

                default:
                    debugText = "DBCellEdit.elementType";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(elementType), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(elementType) + " is not created for this element");
            }
        }

        /// <summary>
        /// Remove column comboBox element from controls
        /// </summary>
        private void DeleteColumnComboBox()
        {
            foreach (var item in this.Controls)
            {
                if (!item.GetType().Name.Contains("ComboBox"))
                    continue;

                if (((System.Windows.Forms.ComboBox)item).Name != "ComboBoxCell")
                    continue;

                DropDownClass comboBox = new DropDownClass((System.Windows.Forms.ComboBox)item)
                {
                    IndexChangedEventRemove = ComboBoxValueChangedEvent,
                };
                this.Controls.Remove((System.Windows.Forms.Control)item);
                break;
            }
        }

        /// <summary>
        /// Grid cell click and create comboBox for selection or other edit options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteColumnComboBox();
            DataGridViewCell cell = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];
            Grid.ReadOnly = true;

            if (cell.Tag == null)
                return;

            CellTag cellTag = (CellTag)cell.Tag;

            //no need of comboBox
            switch (cellTag.ComboType)
            {
                case ComboBoxType.Text:
                case ComboBoxType.Number:
                    Grid.ReadOnly = cellTag.Restrain == RestrainLevel.NoEdit;
                    return;
            }

            List<string> selectList = GetSelectList(cellTag.Restrain, cellTag.ComboType);
            DropDownClass dropDowns = new DropDownClass("ComboBoxCell");
            dropDowns.ChangeDisplayMember(DropDownElementType.Name);
            dropDowns.Location = PointToClient(Cursor.Position);
            dropDowns.Editable(false);
            dropDowns.Tag = new ComboBoxTag(cellTag.ComboType, cellTag.UniqueName, cellTag.Keyword, e.RowIndex, e.ColumnIndex);

            foreach (string item in selectList)
                dropDowns.AddItemFull(string.Empty, item);

            //change index event
            dropDowns.IndexChangedEvent = ComboBoxValueChangedEvent;

            this.Controls.Add(dropDowns.Element);
            dropDowns.Element.BringToFront();
        }

        /// <summary>
        /// ComboBox value changed event
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">event arguments</param>
        private void ComboBoxValueChangedEvent(object sender, EventArgs e)
        {
            DropDownClass box = new DropDownClass((System.Windows.Forms.ComboBox)sender);

            string currentValue = box.SelectedKeyword();

            //decode tag
            ComboBoxTag tag = box.Tag;

            DeleteColumnComboBox();
            //ComboBox type is of type that need to change layout
            switch (tag.Type)
            {
                case ComboBoxType.Data_:
                case ComboBoxType.Object:
                case ComboBoxType.Module:
                    ((CellTag)Grid.Rows[tag.X].Cells[tag.Y].Tag).UpdateKeyword(currentValue);
                    Grid.Rows[tag.X].Cells[tag.Y].Value = currentValue;
                    return;

                case ComboBoxType.Text:
                case ComboBoxType.Number:
                    return;
            }

            string previousValue = tag.PreviousValue;
            string elementName = tag.Name;
            //dropdown value changed
            if (previousValue == currentValue)
                return;

            List<string> list = GetElementsUnique();
            OutputData.Clear();

            //extracting keyword to list
            List<string> listElementKeywords = new List<string>();
            foreach (string keyword in list)
                listElementKeywords.Add(GetDropDownSelectedKeyword(keyword));

            //before element
            int index = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == elementName)
                {
                    index = i;
                    break;
                }
                //extracting keyword to list
                OutputData.Add(GetDropDownSelectedKeyword(list[i]));
            }
            //delete elements
            if (currentValue != KeywordDBChoices.Insert)
                DeleteOldElements(index, list, previousValue);

            //add new
            switch (currentValue)
            {
                case KeywordDBChoices.If:
                    //if
                    OutputData.Add(currentValue);
                    //object
                    OutputData.Add(KeywordDBChoices.Data);
                    OutputData.Add(Choices.DataColumns[0]);
                    OutputData.Add(GetIODefaultText());
                    //statement
                    OutputData.Add(Choices.ChoicesIfConditions[0]);
                    //true object
                    OutputData.Add(KeywordDBChoices.Text);
                    OutputData.Add("edit");
                    //false object
                    OutputData.Add(KeywordDBChoices.Text);
                    OutputData.Add("edit");

                    break;

                case KeywordDBChoices.BaseAddress:
                    OutputData.Add(currentValue);
                    //index memory area
                    OutputData.Add(string.Empty);
                    //index multiplier
                    OutputData.Add("1");
                    //index offset
                    OutputData.Add("0");
                    break;

                case KeywordDBChoices.Address:
                    OutputData.Add(currentValue);
                    //index offset
                    OutputData.Add("0");
                    break;

                case KeywordDBChoices.AddressArea:
                case KeywordDBChoices.GetBaseAddress:
                    OutputData.Add(currentValue);
                    OutputData.Add("");
                    break;

                case KeywordDBChoices.Tab:
                case KeywordDBChoices.CPU:
                case KeywordDBChoices.ObjectName:
                    OutputData.Add(currentValue);
                    break;

                case KeywordDBChoices.None:
                    break;

                case KeywordDBChoices.Insert:
                case KeywordDBChoices.Text:
                    OutputData.Add(KeywordDBChoices.Text);
                    OutputData.Add("edit");
                    break;

                case KeywordDBChoices.Data:
                    OutputData.Add(currentValue);
                    OutputData.Add(Choices.DataColumns[0]);
                    OutputData.Add(GetIODefaultText());
                    break;

                case KeywordDBChoices.Object:
                    OutputData.Add(currentValue);
                    OutputData.Add(Choices.ObjectColumns[0]);
                    break;

                case KeywordDBChoices.Modules:
                    OutputData.Add(currentValue);
                    OutputData.Add(Choices.ModuleColumns[0]);
                    break;

                case KeywordDBChoices.Equal:
                case KeywordDBChoices.nEqual:
                case KeywordDBChoices.GreaterEqual:
                case KeywordDBChoices.Greater:
                case KeywordDBChoices.Less:
                case KeywordDBChoices.LessEqual:
                    OutputData.Add(currentValue);
                    OutputData.Add(KeywordDBChoices.Text);
                    OutputData.Add("edit");
                    break;

                case KeywordDBChoices.IsEmpty:
                case KeywordDBChoices.IsNotEmpty:
                    OutputData.Add(currentValue);
                    break;

                case KeywordDBChoices.MultiLine:
                    OutputData.Add(currentValue);
                    OutputData.Add(KeywordDBChoices.Text);
                    OutputData.Add("edit");
                    OutputData.Add(KeywordDBChoices.MultiLineEnd);
                    break;

                default:
                    Debug debug = new Debug();
                    const string debugText = "DBCellEdit.ComboBoxValueChangedEvent";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + currentValue, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + currentValue + " is not created for this element");
            }

            //add after
            if (currentValue == KeywordDBChoices.Insert)
            {
                OutputData.Add(previousValue);
                index++;
            }

            for (int i = index; i < list.Count; i++)
                OutputData.Add(GetDropDownSelectedKeyword(list[i]));

            DeleteAllItem();
            DecodeElementsAll(OutputData);
        }

        /// <summary>
        /// ComboBox accept only numbers
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">event arguments</param>
        private void Cell_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key is not a digit and not a control key (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Block the input
        }

        /// <summary>
        /// Grid cell add key check event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= Cell_KeyPress;

            if (Grid.CurrentCell.Tag == null)
                return;

            CellTag cellTag = (CellTag)Grid.CurrentCell.Tag;

            if (cellTag.ComboType != ComboBoxType.Number || cellTag.Restrain == RestrainLevel.NoEdit)
                return;

            if (e.Control is TextBox tb)
                tb.KeyPress += Cell_KeyPress;
        }

        /// <summary>
        /// Read all comboBoxes and update Output Data_
        /// </summary>
        private void UpdateOutputData()
        {
            List<string> list = GetElements();
            OutputData.Clear();

            OutputData.AddRange(list);
        }

        /// <summary>
        /// clear all grid
        /// </summary>
        private void DeleteAllItem()
        {
            Grid.Rows.Clear();
            Grid.Columns.Clear();
        }

        /// <summary>
        /// Find object by name and return selected keyword of comboBox
        /// </summary>
        /// <param name="name">object name</param>
        /// <returns>comboBox selected keyword</returns>
        private string GetDropDownSelectedKeyword(string name)
        {
            foreach (DataGridViewRow row in Grid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Tag == null)
                        continue;

                    CellTag cellTag = (CellTag)cell.Tag;

                    if (cellTag.UniqueName == name)
                        return cellTag.Keyword;
                }
            }
            return null;
        }

        /// <summary>
        /// create list of elements unique names
        /// </summary>
        /// <returns>elements unique names</returns>
        private List<string> GetElementsUnique()
        {
            List<string> list = new List<string>();

            foreach (DataGridViewRow row in Grid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Tag == null)
                        continue;

                    CellTag cellTag = (CellTag)cell.Tag;
                    list.Add(cellTag.UniqueName);
                }
            }
            return list;
        }

        /// <summary>
        /// create list of elements keywords
        /// </summary>
        /// <returns>elements keywords</returns>
        private List<string> GetElements()
        {
            List<string> list = new List<string>();

            foreach (DataGridViewRow row in Grid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Tag == null)
                        continue;

                    CellTag cellTag = (CellTag)cell.Tag;
                    switch (cellTag.ComboType)
                    {
                        case ComboBoxType.Main:
                        case ComboBoxType.IfCondition:
                        case ComboBoxType.Data_:
                        case ComboBoxType.Object:
                        case ComboBoxType.Module:
                            list.Add(cellTag.Keyword);
                            break;

                        case ComboBoxType.Text:
                        case ComboBoxType.Number:
                            list.Add(cell.Value.ToString());
                            break;
                    }
                }
            }
            return list;
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
                case KeywordDBChoices.BaseAddress:
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                //remove 3 element
                case KeywordDBChoices.Data:
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    list.RemoveAt(startIndex);
                    break;

                //remove 2 element
                case KeywordDBChoices.Text:
                case KeywordDBChoices.Object:
                case KeywordDBChoices.Modules:
                case KeywordDBChoices.Address:
                case KeywordDBChoices.AddressArea:
                case KeywordDBChoices.GetBaseAddress:
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
                case KeywordDBChoices.CPU:
                case KeywordDBChoices.ObjectName:
                    list.RemoveAt(startIndex);
                    break;

                case KeywordDBChoices.MultiLine:
                    int layer = 1;
                    list.RemoveAt(startIndex);
                    while (layer > 0)
                    {
                        if (GetDropDownSelectedKeyword(list[startIndex]) == KeywordDBChoices.MultiLine)
                            layer++;
                        else if (GetDropDownSelectedKeyword(list[startIndex]) == KeywordDBChoices.MultiLineEnd)
                            layer--;
                        list.RemoveAt(startIndex);
                    }
                    break;

                default:
                    Debug debug = new Debug();
                    const string debugText = "DBCellEdit.DeleteOldElements";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + cellValue, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + cellValue + " is not created for this element");
            }
        }

        /// <summary>
        /// Add elements to form
        /// </summary>
        /// <param name="rowIndex">y location </param>
        /// <param name="column">x location</param>
        /// <param name="comboBoxKeyword">ComboBox name keyword</param>
        /// <param name="labelText">Text of label</param>
        /// <param name="restrainLevel">restrains level</param>
        private void AddElement(int rowIndex, int column, string comboBoxKeyword, string labelText, RestrainLevel restrainLevel, ComboBoxType elementType)
        {
            for (int i = Grid.ColumnCount; i <= column; i++)
                Grid.Columns.Add("", "");

            int row = rowIndex * 2;
            for (int i = Grid.RowCount; i < row + 2; i++)
                Grid.Rows.Add();

            if (labelText != null)
                Grid.Rows[row].Cells[column].Value = labelText;

            switch (elementType)
            {
                case ComboBoxType.Main:
                case ComboBoxType.IfCondition:
                case ComboBoxType.Data_:
                case ComboBoxType.Object:
                case ComboBoxType.Module:
                    Grid.Rows[row + 1].Cells[column].Value = TextHelper.GetColumnOrChoicesName(comboBoxKeyword);
                    Grid.Rows[row + 1].Cells[column].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular);
                    Grid.Rows[row + 1].Cells[column].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    break;

                case ComboBoxType.Text:
                case ComboBoxType.Number:
                    Grid.Rows[row + 1].Cells[column].Value = comboBoxKeyword;
                    Grid.Rows[row + 1].Cells[column].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular);
                    Grid.Rows[row + 1].Cells[column].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
            }
            if (restrainLevel != RestrainLevel.NoEdit)
                Grid.Rows[row + 1].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);

            Grid.Rows[row + 1].Cells[column].Tag = new CellTag(comboBoxKeyword, elementType, restrainLevel, rowIndex, column);
            Index++;
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
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], null, restrainLevel, ComboBoxType.Data_);
            AddElement(PositionRow, PositionColumn + 2, inputData[Index], GetIOLabel(), restrainLevel, GetIOComboBoxType());
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
        private void DecodeTab(List<string> inputData, string labelText, RestrainLevel restrainLevel)
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
        /// Decode base address element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeBaseAddress(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            inputData[Index] = KeywordDBChoices.BaseAddress;
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], Resources.MemoryArea, RestrainLevel.None, ComboBoxType.Text);
            AddElement(PositionRow, PositionColumn + 2, inputData[Index], Resources.Multiplier, RestrainLevel.None, ComboBoxType.Number);
            AddElement(PositionRow, PositionColumn + 3, inputData[Index], Resources.Offset, RestrainLevel.None, ComboBoxType.Number);
            PositionRow++;
        }

        /// <summary>
        /// Decode address element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeAddress(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], Resources.Offset, restrainLevel, ComboBoxType.Text);
            PositionRow++;
        }

        /// <summary>
        /// Decode address get base address element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeGetBaseAddress(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], null, restrainLevel, ComboBoxType.Text);
            PositionRow++;
        }

        /// <summary>
        /// Decode address get area address element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeAddressArea(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            AddElement(PositionRow, PositionColumn + 1, inputData[Index], null, RestrainLevel.None, ComboBoxType.Number);
            PositionRow++;
        }

        /// <summary>
        /// Decode tab element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeCPU(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            PositionRow++;
        }

        /// <summary>
        /// Decode tab element and put to form
        /// </summary>
        /// <param name="inputData">Input data list to decode</param>
        /// <param name="labelText">label text</param>
        /// <param name="restrainLevel">restrains level</param>
        private void DecodeObjectName(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
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
            Debug debug = new Debug();
            string debugText;

            AddElement(PositionRow, PositionColumn, inputData[Index], labelText, restrainLevel, ComboBoxType.Main);
            PositionColumn++;

            int additionalColumnOffset = 0;
            //condition
            switch (inputData[Index])
            {
                case KeywordDBChoices.Object:
                    DecodeObject(inputData, null, RestrainLevel.IfCondition);
                    break;

                case KeywordDBChoices.Data:
                    DecodeData(inputData, null, RestrainLevel.IfCondition);
                    additionalColumnOffset++;
                    break;

                case KeywordDBChoices.Modules:
                    DecodeModule(inputData, null, RestrainLevel.IfCondition);
                    break;

                case KeywordDBChoices.CPU:
                    DecodeCPU(inputData, null, RestrainLevel.IfCondition);
                    break;

                case KeywordDBChoices.ObjectName:
                    DecodeObjectName(inputData, null, RestrainLevel.IfCondition);
                    break;

                default:
                    debugText = "DCCellEdit.DecodeIf.Condition";

                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + inputData[Index], DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + inputData[Index] + " is not created for this element");
            }
            //go back 1 row to have if in one line
            PositionRow--;
            PositionColumn += 2 + additionalColumnOffset;
            int removeColumnCount;

            switch (inputData[Index])
            {
                case KeywordDBChoices.IsEmpty:
                case KeywordDBChoices.IsNotEmpty:
                    AddElement(PositionRow, PositionColumn, inputData[Index], Resources.Statement, RestrainLevel.IfStatement, ComboBoxType.IfCondition);
                    PositionRow++;
                    removeColumnCount = 3;
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
                    removeColumnCount = 4;
                    break;

                default:
                    debugText = "DCCellEdit.DecodeIf.Statement";

                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + inputData[Index], DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + inputData[Index] + " is not created for this element");
            }
            //if = true
            DecodeElement(inputData, Resources.True, RestrainLevel.IfStatement);
            //if = false
            DecodeElement(inputData, Resources.False, RestrainLevel.IfStatement);

            PositionColumn -= removeColumnCount + additionalColumnOffset;
        }

        /// <summary>
        /// Decode one element
        /// </summary>
        /// <param name="inputData">all element list</param>
        /// <param name="labelText">label text of element</param>
        /// <param name="restrainLevel">choices are main not if statement</param>
        private void DecodeElement(List<string> inputData, string labelText, RestrainLevel restrainLevel)
        {
            string debugText;
            Debug debug = new Debug();

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

                case KeywordDBChoices.BaseAddress:
                    DecodeBaseAddress(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.Address:
                    DecodeAddress(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.GetBaseAddress:
                    DecodeGetBaseAddress(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.AddressArea:
                    DecodeAddressArea(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.If:
                    DecodeIf(inputData, labelText, restrainLevel);
                    break;

                case KeywordDBChoices.CPU:
                    DecodeCPU(inputData, null, restrainLevel);
                    break;

                case KeywordDBChoices.ObjectName:
                    DecodeObjectName(inputData, null, restrainLevel);
                    break;

                case KeywordDBChoices.None:
                    DecodeNone();
                    break;

                case KeywordDBChoices.MultiLine:
                    DecodeMultiline(inputData, labelText, restrainLevel);
                    break;

                default:
                    debugText = "DBCellEdit.Decode";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + inputData[Index], DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + inputData[Index] + " is not created for this element");
            }
        }

        /// <summary>
        /// Clear all data rows and columns in Data_Grid
        /// </summary>
        private void ClearEmptyRows()
        {
            bool found;
            for (int row = Grid.RowCount - 2; row > 1; row--)
            {
                found = false;

                foreach (DataGridViewCell cell in Grid.Rows[row].Cells)
                {
                    if (cell.Value == null)
                        continue;

                    if (!string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    Grid.Rows.RemoveAt(row);
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

            int count = 0;
            while (Index < inputData.Count)
            {
                count++;
                DecodeElement(inputData, null, RestrainLevel.None);
                if (count > 1000)
                {
                    Debug debug = new Debug();
                    const string debugText = "DecodeElementsAll";
                    debug.ToFile(debugText + ": infinite loop", DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + ": infinite loop");
                }
            }
            AddElement(PositionRow, PositionColumn, "", null, RestrainLevel.None, ComboBoxType.Main);
            ClearEmptyRows();

            Grid.AutoResizeColumns();
            Grid.AutoResizeRows();

            this.ResumeLayout(true);
            this.Refresh();
        }

        public DBCellEdit(List<string> inputData, BaseTypes inputBase)
        {
            OutputData = new List<string>(inputData);

            InitializeComponent();
            Base = inputBase;
            DecodeElementsAll(inputData);
        }

        private void DBCellEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateOutputData();
        }

        private void DBCellEdit_Shown(object sender, EventArgs e)
        {
            Grid.AutoResizeColumns();
            Grid.AutoResizeRows();
        }
    }
}