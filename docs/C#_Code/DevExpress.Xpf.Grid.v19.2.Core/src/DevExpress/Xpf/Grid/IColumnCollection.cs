namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;

    public interface IColumnCollection : IList, ICollection, IEnumerable, INotifyCollectionChanged, ILockable, ISupportGetCachedIndex<ColumnBase>
    {
        ColumnBase GetColumnByName(string name);
        void OnColumnsChanged();

        DataControlBase Owner { get; }

        ColumnBase this[string fieldName] { get; }

        ColumnBase this[int index] { get; }
    }
}

