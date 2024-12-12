namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.Metadata;
    using System;

    public interface IEdmTypeInfo
    {
        DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind { get; }

        Type ClrType { get; }

        string Name { get; }

        IEdmTypeInfo CollectionElementType { get; }

        object Value { get; }
    }
}

