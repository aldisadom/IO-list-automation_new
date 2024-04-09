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
            string formattedString = _number.ToString();
            //check size of number if not enough numbers add 0 in front
            for (int i = formattedString.Length; i < decimals; i++)
                formattedString = "0" + formattedString;
            return formattedString;
        }

        /// <summary>
        /// Get current time
        /// </summary>
        /// <returns>Formatted current time as string</returns>
        private string FormatTime()
        {
            System.DateTime currentTime = DateTime.Now;
            string formattedTime = NumberToString(currentTime.Year, 4) + "-" + NumberToString(currentTime.Month, 2) + "-" + NumberToString(currentTime.Day, 2) + " " +
                                    NumberToString(currentTime.Hour, 2) + ":" + NumberToString(currentTime.Minute, 2) + ":" + NumberToString(currentTime.Second, 2) + "." +
                                    NumberToString(currentTime.Millisecond, 3);
            return formattedTime;
        }

        /// <summary>
        /// Clear debug file
        /// </summary>
        public void ClearDebug()
        {
            File.Create("debug.txt").Dispose();
        }

        /// <summary>
        /// Write debug message to file
        /// </summary>
        /// <param name="message">debug message</param>
        private void WriteToFile(string message)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "debug.txt"), true))
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

            string messageText = FormatTime() + " - ";

            switch (messageType)
            {
                case DebugMessageType.Info:
                    break;

                case DebugMessageType.Warning:
                    messageText += Resources.DebugWarning;
                    messageText += ": ";
                    break;

                case DebugMessageType.Alarm:
                    messageText += Resources.DebugAlarm;
                    messageText += ": ";
                    break;

                case DebugMessageType.Critical:
                    messageText += Resources.DebugCritical;
                    messageText += ": ";
                    break;
            }
            messageText += message;
            WriteToFile(messageText);
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

            string messageText = FormatTime() + " - ";
            MessageBoxIcon icon = MessageBoxIcon.Information;

            switch (messageType)
            {
                case DebugMessageType.Info:
                    break;

                case DebugMessageType.Warning:
                    icon = MessageBoxIcon.Information;
                    messageText += Resources.DebugWarning;
                    messageText += ": ";
                    break;

                case DebugMessageType.Alarm:
                    icon = MessageBoxIcon.Warning;
                    messageText += Resources.DebugAlarm;
                    messageText += ": ";
                    break;

                case DebugMessageType.Critical:
                    icon = MessageBoxIcon.Error;
                    messageText += Resources.DebugCritical;
                    messageText += ": ";
                    break;
            }
            messageText += message;
            WriteToFile(messageText);
            PopUp(message, icon);
        }

        /// <summary>
        /// Prints current debug level to file
        /// </summary>
        /// <returns>current debug level</returns>
        public uint CurrentDebugLevel()
        {
            string currentDebugLevel = ((DebugLevels)Settings.Default.DebugLevel).ToString();

            ToFile($"{Resources.CurrentDebugLevel}: {currentDebugLevel}", DebugLevels.None, DebugMessageType.Info);
            return Settings.Default.DebugLevel;
        }

        /// <summary>
        /// Set new debug level
        /// </summary>
        /// <param name="newDebugLevel">new debugging level</param>
        public void SetDebugLevel(DebugLevels newDebugLevel)
        {
            string currentDebugLevel = ((DebugLevels)Settings.Default.DebugLevel).ToString();

            ToPopUp($"{Resources.DebugLevel} {currentDebugLevel} -> {nameof(newDebugLevel)}", DebugLevels.High, DebugMessageType.Info);
            Settings.Default.DebugLevel = (uint)newDebugLevel;
            Settings.Default.Save();
        }
    }
}