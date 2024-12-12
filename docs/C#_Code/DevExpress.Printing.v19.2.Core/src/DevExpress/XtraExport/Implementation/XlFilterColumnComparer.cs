namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal class XlFilterColumnComparer : IComparer<XlFilterColumn>
    {
        private static XlFilterColumnComparer instance = new XlFilterColumnComparer();

        public int Compare(XlFilterColumn x, XlFilterColumn y)
        {
            if (!ReferenceEquals(x, y))
            {
                if (x.ColumnId < y.ColumnId)
                {
                    return -1;
                }
                if (x.ColumnId > y.ColumnId)
                {
                    return 1;
                }
            }
            return 0;
        }

        public static XlFilterColumnComparer Instance =>
            instance;
    }
}

