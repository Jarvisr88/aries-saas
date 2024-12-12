namespace DevExpress.Utils.IoC
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class FactoryRegistration : Registration, IDisposable
    {
        private readonly Func<object> factory;
        private readonly object syncRoot = new object();
        private bool isCachedInstanceInitialized;
        private object cachedInstance;

        public FactoryRegistration(Func<object> factory)
        {
            Guard.ArgumentNotNull(factory, "factory");
            this.factory = factory;
        }

        public void AsTransient()
        {
            this.Transient = true;
        }

        public void Dispose()
        {
            using (this.cachedInstance as IDisposable)
            {
            }
            this.cachedInstance = null;
            this.isCachedInstanceInitialized = false;
        }

        public object Instance
        {
            get
            {
                if (this.Transient)
                {
                    return this.factory();
                }
                if (!this.isCachedInstanceInitialized)
                {
                    object syncRoot = this.syncRoot;
                    lock (syncRoot)
                    {
                        if (!this.isCachedInstanceInitialized)
                        {
                            this.isCachedInstanceInitialized = true;
                            this.cachedInstance = this.factory();
                        }
                    }
                }
                return this.cachedInstance;
            }
        }

        public bool Transient { get; private set; }
    }
}

