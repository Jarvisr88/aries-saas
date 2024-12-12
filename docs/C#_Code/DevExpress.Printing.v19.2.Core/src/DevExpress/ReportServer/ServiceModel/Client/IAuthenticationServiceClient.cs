namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using System;
    using System.Threading.Tasks;

    public interface IAuthenticationServiceClient : IServiceClientBase
    {
        void Login(string userName, string password, object state, Action<ScalarOperationCompletedEventArgs<bool>> onCompleted);
        Task<bool> LoginAsync(string userName, string password, object asyncState);
    }
}

