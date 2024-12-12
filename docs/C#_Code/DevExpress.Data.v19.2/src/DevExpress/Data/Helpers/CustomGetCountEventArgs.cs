namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomGetCountEventArgs : EventArgs
    {
        private readonly CriteriaOperator where;

        public CustomGetCountEventArgs(CriteriaOperator where);

        public bool Handled { get; set; }

        public int Result { get; set; }

        public CriteriaOperator Where { get; }
    }
}

