namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IBaseObject : IDisposable
    {
        event EventHandler Disposed;

        bool IsDisposing { get; }
    }
}

