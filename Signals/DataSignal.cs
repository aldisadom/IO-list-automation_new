using IO_list_automation_new.General;
using System;

namespace IO_list_automation_new.Data
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
            ID = string.Empty;
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
}
