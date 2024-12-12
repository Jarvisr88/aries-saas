namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SearchStringToFilterCriteriaEventArgs : EventArgs
    {
        public SearchStringToFilterCriteriaEventArgs(DataControlBase dataControl, string searchString, CriteriaOperator filter, List<FieldAndHighlightingString> highlighting)
        {
            this.Source = dataControl;
            this.SearchString = searchString;
            this.Filter = filter;
            this.Highlighting = highlighting;
        }

        public DataControlBase Source { get; private set; }

        public bool ApplyToColumnsFilter { get; set; }

        public string SearchString { get; private set; }

        public CriteriaOperator Filter { get; set; }

        public List<FieldAndHighlightingString> Highlighting { get; set; }
    }
}

