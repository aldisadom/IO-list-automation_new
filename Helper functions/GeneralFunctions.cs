using IO_list_automation_new.Properties;
using System.Data;
using System.Windows.Forms;

namespace IO_list_automation_new.General
{
    internal static class GeneralFunctions
    {
        /// <summary>
        /// add additional zeros before number for better sorting
        /// </summary>
        /// <param name="input">value</param>
        /// <returns>formatted string</returns>
        public static string AddZeroes(int input)
        {
            if (input < 10)
                return "000" + input.ToString();
            else if (input < 100)
                return "00" + input.ToString();
            else if (input < 1000)
                return "0" + input.ToString();
            else
                return input.ToString();
        }

        /// <summary>
        /// Paste data to grid
        /// </summary>
        /// <param name="grid"></param>
        public static void Paste(DataGridView grid)
        {
            Debug debug = new Debug();

            IDataObject dataInClipboard = Clipboard.GetDataObject();

            string stringInClipboard = dataInClipboard.GetData(DataFormats.UnicodeText).ToString();

            // no row data
            if (stringInClipboard == null)
            {
                debug.ToPopUp(Resources.NoPasteData, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            int selRowMin = grid.SelectedCells[0].RowIndex;
            int selColMin = grid.SelectedCells[0].ColumnIndex;

            int selRow;
            int selCol;

            //get selected min and max cells
            for (int i = 0; i < grid.SelectedCells.Count; i++)
            {
                selRow = grid.SelectedCells[i].RowIndex;
                selCol = grid.SelectedCells[i].ColumnIndex;

                if (selRow < selRowMin)
                    selRowMin = selRow;

                if (selCol < selColMin)
                    selColMin = selCol;
            }

            int enableRowCount = grid.RowCount - selRowMin;
            int enableColumnCount = grid.ColumnCount - selColMin;

            stringInClipboard = stringInClipboard.Replace("\r", "");
            string[] clipboardRows = stringInClipboard.Split('\n');

            int rowsInBoard = clipboardRows.Length;

            string[] clipboardCells = clipboardRows[0].Split('\t');
            int colsInBoard = clipboardCells.Length;

            if (rowsInBoard < 1)
            {
                debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Row, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }
            else if (colsInBoard < 1)
            {
                debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Column, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            debug.ToFile(Resources.PasteData + ": " + grid.Name +
                            " " + Resources.Row + "(" + rowsInBoard + ")" +
                            " " + Resources.Column + "(" + colsInBoard + ")" +
                            " " + Resources.PasteAt + "(" + selRowMin + ":" + selColMin + ")"
                            , DebugLevels.Development, DebugMessageType.Info);

            if ((enableRowCount < rowsInBoard) || (enableColumnCount < colsInBoard))
            {
                debug.ToPopUp(Resources.ToMuchDataPaste, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            int row;
            //when only 1 row is copied, paste it in all selected rows
            if (rowsInBoard == 1)
            {
                for (int i = 0; i < grid.SelectedCells.Count; i++)
                {
                    row = grid.SelectedCells[i].RowIndex;
                    for (int col = 0; col < clipboardCells.Length; col++)
                        grid.Rows[row].Cells[selColMin + col].Value = clipboardCells[col];
                }
            }
            //else paste to required amount
            else
            {
                for (row = 0; row < rowsInBoard; row++)
                {
                    clipboardCells = clipboardRows[row].Split('\t');
                    for (int col = 0; col < clipboardCells.Length; col++)
                        grid.Rows[selRowMin + row].Cells[selColMin + col].Value = clipboardCells[col];
                }
            }
        }

        public static bool ValidDataTable(DataTable dataTable)
        {
            if (dataTable == null)
                return false;

            if (dataTable.Rows.Count == 0)
                return false;

            return true;
        }

        public static string GetDataTableValue(DataTable dataTable, int row, int column)
        {
            if (!ValidDataTable(dataTable))
                return string.Empty;

            if (dataTable.Rows[row][column] is System.DBNull)
                return string.Empty;

            string value = (string)dataTable.Rows[row][column];

            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value;
        }
    }
}