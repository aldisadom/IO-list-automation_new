using IO_list_automation_new.Properties;
using System;

namespace IO_list_automation_new.Helper_functions
{
    public static class TextHelper
    {
        /// <summary>
        /// Get name from column keyword and from DB choices
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <returns>name of column</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public static string GetColumnOrChoicesName(string keyword)
        {
            string returnName = GetChoicesName(keyword, true) ?? GetColumnName(keyword, true);

            if (returnName != null)
                return returnName;

            const string debugText = "GeneralColumn.GetColumnOrChoicesName";
            Debug debug = new Debug();
            debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + keyword, DebugLevels.None, DebugMessageType.Critical);
            throw new InvalidProgramException(debugText + "." + keyword + " is not created for this element");
        }

        /// <summary>
        /// Get name from column keyword
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <param name="suppressError">suppress error if not found</param>
        /// <returns>name of column</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public static string GetColumnName(string keyword, bool suppressError)
        {
            switch (keyword)
            {
                case KeywordColumn.ID:
                    return ResourcesColumns.ID;

                case KeywordColumn.CPU:
                    return ResourcesColumns.CPU;

                case KeywordColumn.KKS:
                    return ResourcesColumns.KKS;

                case KeywordColumn.RangeMin:
                    return ResourcesColumns.RangeMin;

                case KeywordColumn.RangeMax:
                    return ResourcesColumns.RangeMax;

                case KeywordColumn.Units:
                    return ResourcesColumns.Units;

                case KeywordColumn.FalseText:
                    return ResourcesColumns.FalseText;

                case KeywordColumn.TrueText:
                    return ResourcesColumns.TrueText;

                case KeywordColumn.Revision:
                    return ResourcesColumns.Revision;

                case KeywordColumn.Cable:
                    return ResourcesColumns.Cable;

                case KeywordColumn.Cabinet:
                    return ResourcesColumns.Cabinet;

                case KeywordColumn.ModuleName:
                    return ResourcesColumns.ModuleName;

                case KeywordColumn.Pin:
                    return ResourcesColumns.Pin;

                case KeywordColumn.Channel:
                    return ResourcesColumns.Channel;

                case KeywordColumn.IOText:
                    return ResourcesColumns.IOText;

                case KeywordColumn.Page:
                    return ResourcesColumns.Page;

                case KeywordColumn.Changed:
                    return ResourcesColumns.Changed;

                case KeywordColumn.Operative:
                    return ResourcesColumns.Operative;

                case KeywordColumn.KKSPlant:
                    return ResourcesColumns.KKSPlant;

                case KeywordColumn.KKSLocation:
                    return ResourcesColumns.KKSLocation;

                case KeywordColumn.KKSDevice:
                    return ResourcesColumns.KKSDevice;

                case KeywordColumn.KKSFunction:
                    return ResourcesColumns.KKSFunction;

                case KeywordColumn.Used:
                    return ResourcesColumns.Used;

                case KeywordColumn.ObjectType:
                    return ResourcesColumns.ObjectType;

                case KeywordColumn.ObjectName:
                    return ResourcesColumns.ObjectName;

                case KeywordColumn.ObjectSpecifics:
                    return ResourcesColumns.ObjectSpecifics;

                case KeywordColumn.FunctionText:
                    return ResourcesColumns.FunctionText;

                case KeywordColumn.Function:
                    return ResourcesColumns.Function;

                case KeywordColumn.Terminal:
                    return ResourcesColumns.Terminal;

                case KeywordColumn.DeviceTypeText:
                    return ResourcesColumns.DeviceTypeText;

                case KeywordColumn.FunctionText1:
                    return ResourcesColumns.FunctionText1;

                case KeywordColumn.Function1:
                    return ResourcesColumns.Function1;

                case KeywordColumn.FunctionText1o2:
                    return ResourcesColumns.FunctionText1o2;

                case KeywordColumn.FunctionText2o2:
                    return ResourcesColumns.FunctionText2o2;

                case KeywordColumn.Function2:
                    return ResourcesColumns.Function2;

                case KeywordColumn.Tag:
                    return ResourcesColumns.Tag;

                case KeywordColumn.ModuleType:
                    return ResourcesColumns.ModuleType;

                default:
                    if (suppressError)
                        return string.Empty;

                    const string debugText = "GeneralColumn.GetColumnName";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + keyword, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + keyword + " is not created for this element");
            }
        }

        /// <summary>
        /// Get name from choices keyword
        /// </summary>
        /// <param name="keyword">choices keyword</param>
        /// <param name="suppressError">suppress error if not found</param>
        /// <returns>name of choice</returns>
        /// <exception cref="InvalidProgramException"></exception>
        public static string GetChoicesName(string keyword, bool suppressError)
        {
            switch (keyword)
            {
                case KeywordDBChoices.None:
                    return string.Empty;

                case KeywordDBChoices.Tab:
                    return ResourcesChoices.Tab;

                case KeywordDBChoices.If:
                    return ResourcesChoices.If;

                case KeywordDBChoices.Text:
                    return ResourcesChoices.Text;

                case KeywordDBChoices.Data:
                    return ResourcesChoices.Data_;

                case KeywordDBChoices.Object:
                    return ResourcesChoices.Object;

                case KeywordDBChoices.Modules:
                    return ResourcesChoices.Modules;

                case KeywordDBChoices.BaseAddress:
                    return ResourcesChoices.BaseAddress;

                case KeywordDBChoices.Address:
                    return ResourcesChoices.Address;

                case KeywordDBChoices.AddressArea:
                    return ResourcesChoices.AddressArea;

                case KeywordDBChoices.GetBaseAddress:
                    return ResourcesChoices.GetBaseAddress;

                case KeywordDBChoices.Insert:
                    return ResourcesChoices.Insert;

                case KeywordDBChoices.MultiLine:
                    return ResourcesChoices.MultiLine;

                case KeywordDBChoices.IsEmpty:
                    return ResourcesChoices.IsEmpty;

                case KeywordDBChoices.IsNotEmpty:
                    return ResourcesChoices.IsNotEmpty;

                case KeywordDBChoices.Equal:
                    return ResourcesChoices.Equal;

                case KeywordDBChoices.nEqual:
                    return ResourcesChoices.nEqual;

                case KeywordDBChoices.GreaterEqual:
                    return ResourcesChoices.GreaterEqual;

                case KeywordDBChoices.Greater:
                    return ResourcesChoices.Greater;

                case KeywordDBChoices.LessEqual:
                    return ResourcesChoices.LessEqual;

                case KeywordDBChoices.Less:
                    return ResourcesChoices.Less;

                default:
                    if (suppressError)
                        return null;

                    const string debugText = "GeneralColumn.GetChoicesName";
                    Debug debug = new Debug();
                    debug.ToFile(debugText + " " + Resources.ParameterNotFound + ":" + keyword, DebugLevels.None, DebugMessageType.Critical);
                    throw new InvalidProgramException(debugText + "." + keyword + " is not created for this element");
            }
        }
    }

}
