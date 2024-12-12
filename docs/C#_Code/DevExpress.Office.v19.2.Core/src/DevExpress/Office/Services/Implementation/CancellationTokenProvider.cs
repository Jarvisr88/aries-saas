namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using System;
    using System.Threading;

    public class CancellationTokenProvider : ICancellationTokenProvider
    {
        private CancellationToken token;

        public CancellationTokenProvider()
        {
            this.token = new CancellationToken();
        }

        public CancellationTokenProvider(CancellationToken token)
        {
            this.token = token;
        }

        public CancellationTokenRegistration Register(Action action) => 
            this.token.Register(action);

        public void ThrowIfCancellationRequested()
        {
            this.token.ThrowIfCancellationRequested();
        }

        public bool IsCancellationRequested =>
            this.token.IsCancellationRequested;

        public bool CanBeCanceled =>
            this.token.CanBeCanceled;
    }
}

