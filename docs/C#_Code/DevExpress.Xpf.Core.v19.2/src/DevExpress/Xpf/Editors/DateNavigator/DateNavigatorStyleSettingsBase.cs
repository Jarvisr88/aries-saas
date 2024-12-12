namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Editors.DateNavigator.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class DateNavigatorStyleSettingsBase : FrameworkContentElement, IServiceContainer, IServiceProvider
    {
        public DateNavigatorStyleSettingsBase()
        {
            this.RegisteredServices = new Dictionary<Type, object>();
        }

        protected abstract INavigationService CreateNavigationService();
        internal T GetService<T>() => 
            (T) this.ServiceContainer.GetService(typeof(T));

        protected virtual object GetServiceInternal(Type serviceType)
        {
            object obj2;
            this.RegisteredServices.TryGetValue(serviceType, out obj2);
            return obj2;
        }

        protected internal virtual void Initialize(DevExpress.Xpf.Editors.DateNavigator.DateNavigator navigator)
        {
            this.Navigator = navigator;
            this.RegisterDefaultServices();
        }

        protected void RegisterDefaultService(Type serviceType, object serviceInstance)
        {
            if (!this.RegisteredServices.ContainsKey(serviceType))
            {
                this.RegisteredServices.Add(serviceType, serviceInstance);
            }
        }

        protected virtual void RegisterDefaultServices()
        {
            this.RegisterDefaultService(typeof(IValueEditingService), new ValueEditingStrategy(this.Navigator));
            this.RegisterDefaultService(typeof(INavigationCallbackService), new DummyNavigationCallbackService());
            this.RegisterDefaultService(typeof(IValueValidatingService), new ValueValidatingStrategy(this.Navigator));
            this.RegisterDefaultService(typeof(IDateCalculationService), new DateNavigatorWorkdayCalculator(this.Navigator));
            this.RegisterDefaultService(typeof(INavigationService), this.CreateNavigationService());
        }

        protected internal virtual void RegisterNavigationService()
        {
            this.RegisterService(typeof(INavigationService), this.CreateNavigationService());
        }

        protected void RegisterService(Type serviceType, object serviceInstance)
        {
            this.ServiceContainer.RemoveService(serviceType);
            this.ServiceContainer.AddService(serviceType, serviceInstance);
        }

        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            this.ServiceContainer.AddService(serviceType, callback(this, serviceType));
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance)
        {
            this.RegisteredServices.Add(serviceType, serviceInstance);
        }

        void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            this.ServiceContainer.AddService(serviceType, callback);
        }

        void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
        {
            this.ServiceContainer.AddService(serviceType, serviceInstance);
        }

        void IServiceContainer.RemoveService(Type serviceType)
        {
            if (this.RegisteredServices.ContainsKey(serviceType))
            {
                this.RegisteredServices.Remove(serviceType);
            }
        }

        void IServiceContainer.RemoveService(Type serviceType, bool promote)
        {
            this.ServiceContainer.RemoveService(serviceType);
        }

        object IServiceProvider.GetService(Type serviceType) => 
            this.GetServiceInternal(serviceType);

        protected DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator { get; private set; }

        protected IServiceContainer ServiceContainer =>
            this;

        private Dictionary<Type, object> RegisteredServices { get; set; }
    }
}

