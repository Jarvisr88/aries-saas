namespace DevExpress.Data.Utils.ServiceModel
{
    using System;
    using System.Threading;

    public interface IServiceClientBase
    {
        void Abort();
        void CloseAsync();
        void SetSynchronizationContext(SynchronizationContext syncContext);
    }
}

