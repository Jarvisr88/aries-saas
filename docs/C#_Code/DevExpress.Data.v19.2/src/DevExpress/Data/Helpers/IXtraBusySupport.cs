namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IXtraBusySupport
    {
        event EventHandler IsBusyChanged;

        bool IsBusySupported { get; }

        bool IsBusy { get; }
    }
}

