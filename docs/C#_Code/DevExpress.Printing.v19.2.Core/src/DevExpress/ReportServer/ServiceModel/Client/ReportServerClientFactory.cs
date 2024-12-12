namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.Xpf.Printing;
    using System;
    using System.ComponentModel;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    [TypeConverter(typeof(ServiceClientFactoryConverter))]
    public class ReportServerClientFactory : ServiceClientFactory<IReportServerClient, IReportServerFacadeAsync>, IReportServiceClientFactory, IServiceClientFactory<IReportServiceClient>
    {
        private readonly string endpointConfigurationName;
        private readonly string endpointAddress;
        private readonly bool useSSL;

        static ReportServerClientFactory()
        {
            ServiceKnownTypeProvider.Register<TransientReportId>();
            ServiceKnownTypeProvider.Register<ReportIdentity>();
            ServiceKnownTypeProvider.Register<ReportLayoutRevisionIdentity>();
            ServiceKnownTypeProvider.Register<GeneratedReportIdentity>();
        }

        public ReportServerClientFactory(System.ServiceModel.EndpointAddress remoteAddress) : base(remoteAddress)
        {
            this.endpointAddress = remoteAddress.Uri.OriginalString;
            this.useSSL = remoteAddress.Uri.Scheme == Uri.UriSchemeHttps;
        }

        public ReportServerClientFactory(string endpointConfigurationName) : base(endpointConfigurationName)
        {
            this.endpointConfigurationName = endpointConfigurationName;
        }

        public ReportServerClientFactory(System.ServiceModel.EndpointAddress remoteAddress, Binding binding) : base(remoteAddress, binding)
        {
            this.endpointAddress = remoteAddress.Uri.OriginalString;
        }

        public ReportServerClientFactory(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
            this.endpointConfigurationName = endpointConfigurationName;
            this.endpointAddress = remoteAddress.Uri.OriginalString;
        }

        public override IReportServerClient Create() => 
            new ReportServerClient(base.ChannelFactory.CreateChannel());

        IReportServiceClient IServiceClientFactory<IReportServiceClient>.Create() => 
            this.Create();

        internal string EndpointConfigurationName =>
            this.endpointConfigurationName;

        internal string EndpointAddress =>
            this.endpointAddress;
    }
}

