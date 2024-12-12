namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IXtraSourceError
    {
        event EventHandler<ErrorEventArgs> ErrorOccurred;
    }
}

