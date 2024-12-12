namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PageBreakList : Collection<PageBreakInfo>, IListWrapper<PageBreakInfo>, IEnumerable<PageBreakInfo>, IEnumerable
    {
        int IListWrapper<PageBreakInfo>.IndexOf(PageBreakInfo pageBreak);
        void IListWrapper<PageBreakInfo>.Insert(PageBreakInfo pageBreak, int index);
        protected override void InsertItem(int index, PageBreakInfo pageBreak);

        internal ValueInfo LastValue { get; }

        public float MaxPageBreak { get; }
    }
}

