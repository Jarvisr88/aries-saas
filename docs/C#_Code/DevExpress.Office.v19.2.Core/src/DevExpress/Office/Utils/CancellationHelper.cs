namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Services;
    using DevExpress.Office.Services.Implementation;
    using DevExpress.Utils;
    using System;
    using System.Threading;

    public class CancellationHelper : IDisposable
    {
        private IOfficeServiceContainer serviceContainer;
        private ICancellationTokenProvider savedCancellationTokenProvider;

        public CancellationHelper(IOfficeServiceContainer serviceContainer, CancellationToken cancellationToken)
        {
            Guard.ArgumentNotNull(serviceContainer, "serviceContainer");
            this.serviceContainer = serviceContainer;
            this.savedCancellationTokenProvider = this.serviceContainer.ReplaceService<ICancellationTokenProvider>(new CancellationTokenProvider(cancellationToken));
        }

        public void Dispose()
        {
            if (this.serviceContainer != null)
            {
                this.serviceContainer.ReplaceService<ICancellationTokenProvider>(this.savedCancellationTokenProvider);
            }
            this.serviceContainer = null;
        }
    }
}

