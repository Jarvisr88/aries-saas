namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Collections.Generic;

    public interface IDbContainerInfo : IContainerInfo, IDXTypeInfo, IHasName
    {
        IEntityContainerInfo EntityContainer { get; }

        IEnumerable<IEntitySetInfo> EntitySets { get; }

        IEnumerable<IEntityFunctionInfo> EntityFunctions { get; }

        object MetadataWorkspace { get; }

        Type DbContextType { get; }

        string SourceUrl { get; set; }
    }
}

