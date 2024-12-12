namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ISupportNotification<T> where T: class
    {
        event CollectionChangedHandler<T> CollectionChanged;
    }
}

