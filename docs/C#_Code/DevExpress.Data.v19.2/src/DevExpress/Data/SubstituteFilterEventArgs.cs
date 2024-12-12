namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class SubstituteFilterEventArgs : EventArgs
    {
        public SubstituteFilterEventArgs();
        public SubstituteFilterEventArgs(CriteriaOperator filter);

        public CriteriaOperator Filter { get; set; }
    }
}

