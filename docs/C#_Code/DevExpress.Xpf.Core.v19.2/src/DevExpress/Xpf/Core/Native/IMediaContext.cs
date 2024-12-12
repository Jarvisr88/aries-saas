namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Threading;

    public interface IMediaContext
    {
        void BeginInvokeOnRender(DispatcherOperationCallback callback, object arg);
    }
}

