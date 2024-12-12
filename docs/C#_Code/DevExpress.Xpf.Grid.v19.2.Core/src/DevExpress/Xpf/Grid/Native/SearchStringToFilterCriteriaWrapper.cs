namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SearchStringToFilterCriteriaWrapper
    {
        public SearchStringToFilterCriteriaWrapper(CriteriaOperator filter, List<FieldAndHighlightingString> highlighting, bool applyToColumnsFilter)
        {
            this.Filter = filter;
            this.Highlighting = highlighting;
            this.ApplyToColumnsFilter = applyToColumnsFilter;
        }

        public CriteriaOperator Filter { get; private set; }

        public List<FieldAndHighlightingString> Highlighting { get; private set; }

        public bool ApplyToColumnsFilter { get; private set; }
    }
}

