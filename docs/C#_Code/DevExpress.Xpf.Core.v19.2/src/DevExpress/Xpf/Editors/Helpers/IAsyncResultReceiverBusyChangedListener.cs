namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public interface IAsyncResultReceiverBusyChangedListener
    {
        void ProcessBusyChanged(bool isBusy);
    }
}

