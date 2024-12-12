namespace DevExpress.ReportServer.ServiceModel.Client
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        bool Login(string userName, string password);
    }
}

