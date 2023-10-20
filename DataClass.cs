using ExcelDataReader;
using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using SharpCompress.Common;
using SharpCompress.Readers.Zip;
using SwiftExcel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace IO_list_automation_new
{
    internal class DataSignal : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string Operative { get; private set; }
        public string KKS { get; private set; }
        public string KKSPlant { get; private set; }
        public string KKSLocation { get; private set; }
        public string KKSDevice { get; private set; }
        public string KKSFunction { get; private set; }
        public string Used { get; private set; }
        public string ObjectType { get; private set; }
        public string RangeMin { get; private set; }
        public string RangeMax { get; private set; }
        public string Units { get; private set; }
        public string FalseText { get; private set; }
        public string TrueText { get; private set; }
        public string Revision { get; private set; }
        public string Cable { get; private set; }
        public string Cabinet { get; private set; }
        public string ModuleName { get; private set; }
        public string Pin { get; private set; }
        public string Channel { get; private set; }
        public string IOText { get; private set; }
        public string ObjectName { get; private set; }
        public string ObjectDetalisation { get; private set; }
        public string FunctionText { get; private set; }
        public string Function { get; private set; }
        public string Page { get; private set; }
        public string Changed { get; private set; }
        public string Terminal { get; private set; }
        public string Tag { get; private set; }

        public DataSignal() : base()
        {
            CPU = string.Empty;
            Operative = string.Empty;
            KKS = string.Empty;
            KKSPlant = string.Empty;
            KKSLocation = string.Empty;
            KKSDevice = string.Empty;
            KKSFunction = string.Empty;
            RangeMin = string.Empty;
            RangeMax = string.Empty;
            Units = string.Empty;
            FalseText = string.Empty;
            TrueText = string.Empty;
            Revision = string.Empty;
            Cable = string.Empty;
            Cabinet = string.Empty;
            ModuleName = string.Empty;
            Pin = string.Empty;
            Channel = string.Empty;
            IOText = string.Empty;
            Page = string.Empty;
            Changed = string.Empty;
            Used = string.Empty;
            ObjectType = string.Empty;
            ObjectName = string.Empty;
            ObjectDetalisation = string.Empty;
            FunctionText = string.Empty;
            Function = string.Empty;
            Terminal = string.Empty;
            Tag = string.Empty;
        }
        
        /// <summary>
        /// Parsing data from excel or grid, according to Column to signal element
        /// </summary>
        /// <param name="value">value to be passed</param>
        /// <param name="parameterName">parameter to be set</param>
        public override void SetValueFromString(string value, string parameterName)
        {
            switch (parameterName)
            {
                case ConstCol.ColumnNameID:
                    ID = value;
                    break;
                case ConstCol.ColumnNameCPU:
                    CPU = value;
                    break;
                case ConstCol.ColumnNameKKS:
                    KKS = value;
                    break;
                case ConstCol.ColumnNameRangeMin:
                    RangeMin = value;
                    break;
                case ConstCol.ColumnNameRangeMax:
                    RangeMax = value;
                    break;
                case ConstCol.ColumnNameUnits:
                    Units = value;
                    break;
                case ConstCol.ColumnNameFalseText:
                    FalseText = value;
                    break;
                case ConstCol.ColumnNameTrueText:
                    TrueText = value;
                    break;
                case ConstCol.ColumnNameRevision:
                    Revision = value;
                    break;
                case ConstCol.ColumnNameCable:
                    Cable = value;
                    break;
                case ConstCol.ColumnNameCabinet:
                    Cabinet = value;
                    break;
                case ConstCol.ColumnNameModuleName:
                    ModuleName = value;
                    break;
                case ConstCol.ColumnNamePin:
                    Pin = value;
                    break;
                case ConstCol.ColumnNameChannel:
                    Channel = value;
                    break;
                case ConstCol.ColumnNameIOText:
                    IOText = value;
                    break;
                case ConstCol.ColumnNamePage:
                    Page = value;
                    break;
                case ConstCol.ColumnNameChanged:
                    Changed = value;
                    break;
                case ConstCol.ColumnNameOperative:
                    Operative = value;
                    break;
                case ConstCol.ColumnNameKKSPlant:
                    KKSPlant = value;
                    break;
                case ConstCol.ColumnNameKKSLocation:
                    KKSLocation = value;
                    break;
                case ConstCol.ColumnNameKKSDevice:
                    KKSDevice = value;
                    break;
                case ConstCol.ColumnNameKKSFunction:
                    KKSFunction = value;
                    break;
                case ConstCol.ColumnNameUsed:
                    Used = value;
                    break;
                case ConstCol.ColumnNameObjectType:
                    ObjectType = value;
                    break;
                case ConstCol.ColumnNameObjectName:
                    ObjectName = value;
                    break;
                case ConstCol.ColumnNameObjectDetalisation:
                    ObjectDetalisation = value;
                    break;
                case ConstCol.ColumnNameFunctionText:
                    FunctionText = value;
                    break;
                case ConstCol.ColumnNameFunction:
                    Function = value;
                    break;
                case ConstCol.ColumnNameTerminal:
                    Terminal = value;
                    break;
                case ConstCol.ColumnNameTag:
                    Tag = value;
                    break;
            }
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="supressError">supress alarm message, used only for transfering from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public override string GetValueString(string parameterName, bool supressError)
        {
            string _returnValue = string.Empty;
            switch (parameterName)
            {
                case ConstCol.ColumnNameID:
                    _returnValue = ID;
                    break;
                case ConstCol.ColumnNameCPU:
                    _returnValue = CPU;
                    break;
                case ConstCol.ColumnNameKKS:
                    _returnValue = KKS;
                    break;
                case ConstCol.ColumnNameRangeMin:
                    _returnValue = RangeMin;
                    break;
                case ConstCol.ColumnNameRangeMax:
                    _returnValue = RangeMax;
                    break;
                case ConstCol.ColumnNameUnits:
                    _returnValue = Units;
                    break;
                case ConstCol.ColumnNameFalseText:
                    _returnValue = FalseText;
                    break;
                case ConstCol.ColumnNameTrueText:
                    _returnValue = TrueText;
                    break;
                case ConstCol.ColumnNameRevision:
                    _returnValue = Revision;
                    break;
                case ConstCol.ColumnNameCable:
                    _returnValue = Cable;
                    break;
                case ConstCol.ColumnNameCabinet:
                    _returnValue = Cabinet;
                    break;
                case ConstCol.ColumnNameModuleName:
                    _returnValue = ModuleName;
                    break;
                case ConstCol.ColumnNamePin:
                    _returnValue = Pin;
                    break;
                case ConstCol.ColumnNameChannel:
                    _returnValue = Channel;
                    break;
                case ConstCol.ColumnNameIOText:
                    _returnValue = IOText;
                    break;
                case ConstCol.ColumnNamePage:
                    _returnValue = Page;
                    break;
                case ConstCol.ColumnNameChanged:
                    _returnValue = Changed;
                    break;
                case ConstCol.ColumnNameOperative:
                    _returnValue = Operative;
                    break;
                case ConstCol.ColumnNameKKSPlant:
                    _returnValue = KKSPlant ;
                    break;
                case ConstCol.ColumnNameKKSLocation:
                    _returnValue = KKSLocation;
                    break;
                case ConstCol.ColumnNameKKSDevice:
                    _returnValue = KKSDevice;
                    break;
                case ConstCol.ColumnNameKKSFunction:
                    _returnValue = KKSFunction;
                    break;
                case ConstCol.ColumnNameUsed:
                    _returnValue = Used;
                    break;
                case ConstCol.ColumnNameObjectType:
                    _returnValue = ObjectType;
                    break;
                case ConstCol.ColumnNameObjectName:
                    _returnValue = ObjectName;
                    break;
                case ConstCol.ColumnNameObjectDetalisation:
                    _returnValue = ObjectDetalisation;
                    break;
                case ConstCol.ColumnNameFunctionText:
                    _returnValue = FunctionText;
                    break;
                case ConstCol.ColumnNameFunction:
                    _returnValue = Function;
                    break;
                case ConstCol.ColumnNameTerminal:
                    _returnValue = Terminal;
                    break;
                case ConstCol.ColumnNameTag:
                    _returnValue = Tag;
                    break;
                default:
                    if (!supressError)
                    {
                        Debug _debug = new Debug();
                        _debug.ToPopUp(Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    }
                    break;
            }
            return _returnValue;
        }

        /// <summary>
        /// Checks if at least one KKS part has value
        /// </summary>
        /// <returns></returns>
        public bool HasKKS()
        {
            return (KKS.Length != 0) || (KKSPlant.Length != 0) || (KKSLocation.Length != 0) || (KKSDevice.Length != 0) || (KKSFunction.Length != 0);
        }
        /// <summary>
        /// Checks if design signal is valid:
        /// *Has Channel or PIN
        /// *Has Module
        /// *Has IO funcion text
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            bool _returnValue = true;

            if (ModuleName == "" || ModuleName == null)
                _returnValue = false;
            else if (IOText == "" || IOText == null)
                _returnValue = false;
            else if ((Pin == "" || Pin == null) && (Channel == "" || Channel == null))
                _returnValue = false;

            return _returnValue;
        }

        /// <summary>
        /// get next index of number
        /// </summary>
        /// <param name="text">search text</param>
        /// <param name="startIndex">search index start</param>
        /// <returns>index of number</returns>
        private int NumberIndex(string text, int startIndex)
        {
            for (int i = startIndex; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// get number of consecutive numbers
        /// </summary>
        /// <param name="text">search text</param>
        /// <param name="startIndex">search index start</param>
        /// <returns>number of consecutive numbers</returns>
        private int CountConsecutiveNumbers(string text, int startIndex)
        {
            for (int i = startIndex; i < text.Length; i++)
            {
                if (!Char.IsDigit(text[i]))
                    return i - startIndex;
            }
            return text.Length - startIndex;
        }

        /// <summary>
        /// get next index of letter
        /// </summary>
        /// <param name="text">search text</param>
        /// <param name="startIndex">search index start</param>
        /// <returns>index of letter</returns>
        private int LetterIndex(string text, int startIndex)
        {
            for (int i = startIndex; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// get number of consecutive letters
        /// </summary>
        /// <param name="text">search text</param>
        /// <param name="startIndex">search index start</param>
        /// <returns>number of consecutive letter</returns>
        private int CountConsecutiveLetters(string text, int startIndex)
        {
            for (int i = startIndex; i < text.Length; i++)
            {
                if (!Char.IsLetter(text[i]))
                    return i- startIndex;
            }
            return text.Length - startIndex;
        }

        /// <summary>
        /// find kks in text
        /// </summary>
        private string FindKKSInText(string text)
        {
            string _KKS = string.Empty;

            if (text != null && text != string.Empty)
            {
                int _indexLetter = 0;
                int _countLetter = 0;
                int _countDigits = 0;

                int _indexSpace1 = -1;
                int _indexSpace2 = 0;

                //repeat not more than 50 times
                //find letter, then check if letter count is >= 2 and <=5
                //then check if number count is >=2
                // asume that from space to end its KKS
                for (int i = 0; i < 50; i++)
                {
                    _indexLetter = LetterIndex(text, _indexLetter);
                    _indexSpace2 = text.IndexOf(" ", _indexSpace1 + 1);

                    //KKS indication can be further in word, not first occurence
                    if (_indexLetter > _indexSpace2)
                    {
                        _indexSpace1 = text.IndexOf(" ", _indexSpace1 + 1);
                        _indexLetter = _indexSpace1;

                        if (_indexSpace1 == -1)
                            break;
                    }
                    else
                    {
                        if (_indexLetter >= 0)
                        {
                            _countLetter = CountConsecutiveLetters(text, _indexLetter);
                            if (_countLetter >= 2 && _countLetter <= 5)
                            {
                                _countDigits = CountConsecutiveNumbers(text, _indexLetter + _countLetter);
                                //Probably KKS
                                if (_countDigits >= 2)
                                {

                                    if (_indexSpace2 == -1)
                                        _KKS = text.Substring(_indexSpace1+1);
                                    else
                                        _KKS = text.Substring(_indexSpace1+1, _indexSpace2 - _indexSpace1-1);
                                    break;
                                }
                            }
                        }
                        //no letter found
                        else
                            break;

                        // shift 
                        _indexLetter += _countLetter;
                    }
                    
                    
                }
            }
            return _KKS;
        }

        /// <summary>
        /// find kks in description
        /// </summary>
        public void FindKKSInSignal(bool forceSearch)
        {
            if (KKS == string.Empty || forceSearch)
                KKS = FindKKSInText(IOText);
        }

        /// <summary>
        /// Decode KKS signal into 4 parts based on standard
        /// </summary>
        public void KKSDecode()
        {
            /*
             * X - number or letter
             * A - letter
             * N - number
             * Part
             *      0 - XXX     (1S , KB1, 2, 28)
             *      1 - AAANN   (HNA40, HOP20) 
             *      2 - AANNN   (CP001, AN001), based on experience it can failure in design phase, CP01, AN01
             *      3 - AANN    (XQ01, XB02)
             */
            string _KKS = KKS;
            KKSDevice = string.Empty;
            KKSFunction = string.Empty;
            KKSLocation = string.Empty;
            KKSPlant = string.Empty;

            if (_KKS != null && _KKS != string.Empty)
            {
                int _indexLetter = 0;
                int _countLetter = 0;
                int _countDigits = 0;
                string _KKSAfter = string.Empty;

                int _lengthPartKKS = 0;

                //try finding part 1
                for (int i = 0; i < 50; i++)
                {
                    _indexLetter = LetterIndex(_KKS, _indexLetter);
                    if (_indexLetter >= 0)
                    {
                        _countLetter = CountConsecutiveLetters(_KKS, _indexLetter);
                        if (_countLetter == 3)
                        {
                            _countDigits = CountConsecutiveNumbers(_KKS, _indexLetter + _countLetter);
                            if (_countDigits == 2)
                            {
                                _lengthPartKKS = _countLetter + _countDigits;
                                KKSLocation = _KKS.Substring(_indexLetter, _lengthPartKKS);

                                if (_KKS.Length > (_indexLetter + _lengthPartKKS))
                                    _KKSAfter = _KKS.Substring(_indexLetter + _lengthPartKKS);

                                //found then break;
                                break;
                            }
                        }
                    }
                    //no letter found
                    else
                        break;

                    // shift 
                    _indexLetter += _countLetter;
                }
                // part 1 not found
                if (KKSLocation == string.Empty)
                    _KKSAfter = _KKS;

                _indexLetter = 0;
                //try find part 2
                for (int i = 0; i < 50; i++)
                {
                    _indexLetter = LetterIndex(_KKSAfter, _indexLetter);
                    if (_indexLetter >= 0)
                    {
                        _countLetter = CountConsecutiveLetters(_KKSAfter, _indexLetter);
                        if (_countLetter == 2)
                        {
                            _countDigits = CountConsecutiveNumbers(_KKSAfter, _indexLetter + _countLetter);
                            //in account to design failure
                            if (_countDigits == 2 || _countDigits == 3)
                            {
                                _lengthPartKKS = _countLetter + _countDigits;
                                KKSDevice = _KKSAfter.Substring(_indexLetter, _lengthPartKKS);

                                //what is left is part 3
                                if (_KKSAfter.Length > (_indexLetter + _lengthPartKKS))
                                    KKSFunction = _KKSAfter.Substring(_indexLetter + _lengthPartKKS);

                                //found then break;
                                break;
                            }
                        }
                    }
                    //no letter found
                    else
                        break;

                    // shift 
                    _indexLetter += _countLetter;
                }

                _KKS = KKSLocation + KKSDevice + KKSFunction;

                //what is left is part 0
                if (_KKS == string.Empty)
                    KKSPlant = KKS;
                else
                {
                    int _index = KKS.IndexOf(_KKS);
                    if (_index > 0)
                        KKSPlant = KKS.Substring(0, _index);
                }
            }
        }
    }
    
    internal class DataClass : GeneralClass<DataSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(ConstCol.ColumnNameID, SettingsData.Default.ColumnID, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCPU, SettingsData.Default.ColumnCPU, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKS, SettingsData.Default.ColumnKKS, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameRangeMin, SettingsData.Default.ColumnRangeMin, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameRangeMax, SettingsData.Default.ColumnRangeMax, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameUnits, SettingsData.Default.ColumnUnits, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFalseText, SettingsData.Default.ColumnFalseText, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameTrueText, SettingsData.Default.ColumnTrueText, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameRevision, SettingsData.Default.ColumnRevision, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCable, SettingsData.Default.ColumnCable, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameCabinet, SettingsData.Default.ColumnCabinet, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameModuleName, SettingsData.Default.ColumnModuleName, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNamePin, SettingsData.Default.ColumnPin, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameChannel, SettingsData.Default.ColumnChannel, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameIOText, SettingsData.Default.ColumnIOText, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNamePage, SettingsData.Default.ColumnPage, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameChanged, SettingsData.Default.ColumnChanged, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameOperative, SettingsData.Default.ColumnOperative, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKSPlant, SettingsData.Default.ColumnKKSPlant, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKSLocation, SettingsData.Default.ColumnKKSLocation, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKSDevice, SettingsData.Default.ColumnKKSDevice, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameKKSFunction, SettingsData.Default.ColumnKKSFunction, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameUsed, SettingsData.Default.ColumnUsed, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectType, SettingsData.Default.ColumnObjectType, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectName, SettingsData.Default.ColumnObjectName, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameObjectDetalisation, SettingsData.Default.ColumnObjectDetalisation, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFunctionText, SettingsData.Default.ColumnFunctionText, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameFunction, SettingsData.Default.ColumnFunction, false));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameTerminal, SettingsData.Default.ColumnTerminal, true));
            _columns.Add(new GeneralColumn(ConstCol.ColumnNameTag, SettingsData.Default.ColumnTag, false));

            return _columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            SettingsData.Default.ColumnID = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameID);
            SettingsData.Default.ColumnCPU = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameCPU);
            SettingsData.Default.ColumnKKS = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameKKS);
            SettingsData.Default.ColumnRangeMin = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameRangeMin);
            SettingsData.Default.ColumnRangeMax = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameRangeMax);
            SettingsData.Default.ColumnUnits = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameUnits);
            SettingsData.Default.ColumnFalseText = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameFalseText);
            SettingsData.Default.ColumnTrueText = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameTrueText);
            SettingsData.Default.ColumnRevision = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameRevision);
            SettingsData.Default.ColumnCable = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameCable);
            SettingsData.Default.ColumnCabinet = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameCabinet);
            SettingsData.Default.ColumnModuleName = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameModuleName);
            SettingsData.Default.ColumnPin = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNamePin);
            SettingsData.Default.ColumnChannel = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameChannel);
            SettingsData.Default.ColumnIOText = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameIOText);
            SettingsData.Default.ColumnPage = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNamePage);
            SettingsData.Default.ColumnChanged = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameChanged);
            SettingsData.Default.ColumnKKSPlant = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameKKSPlant);
            SettingsData.Default.ColumnKKSLocation = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameKKSLocation);
            SettingsData.Default.ColumnKKSDevice = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameKKSDevice);
            SettingsData.Default.ColumnKKSFunction = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameKKSFunction);
            SettingsData.Default.ColumnUsed = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameUsed);
            SettingsData.Default.ColumnObjectType = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameObjectType);
            SettingsData.Default.ColumnObjectName = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameObjectName);
            SettingsData.Default.ColumnObjectDetalisation = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameObjectDetalisation);
            SettingsData.Default.ColumnFunctionText = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameFunctionText);
            SettingsData.Default.ColumnFunction = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameFunction);
            SettingsData.Default.ColumnTerminal = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameTerminal);
            SettingsData.Default.ColumnTag = _columns.GetColumnNumberFromKeyword(ConstCol.ColumnNameTag);

            SettingsData.Default.Save();
        }

        public DataClass(ProgressIndication progress, DataGridView grid) :base("Data",false, FileExtensions.data.ToString(), progress, grid)
        {
        }

        public DataClass() : base("Data", false, FileExtensions.data.ToString(), null, null)
        {
        }

        /// <summary>
        /// Extract data from design data
        /// </summary>
        /// <param name="design">design data</param>
        public void ExtractFromDesign (DesignClass design)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ExtractDataFromDesign, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ExtractDataFromDesign, design.Signals.Count );

            UpdateColumnNumbers(design.BaseColumns.Columns);

            Signals.Clear();
            design.Grid.GetData();
            string _keyword = string.Empty;
            for (int _designNumber = 0; _designNumber < design.Signals.Count; _designNumber++)
            {
                DataSignal _dataSignal = new DataSignal();
                DesignSignal _designSignal = design.Signals.ElementAt(_designNumber);

                // go through all collumn in design and send it to signals
                foreach (GeneralColumn _column in design.Columns)
                {
                    _keyword = _column.Keyword;
                    _dataSignal.SetValueFromString(_designSignal.GetValueString(_keyword,true), _keyword);
                }
                _dataSignal.FindKKSInSignal(false);
                _dataSignal.KKSDecode();

                Signals.Add(_dataSignal);
                Progress.UpdateProgressBar(_designNumber);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ExtractDataFromDesign + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }

        /// <summary>
        /// Create KKS from 4 KKS parts
        /// </summary>
        public void MakeKKS()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.KKSCombine, DebugLevels.Development, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.KKSCombine, Signals.Count);
                
            KKSEdit _KKSEdit = new KKSEdit();

            for (int _signalNumber = 0; _signalNumber < Signals.Count; _signalNumber++)
            {
                DataSignal _signal = Signals.ElementAt(_signalNumber);
                if(_signal.HasKKS())
                {
                    //transfer KKS to KKS edit form
                    _KKSEdit.UpdateKKS(_signal.KKS, _signal.KKSPlant, _signal.KKSLocation, _signal.KKSDevice, _signal.KKSFunction);
                    
                    //not configured
                    if (_KKSEdit.Configured == 0)
                    {
                        _KKSEdit.ShowDialog();
                        //canceled edit
                        if (_KKSEdit.Configured == -1)
                        {
                            debug.ToFile(Resources.KKSCombine+ " - " + Resources.Canceled, DebugLevels.Minimum, DebugMessageType.Info);
                            break;
                        }
                    }
                    _signal.SetValueFromString(_KKSEdit.GetCombined(), ConstCol.ColumnNameKKS);
                }
                Progress.UpdateProgressBar(_signalNumber);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.KKSCombine + " - " + Resources.Finished, DebugLevels.Development, DebugMessageType.Info);
        }
    }
}
