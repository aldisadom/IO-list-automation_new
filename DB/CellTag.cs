using IO_list_automation_new.Forms;
using IO_list_automation_new.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_list_automation_new.DB
{
    internal class CellTag
    {
        public int X { get; }
        public int Y { get; }

        public string Keyword { get; }

        public string UniqueName { get; }

        public ComboBoxType ComboType { get; }

        public RestrainLevel Restrain { get; }

        public CellTag(string keyword, ComboBoxType comboBoxType, RestrainLevel restrain, int x, int y)
        {
            X = x;
            Y = y;
            Keyword = keyword;
            ComboType = comboBoxType;
            Restrain = restrain;
            UniqueName = "X" + X.ToString() + "Y" + Y.ToString() +"@"+ keyword;
        }
    }
}
