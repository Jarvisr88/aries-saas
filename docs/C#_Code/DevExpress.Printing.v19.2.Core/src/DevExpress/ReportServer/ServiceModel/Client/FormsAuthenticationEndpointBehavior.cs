namespace DevExpress.ReportServer.ServiceModel.Client
{
    using System;
    using System.Collections.Concurrent;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public class FormsAuthenticationEndpointBehavior : IEndpointBehavior
    {
        private readonly ConcurrentDictionary<string, FormsAuthenticationMessageInspector> messageInspectors = new ConcurrentDictionary<string, FormsAuthenticationMessageInspector>();

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            FormsAuthenticationMessageInspector inspector;
            if (!this.messageInspectors.TryGetValue(endpoint.Address.Uri.Host, out inspector))
            {
                inspector = this.CreateCurrentInspector();
                this.messageInspectors.TryAdd(endpoint.Address.Uri.Host, inspector);
            }
            clientRuntime.ClientMessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        protected virtual FormsAuthenticationMessageInspector CreateCurrentInspector() => 
            new FormsAuthenticationMessageInspector();

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}

