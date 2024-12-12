namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class GetUniqueValuesEventArgs : GetUniqueValuesEventArgsBase
    {
        internal GetUniqueValuesEventArgs(CancellationToken cancellationToken, object source, string propertyName, CriteriaOperator filter) : base(cancellationToken, propertyName, filter)
        {
            this.Source = source;
        }

        public object[] Result { get; set; }

        public ValueAndCount[] ResultWithCounts { get; set; }

        public object Source { get; private set; }
    }
}

