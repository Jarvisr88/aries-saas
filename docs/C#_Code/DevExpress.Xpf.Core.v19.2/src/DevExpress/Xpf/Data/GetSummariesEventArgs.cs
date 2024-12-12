namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class GetSummariesEventArgs : GetSummariesEventArgsBase
    {
        internal GetSummariesEventArgs(CancellationToken cancellationToken, object source, SummaryDefinition[] summaries, CriteriaOperator filter) : base(cancellationToken, summaries, filter)
        {
            this.Source = source;
        }

        public object[] Result { get; set; }

        public object Source { get; private set; }
    }
}

