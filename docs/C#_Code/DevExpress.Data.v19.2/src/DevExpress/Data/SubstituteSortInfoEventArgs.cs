namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class SubstituteSortInfoEventArgs : EventArgs
    {
        public DataColumnSortInfo[] SortInfo { get; set; }

        public int GroupCount { get; internal set; }
    }
}

