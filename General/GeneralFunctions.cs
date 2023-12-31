﻿using ExcelDataReader;
using IO_list_automation_new.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace IO_list_automation_new.General
{
    internal static class GeneralFunctions
    {
        /// <summary>
        /// Copy one list to another
        /// </summary>
        /// <param name="input">input list</param>
        /// <returns>new list</returns>
        public static List<string> ListCopy(List<string> input)
        {
            List<string> _list = new List<string>();

            for (int i = 0; i < input.Count; i++)
                _list.Add(input[i]);

            return _list;
        }

        /// <summary>
        /// add additional zeros before number for better sorting
        /// </summary>
        /// <param name="_input">value</param>
        /// <returns>formatted string</returns>
        public static string AddZeroes(int _input)
        {
            if (_input < 10)
                return "000" + _input.ToString();
            else if (_input < 100)
                return "00" + _input.ToString();
            else if (_input < 1000)
                return "0" + _input.ToString();
            else
                return _input.ToString();
        }

        /// <summary>
        /// Paste data to grid
        /// </summary>
        /// <param name="_grid"></param>
        static public void Paste(DataGridView _grid)
        {
            Debug _debug = new Debug();

            IDataObject _dataInClipboard = Clipboard.GetDataObject();

            string _stringInClipboard = _dataInClipboard.GetData(DataFormats.UnicodeText).ToString();

            // no row data
            if (_stringInClipboard == null)
            {
                _debug.ToPopUp(Resources.NoPasteData, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            int _selRowMin = _grid.SelectedCells[0].RowIndex;
            int _selColMin = _grid.SelectedCells[0].ColumnIndex;

            int _selRow;
            int _selCol;

            //get selected min and max cells
            for (int i = 0; i < _grid.SelectedCells.Count; i++)
            {
                _selRow = _grid.SelectedCells[i].RowIndex;
                _selCol = _grid.SelectedCells[i].ColumnIndex;

                if (_selRow < _selRowMin)
                    _selRowMin = _selRow;

                if (_selCol < _selColMin)
                    _selColMin = _selCol;
            }

            int _enableRowCount = _grid.RowCount - _selRowMin;
            int _enableColumnCount = _grid.ColumnCount - _selColMin;

            _stringInClipboard = _stringInClipboard.Replace("\r", "");
            string[] _clipboardRows = _stringInClipboard.Split('\n');

            int _rowsInBoard = _clipboardRows.Length;

            string[] _clipboardCells = _clipboardRows[0].Split('\t');
            int _colsInBoard = _clipboardCells.Length;

            if (_rowsInBoard < 1)
            {
                _debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Row, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }
            else if (_colsInBoard < 1)
            {
                _debug.ToPopUp(Resources.NoPasteData + ": " + Resources.Column, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            _debug.ToFile(Resources.PasteData + ": " + _grid.Name +
                            " " + Resources.Row + "(" + _rowsInBoard + ")" +
                            " " + Resources.Column + "(" + _colsInBoard + ")" +
                            " " + Resources.PasteAt + "(" + _selRowMin + ":" + _selColMin + ")"
                            , DebugLevels.Development, DebugMessageType.Info);

            if ((_enableRowCount < _rowsInBoard) || (_enableColumnCount < _colsInBoard))
            {
                _debug.ToPopUp(Resources.ToMuchDataPaste, DebugLevels.None, DebugMessageType.Alarm);
                return;
            }

            int _row;
            //when only 1 row is copied, paste it in all selected rows
            if (_rowsInBoard == 1)
            {
                for (int i = 0; i < _grid.SelectedCells.Count; i++)
                {
                    _row = _grid.SelectedCells[i].RowIndex;
                    for (int _col = 0; _col < _clipboardCells.Length; _col++)
                        _grid.Rows[_row].Cells[_selColMin + _col].Value = _clipboardCells[_col];
                }
            }
            //else paste to required amount
            else
            {
                for (_row = 0; _row < _rowsInBoard; _row++)
                {
                    _clipboardCells = _clipboardRows[_row].Split('\t');
                    for (int _col = 0; _col < _clipboardCells.Length; _col++)
                        _grid.Rows[_selRowMin + _row].Cells[_selColMin + _col].Value = _clipboardCells[_col];
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

            string _value = (string)dataTable.Rows[row][column];

            if (string.IsNullOrEmpty(_value))
                return string.Empty;

            return _value;
        }
    }
}