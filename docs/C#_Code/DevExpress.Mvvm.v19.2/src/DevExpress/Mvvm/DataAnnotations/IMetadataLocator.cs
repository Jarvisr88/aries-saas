namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public interface IMetadataLocator
    {
        Type[] GetMetadataTypes(Type type);
    }
}

