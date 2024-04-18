using IO_list_automation_new.Helper_functions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace IO_list_automation_new.General
{
    public class ColumnList
    {
        public Dictionary<string, ColumnParameters> Columns { get; set; } = new Dictionary<string, ColumnParameters>();
        public string Name { get; private set; } = string.Empty;

        public ColumnList(string name)
        {
            Name = name;
        }

        private string ColumnSettingsFileLocation()
        {
            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            path += "\\" + Name + ".json";

            return path;
        }

        public void SaveColumnsParameters()
        {
            string data = JsonConvert.SerializeObject(Columns);
            File.WriteAllText(ColumnSettingsFileLocation(), data);
        }

        public void LoadColumnsParameters()
        {
            string path = ColumnSettingsFileLocation();

            if (File.Exists(path))
            {
                string data = File.ReadAllText(ColumnSettingsFileLocation());
                Dictionary<string, ColumnParameters> newColumns = JsonConvert.DeserializeObject<Dictionary<string, ColumnParameters>>(data);

                foreach (var column in newColumns)
                {
                    if (Columns.TryGetValue(column.Key, out ColumnParameters columnParameters))
                    {
                        if (columnParameters.CanHide)
                            columnParameters.Hidden = column.Value.Hidden;

                        columnParameters.NR = column.Value.NR;
                    }
                }

            }
            else
                SaveColumnsParameters();
        }

        /// <summary>
        /// Find column with keyword and return its column number
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <returns>column number</returns>
        public int GetColumnNumberFromKey(string keyword)
        {
            if (Columns.TryGetValue(keyword, out var result))
                return result.NR;
            else
                return -1;
        }

        /// <summary>
        /// Find column with keyword and return its column number
        /// </summary>
        /// <param name="keyword">column keyword</param>
        /// <returns>column number</returns>
        public string GetColumnNameFromKey(string keyword)
        {
            return TextHelper.GetColumnName(keyword, false);
        }
    }
}
