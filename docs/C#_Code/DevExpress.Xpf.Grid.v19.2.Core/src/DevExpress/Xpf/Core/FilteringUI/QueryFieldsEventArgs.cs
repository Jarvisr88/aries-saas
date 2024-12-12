namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class QueryFieldsEventArgs
    {
        internal QueryFieldsEventArgs(FieldList fields, CriteriaOperator filter)
        {
            this.Fields = fields;
            this.<Filter>k__BackingField = filter;
            this.ShowSearchPanel = true;
        }

        public FieldList Fields { get; set; }

        public bool ShowSearchPanel { get; set; }

        public CriteriaOperator Filter { get; }
    }
}

