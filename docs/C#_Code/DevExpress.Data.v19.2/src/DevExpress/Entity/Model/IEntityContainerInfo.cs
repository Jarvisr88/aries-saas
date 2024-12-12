namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;

    public interface IEntityContainerInfo
    {
        string Name { get; }

        IEnumerable<IEntitySetInfo> EntitySets { get; }

        IEnumerable<IEntityFunctionInfo> EntityFunctions { get; }
    }
}

