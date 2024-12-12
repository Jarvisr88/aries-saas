namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public interface IBoundProperty
    {
        string Name { get; }

        string DisplayName { get; }

        System.Type Type { get; }

        bool HasChildren { get; }

        List<IBoundProperty> Children { get; }

        bool IsAggregate { get; }

        bool IsList { get; }

        IBoundProperty Parent { get; }
    }
}

