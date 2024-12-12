namespace DevExpress.Printing.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;

    internal class NonSafeServiceContainer : ServiceContainer
    {
        private Dictionary<Type, Func<object>> serviceCreators;
        private Dictionary<string, object> serviceInstances;
        private IThreadingServices threadingServices;

        public NonSafeServiceContainer() : this(new DefaultThreadingServices())
        {
        }

        internal NonSafeServiceContainer(IThreadingServices threadingServices)
        {
            this.serviceCreators = new Dictionary<Type, Func<object>>();
            this.serviceInstances = new Dictionary<string, object>();
            this.threadingServices = threadingServices;
        }

        public void AddNonSafeService(Type service, Func<object> creator)
        {
            this.serviceCreators.Add(service, creator);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                foreach (object obj2 in this.serviceInstances.Values)
                {
                    if (obj2 is IDisposable)
                    {
                        ((IDisposable) obj2).Dispose();
                    }
                }
                this.serviceInstances.Clear();
            }
        }

        public override object GetService(Type serviceType)
        {
            object obj2;
            object obj3;
            if (!this.serviceCreators.ContainsKey(serviceType))
            {
                return base.GetService(serviceType);
            }
            string key = $"{serviceType.FullName}-{this.threadingServices.CurrentThreadID}";
            if (this.serviceInstances.TryGetValue(key, out obj2) && (obj2 != null))
            {
                return obj2;
            }
            this.serviceInstances[key] = obj3 = this.serviceCreators[serviceType]();
            return obj3;
        }

        public override void RemoveService(Type serviceType, bool promote)
        {
            if (!this.serviceCreators.ContainsKey(serviceType))
            {
                base.RemoveService(serviceType, promote);
            }
            else
            {
                (from pair in this.serviceInstances
                    where pair.Value.GetType() == serviceType
                    select pair).ToList<KeyValuePair<string, object>>().ForEach(pair => this.serviceInstances.Remove(pair.Key));
            }
        }
    }
}

