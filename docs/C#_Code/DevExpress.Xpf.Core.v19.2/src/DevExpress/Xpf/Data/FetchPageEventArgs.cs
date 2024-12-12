namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class FetchPageEventArgs : FetchPageEventArgsBase
    {
        internal FetchPageEventArgs(CancellationToken cancellationToken, object source, int skip, object skipToken, int take, SortDefinition[] order, CriteriaOperator filter) : base(cancellationToken, skip, skipToken, take, order, filter)
        {
            this.Source = source;
            this.HasMoreRows = true;
        }

        public object[] Result { get; set; }

        public bool HasMoreRows { get; set; }

        public object NextSkipToken { get; set; }

        public object Source { get; private set; }
    }
}

