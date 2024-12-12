namespace DevExpress.Internal
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public enum ToastDismissalReason : long
    {
        UserCanceled = 0L,
        ApplicationHidden = 1L,
        TimedOut = 2L
    }
}

