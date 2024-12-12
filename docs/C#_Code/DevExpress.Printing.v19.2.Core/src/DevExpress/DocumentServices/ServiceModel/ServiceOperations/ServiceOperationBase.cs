namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    public abstract class ServiceOperationBase
    {
        private static IDelayerFactory delayerFactory = new SynchronizedDelayerFactory();
        protected readonly Guid instanceId = Guid.NewGuid();
        private readonly IServiceClientBase client;
        private readonly IDelayer delayer;
        protected DocumentId documentId;

        public ServiceOperationBase(IServiceClientBase client, TimeSpan statusUpdateInterval)
        {
            Guard.ArgumentNotNull(client, "client");
            this.delayer = DelayerFactory.Create(statusUpdateInterval);
            this.client = client;
        }

        [Conditional("DEBUG")]
        protected void AssertInstanceId(object userState)
        {
        }

        protected bool IsSameInstanceId(object userState) => 
            userState.Equals(this.instanceId);

        protected virtual bool TryProcessError(AsyncCompletedEventArgs e) => 
            e.Cancelled || (e.Error != null);

        protected abstract void UnsubscribeClientEvents();

        public static IDelayerFactory DelayerFactory
        {
            get => 
                delayerFactory;
            set
            {
                Guard.ArgumentNotNull(value, "value");
                delayerFactory = value;
            }
        }

        protected IDelayer Delayer =>
            this.delayer;

        protected IServiceClientBase Client =>
            this.client;
    }
}

