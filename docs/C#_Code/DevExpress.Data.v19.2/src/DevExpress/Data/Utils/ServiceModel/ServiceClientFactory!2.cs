namespace DevExpress.Data.Utils.ServiceModel
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public abstract class ServiceClientFactory<TClient, TChannel> : IServiceClientFactory<TClient>
    {
        [Obsolete("Use ServiceClientFactory.DefaultBindingBufferSizeLimit constant instead.")]
        public const int DefaultBindingBufferSizeLimit = 0x400000;
        private Lazy<ChannelFactory<TChannel>> lazyChannelFactory;

        public ServiceClientFactory(EndpointAddress remoteAddress);
        public ServiceClientFactory(string endpointConfigurationName);
        public ServiceClientFactory(EndpointAddress remoteAddress, Binding binding);
        public ServiceClientFactory(string endpointConfigurationName, EndpointAddress remoteAddress);
        public abstract TClient Create();
        protected virtual Binding CreateDefaultBinding(EndpointAddress remoteAddress);
        protected virtual ChannelFactory<TChannel> WrapVersionInspectorBehavior(ChannelFactory<TChannel> channelFactory);

        public ChannelFactory<TChannel> ChannelFactory { get; }

        public ClientCredentials Credentials { get; }

        private class VersionEndpointBehavior : IEndpointBehavior
        {
            public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters);
            public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime);
            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher);
            public void Validate(ServiceEndpoint endpoint);
        }

        private class VersionMessageInspector : IClientMessageInspector
        {
            public void AfterReceiveReply(ref Message reply, object correlationState);
            public object BeforeSendRequest(ref Message request, IClientChannel channel);
        }
    }
}

