using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_list_automation_new.DB
{
    internal class DBGeneralSignal
    {
        public List<string> Line { get; private set; }

        public string TagType { get; private set; }
        public string MemoryArea { get; private set; }
        public string Address { get; private set; }

        private int Index;

        private string Value = "";

        private bool Simulation = false;

        public DBGeneralSignal(bool simulation)
        {
            TagType = string.Empty;
            MemoryArea = string.Empty;
            Address = string.Empty;
            Line = new List<string>();
            Simulation = simulation;
        }

        public DBGeneralSignal()
        {
            TagType = string.Empty;
            MemoryArea = string.Empty;
            Address = string.Empty;
            Line = new List<string>();
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
        /// Find IO tag in data based on function of object KKS
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">object</param>
        /// <returns>IO tag</returns>
        private string DecodeIOTag(DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index+=2;
            if (Simulation)
            {
                Value = "KKS." + Line[Index - 1];
                return Value;
            }

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
            else
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if ((moduleSignal.ModuleName == data.Signals[i].ModuleName) && (data.Signals[i].Channel == Line[Index - 1]))
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
            else
            {
                for (int i = 0; i < data.Signals.Count; i++)
                {
                    if ((moduleSignal.ModuleName == data.Signals[i].ModuleName) && (data.Signals[i].Channel == Line[Index - 1]))
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
        /// Find requested column in data
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <returns>column value</returns>
        private string DecodeData(DataClass data, ObjectSignal objectSignal)
        {
            Index+=2;
            if (Simulation)
            {
                Value = "Data." + Line[Index-1];
                return Value;
            }

            for (int i = 0; i < data.Signals.Count; i++)
            {
                if (objectSignal.KKS == data.Signals[i].KKS)
                {
                    Value = data.Signals[i].GetValueString(Line[Index-1], false);
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
            Index+=2;
            if (Simulation)
            {
                Value = "Object." + Line[Index-1];
                return Value;
            }

            Value = objectSignal.GetValueString(Line[Index-1], false);
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
        /// Set Tag of this line
        /// </summary>
        /// <param name="tagType">tag type</param>
        private void DecodeTagType(string tagType)
        {
            Index+=2;
            TagType = tagType;
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
        /// Calculate value
        /// </summary>
        /// <param name="index">device index</param>
        /// <param name="line">coded line</param>
        /// <returns>value calculated with offset and multiplier</returns>
        private string DecodeIndex(int index,List<string> line)
        {
            Index += 4;

            MemoryArea = line[Index-3];
            if (!int.TryParse(line[Index -2], out int _value))
            {
                Value = string.Empty;
                return Value;
            }

            int _returnValue = index * _value;
            if (int.TryParse(line[Index -1], out _value))
                _returnValue += _value;

            Address = _returnValue.ToString();
            Value = Address;
            return Value;
        }

        /// <summary>
        /// decoding if statement from 4 cells
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine">decoded list</param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        private void DecodeIf(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            Index++;
            string _text = Line[Index];
            string _returnPart1;
            int _newIndex;
            Debug _debug = new Debug();
            string text;

            switch (_text)
            {
                case KeywordDBChoices.Data:
                    _returnPart1 = DecodeData(data, objectSignal);
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

                default:
                    text = "DBGeneralSignal.DecodeIf";

                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + _text + " is not created for this element");
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
                    //decode padaro kad iraso i linija reik padaryt kad nerasytu
                    DecodeCell(Index, decodedLine, data, objectSignal, moduleSignal);
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
                            text = "DBGeneralSignal.DecodeIf";

                            _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                            throw new InvalidProgramException(text + "." + _text + " is not created for this element");
                    }
                    break;
            }
            //if = true
            if (_ifTrue)
            {
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal);

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
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal);
            }
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

            switch (_text)
            {
                case KeywordDBChoices.If:
                    //peek for variable
                    _newIndex = DecodePeek(_newIndex);
                    //skip operation
                    _newIndex++;
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
                case KeywordDBChoices.IOChannel:
                case KeywordDBChoices.Text:
                case KeywordDBChoices.VariableType:
                    return _newIndex+=2;

                case KeywordDBChoices.Index:
                    return _newIndex += 4;

                default:
                    Debug _debug = new Debug();
                    const string text = "DBGeneralSignal.DecodePeek.IF";
                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + _text + " is not created for this element");
            }
        }

        /// <summary>
        /// 1 cell decoding
        /// </summary>
        /// <param name="index">index of object</param>
        /// <param name="decodedLine"></param>
        /// <param name="data">data</param>
        /// <param name="objectSignal">object</param>
        /// <param name="moduleSignal">module</param>
        private void DecodeCell(int index, List<string> decodedLine, DataClass data, ObjectSignal objectSignal, ModuleSignal moduleSignal)
        {
            string _text = Line[Index];

            switch (_text)
            {
                case KeywordDBChoices.If:
                    DecodeIf(index, decodedLine, data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.Tab:
                    decodedLine.Add(string.Empty);
                    break;

                case KeywordDBChoices.Data:
                    decodedLine[decodedLine.Count - 1] += DecodeData(data, objectSignal);
                    break;

                case KeywordDBChoices.Index:
                    decodedLine[decodedLine.Count - 1] += DecodeIndex(index,Line);
                    break;

                case KeywordDBChoices.Object:
                    decodedLine[decodedLine.Count - 1] += DecodeObjects(objectSignal);
                    break;

                case KeywordDBChoices.Modules:
                    decodedLine[decodedLine.Count - 1] += DecodeModules(moduleSignal);
                    break;

                case KeywordDBChoices.VariableType:
                    DecodeTagType(Line[Index]);
                    break;

                case KeywordDBChoices.Text:
                    decodedLine[decodedLine.Count - 1] += DecodeText();
                    break;

                case KeywordDBChoices.IOTag:
                    decodedLine[decodedLine.Count - 1] += DecodeIOTag(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.IOChannel:
                    decodedLine[decodedLine.Count - 1] += DecodeIOChannel(data, objectSignal, moduleSignal);
                    break;

                case KeywordDBChoices.None:
                    DecodeNone();
                    break;

                default:
                    Debug _debug = new Debug();
                    const string text = "DBGeneralSignal.DecodeCell";
                    _debug.ToFile(text + " " + Resources.ParameterNotFound + ":" + _text, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(text + "." + _text + " is not created for this element");
            }
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

            while (Index < Line.Count)
                DecodeCell(index, decodedLine, data, objectSignal, moduleSignal);

            return decodedLine;
        }
    }
}
