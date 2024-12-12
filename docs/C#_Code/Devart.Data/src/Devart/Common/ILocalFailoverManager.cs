namespace Devart.Common
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public interface ILocalFailoverManager : IDisposable
    {
        RetryMode DoLocalFailoverEvent(object sender, ConnectionLostCause cause, RetryMode retryMode, Exception e);
        ILocalFailoverManager StartUse();
        ILocalFailoverManager StartUse(bool fireConnErrorEvent);
    }
}

