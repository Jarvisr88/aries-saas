namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal static class XlFilterColumnsChecker
    {
        public static void Check(IXlFilterColumns columns, XlCellRange range)
        {
            Check(columns, range.ColumnCount);
        }

        public static void Check(IXlFilterColumns columns, int columnCount)
        {
            for (int i = columns.Count - 1; i >= 0; i--)
            {
                if (columns[i].ColumnId >= columnCount)
                {
                    columns.RemoveAt(i);
                }
            }
        }
    }
}

