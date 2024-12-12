namespace DevExpress.ReportServer.ServiceModel.ConnectionProviders
{
    using DevExpress.ReportServer.Printing;
    using DevExpress.ReportServer.ServiceModel.Client;
    using System;
    using System.Threading.Tasks;

    public class ServerUserConnectionProvider : ConnectionProvider
    {
        private readonly string userName;
        private readonly string password;

        public ServerUserConnectionProvider(string serverAddress, string userName, string password) : base(serverAddress)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("userName");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password");
            }
            this.userName = userName;
            this.password = password;
        }

        protected override AuthenticationServiceClientFactory CreateAuthClientFactory()
        {
            AuthenticationServiceClientFactory factory = new AuthenticationServiceClientFactory(base.GetEndpointAddress("AuthenticationService.svc"), AuthenticationType.Forms);
            factory.ChannelFactory.Endpoint.EndpointBehaviors.Add(base.CookieBehavior);
            return factory;
        }

        public override Task<bool> LoginAsync() => 
            base.LoginCoreAsync(this.userName, this.password);
    }
}

