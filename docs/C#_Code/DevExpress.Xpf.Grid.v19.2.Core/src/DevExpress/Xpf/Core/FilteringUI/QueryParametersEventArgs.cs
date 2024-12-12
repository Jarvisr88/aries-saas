namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class QueryParametersEventArgs
    {
        internal QueryParametersEventArgs(CriteriaOperator filter)
        {
            this.<Filter>k__BackingField = filter;
        }

        public CriteriaOperator Filter { get; }

        public IReadOnlyCollection<string> Parameters { get; set; }
    }
}

