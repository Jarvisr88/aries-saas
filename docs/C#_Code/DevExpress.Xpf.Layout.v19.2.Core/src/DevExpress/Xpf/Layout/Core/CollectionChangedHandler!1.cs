namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void CollectionChangedHandler<T>(CollectionChangedEventArgs<T> ea) where T: class;
}

