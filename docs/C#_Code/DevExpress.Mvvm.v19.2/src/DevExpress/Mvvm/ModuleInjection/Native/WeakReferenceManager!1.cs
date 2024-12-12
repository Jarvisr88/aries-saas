namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class WeakReferenceManager<T> where T: class
    {
        private readonly List<WeakReference> serviceReferences;

        public WeakReferenceManager()
        {
            this.serviceReferences = new List<WeakReference>();
        }

        public void Add(T service)
        {
            this.UpdateServiceReferences();
            this.AddCore(service);
        }

        protected void AddCore(T service)
        {
            if ((service != null) && (this.serviceReferences.FirstOrDefault<WeakReference>(x => (x.Target == service)) == null))
            {
                this.serviceReferences.Add(new WeakReference(service));
            }
        }

        public bool Contains(T service) => 
            this.serviceReferences.FirstOrDefault<WeakReference>(x => (x.Target == service)) != null;

        public IEnumerable<T> Get()
        {
            this.UpdateServiceReferences();
            Func<WeakReference, object> selector = <>c<T>.<>9__5_0;
            if (<>c<T>.<>9__5_0 == null)
            {
                Func<WeakReference, object> local1 = <>c<T>.<>9__5_0;
                selector = <>c<T>.<>9__5_0 = x => x.Target;
            }
            return this.serviceReferences.Select<WeakReference, object>(selector).OfType<T>();
        }

        public void Remove(T service)
        {
            this.UpdateServiceReferences();
            this.RemoveCore(service);
        }

        protected void RemoveCore(T service)
        {
            if (service != null)
            {
                WeakReference item = this.serviceReferences.FirstOrDefault<WeakReference>(x => x.Target == service);
                if (item != null)
                {
                    this.serviceReferences.Remove(item);
                }
            }
        }

        protected virtual void UpdateServiceReferences()
        {
            List<WeakReference> list = new List<WeakReference>();
            foreach (WeakReference reference in this.serviceReferences)
            {
                if (!reference.IsAlive)
                {
                    list.Add(reference);
                }
            }
            foreach (WeakReference reference2 in list)
            {
                this.serviceReferences.Remove(reference2);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WeakReferenceManager<T>.<>c <>9;
            public static Func<WeakReference, object> <>9__5_0;

            static <>c()
            {
                WeakReferenceManager<T>.<>c.<>9 = new WeakReferenceManager<T>.<>c();
            }

            internal object <Get>b__5_0(WeakReference x) => 
                x.Target;
        }
    }
}

