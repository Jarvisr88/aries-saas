namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class FetchRowsEventArgsBase : FetchEventArgsBase
    {
        private volatile bool allowRetry;

        internal FetchRowsEventArgsBase(CancellationToken cancellationToken, int skip, object skipToken, SortDefinition[] sortOrder, CriteriaOperator filter) : base(cancellationToken, skip, skipToken, sortOrder, filter)
        {
            this.AllowRetry = true;
        }

        public bool AllowRetry
        {
            get => 
                this.allowRetry;
            set => 
                this.allowRetry = value;
        }
    }
}

