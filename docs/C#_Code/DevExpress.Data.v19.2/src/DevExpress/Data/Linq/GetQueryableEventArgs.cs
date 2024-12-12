namespace DevExpress.Data.Linq
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class GetQueryableEventArgs : EventArgs
    {
        public GetQueryableEventArgs();

        public IQueryable QueryableSource { get; set; }

        public string KeyExpression { get; set; }

        public bool AreSourceRowsThreadSafe { get; set; }

        public object Tag { get; set; }
    }
}

