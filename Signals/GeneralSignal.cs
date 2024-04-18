using IO_list_automation_new.Properties;
using System;

namespace IO_list_automation_new.General
{
    public abstract class GeneralSignal
    {
        /// <summary>
        /// Set value of property
        /// </summary>
        /// <param name="value">value to sety</param>
        /// <param name="parameterName">parameter name</param>
        public void SetValueFromString(string value, string parameterName)
        {
            if (IsDefined(parameterName))
                this.GetType().GetProperty(parameterName).SetValue(this, value);
        }

        /// <summary>
        /// Property is defined
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <returns>value of parameter</returns>
        public bool IsDefined(string parameterName)
        {
            bool result = this.GetType().GetProperty(parameterName) != null;
            return result;
        }

        /// <summary>
        /// Parsing data signal element to string for grid
        /// </summary>
        /// <param name="parameterName">parameter to be read</param>
        /// <param name="suppressError">suppress alarm message, used only for transferring from one type to another type data classes</param>
        /// <returns>value of parameter</returns>
        public string GetValueString(string parameterName, bool suppressError)
        {
            if (IsDefined(parameterName))
                return this.GetType().GetProperty(parameterName).GetValue(this, null).ToString();
            else if (suppressError)
                return string.Empty;
            else
            {
                string debugText = this.GetType() + ".GetValueString ";
                Debug debug = new Debug();
                debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + parameterName, DebugLevels.None, DebugMessageType.Critical);
                throw new InvalidProgramException(debugText + "." + parameterName + " is not created for this element");
            }
        }

        /// <summary>
        /// Checks if signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public abstract bool ValidateSignal();
    }
}
