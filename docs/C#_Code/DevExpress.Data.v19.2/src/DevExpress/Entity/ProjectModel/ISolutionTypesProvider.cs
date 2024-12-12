namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections.Generic;

    public interface ISolutionTypesProvider : IHasTypesCache
    {
        void AddReference(string assemblyName);
        void AddReferenceFromFile(string assemblyPath);
        IDXTypeInfo FindType(Predicate<IDXTypeInfo> predicate);
        IDXTypeInfo FindType(string fullName);
        IEnumerable<IDXTypeInfo> FindTypes(Predicate<IDXTypeInfo> predicate);
        IEnumerable<IDXTypeInfo> FindTypes(IDXTypeInfo baseClass, Predicate<IDXTypeInfo> predicate);
        IDXAssemblyInfo GetAssembly(string assemblyName);
        string GetAssemblyReferencePath(string projectAssemblyFullName, string referenceName);
        IProjectTypes GetProjectTypes(string assemblyFullName);
        IEnumerable<IDXTypeInfo> GetTypes();
        bool IsReferenceExists(string assemblyName);

        IProjectTypes ActiveProjectTypes { get; }
    }
}

