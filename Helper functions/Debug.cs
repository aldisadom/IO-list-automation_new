using IO_list_automation_new.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace IO_list_automation_new
{
    internal enum DebugLevels
    {
        None = 0,
        Minimum = 1,
        Medium = 2,
        High = 3,
        Development = 10,
    }

    internal enum DebugMessageType
    {
        Info,
        Warning,
        Alarm,
        Critical,
    }

    internal class Debug
    {
        /// <summary>
        /// Function that returns number as string with additional zeroes
        /// </summary>
        /// <param name="_number">number to be converted to string</param>
        /// <param name="decimals">minimum string length</param>
        /// <returns>number as string with additional zeroes</returns>
        private string NumberToString(int _number, int decimals)
        {
            string _formattedString = _number.ToString();
            //check size of number if not enough numbers add 0 in front
            for (int i = _formattedString.Length; i < decimals; i++)
                _formattedString = "0" + _formattedString;
            return _formattedString;
        }

        /// <summary>
        /// Get current time
        /// </summary>
        /// <returns>Formatted current time as string</returns>
        private string FormatTime()
        {
            System.DateTime currentTime = DateTime.Now;
            string _formattedTime = NumberToString(currentTime.Year, 4) + "-" + NumberToString(currentTime.Month, 2) + "-" + NumberToString(currentTime.Day, 2) + " " +
                                    NumberToString(currentTime.Hour, 2) + ":" + NumberToString(currentTime.Minute, 2) + ":" + NumberToString(currentTime.Second, 2) + "." +
                                    NumberToString(currentTime.Millisecond, 3);
            return _formattedTime;
        }

        /// <summary>
        /// Clear debug file
        /// </summary>
        public void ClearDebug()
        {
            File.Create("_debug.txt").Dispose();
        }

        /// <summary>
        /// Write debug message to file
        /// </summary>
        /// <param name="message">debug message</param>
        private void WriteToFile(string message)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "_debug.txt"), true))
                outputFile.WriteLine(message);
        }

        /// <summary>
        /// Creates debug pop up message
        /// </summary>
        /// <param name="message">debug message</param>
        /// <param name="icon">debug message icon</param>
        private void PopUp(string message, MessageBoxIcon icon)
        {
            MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// Formats debug message
        /// </summary>
        /// <param name="message">debug message</param>
        /// <param name="messageLevel">debug level of the message</param>
        /// <param name="messageType">debug message type</param>
        public void ToFile(string message, DebugLevels messageLevel, DebugMessageType messageType)
        {
            //check if current debug level is as message level
            if (Settings.Default.DebugLevel < (uint)messageLevel)
                return;

            string _messageText = FormatTime() + " - ";

            switch (messageType)
            {
                case DebugMessageType.Info:
                    break;

                case DebugMessageType.Warning:
                    _messageText += Resources.DebugWarning;
                    _messageText += ": ";
                    break;

                case DebugMessageType.Alarm:
                    _messageText += Resources.DebugAlarm;
                    _messageText += ": ";
                    break;

                case DebugMessageType.Critical:
                    _messageText += Resources.DebugCritical;
                    _messageText += ": ";
                    break;
            }
            _messageText += message;
            WriteToFile(_messageText);
        }

        /// <summary>
        /// Formats debug message for pop up and send it to file
        /// </summary>
        /// <param name="message">debug message</param>
        /// <param name="messageLevel">debug level of the message</param>
        /// <param name="messageType">debug message type</param>
        public void ToPopUp(string message, DebugLevels messageLevel, DebugMessageType messageType)
        {
            //check if current debug level is
            if (Settings.Default.DebugLevel < (uint)messageLevel)
                return;

            string _messageText = FormatTime() + " - ";
            MessageBoxIcon _icon = MessageBoxIcon.Information;

            switch (messageType)
            {
                case DebugMessageType.Info:
                    break;

                case DebugMessageType.Warning:
                    _icon = MessageBoxIcon.Information;
                    _messageText += Resources.DebugWarning;
                    _messageText += ": ";
                    break;

                case DebugMessageType.Alarm:
                    _icon = MessageBoxIcon.Warning;
                    _messageText += Resources.DebugAlarm;
                    _messageText += ": ";
                    break;

                case DebugMessageType.Critical:
                    _icon = MessageBoxIcon.Error;
                    _messageText += Resources.DebugCritical;
                    _messageText += ": ";
                    break;
            }
            _messageText += message;
            WriteToFile(_messageText);
            PopUp(message, _icon);
        }

        /// <summary>
        /// Prints current debug level to file
        /// </summary>
        /// <returns>current debug level</returns>
        public uint CurrentDebugLevel()
        {
            string _currentDebugLevel = ((DebugLevels)Settings.Default.DebugLevel).ToString();

            ToFile($"{Resources.CurrentDebugLevel}: {_currentDebugLevel}", DebugLevels.None, DebugMessageType.Info);
            return Settings.Default.DebugLevel;
        }

        /// <summary>
        /// Set new debug level
        /// </summary>
        /// <param name="newDebugLevel">new debugging level</param>
        public void SetDebugLevel(uint newDebugLevel)
        {
            string _currentDebugLevel = ((DebugLevels)Settings.Default.DebugLevel).ToString();
            string _newDebugLevel = ((DebugLevels)newDebugLevel).ToString();

            ToPopUp($"{Resources.DebugLevel} {_currentDebugLevel} -> {_newDebugLevel}", DebugLevels.High, DebugMessageType.Info);
            Settings.Default.DebugLevel = newDebugLevel;
            Settings.Default.Save();
        }
    }
}