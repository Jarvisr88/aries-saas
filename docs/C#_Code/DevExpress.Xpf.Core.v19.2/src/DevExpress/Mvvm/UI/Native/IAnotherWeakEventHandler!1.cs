namespace DevExpress.Mvvm.UI.Native
{
    using System;

    public interface IAnotherWeakEventHandler<E> where E: EventArgs
    {
        EventHandler<E> Handler { get; }
    }
}

