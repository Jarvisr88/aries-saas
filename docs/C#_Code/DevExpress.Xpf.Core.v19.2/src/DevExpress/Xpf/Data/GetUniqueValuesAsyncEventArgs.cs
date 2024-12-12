namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUniqueValuesAsyncEventArgs : GetUniqueValuesEventArgsBase
    {
        internal GetUniqueValuesAsyncEventArgs(CancellationToken cancellationToken, string propertyName, CriteriaOperator filter) : base(cancellationToken, propertyName, filter)
        {
        }

        public Task<object[]> Result { get; set; }

        public Task<ValueAndCount[]> ResultWithCounts { get; set; }
    }
}

