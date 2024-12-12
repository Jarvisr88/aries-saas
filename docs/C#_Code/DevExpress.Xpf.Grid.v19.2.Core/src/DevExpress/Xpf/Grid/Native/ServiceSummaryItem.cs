namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    public class ServiceSummaryItem : SummaryItemBase
    {
        internal readonly bool ShouldUpdateRows;

        public ServiceSummaryItem() : this(true)
        {
        }

        internal ServiceSummaryItem(bool shouldUpdateRows)
        {
            this.ShouldUpdateRows = shouldUpdateRows;
        }

        internal override bool? IgnoreNullValues =>
            true;

        public DevExpress.Xpf.Grid.Native.CustomServiceSummaryItemType? CustomServiceSummaryItemType { get; set; }
    }
}

