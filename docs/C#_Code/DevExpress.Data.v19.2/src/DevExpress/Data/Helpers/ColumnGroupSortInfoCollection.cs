namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Obsolete]
    public class ColumnGroupSortInfoCollection : CollectionBase
    {
        public virtual void Add(ColumnGroupSortInfo sInfo);
        public virtual bool IsEquals(DataColumnSortInfoCollection sortInfo);
        public virtual void Remove(ColumnGroupSortInfo sInfo);

        public ColumnGroupSortInfo this[int index] { get; }

        public int GroupCount { get; }
    }
}

