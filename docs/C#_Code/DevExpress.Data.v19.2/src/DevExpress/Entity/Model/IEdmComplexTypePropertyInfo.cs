namespace DevExpress.Entity.Model
{
    using System;

    public interface IEdmComplexTypePropertyInfo
    {
        string Name { get; }

        Type ClrType { get; }
    }
}

