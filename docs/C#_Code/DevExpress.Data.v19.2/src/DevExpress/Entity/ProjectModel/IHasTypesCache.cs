namespace DevExpress.Entity.ProjectModel
{
    using System;

    public interface IHasTypesCache
    {
        void Add(IDXTypeInfo typeInfo);
        void ClearCache();
        bool Contains(IDXTypeInfo typeInfo);
        IDXTypeInfo GetTypeInfo(string fullName);
        void Remove(IDXTypeInfo typeInfo);
    }
}

