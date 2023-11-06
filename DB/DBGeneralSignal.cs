using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IO_list_automation_new.DB
{
    internal class DBGeneralSignal
    {
        public List<string> Line { get; private set; }

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
            Line = new List<string>();
            Simulation = simulation;
            BaseAddressSet = false;
        }

        public DBGeneralSignal()
        {
            MemoryArea = string.Empty;
            AddressSize = 0;
            BaseAddress = 0;
            Line = new List<string>();
            BaseAddressSet = false;
        }

        /// <summary>
        /// Update signal data
        /// </summary>
        /// <param name="newList">new data of signal</param>
        public void SetValue(List<string> newList)
        {
            Line = newList;
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
        /// Find IO tag in data based on function of object KKS
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">object</param>
        /// <returns>IO tag</returns>
        private string DecodeIOTag(DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index += 2;
            if (Simulation)
            {
                Value = "KKS." + Line[Index - 1];
                return Value;
            }
            //objects based
            if (moduleSignal == null)
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if ((objectSignal.KKS == data.Signals[i].KKS) && (data.Signals[i].Function == Line[Index - 1]))
                    {
                        Value = data.Signals[i].Tag;
                        return Value;
                    }
                }
            }
            //module based
            else
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if (!int.TryParse(data.Signals[i].Channel, out int _channel))
                        continue;

                    if ((moduleSignal.ModuleName == data.Signals[i].ModuleName) && (_channel == int.Parse(Line[Index - 1])))
                    {
                        Value = data.Signals[i].Tag;
                        return Value;
                    }
                }
            }
            Value = string.Empty;
            return Value;
        }

        /// <summary>
        /// Find IO Pin in data based on channel number of module
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">object</param>
        /// <returns>IO Pin</returns>
        private string DecodeIOPin(DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index += 2;
            if (Simulation)
            {
                Value = "KKS.Pin_" + Line[Index - 1];
                return Value;
            }
            //objects based
            if (moduleSignal == null)
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if ((objectSignal.KKS == data.Signals[i].KKS) && (data.Signals[i].Function == Line[Index - 1]))
                    {
                        Value = data.Signals[i].Pin;
                        return Value;
                    }
                }
            }
            //module based
            else
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if (!int.TryParse(data.Signals[i].Channel, out int _channel))
                        continue;

                    if ((moduleSignal.ModuleName == data.Signals[i].ModuleName) && (_channel == int.Parse(Line[Index - 1])))
                    {
                        Value = data.Signals[i].Pin;
                        return Value;
                    }
                }
            }

            Value = string.Empty;
            return Value;
        }

        /// <summary>
        /// Find IO channel in data based on channel number of module
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">object</param>
        /// <returns>IO channel</returns>
        private string DecodeIOChannel(DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index += 2;
            if (Simulation)
            {
                Value = "KKS.Channel_" + Line[Index - 1];
                return Value;
            }
            //objects based
            if (moduleSignal == null)
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if ((objectSignal.KKS == data.Signals[i].KKS) && (data.Signals[i].Function == Line[Index - 1]))
                    {
                        Value = data.Signals[i].Channel;
                        return Value;
                    }
                }
            }
            //module based
            else
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if (!int.TryParse(data.Signals[i].Channel, out int _channel))
                        continue;

                    if ((moduleSignal.ModuleName == data.Signals[i].ModuleName) && (_channel == int.Parse(Line[Index - 1])))
                    {
                        Value = data.Signals[i].Channel;
                        return Value;
                    }
                }
            }

            Value = string.Empty;
            return Value;
        }

        /// <summary>
        /// Find IO text in data based on channel number of module
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">object</param>
        /// <returns>IO channel</returns>
        private string DecodeIOText(DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index += 2;
            if (Simulation)
            {
                Value = "KKS." + Line[Index - 1];
                return Value;
            }
            //objects based
            if (moduleSignal == null)
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if ((objectSignal.KKS == data.Signals[i].KKS) && (data.Signals[i].Function == Line[Index - 1]))
                    {
                        Value = data.Signals[i].IOText;
                        return Value;
                    }
                }
            }
            //module based
            else
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if (!int.TryParse(data.Signals[i].Channel, out int _channel))
                        continue;

                    if ((moduleSignal.ModuleName == data.Signals[i].ModuleName) && (_channel == int.Parse(Line[Index - 1])))
                    {
                        Value = data.Signals[i].IOText;
                        return Value;
                    }
                }
            }

            Value = string.Empty;
            return Value;
        }

        /// <summary>
        /// Find requested column in data
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <returns>column value</returns>
        private string DecodeData(DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index += 2;
            if (Simulation)
            {
                Value = "Data." + Line[Index - 1];
                return Value;
            }

            if (moduleSignal == null)
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if (objectSignal.KKS == data.Signals[i].KKS)
                    {
                        Value = data.Signals[i].GetValueString(Line[Index - 1], false);
                        return Value;
                    }
                }
            }
            else
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if (moduleSignal.ModuleName == data.Signals[i].ModuleName)
                    {
                        Value = data.Signals[i].GetValueString(Line[Index - 1], false);
                        return Value;
                    }
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
                Value = "Object." + Line[Index - 1];
                return Value;
            }

            Value = objectSignal.GetValueString(Line[Index - 1], false);
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
                Value = "Module." + Line[Index - 1];
                return Value;
            }

            Value = moduleSignal.GetValueString(Line[Index - 1], false);
            return Value;
        }

        /// <summary>
        /// Decode text of this line
        /// </summary>
        private string DecodeText()
        {
            Index += 2;
            Value = Line[Index - 1];
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

            if (!int.TryParse(line[Index - 2], out int _addressSize))
                return;

            int _baseAddress = index * _addressSize;
            if (int.TryParse(line[Index - 1], out int _value))
                _baseAddress += _value;

            UpdateBaseAddress(line[Index - 3], _baseAddress, _addressSize);
        }

        /// <summary>
        /// Calculate address value
        /// </summary>
        /// <param name="line">coded line</param>
        /// <returns>value calculated with offset and multiplier</returns>
        private string DecodeAddress(List<string> line)
        {
            Index += 2;

            if (int.TryParse(line[Index - 1], out int _value))
                return (BaseAddress + _value).ToString();

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
        private List<string> DecodeIf(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, bool decodeReadOnly)
        {
            Index++;
            string _text = Line[Index];
            string _returnPart1;
            int _newIndex;
            Debug _debug = new Debug();
            string _debugText;

            switch (_text)
            {
                case KeywordDBChoices.Data:
                    _returnPart1 = DecodeData(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.Object:
                    _returnPart1 = DecodeObjects(objectSignal);
                    break;

                case KeywordDBChoices.Modules:
                    _returnPart1 = DecodeModules(moduleSignal);
                    break;

                case KeywordDBChoices.IOTag:
                    _returnPart1 = DecodeIOTag(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.IOChannel:
                    _returnPart1 = DecodeIOChannel(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.IOPin:
                    _returnPart1 = DecodeIOPin(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.IOText:
                    _returnPart1 = DecodeIOText(data, objectSignal, moduleSignal);
                    break;

                default:
                    _debugText = "DBGeneralSignal.DecodeIf";

                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + _text + " is not created for this element");
            }

            _text = Line[Index];
            bool _ifTrue;

            Index++;
            switch (_text)
            {
                case KeywordDBChoices.IsEmpty:
                    _ifTrue = string.IsNullOrEmpty(_returnPart1);
                    break;

                case KeywordDBChoices.IsNotEmpty:
                    _ifTrue = !string.IsNullOrEmpty(_returnPart1);
                    break;

                default:
                    string _value = Value;
                    float _value1, _value2;

                    // then it is variable
                    DecodeCell(Index, decodedLine, data, objectSignal, moduleSignal, true);
                    //then search for operation sign
                    switch (_text)
                    {
                        case KeywordDBChoices.Equal:
                            _ifTrue = Value == _value;
                            break;

                        case KeywordDBChoices.nEqual:
                            _ifTrue = Value != _value;
                            break;

                        case KeywordDBChoices.GreaterEqual:
                            if (float.TryParse(_value, out _value1) && float.TryParse(Value, out _value2))
                                _ifTrue = _value1 >= _value2;
                            else
                                _ifTrue = false;
                            break;

                        case KeywordDBChoices.Greater:
                            if (float.TryParse(_value, out _value1) && float.TryParse(Value, out _value2))
                                _ifTrue = _value1 > _value2;
                            else
                                _ifTrue = false;
                            break;

                        case KeywordDBChoices.LessEqual:
                            if (float.TryParse(_value, out _value1) && float.TryParse(Value, out _value2))
                                _ifTrue = _value1 <= _value2;
                            else
                                _ifTrue = false;
                            break;

                        case KeywordDBChoices.Less:
                            if (float.TryParse(_value, out _value1) && float.TryParse(Value, out _value2))
                                _ifTrue = _value1 < _value2;
                            else
                                _ifTrue = false;
                            break;

                        default:
                            _debugText = "DBGeneralSignal.DecodeIf";

                            _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(_debugText + "." + _text + " is not created for this element");
                    }
                    break;
            }

            List<string> _cellText;
            //if = true
            if (_ifTrue)
            {
                _cellText = DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, decodeReadOnly);

                //peek false statement and update index to skip it
                _newIndex = DecodePeek(Index);
                Index = _newIndex;
            }
            //if = false
            else
            {
                //peek true statement and update index to skip it
                _newIndex = DecodePeek(Index);

                Index = _newIndex;
                _cellText = DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, decodeReadOnly);
            }

            return _cellText;
        }

        /// <summary>
        /// decoding multiline
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine">decoded list</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        private List<string> DecodeMultiline(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index++;
            List<string> _cellText = new List<string>() { string.Empty };

            while (Line[Index] != KeywordDBChoices.MultiLineEnd)
                TransferCellTextToDecoded(DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, false), _cellText);

            return _cellText;
        }

        /// <summary>
        /// Peek to next cell and return next cell to read next
        /// </summary>
        /// <param name="inIndex">index to search from</param>
        /// <returns>last index to of data</returns>
        private int DecodePeek(int inIndex)
        {
            int _newIndex = inIndex;
            string _text = Line[_newIndex];
            Debug _debug = new Debug();
            string _debugText;

            switch (_text)
            {
                case KeywordDBChoices.If:
                    //peek for variable
                    _newIndex = DecodePeek(_newIndex);
                    //peek operation
                    _newIndex++;
                    switch (Line[_newIndex])
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
                            _newIndex = DecodePeek(_newIndex);
                            break;

                        default:
                            _debugText = "DBGeneralSignal.DecodePeek.Operation";

                            _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(_debugText + "." + _text + " is not created for this element");
                    }
                    //peek for true
                    _newIndex = DecodePeek(_newIndex);
                    //peek for false
                    _newIndex = DecodePeek(_newIndex);
                    return _newIndex++;

                case KeywordDBChoices.Tab:
                    return _newIndex++;

                case KeywordDBChoices.Data:
                case KeywordDBChoices.Object:
                case KeywordDBChoices.Modules:
                case KeywordDBChoices.IOTag:
                case KeywordDBChoices.IOPin:
                case KeywordDBChoices.IOChannel:
                case KeywordDBChoices.IOText:
                case KeywordDBChoices.Text:
                case KeywordDBChoices.Address:
                    return _newIndex += 2;

                case KeywordDBChoices.BaseIndex:
                    return _newIndex += 4;

                case KeywordDBChoices.MultiLine:
                    int _layer = 1;
                    _newIndex++;
                    while (_layer > 0)
                    {
                        if (Line[_newIndex] == KeywordDBChoices.MultiLine)
                            _layer++;
                        else if (Line[_newIndex] == KeywordDBChoices.MultiLineEnd)
                            _layer--;

                        _newIndex++;
                    }
                    return _newIndex;

                default:
                    _debugText = "DBGeneralSignal.DecodePeek.IF";
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + _text + " is not created for this element");
            }
        }

        /// <summary>
        /// Transfer one list elements to another, first element is replacing last
        /// </summary>
        /// <param name="cellText"></param>
        /// <param name="decodedLine"></param>
        private void TransferCellTextToDecoded(List<string> cellText, List<string> decodedLine)
        {
            decodedLine[decodedLine.Count - 1] = cellText[0];
            for (int i = 1; i < cellText.Count; i++)
                decodedLine.Add(cellText[i]);
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
        private List<string> DecodeCell(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal, bool decodeReadOnly)
        {
            string _text = Line[Index];
            List<string> _cellText = new List<string>() { decodedLine.Last() };

            switch (_text)
            {
                case KeywordDBChoices.If:
                    TransferCellTextToDecoded(DecodeIf(index, decodedLine, data, objectSignal, moduleSignal, decodeReadOnly), _cellText);
                    break;

                case KeywordDBChoices.Tab:
                    Index++;
                    _cellText.Add(string.Empty);
                    break;

                case KeywordDBChoices.Data:
                    _cellText[_cellText.Count - 1] += DecodeData(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.BaseIndex:
                    DecodeBaseAddress(index, Line);
                    break;

                case KeywordDBChoices.Address:
                    _cellText[_cellText.Count - 1] += DecodeAddress(Line);
                    break;

                case KeywordDBChoices.Object:
                    _cellText[_cellText.Count - 1] += DecodeObjects(objectSignal);
                    break;

                case KeywordDBChoices.Modules:
                    _cellText[_cellText.Count - 1] += DecodeModules(moduleSignal);
                    break;

                case KeywordDBChoices.Text:
                    _cellText[_cellText.Count - 1] += DecodeText();
                    break;

                case KeywordDBChoices.IOPin:
                    _cellText[_cellText.Count - 1] += DecodeIOPin(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.IOTag:
                    _cellText[_cellText.Count - 1] += DecodeIOTag(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.IOChannel:
                    _cellText[_cellText.Count - 1] += DecodeIOChannel(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.IOText:
                    _cellText[_cellText.Count - 1] += DecodeIOText(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.MultiLine:
                    _cellText = DecodeMultiline(index, decodedLine, data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.MultiLineEnd:
                case KeywordDBChoices.None:
                    DecodeNone();
                    break;

                default:
                    Debug _debug = new Debug();
                    const string _debugText = "DBGeneralSignal.DecodeCell";
                    _debug.ToFile(_debugText + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + "." + _text + " is not created for this element");
            }
            if (!decodeReadOnly)
                TransferCellTextToDecoded(_cellText, decodedLine);

            return _cellText;
        }

        /// <summary>
        /// Decode line
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <returns>decoded text line</returns>
        public List<string> DecodeLine(int index, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index = 0;
            List<string> decodedLine = new List<string>() { string.Empty };

            int _count = 0;
            while (Index < Line.Count)
            {
                _count++;
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal, false);
                if (_count > 1000)
                {
                    Debug _debug = new Debug();
                    const string _debugText = "DBGeneralSignal.DecodeLine";
                    _debug.ToFile(_debugText + ": infinite loop", DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(_debugText + ": infinite loop");
                }
            }
            return decodedLine;
        }
    }
}