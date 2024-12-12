namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class ServiceContainer : ServiceContainerBase, IServiceContainer
    {
        private static IServiceContainer _default = new DefaultServiceContainer();
        private static IServiceContainer custom = null;
        private readonly object owner;

        public ServiceContainer(object owner)
        {
            this.owner = owner;
        }

        public void Clear()
        {
            base.ClearCore();
        }

        T IServiceContainer.GetService<T>(ServiceSearchMode searchMode) where T: class
        {
            bool flag;
            return (T) this.GetServiceCore(typeof(T), null, searchMode, out flag);
        }

        T IServiceContainer.GetService<T>(string key, ServiceSearchMode searchMode) where T: class
        {
            bool flag;
            return (T) this.GetServiceCore(typeof(T), key, searchMode, out flag);
        }

        T IServiceContainer.GetService<T>(string key, ServiceSearchMode searchMode, out bool serviceHasKey) where T: class => 
            (T) this.GetServiceCore(typeof(T), key, searchMode, out serviceHasKey);

        object IServiceContainer.GetService(Type type, string key, ServiceSearchMode searchMode, out bool serviceHasKey) => 
            this.GetServiceCore(type, key, searchMode, out serviceHasKey);

        IEnumerable<object> IServiceContainer.GetServices(Type type, bool localOnly) => 
            this.GetServicesCore(type, localOnly);

        public T GetService<T>(ServiceSearchMode searchMode = 0) where T: class => 
            this.GetService<T>(searchMode);

        public T GetService<T>(string key, ServiceSearchMode searchMode = 0) where T: class => 
            this.GetService<T>(key, searchMode);

        public void RegisterService(object service, bool yieldToParent)
        {
            this.RegisterService(null, service, yieldToParent);
        }

        public void RegisterService(string key, object service, bool yieldToParent)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            this.UnregisterService(service);
            base.AddCore(ServiceContainerBase.ServiceInfo.Create(key, service, yieldToParent));
        }

        public void UnregisterService(object service)
        {
            base.RemoveCore(base.GetCore(service));
        }

        public static IServiceContainer Default
        {
            get => 
                custom ?? _default;
            set => 
                custom = value;
        }

        protected override IServiceContainer ParentServiceContainer
        {
            get
            {
                if (ReferenceEquals(this, Default))
                {
                    return null;
                }
                ISupportParentViewModel owner = this.owner as ISupportParentViewModel;
                ISupportServices objA = (owner != null) ? (owner.ParentViewModel as ISupportServices) : null;
                IServiceContainer local1 = ((objA == null) || ReferenceEquals(objA, this.owner)) ? null : objA.ServiceContainer;
                IServiceContainer container1 = local1;
                if (local1 == null)
                {
                    IServiceContainer local2 = local1;
                    container1 = Default;
                }
                return container1;
            }
        }
    }
}

