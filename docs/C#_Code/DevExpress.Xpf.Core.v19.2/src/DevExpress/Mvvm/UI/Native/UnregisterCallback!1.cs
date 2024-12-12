namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void UnregisterCallback<E>(EventHandler<E> eventHandler) where E: EventArgs;
}

