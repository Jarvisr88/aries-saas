namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Windows;

    public interface ISummaryItemOwner : IEnumerable<SummaryItemBase>, IEnumerable, INotifyCollectionChanged, IList, ICollection, ISupportGetCachedIndex<SummaryItemBase>
    {
        void Add(SummaryItemBase item);
        void BeginUpdate();
        void EndUpdate();
        void OnSummaryChanged(SummaryItemBase summaryItem, DependencyPropertyChangedEventArgs e);
        void Remove(SummaryItemBase item);

        SummaryItemBase this[int index] { get; }
    }
}

