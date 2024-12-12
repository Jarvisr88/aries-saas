namespace DevExpress.DocumentServices.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    public class ReportServiceClientFactory : ServiceClientFactory<IReportServiceClient, IAsyncReportService>, IReportServiceClientFactory, IServiceClientFactory<IReportServiceClient>
    {
        public ReportServiceClientFactory(EndpointAddress remoteAddress) : base(remoteAddress)
        {
        }

        public ReportServiceClientFactory(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public ReportServiceClientFactory(EndpointAddress remoteAddress, Binding binding) : base(remoteAddress, binding)
        {
        }

        public ReportServiceClientFactory(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public override IReportServiceClient Create() => 
            new ReportServiceClient(base.ChannelFactory.CreateChannel());
    }
}

