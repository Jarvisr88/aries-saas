namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetSummariesAsyncEventArgs : GetSummariesEventArgsBase
    {
        internal GetSummariesAsyncEventArgs(CancellationToken cancellationToken, SummaryDefinition[] summaries, CriteriaOperator filter) : base(cancellationToken, summaries, filter)
        {
        }

        public Task<object[]> Result { get; set; }
    }
}

