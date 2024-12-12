namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void DependencyPropertyChangedCallback<Owner, T>(Owner owner, DependencyPropertyChangedEventArgs<T> e);
}

