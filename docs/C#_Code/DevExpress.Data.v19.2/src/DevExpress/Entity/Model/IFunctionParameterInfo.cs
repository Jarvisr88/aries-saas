namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.Metadata;
    using System;

    public interface IFunctionParameterInfo
    {
        string Name { get; }

        DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind { get; }

        string TypeName { get; }

        IEdmTypeInfo EdmType { get; }

        Type ClrType { get; }
    }
}

