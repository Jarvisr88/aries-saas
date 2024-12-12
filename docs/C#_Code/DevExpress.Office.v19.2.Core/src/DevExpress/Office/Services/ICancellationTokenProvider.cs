namespace DevExpress.Office.Services
{
    using System;
    using System.Threading;

    public interface ICancellationTokenProvider
    {
        CancellationTokenRegistration Register(Action action);
        void ThrowIfCancellationRequested();

        bool IsCancellationRequested { get; }

        bool CanBeCanceled { get; }
    }
}

