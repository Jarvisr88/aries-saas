namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.Metadata;
    using System;

    public interface IEdmMemberInfo
    {
        string Name { get; }

        bool IsProperty { get; }

        bool IsNavigationProperty { get; }

        bool IsCollectionProperty { get; }

        DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind { get; }

        object FromEndMember { get; }

        bool IsKeyMember { get; }

        IPrimitiveType PrimitiveType { get; }
    }
}

