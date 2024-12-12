namespace DevExpress.ReportServer.ServiceModel.ConnectionProviders
{
    using DevExpress.ReportServer.Printing;
    using DevExpress.ReportServer.ServiceModel.Client;
    using System;
    using System.Threading.Tasks;

    public class WindowsUserConnectionProvider : ConnectionProvider
    {
        public WindowsUserConnectionProvider(string serverAddress) : base(serverAddress)
        {
        }

        protected override AuthenticationServiceClientFactory CreateAuthClientFactory()
        {
            AuthenticationServiceClientFactory factory = new AuthenticationServiceClientFactory(base.GetEndpointAddress("WindowsAuthentication/AuthenticationService.svc"), AuthenticationType.Windows);
            factory.ChannelFactory.Endpoint.EndpointBehaviors.Add(base.CookieBehavior);
            return factory;
        }

        public override Task<bool> LoginAsync() => 
            base.LoginCoreAsync(null, null);
    }
}

