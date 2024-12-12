namespace DevExpress.Xpf.Core.ServerMode
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GetODataSourceEventArgs : RoutedEventArgs
    {
        public bool AreSourceRowsThreadSafe { get; set; }

        public string[] KeyExpressions { get; set; }

        public IQueryable Query { get; set; }

        public object Tag { get; set; }
    }
}

