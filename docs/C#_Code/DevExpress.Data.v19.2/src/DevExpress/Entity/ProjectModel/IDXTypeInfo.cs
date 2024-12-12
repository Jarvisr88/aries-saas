namespace DevExpress.Entity.ProjectModel
{
    using System;

    public interface IDXTypeInfo : IHasName
    {
        Type ResolveType();

        string NamespaceName { get; }

        string FullName { get; }

        IDXAssemblyInfo Assembly { get; set; }

        bool IsSolutionType { get; }
    }
}

