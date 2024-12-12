namespace DevExpress.Data.TreeList
{
    using System;
    using System.ComponentModel;

    public class TreeListSummaryItemCollection : TreeListNotificationCollectionBase<TreeListSummaryItem>
    {
        public TreeListSummaryItemCollection();
        public TreeListSummaryItemCollection(CollectionChangeEventHandler collectionChanged);
        public TreeListSummaryItem Add(TreeListSummaryItem item);
        public TreeListSummaryItem GetSummaryItemByTag(object tag);
    }
}

