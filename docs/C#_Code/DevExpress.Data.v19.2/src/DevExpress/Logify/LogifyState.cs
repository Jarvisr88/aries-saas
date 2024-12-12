namespace DevExpress.Logify
{
    using System;
    using System.Collections.Generic;

    public class LogifyState
    {
        private static LogifyState instance;
        private readonly Dictionary<string, string> customData = new Dictionary<string, string>();
        private readonly List<WeakReference> customDataProviders = new List<WeakReference>();

        public void AddCustomDataProvider(ILogifyCustomDataProvider provider)
        {
            bool flag = true;
            for (int i = this.customDataProviders.Count - 1; i >= 0; i--)
            {
                WeakReference reference = this.customDataProviders[i];
                if (!reference.IsAlive)
                {
                    this.customDataProviders.RemoveAt(i);
                }
                else
                {
                    ILogifyCustomDataProvider target = reference.Target as ILogifyCustomDataProvider;
                    if (target == null)
                    {
                        this.customDataProviders.RemoveAt(i);
                    }
                    else if (ReferenceEquals(target, provider))
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                this.customDataProviders.Add(new WeakReference(provider));
            }
        }

        internal void ExecuteCustomDataProviders()
        {
            for (int i = this.customDataProviders.Count - 1; i >= 0; i--)
            {
                WeakReference reference = this.customDataProviders[i];
                if (!reference.IsAlive)
                {
                    this.customDataProviders.RemoveAt(i);
                }
                else
                {
                    ILogifyCustomDataProvider target = reference.Target as ILogifyCustomDataProvider;
                    if (target == null)
                    {
                        this.customDataProviders.RemoveAt(i);
                    }
                    else
                    {
                        target.CollectCustomData(this.CustomData);
                    }
                }
            }
        }

        public void RemoveCustomDataProvider(ILogifyCustomDataProvider provider)
        {
            for (int i = this.customDataProviders.Count - 1; i >= 0; i--)
            {
                WeakReference reference = this.customDataProviders[i];
                if (!reference.IsAlive)
                {
                    this.customDataProviders.RemoveAt(i);
                }
                else
                {
                    ILogifyCustomDataProvider target = reference.Target as ILogifyCustomDataProvider;
                    if (target == null)
                    {
                        this.customDataProviders.RemoveAt(i);
                    }
                    else if (ReferenceEquals(target, provider))
                    {
                        this.customDataProviders.RemoveAt(i);
                    }
                }
            }
        }

        public static LogifyState Instance
        {
            get
            {
                if (instance == null)
                {
                    Type type = typeof(LogifyState);
                    lock (type)
                    {
                        if (instance == null)
                        {
                            instance = new LogifyState();
                        }
                        else
                        {
                            return instance;
                        }
                    }
                }
                return instance;
            }
        }

        public IDictionary<string, string> CustomData =>
            this.customData;
    }
}

