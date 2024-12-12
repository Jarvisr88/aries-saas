namespace DevExpress.ReportServer.ServiceModel.Client
{
    using System;
    using System.ServiceModel;

    [ServiceContract(Name="IAuthenticationService")]
    public interface IAuthenticationServiceAsync
    {
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginLogin(string userName, string password, AsyncCallback callback, object asyncState);
        bool EndLogin(IAsyncResult ar);
    }
}

