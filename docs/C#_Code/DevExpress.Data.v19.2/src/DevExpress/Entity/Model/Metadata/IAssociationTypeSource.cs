namespace DevExpress.Entity.Model.Metadata
{
    using System;

    public interface IAssociationTypeSource
    {
        AssociationTypeInfo GetAssociationTypeFromCSpace(string fullName);
    }
}

