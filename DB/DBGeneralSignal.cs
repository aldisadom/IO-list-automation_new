using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IO_list_automation_new.DB
{
    internal class DBGeneralSignal
    {
        public List<string> Cell { get; private set; }
        public string MemoryArea { get; private set; }
        public int BaseAddress { get; private set; }
        public int AddressSize { get; private set; }
        public bool BaseAddressSet { get; private set; }

        private int Index;

        private string Value = "";

        private readonly bool Simulation = false;

        public DBGeneralSignal(bool simulation)
        {
            MemoryArea = string.Empty;
            BaseAddress = 0;
            AddressSize = 0;
            Cell = new List<string>();
            Simulation = simulation;
            BaseAddressSet = false;
        }

        public DBGeneralSignal()
        {
            MemoryArea = string.Empty;
            AddressSize = 0;
            BaseAddress = 0;
            Cell = new List<string>();
            BaseAddressSet = false;
        }

        /// <summary>
        /// Update signal data
        /// </summary>
        /// <param name="newList">new data of signal</param>
        public void SetValue(List<string> newList)
        {
            Cell = newList;
        }

        /// <summary>
        /// Update base addresses
        /// </summary>
        /// <param name="memoryArea">new memory area</param>
        /// <param name="baseAddress">new base address</param>
        /// <param name="addressSize">new address size</param>
        public void UpdateBaseAddress(string memoryArea, int baseAddress, int addressSize)
        {
            MemoryArea = memoryArea;
            BaseAddress = baseAddress;
            AddressSize = addressSize;
            BaseAddressSet = true;
        }

        /// <summary>
        /// Checks data signal with correct object signal if it is a match
        /// </summary>
        /// <param name="inputText">input text to check</param>
        /// <param name="dataSignal">data signal</param>
        /// <param name="objectSignal">object signal</param>
        /// <param name="moduleSignal">module signal</param>
        /// <param name="addressObject">address Object</param>
        /// <param name="inputBase">signal base</param>
        /// <returns>true if it is a match</returns>
        private bool CheckDataSignal(string inputText, DataSignal dataSignal, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase)
        {
            switch (inputBase)
            {
                case BaseTypes.ModuleCPU:
                    if (!int.TryParse(dataSignal.Channel, out int channel))
                        return false;
                    return (moduleSignal.ModuleName == dataSignal.ModuleName) && ((channel == int.Parse(inputText)) || string.IsNullOrEmpty(inputText));

                case BaseTypes.ModuleSCADA:
                    if (!int.TryParse(dataSignal.Channel, out channel))
                        return false;
                    return (addressObject.ObjectName == dataSignal.ModuleName) && ((channel == int.Parse(inputText)) || string.IsNullOrEmpty(inputText));

                case BaseTypes.ObjectsCPU:
                    return (objectSignal.KKS == dataSignal.KKS) && ((dataSignal.Function == inputText) || (inputText == "*"));

                case BaseTypes.ObjectSCADA:
                    return (addressObject.ObjectName == dataSignal.KKS) && ((dataSignal.Function == inputText) || (inputText == "*"));

                default:
                    const string debugText = "DBGeneralSignal.CheckDataSignal";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + nameof(inputBase), DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + nameof(inputBase) + " is not created for this element");
            }
        }

        /// <summary>
        /// Find requested address area in address object
        /// </summary>
        /// <param name="addressObject">address object</param>
        /// <returns>address area</returns>
        private string GetAreaAddress(AddressObject addressObject)
        {
            Index += 2;
            if (Simulation)
                return "KKS.Area";

            foreach (AddressSingle addressSingle in addressObject.Addresses)
            {
                if (Cell[Index - 1] == addressSingle.ObjectVariableType)
                {
                    Value = addressSingle.Area;
                    return Value;
                }
            }
            Value = string.Empty;
            return Value;
        }

        /// <summary>
        /// Find requested address base in address object
        /// </summary>
        /// <param name="addressObject">address object</param>
        /// <returns>address base address</returns>
        private void GetBaseAddress(AddressObject addressObject)
        {
            Index += 2;
            if (Simulation)
                return;

            foreach (AddressSingle addressSingle in addressObject.Addresses)
            {
                if (Cell[Index - 1] == addressSingle.ObjectVariableType)
                {
                    Value = addressSingle.Area;
                    UpdateBaseAddress(addressSingle.Area, int.Parse(addressSingle.AddressStart), int.Parse(addressSingle.AddressSize));
                    return;
                }
            }
        }

        /// <summary>
        /// Find requested column in data
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <returns>column value</returns>
        private string DecodeData(DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase)
        {
            Index += 3;
            if (Simulation)
            {
                Value = "Data." + Cell[Index - 2];
                return Value;
            }
            foreach (DataSignal dataSignal in data.Signals)
            {
                if (CheckDataSignal(Cell[Index - 1], dataSignal, objectSignal, moduleSignal, addressObject, inputBase))
                {
                    Value = dataSignal.GetValueString(Cell[Index - 2], false);
                    dataSignal.SetValueFromString("1", KeywordColumn.Used);
                    return Value;
                }
            }

            Value = string.Empty;
            return Value;
        }

        /// <summary>
        /// find requested data in objects
        /// </summary>
        /// <param name="objectSignal">object</param>
        /// <returns>column value</returns>
        private string DecodeObjects(ObjectSignal objectSignal)
        {
            Index += 2;
            if (Simulation)
            {
                Value = "Object." + Cell[Index - 1];
                return Value;
            }

            Value = objectSignal.GetValueString(Cell[Index - 1], false);
            return Value;
        }

        /// <summary>
        /// find CPU in address object
        /// </summary>
        /// <param name="addressObject">object</param>
        /// <returns>column value</returns>
        private string DecodeCPU(AddressObject addressObject)
        {
            Index++;
            if (Simulation)
            {
                Value = "Object.CPU";
                return Value;
            }

            Value = addressObject.CPU;
            return Value;
        }

        /// <summary>
        /// find object name in address object
        /// </summary>
        /// <param name="addressObject">object</param>
        /// <returns>column value</returns>
        private string DecodeObjectName(AddressObject addressObject)
        {
            Index++;
            if (Simulation)
            {
                Value = "Object.ObjectName";
                return Value;
            }

            Value = addressObject.ObjectName;
            return Value;
        }

        /// <summary>
        /// find requested data in objects
        /// </summary>
        /// <param name="moduleSignal">object</param>
        /// <returns>column value</returns>
        private string DecodeModules(ModuleSignal moduleSignal)
        {
            Index += 2;

            if (Simulation)
            {
                Value = "Module." + Cell[Index - 1];
                return Value;
            }

            Value = moduleSignal.GetValueString(Cell[Index - 1], false);
            return Value;
        }

        /// <summary>
        /// Decode text of this line
        /// </summary>
        private string DecodeText()
        {
            Index += 2;
            Value = Cell[Index - 1];
            return Value;
        }

        /// <summary>
        /// Decode text of this line
        /// </summary>
        private void DecodeNone()
        {
            Index++;
            Value = string.Empty;
        }

        /// <summary>
        /// Calculate base address
        /// </summary>
        /// <param name="index">device index</param>
        /// <param name="line">coded line</param>
        /// <returns>value calculated with offset and multiplier</returns>
        private void DecodeBaseAddress(int index, List<string> line)
        {
            Index += 4;

            if (!int.TryParse(line[Index - 2], out int addressSize))
                return;

            int baseAddress = index * addressSize;
            if (int.TryParse(line[Index - 1], out int value))
                baseAddress += value;

            UpdateBaseAddress(line[Index - 3], baseAddress, addressSize);
        }

        /// <summary>
        /// Calculate address value
        /// </summary>
        /// <param name="line">coded line</param>
        /// <returns>value calculated with offset and multiplier</returns>
        private string DecodeAddress(List<string> line)
        {
            Index += 2;

            if (int.TryParse(line[Index - 1], out int value))
                return (BaseAddress + value).ToString();

            return BaseAddress.ToString();
        }

        /// <summary>
        /// decoding if statement from 4 cells
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine">decoded list</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        /// <param name="decodeReadOnly">do not put decoded cell to decode line</param>
        private List<string> DecodeIf(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase, bool decodeReadOnly)
        {
            Index++;
            string text = Cell[Index];
            string returnPart1;
            Debug debug = new Debug();
            string debugText;

            switch (text)
            {
                case KeywordDBChoices.Data:
                    returnPart1 = DecodeData(data, objectSignal, moduleSignal, addressObject, inputBase);
                    break;

                case KeywordDBChoices.Object:
                    returnPart1 = DecodeObjects(objectSignal);
                    break;

                case KeywordDBChoices.Modules:
                    returnPart1 = DecodeModules(moduleSignal);
                    break;

                case KeywordDBChoices.CPU:
                    returnPart1 = DecodeCPU(addressObject);
                    break;

                case KeywordDBChoices.ObjectName:
                    returnPart1 = DecodeObjectName(addressObject);
                    break;

                default:
                    debugText = "DBGeneralSignal.DecodeIf";

                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + text + " is not created for this element");
            }

            text = Cell[Index];
            bool ifTrue;

            Index++;
            switch (text)
            {
                case KeywordDBChoices.IsEmpty:
                    ifTrue = string.IsNullOrEmpty(returnPart1);
                    break;

                case KeywordDBChoices.IsNotEmpty:
                    ifTrue = !string.IsNullOrEmpty(returnPart1);
                    break;

                default:
                    string value = Value;
                    float value1, value2;

                    // then it is variable
                    DecodeCell(Index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, true);
                    //then search for operation sign
                    switch (text)
                    {
                        case KeywordDBChoices.Equal:
                            ifTrue = Value == value;
                            break;

                        case KeywordDBChoices.nEqual:
                            ifTrue = Value != value;
                            break;

                        case KeywordDBChoices.GreaterEqual:
                            if (float.TryParse(value, out value1) && float.TryParse(Value, out value2))
                                ifTrue = value1 >= value2;
                            else
                                ifTrue = false;
                            break;

                        case KeywordDBChoices.Greater:
                            if (float.TryParse(value, out value1) && float.TryParse(Value, out value2))
                                ifTrue = value1 > value2;
                            else
                                ifTrue = false;
                            break;

                        case KeywordDBChoices.LessEqual:
                            if (float.TryParse(value, out value1) && float.TryParse(Value, out value2))
                                ifTrue = value1 <= value2;
                            else
                                ifTrue = false;
                            break;

                        case KeywordDBChoices.Less:
                            if (float.TryParse(value, out value1) && float.TryParse(Value, out value2))
                                ifTrue = value1 < value2;
                            else
                                ifTrue = false;
                            break;

                        default:
                            debugText = "DBGeneralSignal.DecodeIf";

                            debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + text, DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(debugText + "." + text + " is not created for this element");
                    }
                    break;
            }

            List<string> cellText;
            //if = true
            if (ifTrue)
            {
                cellText = DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, decodeReadOnly);

                //peek false statement and update index to skip it
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, true);
            }
            //if = false
            else
            {
                //peek true statement and update index to skip it
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, true);
                cellText = DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, decodeReadOnly);
            }

            return cellText;
        }

        /// <summary>
        /// decoding multiline
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine">decoded list</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        private List<string> DecodeMultiline(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase)
        {
            Index++;
            List<string> cellText = new List<string>() { string.Empty };

            while (Cell[Index] != KeywordDBChoices.MultiLineEnd)
                TransferCellTextToDecoded(DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, false), cellText);

            return cellText;
        }

        /// <summary>
        /// Peek to next cell and return next cell to read next
        /// </summary>
        /// <param name="inIndex">index to search from</param>
        /// <returns>last index to of data</returns>
 /*       private int DecodePeek(int inIndex)
        {
            int newIndex = inIndex;
            string text = Line[newIndex];
            Debug debug = new Debug();
            string debugText;

            switch (text)
            {
                case KeywordDBChoices.If:
                    //peek for variable
                    newIndex++;
                    newIndex = DecodePeek(newIndex);
                    //peek operation
                    newIndex++;
                    switch (Line[newIndex])
                    {
                        case KeywordDBChoices.IsEmpty:
                        case KeywordDBChoices.IsNotEmpty:
                            break;

                        case KeywordDBChoices.Equal:
                        case KeywordDBChoices.nEqual:
                        case KeywordDBChoices.GreaterEqual:
                        case KeywordDBChoices.Greater:
                        case KeywordDBChoices.LessEqual:
                        case KeywordDBChoices.Less:
                            newIndex = DecodePeek(newIndex);
                            break;

                        default:
                            debugText = "DBGeneralSignal.DecodePeek.Operation";

                            debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + text, DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(debugText + "." + text + " is not created for this element");
                    }
                    //peek for true
                    newIndex = DecodePeek(newIndex);
                    //peek for false
                    newIndex = DecodePeek(newIndex);
                    return newIndex++;

                case KeywordDBChoices.Tab:
                    return newIndex++;

                case KeywordDBChoices.Data_:
                    return newIndex += 3;

                case KeywordDBChoices.Object:
                case KeywordDBChoices.Modules:
                case KeywordDBChoices.Text:
                case KeywordDBChoices.Address:
                case KeywordDBChoices.AddressArea:
                case KeywordDBChoices.GetBaseAddress:
                    return newIndex += 2;

                case KeywordDBChoices.CPU:
                case KeywordDBChoices.ObjectName:
                    return newIndex++;

                case KeywordDBChoices.BaseAddress:
                    return newIndex += 4;

                case KeywordDBChoices.MultiLine:
                    int layer = 1;
                    newIndex++;
                    while (layer > 0)
                    {
                        if (Line[newIndex] == KeywordDBChoices.MultiLine)
                            layer++;
                        else if (Line[newIndex] == KeywordDBChoices.MultiLineEnd)
                            layer--;

                        newIndex++;
                    }
                    return newIndex;

                default:
                    debugText = "DBGeneralSignal.DecodePeek.IF";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + text + " is not created for this element");
            }
        }
 */

        /// <summary>
        /// Transfer one list elements to another, first element is replacing last
        /// </summary>
        /// <param name="cellTexts"></param>
        /// <param name="decodedLine"></param>
        private void TransferCellTextToDecoded(List<string> cellTexts, List<string> decodedLine)
        {
            decodedLine[decodedLine.Count - 1] = cellTexts[0];

            for (int i = 1; i < cellTexts.Count; i++)
                decodedLine.Add(cellTexts[i]);
        }

        /// <summary>
        /// 1 cell decoding
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine"></param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        /// <param name="decodeReadOnly">do not put decoded cell to decode line</param>
        private List<string> DecodeCell(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase, bool decodeReadOnly)
        {
            string text = Cell[Index];
            List<string> cellText = new List<string>() { decodedLine.Last() };

            switch (text)
            {
                case KeywordDBChoices.If:
                    TransferCellTextToDecoded(DecodeIf(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, decodeReadOnly), cellText);
                    break;

                case KeywordDBChoices.Tab:
                    Index++;
                    cellText.Add(string.Empty);
                    break;

                case KeywordDBChoices.Data:
                    cellText[cellText.Count - 1] += DecodeData(data, objectSignal, moduleSignal, addressObject, inputBase);
                    break;

                case KeywordDBChoices.BaseAddress:
                    DecodeBaseAddress(index, Cell);
                    break;

                case KeywordDBChoices.Address:
                    cellText[cellText.Count - 1] += DecodeAddress(Cell);
                    break;

                case KeywordDBChoices.GetBaseAddress:
                    GetBaseAddress(addressObject);
                    break;

                case KeywordDBChoices.AddressArea:
                    cellText[cellText.Count - 1] += GetAreaAddress(addressObject);
                    break;

                case KeywordDBChoices.Object:
                    cellText[cellText.Count - 1] += DecodeObjects(objectSignal);
                    break;

                case KeywordDBChoices.Modules:
                    cellText[cellText.Count - 1] += DecodeModules(moduleSignal);
                    break;

                case KeywordDBChoices.Text:
                    cellText[cellText.Count - 1] += DecodeText();
                    break;

                case KeywordDBChoices.CPU:
                    cellText[cellText.Count - 1] += DecodeCPU(addressObject);
                    break;

                case KeywordDBChoices.ObjectName:
                    cellText[cellText.Count - 1] += DecodeObjectName(addressObject);
                    break;

                case KeywordDBChoices.MultiLine:
                    cellText = DecodeMultiline(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase);
                    break;

                case KeywordDBChoices.MultiLineEnd:
                case KeywordDBChoices.None:
                    DecodeNone();
                    break;

                default:
                    Debug debug = new Debug();
                    const string debugText = "DBGeneralSignal.DecodeCell";
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + text + " is not created for this element");
            }
            if (!decodeReadOnly)
                TransferCellTextToDecoded(cellText, decodedLine);

            return cellText;
        }

        /// <summary>
        /// Decode line
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <returns>decoded text line</returns>
        public void DecodeLine(DataTable dataTable, int index, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, AddressObject addressObject, BaseTypes inputBase)
        {
            Index = 0;
            List<string> decodedLine = new List<string>() { string.Empty };

            int count = 0;
            while (Index < Cell.Count)
            {
                count++;
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, addressObject, inputBase, false);
                if (count > 1000)
                {
                    Debug debug = new Debug();
                    const string debugText = "DBGeneralSignal.DecodeLine";
                    debug.ToFile(debugText + ": infinite loop", DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + ": infinite loop");
                }
            }
            for (int column = dataTable.Columns.Count; column < decodedLine.Count; column++)
                dataTable.Columns.Add(column.ToString());

            DataRow row = dataTable.NewRow();
            for (int i = 0; i < decodedLine.Count; i++)
                row[i] = decodedLine[i];

            dataTable.Rows.Add(row);
        }
    }
}