namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Services;
    using DevExpress.Utils;
    using System;

    public class CancellationProvider
    {
        public const int DefaultCheckInterval = 0x3e8;
        private readonly IOfficeServiceContainer serviceContainer;
        private int checkInterval;
        private int checkCount;

        public CancellationProvider(IOfficeServiceContainer serviceContainer)
        {
            Guard.ArgumentNotNull(serviceContainer, "serviceContainer");
            this.serviceContainer = serviceContainer;
            this.checkInterval = 0x3e8;
            this.checkCount = 0;
        }

        public void ThrowIfCancellationRequested()
        {
            this.checkCount++;
            if (this.checkCount >= this.checkInterval)
            {
                ICancellationTokenProvider service = this.serviceContainer.GetService<ICancellationTokenProvider>();
                if (service != null)
                {
                    service.ThrowIfCancellationRequested();
                }
                this.checkCount = 0;
            }
        }

        public int CheckInterval
        {
            get => 
                this.checkInterval;
            set
            {
                Guard.ArgumentPositive(value, "CheckInterval");
                this.checkInterval = value;
                this.checkCount = 0;
            }
        }
    }
}

