namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class FetchPageEventArgsBase : FetchEventArgsBase
    {
        internal FetchPageEventArgsBase(CancellationToken cancellationToken, int skip, object skipToken, int take, SortDefinition[] sortOrder, CriteriaOperator filter) : base(cancellationToken, skip, skipToken, sortOrder, filter)
        {
            this.Take = take;
        }

        public int Take { get; private set; }
    }
}

