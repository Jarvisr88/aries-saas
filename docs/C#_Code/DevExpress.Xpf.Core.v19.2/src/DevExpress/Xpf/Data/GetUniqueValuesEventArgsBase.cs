namespace DevExpress.Xpf.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class GetUniqueValuesEventArgsBase : BaseAsyncArgs
    {
        internal GetUniqueValuesEventArgsBase(CancellationToken cancellationToken, string propertyName, CriteriaOperator filter) : base(cancellationToken)
        {
            this.PropertyName = propertyName;
            this.Filter = filter;
        }

        public string PropertyName { get; private set; }

        public CriteriaOperator Filter { get; private set; }
    }
}

