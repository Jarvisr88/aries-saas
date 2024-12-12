namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public class SummarySortInfoCollection : NotificationCollectionBase
    {
        public SummarySortInfoCollection(CollectionChangeEventHandler collectionChanged);
        public SummarySortInfo Add(SummaryItem summaryItem, int groupLevel, ColumnSortOrder sortOrder);
        public void AddRange(SummarySortInfo[] sortInfos);
        internal bool CheckSummaryCollection(SummaryItemCollection summaryCollection);
        public void ClearAndAddRange(SummarySortInfo[] sortInfos);
        public SummarySortInfo GetByLevel(int groupLevel);
        public void Remove(SummarySortInfo sortInfo);

        public SummarySortInfo this[int index] { get; }
    }
}

