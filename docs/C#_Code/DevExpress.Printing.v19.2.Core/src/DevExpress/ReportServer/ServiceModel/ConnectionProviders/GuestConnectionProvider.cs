namespace DevExpress.ReportServer.ServiceModel.ConnectionProviders
{
    using DevExpress.ReportServer.Printing;
    using DevExpress.ReportServer.ServiceModel.Client;
    using System;
    using System.Threading.Tasks;

    public class GuestConnectionProvider : ConnectionProvider
    {
        public GuestConnectionProvider(string serverAddress) : base(serverAddress)
        {
        }

        protected override AuthenticationServiceClientFactory CreateAuthClientFactory()
        {
            AuthenticationServiceClientFactory factory = new AuthenticationServiceClientFactory(base.GetEndpointAddress("AuthenticationService.svc"), AuthenticationType.Guest);
            factory.ChannelFactory.Endpoint.EndpointBehaviors.Add(base.CookieBehavior);
            return factory;
        }

        public override Task<bool> LoginAsync() => 
            base.LoginCoreAsync(null, null);
    }
}

