namespace DevExpress.ReportServer.ServiceModel.ConnectionProviders
{
    using DevExpress.ReportServer.ServiceModel.Client;
    using System;
    using System.Security;
    using System.ServiceModel;
    using System.Threading.Tasks;

    public abstract class ConnectionProvider
    {
        private readonly Uri serverUri;
        private readonly FormsAuthenticationEndpointBehavior cookieBehavior;
        private readonly Lazy<ReportServerClientFactory> serverClientFactory;
        private readonly Lazy<AuthenticationServiceClientFactory> authClientFactory;

        public ConnectionProvider(string serverAddress)
        {
            this.serverUri = new Uri(serverAddress, UriKind.Absolute);
            this.cookieBehavior = new FormsAuthenticationEndpointBehavior();
            this.serverClientFactory = new Lazy<ReportServerClientFactory>(new Func<ReportServerClientFactory>(this.CreateReportServerClientFactory));
            this.authClientFactory = new Lazy<AuthenticationServiceClientFactory>(new Func<AuthenticationServiceClientFactory>(this.CreateAuthClientFactory));
        }

        public Task<IReportServerClient> ConnectAsync() => 
            this.LoginAsync().ContinueWith<IReportServerClient>(delegate (Task<bool> task) {
                if (!task.Result)
                {
                    throw new SecurityException("Invalid user name or password.");
                }
                return this.CreateClient();
            });

        protected abstract AuthenticationServiceClientFactory CreateAuthClientFactory();
        public IReportServerClient CreateClient() => 
            this.serverClientFactory.Value.Create();

        private ReportServerClientFactory CreateReportServerClientFactory()
        {
            ReportServerClientFactory factory = new ReportServerClientFactory(this.GetEndpointAddress("ReportServerFacade.svc"));
            factory.ChannelFactory.Endpoint.EndpointBehaviors.Add(this.cookieBehavior);
            return factory;
        }

        protected EndpointAddress GetEndpointAddress(string epRelativeAddress) => 
            new EndpointAddress(new Uri(this.serverUri, epRelativeAddress), new AddressHeader[0]);

        public abstract Task<bool> LoginAsync();
        protected Task<bool> LoginCoreAsync(string userName, string password) => 
            this.authClientFactory.Value.Create().LoginAsync(userName, password, null);

        protected FormsAuthenticationEndpointBehavior CookieBehavior =>
            this.cookieBehavior;
    }
}

