namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class VirtualServerModeGetUniqueValuesEventArgs : EventArgs
    {
        private System.Threading.CancellationToken _CancellationToken;
        private CriteriaOperator _ValuesExpression;
        private int _MaxCount;
        private CriteriaOperator _FilterExpression;

        public VirtualServerModeGetUniqueValuesEventArgs(System.Threading.CancellationToken cancellationToken, CriteriaOperator valuesExpression, CriteriaOperator filterExpression, int maxCount);

        public System.Threading.CancellationToken CancellationToken { get; }

        public CriteriaOperator ValuesExpression { get; }

        public int MaxCount { get; }

        public CriteriaOperator FilterExpression { get; }

        public Task<object[]> UniqueValuesTask { get; set; }

        public string ValuesPropertyName { get; }
    }
}

