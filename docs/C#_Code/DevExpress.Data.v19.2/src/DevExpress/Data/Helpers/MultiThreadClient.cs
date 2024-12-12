namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public class MultiThreadClient : IDataControllerThreadClient
    {
        private List<IDataControllerThreadClient> listeners;

        public void Add(IDataControllerThreadClient client);
        void IDataControllerThreadClient.OnAsyncBegin();
        void IDataControllerThreadClient.OnAsyncEnd();
        void IDataControllerThreadClient.OnRowLoaded(int controllerRowHandle);
        void IDataControllerThreadClient.OnTotalsReceived();
        public void Remove(IDataControllerThreadClient client);

        public List<IDataControllerThreadClient> Listeners { get; }
    }
}

