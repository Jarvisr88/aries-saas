namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;

    public interface INotifyLockableChanged : ILockable
    {
        event EventHandler OnBeginUpdate;

        event EventHandler OnEndUpdate;
    }
}

