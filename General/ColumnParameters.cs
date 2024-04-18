namespace IO_list_automation_new
{
    public class ColumnParameters
    {
        //column number for sorting
        public int NR { get; set; } = 0;
        public bool CanHide { get; private set; } = false;
        public bool Hidden { get; set; } = false;

        public ColumnParameters(int columnNumber, bool canHide, bool hidden)
        {
            NR = columnNumber;
            CanHide = canHide;
            Hidden = hidden;
        }
    }
}