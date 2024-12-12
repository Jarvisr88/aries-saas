namespace DevExpress.Data
{
    using System;

    public interface IDataControllerThreadClient
    {
        void OnAsyncBegin();
        void OnAsyncEnd();
        void OnRowLoaded(int controllerRowHandle);
        void OnTotalsReceived();
    }
}

