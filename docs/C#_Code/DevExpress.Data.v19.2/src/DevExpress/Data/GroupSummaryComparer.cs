namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class GroupSummaryComparer : IComparer<GroupRowInfo>, IComparer
    {
        private DataController controller;
        private SummarySortInfo sortInfo;

        public GroupSummaryComparer(DataController controller, SummarySortInfo sortInfo);
        public int Compare(GroupRowInfo x, GroupRowInfo y);
        public int Compare(object x, object y);
        public int Compare(GroupRowInfo groupRow1, GroupRowInfo groupRow2, SummaryItem item);
        private int CompareCore(GroupRowInfo groupRow1, GroupRowInfo groupRow2);

        protected DataController Controller { get; }

        protected DevExpress.Data.ValueComparer ValueComparer { get; }
    }
}

