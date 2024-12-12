namespace DevExpress.DocumentServices.ServiceModel.Native
{
    using DevExpress.Utils;
    using System;
    using System.ServiceModel;

    public class ServiceClientCreator<TClient, TFactory> where TFactory: IServiceClientFactory<TClient>
    {
        private readonly Func<EndpointAddress, TFactory> createFactory;
        private TFactory factory;
        private string uri;

        public ServiceClientCreator(Func<EndpointAddress, TFactory> createFactory)
        {
            Guard.ArgumentNotNull(createFactory, "createFactory");
            this.createFactory = createFactory;
        }

        public TClient Create()
        {
            if (!this.CanCreateClient)
            {
                throw new InvalidOperationException("Cannot create a service client object because neither service uri nor service client factory is set.");
            }
            return ((this.Factory != null) ? this.Factory : this.createFactory(new EndpointAddress(this.ServiceUri))).Create();
        }

        public string ServiceUri
        {
            get => 
                this.uri;
            set
            {
                if (!string.IsNullOrEmpty(value) && (this.Factory != null))
                {
                    throw new InvalidOperationException("Use either service uri or client factory, but not both of them.");
                }
                this.uri = value;
            }
        }

        public TFactory Factory
        {
            get => 
                this.factory;
            set
            {
                if ((value != null) && !string.IsNullOrEmpty(this.ServiceUri))
                {
                    throw new InvalidOperationException("Use either service uri or client factory, but not both of them.");
                }
                this.factory = value;
            }
        }

        public bool CanCreateClient =>
            !string.IsNullOrEmpty(this.ServiceUri) || (this.Factory != null);
    }
}

