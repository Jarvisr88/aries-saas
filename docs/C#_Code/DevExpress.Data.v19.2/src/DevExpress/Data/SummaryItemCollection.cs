namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class SummaryItemCollection : ColumnInfoNotificationCollection<SummaryItem>
    {
        public SummaryItemCollection(DataControllerBase controller, CollectionChangeEventHandler collectionChanged);
        public virtual SummaryItem Add(SummaryItem item);
        public void AddRange(SummaryItem[] summaryItems);
        public void ClearAndAddRange(SummaryItem[] summaryItems);
        public bool Contains(SummaryItem item);
        public static int GetActiveCount(IList list);
        public static IList<SummaryItem> GetOrderedList(IList items);
        public SummaryItem GetSummaryItemByKey(object key);
        public SummaryItem GetSummaryItemByTag(object tag);
        public List<SummaryItem> GetSummaryItemByTagType(Type tagType);
        public List<ListSourceSummaryItem> GetSummaryItems();
        public List<ListSourceSummaryItem> GetSummaryItems(bool allowUnbound);
        protected override bool IsColumnInfoUsed(int index, IList<DataColumnInfo> unusedColumns);
        protected override void OnClear();
        protected override void OnInsertComplete(int index, object value);
        protected override void OnRemoveComplete(int index, object value);
        protected internal virtual void OnSummaryItemChanged(SummaryItem item);
        protected internal virtual void RequestSummaryValue();
        protected internal virtual void RequestSummaryValue(SummaryItem item);

        public int ActiveCount { get; }
    }
}

