namespace DevExpress.Entity.ProjectModel
{
    using System;
    using System.Collections.Generic;

    public interface IDXAssemblyInfo : IHasTypesCache, IHasName
    {
        string AssemblyFullName { get; }

        IEnumerable<IDXTypeInfo> TypesInfo { get; }

        bool IsProjectAssembly { get; }

        IResourceOptions ResourceOptions { get; }

        bool IsSolutionAssembly { get; }
    }
}

