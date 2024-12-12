namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class FetchPageAsyncEventArgs : FetchPageEventArgsBase
    {
        internal FetchPageAsyncEventArgs(CancellationToken cancellationToken, int skip, object skipToken, int take, SortDefinition[] sortOrder, CriteriaOperator filter) : base(cancellationToken, skip, skipToken, take, sortOrder, filter)
        {
        }

        public Task<FetchRowsResult> Result { get; set; }
    }
}

