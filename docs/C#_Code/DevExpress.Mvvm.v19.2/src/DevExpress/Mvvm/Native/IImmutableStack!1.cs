namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IImmutableStack<out T> : IEnumerable<T>, IEnumerable
    {
        T Peek();
        IImmutableStack<T> Pop();

        bool IsEmpty { get; }
    }
}

