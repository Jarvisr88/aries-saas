namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;

    public interface IEntitySetInfo
    {
        IEntityTypeInfo ElementType { get; }

        bool IsView { get; }

        bool ReadOnly { get; }

        string Name { get; }

        IEntityContainerInfo EntityContainerInfo { get; }

        Dictionary<string, object> AttachedInfo { get; }
    }
}

