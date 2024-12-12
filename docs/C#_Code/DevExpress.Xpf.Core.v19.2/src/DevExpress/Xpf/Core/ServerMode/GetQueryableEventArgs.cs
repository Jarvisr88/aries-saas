namespace DevExpress.Xpf.Core.ServerMode
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GetQueryableEventArgs : RoutedEventArgs
    {
        public bool AreSourceRowsThreadSafe { get; set; }

        public string KeyExpression { get; set; }

        public IQueryable QueryableSource { get; set; }

        public object Tag { get; set; }
    }
}

