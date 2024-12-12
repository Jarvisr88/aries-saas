namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;

    public interface IEdmPropertyInfo
    {
        IEdmPropertyInfo AddAttributes(IEnumerable<Attribute> newAttributes);

        string Name { get; }

        string DisplayName { get; }

        Type PropertyType { get; }

        bool IsForeignKey { get; }

        bool IsReadOnly { get; }

        DataColumnAttributes Attributes { get; }

        object ContextObject { get; }

        bool IsNavigationProperty { get; }
    }
}

