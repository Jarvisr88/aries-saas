namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class FetchEventArgsBase : BaseAsyncArgs
    {
        internal FetchEventArgsBase(CancellationToken cancellationToken, int skip, object skipToken, SortDefinition[] sortOrder, CriteriaOperator filter) : base(cancellationToken)
        {
            this.Skip = skip;
            this.SkipToken = skipToken;
            this.SortOrder = sortOrder;
            this.Filter = filter;
        }

        public int Skip { get; private set; }

        public object SkipToken { get; private set; }

        public SortDefinition[] SortOrder { get; private set; }

        public CriteriaOperator Filter { get; private set; }
    }
}

