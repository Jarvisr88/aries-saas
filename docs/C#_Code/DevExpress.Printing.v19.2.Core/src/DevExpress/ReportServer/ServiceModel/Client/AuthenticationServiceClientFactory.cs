namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.ReportServer.Printing;
    using DevExpress.Utils;
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    public class AuthenticationServiceClientFactory : ServiceClientFactory<IAuthenticationServiceClient, IAuthenticationServiceAsync>
    {
        private readonly AuthenticationType authenticationType;
        private readonly bool useSSL;

        public AuthenticationServiceClientFactory(string endpointConfigurationName) : this(endpointConfigurationName, null)
        {
        }

        public AuthenticationServiceClientFactory(EndpointAddress address, AuthenticationType authenticationType) : base(address)
        {
            this.authenticationType = authenticationType;
            this.useSSL = address.Uri.Scheme == Uri.UriSchemeHttps;
        }

        public AuthenticationServiceClientFactory(EndpointAddress remoteAddress, Binding binding) : base(remoteAddress, binding)
        {
            DevExpress.Utils.Guard.ArgumentNotNull(remoteAddress, "remoteAddress");
        }

        public AuthenticationServiceClientFactory(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public override IAuthenticationServiceClient Create() => 
            new AuthenticationServiceClient(base.ChannelFactory.CreateChannel());

        protected override Binding CreateDefaultBinding(EndpointAddress remoteAddress)
        {
            BasicHttpSecurity security = new BasicHttpSecurity();
            if (this.authenticationType != AuthenticationType.Windows)
            {
                security.Mode = this.useSSL ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None;
            }
            else
            {
                security.Mode = this.useSSL ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.TransportCredentialOnly;
                HttpTransportSecurity security1 = new HttpTransportSecurity();
                security1.ClientCredentialType = HttpClientCredentialType.Windows;
                security.Transport = security1;
            }
            BasicHttpBinding binding1 = new BasicHttpBinding();
            binding1.Security = security;
            return binding1;
        }
    }
}

