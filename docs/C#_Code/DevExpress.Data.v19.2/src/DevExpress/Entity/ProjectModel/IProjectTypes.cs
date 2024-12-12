namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IProjectTypes : IEnumerable<IDXAssemblyInfo>, IEnumerable
    {
        IDXTypeInfo GetExistingOrCreateNew(Type type);
        IEnumerable<IDXTypeInfo> GetTypes(Func<IDXTypeInfo, bool> filter);
        IEnumerable<IDXAssemblyInfo> GetTypesPerAssembly(Func<IDXTypeInfo, bool> filter);

        IEnumerable<IDXAssemblyInfo> Assemblies { get; }

        string ProjectAssemblyName { get; }

        IDXAssemblyInfo ProjectAssembly { get; }
    }
}

