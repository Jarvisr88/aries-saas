namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class FetchRowsAsyncEventArgs : FetchRowsEventArgsBase
    {
        internal FetchRowsAsyncEventArgs(CancellationToken cancellationToken, int skip, object skipToken, SortDefinition[] sortOrder, CriteriaOperator filter) : base(cancellationToken, skip, skipToken, sortOrder, filter)
        {
        }

        public Task<FetchRowsResult> Result { get; set; }
    }
}

