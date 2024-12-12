namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections.Generic;

    public class HasTypesCacheBase : IHasTypesCache
    {
        private Dictionary<string, IDXTypeInfo> cache;

        public virtual void Add(IDXTypeInfo typeInfo)
        {
            if ((typeInfo != null) && !string.IsNullOrEmpty(typeInfo.FullName))
            {
                this.Cache[typeInfo.FullName] = typeInfo;
            }
        }

        public void AddRange(IEnumerable<IDXTypeInfo> typesInfo)
        {
            if (typesInfo != null)
            {
                foreach (IDXTypeInfo info in typesInfo)
                {
                    this.Add(info);
                }
            }
        }

        public void ClearCache()
        {
            if (this.cache != null)
            {
                this.cache = null;
            }
        }

        public bool Contains(IDXTypeInfo typeInfo) => 
            (typeInfo != null) && (!string.IsNullOrEmpty(typeInfo.FullName) && (this.GetTypeInfo(typeInfo.FullName) != null));

        public IDXTypeInfo GetTypeInfo(string fullName)
        {
            IDXTypeInfo info;
            return (!string.IsNullOrEmpty(fullName) ? (!this.Cache.TryGetValue(fullName, out info) ? null : info) : null);
        }

        public void Remove(IDXTypeInfo typeInfo)
        {
            if (((typeInfo != null) && !this.IsCacheEmpty) && this.Cache.ContainsKey(typeInfo.FullName))
            {
                this.Cache.Remove(typeInfo.FullName);
            }
        }

        protected Dictionary<string, IDXTypeInfo> Cache
        {
            get
            {
                this.cache ??= new Dictionary<string, IDXTypeInfo>();
                return this.cache;
            }
        }

        protected bool IsCacheEmpty =>
            (this.cache == null) || (this.cache.Count == 0);

        public IEnumerable<IDXTypeInfo> TypesInfo =>
            !this.IsCacheEmpty ? ((IEnumerable<IDXTypeInfo>) this.Cache.Values) : ((IEnumerable<IDXTypeInfo>) new IDXTypeInfo[0]);
    }
}

