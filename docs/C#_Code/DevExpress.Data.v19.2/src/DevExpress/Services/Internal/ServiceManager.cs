namespace DevExpress.Services.Internal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ServiceManager : IServiceContainer, IServiceProvider, IDisposable
    {
        private bool isDisposed;
        private Dictionary<Type, object> services = new Dictionary<Type, object>();

        public event EventHandler ServiceListChanged;

        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            if (!this.IsServiceExists(serviceType))
            {
                this.Services.Add(serviceType, callback);
                this.RaiseServiceListChanged(ListChangedType.ItemAdded, serviceType);
            }
        }

        public void AddService(Type serviceType, object serviceInstance)
        {
            if (!this.IsServiceExists(serviceType))
            {
                this.Services.Add(serviceType, serviceInstance);
                this.RaiseServiceListChanged(ListChangedType.ItemAdded, serviceType);
            }
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            if (!this.IsServiceExists(serviceType))
            {
                this.Services.Add(serviceType, callback);
                this.RaiseServiceListChanged(ListChangedType.ItemAdded, serviceType);
            }
        }

        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            if (!this.IsServiceExists(serviceType))
            {
                this.Services.Add(serviceType, serviceInstance);
                this.RaiseServiceListChanged(ListChangedType.ItemAdded, serviceType);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.services != null))
            {
                foreach (object obj2 in this.services.Values)
                {
                    IDisposable disposable = obj2 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                this.services = null;
            }
            this.isDisposed = true;
        }

        public virtual object GetService(Type serviceType)
        {
            object obj2;
            if (!this.Services.TryGetValue(serviceType, out obj2))
            {
                return null;
            }
            ServiceCreatorCallback callback = obj2 as ServiceCreatorCallback;
            if (callback != null)
            {
                obj2 = callback(this, serviceType);
                if (obj2 != null)
                {
                    this.Services[serviceType] = obj2;
                }
            }
            return obj2;
        }

        public virtual bool IsServiceExists(Type serviceType) => 
            this.Services.ContainsKey(serviceType);

        protected internal virtual void RaiseServiceListChanged(ListChangedType changedType, Type serviceType)
        {
            if (this.ServiceListChanged != null)
            {
                this.ServiceListChanged(this, new ServiceListChangedEventArgs(serviceType, changedType));
            }
        }

        public void RemoveService(Type serviceType)
        {
            if (this.IsServiceExists(serviceType))
            {
                this.Services.Remove(serviceType);
                this.RaiseServiceListChanged(ListChangedType.ItemDeleted, serviceType);
            }
        }

        public void RemoveService(Type serviceType, bool promote)
        {
            if (this.IsServiceExists(serviceType))
            {
                this.Services.Remove(serviceType);
                this.RaiseServiceListChanged(ListChangedType.ItemDeleted, serviceType);
            }
        }

        public Dictionary<Type, object> Services =>
            this.services;

        public bool IsDisposed =>
            this.isDisposed;
    }
}

