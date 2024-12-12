namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;

    public sealed class ServiceManagerBase : IServiceProvider, IServiceContainer
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
        private readonly List<IServiceProvider> innerProviders = new List<IServiceProvider>();

        public void AddInnerProvider(IServiceProvider provider)
        {
            Guard.ArgumentNotNull(provider, "provider");
            this.innerProviders.Add(provider);
        }

        public bool Contains(Type serviceType)
        {
            Guard.ArgumentNotNull(serviceType, "serviceType");
            return this.services.ContainsKey(serviceType);
        }

        public object GetService(Type serviceType)
        {
            object service;
            object obj3;
            Guard.ArgumentNotNull(serviceType, "serviceType");
            if (this.services.TryGetValue(serviceType, out service))
            {
                Func<object> func = service as Func<object>;
                if (func != null)
                {
                    service = func();
                    this.services[serviceType] = service;
                }
                return service;
            }
            using (List<IServiceProvider>.Enumerator enumerator = this.innerProviders.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        service = enumerator.Current.GetService(serviceType);
                        if (service == null)
                        {
                            continue;
                        }
                        obj3 = service;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return obj3;
        }

        public void Publish<TService>(Func<TService> serviceCallback)
        {
            Guard.ArgumentNotNull(serviceCallback, "serviceCallback");
            this.services[typeof(TService)] = () => serviceCallback();
        }

        public void Publish<TService>(object serviceInstance)
        {
            Guard.ArgumentNotNull(serviceInstance, "serviceInstance");
            this.services[typeof(TService)] = serviceInstance;
        }

        public void Publish(Type serviceType, Func<object> serviceCallback)
        {
            Guard.ArgumentNotNull(serviceType, "serviceType");
            Guard.ArgumentNotNull(serviceCallback, "serviceCallback");
            this.services[serviceType] = serviceCallback;
        }

        public void Publish(Type serviceType, object serviceInstance)
        {
            Guard.ArgumentNotNull(serviceType, "serviceType");
            Guard.ArgumentNotNull(serviceInstance, "serviceInstance");
            this.services[serviceType] = serviceInstance;
        }

        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            this.Publish(serviceType, (Func<object>) (() => callback(this, serviceType)));
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance)
        {
            this.Publish(serviceType, serviceInstance);
        }

        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            this.Publish(serviceType, (Func<object>) (() => callback(this, serviceType)));
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
        {
            this.Publish(serviceType, serviceInstance);
        }

        void IServiceContainer.RemoveService(Type serviceType)
        {
            throw new NotSupportedException();
        }

        void IServiceContainer.RemoveService(Type serviceType, bool promote)
        {
            throw new NotSupportedException();
        }
    }
}

