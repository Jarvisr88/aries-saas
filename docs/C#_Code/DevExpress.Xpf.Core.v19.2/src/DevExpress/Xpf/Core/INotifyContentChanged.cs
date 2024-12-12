namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public interface INotifyContentChanged
    {
        event EventHandler ContentChanged;
    }
}

