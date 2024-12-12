namespace DevExpress.Data
{
    using System;

    public interface IBoundControl
    {
        IAsyncResult BeginInvoke(Delegate method, params object[] arguments);

        bool IsHandleCreated { get; }

        bool InvokeRequired { get; }
    }
}

