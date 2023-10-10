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
        private string CPU { get; set; }
        private string Operative { get; set; }
        private string KKS { get; set; }
        private string KKSPlant { get; set; }
        private string KKSLocation { get; set; }
        private string KKSDevice { get; set; }
        private string KKSFunction { get; set; }
        private string Used { get; set; }
        private string ObjectType { get; set; }
        private string RangeMin { get; set; }
        private string RangeMax { get; set; }
        private string Units { get; set; }
        private string FalseText { get; set; }
        private string TrueText { get; set; }
        private string Revision { get; set; }
        private string Cable { get; set; }
        private string Cabinet { get; set; }
        private string ModuleName { get; set; }
        private string Pin { get; set; }
        private string Channel { get; set; }
        private string IOText { get; set; }
        private string ObjectName { get; set; }
        private string ObjectDetalisation { get; set; }
        private string FunctionText { get; set; }
        private string Function { get; set; }
        private string Page { get; set; }
        private string Changed { get; set; }
        private string Terminal { get; set; }

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
                case Const.ColumnNameID:
                    SetID(value);
                    break;
                case Const.ColumnNameCPU:
                    CPU = value;
                    break;
                case Const.ColumnNameKKS:
                    KKS = value;
                    break;
                case Const.ColumnNameRangeMin:
                    RangeMin = value;
                    break;
                case Const.ColumnNameRangeMax:
                    RangeMax = value;
                    break;
                case Const.ColumnNameUnits:
                    Units = value;
                    break;
                case Const.ColumnNameFalseText:
                    FalseText = value;
                    break;
                case Const.ColumnNameTrueText:
                    TrueText = value;
                    break;
                case Const.ColumnNameRevision:
                    Revision = value;
                    break;
                case Const.ColumnNameCable:
                    Cable = value;
                    break;
                case Const.ColumnNameCabinet:
                    Cabinet = value;
                    break;
                case Const.ColumnNameModuleName:
                    ModuleName = value;
                    break;
                case Const.ColumnNamePin:
                    Pin = value;
                    break;
                case Const.ColumnNameChannel:
                    Channel = value;
                    break;
                case Const.ColumnNameIOText:
                    IOText = value;
                    break;
                case Const.ColumnNamePage:
                    Page = value;
                    break;
                case Const.ColumnNameChanged:
                    Changed = value;
                    break;
                case Const.ColumnNameOperative:
                    Operative = value;
                    break;
                case Const.ColumnNameKKSPlant:
                    KKSPlant = value;
                    break;
                case Const.ColumnNameKKSLocation:
                    KKSLocation = value;
                    break;
                case Const.ColumnNameKKSDevice:
                    KKSDevice = value;
                    break;
                case Const.ColumnNameKKSFunction:
                    KKSFunction = value;
                    break;
                case Const.ColumnNameUsed:
                    Used = value;
                    break;
                case Const.ColumnNameObjectType:
                    ObjectType = value;
                    break;
                case Const.ColumnNameObjectName:
                    ObjectName = value;
                    break;
                case Const.ColumnNameObjectDetalisation:
                    ObjectDetalisation = value;
                    break;
                case Const.ColumnNameFunctionText:
                    FunctionText = value;
                    break;
                case Const.ColumnNameFunction:
                    Function = value;
                    break;
                case Const.ColumnNameTerminal:
                    Terminal = value;
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
                case Const.ColumnNameID:
                    _returnValue = GetID();
                    break;
                case Const.ColumnNameCPU:
                    _returnValue = CPU;
                    break;
                case Const.ColumnNameKKS:
                    _returnValue = KKS;
                    break;
                case Const.ColumnNameRangeMin:
                    _returnValue = RangeMin;
                    break;
                case Const.ColumnNameRangeMax:
                    _returnValue = RangeMax;
                    break;
                case Const.ColumnNameUnits:
                    _returnValue = Units;
                    break;
                case Const.ColumnNameFalseText:
                    _returnValue = FalseText;
                    break;
                case Const.ColumnNameTrueText:
                    _returnValue = TrueText;
                    break;
                case Const.ColumnNameRevision:
                    _returnValue = Revision;
                    break;
                case Const.ColumnNameCable:
                    _returnValue = Cable;
                    break;
                case Const.ColumnNameCabinet:
                    _returnValue = Cabinet;
                    break;
                case Const.ColumnNameModuleName:
                    _returnValue = ModuleName;
                    break;
                case Const.ColumnNamePin:
                    _returnValue = Pin;
                    break;
                case Const.ColumnNameChannel:
                    _returnValue = Channel;
                    break;
                case Const.ColumnNameIOText:
                    _returnValue = IOText;
                    break;
                case Const.ColumnNamePage:
                    _returnValue = Page;
                    break;
                case Const.ColumnNameChanged:
                    _returnValue = Changed;
                    break;
                case Const.ColumnNameOperative:
                    _returnValue = Operative;
                    break;
                case Const.ColumnNameKKSPlant:
                    _returnValue = KKSPlant ;
                    break;
                case Const.ColumnNameKKSLocation:
                    _returnValue = KKSLocation;
                    break;
                case Const.ColumnNameKKSDevice:
                    _returnValue = KKSDevice;
                    break;
                case Const.ColumnNameKKSFunction:
                    _returnValue = KKSFunction;
                    break;
                case Const.ColumnNameUsed:
                    _returnValue = Used;
                    break;
                case Const.ColumnNameObjectType:
                    _returnValue = ObjectType;
                    break;
                case Const.ColumnNameObjectName:
                    _returnValue = ObjectName;
                    break;
                case Const.ColumnNameObjectDetalisation:
                    _returnValue = ObjectDetalisation;
                    break;
                case Const.ColumnNameFunctionText:
                    _returnValue = FunctionText;
                    break;
                case Const.ColumnNameFunction:
                    _returnValue = Function;
                    break;
                case Const.ColumnNameTerminal:
                    _returnValue = Terminal;
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

        public string GetKKS()
        {
            return KKS;
        }

        public string GetKKSPlant()
        {
            return KKSPlant;
        }

        public string GetKKSLocation()
        {
            return KKSLocation;
        }

        public string GetKKSDevice()
        {
            return KKSDevice;
        }

        public string GetKKSFunction()
        {
            return KKSFunction;
        }

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
        public override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> _columns = new List<GeneralColumn>();

            _columns.Add(new GeneralColumn(Const.ColumnNameID, Settings.Default.DataColumnID));
            _columns.Add(new GeneralColumn(Const.ColumnNameCPU, Settings.Default.DataColumnCPU));
            _columns.Add(new GeneralColumn(Const.ColumnNameKKS, Settings.Default.DataColumnKKS));
            _columns.Add(new GeneralColumn(Const.ColumnNameRangeMin, Settings.Default.DataColumnRangeMin));
            _columns.Add(new GeneralColumn(Const.ColumnNameRangeMax, Settings.Default.DataColumnRangeMax));
            _columns.Add(new GeneralColumn(Const.ColumnNameUnits, Settings.Default.DataColumnUnits));
            _columns.Add(new GeneralColumn(Const.ColumnNameFalseText, Settings.Default.DataColumnFalseText));
            _columns.Add(new GeneralColumn(Const.ColumnNameTrueText, Settings.Default.DataColumnTrueText));
            _columns.Add(new GeneralColumn(Const.ColumnNameRevision, Settings.Default.DataColumnRevision));
            _columns.Add(new GeneralColumn(Const.ColumnNameCable, Settings.Default.DataColumnCable));
            _columns.Add(new GeneralColumn(Const.ColumnNameCabinet, Settings.Default.DataColumnCabinet));
            _columns.Add(new GeneralColumn(Const.ColumnNameModuleName, Settings.Default.DataColumnModuleName));
            _columns.Add(new GeneralColumn(Const.ColumnNamePin, Settings.Default.DataColumnPin));
            _columns.Add(new GeneralColumn(Const.ColumnNameChannel, Settings.Default.DataColumnChannel));
            _columns.Add(new GeneralColumn(Const.ColumnNameIOText, Settings.Default.DataColumnIOText));
            _columns.Add(new GeneralColumn(Const.ColumnNamePage, Settings.Default.DataColumnPage));
            _columns.Add(new GeneralColumn(Const.ColumnNameChanged, Settings.Default.DataColumnChanged));
            _columns.Add(new GeneralColumn(Const.ColumnNameOperative, Settings.Default.DataColumnOperative));
            _columns.Add(new GeneralColumn(Const.ColumnNameKKSPlant, Settings.Default.DataColumnKKSPlant));
            _columns.Add(new GeneralColumn(Const.ColumnNameKKSLocation, Settings.Default.DataColumnKKSLocation));
            _columns.Add(new GeneralColumn(Const.ColumnNameKKSDevice, Settings.Default.DataColumnKKSDevice));
            _columns.Add(new GeneralColumn(Const.ColumnNameKKSFunction, Settings.Default.DataColumnKKSFunction));
            _columns.Add(new GeneralColumn(Const.ColumnNameUsed, Settings.Default.DataColumnUsed));
            _columns.Add(new GeneralColumn(Const.ColumnNameObjectType, Settings.Default.DataColumnObjectType));
            _columns.Add(new GeneralColumn(Const.ColumnNameObjectName, Settings.Default.DataColumnObjectName));
            _columns.Add(new GeneralColumn(Const.ColumnNameObjectDetalisation, Settings.Default.DataColumnObjectDetalisation));
            _columns.Add(new GeneralColumn(Const.ColumnNameFunctionText, Settings.Default.DataColumnFunctionText));
            _columns.Add(new GeneralColumn(Const.ColumnNameFunction, Settings.Default.DataColumnFunction));
            _columns.Add(new GeneralColumn(Const.ColumnNameTerminal, Settings.Default.DataColumnTerminal));

            return _columns;
        }

        public override void UpdateSettingsColumnsList()
        {
            ColumnList _columns = Columns;

            Settings.Default.DataColumnID = _columns.GetColumnNumberFromKeyword(Const.ColumnNameID);
            Settings.Default.DataColumnCPU = _columns.GetColumnNumberFromKeyword(Const.ColumnNameCPU);
            Settings.Default.DataColumnKKS = _columns.GetColumnNumberFromKeyword(Const.ColumnNameKKS);
            Settings.Default.DataColumnRangeMin = _columns.GetColumnNumberFromKeyword(Const.ColumnNameRangeMin);
            Settings.Default.DataColumnRangeMax = _columns.GetColumnNumberFromKeyword(Const.ColumnNameRangeMax);
            Settings.Default.DataColumnUnits = _columns.GetColumnNumberFromKeyword(Const.ColumnNameUnits);
            Settings.Default.DataColumnFalseText = _columns.GetColumnNumberFromKeyword(Const.ColumnNameFalseText);
            Settings.Default.DataColumnTrueText = _columns.GetColumnNumberFromKeyword(Const.ColumnNameTrueText);
            Settings.Default.DataColumnRevision = _columns.GetColumnNumberFromKeyword(Const.ColumnNameRevision);
            Settings.Default.DataColumnCable = _columns.GetColumnNumberFromKeyword(Const.ColumnNameCable);
            Settings.Default.DataColumnCabinet = _columns.GetColumnNumberFromKeyword(Const.ColumnNameCabinet);
            Settings.Default.DataColumnModuleName = _columns.GetColumnNumberFromKeyword(Const.ColumnNameModuleName);
            Settings.Default.DataColumnPin = _columns.GetColumnNumberFromKeyword(Const.ColumnNamePin);
            Settings.Default.DataColumnChannel = _columns.GetColumnNumberFromKeyword(Const.ColumnNameChannel);
            Settings.Default.DataColumnIOText = _columns.GetColumnNumberFromKeyword(Const.ColumnNameIOText);
            Settings.Default.DataColumnPage = _columns.GetColumnNumberFromKeyword(Const.ColumnNamePage);
            Settings.Default.DataColumnChanged = _columns.GetColumnNumberFromKeyword(Const.ColumnNameChanged);
            Settings.Default.DataColumnKKSPlant = _columns.GetColumnNumberFromKeyword(Const.ColumnNameKKSPlant);
            Settings.Default.DataColumnKKSLocation = _columns.GetColumnNumberFromKeyword(Const.ColumnNameKKSLocation);
            Settings.Default.DataColumnKKSDevice = _columns.GetColumnNumberFromKeyword(Const.ColumnNameKKSDevice);
            Settings.Default.DataColumnKKSFunction = _columns.GetColumnNumberFromKeyword(Const.ColumnNameKKSFunction);
            Settings.Default.DataColumnUsed = _columns.GetColumnNumberFromKeyword(Const.ColumnNameUsed);
            Settings.Default.DataColumnObjectType = _columns.GetColumnNumberFromKeyword(Const.ColumnNameObjectType);
            Settings.Default.DataColumnObjectName = _columns.GetColumnNumberFromKeyword(Const.ColumnNameObjectName);
            Settings.Default.DataColumnObjectDetalisation = _columns.GetColumnNumberFromKeyword(Const.ColumnNameObjectDetalisation);
            Settings.Default.DataColumnFunctionText = _columns.GetColumnNumberFromKeyword(Const.ColumnNameFunctionText);
            Settings.Default.DataColumnFunction = _columns.GetColumnNumberFromKeyword(Const.ColumnNameFunction);
            Settings.Default.DataColumnTerminal = _columns.GetColumnNumberFromKeyword(Const.ColumnNameTerminal);

            Settings.Default.Save();
        }

        public DataClass(ProgressIndication progress, DataGridView grid) :base(Const.DataName, progress, grid)
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

            design.Gridasikas.GridGetData();
            string _keyword = string.Empty;
            for (int _signalNumber = 0; _signalNumber < design.Signals.Count; _signalNumber++)
            {
                DataSignal _signal = new DataSignal();
                DesignSignal _designSignal = design.Signals.ElementAt(_signalNumber);

                // go through all collumn in design and send it to signals
                foreach (GeneralColumn column in design.Columns)
                {
                    _keyword = column.GetColumnKeyword();
                    _signal.SetValueFromString(_designSignal.GetValueString(_keyword,true), _keyword);
                }
                _signal.FindKKSInSignal(false);
                _signal.KKSDecode();

                Signals.Add(_signal);
                Progress.UpdateProgressBar(_signalNumber);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ExtractDataFromDesign + " - finished", DebugLevels.Development, DebugMessageType.Info);
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
                    _KKSEdit.UpdateKKS(_signal.GetKKS(), _signal.GetKKSPlant(), _signal.GetKKSLocation(), _signal.GetKKSDevice(), _signal.GetKKSFunction());
                    
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
                    _signal.SetValueFromString(_KKSEdit.GetCombined(), Const.ColumnNameKKS);
                }
                Progress.UpdateProgressBar(_signalNumber);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.KKSCombine + " - finished", DebugLevels.Development, DebugMessageType.Info);
        }
    }
}
