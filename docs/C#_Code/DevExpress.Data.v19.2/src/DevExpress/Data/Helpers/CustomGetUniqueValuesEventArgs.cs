namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomGetUniqueValuesEventArgs : EventArgs
    {
        private CriteriaOperator expression;
        private int maxCount;
        private CriteriaOperator where;

        public CustomGetUniqueValuesEventArgs(CriteriaOperator expression, int maxCount, CriteriaOperator where);

        public bool Handled { get; set; }

        public object[] Result { get; set; }

        public CriteriaOperator Expression { get; }

        public int MaxCount { get; }

        public CriteriaOperator Where { get; }
    }
}

