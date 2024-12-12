namespace DevExpress.Data
{
    using System;

    internal class NullThreadClient : IDataControllerThreadClient
    {
        internal static NullThreadClient Default;

        static NullThreadClient();
        void IDataControllerThreadClient.OnAsyncBegin();
        void IDataControllerThreadClient.OnAsyncEnd();
        void IDataControllerThreadClient.OnRowLoaded(int controllerRowHandle);
        void IDataControllerThreadClient.OnTotalsReceived();
    }
}

