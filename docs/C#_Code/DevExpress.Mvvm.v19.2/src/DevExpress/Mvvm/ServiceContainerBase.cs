namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ServiceContainerBase
    {
        private readonly List<ServiceInfo> services = new List<ServiceInfo>();
        private bool searchInParentServiceLocker;

        protected void AddCore(ServiceInfo info)
        {
            List<ServiceInfo> services = this.services;
            lock (services)
            {
                this.services.Add(info);
            }
        }

        private void CheckServiceType(Type type)
        {
            if (!type.IsInterface)
            {
                throw new ArgumentException("Services can be only accessed via interface types");
            }
        }

        protected void ClearCore()
        {
            List<ServiceInfo> services = this.services;
            lock (services)
            {
                this.services.Clear();
            }
        }

        private ServiceInfo FindService(Type t, string key, ServiceSearchMode searchMode)
        {
            ServiceInfo info;
            bool findServiceWithKey = !string.IsNullOrEmpty(key);
            if ((searchMode == ServiceSearchMode.LocalOnly) || (this.ParentServiceContainer == null))
            {
                return this.GetLocalService(t, findServiceWithKey, key);
            }
            try
            {
                if (this.searchInParentServiceLocker)
                {
                    throw new Exception("A ServiceContainer should not be a direct or indirect parent for itself.");
                }
                this.searchInParentServiceLocker = true;
                info = ServiceInfo.Create(this.ParentServiceContainer, t, key, searchMode);
            }
            finally
            {
                this.searchInParentServiceLocker = false;
            }
            if ((searchMode == ServiceSearchMode.PreferParents) && ((info != null) && (findServiceWithKey || !info.HasKey)))
            {
                return info;
            }
            ServiceInfo secondary = this.GetLocalService(t, findServiceWithKey, key);
            return ((secondary != null) ? ((info != null) ? (((searchMode == ServiceSearchMode.PreferParents) || secondary.YieldToParent) ? this.FindServiceCore(findServiceWithKey, info, secondary) : this.FindServiceCore(findServiceWithKey, secondary, info)) : secondary) : info);
        }

        private ServiceInfo FindServiceCore(bool findServiceWithKey, ServiceInfo primary, ServiceInfo secondary) => 
            (findServiceWithKey || (!primary.HasKey || secondary.HasKey)) ? primary : secondary;

        protected ServiceInfo GetCore(object service)
        {
            List<ServiceInfo> services = this.services;
            lock (services)
            {
                return this.services.FirstOrDefault<ServiceInfo>(x => (x.Service == service));
            }
        }

        private ServiceInfo GetLocalService(Type t, bool findServiceWithKey, string key)
        {
            IEnumerable<ServiceInfo> source = null;
            List<ServiceInfo> services = this.services;
            lock (services)
            {
                source = (from x in this.services
                    where x.Is(t)
                    select x).ToList<ServiceInfo>();
            }
            ServiceInfo info = source.LastOrDefault<ServiceInfo>(x => x.Key == key);
            Func<ServiceInfo, bool> predicate = <>c.<>9__14_2;
            if (<>c.<>9__14_2 == null)
            {
                Func<ServiceInfo, bool> local1 = <>c.<>9__14_2;
                predicate = <>c.<>9__14_2 = x => !x.HasKey;
            }
            ServiceInfo info2 = source.LastOrDefault<ServiceInfo>(predicate);
            return (!findServiceWithKey ? (info2 ?? source.LastOrDefault<ServiceInfo>()) : info);
        }

        protected virtual object GetServiceCore(Type type, string key, ServiceSearchMode searchMode, out bool serviceHasKey)
        {
            ServiceInfo info;
            this.CheckServiceType(type);
            List<ServiceInfo> services = this.services;
            lock (services)
            {
                info = this.FindService(type, key, searchMode);
            }
            if (info == null)
            {
                serviceHasKey = false;
                return null;
            }
            serviceHasKey = info.HasKey;
            return info.Service;
        }

        [IteratorStateMachine(typeof(<GetServicesCore>d__11))]
        protected virtual IEnumerable<object> GetServicesCore(Type type, bool localOnly)
        {
            IEnumerator<object> <>7__wrap2;
            IEnumerable<ServiceInfo> enumerable = null;
            List<ServiceInfo> services = this.services;
            lock (services)
            {
                enumerable = (from x in this.services
                    where x.Is(type)
                    select x).ToList<ServiceInfo>();
            }
            IEnumerator<ServiceInfo> enumerator = enumerable.GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                ServiceInfo current = enumerator.Current;
                yield return current.Service;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                if (!localOnly && (this.ParentServiceContainer != null))
                {
                    <>7__wrap2 = this.ParentServiceContainer.GetServices(type, false).GetEnumerator();
                }
            }
            while (true)
            {
                if (<>7__wrap2.MoveNext())
                {
                    object current = <>7__wrap2.Current;
                    yield return current;
                }
                else
                {
                    <>7__wrap2 = null;
                }
            }
        }

        protected void RemoveCore(ServiceInfo info)
        {
            if (info != null)
            {
                List<ServiceInfo> services = this.services;
                lock (services)
                {
                    this.services.Remove(info);
                }
            }
        }

        protected abstract IServiceContainer ParentServiceContainer { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ServiceContainerBase.<>c <>9 = new ServiceContainerBase.<>c();
            public static Func<ServiceContainerBase.ServiceInfo, bool> <>9__14_2;

            internal bool <GetLocalService>b__14_2(ServiceContainerBase.ServiceInfo x) => 
                !x.HasKey;
        }


        protected class ServiceInfo
        {
            private ServiceInfo(string key, bool hasKey, object service, bool yieldToParent)
            {
                this.Key = key;
                this.HasKey = hasKey;
                this.Service = service;
                this.YieldToParent = yieldToParent;
            }

            public static ServiceContainerBase.ServiceInfo Create(string key, object service, bool yieldToParent = false) => 
                (service != null) ? new ServiceContainerBase.ServiceInfo(key, !string.IsNullOrEmpty(key), service, yieldToParent) : null;

            public static ServiceContainerBase.ServiceInfo Create(IServiceContainer container, Type t, string key, ServiceSearchMode searchMode)
            {
                bool flag;
                object service = container.GetService(t, key, searchMode, out flag);
                return ((service != null) ? new ServiceContainerBase.ServiceInfo(key, flag, service, false) : null);
            }

            public bool Is(Type t) => 
                t.IsAssignableFrom(this.ServiceType);

            public bool HasKey { get; private set; }

            public string Key { get; private set; }

            public object Service { get; private set; }

            public Type ServiceType
            {
                get
                {
                    Func<object, Type> evaluator = <>c.<>9__16_0;
                    if (<>c.<>9__16_0 == null)
                    {
                        Func<object, Type> local1 = <>c.<>9__16_0;
                        evaluator = <>c.<>9__16_0 = x => x.GetType();
                    }
                    return this.Service.With<object, Type>(evaluator);
                }
            }

            public bool YieldToParent { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ServiceContainerBase.ServiceInfo.<>c <>9 = new ServiceContainerBase.ServiceInfo.<>c();
                public static Func<object, Type> <>9__16_0;

                internal Type <get_ServiceType>b__16_0(object x) => 
                    x.GetType();
            }
        }
    }
}

