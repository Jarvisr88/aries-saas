namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Runtime.InteropServices;

    internal static class MergingUtils
    {
        public static void MergeCells(IXlSheet sheet, BandNodeDescriptor desc, int cellI, int row, bool checkOverlapped = false, bool onlyHorzMerging = false)
        {
            int top = row;
            int bottom = onlyHorzMerging ? row : ((desc.RowCount - 1) + row);
            XlCellRange range = XlCellRange.FromLTRB(cellI, top, (cellI + desc.Leaves) - 1, bottom);
            if (!RangeContainsOneCell(range))
            {
                try
                {
                    sheet.MergedCells.Add(range, checkOverlapped);
                }
                catch
                {
                }
            }
        }

        public static void MergeCells(IXlSheet sheet, BandNodeDescriptor desc, int cellI, int top, int bottom, bool checkOverlapped = false)
        {
            XlCellRange range = XlCellRange.FromLTRB(cellI, top, (cellI + desc.Leaves) - 1, bottom);
            if (!RangeContainsOneCell(range))
            {
                try
                {
                    sheet.MergedCells.Add(range, checkOverlapped);
                }
                catch
                {
                }
            }
        }

        private static bool RangeContainsOneCell(XlCellRange range) => 
            (range.ColumnCount == 1) && (range.RowCount == 1);
    }
}

