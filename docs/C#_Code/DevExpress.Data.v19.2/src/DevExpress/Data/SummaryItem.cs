namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class SummaryItem : SummaryItemBase
    {
        private SummaryItemCollection collection;
        private object summaryValue;
        protected bool? ignoreNullValuesCore;
        private SummaryItemTypeEx summaryTypeEx;
        private decimal summaryArgument;
        private bool exists;

        public SummaryItem();
        public SummaryItem(DataColumnInfo columnInfo, SummaryItemType summaryType);
        public SummaryItem(DataColumnInfo columnInfo, SummaryItemType summaryType, object tag, bool? ignoreNullValues = new bool?());
        public SummaryItem(DataColumnInfo columnInfo, SummaryItemTypeEx summaryType, decimal argument, bool? ignoreNullValues = new bool?());
        public bool GetAllowExternalCalculate(bool allowUnbound);
        protected internal bool IgnoreNullValues(bool defaultValue);
        protected override void OnSummaryChanged();

        public bool Exists { get; set; }

        public string FieldName { get; set; }

        private SummaryItemType summaryType { get; set; }

        public decimal SummaryArgument { get; set; }

        public virtual bool IsListBasedSummary { get; }

        protected internal bool IsSummaryArgumentRequired { get; }

        protected internal bool IsPercentArgument { get; }

        public SummaryItemType SummaryType { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public SummaryItemTypeEx SummaryTypeEx { get; set; }

        public object SummaryValue { get; set; }

        public IList SummaryValueListBased { get; }

        public bool AllowCalculate { get; }

        [Obsolete]
        public bool AllowExternalCalculate { get; }

        public bool IsNoneSummary { get; }

        public bool IsCustomSummary { get; }

        protected internal SummaryItemCollection Collection { get; set; }
    }
}

