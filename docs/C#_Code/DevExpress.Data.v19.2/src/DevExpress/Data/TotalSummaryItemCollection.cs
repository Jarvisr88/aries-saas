namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class TotalSummaryItemCollection : SummaryItemCollection
    {
        private bool isDirty;

        public TotalSummaryItemCollection(DataControllerBase controller, CollectionChangeEventHandler collectionChanged);
        public int IndexOf(SummaryItem item);
        public bool RemoveItems(ICollection items);
        protected internal override void RequestSummaryValue();
        public void SetDirty();

        public bool IsDirty { get; set; }
    }
}

