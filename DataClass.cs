using IO_list_automation_new.Forms;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        public string Function { get; private set; }
        public string Page { get; private set; }
        public string Changed { get; private set; }
        public string Terminal { get; private set; }
        public string Tag { get; private set; }

        public string UniqueKKS
        { get { return KKS + "_" + CPU; } }

        public string UniqueModuleName
        { get { return Cabinet + "_" + ModuleName + "_" + CPU; } }

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
                case KeywordColumn.ID:
                    ID = value;
                    break;

                case KeywordColumn.CPU:
                    CPU = value;
                    break;

                case KeywordColumn.KKS:
                    KKS = value;
                    break;

                case KeywordColumn.RangeMin:
                    RangeMin = value;
                    break;

                case KeywordColumn.RangeMax:
                    RangeMax = value;
                    break;

                case KeywordColumn.Units:
                    Units = value;
                    break;

                case KeywordColumn.FalseText:
                    FalseText = value;
                    break;

                case KeywordColumn.TrueText:
                    TrueText = value;
                    break;

                case KeywordColumn.Revision:
                    Revision = value;
                    break;

                case KeywordColumn.Cable:
                    Cable = value;
                    break;

                case KeywordColumn.Cabinet:
                    Cabinet = value;
                    break;

                case KeywordColumn.ModuleName:
                    ModuleName = value;
                    break;

                case KeywordColumn.Pin:
                    Pin = value;
                    break;

                case KeywordColumn.Channel:
                    Channel = value;
                    break;

                case KeywordColumn.IOText:
                    IOText = value;
                    break;

                case KeywordColumn.Page:
                    Page = value;
                    break;

                case KeywordColumn.Changed:
                    Changed = value;
                    break;

                case KeywordColumn.Operative:
                    Operative = value;
                    break;

                case KeywordColumn.KKSPlant:
                    KKSPlant = value;
                    break;

                case KeywordColumn.KKSLocation:
                    KKSLocation = value;
                    break;

                case KeywordColumn.KKSDevice:
                    KKSDevice = value;
                    break;

                case KeywordColumn.KKSFunction:
                    KKSFunction = value;
                    break;

                case KeywordColumn.Used:
                    Used = value;
                    break;

                case KeywordColumn.ObjectType:
                    ObjectType = value;
                    break;

                case KeywordColumn.Function:
                    Function = value;
                    break;

                case KeywordColumn.Terminal:
                    Terminal = value;
                    break;

                case KeywordColumn.Tag:
                    Tag = value;
                    break;
            }
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="suppressError">suppress alarm message, used only for transferring from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public override string GetValueString(string parameterName, bool suppressError)
        {
            switch (parameterName)
            {
                case KeywordColumn.ID:
                    return ID;

                case KeywordColumn.CPU:
                    return CPU;

                case KeywordColumn.KKS:
                    return KKS;

                case KeywordColumn.RangeMin:
                    return RangeMin;

                case KeywordColumn.RangeMax:
                    return RangeMax;

                case KeywordColumn.Units:
                    return Units;

                case KeywordColumn.FalseText:
                    return FalseText;

                case KeywordColumn.TrueText:
                    return TrueText;

                case KeywordColumn.Revision:
                    return Revision;

                case KeywordColumn.Cable:
                    return Cable;

                case KeywordColumn.Cabinet:
                    return Cabinet;

                case KeywordColumn.ModuleName:
                    return ModuleName;

                case KeywordColumn.Pin:
                    return Pin;

                case KeywordColumn.Channel:
                    return Channel;

                case KeywordColumn.IOText:
                    return IOText;

                case KeywordColumn.Page:
                    return Page;

                case KeywordColumn.Changed:
                    return Changed;

                case KeywordColumn.Operative:
                    return Operative;

                case KeywordColumn.KKSPlant:
                    return KKSPlant;

                case KeywordColumn.KKSLocation:
                    return KKSLocation;

                case KeywordColumn.KKSDevice:
                    return KKSDevice;

                case KeywordColumn.KKSFunction:
                    return KKSFunction;

                case KeywordColumn.Used:
                    return Used;

                case KeywordColumn.ObjectType:
                    return ObjectType;

                case KeywordColumn.Function:
                    return Function;

                case KeywordColumn.Terminal:
                    return Terminal;

                case KeywordColumn.Tag:
                    return Tag;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string debugText = "DataSignals.GetValueString";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + parameterName + " is not created for this element");
            }
        }

        /// <summary>
        /// Checks if at least one KKS part has value
        /// </summary>
        /// <returns>KKS found</returns>
        public bool HasKKS()
        {
            return (KKS.Length != 0) || (KKSPlant.Length != 0) || (KKSLocation.Length != 0) || (KKSDevice.Length != 0) || (KKSFunction.Length != 0);
        }

        /// <summary>
        /// Checks if design signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            if (string.IsNullOrEmpty(ModuleName))
                return false;
            else if (string.IsNullOrEmpty(IOText))
                return false;
            else if (string.IsNullOrEmpty(Pin) && string.IsNullOrEmpty(Channel))
                return false;

            return true;
        }

        /// <summary>
        /// get next index of letter
        /// </summary>
        /// <param name="text">search text</param>
        /// <param name="startIndex">search index start</param>
        /// <returns>index of letter</returns>
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
                    return i - startIndex;
            }
            return text.Length - startIndex;
        }

        /// <summary>
        /// find kks in text
        /// </summary>
        private string FindKKSInText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            int indexLetter = 0;
            int countLetter;
            int countDigits;

            int indexSpace1 = -1;
            int indexSpace2;

            //repeat not more than 50 times
            //find letter, then check if letter count is >= 2 and <=5
            //then check if number count is >=2
            //assume that from space to end its KKS
            for (int i = 0; i < 50; i++)
            {
                indexLetter = LetterIndex(text, indexLetter);
                indexSpace2 = text.IndexOf(" ", indexSpace1 + 1);

                //KKS indication can be further in word, not first occurrence
                if (indexLetter > indexSpace2)
                {
                    indexSpace1 = text.IndexOf(" ", indexSpace1 + 1);
                    indexLetter = indexSpace1;

                    if (indexSpace1 == -1)
                        return string.Empty;
                }
                else
                {
                    //no letter found
                    if (indexLetter < 0)
                        return string.Empty;

                    countLetter = CountConsecutiveLetters(text, indexLetter);
                    if (countLetter >= 2 && countLetter <= 5)
                    {
                        countDigits = CountConsecutiveNumbers(text, indexLetter + countLetter);
                        //Probably KKS
                        if (countDigits >= 2)
                        {
                            if (indexSpace2 == -1)
                                return text.Substring(indexSpace1 + 1);
                            else
                                return text.Substring(indexSpace1 + 1, indexSpace2 - indexSpace1 - 1);
                        }
                    }
                    // shift
                    indexLetter += countLetter;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// find kks in description
        /// </summary>
        public void FindKKSInSignal(bool forceSearch)
        {
            if (string.IsNullOrEmpty(KKS) || forceSearch)
                KKS = FindKKSInText(IOText);
        }

        /// <summary>
        /// Decode KKS signal into 4 parts based on standard
        /// </summary>
        public void KKSDecode()
        {
            if (string.IsNullOrEmpty(KKS))
                return;

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
            string kks = KKS;
            KKSDevice = string.Empty;
            KKSFunction = string.Empty;
            KKSLocation = string.Empty;
            KKSPlant = string.Empty;

            int indexLetter = 0;
            int countLetter;
            int countDigits;
            string kksAfter = string.Empty;

            int lengthPartKKS;

            //try finding part 1
            for (int i = 0; i < 50; i++)
            {
                indexLetter = LetterIndex(kks, indexLetter);
                //no letter found
                if (indexLetter < 0)
                    break;

                countLetter = CountConsecutiveLetters(kks, indexLetter);
                if (countLetter == 3)
                {
                    countDigits = CountConsecutiveNumbers(kks, indexLetter + countLetter);
                    if (countDigits == 2)
                    {
                        lengthPartKKS = countLetter + countDigits;
                        KKSLocation = kks.Substring(indexLetter, lengthPartKKS);

                        if (kks.Length > (indexLetter + lengthPartKKS))
                            kksAfter = kks.Substring(indexLetter + lengthPartKKS);

                        //found then break;
                        break;
                    }
                }
                // shift
                indexLetter += countLetter;
            }
            // part 1 not found
            if (string.IsNullOrEmpty(KKSLocation))
                kksAfter = kks;

            indexLetter = 0;
            //try find part 2
            for (int i = 0; i < 50; i++)
            {
                indexLetter = LetterIndex(kksAfter, indexLetter);

                //no letter found
                if (indexLetter < 0)
                    break;

                countLetter = CountConsecutiveLetters(kksAfter, indexLetter);
                if (countLetter == 2)
                {
                    countDigits = CountConsecutiveNumbers(kksAfter, indexLetter + countLetter);
                    //in account to design failure
                    if (countDigits == 2 || countDigits == 3)
                    {
                        lengthPartKKS = countLetter + countDigits;
                        KKSDevice = kksAfter.Substring(indexLetter, lengthPartKKS);

                        //what is left is part 3
                        if (kksAfter.Length > (indexLetter + lengthPartKKS))
                            KKSFunction = kksAfter.Substring(indexLetter + lengthPartKKS);

                        //found then break;
                        break;
                    }
                }
                // shift
                indexLetter += countLetter;
            }

            kks = KKSLocation + KKSDevice + KKSFunction;

            //what is left is part 0
            if (string.IsNullOrEmpty(kks))
            {
                KKSPlant = KKS;
            }
            else
            {
                int index = KKS.IndexOf(kks);
                if (index > 0)
                    KKSPlant = KKS.Substring(0, index);
            }
        }

        public void ExtractNumberFromChannel()
        {
            int indexNumber = NumberIndex(Channel, 0);

            //no number found
            if (indexNumber < 0)
            {
                Channel = string.Empty;
                return;
            }

            int countNumbers = CountConsecutiveNumbers(Channel, indexNumber);
            Channel = Channel.Substring(indexNumber, countNumbers);
        }
    }

    internal class DataClass : GeneralClass<DataSignal>
    {
        protected override List<GeneralColumn> GeneralGenerateColumnsList()
        {
            List<GeneralColumn> columns = new List<GeneralColumn>()
            {
                new GeneralColumn(KeywordColumn.ID, SettingsData.Default.ColumnID, true),
                new GeneralColumn(KeywordColumn.CPU, SettingsData.Default.ColumnCPU, true),
                new GeneralColumn(KeywordColumn.KKS, SettingsData.Default.ColumnKKS, false),
                new GeneralColumn(KeywordColumn.RangeMin, SettingsData.Default.ColumnRangeMin, true),
                new GeneralColumn(KeywordColumn.RangeMax, SettingsData.Default.ColumnRangeMax, true),
                new GeneralColumn(KeywordColumn.Units, SettingsData.Default.ColumnUnits, true),
                new GeneralColumn(KeywordColumn.FalseText, SettingsData.Default.ColumnFalseText, true),
                new GeneralColumn(KeywordColumn.TrueText, SettingsData.Default.ColumnTrueText, true),
                new GeneralColumn(KeywordColumn.Revision, SettingsData.Default.ColumnRevision, true),
                new GeneralColumn(KeywordColumn.Cable, SettingsData.Default.ColumnCable, true),
                new GeneralColumn(KeywordColumn.Cabinet, SettingsData.Default.ColumnCabinet, false),
                new GeneralColumn(KeywordColumn.ModuleName, SettingsData.Default.ColumnModuleName, false),
                new GeneralColumn(KeywordColumn.Pin, SettingsData.Default.ColumnPin, true),
                new GeneralColumn(KeywordColumn.Channel, SettingsData.Default.ColumnChannel, false),
                new GeneralColumn(KeywordColumn.IOText, SettingsData.Default.ColumnIOText, false),
                new GeneralColumn(KeywordColumn.Page, SettingsData.Default.ColumnPage, true),
                new GeneralColumn(KeywordColumn.Changed, SettingsData.Default.ColumnChanged, true),
                new GeneralColumn(KeywordColumn.Operative, SettingsData.Default.ColumnOperative, true),
                new GeneralColumn(KeywordColumn.KKSPlant, SettingsData.Default.ColumnKKSPlant, true),
                new GeneralColumn(KeywordColumn.KKSLocation, SettingsData.Default.ColumnKKSLocation, true),
                new GeneralColumn(KeywordColumn.KKSDevice, SettingsData.Default.ColumnKKSDevice, true),
                new GeneralColumn(KeywordColumn.KKSFunction, SettingsData.Default.ColumnKKSFunction, true),
                new GeneralColumn(KeywordColumn.Used, SettingsData.Default.ColumnUsed, false),
                new GeneralColumn(KeywordColumn.ObjectType, SettingsData.Default.ColumnObjectType, false),
                new GeneralColumn(KeywordColumn.Function, SettingsData.Default.ColumnFunction, false),
                new GeneralColumn(KeywordColumn.Terminal, SettingsData.Default.ColumnTerminal, true),
                new GeneralColumn(KeywordColumn.Tag, SettingsData.Default.ColumnTag, false),
            };

            return columns;
        }

        protected override void UpdateSettingsColumnsList()
        {
            ColumnList columns = Columns;

            SettingsData.Default.ColumnID = columns.GetColumnNumberFromKeyword(KeywordColumn.ID);
            SettingsData.Default.ColumnCPU = columns.GetColumnNumberFromKeyword(KeywordColumn.CPU);
            SettingsData.Default.ColumnKKS = columns.GetColumnNumberFromKeyword(KeywordColumn.KKS);
            SettingsData.Default.ColumnRangeMin = columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMin);
            SettingsData.Default.ColumnRangeMax = columns.GetColumnNumberFromKeyword(KeywordColumn.RangeMax);
            SettingsData.Default.ColumnUnits = columns.GetColumnNumberFromKeyword(KeywordColumn.Units);
            SettingsData.Default.ColumnFalseText = columns.GetColumnNumberFromKeyword(KeywordColumn.FalseText);
            SettingsData.Default.ColumnTrueText = columns.GetColumnNumberFromKeyword(KeywordColumn.TrueText);
            SettingsData.Default.ColumnRevision = columns.GetColumnNumberFromKeyword(KeywordColumn.Revision);
            SettingsData.Default.ColumnCable = columns.GetColumnNumberFromKeyword(KeywordColumn.Cable);
            SettingsData.Default.ColumnCabinet = columns.GetColumnNumberFromKeyword(KeywordColumn.Cabinet);
            SettingsData.Default.ColumnModuleName = columns.GetColumnNumberFromKeyword(KeywordColumn.ModuleName);
            SettingsData.Default.ColumnPin = columns.GetColumnNumberFromKeyword(KeywordColumn.Pin);
            SettingsData.Default.ColumnChannel = columns.GetColumnNumberFromKeyword(KeywordColumn.Channel);
            SettingsData.Default.ColumnIOText = columns.GetColumnNumberFromKeyword(KeywordColumn.IOText);
            SettingsData.Default.ColumnPage = columns.GetColumnNumberFromKeyword(KeywordColumn.Page);
            SettingsData.Default.ColumnChanged = columns.GetColumnNumberFromKeyword(KeywordColumn.Changed);
            SettingsData.Default.ColumnKKSPlant = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSPlant);
            SettingsData.Default.ColumnKKSLocation = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSLocation);
            SettingsData.Default.ColumnKKSDevice = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSDevice);
            SettingsData.Default.ColumnKKSFunction = columns.GetColumnNumberFromKeyword(KeywordColumn.KKSFunction);
            SettingsData.Default.ColumnUsed = columns.GetColumnNumberFromKeyword(KeywordColumn.Used);
            SettingsData.Default.ColumnObjectType = columns.GetColumnNumberFromKeyword(KeywordColumn.ObjectType);
            SettingsData.Default.ColumnFunction = columns.GetColumnNumberFromKeyword(KeywordColumn.Function);
            SettingsData.Default.ColumnTerminal = columns.GetColumnNumberFromKeyword(KeywordColumn.Terminal);
            SettingsData.Default.ColumnTag = columns.GetColumnNumberFromKeyword(KeywordColumn.Tag);

            SettingsData.Default.Save();
        }

        public DataClass(ProgressIndication progress, DataGridView grid) : base("Data", nameof(FileExtensions.data), progress, grid, true)
        {
        }

        public DataClass() : base("Data", nameof(FileExtensions.data), null, null, true)
        {
        }

        /// <summary>
        /// Extract data from design data
        /// </summary>
        /// <param name="design">design data</param>
        public void ExtractFromDesign(DesignClass design)
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.ExtractDataFromDesign, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.ExtractDataFromDesign, design.Signals.Count);

            UpdateColumnNumbers(design.BaseColumns.Columns);

            Signals.Clear();
            design.Grid.GetData();
            string keyword;

            foreach (DesignSignal designSignal in design.Signals)
            {
                DataSignal dataSignal = new DataSignal();

                // go through all column in design and send it to signals
                foreach (GeneralColumn column in design.Columns)
                {
                    keyword = column.Keyword;
                    dataSignal.SetValueFromString(designSignal.GetValueString(keyword, true), keyword);
                }
                dataSignal.FindKKSInSignal(false);
                dataSignal.KKSDecode();
                dataSignal.ExtractNumberFromChannel();

                Signals.Add(dataSignal);
                Progress.UpdateProgressBar(Signals.Count);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.ExtractDataFromDesign + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }

        /// <summary>
        /// Create KKS from 4 KKS parts
        /// </summary>
        public void MakeKKS()
        {
            Debug debug = new Debug();
            debug.ToFile(Resources.KKSCombine, DebugLevels.High, DebugMessageType.Info);

            Progress.RenameProgressBar(Resources.KKSCombine, Signals.Count);

            KKSEdit kksEdit = new KKSEdit();

            int progress = 0;
            foreach (DataSignal dataSignal in Signals)
            {
                if (!dataSignal.HasKKS())
                    continue;

                //transfer KKS to KKS edit form
                kksEdit.UpdateKKS(dataSignal.KKS, dataSignal.KKSPlant, dataSignal.KKSLocation, dataSignal.KKSDevice, dataSignal.KKSFunction);

                //not configured
                if (kksEdit.Configured == 0)
                {
                    kksEdit.ShowDialog();
                    //canceled edit
                    if (kksEdit.Configured == -1)
                    {
                        debug.ToFile(Resources.KKSCombine + " - " + Resources.Canceled, DebugLevels.Minimum, DebugMessageType.Info);
                        break;
                    }
                }
                dataSignal.SetValueFromString(kksEdit.GetCombined(), KeywordColumn.KKS);
                progress++;
                Progress.UpdateProgressBar(progress);
            }
            Progress.HideProgressBar();

            debug.ToFile(Resources.KKSCombine + " - " + Resources.Finished, DebugLevels.High, DebugMessageType.Info);
        }
    }
}