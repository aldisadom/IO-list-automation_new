using IO_list_automation_new.Properties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IO_list_automation_new
{
    internal class PropertiesChange
    {

        /// <summary>
        /// Change height of program
        /// </summary>
        /// <param name="newHeight">new height to set</param>
        public void Height(uint newHeight)
        {
            Debug _debug = new Debug();
            _debug.ToFile($"{Resources.ChangeHeight} {Settings.Default.AplicationHeight} -> {newHeight}", DebugLevels.High, DebugMessageType.Info);
            Settings.Default.AplicationHeight = newHeight;
            Settings.Default.Save();
        }

        /// <summary>
        /// Change width of program
        /// </summary>
        /// <param name="newWidth">new width to set</param>
        public void Width(uint newWidth)
        {
            Debug _debug = new Debug();
            _debug.ToFile($"{Resources.ChangeWidth} {Settings.Default.AplicationWidth} -> {newWidth}", DebugLevels.High, DebugMessageType.Info);
            Settings.Default.AplicationWidth = newWidth;
            Settings.Default.Save();
        }
    }
}
