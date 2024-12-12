namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class GetSummariesEventArgsBase : BaseAsyncArgs
    {
        internal GetSummariesEventArgsBase(CancellationToken cancellationToken, SummaryDefinition[] summaries, CriteriaOperator filter) : base(cancellationToken)
        {
            this.Summaries = summaries;
            this.Filter = filter;
        }

        public SummaryDefinition[] Summaries { get; private set; }

        public CriteriaOperator Filter { get; private set; }
    }
}

