namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ResourceDictionaryEx : ResourceDictionary
    {
        private static readonly object Locker = new object();
        private static readonly Dictionary<Uri, WeakReference> SharedCache = new Dictionary<Uri, WeakReference>();
        private Uri sourceCore;

        private void CacheSource()
        {
            object locker = Locker;
            lock (locker)
            {
                SharedCache[this.sourceCore] = new WeakReference(this, false);
            }
        }

        private void InvalidateCache()
        {
            bool flag;
            object locker = Locker;
            lock (locker)
            {
                flag = SharedCache.ContainsKey(this.sourceCore);
            }
            if (!flag || this.DisableCache)
            {
                base.Source = this.sourceCore;
                if (!this.DisableCache)
                {
                    this.CacheSource();
                }
            }
            else
            {
                ResourceDictionary target;
                object obj3 = Locker;
                lock (obj3)
                {
                    target = (ResourceDictionary) SharedCache[this.sourceCore].Target;
                }
                if (target != null)
                {
                    base.MergedDictionaries.Add(target);
                    this.sourceCore = target.Source;
                }
                else
                {
                    base.Source = this.sourceCore;
                    this.CacheSource();
                }
            }
        }

        public bool DisableCache { get; set; }

        public Uri Source
        {
            get => 
                this.sourceCore;
            set
            {
                this.sourceCore = value;
                this.InvalidateCache();
            }
        }
    }
}

