namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class SubstituteDisplayFilterEventArgs : EventArgs
    {
        public SubstituteDisplayFilterEventArgs(CriteriaOperator displayFilter, string displayText)
        {
            this.DisplayText = displayText;
            this.DisplayFilter = displayFilter;
        }

        public string DisplayText { get; private set; }

        public bool Handled { get; set; }

        public CriteriaOperator DisplayFilter { get; set; }
    }
}

